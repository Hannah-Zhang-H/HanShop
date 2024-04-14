using HanShop.Data;
using HanShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using System.Xml.Linq;

namespace HanShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);




            // need to read all the categories and product data to the database
            // HanShopContext is an instantiation of an Entity Framework DbContext.
            // Within this code block, operations related to the database are executed.
            using (var context = new HanShopContext())
            {
                // category needs to be created
                var categoriesToAdd = new List<Category>
            {
                new Category
                {ID = 6,
                    Name = "Clothing",

                },
                new Category
                {ID = 5,
                    Name = "HomeAndLiving",

                },
                new Category
                {
                    ID = 3,
                    Name = "Pets",

                },
                 new Category
                {
                     ID = 4,
                    Name = "SportsAndOutdoor",

                },
                  new Category
                {
                    ID = 2,
                    Name = "Technology",

                },
                   new Category
                {ID = 1,
                    Name = "Toys",

                },
            };

                foreach (var category in categoriesToAdd)
                {
                    var existingCategory = context.Categories.FirstOrDefault(c => c.Name == category.Name);

                    if (existingCategory == null)
                    {
                        // if can not find the category with the same name, then save them
                        context.Categories.Add(category);
                        context.SaveChanges();
                    }
                    else
                    {
                        // otherwise do nothing
                    }
                }
            }




            // initial products
            using (var context = new HanShopContext())
            {
                // "CategoryID is the foreign key in the Product entity used to associate products with their respective categories.
                int ToysCategoryId = context.Categories.FirstOrDefault(c => c.Name == "Toys")?.ID ?? 1;
                int TechnologyCategoryId = context.Categories.FirstOrDefault(c => c.Name == "Technology")?.ID ?? 2;
                int PetsCategoryId = context.Categories.FirstOrDefault(c => c.Name == "Pets")?.ID ?? 3;
                int SportsAndOutdoorCategoryId = context.Categories.FirstOrDefault(c => c.Name == "SportsAndOutdoor")?.ID ?? 4;
                int HomeAndLivingCategoryId = context.Categories.FirstOrDefault(c => c.Name == "HomeAndLiving")?.ID ?? 5;
                int ClothingCategoryId = context.Categories.FirstOrDefault(c => c.Name == "Clothing")?.ID ?? 6;




                // productlist needed to be added to db
                var productsToAdd = new List<Product>
            {

               // Toys 2023100x
                new Product
                {
                    Name = "Chalk",
                    Price = 9,
                    SalePrice = 8.6m,
                    storage=10,
                    Description="Little ones can draw, spell out or doodle on a board using these sidewalk colourful chalks.",
                    Img= "/Content/Resources/categories/toys/chalk.png",
                    ProductCode="20231001",
                    CategoryID = ToysCategoryId




                },
                new Product
                {
                    Name = "Fairy",
                    Price = 7,
                    SalePrice = 6,
                    storage=15,
                    Description="Inspire the imagination and enjoy nature whilst creating your very own enchanted-fairy garden. " +
                    "This special product is one of the beautiful collections that can be an adorable addition or the beginning of" +
                    " your first magical garden assortment. " +
                    " Inspire the imagination and enjoy nature whilst creating your very own enchanted fairy garden.",
                    Img= "/Content/Resources/categories/toys/fairy.png",
                    ProductCode="20231002",
                    CategoryID = ToysCategoryId
                },
                new Product
                {
                    Name = "Fairyhouse",
                    Price = 14,
                    SalePrice = 4,
                    storage=15,
                    Description="This special product is one of the beautiful collections that can be an adorable addition " +
                    "or the beginning of your first magical garden assortment.Collect the whole range of fairies, " +
                    "fairy friends and village figurines.Add a sprinkle of fairy dust and make a wish to create some true magic",
                    Img= "/Content/Resources/categories/toys/fairyhouse.png",
                    ProductCode="20231003",
                    CategoryID = ToysCategoryId
                },new Product
                {
                    Name = "Playbin",
                    Price = 18,
                    SalePrice = 14,
                    storage=15,
                    Description="Provide your child with an engaging and exploratory playtime with this versatile messy play bin.",
                    Img= "/Content/Resources/categories/toys/playbin.png",
                    ProductCode="20231004",
                    CategoryID = ToysCategoryId
                },
                new Product
                {
                    Name = "WoodenEggPack",
                    Price = 6,
                    SalePrice = 3.6m,
                    storage=15,
                    Description="Encourage creative pretend play with this wooden egg pack that comes in an egg holder. Warning: Choking hazard. Small parts. Follow usage instructions carefully.",
                    Img= "/Content/Resources/categories/toys/woodenEggPack.png",
                    ProductCode="20231005",
                    CategoryID = ToysCategoryId
                },
                new Product
                {
                    Name = "MegaSet",
                    Price = 79,
                    SalePrice = 76,
                    storage=15,
                    Description="Kids can have fun and learn about different colours and shapes with this magnetic castle tiles set. Warning: Choking hazard. Small parts. Follow usage instructions carefully.",
                    Img= "/Content/Resources/categories/toys/megaSet.png",
                    ProductCode="20231006",
                    CategoryID = ToysCategoryId
                },
                    new Product
                {
                    Name = "DrawingBoard",
                    Price = 13.2m,
                    SalePrice = 11.9m,
                    storage=28,
                    Description="Little ones can create amazing glowing pictures and messages with this light-up board that will make a delightful addition to their toy collection.",
                    Img= "/Content/Resources/categories/toys/drawingBoard.jpg",
                    ProductCode="20231007",
                    CategoryID = ToysCategoryId
                },
                        new Product
                {
                    Name = "PlushToy",
                    Price = 9.7m,
                    SalePrice = 7.6m,
                    storage=15,
                    Description="The newest Care Bears in Care-A-Lot are the cuddliest kids in town; welcome to the Care Bear Cushy Plush!\r\nGet ready to snuggle up with Cheer Bear, Share Bear, Wish and Funshine Bear in the super soft and huggable form you've been dreaming of.\r\n\r\nExperience the ultimate comfort and plush cuddles with these adorable Care Bears!",
                    Img= "/Content/Resources/categories/toys/plushToy.jpg",
                    ProductCode="20231008",
                    CategoryID = ToysCategoryId
                },
                            new Product
                {
                    Name = "WoodenVehicle",
                    Price = 5.5m,
                    SalePrice = 4.9m,
                    storage=15,
                    Description="Little ones can have hours of pretend play fun with this Wooden Rescue Vehicle.",
                    Img= "/Content/Resources/categories/toys/woodenVehicle.jpg",
                    ProductCode="20231009",
                    CategoryID = ToysCategoryId
                },


                // Technology 2023200x
                new Product
                {
                    Name = "EarPhone",
                    Price = 29,
                    SalePrice = 28,
                    storage=10,
                    Description="Enjoy uninterrupted music with these wireless earphones that come with a charging case",
                    Img= "/Content/Resources/categories/technology/earphone.png",
                    ProductCode="20232001",
                    CategoryID = TechnologyCategoryId
                },new Product
                {
                    Name = "Keyboard",
                    Price = 20,
                    SalePrice = 19,
                    storage=15,
                    Description="Enjoy hassle-free connectivity with this wireless keyboard and mouse combo set that has a 10-metre working range and is ideal for home and office use.",
                    Img= "/Content/Resources/categories/technology/keyboard.png",
                    ProductCode="20232002",
                    CategoryID = TechnologyCategoryId

                },new Product
                {
                    Name = "Laminator",
                    Price = 39,
                    SalePrice = 36,
                    storage=15,
                    Description="Create professional laminated documents with our A3 laminator that features dual mode lamination.",
                    Img= "/Content/Resources/categories/technology/laminator.png",
                    ProductCode="20232003",
                    CategoryID = TechnologyCategoryId

                },new Product
                {
                    Name = "Watch",
                    Price = 59,
                    SalePrice = 56,
                    storage=15,
                    Description="Monitor your daily activity, heart rate, sleep cycle any many more using this smart watch.",
                    Img= "/Content/Resources/categories/technology/watch.png",
                    ProductCode="20232004",
                    CategoryID = TechnologyCategoryId

                },
                new Product
                {
                    Name = "Clock",
                    Price = 10,
                    SalePrice = 9.5m,
                    storage=15,
                    Description="Designed with a clear LCD display, this digital clock will let you take daily stock of important dates and room temperature.",
                    Img= "/Content/Resources/categories/technology/clock.png",
                    ProductCode="20232005",
                    CategoryID = TechnologyCategoryId

                },
                new Product
                {
                    Name = "DigitalCamera",
                    Price = 36,
                    SalePrice = 30,
                    storage=15,
                    Description="Kids can capture their imagination and memories with this mini digital camera.",
                    Img= "/Content/Resources/categories/technology/digitalCamera.png",
                    ProductCode="20232006",
                    CategoryID = TechnologyCategoryId

                },
                  new Product
                {
                    Name = "OutdoorCamera",
                    Price = 156,
                    SalePrice = 149.9m,
                    storage=15,
                    Description="Protect your home against intruders with this Wi-Fi enabled, battery powered outdoor camera that detects motion and is equipped with two-way audio. The Anko rechargeable indoor/outdoor camera is controllable with the Mirabella Genio App on your smartphone. Mirabella Genio App available from the App Store or Google Play.",
                    Img= "/Content/Resources/categories/technology/outdoorCamera.jpg",
                    ProductCode="20232007",
                    CategoryID = TechnologyCategoryId

                },
                    new Product
                {
                    Name = "RemoteControl",
                    Price = 16,
                    SalePrice = 15,
                    storage=15,
                    Description="Sick of having multiple remote controls for your home entertainment systems? Consolidate up to six into one with this universal remote control. Easy to program and simple to use, it can operate devices including TVs, DVD players, VCR players, set top boxes and home theatres. A great way to reduce clutter and confusion and get more enjoyment out of your home entertainment.",
                    Img= "/Content/Resources/categories/technology/remoteControl.jpg",
                    ProductCode="20232008",
                    CategoryID = TechnologyCategoryId

                },
                      new Product
                {
                    Name = "Cable",
                    Price = 15.9m,
                    SalePrice = 14.8m,
                    storage=15,
                    Description="High qulaity of enable your tools to get electricity.",
                    Img= "/Content/Resources/categories/technology/cable.jpg",
                    ProductCode="20232009",
                    CategoryID = TechnologyCategoryId

                },

                // Pets 2023300x
                new Product
                {
                    Name = "Petbed",
                    Price = 15,
                    SalePrice = 3,
                    storage=15,
                    Description="Bring home this pet bed lounge and give your furry friend a cushy little spot to cuddle up in.",
                    Img= "/Content/Resources/categories/pets/petbed.png",
                    ProductCode="20233001",
                    CategoryID = PetsCategoryId

                },
                 new Product
                {
                    Name = "PetComfortBed",
                    Price = 22,
                    SalePrice = 21,
                    storage=15,
                    Description="Your pet can laze around or enjoy a comfortable nap on this medium-sized, tie-dye pet comfort bed.",
                    Img= "/Content/Resources/categories/pets/petComfortBed.png",
                    ProductCode="20233002",
                    CategoryID = PetsCategoryId

                },
                  new Product
                {
                    Name = "CatFlatCross",
                    Price = 15,
                    SalePrice = 14,
                    storage=15,
                    Description="Let your feline friend scratch to their heart's content with this cat scratcher flat cross.",
                    Img= "/Content/Resources/categories/pets/catFlatCross.png",
                    ProductCode="20233003",
                    CategoryID = PetsCategoryId

                },
                   new Product
                {
                    Name = "CatScratcherCactus",
                    Price = 29,
                    SalePrice = 21,
                    storage=15,
                    Description="Keep your feline friend busy and happy with this cactus-shaped cat scratcher.",
                    Img= "/Content/Resources/categories/pets/catScratcherCactus.png",
                    ProductCode="20233004",
                    CategoryID = PetsCategoryId

                },
                    new Product
                {
                    Name = "PetToys",
                    Price = 17,
                    SalePrice = 16.5m,
                    storage=15,
                    Description="Give your furry friend a variety of toys to indulge in for hours of fun with this set of pet toys.",
                    Img= "/Content/Resources/categories/pets/petToys.png",
                    ProductCode="20233005",
                    CategoryID = PetsCategoryId

                },
                     new Product
                {
                    Name = "PetTravelBottle",
                    Price = 8,
                    SalePrice = 6,
                    storage=15,
                    Description="This pet travel bottle ensures that your beloved pet is well hydrated for that next getaway.",
                    Img= "/Content/Resources/categories/pets/petTravelBottle.png",
                    ProductCode="20233006",
                    CategoryID = PetsCategoryId

                },
                     new Product
                {
                    Name = "CatLitterTray",
                    Price = 14.9m,
                    SalePrice = 10,
                    storage=15,
                    Description="Keep your home clean and hygienic by ensuring that your cat does its business in the same place every time! Fill this tray with cat litter and teach your cat that this is where their mess goes. The lipped top helps prevent litter spilling out onto your floor as your cat moves in and out of the litter tray.",
                    Img= "/Content/Resources/categories/pets/catLitterTray.jpg",
                    ProductCode="20233007",
                    CategoryID = PetsCategoryId

                },
                     new Product
                {
                    Name = "PetDentalRubs",
                    Price = 4.9m,
                    SalePrice = 4.3m,
                    storage=15,
                    Description="Take care of your furry friend's oral hygiene and clean their teeth with these dental rubs.",
                    Img= "/Content/Resources/categories/pets/petDentalRubs.jpg",
                    ProductCode="20233008",
                    CategoryID = PetsCategoryId

                },
                     new Product
                {
                    Name = "PetEnclosure",
                    Price = 69,
                    SalePrice =68,
                    storage=15,
                    Description="Ideal for small pets, this pet enclosure will help keep your furry friend contained in the backyard.",
                    Img= "/Content/Resources/categories/pets/petEnclosure.jpg",
                    ProductCode="20233009",
                    CategoryID = PetsCategoryId

                },
                // Sports_Outdoor  2023400x
                 new Product
                {
                    Name = "PicnicTable",
                    Price = 19,
                    SalePrice = 18,
                    storage=15,
                    Description="Perfect for outdoor food preparation and serving refreshments, this small bamboo picnic table is a great addition to your camping furniture.",
                    Img= "/Content/Resources/categories/sports_outdoor/picnicTable.jpg",
                    ProductCode="20234001",
                    CategoryID = SportsAndOutdoorCategoryId

                 },
                 new Product
                    {
                    Name = "Shelter",
                    Price = 39,
                    SalePrice = 28,
                    storage=15,
                    Description="Protect yourself from the sun as you relax in comfort with this quick and easy-to-set-up instant-up shelter.",
                    Img= "/Content/Resources/categories/sports_outdoor/shelters.png",
                    ProductCode="20234002",
                    CategoryID = SportsAndOutdoorCategoryId

                },


                 new Product
                    {
                    Name = "Bike",
                    Price = 79,
                    SalePrice = 78,
                    storage=15,
                    Description="Designed with a sturdy steel frame, this balance bike will provide your little one with an effortless and smooth riding experience.",
                    Img= "/Content/Resources/categories/sports_outdoor/bike.png",
                    ProductCode="20234003",
                    CategoryID = SportsAndOutdoorCategoryId

                }
                 ,
                 new Product
                    {
                    Name = "Helmet",
                    Price = 20,
                    SalePrice = 19,
                    storage=15,
                    Description="Make sure your child stays safe as they go skating, cycling or rollerblading with this cool helmet with 10 air vents.",
                    Img= "/Content/Resources/categories/sports_outdoor/helmet.png",
                    ProductCode="20234004",
                    CategoryID = SportsAndOutdoorCategoryId

                }
                 ,
                 new Product
                    {
                    Name = "TropicReef",
                    Price = 39,
                    SalePrice = 28,
                    storage=15,
                    Description="Each sold separately. Set your child up on an adventurous spree of catching fish underwater with this Tropic Reef Scrambler Dive Set.",
                    Img= "/Content/Resources/categories/sports_outdoor/tropicReef.png",
                    ProductCode="20234005",
                    CategoryID = SportsAndOutdoorCategoryId

                }
                 ,
                 new Product
                    {
                    Name = "WaterRider",
                    Price = 35,
                    SalePrice = 28,
                    storage=15,
                    Description="Add a splash of fun to your kid's backyard playtime with this triple water rider. Warning: Choking hazard. Small parts. Follow usage instructions carefully.",
                    Img= "/Content/Resources/categories/sports_outdoor/waterRider.png",
                    ProductCode="20234006",
                    CategoryID = SportsAndOutdoorCategoryId

                },
                    new Product
                    {
                    Name = "RoastingFork",
                    Price = 8,
                    SalePrice = 6.9m,
                    storage=15,
                    Description="Roast meat, sausages and veggies conveniently with these telescopic roasting forks.",
                    Img= "/Content/Resources/categories/sports_outdoor/roastingFork.jpg",
                    ProductCode="20234007",
                    CategoryID = SportsAndOutdoorCategoryId

                },
                       new Product
                    {
                    Name = "SmokelessGrill",
                    Price = 49,
                    SalePrice = 46,
                    storage=15,
                    Description="Prepare mouth-watering kebabs, grilled fish, veggies and more while on a camping or outdoor trip using this portable grill.",
                    Img= "/Content/Resources/categories/sports_outdoor/smokelessGrill.jpg",
                    ProductCode="20234008",
                    CategoryID = SportsAndOutdoorCategoryId

                },
                          new Product
                    {
                    Name = "WaterContainer",
                    Price = 25,
                    SalePrice =23,
                    storage=15,
                    Description="Have access to clean water, right at your fingertips with this 23-litre container. You won't have to constantly trudge back and forwards to the tap anymore. Features a tap with on/off feature for your convenience.",
                    Img= "/Content/Resources/categories/sports_outdoor/waterContainer.jpg",
                    ProductCode="20234009",
                    CategoryID = SportsAndOutdoorCategoryId

                },




                // Home_Living  2023500x
                  new Product
                    {
                    Name = "Mug",
                    Price = 2,
                    SalePrice = 1,
                    storage=15,
                    Description="Relish your morning coffee or afternoon tea from this beautiful mug!",
                    Img= "/Content/Resources/categories/home_living/mug.png",
                    ProductCode="20235001",
                    CategoryID = HomeAndLivingCategoryId

                },
                    new Product
                    {
                    Name = "Plate",
                    Price = 3,
                    SalePrice = 1,
                    storage=15,
                    Description="Serve main course dishes on this white dinner plate - an elegant addition to your dinnerware.",
                    Img= "/Content/Resources/categories/home_living/plate.png",
                    ProductCode="20235002",
                    CategoryID = HomeAndLivingCategoryId

                },
                      new Product
                    {
                    Name = "StorageUnit",
                    Price = 17,
                    SalePrice = 16,
                    storage=15,
                    Description="Store your bathroom supplies neatly and conveniently in this 3 drawer bamboo storage unit.",
                    Img= "/Content/Resources/categories/home_living/storageunit.png",
                    ProductCode="20235003",
                    CategoryID = HomeAndLivingCategoryId

                },
                       new Product
                    {
                    Name = "IcedCoffeeMaker",
                    Price = 45,
                    SalePrice = 42,
                    storage=15,
                    Description="Enjoy cold iced coffee every day with this easy-to-use iced coffee maker.",
                    Img= "/Content/Resources/categories/home_living/icedCoffeeMaker.png",
                    ProductCode="20235004",
                    CategoryID = HomeAndLivingCategoryId

                },
                        new Product
                    {
                    Name = "Mat",
                    Price = 20,
                    SalePrice = 19,
                    storage=15,
                    Description="Add a classy touch to your patio or living room entrance with this printed Lola indoor mat that features pretty side fringes. Warning: Direct contact with pale or damp surfaces may result in colour transfer.",
                    Img= "/Content/Resources/categories/home_living/mat.png",
                    ProductCode="20235005",
                    CategoryID = HomeAndLivingCategoryId

                },
                         new Product
                    {
                    Name = "PumpkinMug",
                    Price = 6,
                    SalePrice = 5,
                    storage=15,
                    Description="Add a spooky touch to your serveware with this Halloween pumpkin mug.",
                    Img= "/Content/Resources/categories/home_living/pumpkinMug.png",
                    ProductCode="20235006",
                    CategoryID = HomeAndLivingCategoryId

                },
                           new Product
                    {
                    Name = "Basket",
                    Price = 38,
                    SalePrice = 17.6m,
                    storage=15,
                    Description="Keep your baby's toys and and other knick-knacks neatly stacked away in these nestled storage baskets that featurecarry handles.",
                    Img= "/Content/Resources/categories/home_living/basket.jpg",
                    ProductCode="20235007",
                    CategoryID = HomeAndLivingCategoryId

                },
                             new Product
                    {
                    Name = "Hangers",
                    Price = 2.25m,
                    SalePrice = 1,
                    storage=15,
                    Description="A great space saving solution, these hangers can be used to sort your shirts, dresses, jackets and other clothing.",
                    Img= "/Content/Resources/categories/home_living/hangers.jpg",
                    ProductCode="20235008",
                    CategoryID = HomeAndLivingCategoryId

                },
                               new Product
                    {
                    Name = "Vacuum",
                    Price = 248,
                    SalePrice = 220,
                    storage=15,
                    Description="Keep your living space clean and dirt free with this easy-to-use bagless vacuum cleaner.",
                    Img= "/Content/Resources/categories/home_living/vacuum.jpg",
                    ProductCode="20235009",
                    CategoryID = HomeAndLivingCategoryId

                },
                // Clothing  2023600x
                  new Product
                    {
                    Name = "Leggings",
                    Price = 7,
                    SalePrice = 6,
                    storage=15,
                    Description="Cotton and elastane, good to wear.",
                    Img= "/Content/Resources/categories/clothing/leggings.png",
                    ProductCode="20236001",
                    CategoryID = ClothingCategoryId

                },
                   new Product
                    {
                    Name = "Skirt",
                    Price = 22,
                    SalePrice = 20,
                    storage=15,
                    Description="Nylon and elastane, Single jersey fabric",
                    Img= "/Content/Resources/categories/clothing/skirt.png",
                    ProductCode="20236002",
                    CategoryID = ClothingCategoryId

                },
                         new Product
                    {
                    Name = "Pants",
                    Price = 22,
                    SalePrice = 19,
                    storage=15,
                    Description="These pants are fantastic, my 8 year old thinks they are great, so comfortable and on trend at the moment. The top button was a bit stiff but otherwise these pants are fantastic and recommend.",
                    Img= "/Content/Resources/categories/clothing/pants.png",
                    ProductCode="20236003",
                    CategoryID = ClothingCategoryId

                },
                               new Product
                    {
                    Name = "Romper",
                    Price = 7,
                    SalePrice = 6,
                    storage=15,
                    Description="Novelty print, Ribbed crew neck, Short sleeves, Inleg gusset, Snap crotch closure, Regular fit.",
                    Img= "/Content/Resources/categories/clothing/romper.png",
                    ProductCode="20236004",
                    CategoryID = ClothingCategoryId

                },
                                     new Product
                    {
                    Name = "Scuffs",
                    Price = 12,
                    SalePrice = 11,
                    storage=15,
                    Description="From a slipper scuff to a boot, our collection offers a wide range of cosy styles to wear at home.",
                    Img= "/Content/Resources/categories/clothing/scuffs.png",
                    ProductCode="20236005",
                    CategoryID = ClothingCategoryId

                },
                                           new Product
                    {
                    Name = "Shorts",
                    Price = 6,
                    SalePrice = 5,
                    storage=15,
                    Description="Elastic waistband with functional drawcord, Mock fly detail, Relaxed fit.",
                    Img= "/Content/Resources/categories/clothing/shorts.png",
                    ProductCode="20236006",
                    CategoryID = ClothingCategoryId

                },
                                              new Product
                    {
                    Name = "Cap",
                    Price = 9,
                    SalePrice = 6.6m,
                    storage=15,
                    Description="Button on the top, Front foam panel, Side mesh panels, Curved peak, Adjustable back strap",
                    Img= "/Content/Resources/categories/clothing/cap.jpg",
                    ProductCode="20236007",
                    CategoryID = ClothingCategoryId

                },
                                                 new Product
                    {
                    Name = "ComfortShorts",
                    Price = 12,
                    SalePrice = 11,
                    storage=15,
                    Description="The model featured is wearing a Size M, Care instructions: Machine wash according to instructions on the care label",
                    Img= "/Content/Resources/categories/clothing/comfortShorts.jpg",
                    ProductCode="20236008",
                    CategoryID = ClothingCategoryId

                },
                                                    new Product
                    {
                    Name = "StrawHat",
                    Price = 14,
                    SalePrice = 12.8m,
                    storage=15,
                    Description="Cotton and elastane, good to wear.",
                    Img= "/Content/Resources/categories/clothing/strawHat.jpg",
                    ProductCode="20236009",
                    CategoryID = ClothingCategoryId

                }
            };





                foreach (var product in productsToAdd)
                {
                    var existingProduct = context.Products.FirstOrDefault(p => p.Name == product.Name);

                    if (existingProduct == null)
                    {
                        // if all products got the different name, then add the product
                        context.Products.Add(product);
                    }
                    else
                    {
                        // otherwise just skip
                    }
                }

                // save all changes
                context.SaveChanges();
            }
        }
































    }
}

