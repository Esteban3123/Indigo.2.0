using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Infrastructure.CrossCutting.Base;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;

namespace Infrastructure.CrossCutting.AzureBlobStorage.Storage
{
    public class AzureBlobStorateService : IStorage
    {
       #region "Builder"
        /// <summary>
        /// constructor de la clase que se encarga de crear el cliente para el uso del blob storage de azure
        /// </summary>
        /// <param name="currentBlobConnectionString"></param>
        /// <param name="electronicBlobContainerName"></param>
        public AzureBlobStorateService(string currentBlobConnectionString, string blobContainerName)
        {
            _currentBlobConnectionString = currentBlobConnectionString;
            _blobContainerName = blobContainerName;

            if (string.IsNullOrEmpty(_currentBlobConnectionString))
            {
                throw new ArgumentNullException(nameof(_currentBlobConnectionString));
            }

            if (string.IsNullOrEmpty(_blobContainerName))
            {
                throw new ArgumentNullException(nameof(_blobContainerName));
            }
        }
        #endregion

        #region"Properties"
        private readonly string _currentBlobConnectionString;
        private readonly string _blobContainerName;
        private BlobContainerClient _blobContainerClient;
        private BlobServiceClient _blobServiceClient;
        private LocalStorateService _localStorateService;
        private  LocalStorateService LocalStorateService { get
            {
                if (_localStorateService is null)
                {
                    _localStorateService = new LocalStorateService();
                }
                return _localStorateService;
            }
        }
        /// <summary>
        /// Propiedad Tipo de almacenamiento
        /// </summary>
        EStorageType IStorage.StorageType => EStorageType.BlobStorage;
        #endregion

        #region"Methods"
        /// <summary>
        /// metodo para eliminar un archivo en el blobStorage
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void DeleteFile(string filePath, string fileName)
        {
            try
            {
                var container = GetClient();
                foreach (var file in GetBlobNameCandidates(filePath, fileName))
                {
                    var blockBlob = container.GetBlobClient(file);
                    blockBlob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
                }
            }
            catch (Exception)
            {
                LocalStorateService.DeleteFile(filePath, fileName);
            }
        }

        /// <summary>
        /// Metodo que valida si existe un archivo en el blob storage
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ValidateFileExists(string filePath, string fileName)
        {
            try
            {
                var container = GetClient();
                foreach (var file in GetBlobNameCandidates(filePath, fileName))
                {
                    var blockBlob = container.GetBlobClient(file);
                    if (blockBlob.Exists()) return true;
                }

                return false;
            }
            catch (Exception)
            {
                return this.LocalStorateService.ValidateFileExists(filePath, fileName);
            }
        }

        /// <summary>
        ///  Asegura que la ruta a directorio exista. Si no existe la crea
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns>
        ///  Si existe el archivo - false
        ///  Si NO existe crea la ruta  - true
        /// </returns>
        public bool ValidateIfNotExists(string filePath, string fileName)
        {
            try
            {
                var container = GetClient();

                foreach (var file in GetBlobNameCandidates(filePath, fileName))
                {
                    if (container?.GetBlobClient(file)?.Exists() == true) return false;
                }

                return true;

            }
            catch (Exception)
            {
                return this.LocalStorateService.ValidateIfNotExists(filePath, fileName);
            }
        }

