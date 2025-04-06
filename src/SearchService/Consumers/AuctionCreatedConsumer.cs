namespace SearchService.Consumers;

public class AuctionCreatedConsumer(IMapper _mapper) : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
         Console.WriteLine("--> Consuming auction created: " + context.Message.Id);
         var item = _mapper.Map<Item>(context.Message);
         if(item.Model == "Foo") throw new ArgumentException("the name is not correct");
         await item.SaveAsync();
    }
}
