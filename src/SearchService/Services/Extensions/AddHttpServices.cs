using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace SearchService.Services.Extensions;

public static class AddHttpServices
{
    public static void AddSpecificHttpClient(this IServiceCollection services) 
    {
        services.AddHttpClient<AuctionSvcHttpClient>()
            .AddPolicyHandler(GetPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetPolicy()
        => HttpPolicyExtensions.HandleTransientHttpError()
                               .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                               .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
}
