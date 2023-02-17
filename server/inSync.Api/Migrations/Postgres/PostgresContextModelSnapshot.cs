﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using inSync.Api.Data;

#nullable disable

namespace inSync.Api.Migrations.Postgres
{
    [DbContext(typeof(PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("inSync.Core.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ItemListId")
                        .HasColumnType("uuid");

                    b.Property<string>("ResourceKey")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ItemListId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("inSync.Core.Models.ItemList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLockedByAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LockReason")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ItemLists");
                });

            modelBuilder.Entity("inSync.Core.Models.MinecraftItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("ResourceKey")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MinecraftItems");
                });

            modelBuilder.Entity("inSync.Core.Models.Item", b =>
                {
                    b.HasOne("inSync.Core.Models.ItemList", null)
                        .WithMany("Items")
                        .HasForeignKey("ItemListId");
                });

            modelBuilder.Entity("inSync.Core.Models.ItemList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}