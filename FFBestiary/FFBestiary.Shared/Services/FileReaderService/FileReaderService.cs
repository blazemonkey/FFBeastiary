using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FFBestiary.Services.FileReaderService
{
    public class FileReaderService : IFileReaderService
    {
        public async Task<string> ReadFile(IStorageFolder folder, string fileName)
        {
            var dataFolder = await folder.GetFolderAsync("Data");
            var file = await dataFolder.GetFileAsync(fileName);
            var content = await FileIO.ReadTextAsync(file);

            return content;
        }

        public async void WriteFile(IStorageFolder folder, string text, string fileName)
        {
            var dataFolder = await folder.GetFolderAsync("Data");
            var file = await dataFolder.GetFileAsync(fileName);
            await FileIO.WriteTextAsync(file, text);
        }

        public async Task CopyFile(IStorageFolder from, IStorageFolder to, string fileName)
        {
            var dataFolder = await from.GetFolderAsync("Data");
            var file = await dataFolder.GetFileAsync(fileName);

            var createDataFolder = await to.CreateFolderAsync("Data");
            await file.CopyAsync(createDataFolder);
        }

        public async Task<bool> FileExists(IStorageFolder folder, string fileName)
        {
            try
            {
                var dataFolder = await folder.GetFolderAsync("Data");
                await dataFolder.GetFileAsync(fileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }            
        }
    }
}
