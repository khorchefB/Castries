
namespace SearchService.Consumers;

public class AuctionDeletedConsumer(IMapper _mapper)  : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        var item = _mapper.Map<Item>(context.Message);
        var result = await DB.DeleteAsync<Item>(item);
        if(!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionDeleted), "Problem deleting auction");
    }
}
