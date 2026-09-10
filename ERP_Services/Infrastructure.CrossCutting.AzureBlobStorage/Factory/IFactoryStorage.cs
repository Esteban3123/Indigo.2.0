using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.AzureBlobStorage.Factory
{
   public interface IFactoryStorage
    {
        /// <summary>
        /// metodo que crea la instancia al servicio de almacenamiento
        /// </summary>
        /// <param name="currentBlobConnectionString"></param>
        /// <param name="BlobContainerName"></param>
        /// <returns></returns>
        IStorage CreateStorageControl(string currentBlobConnectionString = null, string BlobContainerName = null);
    }
}
