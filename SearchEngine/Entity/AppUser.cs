using Microsoft.AspNetCore.Identity;

namespace SearchEngine.Entity
{
    public class AppUser : IdentityUser
    {

        public string FullName { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }


    }
}
