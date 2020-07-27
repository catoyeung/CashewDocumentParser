using Microsoft.AspNetCore.Identity;

namespace CashewDocumentParser.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ApplicationUser()
        {
            
        }
    }
}
