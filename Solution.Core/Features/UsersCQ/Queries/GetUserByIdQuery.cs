using AutoMapper;
using MediatR;
using Solution.Core.Features.UsersCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.UsersCQ.Queries
{
    public class GetUserByIdQuery : IRequest<UsersVM>
    {
        public int User_Id { get; set; }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UsersVM>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersVM> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UsersVM>(await _context.Users.FindAsync(request.User_Id, cancellationToken));
        }
    }
}
