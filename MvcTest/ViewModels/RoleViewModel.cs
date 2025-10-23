using System.CodeDom;

namespace Mvc.PAL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
