namespace FileUplod.WebApp.Repositories
{
    public interface IFileRepository
    {
        Task UplodFile(byte[] data, string fileName);
    }
}
