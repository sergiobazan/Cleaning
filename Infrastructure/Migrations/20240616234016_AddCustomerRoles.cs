using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_role",
                columns: table => new
                {
                    customers_id = table.Column<Guid>(type: "uuid", nullable: false),
                    roles_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_role", x => new { x.customers_id, x.roles_id });
                    table.ForeignKey(
                        name: "fk_customer_role_customers_customers_id",
                        column: x => x.customers_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_role_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("5a161a3a-bdd1-4bb2-964d-ce0eab94490d"), "Customer" },
                    { new Guid("8d3e1ac5-7933-4cfe-af04-4066c3bbb3c6"), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_role_roles_id",
                table: "customer_role",
                column: "roles_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_role");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
