using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "fas fa-coffee", "مشروبات" },
                    { 2, "fas fa-hamburger", "أطعمة" }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "DiscountAmount", "ExpiryDate", "IsActive" },
                values: new object[,]
                {
                    { 1, "WELCOME10", 10.00m, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, "SUMMER5", 5.00m, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name", "Phone" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "الرياض", "عميل افتراضي", "0500000001" },
                    { "22222222-2222-2222-2222-222222222222", "جدة", "زائر", "0500000002" }
                });

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "Id", "DiscountName", "DiscountValue" },
                values: new object[,]
                {
                    { 1, "خصم موسم الصيف", 15.0 },
                    { 2, "عرض نهاية الأسبوع", 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 2, "جبنة إضافية", 3.00m },
                    { 2, 1, "سكر إضافي", 1.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "PaymentMethod", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "11111111-1111-1111-1111-111111111111", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", "Completed", 21.50m },
                    { 2, "22222222-2222-2222-2222-222222222222", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card", "InProgress", 35.00m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "قهوة تركية ساخنة", "قهوة تركي", 12.50m, 100 },
                    { 2, 1, "شاي أخضر", "شاي", 8.00m, 150 },
                    { 3, 2, "برجر مع جبن", "برجر لحم", 35.00m, 50 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 12.50m },
                    { 2, 1, 2, 1, 9.00m },
                    { 3, 2, 3, 1, 35.00m }
                });

            migrationBuilder.InsertData(
                table: "ExtrasOrderItem",
                columns: new[] { "ExtrasId", "OrderItemsOrderItemId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExtrasOrderItem",
                keyColumns: new[] { "ExtrasId", "OrderItemsOrderItemId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ExtrasOrderItem",
                keyColumns: new[] { "ExtrasId", "OrderItemsOrderItemId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");
        }
    }
}
