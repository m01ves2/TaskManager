using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceIsCompletedWithStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                    name: "Status",
                    table: "Tasks",
                    type: "INTEGER",
                    nullable: false,
                    defaultValue: 0);

            migrationBuilder.Sql("""
                    UPDATE Tasks
                    SET Status =
                        CASE
                            WHEN IsCompleted = 1 THEN 1
                            ELSE 0
                        END
                """);

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Status",
            //    table: "Tasks",
            //    newName: "IsCompleted");
            migrationBuilder.AddColumn<int>(
                    name: "IsCompleted",
                    table: "Tasks",
                    type: "INTEGER",
                    nullable: false,
                    defaultValue: 0);
            migrationBuilder.Sql("""
                    UPDATE Tasks
                    SET IsCompleted =
                        CASE
                            WHEN Status = 1 THEN 1
                            ELSE 0
                        END
                """);
            migrationBuilder.DropColumn(
            name: "Status",
            table: "Tasks");
        }
    }
}
