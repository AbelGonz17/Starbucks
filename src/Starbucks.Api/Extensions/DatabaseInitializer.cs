using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Starbucks.Api.Resorces;
using Starbucks.Domain;
using Starbucks.Persistence;

namespace Starbucks.Api.Extensions;

public static class DatabaseInitializer
{
    public static async Task ApplyMigration(
        this IApplicationBuilder builder,
        IWebHostEnvironment? env)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = service.GetRequiredService<StarcbucksDbContext>();
                await context.Database.MigrateAsync();
                await SeedData(context, env);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error en la migracion");
            }
        }
    }

    private static async Task SeedData(
        StarcbucksDbContext context,
        IWebHostEnvironment? environment)
    {
        if (context.Coffes.Any())
            return;

        if (environment is null)
            throw new Exception("El enviroment no se cargo");

        var rootPath = environment.ContentRootPath;
        var fullPathCoffe = Path.Combine(rootPath, "Resorces/Coffe.json");
        var coffeDataText = await File.ReadAllTextAsync(fullPathCoffe);
        var data = JsonConvert.DeserializeObject<List<CoffeJson>>(coffeDataText)
                        ?? Enumerable.Empty<CoffeJson>();

        var ingredientMaster = new List<Ingredient>();
        var coffeMaster = new List<Coffe>();
        var random = new Random();

        foreach (var coffeJson in data)
        {
            var ingredientsLocal = new List<Ingredient>();

            foreach (var ingr in coffeJson.Ingredients)
            {
                var ingredient = ingredientMaster
                                    .Where(
                                        s => 
                                        string.Equals(
                                            s.Name,
                                            ingr,
                                            StringComparison.CurrentCultureIgnoreCase
                                            ))
                                    .FirstOrDefault();
                if (ingredient is null)
                {
                    ingredient = new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Name = ingr
                    };
                    ingredientMaster.Add(ingredient);
                }
                ingredientsLocal.Add(ingredient);
            }

            var coffe = new Coffe
            {
                Name = coffeJson.Title!,
                Description = coffeJson.Description,
                Imagen = coffeJson.Image,
                CategoryId = coffeJson.Category,
                Price = RandomPrice(random, 2, 15),
                Ingredients = ingredientsLocal
            };

            coffeMaster.Add(coffe);
        }

        await context.Ingredients.AddRangeAsync(ingredientMaster);
        await context.Coffes.AddRangeAsync(coffeMaster);

        await context.SaveChangesAsync();
    }

    private static decimal RandomPrice(Random random, double min, double max)
    {
        return Convert.
              ToDecimal(Math.Round(random.NextDouble() * Math.Abs(max - min) + min, 2));
    }
}