using System.Net;
using System.Net.Http.Json;
using API;
using API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.IntegrationTests;

public class UploadsV2ControllerTest : WebApplicationFactory<Startup>
{
    private readonly HttpClient _client;

    public UploadsV2ControllerTest()
    {
        _client = CreateClient();
    }

    [Fact]
    public async Task Test()
    {
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new ByteArrayContent(new byte[] { 1, 2, 3, 4 }), "Files", "FileOne.txt");
        multipartContent.Add(new ByteArrayContent(new byte[] { 4, 3, 2, 1}), "Files", "FileTwp.txt");
        multipartContent.Add(new StringContent("Clark"), nameof(UploadV2.FirstName));
        multipartContent.Add(new StringContent("Kent"), nameof(UploadV2.LastName));

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/api/v2/uploads", UriKind.Relative));
        request.Content = multipartContent;
        var result = await _client.SendAsync(request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var upload = await result.Content.ReadFromJsonAsync<UploadV2Response>();

        upload!.FirstName.Should().Be("Clark");
        upload.LastName.Should().Be("Kent");
        upload.Files.Count().Should().Be(2);
    }
}