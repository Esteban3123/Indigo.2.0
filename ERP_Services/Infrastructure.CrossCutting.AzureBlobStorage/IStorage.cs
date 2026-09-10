using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.AzureBlobStorage
{
  public  interface IStorage
    {
        void DeleteFile(string filePath, string fileName);

        bool ValidateFileExists(string filePath, string fileName);

        void WriteFile(string filePath, string fileName, byte[] fileBytes );

        EStorageType StorageType { get; }

        bool ValidateIfNotExists(string filePath, string fileName);
        byte[] ReadFile(string filePath, string fileName);

        string ReadFileFromBlobUrl(string blobUrl);

    }
}
