using Microsoft.AspNetCore.Identity;
using Padrinly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padrinly.Domain.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [DisplayName("Conteúdo")]
        public string Content { get; set; }

        public bool IsFixed { get; set; } = false;

        [ForeignKey(nameof(IdUser))]
        public IdentityUser<int> User { get; set; }

        public int IdUser { get; set; }
    }
}
