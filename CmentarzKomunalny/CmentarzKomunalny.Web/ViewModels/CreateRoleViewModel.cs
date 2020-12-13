using System.ComponentModel.DataAnnotations;
namespace CmentarzKomunalny.Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
