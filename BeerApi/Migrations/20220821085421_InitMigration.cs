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
                name: "Wholesaler",
                columns: table => new
                {
                    WholesalerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesaler", x => x.WholesalerId);
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

            migrationBuilder.CreateTable(
                name: "InventoryBeer",
                columns: table => new
                {
                    InventoryBeerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    WholesalerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBeer", x => x.InventoryBeerId);
                    table.ForeignKey(
                        name: "FK_InventoryBeer_Beer_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beer",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBeer_Wholesaler_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesaler",
                        principalColumn: "WholesalerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfUnits = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    WholesalerId = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sale_Beer_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beer",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Wholesaler_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesaler",
                        principalColumn: "WholesalerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brewery",
                columns: new[] { "BreweryId", "Address", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "walplein 26 8000 brugge", "info@halvemaan.be", "huisbrouwerij de halve maan" },
                    { 2, "kartuizerinnenstraat 6 8000 brugge", "visits@bourgognedesflandres", "bourgogne des flandres" }
                });

            migrationBuilder.InsertData(
                table: "Wholesaler",
                columns: new[] { "WholesalerId", "Address", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "jump street 21", "info@thebeer.be", "thebeer" },
                    { 2, "evergreen street 32", "contact@berallaxcorp.com", "berallax corp" },
                    { 3, "sesame street 77", "thebeercorporationinfo@beercorp.com", "the beer corporation" }
                });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "AlcoholContent", "BreweryId", "InProduction", "Name", "OutOfProductionDate", "SellingPriceToClients", "SellingPriceToWholesalers" },
                values: new object[,]
                {
                    { 1, 11.0, 1, true, "forte hendrik quadrupel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.99m, 3.99m },
                    { 2, 6.0, 1, true, "brugse zot blond", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.99m, 4.99m },
                    { 3, 0.40000000000000002, 1, true, "sportzot", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6.99m, 1.59m },
                    { 4, 5.0, 2, true, "bourgogne des flandres", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.59m, 0.29m },
                    { 5, 7.5, 1, false, "Brugse Zot Dubbel", new DateTime(2022, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 19.99m, 8.99m }
                });

            migrationBuilder.InsertData(
                table: "InventoryBeer",
                columns: new[] { "InventoryBeerId", "BeerId", "Quantity", "WholesalerId" },
                values: new object[,]
                {
                    { 1, 1, 250, 1 },
                    { 2, 2, 30, 2 },
                    { 3, 1, 70, 2 }
                });

            migrationBuilder.InsertData(
                table: "Sale",
                columns: new[] { "SaleId", "BeerId", "Discount", "NumberOfUnits", "PricePerUnit", "SaleDate", "Total", "WholesalerId" },
                values: new object[,]
                {
                    { 1, 1, 0, 1000m, 3.99m, new DateTime(2020, 9, 4, 18, 0, 0, 0, DateTimeKind.Unspecified), 3990m, 1 },
                    { 2, 5, 0, 1000m, 8.99m, new DateTime(2020, 10, 4, 18, 0, 0, 0, DateTimeKind.Unspecified), 8990m, 1 },
                    { 3, 2, 0, 200m, 4.99m, new DateTime(2020, 11, 4, 17, 0, 0, 0, DateTimeKind.Unspecified), 998m, 1 },
                    { 4, 1, 2, 300m, 3.99m, new DateTime(2021, 2, 3, 16, 0, 0, 0, DateTimeKind.Unspecified), 978.04m, 2 },
                    { 5, 2, 0, 2000m, 4.59m, new DateTime(2021, 8, 6, 18, 0, 0, 0, DateTimeKind.Unspecified), 9180m, 3 },
                    { 6, 4, 0, 200m, 0.29m, new DateTime(2021, 5, 4, 14, 7, 0, 0, DateTimeKind.Unspecified), 196m, 2 },
                    { 7, 2, 0, 200m, 4.99m, new DateTime(2022, 1, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), 998m, 1 },
                    { 8, 1, 0, 100m, 3.99m, new DateTime(2022, 2, 2, 20, 10, 0, 0, DateTimeKind.Unspecified), 399m, 2 },
                    { 9, 2, 0, 150m, 4.99m, new DateTime(2022, 3, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), 748.5m, 3 },
                    { 10, 3, 10, 1431m, 1.59m, new DateTime(2022, 5, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 998m, 1 }
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

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBeer_BeerId_WholesalerId",
                table: "InventoryBeer",
                columns: new[] { "BeerId", "WholesalerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBeer_WholesalerId",
                table: "InventoryBeer",
                column: "WholesalerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_BeerId",
                table: "Sale",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_WholesalerId",
                table: "Sale",
                column: "WholesalerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wholesaler_Name",
                table: "Wholesaler",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryBeer");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Wholesaler");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
