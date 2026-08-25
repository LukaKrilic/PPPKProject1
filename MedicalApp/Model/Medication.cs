using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    public class Medication
    {
        public long Id { get; set; }

        [MaxLength(150)]
        public required string Naziv { get; set; }
        [MaxLength(50)]
        public required string Oblik { get; set; }

        public virtual ICollection<Prescription> Recepti { get; set; } = [];
    }
}
