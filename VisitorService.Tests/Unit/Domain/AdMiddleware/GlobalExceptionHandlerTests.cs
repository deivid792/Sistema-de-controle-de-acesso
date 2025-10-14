using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using VisitorService.Interfaces.Middleware; // <-- namespace do seu middleware

public class GlobalExceptionHandlerTests
{
    [Fact]
    public async Task Middleware_ShouldReturnInternalServerError_OnException()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder.UseTestServer();
                webBuilder.Configure(app =>
                {
                    // Middleware global de tratamento de exceções
                    app.UseGlobalExceptionHandler();

                    // Força uma exceção para testar o middleware
                    app.Run(context => throw new Exception("Test exception"));
                });
            })
            .StartAsync();

        var client = host.GetTestClient();

        var response = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}