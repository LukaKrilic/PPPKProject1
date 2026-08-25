using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    internal class Doctor
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public required string Ime { get; set; }
        [MaxLength(50)]
        public required string Prezime { get; set; }
        [MaxLength(100)]
        public required string Specijalizacija { get; set; }

        public virtual ICollection<SpecialistExam> Pregledi { get; set; } = [];
    }
}
