using MedicalApp.Data;
using MedicalApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui.Menus
{
    internal sealed class MedicationMenu
    {
        private readonly MedicalDbContext _db;

        public MedicationMenu(MedicalDbContext db)
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""

                    --- LIJEKOVI ---
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
            Console.WriteLine();
            foreach (var l in _db.Medications.OrderBy(l => l.Naziv))
                Console.WriteLine($"{l.Id,4} | {l.Naziv} | {l.Oblik}");
        }

        private void Dodaj()
        {
            var lijek = new Medication
            {
                Naziv = ConsoleInput.Required("Naziv: "),
                Oblik = ConsoleInput.Required("Oblik (tableta, kapsula, sirup...): ")
            };

            _db.Medications.Add(lijek);
            DBHelper.Spremi(_db);
        }

        private void Uredi()
        {
            var id = ConsoleInput.ReadLong("ID lijeka: ");
            var lijek = _db.Medications.Find(id);
            if (lijek is null) { Console.WriteLine("Lijek ne postoji."); return; }

            Console.WriteLine("(Enter zadrzava trenutnu vrijednost)");
            lijek.Naziv = ConsoleInput.Optional($"Naziv [{lijek.Naziv}]: ") ?? lijek.Naziv;
            lijek.Oblik = ConsoleInput.Optional($"Oblik [{lijek.Oblik}]: ") ?? lijek.Oblik;

            DBHelper.Spremi(_db);
        }

        private void Obrisi()
        {
            var id = ConsoleInput.ReadLong("ID lijeka: ");
            var lijek = _db.Medications.Find(id);
            if (lijek is null) { Console.WriteLine("Lijek ne postoji."); return; }

            Console.WriteLine("PAZNJA: brisanjem lijeka kaskadno se brisu i recepti koji ga koriste.");
            if (!ConsoleInput.Confirm($"Obrisati {lijek.Naziv}? (d/n): ")) return;

            _db.Medications.Remove(lijek);
            DBHelper.Spremi(_db);
        }
    }
}
