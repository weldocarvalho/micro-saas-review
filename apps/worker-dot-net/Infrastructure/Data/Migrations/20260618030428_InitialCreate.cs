using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceWorker.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skin_analyses",
                columns: table => new
                {
                    analysis_id = table.Column<Guid>(type: "uuid", nullable: false),
                    paciente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    overall_health_score = table.Column<float>(type: "real", nullable: false),
                    conditions_json = table.Column<string>(type: "jsonb", nullable: false),
                    recommendations_json = table.Column<string>(type: "jsonb", nullable: false),
                    analyzed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skin_analyses", x => x.analysis_id);
                });

            migrationBuilder.CreateTable(
                name: "schedule_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    schedule_day = table.Column<int>(type: "integer", nullable: false),
                    recommended_product = table.Column<string>(type: "text", nullable: false),
                    usage_instruction = table.Column<string>(type: "text", nullable: false),
                    schedule_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedule_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_schedule_items_schedules_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_schedule_items_schedule_id",
                table: "schedule_items",
                column: "schedule_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedule_items");

            migrationBuilder.DropTable(
                name: "skin_analyses");

            migrationBuilder.DropTable(
                name: "schedules");
        }
    }
}
