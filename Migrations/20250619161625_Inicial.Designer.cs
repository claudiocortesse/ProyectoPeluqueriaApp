﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoPeluqueriaApp.Data;

#nullable disable

namespace ProyectoPeluqueriaApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250619161625_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("ProyectoPeluqueriaApp.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Turnos");
                });
#pragma warning restore 612, 618
        }
    }
}
