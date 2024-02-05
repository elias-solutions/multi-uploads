namespace API.Models
{
    public record UploadV1(string FirstName, string LastName, IEnumerable<Document> Documents);

    public record Document(string FileName, string FileNameWithExtension, string MimeType, byte[] Bytes);

    public record UploadV2(string FirstName, string LastName, IEnumerable<IFormFile> Files);

    public record UploadV2Response(string FirstName, string LastName, IEnumerable<Document> Files);
}
