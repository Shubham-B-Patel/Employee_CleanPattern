using MediatR;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.UsersCQ.Commands
{
    public class AddProfilePathCommand : IRequest<int>
    {
        public AddProfilePathCommand(int user_Id, string profile_Path)
        {
            User_Id = user_Id;
            Profile_Path = profile_Path;
        }

        public int User_Id { get; set; }
        public string Profile_Path { get; set; }
    }

    public class AddProfilePathHandler : IRequestHandler<AddProfilePathCommand, int>
    {
        private readonly IEmployeeDbContext _context;

        public AddProfilePathHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddProfilePathCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.FindAsync(request.User_Id, cancellationToken);

            dbUser.Profile_Path = request.Profile_Path;
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
