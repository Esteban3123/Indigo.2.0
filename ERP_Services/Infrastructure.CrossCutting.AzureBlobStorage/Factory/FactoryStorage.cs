using Infrastructure.CrossCutting.AzureBlobStorage.Storage;
using Infrastructure.CrossCutting.Base;

namespace Infrastructure.CrossCutting.AzureBlobStorage.Factory
{
    public class FactoryStorage : IFactoryStorage
    {
        private readonly string _blobContainerName;
        private readonly string _blobConnectionString;
        private readonly AzureBlobStorateService _azureBlobStorateService;
        public readonly EStorageType StorageType;
        public readonly bool UndefineConnection = false;

        /// <summary>
        /// constructor de la clase que se encarga de generar la forma en la que se va a guardar (Local o blobStorage)
        /// </summary>
        /// <param name="electronicBlobContainerName"></param>
        public FactoryStorage(string blobContainerName)
        {
            this._blobContainerName = blobContainerName;
            this._blobConnectionString = ServerSessionValues.Current.CurrentBlobConnectionString;

            if (string.IsNullOrEmpty( this._blobContainerName) || string.IsNullOrEmpty(this._blobConnectionString))
            {
                this.StorageType = EStorageType.LocalStore;
                return;
            }

            _azureBlobStorateService = new AzureBlobStorateService(_blobConnectionString, _blobContainerName);

            bool flagblob = _azureBlobStorateService?.TestConnection() ?? false;
            this.StorageType = (flagblob ? EStorageType.BlobStorage : EStorageType.LocalStore);
            return;
        }

        /// <summary>
        /// constructor que no necesita la cadena de conexion ni el nombre del blob 
        /// </summary>
        public FactoryStorage()
        {
            UndefineConnection = true;
        }

        /// <summary>
        /// crea el tipo de almacenamiento local o azureBlobStorage
        /// </summary>
        /// <returns></returns>
        public IStorage CreateStorageControl(string currentBlobConnectionString = null, string BlobContainerName = null)
        {
            if (UndefineConnection)
            {
                return CreateStorageManual(currentBlobConnectionString, BlobContainerName);
            }

            return CreateStorageControlAutomatic();
        }


        /// <summary>
        /// funcion que recibe los parametro de cadena de conexion para instanciar manualmente el blob
        /// </summary>
        /// <param name="currentBlobConnectionString"></param>
        /// <param name="BlobContainerName"></param>
        /// <returns></returns>
        private IStorage CreateStorageManual(string currentBlobConnectionString, string BlobContainerName)
        {
            if (string.IsNullOrEmpty(this._blobContainerName) || string.IsNullOrEmpty(this._blobConnectionString))
            {
                return new LocalStorateService();
            }

            var azureBlobStorateService = new AzureBlobStorateService(_blobConnectionString, _blobContainerName);

            bool flagblob = _azureBlobStorateService?.TestConnection() ?? false;

            if (flagblob)
            {
                return azureBlobStorateService;
            }
            else
            {
                return new LocalStorateService();
            }
        }

        /// <summary>
        /// crea el tipo de almacenamiento local o azureBlobStorage
        /// </summary>
        /// <returns></returns>
        private IStorage CreateStorageControlAutomatic()
        {
            switch (StorageType)
            {
                case (EStorageType.BlobStorage):
                    return _azureBlobStorateService;

                case (EStorageType.LocalStore):
                    return new LocalStorateService();
                default:
                    return new LocalStorateService();
            }
        }

    }
}
