using AutoMapper;
using Mvc.DAL.Models;
using Mvc.PAL.ViewModels;

namespace Mvc.PAL.MappingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
