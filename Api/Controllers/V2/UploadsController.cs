using Api.Models.V2;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/uploads")]
public class UploadsController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromForm] UploadRequestV2 uploadRequest)
    {
        ArgumentNullException.ThrowIfNull(uploadRequest);

        var uploadResponse = new UploadResponseV2(uploadRequest.FirstName, uploadRequest.LastName, Map(uploadRequest.Files));
        return Ok(uploadResponse);
    }

    private IEnumerable<DocumentResponseV2> Map(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            using var stream = file.OpenReadStream();
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            yield return new DocumentResponseV2(file.Name, file.FileName, file.ContentType, ms.ToArray());
        }
    }
}