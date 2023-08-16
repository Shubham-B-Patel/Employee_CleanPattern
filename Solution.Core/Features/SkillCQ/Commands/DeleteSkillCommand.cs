using MediatR;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.SkillCQ.Commands
{
    public class DeleteSkillCommand : IRequest<int>
    {
        public int skill_Id { get; set; }
    }

    public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, int>
    {
        private readonly IEmployeeDbContext _context;

        public DeleteSkillHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var dbSkill = await _context.Skills.FindAsync(request.skill_Id);
            if (dbSkill == null)
            {
                return -1;
            }
            _context.Skills.Remove(dbSkill);
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                return res;
            }

            return 0;
        }
    }
}
