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
                    nl = table.Column<int>(type: "integer", nullable: false),
                    j1 = table.Column<int>(type: "integer", nullable: false),
                    icr = table.Column<int>(type: "integer", nullable: false),
                    jcr = table.Column<int>(type: "integer", nullable: false),
                    rk = table.Column<double>(type: "double precision", nullable: false),
                    utep = table.Column<double>(type: "double precision", nullable: false),
                    zkon = table.Column<double>(type: "double precision", nullable: false),
                    akl1 = table.Column<double>(type: "double precision", nullable: false),
                    u0 = table.Column<double>(type: "double precision", nullable: false),
                    uekvip = table.Column<double[]>(type: "double precision[]", nullable: false),
                    abm = table.Column<double>(type: "double precision", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "BMTableDatas",
                columns: table => new
                {
                    InputDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Z = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    Bm = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    bnorm = table.Column<double>(type: "double precision", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMTableDatas", x => x.InputDataId);
                    table.ForeignKey(
                        name: "FK_BMTableDatas_InputDatas_InputDataId",
                        column: x => x.InputDataId,
                        principalTable: "InputDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NLTableDatas",
                columns: table => new
                {
                    InputDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    N = table.Column<List<int>>(type: "integer[]", nullable: false),
                    L = table.Column<List<int>>(type: "integer[]", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLTableDatas", x => x.InputDataId);
                    table.ForeignKey(
                        name: "FK_NLTableDatas_InputDatas_InputDataId",
                        column: x => x.InputDataId,
                        principalTable: "InputDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NZRUTableDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InputDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    N = table.Column<List<int>>(type: "integer[]", nullable: false),
                    R = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    Z = table.Column<List<double>>(type: "double precision[]", nullable: false),
                    U = table.Column<double>(type: "double precision", nullable: false),
                    WorkpieceType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NZRUTableDatas", x => new { x.InputDataId, x.Id });
                    table.ForeignKey(
                        name: "FK_NZRUTableDatas_InputDatas_InputDataId",
                        column: x => x.InputDataId,
                        principalTable: "InputDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BMTableDatas");

            migrationBuilder.DropTable(
                name: "NLTableDatas");

            migrationBuilder.DropTable(
                name: "NZRUTableDatas");

            migrationBuilder.DropTable(
                name: "InputDatas");
        }
    }
}
