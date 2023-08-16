using AutoMapper;
using Solution.Core.Features.EmployeeCQ.ViewModels;
using Solution.Core.Features.SkillCQ.ViewModels;
using Solution.Core.Features.UsersCQ.ViewModels;
using Solution.Domain.Entities._Employee;
using Solution.Domain.Entities._User;

namespace Solution.Core.Comman.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<SourceType, DestinationType>();
            CreateMap<Employee, EmployeeVM>();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Skill, SkillVM>();
            CreateMap<Skill, SkillVM>().ReverseMap();
            CreateMap<Users, UsersVM>();
            CreateMap<Users, UsersVM>().ReverseMap();
        }
    }
}
