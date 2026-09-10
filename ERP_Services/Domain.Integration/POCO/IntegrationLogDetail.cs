using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace Domain.Integration.POCO
{
    public class IntegrationLogDetail
    {
        public int Id { get; set; }
        public int IntegrationLogId { get; set; }
        public string MessageId { get; set; }
        public string Transmitter { get; set; }
        public string InternalBodyId { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public string Source { get; set; }
        public byte Status { get; set; }
        public DateTime? CreationDate { get; set; }
        public string LogMessage { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        return "Fallido";
                    case 2:
                        return "Válido";
                    default:
                        throw new InvalidOperationException($"Estado inválido: {Status}");
                }
            }
        }

        private const string MODULE = "Integration";

        public string SourceName
        {
            get
            {
                string value = ResourceManager.get_GetString(this.Source, MODULE);
                return string.IsNullOrEmpty(value)? this.Source:value;
            }
        }
    }
}