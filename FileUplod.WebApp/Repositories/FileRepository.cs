namespace FileUplod.WebApp.Repositories
{
    public class FileRepository : IFileRepository
    {
        public async Task UplodFile(byte[] data, string fileName)
        {
            var filePath = Path.Combine("./UplodedFile/", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(data.AsMemory(0, data.Length));
            }
        }
    }
}
