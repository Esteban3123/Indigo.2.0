using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using Infrastructure.CrossCutting.Exceptions;
using System.IO;
using System.IO.Compression;

namespace DistributedService.Notification.SignalR.Helpers
{
    public static class Helper
    {
        #region Utilities

        /// <summary>
        /// Obtiene la ruta a la carpeta de imágenes instantáneas
        /// </summary>
        /// <returns>Ruta a la carpeta de imágenes instantáneas</returns>
        public static string GetScreenshotFilesFolder()
        {
            try
            {
                var conf = ConfigurationManager.AppSettings["ScreenshotFilesFolder"];
                if (conf != null)
                    return conf.ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene la ruta a la carpeta de archivos copmpartidos
        /// configurada en el Web.Config
        /// </summary>
        /// <returns>Ruta a la carpeta de archivos compartidos</returns>
        public static string GetTempFilesFolder()
        {
            try
            {
                var conf = ConfigurationManager.AppSettings["TempFilesFolder"];
                if (conf != null)
                    return conf.ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene la ruta a la carpeta de paquetes de actualización
        /// configurada en el Web.Config
        /// </summary>
        /// <returns>Ruta a la carpeta de paquetes de actualización</returns>
        public static string GetUpgradeFilesFolder()
        {
            try
            {
                var conf = ConfigurationManager.AppSettings["UpgradeFilesFolder"];
                if (conf != null)
                    return conf.ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el nombre del archivo ejecutable de actualización ghost
        /// </summary>
        /// <returns>Nombre del paquete ejecutable de actualización ghost</returns>
        public static string GetUpgradeFileGhostService()
        {
            try
            {
                var conf = ConfigurationManager.AppSettings["UpgradeFileGhostService"];
                if (conf != null)
                    return conf.ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene la Url relativa a la carpeta de paquetes de actualización
        /// configurada en el Web.Config
        /// </summary>
        /// <returns>Url a la carpeta de paquetes de actualización</returns>
        public static string GetUpgradeFilesUrl()
        {
            try
            {
                var conf = ConfigurationManager.AppSettings["UpgradeFilesUrl"];
                if (conf != null)
                    return conf.ToString().Trim();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Comprime una cadena de bytes
        /// </summary>
        /// <param name="data">Bytes a comprimir</param>
        /// <returns>Cadena d ebytes comprimida</returns>
        public static byte[] ZipBytes(byte[] data)
        {
            byte[] dst = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    stream2.Write(data, 0, data.Length);
                    stream2.Close();
                    stream.Position = 0L;
                }
                using (new MemoryStream())
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    dst = new byte[buffer.Length + 4];
                    Buffer.BlockCopy(buffer, 0, dst, 4, buffer.Length);
                    Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, dst, 0, 4);
                }
            }
            return dst;
        }

        /// <summary>
        /// Descomprime una cadena de bytes
        /// </summary>
        /// <param name="data">Bytes a descomprimir</param>
        /// <returns>Cadena de bytes descomprimida</returns>
        public static byte[] UnZipBytes(byte[] data)
        {
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(data, 0);
                stream.Write(data, 4, data.Length - 4);
                buffer = new byte[num];
                stream.Position = 0L;
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    stream2.Read(buffer, 0, buffer.Length);
                }
            }
            return buffer;
        }

        #endregion
    }
}