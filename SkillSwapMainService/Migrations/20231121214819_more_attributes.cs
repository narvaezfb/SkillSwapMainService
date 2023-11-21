using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwapMainService.Migrations
{
    /// <inheritdoc />
    public partial class more_attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillID",
                table: "UserSkill",
                column: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skill_SkillID",
                table: "UserSkill",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "SkillID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skill_SkillID",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillID",
                table: "UserSkill");
        }
    }
}
