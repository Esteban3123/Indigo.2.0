using Infrastructure.CrossCutting.Base;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;

namespace DistributedService.RCM.Utilities
{
    public class HeaderValueUtils
    {


        /// <summary>
        /// Metodo para poder validar los encabezados.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetValues(HttpRequestHeaders headers, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Llave del Header vacia");
            }

            if (headers.Contains(key))
            {
                return headers.GetValues(key).First();
            }

            throw new Exception($"Los encabezados son invalidos.({key})");
        }

        public static string GetOptionalValues(HttpRequestHeaders headers, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Llave del Header vacia");
            }

            if (headers.Contains(key))
            {
                return headers.GetValues(key).First();
            }

            if (! string.IsNullOrEmpty( ConfigurationManager.AppSettings[key]))
            {
               return ConfigurationManager.AppSettings[key];
            }

            throw new Exception($"Los encabezados son invalidos.({key})");
        }

        public static void CleanSessionVariables()
        {
            SessionValues.Instance.HisContainer = string.Empty;
            SessionValues.Instance.TransactionalContainer = string.Empty;
            ServerSessionValues.Current.CurrentContainer = string.Empty;
            SessionValues.Instance.CosmosDbContainer = string.Empty;
            SessionValues.Instance.SecurityContainer = string.Empty;
            SessionValues.Instance.CosmosDB = string.Empty;

        }
    }
}