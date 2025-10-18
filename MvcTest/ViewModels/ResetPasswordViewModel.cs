using System.ComponentModel.DataAnnotations;

namespace Mvc.PAL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="New Password is required.")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm New Password is required.")]
        [Compare("NewPassword",ErrorMessage ="Password doesn't match.")]

        public string ConfirmNewPassword { get; set; }
    }
}
