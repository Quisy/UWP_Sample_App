using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace App.Helpers
{
    public class LocalStorageManager
    {
        private readonly Windows.Storage.StorageFolder _localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async Task WriteData(string data, string fileName)
        {
            StorageFile file = await _localFolder.CreateFileAsync($"{fileName}.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, data);
        }

        public async Task<string> ReadData(string fileName)
        {
            try
            {
                StorageFile file = await _localFolder.GetFileAsync($"{fileName}.txt");
                string data = await FileIO.ReadTextAsync(file);

                return data;
            }
            catch (Exception)
            {
                throw new ArgumentException("File Not Found");
            }
        }
    }
}
