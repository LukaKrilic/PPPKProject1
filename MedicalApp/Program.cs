using MedicalApp;
using MedicalApp.Data;
using MedicalApp.Ui;
using Microsoft.EntityFrameworkCore;

using var db = new MedicalDbContext();
db.Database.Migrate();

if (!db.Doctors.Any())
    DoctorSeeder.FirstRun(db);

new MainMenu(db).Run();