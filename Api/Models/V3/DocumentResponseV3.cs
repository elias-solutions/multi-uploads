namespace Api.Models.V3;

public record DocumentResponseV3(string FileName, string FileNameWithExtension, string MimeType, byte[] Bytes);