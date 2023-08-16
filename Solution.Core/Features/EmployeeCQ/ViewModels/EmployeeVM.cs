using Solution.Domain.Comman;

namespace Solution.Core.Features.EmployeeCQ.ViewModels
{
    public class EmployeeVM : AuditableEntity
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
    }
}
