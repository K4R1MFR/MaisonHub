﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaisonHub.Migrations
{
    public partial class AddGarbageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garbage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasPutNewBag = table.Column<bool>(type: "bit", nullable: false),
                    HasTakenDownBin = table.Column<bool>(type: "bit", nullable: false),
                    HasTakenDownRecycling = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garbage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garbage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garbage_UserId",
                table: "Garbage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Garbage");
        }
    }
}
