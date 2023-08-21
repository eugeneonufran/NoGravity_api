using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoGravity.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_Path",
                table: "Tickets",
                newName: "filePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "filePath",
                table: "Tickets",
                newName: "File_Path");
        }
    }
}
