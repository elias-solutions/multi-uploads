namespace Api.Models.V2;

public record DocumentResponseV2(string FileName, string FileNameWithExtension, string MimeType, byte[] Bytes);