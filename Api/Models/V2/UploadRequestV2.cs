namespace Api.Models.V2;

public record UploadRequestV2(string FirstName, string LastName, IEnumerable<IFormFile> Files);