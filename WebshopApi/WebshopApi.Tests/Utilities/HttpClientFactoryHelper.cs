using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebshopApi;

namespace WebshopApi.Tests.Utilities;
public static class HttpClientFactoryHelper
{
    public static HttpClient GetTestClient()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001"); 
                builder.UseEnvironment("Development");
            });

        return factory.CreateClient();
    }
}