using MedicalApp.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui
{
    internal static class DBHelper
    {
        public static void Spremi(MedicalDbContext db)
        {
            try
            {
                db.SaveChanges();
                Console.WriteLine("Spremljeno.");
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg)
            {
                db.ChangeTracker.Clear();
                Console.WriteLine(pg.SqlState switch
                {
                    "23505" => "Greska: vrijednost mora biti jedinstvena - zapis vec postoji.",
                    "23503" => "Greska: povezani zapis ne postoji ili se zapis jos koristi.",
                    "23514" => "Greska: vrijednost ne zadovoljava ogranicenje.",
                    "22001" => "Greska: unesena vrijednost je preduga za stupac.",
                    _ => $"Greska baze ({pg.SqlState}): {pg.MessageText}"
                });
            }
        }
    }
}
