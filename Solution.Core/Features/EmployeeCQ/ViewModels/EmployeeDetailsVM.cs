﻿using Solution.Core.Features.SkillCQ.ViewModels;

namespace Solution.Core.Features.EmployeeCQ.ViewModels
{
    public class EmployeeDetailsVM
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mobile_Number { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean Is_Deleted { get; set; }
        public List<SkillVM> Skills { get; set; }
    }
}
