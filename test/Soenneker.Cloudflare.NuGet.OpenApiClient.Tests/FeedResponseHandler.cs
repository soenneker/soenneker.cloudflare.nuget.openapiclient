using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.NuGet.OpenApiClient.Tests;

internal sealed class FeedResponseHandler : HttpMessageHandler
{
    public string? RequestUri { get; private set; }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestUri = request.RequestUri!.AbsoluteUri;
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("""{"version":"3.0.0","resources":[{"@id":"https://nuget.test/v3-flatcontainer/","@type":"PackageBaseAddress/3.0.0"}]}""", Encoding.UTF8, "application/json")
        });
    }
}
