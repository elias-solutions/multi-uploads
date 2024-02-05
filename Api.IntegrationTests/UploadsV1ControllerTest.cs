using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Api.IntegrationTests;

public class UploadsV1ControllerTest : WebApplicationFactory<Startup>
{
    private readonly HttpClient _client;

    public UploadsV1ControllerTest()
    {
        _client = CreateClient();
    }

    [Fact]
    public async Task Test()
    {
        var expected = new UploadV1("Clark", "Kent", CreateDocs());
        var stringJson = JsonSerializer.Serialize(expected);
        var stringContent = new StringContent(stringJson, Encoding.UTF8, MediaTypeNames.Application.Json);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/api/v1/uploads", UriKind.Relative));
        request.Content = stringContent;
        var result = await _client.SendAsync(request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var upload = await result.Content.ReadFromJsonAsync<UploadV1>();

        upload.Should().BeEquivalentTo(expected);
    }

    private IEnumerable<Document> CreateDocs()
    {
        yield return new Document("FileOne", "FileOne.txt", "text/plain", [1, 2, 3, 4]);
        yield return new Document("FileTwo", "FileTwp.txt", "text/plain", [1, 2, 3, 4]);
    }
}