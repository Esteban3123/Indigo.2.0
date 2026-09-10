using System;
using System.Linq;
using System.Net.Http.Headers;

namespace DistributedService.SCM.Utilities
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
            if (headers.Contains(key))
            {
                return headers.GetValues(key).First();
            }

            throw new Exception("Los encabezados son invalidos.");
        }
    }
}