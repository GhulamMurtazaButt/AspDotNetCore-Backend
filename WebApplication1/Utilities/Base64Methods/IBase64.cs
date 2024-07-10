namespace WebApplication1.Utilities.Base64
{
    public interface IBase64
    {
        public string Base64Encode(string plainText);
        public string Base64Decode(string base64EncodedData);
    }
}
