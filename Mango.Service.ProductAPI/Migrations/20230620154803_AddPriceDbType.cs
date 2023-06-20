using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceDbType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://product73oktiews6luy.blob.core.windows.net/mango-product/14.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://product73oktiews6luy.blob.core.windows.net/mango-product/12.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://product73oktiews6luy.blob.core.windows.net/mango-product/11.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://product73oktiews6luy.blob.core.windows.net/mango-product/13.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://whitechapel5991.blob.core.windows.net/mango-product/14.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://whitechapel5991.blob.core.windows.net/mango-product/12.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://whitechapel5991.blob.core.windows.net/mango-product/11.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://whitechapel5991.blob.core.windows.net/mango-product/13.jpg");
        }
    }
}
