using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class _3ModelsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "invitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Remitente = table.Column<string>(type: "longtext", nullable: false),
                    Destinatario = table.Column<string>(type: "longtext", nullable: false),
                    Aceptada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitaciones", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomName = table.Column<string>(type: "longtext", nullable: false),
                    CreadorName = table.Column<string>(type: "longtext", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuariosRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRoom = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    Usuarioid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosRoom_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuariosRoom_Usuarios_Usuarioid",
                        column: x => x.Usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoom_RoomId",
                table: "UsuariosRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoom_Usuarioid",
                table: "UsuariosRoom",
                column: "Usuarioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invitaciones");

            migrationBuilder.DropTable(
                name: "UsuariosRoom");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "Activo", "Contraseña", "Email", "FechaRegistro", "Nombre" },
                values: new object[] { 1, true, "$2a$11$MOk/k3OC12lLkyGgBs.qTOBQf9hx/xjOnhiKXdyeV.DhVLh5NHz0y", "admin@example.com", new DateTime(2025, 5, 30, 12, 44, 15, 328, DateTimeKind.Utc).AddTicks(8483), "Administrador" });
        }
    }
}
