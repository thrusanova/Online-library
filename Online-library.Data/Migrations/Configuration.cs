namespace Online_library.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(ApplicationDbContext context)
        {
            // Seed initial data only if the database is empty
            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUserName = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = adminEmail;
                string adminRole = "Administrator";
                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassword, adminRole);
                CreateSeveralBooks(context);
            }
        }
        Genre[] genres = new Genre[]
      {
                new Genre { Title = "adventure" },
                new Genre { Title = "thriller" },
                new Genre { Title = "fantasy" },
                new Genre { Title = "love story" },
                new Genre { Title = "horror" },
                new Genre { Title = "criminal" },
                new Genre { Title = "poetry" },
                new Genre { Title = "drama" }
      };

        private void CreateSeveralBooks(ApplicationDbContext context)
        {
            context.Books.Add(new Book()
            {
                Title = "Amasonia",
                Genre= genres[1],
                PublishedYear = "2002",
                ImageUrl = "http://jamesrollins.com/wp-content/uploads/book_2002_amazonia_usa.jpg",
                BookUser = context.Users.First(),
                Author="James Rolins"
                
            });
            context.Books.Add(new Book()
            {
                Title = "Ice Hunt",
                Author = "James Rollins",
                ImageUrl = "http://jamesrollins.com/wp-content/uploads/book_2003_ice_hunt_usa.jpg",
                Genre = genres[0],
                PublishedYear = "2003",
                Comments = new HashSet<Comment>()
                {
                    new Comment () { Content="Very interesting book.",BookUser=context.Users.First() },
                    new Comment() { Content="So amazing book.", BookUser=context.Users.FirstOrDefault() }
                }
            });
            context.Books.Add(new Book()
            {
                Title = "The lost Symbol",
                Author = "Dan Brown",
                ImageUrl = "http://www.danbrown.com/wp-content/themes/danbrown/images/db/books.01.jpg",
                Genre =genres[0],
                PublishedYear = "2009"
            });
            context.Books.Add(new Book()
            {
                Title = "The Da Vinci Code",
                Author = "Dan Brown",
                Genre = genres[1],
                ImageUrl = " https://lisabernhard.files.wordpress.com/2008/05/9781400079179-723744.jpg",
                PublishedYear = "2003",
                Comments = new HashSet<Comment>()
                {
                    new Comment () { Content="Very interesting book.",BookUser=context.Users.First() },
                    new Comment() { Content="So amazing book.", BookUser=context.Users.FirstOrDefault() },
                     new Comment() { Content="Awasome.", BookUser=context.Users.FirstOrDefault() },
                     new Comment() { Content="I can't stop reading.So interesting.", BookUser=context.Users.FirstOrDefault() }
                }
            });
            context.Books.Add(new Book()
            {
                Title = "Angels and Demons",
                Author = "Dan Brown",
                ImageUrl= "http://www.danbrown.com/wp-content/themes/danbrown/images/db/books.03.jpg",
                Genre = genres[0],
                PublishedYear = "2000"
            });

        }
                private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminUserName, string adminFullName, string adminPassword, string adminRole)
        {
            // Create the "admin" user
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                FullName = adminFullName,
                Email = adminEmail
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var userCreateResult = userManager.Create(adminUser, adminPassword);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            // Create the "Administrator" role
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // Add the "admin" user to "Administrator" role
            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
    }
}

