using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Data.Models
{
    public class Employee : IdentityUser
    {
        public string Department { get; set; } = string.Empty;
    }
}
