namespace Api.Models.V2;

public record UploadResponseV2(string FirstName, string LastName, IEnumerable<DocumentResponseV2> Files);