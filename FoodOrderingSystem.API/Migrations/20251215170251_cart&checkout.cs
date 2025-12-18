using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderingSystem.API.Migrations
{
    public partial class cartcheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FoodOrderingSystem");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A00E01FA1", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__States__C3BA3B3A3AAB524B", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C82A45960", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.Role,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cities__F2D21B7634B4F541", x => x.CityId);
                    table.ForeignKey(
                        name: "FK__Cities__StateId__0D7A0286",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carts__51BCD7B7829FF6C4", x => x.CartId);
                    table.ForeignKey(
                        name: "FK__Carts__UserId__245D67DE",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    customer_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__CD65CB85E95CCFD9", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK__Customers__user___531856C7",
                        column: x => x.user_id,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Addresse__091C2AFB04835BA0", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK__Addresses__CityI__18EBB532",
                        column: x => x.CityId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK__Addresses__State__19DFD96B",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK__Addresses__UserI__17F790F9",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Restaura__87454C9589ADDB5D", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK__Restauran__CityI__1332DBDC",
                        column: x => x.CityId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK__Restauran__State__14270015",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK__Restauran__UserI__123EB7A3",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuCate__19093A0BB2E4B208", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK__MenuCateg__Resta__1CBC4616",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BCF549B10CB", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__Orders__AddressI__2FCF1A8A",
                        column: x => x.AddressId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK__Orders__Restaura__2EDAF651",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK__Orders__UserId__2DE6D218",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79CE6DE03F6B", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK__Reviews__Restaur__6166761E",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK__Reviews__UserId__607251E5",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuItem__8943F72214936418", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK__MenuItems__Categ__208CD6FA",
                        column: x => x.CategoryId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuCategories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__488B0B0AA004A8EB", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK__CartItems__CartI__282DF8C2",
                        column: x => x.CartId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Carts",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK__CartItems__MenuI__29221CFB",
                        column: x => x.MenuItemId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED06815BAA9116", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK__OrderItem__MenuI__3493CFA7",
                        column: x => x.MenuItemId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId");
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__339FAB6E",
                        column: x => x.OrderId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "FoodOrderingSystem",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MenuItemId",
                schema: "FoodOrderingSystem",
                table: "CartItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                schema: "FoodOrderingSystem",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                schema: "FoodOrderingSystem",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_user_id",
                schema: "FoodOrderingSystem",
                table: "Customers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "MenuCategories",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                schema: "FoodOrderingSystem",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                schema: "FoodOrderingSystem",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "FoodOrderingSystem",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CityId",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_StateId",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UserId",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                schema: "FoodOrderingSystem",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__States__554763150B1239E7",
                schema: "FoodOrderingSystem",
                table: "States",
                column: "StateName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "MenuItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "MenuCategories",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Restaurants",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "States",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "FoodOrderingSystem");
        }
    }
}
