using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class NuevosModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "correoElectronico",
                table: "Usuarios",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contraseña", "FechaRegistro" },
                values: new object[] { "$2a$11$MOk/k3OC12lLkyGgBs.qTOBQf9hx/xjOnhiKXdyeV.DhVLh5NHz0y", new DateTime(2025, 5, 30, 12, 44, 15, 328, DateTimeKind.Utc).AddTicks(8483) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "correoElectronico",
                table: "Usuarios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contraseña", "FechaRegistro" },
                values: new object[] { "$2a$11$EdM4LHp8PODf9seCPOLPVuPmyou2N08LKZH0soood3Xc5IAYvQ9e6", new DateTime(2025, 5, 16, 15, 15, 28, 123, DateTimeKind.Utc).AddTicks(3409) });
        }
    }
}
