using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se4458api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicine__4F2128F00160DBD0", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthenticationCredentials = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pharmaci__BD9D2A8E34FD62B4", x => x.PharmacyID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyID = table.Column<int>(type: "int", nullable: true),
                    PatientTC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prescrip__40130812BB6C589F", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK__Prescript__Pharm__2CF2ADDF",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__55433A4B5759782D", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Pharm__339FAB6E",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDetails",
                columns: table => new
                {
                    PrescriptionDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<int>(type: "int", nullable: true),
                    MedicineID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prescrip__6DB7668A477678D2", x => x.PrescriptionDetailID);
                    table.ForeignKey(
                        name: "FK__Prescript__Medic__30C33EC3",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "MedicineID");
                    table.ForeignKey(
                        name: "FK__Prescript__Presc__2FCF1A8A",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_MedicineID",
                table: "PrescriptionDetails",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_PrescriptionID",
                table: "PrescriptionDetails",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacyID",
                table: "Prescriptions",
                column: "PharmacyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PharmacyID",
                table: "Transactions",
                column: "PharmacyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Pharmacies");
        }
    }
}
