using AutoMapper;
using Mvc.DAL.Models;
using Mvc.PAL.ViewModels;

namespace Mvc.PAL.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,userViewModel>().ReverseMap();
        }
    }
}
