using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDelivery.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cuisine = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    DeliveryTimeInMinutes = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    IsPromoted = table.Column<bool>(type: "boolean", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantMenus_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RestaurantMenuId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCategories_RestaurantMenus_RestaurantMenuId",
                        column: x => x.RestaurantMenuId,
                        principalTable: "RestaurantMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    FoodCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItems_FoodCategories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "FoodCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Latitude", "Longitude", "Street" },
                values: new object[,]
                {
                    { 1, "Dhaka", 40.712800000000001, -74.006, "Mirpur" },
                    { 2, "Dhaka", 34.052199999999999, -118.2437, "Mohammedpur" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "Cuisine", "DeliveryTimeInMinutes", "ImageUrl", "IsPromoted", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Italian, Mediterranean", 45, "https://img.freepik.com/free-photo/delicious-burger-with-many-ingredients-isolated-white-background-tasty-cheeseburger-splash-sauce_90220-1266.jpg?t=st=1715625020~exp=1715628620~hmac=ee0ee3d2f13079fdde6fceb6505a1c87055ea22bd4f475c0d1b08be27c7556fd&w=740", true, "Taste of Tuscany", 4.7000000000000002 },
                    { 2, 1, "Indian, Fusion", 50, "https://img.freepik.com/premium-photo/asian-food-chinese-fried-rice_711483-63.jpg?w=740", false, "Spice Route", 3.5 },
                    { 3, 1, "Japanese, Sushi", 35, "https://img.freepik.com/free-photo/well-done-steak-homemade-potatoes_140725-3989.jpg?t=st=1715625633~exp=1715629233~hmac=9358f465c2dc38126fd47b036513a1c40175c4e9c5b6f928f1ce67c535ff00a9&w=360", false, "Sushi Delight", 3.8999999999999999 },
                    { 4, 1, "French, Bakery", 40, "https://img.freepik.com/premium-photo/couscous-with-mussels-shrimps_135427-6342.jpg?w=900", false, "Cafe Parisienne", 4.5999999999999996 },
                    { 5, 1, "Italian, Pizza", 30, "https://img.freepik.com/free-photo/chicken-skewers-with-slices-sweet-peppers-dill_2829-18813.jpg?t=st=1715625626~exp=1715629226~hmac=00611d8c50b843d7394cae185a955cc6ef6043004e197aeeb52089c3f4c0bbe4&w=900", false, "Mamma Mia Pizzeria", 4.7999999999999998 },
                    { 6, 1, "Thai, Street Food", 55, "https://img.freepik.com/free-photo/fruit-salad-spilling-floor-was-mess-vibrant-colors-textures-generative-ai_8829-2895.jpg?t=st=1715625620~exp=1715629220~hmac=6b64105071cebcd0c9da994bcbcc88cd3ea991e25a8d072627160539f99099ec&w=360", true, "Thai Spice", 4.2999999999999998 },
                    { 7, 1, "American, BBQ", 45, "https://img.freepik.com/premium-photo/large-bowl-food-with-fish-vegetables_197463-2405.jpg?w=740", false, "Smokehouse BBQ", 4.4000000000000004 },
                    { 8, 1, "Mediterranean, Seafood", 40, "https://img.freepik.com/free-photo/baked-chicken-wings-asian-style-tomatoes-sauce-plate_2829-10657.jpg?t=st=1715625611~exp=1715629211~hmac=5c1d6b2f70119d1f65f1fc53eec7e531b920c69faa7f534c7c1cd62d4c7510e1&w=900", false, "Mediterranean Breeze", 4.7000000000000002 },
                    { 9, 1, "Indian, Bollywood Fusion", 50, "https://img.freepik.com/free-photo/baked-chicken-wings-asian-style-tomatoes-sauce-plate_2829-10657.jpg?t=st=1715625611~exp=1715629211~hmac=5c1d6b2f70119d1f65f1fc53eec7e531b920c69faa7f534c7c1cd62d4c7510e1&w=900", false, "Bollywood Spice", 4.5 },
                    { 10, 1, "Healthy, Salad", 35, "https://img.freepik.com/premium-photo/grilled-pork-beef-steaks-with-chilli-salt-are-falling-down-black-background-barbecue-grill_157927-18741.jpg?w=360", false, "Green Garden Salad Bar", 4.5999999999999996 },
                    { 11, 1, "Greek, Mediterranean", 25, "https://img.freepik.com/free-photo/greek-gyros-traditional-greek-food-made-beef-meat-tomatoes-onion-tzatziki-sauce-with-potato-fries-served-pita-bread_2829-11257.jpg?w=740&t=st=1715626298~exp=1715629898~hmac=60d1809d987647a0b76f78b07e5fcfb43a647c56dd4fa8a1c1bcb6e52d278c91", false, "The Greek Corner", 4.5 },
                    { 12, 1, "Korean", 35, "https://img.freepik.com/free-photo/traditional-korean-cuisine_1203-1624.jpg?w=740&t=st=1715626333~exp=1715629933~hmac=fe4f6170e5a1619e421eb13cce8f1b404c15d4175891268f98779c26a7158438", false, "Seoul Food", 4.5999999999999996 },
                    { 13, 1, "French, Café", 20, "https://img.freepik.com/free-photo/french-breakfast-with-croissants-cup-coffee-coffee-beans-orange-juice-poppy-seeds_2829-19440.jpg?w=740&t=st=1715626368~exp=1715629968~hmac=8e5d060d8a8e482276f97e4977a2d508e1b53c1ec9da57cbec646d50bbce27f5", false, "Café Parisien", 4.4000000000000004 },
                    { 14, 1, "Thai", 40, "https://img.freepik.com/free-photo/spicy-thai-food-with-herb-ingredients_1150-8117.jpg?w=740&t=st=1715626407~exp=1715630007~hmac=2ff788a3b5f04b1d29d152d52bb791f6ecb8b9a7d29d14d248f7be2f3b6a5d66", false, "Taste of Thailand", 4.7000000000000002 },
                    { 15, 1, "Middle Eastern", 50, "https://img.freepik.com/free-photo/middle-eastern-appetizers-tabule-kibbeh-vegetarian-falafel-spread_2829-14172.jpg?w=740&t=st=1715626443~exp=1715630043~hmac=9c83c5d5c42c6bc29a25a340a43015ad56de69a55bde69f1797b99505ed1c78e", false, "Middle Eastern Delights", 4.5 },
                    { 16, 1, "American, Soul Food", 45, "https://img.freepik.com/free-photo/soul-food-southern-united-states-traditional-southern-food_2829-14630.jpg?w=740&t=st=1715626475~exp=1715630075~hmac=35f8cc327f5f60d736505c0b102b4bbd29f78dd7810e0b82ac8a7277271a9a8d", false, "Soul Food Shack", 4.5999999999999996 },
                    { 17, 1, "Italian", 35, "https://img.freepik.com/free-photo/top-view-pasta-with-tomato-sauce-meatballs-plate_23-2148575934.jpg?w=740&t=st=1715626510~exp=1715630110~hmac=285b517ff52e67e7ed7ff9aa1aa83c5f13eb3ab5ab07f8a5bc6a88ae0bb75111", false, "Viva Italia", 4.7999999999999998 },
                    { 18, 1, "Chinese, Dim Sum", 40, "https://img.freepik.com/free-photo/dim-sum-food-dumpling-in-bamboo-steamer-with-dipping-sauces_2829-19348.jpg?w=740&t=st=1715626546~exp=1715630146~hmac=02d0e3ed5e97320b046b85b13e91d0a441b21e60ac629db60db2301768a07e7a", false, "Dim Sum House", 4.7000000000000002 },
                    { 19, 1, "Caribbean", 55, "https://img.freepik.com/free-photo/caribbean-food-dishes-table_23-2148483890.jpg?w=740&t=st=1715626577~exp=1715630177~hmac=046c3db3d1175edc83e4fabb0ab027ad4263f4b6df4e1a3c0b7c4d76482dbe1d", false, "Caribbean Flavors", 4.5 },
                    { 20, 1, "Mexican, Tacos", 30, "https://img.freepik.com/free-photo/mexican-tacos-tacos-with-meat-tomatoes-onions_2829-13776.jpg?w=740&t=st=1715626615~exp=1715630215~hmac=42a8f604f544e25d80afcb89a68fbc7fa4b280a80cc11b7cf5a2fa4f0c827a20", false, "La Taqueria", 4.4000000000000004 },
                    { 21, 2, "Vietnamese", 35, "https://img.freepik.com/free-photo/traditional-vietnamese-noodle-soup-pho-with-beef-raw-meat-dark-background_2829-13880.jpg?w=740&t=st=1715626651~exp=1715630251~hmac=60a2dcf1f04aa44e9ae9d7998c71f7b6c80b5a2d66d438b19d4491125406e08d", false, "Pho King", 4.5999999999999996 },
                    { 22, 2, "Spanish, Tapas", 25, "https://img.freepik.com/free-photo/assorted-spanish-tapas-served-wooden-board_1220-686.jpg?w=740&t=st=1715626690~exp=1715630290~hmac=26863ae94c06cfcadeea2c92080a27b2a07c5648fa4bba491bb7dc59438d4a27", false, "Tapas Bar", 4.5 },
                    { 23, 2, "American, Bagels", 20, "https://img.freepik.com/free-photo/multicolored-bagels-wooden-tray-grey-surface_2829-15295.jpg?w=740&t=st=1715626732~exp=1715630332~hmac=63a15c1bcb5e8d3c27cb74a98c9ef4b2007b141148ce1cfb46489e94d9ffcf35", false, "Big Apple Bagels", 4.2999999999999998 },
                    { 24, 2, "Indian, Pakistani", 45, "https://img.freepik.com/free-photo/indian-pakistani-food-assorted-set-top-view_2829-12298.jpg?w=740&t=st=1715626768~exp=1715630368~hmac=184ba7a0c6ea709def0f81b61d77ab82cd3535cfe1ab19a91d3c04bdf63ec4f2", false, "Spice Route", 4.7000000000000002 },
                    { 25, 2, "French, Café", 25, "https://img.freepik.com/free-photo/delicious-crepes-with-banana-nutella-topped-sugar_144627-32437.jpg?w=740&t=st=1715626807~exp=1715630407~hmac=cf6b0a59e9296374bc197a1c79b8b037e8e1dbab50c157b5d239c8e92d9a2553", false, "Crepe Café", 4.5 },
                    { 26, 2, "Brazilian", 55, "https://img.freepik.com/free-photo/brazilian-bbq-wooden-cutting-board_1203-1632.jpg?w=740&t=st=1715626841~exp=1715630441~hmac=cf516223c604e907aeab25f8e11e2b7e8b72d2a36904ea4e5b3cc8f2a7e2f4e7", false, "Brazilian BBQ", 4.5999999999999996 },
                    { 27, 2, "American, Burgers", 20, "https://img.freepik.com/free-photo/gourmet-burgers-with-homemade-burger-patties_1220-732.jpg?w=740&t=st=1715626876~exp=1715630476~hmac=fc22df2cf09f4a6d3982cf6073181ed36b7ad708306b03a45a589935af47e92f", false, "Gourmet Burgers", 4.4000000000000004 },
                    { 28, 2, "Hawaiian", 30, "https://img.freepik.com/free-photo/hawaiian-poke-bowl-with-tuna-top-view_2829-10804.jpg?w=740&t=st=1715626911~exp=1715630511~hmac=c9bb2f3da372bf303657e768f6ff669ee8a899d32f482130527c97c3e6b3e3ec", false, "Hawaiian Poke", 4.5 },
                    { 29, 2, "Belgian, Desserts", 20, "https://img.freepik.com/free-photo/belgian-waffles-with-berries-top-view_2829-10998.jpg?w=740&t=st=1715626947~exp=1715630547~hmac=3d4fe69d6b726d9c4b10357df67e7e70ad6c3b8f547af193aeb4edc09cf6e51e", false, "Belgian Waffles", 4.7999999999999998 },
                    { 30, 2, "Persian", 50, "https://img.freepik.com/free-photo/persian-food-skewer-fried-kebab-saffron-rice-top-view_2829-15440.jpg?w=740&t=st=1715626986~exp=1715630586~hmac=f4083f7f021eabe3ab4c8feefcd5c7e34e5468e9efbe3a30d2d2ec68c2a2d9d4", false, "Persian Grill", 4.7000000000000002 },
                    { 31, 2, "American, Diner", 25, "https://img.freepik.com/free-photo/classic-american-diner-meal-with-burgers-fries-milkshake_2829-10698.jpg?w=740&t=st=1715627024~exp=1715630624~hmac=0e748d11d1b9824bb29f409615a8158e6741914701a73535bbed1c0d98db83a7", false, "Classic Diner", 4.2999999999999998 },
                    { 32, 2, "Moroccan", 45, "https://img.freepik.com/free-photo/moroccan-food-tagine-dish_1203-1674.jpg?w=740&t=st=1715627060~exp=1715630660~hmac=46b208318d94979f39b372e18c30d6d9ec25a3f31c897e80bfae4178bb5d738a", false, "Moroccan Delights", 4.5 },
                    { 33, 2, "Italian, Pizza", 30, "https://img.freepik.com/free-photo/pizza-served-wooden-table_1220-671.jpg?w=740&t=st=1715627098~exp=1715630698~hmac=ff92521fa62fa04fc9e96c0a3e04e79b7d3a0d8bdb86a1072e579fb53750b404", false, "Italian Pizzeria", 4.7000000000000002 },
                    { 34, 2, "Indian", 40, "https://img.freepik.com/free-photo/assorted-indian-cuisine-with-butter-chicken-spicy-prawn-curry-tikka-masala-chicken-served-basmati-rice-naan-bread_1150-21072.jpg?w=740&t=st=1715627136~exp=1715630736~hmac=ecb5fa340b990f6183259ac7f7cccf0c0172780e9174d01bc53710394e04f1ff", false, "Indian Spice", 4.5999999999999996 },
                    { 35, 2, "Vegetarian, Salads", 20, "https://img.freepik.com/free-photo/gourmet-salads-wooden-bowls-dark-background_2829-11615.jpg?w=740&t=st=1715627175~exp=1715630775~hmac=09cb50d7852e6e2e708b97e14c0b9f7b2e81e37f967222ed8287dbe2748ec30c", false, "Gourmet Salads", 4.4000000000000004 },
                    { 36, 2, "Italian", 30, "https://img.freepik.com/free-photo/sicilian-italian-pasta-top-view_2829-12130.jpg?w=740&t=st=1715627215~exp=1715630815~hmac=84b32e8a1da4d76c5b2c7ec22e4d3c72224cbe9eaa62eb5f145cf1d84b03f671", false, "Sicilian Trattoria", 4.7000000000000002 },
                    { 37, 2, "Mediterranean", 35, "https://img.freepik.com/free-photo/mediterranean-cuisine-with-grilled-vegetables_1203-1668.jpg?w=740&t=st=1715627255~exp=1715630855~hmac=95d84d23e268e19932ef2f51abde9e9dbb54efb8db3c0861f7e2601675d13ef6", false, "Mediterranean Grille", 4.5999999999999996 },
                    { 38, 2, "Vietnamese", 30, "https://img.freepik.com/free-photo/vietnamese-noodle-soup-pho-with-herbs-beef-top-view_2829-15284.jpg?w=740&t=st=1715627296~exp=1715630896~hmac=f24fd6baf6f7b2206d23c49061d1cf46b19f98207f67d8b6bfb0cd004630f8d0", false, "Vietnamese Kitchen", 4.5999999999999996 },
                    { 39, 2, "Spanish", 40, "https://img.freepik.com/free-photo/spanish-paella-seafood-top-view_2829-11880.jpg?w=740&t=st=1715627334~exp=1715630934~hmac=bd0283f8d084fbfc0a77deaf6aa55da50fe64c84c49e7580b82435de7a0ac4b2", false, "Spanish Paella", 4.7000000000000002 },
                    { 40, 2, "Egyptian", 50, "https://img.freepik.com/free-photo/egyptian-food-traditional-dishes-served-table_2829-15184.jpg?w=740&t=st=1715627369~exp=1715630969~hmac=0c8b6e217b22077d0d2c3f6e5cf2b98d16a208f4849c20330a5f8f8c7e2820f1", false, "Egyptian Cuisine", 4.5 }
                });

            migrationBuilder.InsertData(
                table: "RestaurantMenus",
                columns: new[] { "Id", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "FoodCategories",
                columns: new[] { "Id", "RestaurantMenuId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Appetizers" },
                    { 2, 1, "Main Courses" },
                    { 3, 1, "Desserts" },
                    { 4, 2, "Appetizers" },
                    { 5, 2, "Main Courses" },
                    { 6, 2, "Desserts" },
                    { 7, 3, "Appetizers" },
                    { 8, 3, "Main Courses" },
                    { 9, 3, "Desserts" },
                    { 10, 4, "Appetizers" },
                    { 11, 4, "Main Courses" },
                    { 12, 4, "Desserts" },
                    { 13, 5, "Appetizers" },
                    { 14, 5, "Main Courses" },
                    { 15, 5, "Desserts" },
                    { 16, 6, "Appetizers" },
                    { 17, 6, "Main Courses" },
                    { 18, 6, "Desserts" },
                    { 19, 7, "Appetizers" },
                    { 20, 7, "Main Courses" },
                    { 21, 7, "Desserts" },
                    { 22, 8, "Appetizers" },
                    { 23, 8, "Main Courses" },
                    { 24, 8, "Desserts" },
                    { 25, 9, "Appetizers" },
                    { 26, 9, "Main Courses" },
                    { 27, 9, "Desserts" },
                    { 28, 10, "Appetizers" },
                    { 29, 10, "Main Courses" },
                    { 30, 10, "Desserts" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodCategories_RestaurantMenuId",
                table: "FoodCategories",
                column: "RestaurantMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodCategoryId",
                table: "FoodItems",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantMenus_RestaurantId",
                table: "RestaurantMenus",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "RestaurantMenus");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
