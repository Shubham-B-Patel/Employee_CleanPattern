using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.UsersCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.UsersCQ.Queries
{
    public class LoginUserQuery : IRequest<ReturnUserVM>
    {
        public LoginUserQuery(LoginVM loginVM)
        {
            this.loginVM = loginVM;
        }

        public LoginVM loginVM { get; set; }
    }

    public class LoginQueryHandler : IRequestHandler<LoginUserQuery, ReturnUserVM>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReturnUserVM> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            ReturnUserVM returnUserVM = null;
            var dbUser = _mapper.Map<UsersVM>(await _context.Users.FirstOrDefaultAsync(x => x.User_Name == request.loginVM.User_Name && x.Password == request.loginVM.Password));
            if (dbUser == null)
            {
                return returnUserVM;
            }
            returnUserVM = new ReturnUserVM()
            {
                User_Id = dbUser.User_Id,
                First_Name = dbUser.First_Name,
                Last_Name = dbUser.Last_Name,
                User_Name = dbUser.User_Name,
                DateOfBirth = dbUser.DateOfBirth,
                Email = dbUser.Email,
                Mobile_Number = dbUser.Mobile_Number,
                Profile_Path = dbUser.Profile_Path
            };
            return returnUserVM;
        }
    }
}
