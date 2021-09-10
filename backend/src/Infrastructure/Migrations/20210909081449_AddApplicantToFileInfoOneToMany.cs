using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddApplicantToFileInfoOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "FileInfos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_ApplicantId",
                table: "FileInfos",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "applicant_fileInfo_FK",
                table: "FileInfos",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "applicant_fileInfo_FK",
                table: "FileInfos");

            migrationBuilder.DropIndex(
                name: "IX_FileInfos_ApplicantId",
                table: "FileInfos");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "FileInfos");
        }
    }
}
