namespace SearchService.Consumers;

public class AuctionUpdatedConsumer(IMapper _mapper) : IConsumer<AuctionUpdated>
{
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        var item = _mapper.Map<Item>(context.Message);
        var result = DB.Update<Item>().Match(x => x.ID== item.ID).ModifyOnly(x => new {
            x.Make , 
            x.Model ,
            x.Color ,
            x.Mileage,
            x.Year 
        }, item).ExecuteAsync();
        if(!result.IsCompleted) throw new Exception("Problem updating mongodb");
    }
}
