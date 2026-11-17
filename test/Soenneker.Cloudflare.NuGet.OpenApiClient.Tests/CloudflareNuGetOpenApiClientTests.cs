using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Soenneker.Cloudflare.NuGet.OpenApiClient.Tests;

public sealed class CloudflareNuGetOpenApiClientTests
{
    [Test]
    public async Task Deserializes_service_index_and_targets_configured_endpoint()
    {
        using var handler = new FeedResponseHandler();
        using var http = new HttpClient(handler);
        using var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: http) { BaseUrl = "https://nuget.test" };
        var client = new CloudflareNuGetOpenApiClient(adapter);
        var index = await client.V3.IndexJson.GetAsync();
        await Assert.That(index!.Version).IsEqualTo("3.0.0");
        await Assert.That(index.Resources!.Count).IsEqualTo(1);
        await Assert.That(handler.RequestUri).IsEqualTo("https://nuget.test/v3/index.json");
        await Assert.That(client.V3.Registration["Example"].IndexJson.ToGetRequestInformation().URI.AbsolutePath).IsEqualTo("/v3/registration/Example/index.json");
    }
}
