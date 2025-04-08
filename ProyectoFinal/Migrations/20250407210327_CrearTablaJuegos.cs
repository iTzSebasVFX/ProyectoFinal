using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaJuegos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AdminUsers",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contraseña",
                table: "AdminUsers",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Juegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    ImagenFondoUrl = table.Column<string>(type: "longtext", nullable: false),
                    UrlJuego = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juegos", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contraseña", "FechaRegistro" },
                values: new object[] { "$2a$11$sWvehM9gYNvCVQG1h4Uez.eXHwly9OWHI8E.D08SyEmv5oqmnSXbe", new DateTime(2025, 4, 7, 21, 3, 27, 39, DateTimeKind.Utc).AddTicks(3944) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Juegos");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AdminUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Contraseña",
                table: "AdminUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contraseña", "FechaRegistro" },
                values: new object[] { "$2a$11$2jBDuM0hoeVEWIcbE5BaE..srN0SDgfq3eUy7zsJ/ejcNXBDh5arC", new DateTime(2025, 3, 26, 15, 41, 49, 683, DateTimeKind.Utc).AddTicks(1244) });
        }
    }
}
