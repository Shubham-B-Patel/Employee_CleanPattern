using Solution.Domain.Comman;

namespace Solution.Domain.Entities._User
{
    public class Users : AuditableEntity
    {
        public int User_Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string User_Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile_Number { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Profile_Path { get; set; }
    }
}