﻿// <auto-generated />
using System;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SolucionInmobiliaria.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240312220138_CambiosEnVariables")]
    partial class CambiosEnVariables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SolucionInmobiliaria.Domain.Producto", b =>
                {
                    b.Property<string>("CodigoAlfanumero")
                        .HasColumnType("TEXT");

                    b.Property<string>("Barrio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("UrlImagen")
                        .HasColumnType("TEXT");

                    b.HasKey("CodigoAlfanumero");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            CodigoAlfanumero = "A1",
                            Barrio = "Palermo",
                            Descripcion = "Departamento de 2 ambientes",
                            Estado = 0,
                            Price = 100000.0,
                            UrlImagen = "https://www.google.com"
                        },
                        new
                        {
                            CodigoAlfanumero = "A2",
                            Barrio = "Recoleta",
                            Descripcion = "Departamento de 3 ambientes",
                            Estado = 0,
                            Price = 150000.0,
                            UrlImagen = "https://www.google.com"
                        });
                });

            modelBuilder.Entity("SolucionInmobiliaria.Domain.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteAsociadoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaDesde")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaHasta")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductoReservadoCodigoAlfanumero")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClienteAsociadoId");

                    b.HasIndex("ProductoReservadoCodigoAlfanumero");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("SolucionInmobiliaria.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rol")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SolucionInmobiliaria.Domain.Reserva", b =>
                {
                    b.HasOne("SolucionInmobiliaria.Domain.Usuario", "ClienteAsociado")
                        .WithMany("ReservasUsuario")
                        .HasForeignKey("ClienteAsociadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolucionInmobiliaria.Domain.Producto", "ProductoReservado")
                        .WithMany()
                        .HasForeignKey("ProductoReservadoCodigoAlfanumero");

                    b.Navigation("ClienteAsociado");

                    b.Navigation("ProductoReservado");
                });

            modelBuilder.Entity("SolucionInmobiliaria.Domain.Usuario", b =>
                {
                    b.Navigation("ReservasUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
