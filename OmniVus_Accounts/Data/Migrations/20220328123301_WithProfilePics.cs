using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniVus_Accounts.Data.Migrations
{
    public partial class WithProfilePics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AspNetUsersInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsersInfo");
        }
    }
}
