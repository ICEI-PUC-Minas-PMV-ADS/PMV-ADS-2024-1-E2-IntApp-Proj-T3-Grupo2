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
    public class Mensage : BaseEntity
    {
        [Required]
        [DisplayName("Conteúdo")]
        public string Content { get; set; }

        public DateTime SendedAt { get; set; }

        [ForeignKey(nameof(IdSender))]
        public IdentityUser<int> Sender { get; set; }

        public int IdSender { get; set; }

        [ForeignKey(nameof(IdReceiver))]
        public IdentityUser<int> Receiver { get; set; }

        public int IdReceiver { get; set; }
    }
}
