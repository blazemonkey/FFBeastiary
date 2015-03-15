using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Services.FileReaderService
{
    public interface IFileReaderService
    {
        Task<string> ReadFile(string fileName);
    }
}
