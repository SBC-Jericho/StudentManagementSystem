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
    [Migration("20240126062402_OwnerIdToGroupChat")]
    partial class OwnerIdToGroupChat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Announcements");
                });

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

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.BorrowedBooks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryId");

                    b.ToTable("BorrowedBooks");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnrollmentId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("SubjectId");

                    b.ToTable("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("SchoolYear")
                        .HasColumnType("int");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GroupChats");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupChatId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupChatId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupChatMessages");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BorrowedReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateBorrowed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReturn")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Libraries");
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

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("GroupChatUser", b =>
                {
                    b.Property<int>("GroupChatsId")
                        .HasColumnType("int");

                    b.Property<int>("PaticipantsId")
                        .HasColumnType("int");

                    b.HasKey("GroupChatsId", "PaticipantsId");

                    b.HasIndex("PaticipantsId");

                    b.ToTable("GroupChatUser");
                });

            modelBuilder.Entity("ProfessorSubject", b =>
                {
                    b.Property<int>("ProfessorsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("ProfessorsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("ProfessorSubject");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.BorrowedBooks", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Library", "Library")
                        .WithMany("BorrowedBooks")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.ChatMessage", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", "Users")
                        .WithMany("ChatMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.EnrolledSubjects", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Enrollment", "Enrollment")
                        .WithMany("EnrolledSubjects")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", "Professor")
                        .WithMany("EnrolledSubjects")
                        .HasForeignKey("ProfessorId");

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");

                    b.Navigation("Professor");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Enrollment", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", "Student")
                        .WithMany("Enrollment")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChatMessage", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChat", "GroupChat")
                        .WithMany("Messages")
                        .HasForeignKey("GroupChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Library", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
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

            modelBuilder.Entity("GroupChatUser", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChat", null)
                        .WithMany()
                        .HasForeignKey("GroupChatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("PaticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfessorSubject", b =>
                {
                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", null)
                        .WithMany()
                        .HasForeignKey("ProfessorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Enrollment", b =>
                {
                    b.Navigation("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.GroupChat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Library", b =>
                {
                    b.Navigation("BorrowedBooks");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Professor", b =>
                {
                    b.Navigation("EnrolledSubjects");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.Student", b =>
                {
                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("BlazorWasmDotNet8AspNetCoreHosted.Shared.Models.User", b =>
                {
                    b.Navigation("ChatMessages");

                    b.Navigation("Professor");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