        /// <summary>
        /// metodo encargado de escribir el archivo en el blobStorage
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileBytes"></param>
        public void WriteFile(string filePath, string fileName, byte[] fileBytes)
        {
            try
            {
                var file = NormalizeBlobName(System.IO.Path.Combine(filePath, fileName));
                var container = GetClient();
                container.CreateIfNotExists();
                var blockBlob = container.GetBlobClient(file);

                using (var ms = new MemoryStream(fileBytes))
                {
                    blockBlob.Upload(ms, overwrite: true);
                }
            }
            catch (Exception)
            {
                this.LocalStorateService.WriteFile( filePath,  fileName, fileBytes);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] ReadFile(string filePath, string fileName)
        {
            try
            {
                var container = GetClient();

                string token =this.GetSasTokenStorageAccount();
                foreach (var file in GetBlobNameCandidates(filePath, fileName))
                {
                    try
                    {
                        var blockBlob = container.GetBlobClient(file);
                        var content = blockBlob.DownloadContent();
                        return content.Value.Content.ToArray();
                    }
                    catch (Exception)
                    {
                    }
                }

                return LocalStorateService.ReadFile(filePath, fileName);
            }
            catch (Exception)
            {
                return LocalStorateService.ReadFile(filePath, fileName);
            }
        }

        public string FindFirstFileName(string filePath, string prefix, string extension)
        {
            try
            {
                var container = GetClient();
                foreach (var blobPrefix in GetBlobNameCandidates(filePath, prefix ?? string.Empty))
                {
                    foreach (var blob in container.GetBlobs(prefix: blobPrefix))
                    {
                        var fileName = GetBlobFileName(blob.Name);
                        if (fileName.StartsWith(prefix ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
                            fileName.EndsWith(extension ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                        {
                            return fileName;
                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return LocalStorateService.FindFirstFileName(filePath, prefix, extension);
            }
        }

        private static string[] GetBlobNameCandidates(string filePath, string fileName)
        {
            var rawName = System.IO.Path.Combine(filePath, fileName);
            var normalizedName = NormalizeBlobName(rawName);

            if (string.Equals(rawName, normalizedName, StringComparison.Ordinal))
            {
                return new[] { rawName };
            }

            return new[] { normalizedName, rawName };
        }

        private static string NormalizeBlobName(string blobName)
        {
            return blobName?.Replace('\\', '/');
        }

        private static string GetBlobFileName(string blobName)
        {
            var normalizedName = NormalizeBlobName(blobName);
            var lastSeparator = normalizedName.LastIndexOf('/');
            return lastSeparator >= 0 ? normalizedName.Substring(lastSeparator + 1) : normalizedName;
        }

        /// <summary>
        /// Se obtiene la instancia del cliente del blob
        /// </summary>
        /// <returns></returns>
        private BlobContainerClient GetClient()
        {
            try
            {
                if (this._blobContainerClient is null)
                {
                    this._blobServiceClient = GetCloudBlobClient(this._currentBlobConnectionString);
                    var serviceProperties = _blobServiceClient.GetProperties();
                    _blobServiceClient.SetProperties(serviceProperties);
                    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(this._blobContainerName);
                }

                return _blobContainerClient;
            }
            catch (Exception ex)
            {
                var eventLog = new EventLog();
                eventLog.Source = "Error creando conexión blobStorage";
                eventLog.WriteEntry($"Error: {ex}", EventLogEntryType.Error);
                return null;
            }
        }

        /// <summary>
        /// funcion para abrir la conexion con el servicio del blobStorage
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private BlobServiceClient GetCloudBlobClient(string connectionString)
        {
            try
            {
                var blobClientOptions = new BlobClientOptions();
                var blobClient = new BlobServiceClient(connectionString, blobClientOptions);
                return blobClient;
            }
            catch (RequestFailedException ex)
            {
                Exceptions.IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                throw;
            }
        }

        /// <summary>
        /// metodo encargado de generar el token para el acceso a archivos
        /// </summary>
        /// <returns></returns>
        private string GetSasTokenStorageAccount()
        {
            try
            {
                string sasTokenStorageAccount = "";
                var container = GetClient();

                // Definir los permisos y la fecha de expiración para el token SAS
                BlobContainerSasPermissions permissions = BlobContainerSasPermissions.All; // Permisos: todos
                DateTimeOffset expiresOn = DateTimeOffset.UtcNow.AddHours(1); // Hora actual más 1 hora

                // Generar la URI con el token SAS
                Uri sasUri = container.GenerateSasUri(permissions, expiresOn);

                sasTokenStorageAccount = sasUri.Query;
                return sasTokenStorageAccount;
            }
            catch
            {
                return string.Empty;
            }
           
        }

        /// <summary>
        /// verifica que exista una conexion cliente con el blob storage
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            var client = GetClient();

            return (client != null && client.Exists());
        }

        /// <summary>
        /// Obtiene el archivo de una ruta
        /// </summary>
        /// <param name="blobUrl"></param>
        /// <returns></returns>
        public string ReadFileFromBlobUrl(string blobUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(blobUrl))
                {
                    throw new ArgumentNullException(nameof(blobUrl));
                }

                // 🔹 Decodificar la URL por si tiene caracteres codificados como %3A o %5C
                string decodedUrl = HttpUtility.UrlDecode(blobUrl);

                // 🔹 Extraer el directorio y nombre del archivo
                Uri uri = new Uri(decodedUrl);
                string blobPath = uri.LocalPath.TrimStart('/'); // Remueve el primer '/'
                string directoryPath = Path.GetDirectoryName(blobPath)?.Replace("\\", "/") ?? "";
                directoryPath = directoryPath.Replace($"{_blobContainerName}/", "");
                string fileName = Path.GetFileName(blobPath);


                var data = ReadFile(directoryPath, fileName);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return LocalStorateService.ReadFileFromBlobUrl(blobUrl);
            }
        }

        #endregion


    }
}
