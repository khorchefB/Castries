using System;
using MongoDB.Driver;
using MongoDB.Entities;

namespace BiddingService.Data;

public static class DbInitializer
{
    public static async Task InitDb(this WebApplication app)
    {
        try{
            await DB.InitAsync("BidDb", MongoClientSettings
                            .FromConnectionString(app.Configuration.GetConnectionString("BidDbConnection")));
           
        }catch(Exception e){
            Console.WriteLine(e);
        }
    }
}
