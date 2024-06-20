using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDelivery.API.Migrations
{
    /// <inheritdoc />
    public partial class FoodItemsSeedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "Id", "Description", "FoodCategoryId", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Crispy spring rolls stuffed with vegetables.", 1, "https://img.freepik.com/free-photo/margherita-pizza-with-basil-top-view_2829-10397.jpg?w=740", "Spring Rolls", 5.9900000000000002 },
                    { 2, "Freshly grilled salmon with a side of vegetables.", 2, "https://img.freepik.com/free-photo/spaghetti-carbonara-white-plate_2829-15834.jpg?w=740", "Grilled Salmon", 15.99 },
                    { 3, "Creamy cheesecake with a graham cracker crust.", 3, "https://img.freepik.com/free-photo/tiramisu-italian-dessert_2829-12651.jpg?w=740", "Cheesecake", 6.9900000000000002 },
                    { 4, "Toasted bread topped with tomatoes and basil.", 1, "https://img.freepik.com/free-photo/chicken-tikka-masala-with-rice_2829-19349.jpg?w=740", "Bruschetta", 4.9900000000000002 },
                    { 5, "Classic spaghetti with pancetta and a creamy sauce.", 2, "https://img.freepik.com/free-photo/naan-bread-flat-lay_2829-11117.jpg?w=740", "Spaghetti Carbonara", 12.99 },
                    { 6, "Traditional Italian dessert with coffee and mascarpone.", 3, "https://img.freepik.com/free-photo/mango-lassi-indian-yogurt-drink_2829-11519.jpg?w=740", "Tiramisu", 5.9900000000000002 },
                    { 7, "Bread with garlic and butter, toasted to perfection.", 1, "https://img.freepik.com/free-photo/assorted-sushi-set-black-background_2829-15836.jpg?w=740", "Garlic Bread", 3.9900000000000002 },
                    { 8, "Pasta with creamy Alfredo sauce and grilled chicken.", 2, "https://img.freepik.com/free-photo/miso-soup-wooden-bowl_2829-11711.jpg?w=740", "Chicken Alfredo", 13.99 },
                    { 9, "Rich chocolate cake with a molten chocolate center.", 3, "https://img.freepik.com/free-photo/matcha-green-tea-ice-cream_2829-12799.jpg?w=740", "Chocolate Lava Cake", 7.9900000000000002 },
                    { 10, "Fried mozzarella cheese sticks with marinara sauce.", 1, "https://img.freepik.com/free-photo/beef-tacos-with-lemon-slices_2829-13131.jpg?w=740", "Mozzarella Sticks", 6.9900000000000002 },
                    { 11, "Tacos with seasoned beef, lettuce, and cheese.", 2, "https://img.freepik.com/free-photo/guacamole-dip-with-tortilla-chips_2829-13130.jpg?w=740", "Beef Tacos", 10.99 },
                    { 12, "Vanilla ice cream topped with chocolate sauce and nuts.", 3, "https://img.freepik.com/free-photo/churros-with-chocolate-sauce_2829-13132.jpg?w=740", "Ice Cream Sundae", 4.9900000000000002 },
                    { 13, "Crispy onion rings with a side of ranch dressing.", 1, "https://img.freepik.com/free-photo/caesar-salad-white-bowl_2829-13133.jpg?w=740", "Onion Rings", 5.4900000000000002 },
                    { 14, "Slow-cooked ribs with BBQ sauce and coleslaw.", 2, "https://img.freepik.com/free-photo/grilled-chicken-sandwich_2829-13134.jpg?w=740", "BBQ Ribs", 18.989999999999998 },
                    { 15, "Classic apple pie with a flaky crust.", 3, "https://img.freepik.com/free-photo/chocolate-cake-slice-white-plate_2829-13135.jpg?w=740", "Apple Pie", 5.9900000000000002 },
                    { 16, "Mushrooms stuffed with cheese and herbs.", 1, "https://img.freepik.com/free-photo/pad-thai-noodles-with-shrimp_2829-13136.jpg?w=740", "Stuffed Mushrooms", 6.4900000000000002 },
                    { 17, "Grilled lamb chops with mint sauce.", 2, "https://img.freepik.com/free-photo/spring-rolls-plate_2829-13137.jpg?w=740", "Lamb Chops", 21.989999999999998 },
                    { 18, "Italian dessert made with sweetened cream.", 3, "https://img.freepik.com/free-photo/mango-sticky-rice_2829-13138.jpg?w=740", "Panna Cotta", 6.9900000000000002 },
                    { 19, "Spicy chicken wings served with blue cheese dip.", 1, "https://img.freepik.com/free-photo/cheeseburger-fries-plate_2829-13139.jpg?w=740", "Buffalo Wings", 7.9900000000000002 },
                    { 20, "Juicy cheeseburger with lettuce, tomato, and pickles.", 2, "https://img.freepik.com/free-photo/french-fries-ketchup_2829-13140.jpg?w=740", "Cheeseburger", 9.9900000000000002 },
                    { 21, "Warm brownie topped with ice cream and chocolate sauce.", 3, "https://img.freepik.com/free-photo/milkshake-glass_2829-13141.jpg?w=740", "Brownie Sundae", 6.4900000000000002 },
                    { 22, "Tortilla chips topped with cheese, jalapenos, and salsa.", 1, "https://img.freepik.com/free-photo/greek-salad-bowl_2829-13142.jpg?w=740", "Nachos", 7.4900000000000002 },
                    { 23, "Sizzling fajitas with chicken, peppers, and onions.", 2, "https://img.freepik.com/free-photo/gyro-pita-sandwich_2829-13143.jpg?w=740", "Fajitas", 14.99 },
                    { 24, "Fried dough pastries with cinnamon sugar.", 3, "https://img.freepik.com/free-photo/baklava-dessert_2829-13144.jpg?w=740", "Churros", 4.9900000000000002 },
                    { 25, "Lightly breaded and fried calamari served with marinara sauce.", 1, "https://img.freepik.com/free-photo/bbq-ribs-plate_2829-13145.jpg?w=740", "Fried Calamari", 8.9900000000000002 },
                    { 26, "Spanish rice dish with seafood and saffron.", 2, "https://img.freepik.com/free-photo/coleslaw-salad_2829-13146.jpg?w=740", "Seafood Paella", 19.989999999999998 },
                    { 27, "Rich custard dessert with a caramelized sugar top.", 3, "https://img.freepik.com/free-photo/cornbread-slices_2829-13147.jpg?w=740", "Creme Brulee", 7.4900000000000002 },
                    { 28, "Potato skins topped with cheese and bacon.", 1, "https://img.freepik.com/free-photo/vietnamese-pho-noodle-soup_2829-13148.jpg?w=740", "Potato Skins", 6.4900000000000002 },
                    { 29, "Grilled steak served with mashed potatoes.", 2, "https://img.freepik.com/free-photo/vietnamese-spring-rolls_2829-13149.jpg?w=740", "Steak and Potatoes", 22.989999999999998 },
                    { 30, "Banana split with ice cream, chocolate sauce, and whipped cream.", 3, "https://img.freepik.com/free-photo/banh-mi-vietnamese-sandwich_2829-13150.jpg?w=740", "Banana Split", 5.9900000000000002 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
