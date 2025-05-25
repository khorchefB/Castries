namespace SearchService.Consumers;

public class AuctionUpdatedConsumer(IMapper _mapper) : IConsumer<AuctionUpdated>
{
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        var item = _mapper.Map<Item>(context.Message);
        var result =await  DB.Update<Item>().Match(x => x.ID == context.Message.Id).ModifyOnly(x => new {
            x.Color,
            x.Make,
            x.Model,
            x.Mileage,
            x.Year
        }, item).ExecuteAsync();
        if(!result.IsAcknowledged) throw new Exception("Problem updating mongodb");
    }
}
