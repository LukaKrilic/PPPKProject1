using MedicalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui
{
    internal static class Odabir
    {
        public static long? Pacijent(MedicalDbContext db)
        {
            var pacijenti = db.Patients
                .OrderBy(p => p.Prezime)
                .Select(p => new { p.Id, p.Ime, p.Prezime, p.Oib })
                .ToList();

            if (pacijenti.Count == 0) { Console.WriteLine("Nema unesenih pacijenata."); return null; }

            Console.WriteLine("\nPacijenti:");
            foreach (var p in pacijenti)
                Console.WriteLine($"{p.Id,4} | {p.Prezime} {p.Ime} | {p.Oib}");

            var id = ConsoleInput.ReadLong("ID pacijenta: ");
            if (pacijenti.Any(p => p.Id == id)) return id;

            Console.WriteLine("Pacijent s tim ID-om ne postoji.");
            return null;
        }

        public static long? Lijek(MedicalDbContext db)
        {
            var lijekovi = db.Medications
                .OrderBy(l => l.Naziv)
                .Select(l => new { l.Id, l.Naziv, l.Oblik })
                .ToList();

            if (lijekovi.Count == 0) { Console.WriteLine("Nema unesenih lijekova."); return null; }

            Console.WriteLine("\nLijekovi:");
            foreach (var l in lijekovi)
                Console.WriteLine($"{l.Id,4} | {l.Naziv} ({l.Oblik})");

            var id = ConsoleInput.ReadLong("ID lijeka: ");
            if (lijekovi.Any(l => l.Id == id)) return id;

            Console.WriteLine("Lijek s tim ID-om ne postoji.");
            return null;
        }

        public static long? Doktor(MedicalDbContext db)
        {
            var doktori = db.Doctors
                .OrderBy(d => d.Prezime)
                .Select(d => new { d.Id, d.Ime, d.Prezime, d.Specijalizacija })
                .ToList();

            if (doktori.Count == 0) { Console.WriteLine("Nema unesenih doktora."); return null; }

            Console.WriteLine("\nDoktori:");
            foreach (var d in doktori)
                Console.WriteLine($"{d.Id,4} | {d.Prezime} {d.Ime} | {d.Specijalizacija}");

            var id = ConsoleInput.ReadLong("ID Doktor: ");
            if (doktori.Any(d => d.Id == id)) return id;

            Console.WriteLine("Doktor s tim ID-om ne postoji.");
            return null;
        }
    }
}
