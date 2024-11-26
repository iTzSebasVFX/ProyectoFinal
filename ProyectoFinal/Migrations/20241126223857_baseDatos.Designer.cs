﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ProyectoFinal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126223857_baseDatos")]
    partial class baseDatos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProyectoFinal.Models.RegistroModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("apellidoCompleto")
                        .HasColumnType("longtext");

                    b.Property<string>("contrasena")
                        .HasColumnType("longtext");

                    b.Property<string>("correoElectronico")
                        .HasColumnType("longtext");

                    b.Property<int>("fechaNacimiento")
                        .HasColumnType("int");

                    b.Property<string>("nombreCompleto")
                        .HasColumnType("longtext");

                    b.Property<string>("numeroTelefono")
                        .HasColumnType("longtext");

                    b.Property<string>("pais")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}