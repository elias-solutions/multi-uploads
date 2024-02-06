namespace Api.Models.V1;

public record DocumentRequestV1(string FileName, string FileNameWithExtension, string MimeType, byte[] Bytes);