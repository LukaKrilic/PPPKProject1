using MedicalApp.Data;
using MedicalApp.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui.Menus
{
    internal sealed class PatientMenu
    {
        private readonly MedicalDbContext _db;

        public PatientMenu(MedicalDbContext db)
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""

                    --- PACIJENTI ---
                    1) Prikazi
                    2) Dodaj
                    3) Uredi
                    4) Obrisi

                    0) Natrag
                    """);
                Console.Write("Odabir: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": Prikazi(); break;
                    case "2": Dodaj(); break;
                    case "3": Uredi(); break;
                    case "4": Obrisi(); break;
                    case "0": return;
                    default: Console.WriteLine("Nepoznata opcija."); break;
                }
            }
        }

        private void Obrisi()
        {
            var id = ConsoleInput.ReadLong("ID pacijenta: ");
            var pacijent = _db.Patients.Find(id);
            if (pacijent is null) { Console.WriteLine("Pacijent ne postoji."); return; }

            Console.WriteLine("PAZNJA: brisanjem pacijenta brisu se i njegova povijest, recepti i pregledi.");
            if (!ConsoleInput.Confirm($"Obrisati {pacijent.Prezime} {pacijent.Ime}? (d/n): ")) return;

            _db.Patients.Remove(pacijent);
            DBHelper.Spremi(_db);
        }

        private void Uredi()
        {
            var id = ConsoleInput.ReadLong("ID pacijenta: ");
            var pacijent = _db.Patients.Find(id);
            if (pacijent is null) { Console.WriteLine("Pacijent ne postoji."); return; }

            Console.WriteLine("(Enter zadrzava trenutnu vrijednost)");
            pacijent.Ime = ConsoleInput.Optional($"Ime [{pacijent.Ime}]: ") ?? pacijent.Ime;
            pacijent.Prezime = ConsoleInput.Optional($"Prezime [{pacijent.Prezime}]: ") ?? pacijent.Prezime;
            pacijent.DatumRodjenja = ConsoleInput.OptionalDate(
                $"Datum rodenja [{pacijent.DatumRodjenja:dd.MM.yyyy}]: ") ?? pacijent.DatumRodjenja;
            pacijent.AdresaBoravista = ConsoleInput.Optional(
                $"Adresa boravista [{pacijent.AdresaBoravista}]: ") ?? pacijent.AdresaBoravista;
            pacijent.AdresaPrebivalista = ConsoleInput.Optional(
                $"Adresa prebivalista [{pacijent.AdresaPrebivalista}]: ") ?? pacijent.AdresaPrebivalista;
            pacijent.Email = ConsoleInput.Optional($"Email [{pacijent.Email}]: ") ?? pacijent.Email;

            var oib = ConsoleInput.Optional($"OIB [{pacijent.Oib}]: ");
            if (oib is not null)
            {
                if (oib.Length == 11 && oib.All(char.IsDigit)) pacijent.Oib = oib;
                else Console.WriteLine("OIB mora imati 11 znamenki.");
            }

            var spol = ConsoleInput.Optional($"Spol M/Z [{pacijent.Spol}]: ");
            if (spol is not null)
            {
                var s = spol.ToUpperInvariant();
                if (s is "M" or "Z") pacijent.Spol = s;
                else Console.WriteLine("Spol mora biti M ili Z.");
            }

            DBHelper.Spremi(_db);
        }

        private void Dodaj()
        {
            var pacijent = new Patient
            {
                Ime = ConsoleInput.Required("Ime: "),
                Prezime = ConsoleInput.Required("Prezime: "),
                Oib = UnesiOib(),
                DatumRodjenja = ConsoleInput.ReadDate("Datum rodenja (dd.MM.yyyy): "),
                Spol = UnesiSpol(),
                AdresaBoravista = ConsoleInput.Optional("Adresa boravista (neobavezno): "),
                AdresaPrebivalista = ConsoleInput.Optional("Adresa prebivalista (neobavezno): "),
                Email = ConsoleInput.Optional("Email (neobavezno): ")
            };

            _db.Patients.Add(pacijent);
            DBHelper.Spremi(_db);
        }

        private string UnesiSpol()
        {
            while (true)
            {
                var spol = ConsoleInput.Required("Spol (M/Z): ").ToUpperInvariant();
                if (spol is "M" or "Z") return spol;
                Console.WriteLine("Dozvoljeno: M ili Z.");
            }
        }

        private string UnesiOib()
        {
            while (true)
            {
                var oib = ConsoleInput.Required("OIB (11 znamenki): ");
                if (oib.Length == 11 && oib.All(char.IsDigit)) return oib;
                Console.WriteLine("OIB mora imati tocno 11 znamenki.");
            }
        }

        private void Prikazi()
        {
            var filter = ConsoleInput.Optional("Filtriraj po prezimenu (prazno = svi): ");
            Console.Write("Sortiraj: 1) prezime  2) datum rođenja: ");
            var sort = Console.ReadLine()?.Trim();

            IQueryable<Patient> upit = _db.Patients;

            if (filter is not null)
                upit = upit.Where(p => EF.Functions.ILike(p.Prezime, $"%{filter}%"));

            upit = sort == "2"
                ? upit.OrderBy(p => p.DatumRodjenja)
                : upit.OrderBy(p => p.Prezime);

            Console.WriteLine();
            foreach (var p in upit)
            {
                Console.WriteLine($"{p.Id,4} | {p.Prezime} {p.Ime} | {p.Oib} | {p.DatumRodjenja:dd.MM.yyyy} | {p.Spol}");
                Console.WriteLine($"boravište: {p.AdresaBoravista ?? "-"} | prebivalište: {p.AdresaPrebivalista ?? "-"} | email: {p.Email ?? "-"}");
            }
        }
    }
}
