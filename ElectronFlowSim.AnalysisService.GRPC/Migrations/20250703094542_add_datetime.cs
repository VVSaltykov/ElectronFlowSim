using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronFlowSim.AnalysisService.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class add_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SaveDateTime",
                table: "InputDatas",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveDateTime",
                table: "InputDatas");
        }
    }
}
