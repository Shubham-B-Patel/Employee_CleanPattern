using MediatR;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.SkillCQ.Commands
{
    public class UpdateSkillCommand : IRequest<int>
    {
        public UpdateSkillCommand(UpdateSkillVM updateSkillVM)
        {
            this.updateSkillVM = updateSkillVM;
        }

        public UpdateSkillVM updateSkillVM { get; set; }
    }

    public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, int>
    {
        private readonly IEmployeeDbContext _context;

        public UpdateSkillHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var dbSkill = await _context.Skills.FindAsync(request.updateSkillVM.Skill_Id);
            if (dbSkill == null)
            {
                return -1;
            }

            dbSkill.Skill_Id = request.updateSkillVM.Skill_Id;
            dbSkill.Tech_Name = request.updateSkillVM.Tech_Name;
            dbSkill.Work_Exp = request.updateSkillVM.Work_Exp;
            dbSkill.Rating = request.updateSkillVM.Rating;


            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                return res;
            }
            return 0;
        }
    }
}
