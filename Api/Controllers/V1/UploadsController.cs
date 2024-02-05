using Api.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/uploads")]
public class UploadsController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromBody] UploadV1 upload)
    {
        ArgumentNullException.ThrowIfNull(upload);
        return Ok(upload);
    }
}