using Microsoft.EntityFrameworkCore.Migrations;

namespace Collectors.Data.Migrations
{
    public partial class Tags2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectibleTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectibleId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectibleTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectibleTags_Collectibles_CollectibleId",
                        column: x => x.CollectibleId,
                        principalTable: "Collectibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectibleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18df721f-b7eb-4b7f-a659-578c5d8a4ea0", "AQAAAAEAACcQAAAAEMBJhBs+SgIh3VfyTU1UvCFnTwyO47h5YhSKW49Pv8khq3RRs+lB5nBwBCJKd0YQUg==" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectibleTags_CollectibleId",
                table: "CollectibleTags",
                column: "CollectibleId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectibleTags_TagId",
                table: "CollectibleTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectibleTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aed42b48-56da-45b2-a664-1b3eb8e0ec40", "AQAAAAEAACcQAAAAEHvZJrA7a3TyjntyCTCNRN5mC+9YBMg378P15LyJJ6u0LTfjQFTPp5L1rpAavhdrWw==" });
        }
    }
}
