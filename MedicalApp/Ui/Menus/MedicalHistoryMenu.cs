using MedicalApp.Data;
using MedicalApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui.Menus
{
    internal sealed class MedicalHistoryMenu
    {
        private readonly MedicalDbContext _db;

        public MedicalHistoryMenu(MedicalDbContext db)
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""

                    --- POVIJEST BOLESTI ---
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

        private void Prikazi()
        {
            var upit = _db.MedicalHistories
                .Include(h => h.Pacijent)
                .OrderBy(h => h.PacijentId)
                .ThenBy(h => h.DatumOd);

            Console.WriteLine();
            foreach (var h in upit)
                Console.WriteLine($"{h.Id,4} | {h.Pacijent.Prezime} {h.Pacijent.Ime} | {h.Bolest} | " +
                                  $"{h.DatumOd:dd.MM.yyyy} - {(h.DatumDo?.ToString("dd.MM.yyyy") ?? "traje")}");
        }

        private void Dodaj()
        {
            var pacijentId = Odabir.Pacijent(_db);
            if (pacijentId is null) return;

            var zapis = new MedicalHistory
            {
                PacijentId = pacijentId.Value,
                Bolest = ConsoleInput.Required("Bolest: "),
                DatumOd = ConsoleInput.ReadDate("Datum od (dd.MM.yyyy): "),
                DatumDo = ConsoleInput.OptionalDate("Datum do (prazno = bolest jos traje): ")
            };

            _db.MedicalHistories.Add(zapis);
            DBHelper.Spremi(_db);
        }

        private void Uredi()
        {
            var id = ConsoleInput.ReadLong("ID zapisa: ");
            var zapis = _db.MedicalHistories.Find(id);
            if (zapis is null) { Console.WriteLine("Zapis ne postoji."); return; }

            Console.WriteLine("(Enter zadrzava trenutnu vrijednost)");
            zapis.Bolest = ConsoleInput.Optional($"Bolest [{zapis.Bolest}]: ") ?? zapis.Bolest;
            zapis.DatumOd = ConsoleInput.OptionalDate($"Datum od [{zapis.DatumOd:dd.MM.yyyy}]: ") ?? zapis.DatumOd;
            zapis.DatumDo = ConsoleInput.OptionalDate(
                $"Datum do [{zapis.DatumDo?.ToString("dd.MM.yyyy") ?? "traje"}]: ") ?? zapis.DatumDo;

            DBHelper.Spremi(_db);
        }

        private void Obrisi()
        {
            var id = ConsoleInput.ReadLong("ID zapisa: ");
            var zapis = _db.MedicalHistories.Find(id);
            if (zapis is null) { Console.WriteLine("Zapis ne postoji."); return; }

            if (!ConsoleInput.Confirm($"Obrisati zapis '{zapis.Bolest}'? (d/n): ")) return;

            _db.MedicalHistories.Remove(zapis);
            DBHelper.Spremi(_db);
        }
    }
}
