using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FFBestiary.Services.FileReaderService
{
    public interface IFileReaderService
    {
        Task<string> ReadFile(IStorageFolder folder, string fileName);
        Task WriteFile(IStorageFolder folder, string text, string fileName);
        Task CopyFile(IStorageFolder from, IStorageFolder to, string fileName);
        Task<bool> FileExists(IStorageFolder folder, string fileName);
    }
}
