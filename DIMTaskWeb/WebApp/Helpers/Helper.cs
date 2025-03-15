namespace WebApp.Helpers
{
    public class Helper
    {
        public static async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream); // Copy the file to the memory stream
                return memoryStream.ToArray(); // Convert the memory stream to byte[]
            }
        }
        public enum UserRoles
        {
            Admin,
            User,

        }
    }
}
