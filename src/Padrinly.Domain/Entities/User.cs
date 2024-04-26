using Microsoft.AspNetCore.Identity;


namespace Padrinly.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public int CreatBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdateBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
