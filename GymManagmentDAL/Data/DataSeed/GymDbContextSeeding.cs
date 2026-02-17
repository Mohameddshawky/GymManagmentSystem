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
        public static bool SeedData(GymDbcontext context, string? webRootPath, string? contentRootPath = null)
        {
            var rootPath = webRootPath ?? contentRootPath;
            if (string.IsNullOrEmpty(rootPath))
            {
                Console.WriteLine("Seeding skipped: No root path provided.");
                return false;
            }

            try
            {
                var hasPlans = context.plans.Any();
                var hasCategories = context.categories.Any();

                if (hasPlans && hasCategories) return false;

                bool changed = false;
                if (!hasCategories)
                {
                    var categories = LoadDataFromJson<Category>("categories.json", rootPath);
                    if (categories != null && categories.Any())
                    {
                        context.categories.AddRange(categories);
                        changed = true;
                    }
                }
                if (!hasPlans)
                {
                    var plans = LoadDataFromJson<Plan>("plans.json", rootPath);
                    if (plans != null && plans.Any())
                    {
                        context.plans.AddRange(plans);
                        changed = true;
                    }
                }
                
                if (changed)
                {
                    return context.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Re-throw or log more aggressively to ensure visibility in production logs
                var message = $"Seeding Failed: {ex.Message}";
                if (ex.InnerException != null) message += $" | Inner: {ex.InnerException.Message}";
                Console.WriteLine(message);
                throw new Exception(message, ex); 
            }
        }


        private static List<T> LoadDataFromJson<T>(string fileName, string rootPath) 
        {
            var filePath = Path.Combine(rootPath, "Files", fileName);
            
            // Try fallback if wwwroot/Files doesn't find it (common in some deployment structures)
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(rootPath, "wwwroot", "Files", fileName);
            }

            if (!File.Exists(filePath)) 
                throw new FileNotFoundException($"The file {fileName} was not found. Checked: {Path.Combine(rootPath, "Files", fileName)} and fallback.");

            string data = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();
        }
    }
}
