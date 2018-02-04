﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MoreAuctions.Models;
using System;

namespace MoreAuctions.Migrations
{
    [DbContext(typeof(MoreAuctionsContext))]
    partial class MoreAuctionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoreAuctions.Models.Auction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("MoreAuctions.Models.AuctionItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuctionID");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<float>("StartPrice");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("AuctionID", "Title")
                        .IsUnique();

                    b.ToTable("AuctionItem");
                });

            modelBuilder.Entity("MoreAuctions.Models.AuctionItem", b =>
                {
                    b.HasOne("MoreAuctions.Models.Auction")
                        .WithMany("Items")
                        .HasForeignKey("AuctionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
