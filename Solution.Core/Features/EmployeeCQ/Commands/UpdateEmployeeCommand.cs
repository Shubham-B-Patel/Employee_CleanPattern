using MediatR;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Commands
{
    public class UpdateEmployeeCommand : IRequest<int>
    {
        public UpdateEmployeeCommand(PutEmployeeVM putEmployeeVM)
        {
            this.putEmployeeVM = putEmployeeVM;
        }

        public PutEmployeeVM putEmployeeVM { get; set; }
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeDbContext _context;

        public UpdateEmployeeHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dbEmployee = await _context.Employees.FindAsync(request.putEmployeeVM.Employee_Id, cancellationToken); ;
            if (dbEmployee == null)
            {
                return -1;
            }
            dbEmployee.First_Name = request.putEmployeeVM.First_Name;
            dbEmployee.Last_Name = request.putEmployeeVM.Last_Name;
            dbEmployee.Mobile_Number = request.putEmployeeVM.Mobile_Number;
            dbEmployee.Email = request.putEmployeeVM.Email;
            dbEmployee.DateOfBirth = request.putEmployeeVM.DateOfBirth;
            dbEmployee.Is_Deleted = request.putEmployeeVM.Is_Deleted;
            dbEmployee.LastModifiedBy = request.putEmployeeVM.User_Id.ToString();
            dbEmployee.LastModifiedDate=DateTime.Now;
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res == 0)
            {
                return 0;
            }
            return res;
        }
    }
}