using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CXPApp.Migrations
{
    /// <inheritdoc />
    public partial class DocumetEntry_AccountingSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingSeat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdInventoryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountingAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatAmount = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingSeat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEntry", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingSeat");

            migrationBuilder.DropTable(
                name: "DocumentEntry");
        }
    }
}
