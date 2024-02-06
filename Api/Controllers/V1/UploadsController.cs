using Api.Models.V1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/uploads")]
public class UploadsController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromBody] UploadRequestV1 uploadRequest)
    {
        ArgumentNullException.ThrowIfNull(uploadRequest);
        return Ok(uploadRequest);
    }
}