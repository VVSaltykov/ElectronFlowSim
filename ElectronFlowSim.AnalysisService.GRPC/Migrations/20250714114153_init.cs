using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronFlowSim.AnalysisService.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NLTableDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    N = table.Column<List<int>>(type: "integer[]", nullable: false),
                    L = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLTableDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NZRUTableDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    N = table.Column<List<int>>(type: "integer[]", nullable: false),
                    R = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    Z = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    U = table.Column<double>(type: "double precision", nullable: false),
                    WorkpieceType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NZRUTableDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ig = table.Column<int>(type: "integer", nullable: false),
                    nmas = table.Column<int>(type: "integer", nullable: false),
                    km = table.Column<int>(type: "integer", nullable: false),
                    kp = table.Column<int>(type: "integer", nullable: false),
                    kq = table.Column<int>(type: "integer", nullable: false),
                    kpj6 = table.Column<int>(type: "integer", nullable: false),
                    ik = table.Column<int>(type: "integer", nullable: false),
                    j1 = table.Column<int>(type: "integer", nullable: false),
                    icr = table.Column<int>(type: "integer", nullable: false),
                    jcr = table.Column<int>(type: "integer", nullable: false),
                    NLTableDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    rk = table.Column<double>(type: "double precision", nullable: false),
                    utep = table.Column<double>(type: "double precision", nullable: false),
                    zkon = table.Column<double>(type: "double precision", nullable: false),
                    akl1 = table.Column<double>(type: "double precision", nullable: false),
                    u0 = table.Column<double>(type: "double precision", nullable: false),
                    uekvip = table.Column<double[]>(type: "double precision[]", nullable: false),
                    bnorm = table.Column<double>(type: "double precision", nullable: false),
                    abm = table.Column<double>(type: "double precision", nullable: false),
                    bm = table.Column<double[]>(type: "double precision[]", nullable: false),
                    aik = table.Column<double[]>(type: "double precision[]", nullable: false),
                    ht = table.Column<double>(type: "double precision", nullable: false),
                    dz = table.Column<double>(type: "double precision", nullable: false),
                    dtok = table.Column<double>(type: "double precision", nullable: false),
                    hq1 = table.Column<double>(type: "double precision", nullable: false),
                    ar1s = table.Column<double>(type: "double precision", nullable: false),
                    SaveDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SaveName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputDatas_NLTableDatas_NLTableDataId",
                        column: x => x.NLTableDataId,
                        principalTable: "NLTableDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InputDataNZRUTableData",
                columns: table => new
                {
                    InputDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    NZRUTableDatasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDataNZRUTableData", x => new { x.InputDataId, x.NZRUTableDatasId });
                    table.ForeignKey(
                        name: "FK_InputDataNZRUTableData_InputDatas_InputDataId",
                        column: x => x.InputDataId,
                        principalTable: "InputDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InputDataNZRUTableData_NZRUTableDatas_NZRUTableDatasId",
                        column: x => x.NZRUTableDatasId,
                        principalTable: "NZRUTableDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputDataNZRUTableData_NZRUTableDatasId",
                table: "InputDataNZRUTableData",
                column: "NZRUTableDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_InputDatas_NLTableDataId",
                table: "InputDatas",
                column: "NLTableDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputDataNZRUTableData");

            migrationBuilder.DropTable(
                name: "InputDatas");

            migrationBuilder.DropTable(
                name: "NZRUTableDatas");

            migrationBuilder.DropTable(
                name: "NLTableDatas");
        }
    }
}
