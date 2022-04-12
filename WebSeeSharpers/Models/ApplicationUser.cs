using Microsoft.AspNetCore.Identity;

namespace WebSeeSharpers.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public string? Age { get; set; }

        [PersonalData]
        public string? City { get; set; }

    }
}
