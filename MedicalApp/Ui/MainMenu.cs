using MedicalApp.Data;
using MedicalApp.Ui.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui
{
    internal sealed class MainMenu
    {
        private readonly MedicalDbContext _db;

        public MainMenu(MedicalDbContext db) 
        {
            _db = db;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("""
                    
                    ===MEDICINSKI SUSTAV===
                    1) Pacijenti
                    2) Povijest bolesti
                    3) Lijekovi
                    4) Recepti
                    5) Specijalisticki pregledi
                    6) Doktori

                    0) Izlaz
                    """);
                Console.Write("Odabir: ");

                switch(Console.ReadLine()?.Trim())
                {
                    case "1": new PatientMenu(_db).Run(); break;
                    case "2": new MedicalHistoryMenu(_db).Run(); break;
                    case "3": new MedicationMenu(_db).Run(); break;
                    case "4": new PrescriptionMenu(_db).Run(); break;
                    case "5": new SpecialistExamMenu(_db).Run(); break;
                    case "6": DoctorMenu.ShowAll(_db); break;
                    case "0": return;
                    default: Console.WriteLine("Nepoznata opcija."); break;
                }
                    
            }
        }
    }
}
