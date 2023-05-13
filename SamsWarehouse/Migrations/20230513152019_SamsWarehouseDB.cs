using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SamsWarehouse.Migrations
{
    /// <inheritdoc />
    public partial class SamsWarehouseDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    images = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Details", "Ingredients", "Price", "Size", "Title", "images" },
                values: new object[,]
                {
                    { 1, "Lindt EXCELLENCE, crafted with the finest cocoa creates rich flavours, a smooth texture and silky sheen. A treat for the senses. Discover the finest dark chocolate.\n\nExpertly crafted from the finest cocoa beans, this full-bodied chocolate is masterfully balanced, boasting deep roasted cocoa flavours, which gives away to subtle fruit undertones.\n\nLindt EXCELLENCE 70% Cocoa Dark Chocolate 100g\n\nEnliven your senses with the ultimate chocolate luxury, EXCELLENCE\n\nDiscover the expertise and craftsmanship of our Lindt Master Chocolatiers since 1845\n\nBlends the most aromatic, cocoa beans with the finest ingredients\n\nRich and refined chocolate of intense flavour and elegant texture", "Ingredients\nCocoa Mass, Sugar, Cocoa Butter, Vanilla.", 4.25, "100g", "Lindt Excellence 70% Cocoa Block", 6 },
                    { 2, null, null, 1.8999999999999999, "1 whole", "Cucumber", 1 },
                    { 3, "Made with no artificial colours or flavours, Golden Circle Australian Pieces Pineapple in Juice are the perfect ingredient for cooking, baking, mocktail and cocktail making. Add some pieces on top of ice cream or cake, add them to a fruit salad, or cook up a fantastic pineapple fried rice. Golden Circle Australian Pieces Pineapple in Juice contains 2 fruit serves per can (One serve of fruit = 150g. Aim for a variety of fruit each day.) and is conveniently chopped into pieces ready for cooking and eating. These pineapple pieces are tasty on pizza, with cooked meat, or baked into a fruity cake. On warmer days, make your own punch with lemonade, soda water and a tangy hit of Golden Circle Australian Pieces Pineapple in Juice.\n\nNo artificial colours or flavours\n\nNo preservatives\n\n2 fruit serves per can (One serve of fruit = 150g. Aim for a variety of fruit each day.)\n\nMix with strawberries, lemonade and soda for the perfect holiday fruit punch\n\nConveniently chopped, ready to be used in dishes such as pineapple salsa or pineapple chicken", "Ingredients\nPineapple (70%), Reconstituted Pear Juice, Pineapple Juice or Reconstituted Pineapple Juice.", 3.25, "440g", "Golden Circle Pineapple Pieces in natural juice", 5 },
                    { 4, "Experience the unbeatable deliciousness of Arnotts Scotch Finger, a sweet biscuit preferred for its melt-in-the-mouth crumbliness and heavenly buttery taste. Snap, dunk and enjoy  its one of Australias favourite biscuits for a reason!\n\nInject a bit of fun into your morning with an Arnotts Scotch Finger, a sweet biscuit made to snap and dunk in your favourite cuppa. Its the perfect choice for an indulgent me-time treat.\n\nThe original Arnotts Scotch Finger.\n\nDunkable.\n\nSnap and share.\n\nNo artificial colours, flavours or preservatives.\n\nAussie made, Aussie loved.", "Ingredients\nWheat Flour, Sugar, Butter (Cream (from Milk), Salt), Vegetable Oil, Condensed Milk, Salt, Eggs, Baking Powder, Emulsifier (Soy Lecithin), Antioxidant (E307b from Soy).", 2.8500000000000001, "250g", "Arnott's Scotch Finger", 8 },
                    { 5, "An iconic Aussie treat, Arnott??s Tim Tam Original is a mouth-watering combination of crunchy biscuit, luscious cream centre, wrapped in a smooth milk chocolate coating . It??s easy to see why Tim Tam is Australia??s most-loved chocolate biscuit.\n\nFind your moment of indulgence with Arnott??s Tim Tam Original. Made with irresistible real chocolate, these biscuits are the perfect way to indulge in the afternoon or unwind at the end of a long day. What more could you wish for?\n\nAustralia??s most-loved chocolate biscuits.\n\nMade with irresistible real chocolate.\n\nFor your moment of afternoon indulgence.\n\nThere is no substitute.\n\nMade in Australia.", "Ingredients\nBiscuits: Milk Chocolate (38%) (Sugar, Milk Solids,, Cocoa Butter, Cocoa Mass, Vegetable Oil, Emulsif,iers (Soy Lecithin, E476), Flavour), Wheat Flour,,Sugar, Vegetable Oil (Emulsifier (Soy Lecithin), A,ntioxidant (E307b From Soy)), Golden Syrup, Food C,o", 3.6499999999999999, "200g", "Original Tim Tams Choclate", 5 },
                    { 6, "Experience the smooth taste of Moccona Classic Medium Roast. Its full-bodied flavour and rich aroma make it the perfect coffee to enjoy everyday.", "Ingredients\n100% Coffee beans.", 6.0, "100g", "Moccana Classic Instant Medium Roast", 4 },
                    { 7, "Coca-Cola Classic Soft Drink Bottle 2L\n\nNothing beats the taste of Coca-Cola Classic. It's the perfect companion whether youre on the go, relaxing at home, enjoying with friends or as a drink with your meal. Refresh yourself with the authentic Coke taste. Designed to go with everything, the taste of Classic Coca-Cola has remained unchanged for more than 130 years. Coca-Cola soft drink is available in cans, mini cans, single serve and sharing size bottles as well as multipacks.\n\nClassic Coca-Cola Taste\n\nServe Drink Cold for Maximum Refreshment\n\nA Quality Product of The Coca-Cola Company\n\n2L Soft Drink\n\nPerfect for drinking on the go, having with a meal or relaxing with at home.", "Ingredients\nCarbonated Water, Sugar, Colour (150d), Food Acid (338), Flavour, Caffeine.", 2.8500000000000001, "2 litre", "Coca Cola", 3 },
                    { 8, null, "Ingredients\nMilk, Salt, Cultures, Enzyme (Non-Animal Rennet).", 4.0, "250g", "Bega Farmers Tasty", 5 },
                    { 9, "The Original Philadelphia Cream Cheese is made fresh and creamy, perfect as a spread or as an ingredient in some of your favourite desserts.", "Ingredients\nMilk, Cream (from Milk), Milk Solids, Salt, Vegetable Gum (Locust Bean), Starter Culture.", 4.2999999999999998, "250g", "Philadelphia Original Cream Cheese", 3 },
                    { 10, "PRIMO SHORT-CUT Rindless Bacon is the leanest of them all.Lean round pieces of bacon from top grade loin for those who enjoy a meatier, leaner flavour preference.\n\nWake up happy, Australias Favourite Bacon is Sizzling with Flavour! \nPrimo Bacon is gluten free with no artificial colours or flavours.", "Ingredients\nPork (89%), Water, Curing Premix [Salt, Sugar, Mineral Salts (451, 4150), Antioxidant (316), Preservative (250)], Naturally Wood Smoked.", 5.0, "175g", "Primo Short cut rindless Bacon", 1 },
                    { 11, "Helgas Wholemeal Grain Loaf Sliced Bread contains fibre and protein with no artificial colours, flavours or preservatives. Enjoy toasted with your favourite spread for a quick breakfast.\n\nYou can never go wrong with Helga's Wholemeal Grain Loaf Sliced Bread. A fantastic breakfast option for everyone to enjoy, simply spread with butter and add your favourite toppings or toast with peanut butter for a quick delicious bite. The options are endless.\n\n Source of protein\n\n Good source of fibre\n\n No artificial colours\n\n No artificial flavours\n\n No artificial preservatives", "Ingredients\nWholemeal wheat flour (53%), water, whole grains (10%) (kibbled wheat, malted wheat flakes, kibbled rye, kibbled triticale), yeast, linseeds, vinegar, wheat bran, wheat flour, canola oil, iodised salt, wheat gluten, cultured wheat flour, soy flour, vegetable emulsifiers (481, 471, 472e), malted barley flour, vitamins (thiamin, folic acid).", 3.7000000000000002, "1", "Helga's Wholemeal", 4 },
                    { 12, "San Remo Linguine contains no artificial colourings, flavourings or preservatives. Cooks in 10 minutes.", "Ingredients\nDurum Wheat Semolina.", 1.95, "500g", "San Remo Linguine Pasta No 1", 2 },
                    { 13, "- 15 Fish Fingers Per Pack\n- Made from Hoki Fish\n- MSC Certified\n- Good Source of Omega-3\n- No Artificial Colours, Flavours or Preservatives\nSimply delicious! Since their Australian debut in 1956, Birds Eye iconic Fish Fingers have become a family favourite. Made from Hoki, with no artificial colours, flavours or preservatives. They're coated in a delicioius, crunchy crumb, making them a hit with the crew everytime!", "Ingredients\n*hoki* (51%), *wheat* flour, water, canola oil, maize flour, *wheat* starch, maize starch, salt, sugar, *wheat* gluten, yeast, acidity regulator (451, 450). *Contains fish and wheat.*", 4.5, "375g", "Birds Eye Fish Fingers", 10 },
                    { 14, "Red Washed Potatoes\n\nIn Season: All Year\n\nRed Washed Potatoes have pink, smooth skin with creamy white flesh, they are high in moisture and low in starch which makes them hold their shape well when cooked, making them ideal for roasting.", null, 4.0, "1kg", "Red potato washed", 1 },
                    { 15, "The ultimate tangy apple! Firm, crunchy, and tart making the Granny Smith a great snack, and ideal for baking and cooking.\n\nHow to Pick:\nApples should be bright and crunchy, with a tight skin and juicy flesh. Our apples have a natural shine and no added wax.\n\n\nStoring your apples in the fridge will keep them crisp and fresh for longer.\nConsume apples within 2 weeks for the best eating experience.\n\n\nOur hand picked apples are 100% Australian grown. Our expert growers pick them from trees in the colder climate regions of Australia to ensure they have the best flavour, colour and firmness.", null, 5.5, "1kg", "Granny Smith Apples", 1 },
                    { 16, "Round in shape, with a bright red shiny skin and red pulp and whitish seeds. The tomato is actually a fruit but is considered a vegetable because of its uses. ", null, 5.9000000000000004, "500g", "Fresh tomatoes", 1 },
                    { 17, "Woolworth's Select Carrot are selected fresh from Australian farms, the ever versatile carrots is perfect for cooking or eating raw as a fresh snack. Edible portion includes flesh only. ", null, 2.0, "1kg", "Carrots", 1 },
                    { 18, "Large oval fruit with a thick green skin and a sweet watery pink to red flesh with usually many seeds.\nOften the deeper colored the flesh, the sweeter the taste. Watermelon's flesh contains about 6% sugar and it is comprised primarily of water. \n\n\nAll year.\n\n\nWatermelon is best utilized in fresh, uncooked applications. Its flesh can be cubed, balled, sliced into wedges or pureed. The large shell can be carved into a decorative basket and used as a natural vessel for serving beverages or salads. Pureed it can be used to flavor drinks or to make granitas, sorbets and chilled soups. Its sweet flavor pairs well with cucumber, arugula, basil, mint, citrus juice, peanuts, coconut, pecans and robust cheeses such as feta, romano and parmesan. Though not commonly consumed as a result of its bitter flavor the rind of the Watermelon is edible and can be grilled, stewed, stir-fired or pickled. The seeds as well are edible and can be roasted or dried and seasoned. Unlike most other melons, refrigerate Watermelon for best flavor. ", null, 6.5999999999999996, "Whole", "Watermelon", 1 },
                    { 19, "Red onions have burgundy red skins and red tinged flesh. Spanish type red onions are large and round, while Californian red onions tend to be flatter and milder. They are mild, sweet and juicy and are delicious eaten raw in salads, used as a garnish or added to sandwiches.", null, 3.5, "1kg", "Red onion", 1 },
                    { 20, "Mornings taste better with Nutella!", "Ingredients\nINGREDIENTS: SUGAR, VEGETABLE OIL (PALM*), HAZELNUTS (13%), SKIM MILK POWDER (8.7%), FAT-REDUCED COCOA POWDER (7.4%), EMULSIFIER (LECITHINS) (SOY), FLAVOURING (VANILLIN). TOTAL MILK SOLIDS: 8.7%. TOTAL COCOA SOLIDS: 7.4%. *Sustainable and segregated certified palm oil.", 4.0, "400g", "Nutella jar", 7 },
                    { 21, "Iceberg Lettuce is round in shape, with packed layers of\ncrisp green leaves. The heads are firm and tightly packed with a\ncentral core or heart. The leaves are crunchy and have a mild flavour.\nThe outer leaves are a darker green; the central leaves are pale green.\nThe leaves are cupped, hold their shape and can be used to hold fillings.\n\nAvailability\nAll year.\n\nStore\nRefrigerate in reusable plastic bags, or store in the crisper.\nAvoid squashing the lettuce. Alternatively, if the lettuce roots are still\nattached, stand the lettuce in a jar with the roots in water,\nand do not refrigerate.\n\nWays to eat\nLettuce is used raw in salads, sandwiches and rolls.\nLettuce leaves may also be used as wraps.\nOlder leaves can be used in soups.", null, 2.5, "1", "Iceburg Lettuce", 1 },
                    { 22, "Red Tip Bananas are great for breakfast and snacking occasions and are the perfect lunchbox addition. \n\nHow to Pick:\nBananas that are yellow and lightly flecked with brown spots will be at their peak flavor. Bananas should be plump, firm, and brightly colored.\n\n\nStore bananas at room temperature, where they will continue to ripen. If you refrigerate your bananas, they will stop ripening, but this will also make the skins go black. The fruit is still okay to eat, but will not be the best quality.\n\n\nAustralian Red Tip Bananas are grown in Far North Queensland, near Innisfail.\n\n\nBananas contain potassium for heathy muscle function, and are high in energy-supprting vitamin B6* \n\n*Based on one large cavendish banana, or 2 medium lady finger bananas (150g), as part of a healthy balanced diet", null, 4.9000000000000004, "1kg", "Red tipped bananas", 1 },
                    { 23, "This is a variable weight product and is priced by $/kg. Once we've picked your item in store, we'll refund you for the difference between the weight paid and the weight received. Please advise in your personal shopper notes your preferred weight when reviewing your order at checkout. We will try our best to provide you with the requested weight.", null, 7.5, "1kg", "Free range chicken", 2 },
                    { 24, "Cobram Estate Classic Flavour Intensity is crafted from olives grown in Australias leading groves on the banks of the Murray River. Our oil master skillfully combines the leading varieties from each estate to create a distinctive, first press Extra Virgin Olive Oil packed with appealing fresh fruity aromas and penetrating flavours. Why choose Cobram Estate Extra Virgin Olive Oil? 1.) Australia's freshest and most awarded Extra Virgin Olive Oil. 2.) We treat every olive just right, crushing and juicing within hours of picking, to capture the freshest antioxidants and other essential nutrients. 3.) Natural source of vitamin E, essential to protect body cells from free radical damage 4.) The result is a premium, healthy, value-packed extra virgin olive oil.", "Ingredients\n100% Australian Extra Virgin Olive Oil", 8.0, "375ml", "Cobram EVOO", 1 },
                    { 25, "a2 Milk Light is sourced from Australian dairy farmers and made with 100% Australian pure & natural milk from cows specially selected to produce milk with only the A2 beta-casein protein. a2 Milk Light contains only 1.3g of fat per 100ml and is a natural lower in fat alternative to full cream, with a creamy and smooth consistency. Its completely natural & the way milk is meant to be. Feel the a2 Milk difference - Naturally A1 protein free; No additives, no permeate; Made in Australia from 100% Australian ingredients.", "Ingredients\nReduced Fat Milk", 2.8999999999999999, "1 litre", "A2 light milk", 1 },
                    { 26, "There for as soon as the toast pops up, Lurpak Spreadable Slightly Salted has become a modern classic and kitchen staple with its distinct Lurpak taste. It is perfect for mixing with fresh ingredients such as chives or parsley to create flavoured butters.", "Ingredients\nButter (64%), Canola Oil (26%), Water, Lactic Culture, Salt (0.9%), Vitamin D", 5.0, "250g", "Lurpak Salted Blend", 1 },
                    { 27, "Sanitarium Weet-Bix Organic Breakfast Biscuits is 97% wholegrain, made from certified organic wheat, low in sugar and provides a source of fibre.\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n97% wholegrain\n\nLow in Sugar\n\nCertified Organic Wheat\n\nSource of Fibre", "Ingredients\nOrganic Wholegrain Wheat (97%), Organic Sugar, Salt, Barley Malt Extract.", 5.3300000000000001, "750g", "Weet Bix Saniatarium Organic", 7 },
                    { 28, "Colgate Total Original Toothpaste is clinically proven to protect teeth and gums from plaque bacteria for up to 12 hours, even after eating or drinking.", "Ingredients\nWater, Hydrated Silica, Glycerin, Sorbitol, PVM/MA Copolymer, Sodium Lauryl Sulphate, Flavour, Cellulose Gum, Carrageenan, Sodium Hydroxide, Sodium Fluoride, Sodium Saccharin, Triclosan, CI 77891.\n\nNO COLGATE TOOTHPASTE CONTAINS SUGAR.\nView more", 3.5, "110g", "Colgate Total Toothpaste Original", 4 },
                    { 29, "Sacla pasta sauce with Basil offers the unique flavour of traditional home-made Italian recipes. Delicious and rich tasty sauce made with the best quality ingredients like sweet whole cherry tomatoes. Buonissimo!", "Ingredients\nDiced Tomatoes, Tomato Juice, Tomato Paste, Cherry Tomatoes 12.5%, Sunflower Seed Oil, Onions, Carrots, Garlic, Celery, Basil 1.3%, Extra-Virgin Olive Oil,Fructose, Sea Salt, Modified Corn Starch (1412), Acidity Regulators: (270, 330), Firming Agent: (509).", 4.5, "420g", "Sacla Pasta Tomato Basil Sauce", 1 },
                    { 30, "Smooth, sweet and perfectly balanced, generations of Aussies have grown up with Capilano Pure Honey.\nMade by Aussie bees and hand harvested by their beekeepers, our signature blend of eucalypt and ground flora honey is the taste you know and love.", "Ingredients\n100% Pure Australian Honey", 7.3499999999999996, "500g", "Capilano Squeezable Honey", 2 },
                    { 31, "Strawberries, plump little hearts grown full and tender, red-ripe ready-to-love, and topped with a layer of velvety Chobani Greek Yogurt, for a nutritious snack bursting with flavour.\n\nChobani Greek Yogurt is deliciously simple. Made using just milk and cultures, which we triple strain using an authentic process so its naturally thick and creamy, then layered onto strawberry.\n\n\n\nStrained to be naturally thick\n\nHigh in natural protein\n\nNo preservatives, artificial colours or flavours\n\nSource of calcium\n\nContains Live & Active Cultures\n\nMade in Australia", "Ingredients\nSkim Milk, Strawberry (8%), Sugar, Water, Vegetable Gums (Pectin, Guar Gum, Locust Bean Gum), Natural Flavours, Fruit and Vegetable Concentrates, Acidity Regulators (Citric Acid, Sodium Citrate), Mineral Salt (Calcium Citrate), Live Cultures (Milk).", 1.5, "1", "Chobani Strawberry Yoghurt", 6 },
                    { 32, "MILO is a nutrient rich, low GI choc malt powder drink providing kids the nourishing energy they need to take on the day. Simply add 3-heaped teaspoons of MILO to a glass of hot or cold skim milk for a delicious chocolate malt flavoured milk drink. MILO is great with breakfast, after school, before or after sport as a snack or as a hot chocolately malt drink after dinner. Made from four main ingredients: milk powder, malt barley, sugar and cocoa, MILO also contains 8 essential vitamins and minerals. Contains 10 serves of choc-malty goodness. Do Good Together. Did you know, our MILO tins are made from 100% reyclable materials. And approximately 80% of the energy powering our MILO factory comes from BIOENERGY, a renewable energy source. To find out more visit the MILO website. MILO and Banana Shake Serves: 2 Prep Time: 5 min Ingredients: 1 cup reduced fat milk 0.5 cup natural yoghurt 1/3 cup (40g) MILO 1 medium banana, chopped Method: 1. Combine all ingredients in a blender; blend until smooth, thick and creamy. 2. Pour into tall glasses and serve immediately. *When consumed as part of a healthy diet with a wide variety of foods.", "Ingredients\nExtract of Malt Barley Or Malt Barley and Rice (Total Extract 38%), Milk Solids, Sugar, Cocoa, Choc Malt Blend, Minerals (Calcium, Iron), Maltodextrin (Corn), Vitamins (C, B3, B6, B2, D, B12), Emulsifier ( Soy Lecithin) \n Contains Gluten, Milk and Soy\nView more", 4.0, "200g", "Milo Chocolate Malt", 6 },
                    { 33, "Mini Babybel Cheese Original is made with 155mL of milk, it contains a source of calcium.", "Ingredients\nMilk (98%), Salt, Bacterial Culture, Microbial Rennet.", 4.2000000000000002, "100g", "Babybel Mini", 1 },
                    { 34, "South Cape Greek Style Fetta contains firm, crisp & slightly acidic flavour.", "Ingredients\nPasteurised Cows Milk, Salt, Mineral Salt (509), Starter Cultures, Non-animal Rennet.", 5.0, "200g", "South Cape Greek Feta", 1 },
                    { 35, "With the abundance of clean air, rich earth and pristine water on our farms its no surprise that our free range hens produce such tasty, nutritous eggs. The Matuszny family are pasture based farmers who only produce and package the best free range eggs. Our eggs contain high quality protein and are a great source of Omega 3, folate and selenium with 11 vitamins & minerals.", null, 3.6000000000000001, "6 eggs", "Manning Valley 6-pk", 4 },
                    { 36, "Chunk style pieces of succulent tuna in springwater with a delicate flavour.\n\n- Good Source of Omega-3\n- 98% Fat Free\n- All Natural Ingredients\n- MSC certified\n\nCombine with reduced fat mayonnaise, corn kernels and sliced celery and serve in a pita pocket.", "Ingredients\nPurse seine caught skipjack *tuna* (Katsuwonus pelamis) (65%), springwater, salt. *Contains fish.*", 1.5, "95g", "John West Tuna can", 5 },
                    { 37, "Arnott??s Shapes Originals are an Aussie favourite savoury biscuit snack. Arnott??s Shapes Original Cheddar offer a mild cheddar flavour on a crisp and crunchy cracker base. Oven baked, not fried, and foil-wrapped for freshness.\n\nArnott??s Shapes Originals are big on crunch and even bigger on taste ?? and perfect for any occasion! These cheddar cheese crackers are delicious oven-baked crackers with loads of flavour you can see, and no artificial colours or preservatives.\n\nWhether you??re sharing with friends at the park or looking for a fun afternoon snack with flavours that your whole family will love, Arnott??s Shapes Original Cheddar biscuits are the perfect flavoured cracker. Foil-wrapped for freshness, they??re a go-to savoury cravings-buster for any occasion. Bags of flavour in every box, no matter which flavour you savour!\n\nA mild cheddar flavour on a crisp, crunchy cracker base.\n\nFlavour you can see.\n\nFoil-wrapped for freshness.\n\nOven baked, not fried.\n\nMade in Australia.", "Ingredients\nWheat Flour, Cheese (15%) (Milk Solids, Salt), Vegetable Oil, Salt, Yeast, Yeast Extract (Contains Barley), Malt Extract (From Barley), Wheat Starch, Sugar, Natural Flavour, Baking Powder, Worcestershire Sauce. Milk Solids. Spice, Vegetable Extract, Emulsifier (Soy Lecithin), Antioxidants (E307b From Soy, E304).", 2.0, "175g", "Cheddar Shapes", 10 },
                    { 38, "Primo English Style Leg Ham is lightly seasoned and unsmoked for an authentic ham flavour. Serve on crusty bread with cheese and mustard.", "Ingredients\nPork, Water, Acidity Regulators (326, 325, 262), Cure [Salt, Mineral Salts (451, 450), Sugar, Antioxidant (316), Preservative (250), Natural Flavours].", 3.0, "100g", "Primo English Ham", 2 },
                    { 39, null, null, 6.0, "2 litre", "Berri Orange Juice", 1 },
                    { 40, "Vegemite B Vitamins for vitality.\nStart happy with Vegemite.", "Ingredients\nYeast Extract (from Yeast Grown on Barley and Wheat), Salt, Mineral Salt (508), Malt Extract (from Barley), Colour (150c), Flavours, Niacin, Thiamine, Riboflavin, Folate.", 6.0, "380g", "Vegemite", 3 },
                    { 41, "Deliciously smooth and creamy Cadbury Dairy Milk milk chocolate\nCadbury Dairy Milk milk chocolate block is Australia's favourite chocolate. It has 'the equivalent of a glass and a half of full cream milk in every 200g of Cadbury Dairy Milk milk chocolate'. Cadbury is perfect for treating yourself and sharing among family and friends. Also available in a 50g bar, 180g multipack and 350g block.", "Ingredients\nFull Cream Milk, Sugar, Cocoa Butter, Cocoa Mass, Milk Solids, Emulsifiers (Soy Lecithin, 476), Flavours.", 5.0, "200g", "Cadbury Dairy Milk", 1 },
                    { 42, "Each chip is cooked and seasoned to perfection to create the classic salted taste that has become so richly embedded into the memories of families all over Australia. The Most Iconic Crinkle Cut Chips Around.  Smith's Original Chips are celebrating 90 years of being a household staple and has continued to capture the hearts and tongues of Aussies for generations.\n\nAustralian-made and Aussie Proud\nOur Smith's Chips are made from 100% Australian potatoes, grown by Aussie farmers - including a family-run farm that's been supplying us for over 50 years.\n\nIn 1931, Smith's took the humble potato and created the original crinkle-cut potato chip recipe we all know and love today. Affectionately known as 'the People's Chip', our national heritage is something we are very proud to celebrate.\n\nPotato Snacks to Accompany Your Outdoor Activities\nThere is no better snack when watching the footy grand final or sprawling across the green for a picnic. They're perfect for indulging after a hard day's work and make the ideal school lunch snacks for your kids.\n\nAn Unbelievably Good but Gluten Free Snack\nSmith's Original Crinkle Cut Chips are free from any hidden nasties! No artificial flavours, colours or even gluten means that anyone can enjoy the classic crunch & unbeatable original flavour that only a Smith's potato chip can bring!\n\nWhilst some things have changed since 1931, one thing remains the same, Smith's are still the original and the best!\n\nYou know you love 'em.\n\nThey're the nostalgic Aussie flavour that's made from local ingredients right on your doorstep.\n\nNo artificial flavours or colours mean that theyre perfect as school lunch snacks for your kids.\n\nAs a gluten free snack, anyone can enjoy the classic crinkle cut crunch and Smith's Original flavour.\n\nThere is no better snack when watching the footy grand final or sprawling across the green for a picnic.\n\nThey're the ideal snack to come home to after a long day's work", "Ingredients\nPotatoes (64%), Canola Oil, Sunflower Oil, Salt, Antioxidants (Tocopherols, Ascorbic Acid, Rosemary Extract, Citric Acid).", 3.29, "170g", "Smith's Original Share Pack Crisps", 4 },
                    { 43, "One of the most timeless soup flavours, Heinz Classic Creamy Tomato Soup is a great light meal choice. Serve with a swirl of cream and some fresh basil or parsley. This delicious soup contains no preservatives or artificial flavours, and is 98% fat free too. Designed to be a quick lunch or dinner option, Heinz Classic Creamy Tomato Soup is perfect with a side of crusty bread. If you've got some more time in the evenings, this soup can also be used as the base of a pasta sauce.\n\nCreamy tomato soup\n\nNo preservatives\n\n98% fat free\n\nNo artificial flavours\n\n3 serves of veg per 265g serve (One serve of vegetables = 75g. Aim for a variety of vegetables each day.)", "Ingredients\nTomatoes (90%), Cream (3.4%) (Milk), Sugar, Maize Thickener (1412), Milk, Salt, Cornflour, Butter (Milk), Acidity Regulator (Sodium Bicarbonate), Spice, Natural Flavour.\n\nContains: Milk.\nView more", 2.5, "535g", "Heinz Tomato Soup", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
