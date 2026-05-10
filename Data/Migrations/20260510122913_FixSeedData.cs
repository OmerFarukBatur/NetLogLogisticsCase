using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sistem", "Admin", 1 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ahmet", "Yılmaz", 2 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ayşe", "Kaya", 3 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mehmet", "Demir", 4 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fatma", "Çelik", 5 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ali", "Şahin", 6 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Zeynep", "Arslan", 7 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mustafa", "Koç", 8 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Elif", "Erdoğan", 9 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hasan", "Doğan", 10 });

            migrationBuilder.InsertData(
                table: "Personnels",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "FirstName", "IsDeleted", "LastName", "Status", "UpdatedAt", "UserId" },
                values: new object[] { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Merve", false, "Yıldız", 1, null, 11 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$BURAYA_GERCEK_HASH_GELECEK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmet", "Yılmaz", 2 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayşe", "Kaya", 3 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mehmet", "Demir", 4 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fatma", "Çelik", 5 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", "Şahin", 6 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeynep", "Arslan", 7 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mustafa", "Koç", 8 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elif", "Erdoğan", 9 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hasan", "Doğan", 10 });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "FirstName", "LastName", "UserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Merve", "Yıldız", 11 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$replacethiswithrealbcrypthashbeforedeployment." });
        }
    }
}
