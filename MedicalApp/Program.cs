using MedicalApp.Data;
using Microsoft.EntityFrameworkCore;

using var db = new MedicalDbContext();
db.Database.Migrate();

Console.WriteLine("Baza je spremna.");