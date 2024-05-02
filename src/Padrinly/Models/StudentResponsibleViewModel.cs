using Microsoft.AspNetCore.Mvc.Rendering;
using Padrinly.Domain.Entities;
using Padrinly.Domain.Enums;
using System.ComponentModel;

namespace Padrinly.Models
{
    public class StudentResponsibleViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Aluno")]
        public string StudentName { get; set; }

        [DisplayName("Senha")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [DisplayName("CPF do aluno")]
        public string StudentFirstDocument { get; set; }

        [DisplayName("RG opcional")]
        public string? StudentSecondtDocument { get; set; }

        [DisplayName("Nascimento (estudante)")]
        public DateOnly StudentBirthDate { get; set; }

        [DisplayName("Novo Responsável")]
        public bool IsNewResponsible { get; set; }

        public int? IdResponsible { get; set; }

        [DisplayName("Nome do Responsável")]
        public string ResponsibleName { get; set; }

        [DisplayName("CPF do responsável")]
        public string ResponsibleFirstDocument { get; set; }

        [DisplayName("RG opcional")]
        public string? ResponsibleSecondtDocument { get; set; }

        [DisplayName("Nascimento (responsável)")]
        public DateOnly ResponsibleBirthDate { get; set; }

        [DisplayName("Email do aluno")]
        public string StudentEmail { get; set; }

        [DisplayName("Email do Responsável")]
        public string ResponsibleEmail { get; set; }

        [DisplayName("Número de telefone")]
        public string ResponsiblePhoneNumber { get; set; }

        [DisplayName("Endereço")]
        public string Address { get; set; }

        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Estado")]
        public string State { get; set; }

        [DisplayName("CEP")]
        public string PostalCode { get; set; }

        [DisplayName("Número")]
        public string Number { get; set; }

        [DisplayName("Complemento")]
        public string Complement { get; set; }

        public int? SelectedPersonId { get; set; }
    }
}
