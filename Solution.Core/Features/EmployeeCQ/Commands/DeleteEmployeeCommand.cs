using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Commands
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public int Employee_Id { get; set; }
    }
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployeeDbContext _context;

        public DeleteEmployeeHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dbEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Employee_Id == request.Employee_Id);
            if (dbEmployee != null)
            {
                dbEmployee.Is_Deleted = true;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
    }

}
