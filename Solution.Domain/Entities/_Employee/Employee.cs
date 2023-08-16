using Solution.Domain.Comman;

namespace Solution.Domain.Entities._Employee
{
    public class Employee : AuditableEntity
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Employee_Name { get; set; }
        public string Password { get; set; }
        public string Mobile_Number { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean Is_Deleted { get; set; }
        public List<Skill>? Skills { get; set; }  
    }
}
