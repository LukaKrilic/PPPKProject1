using MedicalApp.Data;
using MedicalApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp
{
    internal class DoctorSeeder
    {
        public static void FirstRun(MedicalDbContext db) 
        {
            Console.WriteLine("=== Prvo pokretanje: unos Doktora ===");
            Console.WriteLine("(Nakon ovog koraka dodavanje doktora vise NIJE moguce!)");
            Console.WriteLine();

            while(true)
            {
                Console.Write("Ime (prazno za kraj unosa): ");
                var ime = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ime))
                {
                    if (!db.Doctors.Any())
                    {
                        Console.WriteLine("Morate unijeti barem jednog doktora! \n");
                        continue;
                    }
                    break;
                }

                Console.Write("Prezime: ");
                var prezime = Console.ReadLine();

                Console.Write("Specijalizacija: ");
                var specijalizacija = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(prezime) || string.IsNullOrWhiteSpace(specijalizacija))
                {
                    Console.WriteLine("Prezime i specijalizacija ne smiju biti prazni! \n");
                    continue;
                }

                db.Doctors.Add(new Doctor
                {
                    Ime = ime.Trim(),
                    Prezime = prezime.Trim(),
                    Specijalizacija = specijalizacija.Trim()
                });

                Console.WriteLine($"Doktor {ime} {prezime} ({specijalizacija}) je dodan. \n");
                db.SaveChanges();
            }
            
            Console.WriteLine($"=== Unos doktora({db.Doctors.Count()}) zavrsen ===\n");
        }
    }
}
