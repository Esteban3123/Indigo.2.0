
#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base.Entities;
using System.Net;
using Newtonsoft.Json.Linq;
using Infrastructure.CrossCutting.Base;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
#endregion

namespace Application.Inventory.ConsumeService
{
    public class ConsumeServiceIntegration
    {

        private static ConsumeServiceIntegration instance = null;

        private ConsumeServiceIntegration() { }

        public static ConsumeServiceIntegration GetInstance()
        {
            if (instance == null)
                instance = new ConsumeServiceIntegration();

            return instance;
        }
        
        public ActionResult<string> GetObjectByUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(url));
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)(request.GetResponse());

            if(response.StatusCode != HttpStatusCode.OK)
            {
                response.Close();
                return new ActionResult<string> { ObjectEmbbeded = null, StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = response.StatusDescription };
            }


            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return new ActionResult<string> { ObjectEmbbeded = responseFromServer, StateResult = true, StatusCode = eStatusResult.SUCCESS };
        }     

 
        public ActionResult<string> PostObjectByUrlAndJson(string url, string json)
        {
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(url));
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var streamWritter = new StreamWriter(request.GetRequestStream()))
            {
                streamWritter.Write(json);
                streamWritter.Flush();
                streamWritter.Close();
            }

            HttpWebResponse response = (HttpWebResponse)(request.GetResponse());

            if(response.StatusCode != HttpStatusCode.OK)
            {
                response.Close();
                return new ActionResult<string> { ObjectEmbbeded = response.StatusDescription, StateResult = false, StatusCode = eStatusResult.WARNING, Message = response.StatusDescription };
            }

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return new ActionResult<string> {ObjectEmbbeded = responseFromServer, StateResult = true, StatusCode = eStatusResult.SUCCESS};
        }
            
    }
}
