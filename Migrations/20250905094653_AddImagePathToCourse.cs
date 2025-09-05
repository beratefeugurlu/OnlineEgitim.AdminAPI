using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEgitim.AdminAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Courses_CourseId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Courses_CourseId1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseId1",
                table: "PurchasedCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_Users_UserId1",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_CourseId1",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_UserId1",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CourseId1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "PurchasedCourses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PurchasedCourses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Instructor",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Courses",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Courses_CourseId",
                table: "OrderItems",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Courses_CourseId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "PurchasedCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "PurchasedCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Instructor",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CourseId1",
                table: "PurchasedCourses",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_UserId1",
                table: "PurchasedCourses",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CourseId1",
                table: "OrderItems",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Courses_CourseId",
                table: "OrderItems",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Courses_CourseId1",
                table: "OrderItems",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_Courses_CourseId1",
                table: "PurchasedCourses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_Users_UserId1",
                table: "PurchasedCourses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
