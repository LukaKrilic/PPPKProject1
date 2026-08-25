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
    internal sealed class PrescriptionMenu
    {
        private readonly MedicalDbContext _db;

        public PrescriptionMenu(MedicalDbContext db)
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""

                    --- RECEPTI ---
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
            var upit = _db.Prescriptions
                .Include(r => r.Pacijent)
                .Include(r => r.Lijek)
                .OrderBy(r => r.PacijentId);

            Console.WriteLine();
            foreach (var r in upit)
                Console.WriteLine($"{r.Id,4} | {r.Pacijent.Prezime} {r.Pacijent.Ime} | {r.Lijek.Naziv} | " +
                                  $"{r.Doza} {r.Jedinica} | {r.Ucestalost} | {r.ZaStanje}");
        }

        private void Dodaj()
        {
            var pacijentId = Odabir.Pacijent(_db);
            if (pacijentId is null) return;

            var lijekId = Odabir.Lijek(_db);
            if (lijekId is null) return;

            var recept = new Prescription
            {
                PacijentId = pacijentId.Value,
                MedicationId = lijekId.Value,
                Doza = ConsoleInput.ReadDecimal("Doza (npr. 2.5): "),
                Jedinica = ConsoleInput.Required("Jedinica (mg, IU, tableta...): "),
                Ucestalost = ConsoleInput.Required("Ucestalost (npr. 3x dnevno): "),
                ZaStanje = ConsoleInput.Required("Za stanje: ")
            };

            _db.Prescriptions.Add(recept);
            DBHelper.Spremi(_db);
        }

        private void Uredi()
        {
            var id = ConsoleInput.ReadLong("ID recepta: ");
            var recept = _db.Prescriptions.Find(id);
            if (recept is null) { Console.WriteLine("Recept ne postoji."); return; }

            Console.WriteLine("(Enter zadrzava trenutnu vrijednost)");
            recept.Doza = ConsoleInput.OptionalDecimal($"Doza [{recept.Doza}]: ") ?? recept.Doza;
            recept.Jedinica = ConsoleInput.Optional($"Jedinica [{recept.Jedinica}]: ") ?? recept.Jedinica;
            recept.Ucestalost = ConsoleInput.Optional($"Ucestalost [{recept.Ucestalost}]: ") ?? recept.Ucestalost;
            recept.ZaStanje = ConsoleInput.Optional($"Za stanje [{recept.ZaStanje}]: ") ?? recept.ZaStanje;

            DBHelper.Spremi(_db);
        }

        private void Obrisi()
        {
            var id = ConsoleInput.ReadLong("ID recepta: ");
            var recept = _db.Prescriptions.Find(id);
            if (recept is null) { Console.WriteLine("Recept ne postoji."); return; }

            if (!ConsoleInput.Confirm($"Obrisati recept {recept.Id}? (d/n): ")) return;

            _db.Prescriptions.Remove(recept);
            DBHelper.Spremi(_db);
        }
    }
}
