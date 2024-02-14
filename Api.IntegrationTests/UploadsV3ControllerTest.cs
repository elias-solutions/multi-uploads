using System.Net;
using System.Net.Http.Json;
using Api.Models.V3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.IntegrationTests;

public class UploadsV3ControllerTest : WebApplicationFactory<Startup>
{
    private readonly HttpClient _client;

    public UploadsV3ControllerTest()
    {
        _client = CreateClient();
    }

    [Fact]
    public async Task Test()
    {
        var uploadRequest = CreateUploadRequest();
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new StringContent(uploadRequest.FirstName), nameof(UploadRequestV3.FirstName));
        multipartContent.Add(new StringContent(uploadRequest.LastName), nameof(UploadRequestV3.LastName));

        var i = 0;
        foreach (var requestDocument in uploadRequest.Documents)
        {
            var stream = requestDocument.File.OpenReadStream();
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            multipartContent.Add(new StreamContent(stream), $"{nameof(UploadRequestV3.Documents)}[{i}].{nameof(DocumentRequestV3.File)}", requestDocument.File.FileName);
            multipartContent.Add(new StringContent(requestDocument.IsFavorite.ToString()), $"{nameof(UploadRequestV3.Documents)}[{i}].{nameof(DocumentRequestV3.IsFavorite)}");
            i++;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/api/v3/uploads", UriKind.Relative));
        request.Content = multipartContent;
        var response = await _client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var upload = await response.Content.ReadFromJsonAsync<UploadResponseV3>();

        upload!.FirstName.Should().Be("Clark");
        upload.LastName.Should().Be("Kent");
        upload.Documents.Count().Should().Be(2);
    }

    private UploadRequestV3 CreateUploadRequest()
    {
        return new UploadRequestV3("Clark", "Kent", CreateDocumentsRequest());
    }

    private IEnumerable<DocumentRequestV3> CreateDocumentsRequest()
    {
        yield return new DocumentRequestV3(true, CreateFormFile("file1"));
        yield return new DocumentRequestV3(false, CreateFormFile("file2"));
    }

    private IFormFile CreateFormFile(string name)
    {
        var bytes = new byte[] { 1, 2, 3, 4 };
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "Files", $"{name}.txt");
    }

}