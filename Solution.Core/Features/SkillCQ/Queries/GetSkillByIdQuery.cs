using AutoMapper;
using Mapster;
using MediatR;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.SkillCQ.Queries
{
    public class GetSkillByIdQuery : IRequest<SkillVM>
    {
        public int skill_Id { get; set; }
    }

    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, SkillVM>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetSkillByIdHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SkillVM> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var dbSkill = await _context.Skills.FindAsync(request.skill_Id, cancellationToken);
            SkillVM returnModel = dbSkill.Adapt<SkillVM>();
            return returnModel;

            //return _mapper.Map<SkillVM>(_context.Skills.FindAsync(request.skill_Id, cancellationToken));
        }
    }
}
