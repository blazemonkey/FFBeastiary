using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FFBestiary.Services.FileReaderService
{
    public class FileReaderService : IFileReaderService
    {
        private StorageFolder _folder;

        public FileReaderService()
        {
            _folder = Package.Current.InstalledLocation;            
        }

        public async Task<string> ReadFile(string fileName)
        {
            var dataFolder = await _folder.GetFolderAsync("Data");
            var file = await dataFolder.GetFileAsync(fileName);
            var content = await FileIO.ReadTextAsync(file);

            return content;
        }
    }
}
