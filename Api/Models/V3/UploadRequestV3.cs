namespace Api.Models.V3;

public record UploadRequestV3(string FirstName, string LastName, IEnumerable<DocumentRequestV3> Documents);

public record DocumentRequestV3(bool IsFavorite, IFormFile File);