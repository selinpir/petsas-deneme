using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;   // AnyAsync için
using petsas2.Data;
using petsas2.Models;

namespace petsas2.Data.Seed
{
    public class DbSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (await context.Provinces.AnyAsync())
                return;

            var json = await File.ReadAllTextAsync("Data/Seed/Turkey.json");
            var list = JsonSerializer.Deserialize<List<ProvinceSeed>>(json);

            // Verileri ekle
            foreach (var p in list)
            {
                var province = new Province { Id = p.Id, Name = p.Name };
                context.Provinces.Add(province);

                foreach (var d in p.Districts)
                {
                    context.Districts.Add(new District
                    {
                        Id = d.Id,
                        Name = d.Name,
                        ProvinceId = p.Id
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
//bunlar gerçek class değil seed ederken kullanacağımız classlar ??? DETAYI ne????
public class ProvinceSeed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<DistrictSeed> Districts { get; set; }
}

public class DistrictSeed
{
    public int Id { get; set; }
    public string Name { get; set; }
}