using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Mvc.PAL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage ="Invalid Email.")]
        public string Email { get; set; }
    }
}
