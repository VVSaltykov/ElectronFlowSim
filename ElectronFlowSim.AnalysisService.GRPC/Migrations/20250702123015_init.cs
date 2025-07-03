using System;
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
                    j1 = table.Column<int>(type: "integer", nullable: false),
                    icr = table.Column<int>(type: "integer", nullable: false),
                    jcr = table.Column<int>(type: "integer", nullable: false),
                    r = table.Column<double[]>(type: "double precision[]", nullable: false),
                    z = table.Column<double[]>(type: "double precision[]", nullable: false),
                    u = table.Column<double[]>(type: "double precision[]", nullable: false),
                    l = table.Column<int[]>(type: "integer[]", nullable: false),
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
                    ar1s = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputDatas");
        }
    }
}
