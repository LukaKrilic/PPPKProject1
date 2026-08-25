using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Specijalizacija = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Oblik = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Oib = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    DatumRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    Spol = table.Column<string>(type: "char(1)", nullable: false),
                    AdresaBorovista = table.Column<string>(type: "text", nullable: true),
                    AdresaPrebivalista = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bolest = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DatumOd = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumDo = table.Column<DateOnly>(type: "date", nullable: true),
                    PacijentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Patients_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Doza = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Jedinica = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ucestalost = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ZaStanje = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PacijentId = table.Column<long>(type: "bigint", nullable: false),
                    MedicationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialistExams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipPregleda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DatumPregleda = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PacijentId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialistExams_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialistExams_Patients_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PacijentId",
                table: "MedicalHistories",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Oib",
                table: "Patients",
                column: "Oib",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PacijentId",
                table: "Prescriptions",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistExams_DoctorId",
                table: "SpecialistExams",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistExams_PacijentId",
                table: "SpecialistExams",
                column: "PacijentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "SpecialistExams");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
