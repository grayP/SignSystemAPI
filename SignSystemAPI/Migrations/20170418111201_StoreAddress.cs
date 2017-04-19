using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignSystem.API.Migrations
{
    public partial class StoreAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Stores",
                maxLength: 255,
                nullable: true);

       

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Stores",
                maxLength: 10,
                nullable: true);

          

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PointsOfInterest");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Stores",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "PointsOfInterest",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cities",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
