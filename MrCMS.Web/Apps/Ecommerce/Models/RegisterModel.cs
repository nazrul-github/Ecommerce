using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MrCMS.Models.Auth;

namespace MrCMS.Web.Apps.Ecommerce.Models
{
    public class RegisterWithoutDetailsModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(128, MinimumLength = 5)]
        [Remote("CheckEmailIsNotRegistered", "Registration", ErrorMessage = "This email is already registered.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage =
            "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public LoginModel ToLoginModel()
        {
            return new LoginModel
            {
                Email = Email,
                Password = Password,
                RememberMe = false
            };
        }
    }
}