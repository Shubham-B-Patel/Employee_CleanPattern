using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.SkillCQ.Queries
{
    public class GetSkillListQuery : IRequest<List<SkillVM>>
    {

    }

    public class GetSkillListHandler : IRequestHandler<GetSkillListQuery, List<SkillVM>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetSkillListHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SkillVM>> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<SkillVM>>(await _context.Skills.ToListAsync());
            /*List<SkillVM> skillVM = new List<SkillVM>();
            var dbSkills = await _context.Skills.ToListAsync();

            foreach (var dbSkill in dbSkills)
            {
                var skill = new SkillVM
                {
                    Skill_Id = dbSkill.Skill_Id,
                    Tech_Name = dbSkill.Tech_Name,
                    Work_Exp = Convert.ToDecimal(dbSkill.Work_Exp),
                    Rating = Convert.ToDecimal(dbSkill.Rating),
                    Employee_Id = dbSkill.Employee_Id,
                };
                skillVM.Add(skill);
            }

            return skillVM;*/
        }
    }
}
