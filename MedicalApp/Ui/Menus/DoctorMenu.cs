using MedicalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui.Menus
{
    internal static class DoctorMenu
    {
        public static void ShowAll(MedicalDbContext db)
        {
            Console.WriteLine("\n--- Doktori (samo pregled) ---");
            foreach (var d in db.Doctors.OrderBy(d => d.Prezime))
                Console.WriteLine($"{d.Id,4} | {d.Prezime} {d.Ime} | {d.Specijalizacija}");
            Console.WriteLine("Doktori se unose samo pri prvom pokretanju aplikacije.");
        }
    }
}
