using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padrinly.Domain.Enums
{
    public enum TypePerson
    {
        [Display (Name = "Instituição")]
        Institution,
        [Display (Name = "Padrinho")]
        Patron,
        [Display (Name ="Aluno")]
        Student,
        [Display (Name = "Responsável")]
        Responsabile
    }
}
