using Infrastructure.CrossCutting.Base;
using System.IO;
using System.Linq;

namespace Infrastructure.CrossCutting.AzureBlobStorage.Storage
{
    public class LocalStorateService : IStorage
    {

        #region "Properties"
        /// <summary>
        /// Propiedad Tipo de almacenamiento
        /// </summary>
        EStorageType IStorage.StorageType => EStorageType.LocalStore;
        #endregion

        #region"Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void DeleteFile(string filePath, string fileName)
        {
            var file = System.IO.Path.Combine(filePath, fileName);
            if (ValidateFileExists( filePath,  fileName))
            {
                System.IO.File.Delete(file);
            }           
        }

        /// <summary>
        /// Solo valida si el archivo existe en dicha ruta 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns>(existe - true, No existe - false)</returns>
        public bool ValidateFileExists(string filePath, string fileName)
        {
            try
            {
                var file = System.IO.Path.Combine(filePath, fileName);
                return File.Exists(file);
            }
            catch
            {
                return false;
            }         
        }

        /// <summary>
        /// Asegura que la ruta a directorio exista. Si no existe la crea
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns>
        /// Si existe el archivo - false 
        /// Si NO existe crea la ruta  - true
        /// </returns>
        public bool ValidateIfNotExists(string filePath, string fileName)
        {
            try
            {
                return Utils.ValidateFileExists(filePath, fileName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// metodo encargado de escribir el archivo en local
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileBytes"></param>
        public void WriteFile(string filePath, string fileName, byte[] fileBytes)
        {
            var fileRoute = System.IO.Path.Combine(filePath, fileName);
            this.DeleteFile(filePath, fileName);

            if (this.ValidateIfNotExists(filePath, fileName))
            {
                File.WriteAllBytes(fileRoute, fileBytes);
            }
        }

      public  byte[] ReadFile(string filePath, string fileName)
        {
            byte[] fileByte = null;
            if (this.ValidateFileExists(filePath, fileName))
            {
                fileByte = Infrastructure.CrossCutting.Base.Utils.FileReadAllBytes(filePath, fileName);
            }
            return fileByte;
        }

        public string FindFirstFileName(string filePath, string prefix, string extension)
        {
            try
            {
                if (!Directory.Exists(filePath)) return null;

                var searchPattern = string.Concat(prefix ?? string.Empty, "*", extension ?? string.Empty);
                var file = Directory.GetFiles(filePath, searchPattern).FirstOrDefault();
                return string.IsNullOrEmpty(file) ? null : Path.GetFileName(file);
            }
            catch
            {
                return null;
            }
        }

        public string ReadFileFromBlobUrl(string blobUrl)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
