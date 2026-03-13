using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;  // for NotMapped

namespace Bookstore.Models
{
    public class User : IdentityUser
    {

        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";


        [NotMapped]
        public IList<string> RoleNames { get; set; } = null!;
    }
}
