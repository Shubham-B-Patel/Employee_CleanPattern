using Solution.Domain.Comman;

namespace Solution.Core.Features.SkillCQ.ViewModels
{
    public class SkillVM : AuditableEntity
    {
        public int Skill_Id { get; set; }
        public string Tech_Name { get; set; }
        public decimal Work_Exp { get; set; }
        public decimal Rating { get; set; }
        public int Employee_Id { get; set; }
    }
}