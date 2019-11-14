using CinemaApp.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="RequiredMaleForm")]
        [Display(ResourceType = typeof(Strings), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="NotAnEmail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="RequiredMaleForm")]
        [Display(ResourceType = typeof(Strings), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="NotAnEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Strings), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Strings), Name = "RememberMeQuestion")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="Required")]
        [Display(ResourceType = typeof(Strings), Name = "Name")]
        [StringLength(500)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="Required")]
        [Display(ResourceType = typeof(Strings), Name = "Surname")]
        [StringLength(500)]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="RequiredMaleForm")]
        [EmailAddress(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="NotAnEmail")]
        [Display(ResourceType = typeof(Strings), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="Required")]
        [StringLength(100, ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="MinimumChars", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Strings), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Strings), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName="DifferentPasswords")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="RequiredMaleForm")]
        [EmailAddress(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="NotAnEmail")]
        [Display(ResourceType = typeof(Strings), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="Required")]
        [StringLength(100, ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="MinimumChars", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Strings), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Strings), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName="DifferentPasswords")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="RequiredMaleForm")]
        [EmailAddress(ErrorMessageResourceType =typeof(Errors), ErrorMessageResourceName ="NotAnEmail")]
        [Display(ResourceType = typeof(Strings), Name = "Email")]
        public string Email { get; set; }
    }
}
