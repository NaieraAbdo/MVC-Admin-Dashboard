using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mvc.PAL.ViewModels;

namespace Mvc.PAL.MappingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>().ForMember(d => d.RoleName, O => O.MapFrom(S => S.Name))
                .ReverseMap();
        }
    }
}
