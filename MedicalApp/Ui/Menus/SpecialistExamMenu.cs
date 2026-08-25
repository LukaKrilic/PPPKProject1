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
    internal sealed class SpecialistExamMenu
    {
        private readonly MedicalDbContext _db;

        public SpecialistExamMenu(MedicalDbContext db)
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""

                    --- SPECIJALISTICKI PREGLEDI ---
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
            var upit = _db.SpecialistExams
                .Include(p => p.Pacijent)
                .Include(p => p.Doktor)
                .OrderBy(p => p.DatumPregleda);

            Console.WriteLine();
            foreach (var p in upit)
                Console.WriteLine($"{p.Id,4} | {p.DatumPregleda:dd.MM.yyyy HH:mm} | {p.TipPregleda} | " +
                                  $"{p.Pacijent.Prezime} {p.Pacijent.Ime} | dr. {p.Doktor.Prezime} ({p.Doktor.Specijalizacija})");
        }

        private void Dodaj()
        {
            var pacijentId = Odabir.Pacijent(_db);
            if (pacijentId is null) return;

            var doktorId = Odabir.Doktor(_db);
            if (doktorId is null) return;

            var pregled = new SpecialistExam
            {
                PacijentId = pacijentId.Value,
                DoctorId = doktorId.Value,
                TipPregleda = OdaberiTip(),
                DatumPregleda = ConsoleInput.ReadUtcDateTime("Termin (dd.MM.yyyy HH:mm): ")
            };

            _db.SpecialistExams.Add(pregled);
            DBHelper.Spremi(_db);
        }

        private void Uredi()
        {
            var id = ConsoleInput.ReadLong("ID pregleda: ");
            var pregled = _db.SpecialistExams.Find(id);
            if (pregled is null) { Console.WriteLine("Pregled ne postoji."); return; }

            if (ConsoleInput.Confirm($"Promijeniti tip [{pregled.TipPregleda}]? (d/n): "))
                pregled.TipPregleda = OdaberiTip();

            if (ConsoleInput.Confirm($"Promijeniti termin [{pregled.DatumPregleda:dd.MM.yyyy HH:mm}]? (d/n): "))
                pregled.DatumPregleda = ConsoleInput.ReadUtcDateTime("Novi termin (dd.MM.yyyy HH:mm): ");

            DBHelper.Spremi(_db);
        }

        private void Obrisi()
        {
            var id = ConsoleInput.ReadLong("ID pregleda: ");
            var pregled = _db.SpecialistExams.Find(id);
            if (pregled is null) { Console.WriteLine("Pregled ne postoji."); return; }

            if (!ConsoleInput.Confirm($"Obrisati pregled {pregled.Id}? (d/n): ")) return;

            _db.SpecialistExams.Remove(pregled);
            DBHelper.Spremi(_db);
        }

        private static ExamType OdaberiTip()
        {
            var tipovi = Enum.GetValues<ExamType>();
            Console.WriteLine("\nTipovi pregleda:");
            for (var i = 0; i < tipovi.Length; i++)
                Console.WriteLine($"{i + 1,3}) {tipovi[i]}");

            while (true)
            {
                var izbor = ConsoleInput.ReadLong("Tip (broj): ");
                if (izbor >= 1 && izbor <= tipovi.Length) return tipovi[izbor - 1];
                Console.WriteLine("Nepostojeci tip.");
            }
        }
    }
}
