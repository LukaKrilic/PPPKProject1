using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    public class Patient
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public required string Ime { get; set; }
        [MaxLength(50)]
        public required string Prezime { get; set; }

        [MaxLength(11)]
        public required string Oib { get; set; }
        public DateOnly DatumRodjenja { get; set; }

        public required string Spol { get; set; }

        public string? AdresaBoravista { get; set; }
        public string? AdresaPrebivalista { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
