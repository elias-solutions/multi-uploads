namespace Api.Models.V1
{
    public record UploadRequestV1(string FirstName, string LastName, IEnumerable<DocumentRequestV1> Documents);
}
