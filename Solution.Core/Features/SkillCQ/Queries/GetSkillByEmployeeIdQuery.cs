using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.SkillCQ.Queries
{
    public class GetSkillByEmployeeIdQuery : IRequest<List<SkillVM>>
    {
        public int Employee_Id { get; set; }
    }

    public class GetSkillByEmployeeIdHandler : IRequestHandler<GetSkillByEmployeeIdQuery, List<SkillVM>>
    {
        private readonly IEmployeeDbContext _context;

        public GetSkillByEmployeeIdHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<SkillVM>> Handle(GetSkillByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            List<SkillVM> skillVM = new List<SkillVM>();
            var dbSkills = await _context.Skills.Where(x => x.Employee_Id == request.Employee_Id).ToListAsync();

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

            return skillVM;
        }
    }
}