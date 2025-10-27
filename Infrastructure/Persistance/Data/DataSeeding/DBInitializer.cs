
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;

namespace Persistance.Data.Data_Seeding
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDBContext _DBContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DBInitializer(ApplicationDBContext applicationDBContext, RoleManager<IdentityRole> roleManager , UserManager<User> userManager)
        {
            _DBContext = applicationDBContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                //if (_DBContext.Database.GetPendingMigrations().Any())
                //{
                    await _DBContext.Database.MigrateAsync();
                   
                    if (!_DBContext.ProductTypes.Any())
                    {
                        var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");
                     
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if(types is not null && types.Any())
                        {
                            await _DBContext.AddRangeAsync(types);
                            await _DBContext.SaveChangesAsync();
                        }
                    }
                    if (!_DBContext.ProductBrands.Any())
                    {
                        var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");

                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        if (brands is not null && brands.Any())
                        {
                            await _DBContext.AddRangeAsync(brands);
                            await _DBContext.SaveChangesAsync();
                        }
                    }
                   
                    if (!_DBContext.Products.Any())
                    {
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\products.json");

                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if (products is not null && products.Any())
                        {
                            await _DBContext.AddRangeAsync(products);
                            await _DBContext.SaveChangesAsync();
                        }
                    }

                    if (!_DBContext.DeliveryMethods.Any())
                    {
                        var methodsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\delivery.json");

                        var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodsData);
                        if (methods is not null && methods.Any())
                        {
                            await _DBContext.AddRangeAsync(methods);
                            await _DBContext.SaveChangesAsync();
                        }
                    }
                //}


            }
            catch (Exception ex)
            {

            }
        }
        public async Task InitializeIdentityAsync()
        {
            //seed roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuberAdmin"));
            }
            //-seed users - Assign users to roles
            if (!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01017548610"
                };
                var suberAdminUser = new User()
                {
                    DisplayName = "SuberAdmin",
                    UserName = "SuberAdmin",
                    Email = "SuberAdmin@gmail.com",
                    PhoneNumber = "01017548610"
                };
                await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                await _userManager.CreateAsync(suberAdminUser, "P@ssw0rd@");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(suberAdminUser, "SuberAdmin");

            }
        }
    }
}

