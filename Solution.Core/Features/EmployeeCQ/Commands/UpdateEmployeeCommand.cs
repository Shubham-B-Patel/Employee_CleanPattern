using MediatR;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Commands
{
    public class UpdateEmployeeCommand : IRequest<int>
    {
        public UpdateEmployeeCommand(EmployeeVM employeeVM)
        {
            this.employeeVM = employeeVM;
        }

        public EmployeeVM employeeVM { get; set; }
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
            var dbEmployee = await _context.Employees.FindAsync(request.employeeVM.Employee_Id, cancellationToken); ;
            if (dbEmployee == null)
            {
                return -1;
            }
            dbEmployee.First_Name = request.employeeVM.First_Name;
            dbEmployee.Last_Name = request.employeeVM.Last_Name;
            dbEmployee.Mobile_Number = request.employeeVM.Mobile_Number;
            dbEmployee.Email = request.employeeVM.Email;
            dbEmployee.DateOfBirth = request.employeeVM.DateOfBirth;
            dbEmployee.Is_Deleted = request.employeeVM.Is_Deleted;
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res == 0)
            {
                return 0;
            }
            return res;
        }
    }
}
