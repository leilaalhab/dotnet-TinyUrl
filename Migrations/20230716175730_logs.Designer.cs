﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet_rpg.Data;

#nullable disable

namespace dotnet_rpg.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230716175730_logs")]
    partial class logs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("dotnet_rpg.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QandAId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QandAId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.Property<float>("charged")
                        .HasColumnType("real");

                    b.Property<int>("item")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("dotnet_rpg.Models.QandA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TinyUrlId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TinyUrlId");

                    b.HasIndex("userId");

                    b.ToTable("QandAs");
                });

            modelBuilder.Entity("dotnet_rpg.Models.TinyUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsagesCount")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TinyUrls");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Usage", b =>
                {
                    b.Property<int>("UsageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsageId"));

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.Property<DateTime>("UseTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("tinyUrlId")
                        .HasColumnType("int");

                    b.HasKey("UsageId");

                    b.HasIndex("tinyUrlId");

                    b.ToTable("Usages");
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subscriptionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Answer", b =>
                {
                    b.HasOne("dotnet_rpg.Models.QandA", "QandA")
                        .WithMany("Answers")
                        .HasForeignKey("QandAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QandA");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Bill", b =>
                {
                    b.HasOne("dotnet_rpg.Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_rpg.Models.QandA", b =>
                {
                    b.HasOne("dotnet_rpg.Models.TinyUrl", null)
                        .WithMany("QandAs")
                        .HasForeignKey("TinyUrlId");

                    b.HasOne("dotnet_rpg.Models.User", "User")
                        .WithMany("QandAs")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_rpg.Models.TinyUrl", b =>
                {
                    b.HasOne("dotnet_rpg.Models.User", "User")
                        .WithMany("Urls")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Usage", b =>
                {
                    b.HasOne("dotnet_rpg.Models.TinyUrl", "tinyUrl")
                        .WithMany("Usages")
                        .HasForeignKey("tinyUrlId");

                    b.Navigation("tinyUrl");
                });

            modelBuilder.Entity("dotnet_rpg.Models.QandA", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("dotnet_rpg.Models.TinyUrl", b =>
                {
                    b.Navigation("QandAs");

                    b.Navigation("Usages");
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("QandAs");

                    b.Navigation("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}