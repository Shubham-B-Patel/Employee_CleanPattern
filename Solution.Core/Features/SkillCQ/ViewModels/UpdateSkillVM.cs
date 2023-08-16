namespace Solution.Core.Features.SkillCQ.ViewModels
{
    public class UpdateSkillVM
    {
        public int Employee_Id { get; set; }
        public int Skill_Id { get; set; }
        public string Tech_Name { get; set; }
        public decimal? Work_Exp { get; set; }
        public decimal? Rating { get; set; }
    }
}
