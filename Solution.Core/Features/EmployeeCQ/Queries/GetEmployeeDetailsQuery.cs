using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Interfaces.Comman;

namespace Solution.Core.Features.EmployeeCQ.Queries
{
    public class GetEmployeeDetailsQuery : IRequest<EmployeeDetailsVM>
    {
        public int Employee_Id { get; set; }
    }

    public class GetEmployeeDetailsHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsVM>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeDetailsHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsVM> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            EmployeeDetailsVM employeeDetailsVM = null;
            var dbEmployee = await _context.Employees.FindAsync(request.Employee_Id, cancellationToken);
            if (dbEmployee == null)
            {
                return employeeDetailsVM;
            }

            var dbSkills = await _context.Skills.Where(x => x.Employee_Id == dbEmployee.Employee_Id).ToListAsync();
            List<SkillVM> skillVM = new List<SkillVM>();
            foreach (var skill in dbSkills)
            {
                var returnskill = new SkillVM()
                {
                    Skill_Id = skill.Skill_Id,
                    Tech_Name = skill.Tech_Name,
                    Work_Exp = Convert.ToDecimal(skill.Work_Exp),
                    Rating = Convert.ToDecimal(skill.Rating),
                    Employee_Id = skill.Employee_Id,
                };
                skillVM.Add(returnskill);
            }

            employeeDetailsVM = new EmployeeDetailsVM
            {
                Employee_Id = dbEmployee.Employee_Id,
                First_Name = dbEmployee.First_Name,
                Last_Name = dbEmployee.Last_Name,
                Employee_Name = dbEmployee.Employee_Name,
                Password = dbEmployee.Password,
                Mobile_Number = dbEmployee.Mobile_Number,
                Email = dbEmployee.Email,
                DateOfBirth = dbEmployee.DateOfBirth,
                Is_Deleted = dbEmployee.Is_Deleted,
                Skills = skillVM
            };

            return employeeDetailsVM;
        }
    }
}
