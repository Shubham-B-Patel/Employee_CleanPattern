using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Queries
{
    public class GetEmployeeListQuery : IRequest<List<EmployeeVM>>
    {

    }

    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeVM>>
    {
        private readonly IEmployeeDbContext _context;

        public GetEmployeeListHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeVM>> Handle(GetEmployeeListQuery query, CancellationToken cancellationToken)
        {
            var employeeList = new List<EmployeeVM>();
            var dbEmployees = await _context.Employees.AsNoTracking().ToListAsync();

            foreach (var employee in dbEmployees)
            {
                var emp = new EmployeeVM
                {
                    Employee_Id = employee.Employee_Id,
                    First_Name = employee.First_Name,
                    Last_Name = employee.Last_Name,
                    Mobile_Number = employee.Mobile_Number,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Is_Deleted = employee.Is_Deleted
                };
                employeeList.Add(emp);
            }

            return employeeList;
        }
    }
}
