using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddCandidateToFileInfoOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_FileInfos_CvFileInfoId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_CvFileInfoId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "CvFileInfoId",
                table: "Applicants");

            migrationBuilder.AddColumn<string>(
                name: "CvFileInfoId",
                table: "VacancyCandidates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacancyCandidates_CvFileInfoId",
                table: "VacancyCandidates",
                column: "CvFileInfoId",
                unique: true,
                filter: "[CvFileInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "candidate_fileInfo_FK",
                table: "VacancyCandidates",
                column: "CvFileInfoId",
                principalTable: "FileInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "candidate_fileInfo_FK",
                table: "VacancyCandidates");

            migrationBuilder.DropIndex(
                name: "IX_VacancyCandidates_CvFileInfoId",
                table: "VacancyCandidates");

            migrationBuilder.DropColumn(
                name: "CvFileInfoId",
                table: "VacancyCandidates");

            migrationBuilder.AddColumn<string>(
                name: "CvFileInfoId",
                table: "Applicants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_CvFileInfoId",
                table: "Applicants",
                column: "CvFileInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_FileInfos_CvFileInfoId",
                table: "Applicants",
                column: "CvFileInfoId",
                principalTable: "FileInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
