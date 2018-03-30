using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreApp.Migrations
{
    public partial class FeedbackAuthoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AuthorId",
                table: "Feedbacks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_AuthorId",
                table: "Feedbacks",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_AuthorId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AuthorId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Feedbacks");
        }
    }
}
