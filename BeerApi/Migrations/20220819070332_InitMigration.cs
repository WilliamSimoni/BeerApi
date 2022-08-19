using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerApi.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    BreweryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewery", x => x.BreweryId);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AlcoholContent = table.Column<double>(type: "float", nullable: false),
                    SellingPriceToWholesalers = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    SellingPriceToClients = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    InProduction = table.Column<bool>(type: "bit", nullable: false),
                    OutOfProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "BreweryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brewery",
                columns: new[] { "BreweryId", "Address", "Email", "Name" },
                values: new object[] { 1, "Walplein 26 8000 Brugge", "info@halvemaan.be", "Huisbrouwerij De Halve Maan" });

            migrationBuilder.InsertData(
                table: "Brewery",
                columns: new[] { "BreweryId", "Address", "Email", "Name" },
                values: new object[] { 2, "Kartuizerinnenstraat 6 8000 Brugge", "visits@bourgognedesflandres", "Bourgogne des Flandres" });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "AlcoholContent", "BreweryId", "InProduction", "Name", "OutOfProductionDate", "SellingPriceToClients", "SellingPriceToWholesalers" },
                values: new object[,]
                {
                    { 1, 11.0, 1, true, "Forte Hendrik Quadrupel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.99m, 3.99m },
                    { 2, 6.0, 1, true, "Brugse Zot Blond", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.99m, 4.99m },
                    { 3, 0.40000000000000002, 1, true, "Sportzot", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6.99m, 1.59m },
                    { 4, 5.0, 2, true, "Bourgogne des Flandres", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.59m, 0.29m },
                    { 5, 7.5, 1, false, "Brugse Zot Dubbel", new DateTime(2022, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 9.99m, 3.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_Name_OutOfProductionDate_BreweryId",
                table: "Beer",
                columns: new[] { "Name", "OutOfProductionDate", "BreweryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brewery_Name",
                table: "Brewery",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
