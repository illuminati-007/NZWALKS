﻿// <auto-generated />
using System;
using FP_NZWALKS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FP_NZWALKS.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    [Migration("20230413205044_One big Migration ")]
    partial class OnebigMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FP_NZWALKS.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f536df82-2678-465d-9d66-d0b5a4818488"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("a7c20756-17ae-4837-b798-77db79d43f0f"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("4abb4eea-404f-4138-9fb9-d0f1fb18aa33"),
                            Name = "Hardcore"
                        });
                });

            modelBuilder.Entity("FP_NZWALKS.Models.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FP_NZWALKS.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2f73ecc4-0559-4ae2-b022-96b95df21d6f"),
                            Code = "LV",
                            Name = "Lviv",
                            RegionImageUrl = "https://wallpapercave.com/wp/wp8910550.jpg"
                        },
                        new
                        {
                            Id = new Guid("9ca7b68f-5a8a-4e5e-84f4-c1d3e7c784b9"),
                            Code = "KV",
                            Name = "Kyiv"
                        },
                        new
                        {
                            Id = new Guid("46bd6124-c108-421a-8440-8a8b4ad04873"),
                            Code = "OD",
                            Name = "Odessa",
                            RegionImageUrl = "https://vie-en-ukraine.fr/wp-content/uploads/2019/11/odessa.jpeg"
                        },
                        new
                        {
                            Id = new Guid("3320a8db-bd57-4d9c-8549-1aa907943682"),
                            Code = "BR",
                            Name = "Bremen",
                            RegionImageUrl = "https://assets-global.website-files.com/5bd82da7b7abc53f312e765d/5c7565f1d885aa313b6db38e_iStock-680198122%20-%20Bremen%20Cathedral.jpg"
                        },
                        new
                        {
                            Id = new Guid("02f72dcc-adeb-4e21-aca9-795eba41a42c"),
                            Code = "BR",
                            Name = "Berlin"
                        },
                        new
                        {
                            Id = new Guid("cb52cea1-d6cd-4ccd-a19e-65ebc67e447c"),
                            Code = "HB  ",
                            Name = "Hamburg",
                            RegionImageUrl = "https://www.marvest.de/wp-content/uploads/2019/09/travel-3136679_1920.jpg"
                        });
                });

            modelBuilder.Entity("FP_NZWALKS.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("FP_NZWALKS.Models.Domain.Walk", b =>
                {
                    b.HasOne("FP_NZWALKS.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FP_NZWALKS.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
