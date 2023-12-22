﻿// <auto-generated />
using System;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231219111931_EnrolledSubjectsEntryCreated")]
    partial class EnrolledSubjectsEntryCreated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnrolledSubjectsEntryId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnrolledSubjectsEntryId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjectsEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EnrolledSubjectsEntry");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjects", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjectsEntry", "EnrolledSubjectsEntry")
                        .WithMany("EnrolledSubjects")
                        .HasForeignKey("EnrolledSubjectsEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", "Student")
                        .WithMany("EnrolledSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Subject", "Subject")
                        .WithMany("EnrolledSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnrolledSubjectsEntry");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", "User")
                        .WithOne("Professor")
                        .HasForeignKey("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", "User")
                        .WithOne("Students")
                        .HasForeignKey("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjectsEntry", b =>
                {
                    b.Navigation("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", b =>
                {
                    b.Navigation("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Subject", b =>
                {
                    b.Navigation("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", b =>
                {
                    b.Navigation("Professor");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
