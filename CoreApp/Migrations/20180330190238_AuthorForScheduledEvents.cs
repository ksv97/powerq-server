using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreApp.Migrations
{
    public partial class AuthorForScheduledEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_Users_UserId",
                table: "ScheduledEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ScheduledEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "ScheduledEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledEvents_AuthorId",
                table: "ScheduledEvents",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_Users_AuthorId",
                table: "ScheduledEvents",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_Users_UserId",
                table: "ScheduledEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_Users_AuthorId",
                table: "ScheduledEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_Users_UserId",
                table: "ScheduledEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledEvents_AuthorId",
                table: "ScheduledEvents");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ScheduledEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ScheduledEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_Users_UserId",
                table: "ScheduledEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
