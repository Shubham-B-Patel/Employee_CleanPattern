using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Queries
{
    public class LoginEmployeeQuery : IRequest<EmployeeVM>
    {
        public LoginEmployeeQuery(LoginEmployeeVM loginEmployeeVM)
        {
            this.loginEmployeeVM = loginEmployeeVM;
        }

        public LoginEmployeeVM loginEmployeeVM { get; set; }
    }

    public class LoginEmployeeQueryHandler : IRequestHandler<LoginEmployeeQuery, EmployeeVM>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public LoginEmployeeQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeVM> Handle(LoginEmployeeQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmployeeVM>(await _context.Employees.FirstOrDefaultAsync(x => x.Employee_Name == request.loginEmployeeVM.Employee_Name && x.Password == request.loginEmployeeVM.Password));

        }
    }
}
