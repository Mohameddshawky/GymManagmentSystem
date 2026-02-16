using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData( GymDbcontext context, string? webRootPath)
        {
            if (string.IsNullOrEmpty(webRootPath)) return false;
            try
            {
                var hasPlans = context.plans.Any();
                var hasCategories = context.categories.Any();

                if (hasPlans && hasCategories) return false;

                if (!hasCategories)
                {
                    var categories = LoadDataFromJson<Category>("categories.json", webRootPath);
                    if (categories.Any())
                    {
                        context.categories.AddRange(categories);
                    }

                }
                if (!hasPlans)
                {
                    var plans = LoadDataFromJson<Plan>("plans.json", webRootPath);
                    if (plans.Any())
                    {
                        context.plans.AddRange(plans);
                    }
                }
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed {ex.Message}");
                return false;
            }
        }


        private static List<T> LoadDataFromJson<T>(string fileName, string webRootPath) 
        {
            var filePath = Path.Combine(webRootPath, "Files", fileName);
            if (!File.Exists(filePath)) throw new FileNotFoundException($"The file {fileName} was not found at path {filePath}.");

            string data = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();
        }
    }
}
