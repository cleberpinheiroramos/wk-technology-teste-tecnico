using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WK.Technology.Teste.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Identificador exclusivo e universal para a entidade")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Registro do usuário que cadastrou a entidade")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Registro de data/hora de quando o usuário cadastrou a entidade"),
                    UpdatedBy = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "Registro do usuário que realizou a última alteração na entidade")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp", nullable: true, comment: "Registro de data/hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Identificador exclusivo e universal para a entidade")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Registro do usuário que cadastrou a entidade")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Registro de data/hora de quando o usuário cadastrou a entidade"),
                    UpdatedBy = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "Registro do usuário que realizou a última alteração na entidade")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp", nullable: true, comment: "Registro de data/hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, "Jonathan_Dietrich@gmail.com", new DateTime(2021, 10, 23, 14, 2, 5, 157, DateTimeKind.Local).AddTicks(2055), "Grocery, Automotive & Books", "Jonathan_Dietrich@gmail.com", new DateTime(2021, 10, 17, 8, 19, 10, 269, DateTimeKind.Local).AddTicks(3295) },
                    { 2L, "Emily98@hotmail.com", new DateTime(2022, 3, 7, 8, 6, 19, 17, DateTimeKind.Local).AddTicks(4826), "Industrial, Tools & Music", "Emily98@hotmail.com", new DateTime(2022, 8, 11, 0, 40, 46, 366, DateTimeKind.Local).AddTicks(6092) },
                    { 3L, "Gregg96@hotmail.com", new DateTime(2022, 9, 22, 23, 5, 51, 936, DateTimeKind.Local).AddTicks(2183), "Industrial, Computers & Kids", "Gregg96@hotmail.com", new DateTime(2022, 8, 3, 20, 56, 1, 626, DateTimeKind.Local).AddTicks(2454) },
                    { 4L, "Marianne30@yahoo.com", new DateTime(2022, 8, 30, 9, 48, 22, 7, DateTimeKind.Local).AddTicks(7574), "Books, Electronics & Clothing", "Marianne30@yahoo.com", new DateTime(2022, 2, 4, 3, 17, 19, 286, DateTimeKind.Local).AddTicks(60) },
                    { 5L, "Kristin64@hotmail.com", new DateTime(2021, 12, 14, 2, 13, 45, 553, DateTimeKind.Local).AddTicks(458), "Computers & Health", "Kristin64@hotmail.com", new DateTime(2021, 10, 20, 7, 35, 10, 456, DateTimeKind.Local).AddTicks(2050) },
                    { 6L, "Marilyn.Hessel84@hotmail.com", new DateTime(2022, 3, 3, 21, 17, 11, 536, DateTimeKind.Local).AddTicks(8693), "Books", "Marilyn.Hessel84@hotmail.com", new DateTime(2022, 5, 3, 15, 39, 34, 760, DateTimeKind.Local).AddTicks(7550) },
                    { 7L, "Wallace_Koch@hotmail.com", new DateTime(2022, 4, 1, 10, 8, 8, 628, DateTimeKind.Local).AddTicks(580), "Outdoors, Toys & Toys", "Wallace_Koch@hotmail.com", new DateTime(2022, 8, 13, 23, 57, 15, 69, DateTimeKind.Local).AddTicks(7307) },
                    { 8L, "Debbie_Stracke32@hotmail.com", new DateTime(2022, 2, 3, 9, 50, 1, 932, DateTimeKind.Local).AddTicks(544), "Clothing & Electronics", "Debbie_Stracke32@hotmail.com", new DateTime(2022, 3, 4, 0, 49, 32, 668, DateTimeKind.Local).AddTicks(5428) },
                    { 9L, "Gary76@yahoo.com", new DateTime(2022, 8, 22, 15, 18, 38, 190, DateTimeKind.Local).AddTicks(9422), "Automotive", "Gary76@yahoo.com", new DateTime(2022, 5, 11, 20, 38, 9, 130, DateTimeKind.Local).AddTicks(9397) },
                    { 10L, "Jesse.Runolfsdottir75@hotmail.", new DateTime(2021, 12, 9, 6, 42, 39, 981, DateTimeKind.Local).AddTicks(9379), "Sports", "Jesse.Runolfsdottir75@hotmail.", new DateTime(2022, 7, 28, 22, 19, 36, 354, DateTimeKind.Local).AddTicks(1277) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "Description", "Name", "Price", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, 1L, "Dennis79@hotmail.com", new DateTime(2022, 5, 26, 0, 29, 58, 495, DateTimeKind.Local).AddTicks(8992), "Practical", "Sleek Metal Hat", 463.63m, "Dennis79@hotmail.com", new DateTime(2021, 11, 9, 4, 11, 4, 441, DateTimeKind.Local).AddTicks(3422) },
                    { 2L, 2L, "Isaac_Kulas96@gmail.com", new DateTime(2022, 1, 6, 14, 41, 43, 918, DateTimeKind.Local).AddTicks(5539), "Fantastic", "Tasty Rubber Chips", 423.38m, "Isaac_Kulas96@gmail.com", new DateTime(2022, 8, 22, 2, 12, 35, 602, DateTimeKind.Local).AddTicks(6966) },
                    { 3L, 3L, "Kathleen.Schamberger20@gmail.c", new DateTime(2021, 10, 8, 16, 39, 2, 214, DateTimeKind.Local).AddTicks(2442), "Handcrafted", "Handmade Plastic Chicken", 451.96m, "Kathleen.Schamberger20@gmail.c", new DateTime(2021, 10, 10, 3, 53, 0, 502, DateTimeKind.Local).AddTicks(969) },
                    { 4L, 4L, "Regina89@yahoo.com", new DateTime(2022, 9, 20, 18, 3, 18, 9, DateTimeKind.Local).AddTicks(9408), "Unbranded", "Handmade Wooden Pizza", 912.76m, "Regina89@yahoo.com", new DateTime(2022, 6, 2, 5, 24, 42, 680, DateTimeKind.Local).AddTicks(6626) },
                    { 5L, 5L, "Louise_Wehner@yahoo.com", new DateTime(2022, 9, 26, 10, 44, 34, 297, DateTimeKind.Local).AddTicks(5648), "Licensed", "Ergonomic Metal Cheese", 832.54m, "Louise_Wehner@yahoo.com", new DateTime(2022, 4, 30, 10, 58, 41, 514, DateTimeKind.Local).AddTicks(2828) },
                    { 6L, 6L, "Thomas.Bashirian@yahoo.com", new DateTime(2022, 6, 26, 10, 0, 40, 999, DateTimeKind.Local).AddTicks(3740), "Licensed", "Incredible Fresh Keyboard", 468.95m, "Thomas.Bashirian@yahoo.com", new DateTime(2022, 1, 27, 11, 0, 29, 69, DateTimeKind.Local).AddTicks(7909) },
                    { 7L, 7L, "Sheri47@yahoo.com", new DateTime(2022, 7, 13, 12, 52, 42, 455, DateTimeKind.Local).AddTicks(7576), "Awesome", "Handmade Concrete Salad", 137.51m, "Sheri47@yahoo.com", new DateTime(2022, 3, 9, 20, 4, 53, 132, DateTimeKind.Local).AddTicks(6439) },
                    { 8L, 8L, "Abraham_Kuhic@yahoo.com", new DateTime(2021, 10, 11, 23, 13, 39, 711, DateTimeKind.Local).AddTicks(1022), "Practical", "Refined Cotton Pizza", 562.86m, "Abraham_Kuhic@yahoo.com", new DateTime(2022, 7, 20, 2, 45, 46, 195, DateTimeKind.Local).AddTicks(8755) },
                    { 9L, 9L, "Catherine_Russel74@gmail.com", new DateTime(2021, 11, 20, 15, 28, 55, 271, DateTimeKind.Local).AddTicks(6737), "Ergonomic", "Unbranded Fresh Pizza", 426.99m, "Catherine_Russel74@gmail.com", new DateTime(2022, 9, 3, 17, 5, 56, 385, DateTimeKind.Local).AddTicks(5905) },
                    { 10L, 10L, "Leo_Parker@gmail.com", new DateTime(2022, 4, 20, 9, 30, 14, 641, DateTimeKind.Local).AddTicks(2464), "Unbranded", "Small Rubber Keyboard", 235.62m, "Leo_Parker@gmail.com", new DateTime(2021, 11, 15, 1, 5, 13, 462, DateTimeKind.Local).AddTicks(5757) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categorys");
        }
    }
}
