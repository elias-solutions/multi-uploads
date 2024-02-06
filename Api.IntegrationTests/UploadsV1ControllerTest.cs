using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Api.Models.V1;
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
        var expected = new UploadRequestV1("Clark", "Kent", CreateDocs());
        var stringJson = JsonSerializer.Serialize(expected);
        var stringContent = new StringContent(stringJson, Encoding.UTF8, MediaTypeNames.Application.Json);

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/api/v1/uploads", UriKind.Relative));
        request.Content = stringContent;
        var result = await _client.SendAsync(request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var upload = await result.Content.ReadFromJsonAsync<UploadRequestV1>();

        upload.Should().BeEquivalentTo(expected);
    }

    private IEnumerable<DocumentRequestV1> CreateDocs()
    {
        yield return new DocumentRequestV1("FileOne", "FileOne.txt", "text/plain", new byte[] { 1, 2, 3, 4 });
        yield return new DocumentRequestV1("FileTwo", "FileTwp.txt", "text/plain", new byte[] { 1, 2, 3, 4 });
    }
}