using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Asisya.IntegrationTests;

public class ProductControllerIntegrationTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductControllerIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            })
            .CreateClient();
    }

    [Fact]
    public async Task GetProducts_ShouldReturnUnauthorized_WhenNoToken()
    {
        // Act

        var response = await _client.GetAsync("/api/Product");

        // Assert

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}