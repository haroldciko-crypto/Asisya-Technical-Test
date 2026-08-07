using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Asisya.IntegrationTests;

public class ProductControllerIntegrationTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductControllerIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
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