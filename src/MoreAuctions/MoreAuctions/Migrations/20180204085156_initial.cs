using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MoreAuctions.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    StartPrice = table.Column<float>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuctionItem_Auction_AuctionID",
                        column: x => x.AuctionID,
                        principalTable: "Auction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_Description",
                table: "Auction",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItem_AuctionID_Title",
                table: "AuctionItem",
                columns: new[] { "AuctionID", "Title" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionItem");

            migrationBuilder.DropTable(
                name: "Auction");
        }
    }
}
