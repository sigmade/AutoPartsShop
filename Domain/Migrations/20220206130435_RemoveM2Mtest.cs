using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigmade.Domain.Migrations
{
    public partial class RemoveM2Mtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildSubChild");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "SubChild");

            migrationBuilder.DropTable(
                name: "Mains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubChild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Childs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Childs_Mains_MainId",
                        column: x => x.MainId,
                        principalTable: "Mains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildSubChild",
                columns: table => new
                {
                    ChildsId = table.Column<int>(type: "int", nullable: false),
                    SubChildsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildSubChild", x => new { x.ChildsId, x.SubChildsId });
                    table.ForeignKey(
                        name: "FK_ChildSubChild_Childs_ChildsId",
                        column: x => x.ChildsId,
                        principalTable: "Childs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildSubChild_SubChild_SubChildsId",
                        column: x => x.SubChildsId,
                        principalTable: "SubChild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Childs_MainId",
                table: "Childs",
                column: "MainId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildSubChild_SubChildsId",
                table: "ChildSubChild",
                column: "SubChildsId");
        }
    }
}
