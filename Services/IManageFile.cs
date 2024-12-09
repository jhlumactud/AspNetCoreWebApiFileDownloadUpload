namespace FTPFileApp.Services;

public interface IManageFile
{
    public Task<string> UploadFile(IFormFile file);
    public Task<(byte[], string, string)> DownloadFile(string fileName);
}
