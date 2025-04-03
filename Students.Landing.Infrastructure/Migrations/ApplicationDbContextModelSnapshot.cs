﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Students.Landing.Core.Data;

#nullable disable

namespace Students.Landing.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Students.Landing.Core.Models.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Achievements")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndYear")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("InstitutionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Interests")
                        .HasColumnType("text");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("uuid");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Motivation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OrganisationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<int>("PracePracticeType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PracticeEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PracticeFieldId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PracticeStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartYear")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StudentCardPhotoUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("WorkExp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.HasIndex("MajorId");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("PracticeFieldId");

                    b.HasIndex("UserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Institution", b =>
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

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.InstitutionMajor", b =>
                {
                    b.Property<Guid>("InstitutionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("InstitutionId", "MajorId");

                    b.HasIndex("MajorId");

                    b.ToTable("InstitutionMajors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PracticeFieldId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PracticeFieldId");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Organisation", b =>
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

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.OrganisationPracticeField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PracticeFieldId")
                        .HasColumnType("uuid");

                    b.Property<int>("Used")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("PracticeFieldId");

                    b.ToTable("OrganisationPracticeFields");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.PracticeField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PracticeFields");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Application", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId");

                    b.HasOne("Students.Landing.Core.Models.PracticeField", "PracticeField")
                        .WithMany()
                        .HasForeignKey("PracticeFieldId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.User", "User")
                        .WithMany("Applications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Institution");

                    b.Navigation("Major");

                    b.Navigation("Organisation");

                    b.Navigation("PracticeField");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.InstitutionMajor", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Institution", "Institution")
                        .WithMany("InstitutionMajors")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.Major", "Major")
                        .WithMany("InstitutionMajors")
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.PracticeField", "PracticeField")
                        .WithMany("Majors")
                        .HasForeignKey("PracticeFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PracticeField");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.OrganisationPracticeField", b =>
                {
                    b.HasOne("Students.Landing.Core.Models.Organisation", "Organisation")
                        .WithMany("OrganisationPracticeFields")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Landing.Core.Models.PracticeField", "PracticeField")
                        .WithMany()
                        .HasForeignKey("PracticeFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");

                    b.Navigation("PracticeField");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Institution", b =>
                {
                    b.Navigation("InstitutionMajors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Major", b =>
                {
                    b.Navigation("InstitutionMajors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.Organisation", b =>
                {
                    b.Navigation("OrganisationPracticeFields");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.PracticeField", b =>
                {
                    b.Navigation("Majors");
                });

            modelBuilder.Entity("Students.Landing.Core.Models.User", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
