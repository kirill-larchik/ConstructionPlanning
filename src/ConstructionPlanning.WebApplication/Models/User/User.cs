using Microsoft.AspNetCore.Identity;

namespace ConstructionPlanning.WebApplication.Models.User
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }

        public string Forename { get; set; }
    }
}
