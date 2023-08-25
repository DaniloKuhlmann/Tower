﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tower.Database;

#nullable disable

namespace Tower.Migrations
{
    [DbContext(typeof(BDContext))]
    [Migration("00000000000000_Start")]
    partial class Start
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Tower.Database.Acesso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataHoraSaida")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PessoaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PessoaID");

                    b.ToTable("Acessos");
                });

            modelBuilder.Entity("Tower.Database.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Empresa")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Tower.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Tower.Database.Acesso", b =>
                {
                    b.HasOne("Tower.Database.Pessoa", "Pessoa")
                        .WithMany("Acessos")
                        .HasForeignKey("PessoaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Tower.Database.Pessoa", b =>
                {
                    b.Navigation("Acessos");
                });
#pragma warning restore 612, 618
        }
    }
}
