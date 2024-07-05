using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartERP.Development.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "development");

            migrationBuilder.CreateTable(
                name: "CustomModules",
                schema: "development",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomEntities",
                schema: "development",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEntities_CustomModules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "development",
                        principalTable: "CustomModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomViews",
                schema: "development",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomViews_CustomModules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "development",
                        principalTable: "CustomModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomEntityFields",
                schema: "development",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEntityFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEntityFields_CustomEntities_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "development",
                        principalTable: "CustomEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntities_ModuleId",
                schema: "development",
                table: "CustomEntities",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEntityFields_EntityId",
                schema: "development",
                table: "CustomEntityFields",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomViews_ModuleId",
                schema: "development",
                table: "CustomViews",
                column: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomEntityFields",
                schema: "development");

            migrationBuilder.DropTable(
                name: "CustomViews",
                schema: "development");

            migrationBuilder.DropTable(
                name: "CustomEntities",
                schema: "development");

            migrationBuilder.DropTable(
                name: "CustomModules",
                schema: "development");
        }
    }
}
