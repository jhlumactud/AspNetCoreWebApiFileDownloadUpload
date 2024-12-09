using FTPFileApp.Helper;
using Microsoft.AspNetCore.StaticFiles;

namespace FTPFileApp.Services;

public class ManageFileService: IManageFile
{
    #region Upload File
    public async Task<string> UploadFile(IFormFile file)
    {
        string fileName = "";
        try
        {
            FileInfo _FileInfo = new FileInfo(file.FileName);
            fileName = file.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
            var _getFilePath = Common.GetFilePath(fileName);
            using (var _fileStream = new FileStream(_getFilePath, FileMode.Create))
            {
                await file.CopyToAsync(_fileStream);
            }
            return fileName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Download File
    public async Task<(byte[], string, string)> DownloadFile(string fileName)
    {
        try
        {
            var _getFilePath = Common.GetFilePath(fileName);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(_getFilePath, out var _ContentType))
            {
                _ContentType = "application/octect-stream";
            }
            var ReadAllBytesAsync = await File.ReadAllBytesAsync(_getFilePath);
            return (ReadAllBytesAsync, _ContentType, Path.GetFileName(_getFilePath));
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
