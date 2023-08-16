using AutoMapper;
using Mapster;
using MediatR;
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
            EmployeeVM employeeVM = new EmployeeVM()
            {
                First_Name = request.addEmployeeVM.First_Name,
                Last_Name = request.addEmployeeVM.Last_Name,
                Mobile_Number = request.addEmployeeVM.Mobile_Number,
                Email = request.addEmployeeVM.Email,
                DateOfBirth = request.addEmployeeVM.DateOfBirth,
                Is_Deleted = false
            };

            /*Employee employee = employeeVM.Adapt<Employee>();

            await _context.Employees.AddAsync(employee, cancellationToken);*/

            await _context.Employees.AddAsync(_mapper.Map<Employee>(employeeVM), cancellationToken);

            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
