using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public static class DbInitializer
{
    public static async Task InitDb(this WebApplication app)
    {
        try{
            await DB.InitAsync("SearchDb", MongoClientSettings
                            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                    .Key(x => x.Make, KeyType.Text)
                    .Key(x => x.Model, KeyType.Text)
                    .Key(x => x.Color, KeyType.Text)
                    .CreateAsync();   
            var count = await DB.CountAsync<Item>();
            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();
            var items = await httpClient.GetItemsForSerachDb();
            await items.SaveAsync();
            Console.WriteLine($"{items.Count()} returned from the auction service");
        }catch(Exception e){
            Console.WriteLine(e);
        }
    }
}
