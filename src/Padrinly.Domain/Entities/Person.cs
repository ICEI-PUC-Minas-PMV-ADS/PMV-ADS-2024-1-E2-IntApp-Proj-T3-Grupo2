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
    public class Person : BaseEntity
    {
        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [DisplayName("Número de Telefone")]
        public int PhoneNumber { get; set; }

        [Required]
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        public bool IsAnonimous { get; set; } = true;

        public string? AvatarFileName { get; set; }

        public string? AvatarInternalName { get; set; }

        [Required]
        [DisplayName("Tipo de Pessoa")]
        public int TypePerson { get; set; }

        [ForeignKey(nameof(IdResponsible))]
        public Person Responsible { get; set; }

        public int? IdResponsible { get; set; }

        [ForeignKey(nameof(IdInstitution))]
        public Person Institution { get; set; }

        public int? IdInstitution { get; set; }

        [Required]
        public int FirstDocument { get; set; }

        public int? SecondDocument { get; set; }

        public ICollection<PersonPatron> Patrons { get; set; }

        public ICollection<Document> Documents { get; set; }

        [ForeignKey(nameof(IdUser))]
        public IdentityUser<int> User { get; set; }

        public int? IdUser { get; set; }

        [Required]
        [DisplayName("Endereço")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [Required]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required]
        [DisplayName("Estado")]
        public string State { get; set; }

        [Required]
        [DisplayName("CEP")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayName("Número")]
        public string Number { get; set; }

        public string Complement { get; set; }
    }
}
