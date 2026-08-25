using MedicalApp;
using MedicalApp.Data;
using Microsoft.EntityFrameworkCore;

using var db = new MedicalDbContext();
db.Database.Migrate();

if (!db.Doctors.Any())
    DoctorSeeder.FirstRun(db);

Console.WriteLine("Baza je spremna.");