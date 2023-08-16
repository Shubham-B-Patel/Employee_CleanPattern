using MediatR;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeVM>
    {
        public int Employee_Id { get; set; }
    }

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeVM>
    {
        private readonly IEmployeeDbContext _context;

        public GetEmployeeByIdHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeVM> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var dbEmployee = await _context.Employees.FindAsync(request.Employee_Id);

            EmployeeVM returnEmployee = null;
            if (dbEmployee == null)
            {
                return returnEmployee;
            }
            returnEmployee = new EmployeeVM
            {
                Employee_Id = request.Employee_Id,
                First_Name = dbEmployee.First_Name,
                Last_Name = dbEmployee.Last_Name,
                Mobile_Number = dbEmployee.Mobile_Number,
                Email = dbEmployee.Email,
                DateOfBirth = dbEmployee.DateOfBirth,
                Is_Deleted = dbEmployee.Is_Deleted
            };

            return returnEmployee;
        }
    }
}
