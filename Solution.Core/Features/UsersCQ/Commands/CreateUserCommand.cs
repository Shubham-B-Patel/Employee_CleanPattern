using AutoMapper;
using MediatR;
using Solution.Core.Features.UsersCQ.Helper;
using Solution.Core.Features.UsersCQ.ViewModels;
using Solution.Core.Interfaces.Comman;
using Solution.Domain.Entities._User;

namespace Solution.Core.Features.UsersCQ.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(AddUserVM addUserVM)
        {
            this.addUserVM = addUserVM;
        }

        public AddUserVM addUserVM { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Random ran = new Random();
            var str = "abcdefghijklmnopqrstuvwxyz";
            var password = "";
            for (int i = 0; i < 7; i++)
            {
                password = password + str[ran.Next(0, 26)];
            }

            var dob = request.addUserVM.DateOfBirth.ToString();
            var newdob = dob.Substring(0, 10);
            newdob = newdob.Replace("/", "");
            newdob = newdob.Replace("-", "");
            newdob = newdob.Replace(" ", "");
            var User_Name = "User_" + request.addUserVM.First_Name + request.addUserVM.Last_Name[0] + newdob;
            User_Name = User_Name.ToUpper();

            UsersVM usersVM = new UsersVM()
            {
                First_Name = request.addUserVM.First_Name,
                Last_Name = request.addUserVM.Last_Name,
                User_Name = User_Name,
                DateOfBirth = request.addUserVM.DateOfBirth,
                Email = request.addUserVM.Email,
                Mobile_Number = request.addUserVM.Mobile_Number,
                Password = password,
                Profile_Path = ""
            };


            await _context.Users.AddAsync(_mapper.Map<Users>(usersVM), cancellationToken);
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                var name = request.addUserVM.First_Name + " " + request.addUserVM.Last_Name;
                UserMailService obj = new UserMailService(name, request.addUserVM.Email, User_Name, password);
                obj.SendRegisterMail();

                return 1;
            }

            return 0;
        }
    }
}