namespace Api.Models.V3;

public record UploadResponseV3(string FirstName, string LastName, IEnumerable<DocumentResponseV3> Documents);