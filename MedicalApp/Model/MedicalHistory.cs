using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    public class MedicalHistory
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public required string Bolest { get; set; }

        public DateOnly DatumOd { get; set; }
        public DateOnly? DatumDo { get; set; }

        public long PacijentId { get; set; }
        public Patient Pacijent { get; set; } = null!;  
    }
}
