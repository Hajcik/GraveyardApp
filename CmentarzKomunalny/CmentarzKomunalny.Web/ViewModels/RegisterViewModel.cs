using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} lub maksymalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło oraz potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }

        // nie musi byc required bo jest ustalane przez kontroler AdminController
        [Display(Name = "Rola użytkownika")]
        public string Role { get; set; }


        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
   
        public bool IsSelected { get; set; }
        
        public string UserId { get; set; }
    
    }
}
