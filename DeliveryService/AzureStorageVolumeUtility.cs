using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class AzureStorageVolumeUtility : IDisposable
    {
        private string stateFolderPath;
        private string stateFilePath;
        private bool disposed = false;

        public AzureStorageVolumeUtility()
        {
            this.stateFolderPath = GetStateFolderPath();

            if (!Directory.Exists(this.stateFolderPath))
            {
                Directory.CreateDirectory(this.stateFolderPath);
            }

            this.stateFilePath = Path.Combine(this.stateFolderPath, "log.txt");
        }


        public void Write(string value)
        {
            try
            {
                File.AppendAllText(this.stateFilePath, value);
            }
            catch (Exception e)
            {
                Console.WriteLine("AzureStorageVolumeUtility: Write-Error {0} ; File: {1}.", e.Message, stateFilePath);
            }
        }

        public string Read ()
        {
            try
            {
                return File.ReadAllText(this.stateFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("AzureStorageVolumeUtility: Read-Error {0} ; File: {1}.", e.Message, stateFilePath);
                return $"Read-Error {e.Message} ; File: {stateFilePath}";
            }
        }


        public string GetStateFolderPath()
        {
            var storeRootPath = GetStoreRootPath("delivery");

            var subFolderName = Environment.GetEnvironmentVariable("STATE_FOLDER_NAME");
            if (string.IsNullOrEmpty(subFolderName))
            {
                subFolderName = Environment.GetEnvironmentVariable("Fabric_Id");
            }

            if (string.IsNullOrEmpty(subFolderName))
            {
                subFolderName = Guid.NewGuid().ToString();
            }

            var stateFolderPath = Path.Combine(storeRootPath, subFolderName);
            return stateFolderPath;
        }

        private static string GetStoreRootPath(string appName)
        {
            var storeRootPath = Environment.GetEnvironmentVariable("STORE_ROOT");
            if (string.IsNullOrEmpty(storeRootPath))
            {
                var codeFolderFullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                storeRootPath = Path.Combine(codeFolderFullPath, "data");
            }

            if (!string.IsNullOrEmpty(appName))
            {
                storeRootPath = Path.Combine(storeRootPath, appName);
            }

            return storeRootPath;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
