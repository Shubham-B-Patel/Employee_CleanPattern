using AutoMapper;
using MediatR;
using Solution.Core.Features.EmployeeCQ.Helper;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;
using Solution.Domain.Entities._Employee;

namespace Solution.Core.Features.EmployeeCQ.Commands
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public AddEmployeeVM addEmployeeVM { get; set; }
    }

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Random ran = new Random();
            var str = "abcdefghijklmnopqrstuvwxyz";
            var password = "";
            for (int i = 0; i < 7; i++)
            {
                password = password + str[ran.Next(0, 26)];
            }

            var dob = request.addEmployeeVM.DateOfBirth.ToString();
            var newdob = dob.Substring(0, 10);
            newdob = newdob.Replace("/", "");
            newdob = newdob.Replace("-", "");
            newdob = newdob.Replace(" ", "");
            var User_Name = "Employee_" + request.addEmployeeVM.First_Name + request.addEmployeeVM.Last_Name[0] + newdob;
            User_Name = User_Name.ToUpper();

            EmployeeVM employeeVM = new EmployeeVM()
            {
                First_Name = request.addEmployeeVM.First_Name,
                Last_Name = request.addEmployeeVM.Last_Name,
                Employee_Name = User_Name,
                Password = password,
                Mobile_Number = request.addEmployeeVM.Mobile_Number,
                Email = request.addEmployeeVM.Email,
                DateOfBirth = request.addEmployeeVM.DateOfBirth,
                Is_Deleted = false,
                CreatedBy=request.addEmployeeVM.User_Id.ToString(),
                CreatedDate=DateTime.Now,
            };

            /*Employee employee = employeeVM.Adapt<Employee>();

            await _context.Employees.AddAsync(employee, cancellationToken);*/

            await _context.Employees.AddAsync(_mapper.Map<Employee>(employeeVM), cancellationToken);

            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                var name = request.addEmployeeVM.First_Name + " " + request.addEmployeeVM.Last_Name;
                EmployeeMailService obj = new EmployeeMailService(name, request.addEmployeeVM.Email, User_Name, password);
                obj.SendAddEmployeeMail();
                return 1;
            }
            return 0;
        }
    }
}
