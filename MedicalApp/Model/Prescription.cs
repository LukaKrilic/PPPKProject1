using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    public class Prescription
    {
        public long Id { get; set; }
        public decimal Doza { get; set; }

        [MaxLength(20)]
        public required string Jedinica { get; set; }
        [MaxLength(100)]
        public required string Ucestalost { get; set; }
        [MaxLength(200)]
        public required string ZaStanje { get; set; }

        public long PacijentId { get; set; }
        public Patient Pacijent { get; set; } = null!;

        public long MedicationId { get; set; }
        public Medication Lijek { get; set; } = null!;
    }
}
