using Api.Models.V3;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[ApiController]
[ApiVersion("3.0")]
[Route("api/v{version:apiVersion}/uploads")]
public class UploadsController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromForm] UploadRequestV3 uploadRequest)
    {
        ArgumentNullException.ThrowIfNull(uploadRequest);

        var uploadResponse = new UploadResponseV3(uploadRequest.FirstName, uploadRequest.LastName, Map(uploadRequest.Documents));
        return Ok(uploadResponse);
    }

    private IEnumerable<DocumentResponseV3> Map(IEnumerable<DocumentRequestV3> documents)
    {
        foreach (var document in documents)
        {
            using var stream = document.File.OpenReadStream();
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            yield return new DocumentResponseV3(document.File.Name, document.File.FileName, document.File.ContentType, ms.ToArray());
        }
    }
}