﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Students.Landing.Core.Data;

#nullable disable

namespace Students.Landing.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250326091028_AddStudent")]
    partial class AddStudent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Students.Landing.Core.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.CompanyDirection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SpecializationDirectionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Used")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SpecializationDirectionId");

                    b.ToTable("CompanyDirections");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SpecializationDirectionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationDirectionId");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.SpecializationDirection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SpecializationDirections");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EnrollmentYear")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("GraduationYear")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("KeycloakUserId")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("uuid");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<string>("StudentCardPhotoUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MajorId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.StudentApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Achievements")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyDirectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Interests")
                        .HasColumnType("text");

                    b.Property<bool>("IsUniversityApproved")
                        .HasColumnType("boolean");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Motivation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PracticeComment")
                        .HasColumnType("text");

                    b.Property<DateTime>("PracticeEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("PracticeStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PracticeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("WorkExperience")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyDirectionId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentApplications");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.University", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.UniversityMajor", b =>
                {
                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("UniversityId", "MajorId");

                    b.HasIndex("MajorId");

                    b.ToTable("UniversityMajors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.CompanyDirection", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Company", "Company")
                        .WithMany("CompanyDirections")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.SpecializationDirection", "SpecializationDirection")
                        .WithMany()
                        .HasForeignKey("SpecializationDirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("SpecializationDirection");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.SpecializationDirection", "SpecializationDirection")
                        .WithMany("Majors")
                        .HasForeignKey("SpecializationDirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpecializationDirection");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Student", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("University");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.StudentApplication", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.CompanyDirection", "CompanyDirection")
                        .WithMany()
                        .HasForeignKey("CompanyDirectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Students.Landing.Core.Models.Student", "Student")
                        .WithMany("StudentApplications")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CompanyDirection");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.UniversityMajor", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Major", "Major")
                        .WithMany("UniversityMajors")
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.University", "University")
                        .WithMany("UniversityMajors")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("University");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Company", b =>
                {
                    b.Navigation("CompanyDirections");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.Navigation("UniversityMajors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.SpecializationDirection", b =>
                {
                    b.Navigation("Majors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Student", b =>
                {
                    b.Navigation("StudentApplications");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.University", b =>
                {
                    b.Navigation("UniversityMajors");
                });
#pragma warning restore 612, 618
        }
    }
}
