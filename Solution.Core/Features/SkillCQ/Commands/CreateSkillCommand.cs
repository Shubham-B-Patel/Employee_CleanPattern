using AutoMapper;
using MediatR;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;
using Solution.Domain.Entities._Employee;

namespace Solution.Core.Features.SkillCQ.Commands
{
    public class CreateSkillCommand : IRequest<int>
    {
        public CreateSkillCommand(AddSkillVM addSkillVM)
        {
            this.addSkillVM = addSkillVM;
        }

        public AddSkillVM addSkillVM { get; set; }
    }

    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public CreateSkillHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var dbEmployee = await _context.Employees.FindAsync(request.addSkillVM.Employee_Id, cancellationToken);
            if (dbEmployee == null)
            {
                return -1;
            }

            SkillVM skillVM = new SkillVM
            {
                Tech_Name = request.addSkillVM.Tech_Name,
                Work_Exp = Convert.ToDecimal(request.addSkillVM.Work_Exp),
                Rating = Convert.ToDecimal(request.addSkillVM.Rating),
                Employee_Id = dbEmployee.Employee_Id,
                Active = true,
                CreatedBy=dbEmployee.Employee_Id.ToString(),
                CreatedDate=DateTime.Now,
            };

            /*Skill skill = skillVM.Adapt<Skill>();
            await _context.Skills.AddAsync(skill, cancellationToken);*/

            await _context.Skills.AddAsync(_mapper.Map<Skill>(skillVM), cancellationToken);
            var res = await _context.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
