using Microsoft.AspNetCore.Identity;

namespace Padrinly.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role() { }

        public Role(string roleName) 
        {
            this.Name = roleName;
        }
    }
}
