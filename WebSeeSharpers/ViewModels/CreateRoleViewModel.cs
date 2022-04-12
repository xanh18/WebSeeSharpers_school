using System.ComponentModel.DataAnnotations;

namespace WebSeeSharpers.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }     
    }
}
