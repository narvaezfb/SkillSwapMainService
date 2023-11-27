using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SkillSwapMainService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Level = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OwnerID = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    UserSkillID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    SkillID = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => x.UserSkillID);
                    table.ForeignKey(
                        name: "FK_UserSkill_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillID",
                table: "UserSkill",
                column: "SkillID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}
