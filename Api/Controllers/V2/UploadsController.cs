using API.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/uploads")]
public class UploadsController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromForm] UploadV2 upload)
    {
        ArgumentNullException.ThrowIfNull(upload);

        var uploadResponse = new UploadV2Response(upload.FirstName, upload.LastName, Map(upload.Files));
        return Ok(uploadResponse);
    }

    private IEnumerable<Document> Map(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            using var stream = file.OpenReadStream();
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            yield return new Document(file.Name, file.FileName, file.ContentType, ms.ToArray());
        }
    }
}