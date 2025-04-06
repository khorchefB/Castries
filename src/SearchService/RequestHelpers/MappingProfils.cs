using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers;

public class MappingProfils: Profile
{
    public MappingProfils()
    {
        CreateMap<AuctionCreated, Item> ();      
        CreateMap<AuctionUpdated, Item> ();      
    }
}
