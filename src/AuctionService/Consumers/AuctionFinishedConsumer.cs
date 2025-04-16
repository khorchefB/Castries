
namespace AuctionService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _dbContext;

    public AuctionFinishedConsumer(AuctionDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async  Task Consume(ConsumeContext<AuctionFinished> context)
    {
        Console.WriteLine("--> Consuming auction consumed");
       var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);
       if(context.Message.ItemSold)
       {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
       }

        auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;
        await _dbContext.SaveChangesAsync();
    }
}
