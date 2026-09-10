using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace Domain.Integration.POCO
{
    public class IntegrationLog
    {
        // Identificador del mensaje
        public int Id { get; set; }

        // Identifica el origen del mensaje (Paciente, Producto, etc...)
        public string Transmitter { get; set; }

        // Código único asociado al mensaje (MessageId)
        public string MessageId { get; set; }

        // Contador de los reintentos de envío del mensaje
        public int RetryCount { get; set; }

        // 
        public string InternalBodyId { get; set; }

        // Fecha y hora del mensaje
        public DateTime EnqueuedTimeUtc { get; set; }

        //
        public string Source {  get; set; }

        // Estado del mensaje (Valido, Registrado, Invalido)
        public int Status { get; set; }

        // Fecha y hora del mensaje
        public DateTime CreationDate { get; set; }

        // Lista de detalles relacionados con el mensaje
        public List<IntegrationLogDetail> Details { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        return "Registrado";
                    case 2:
                        return "Válido";
                    case 3:
                        return "Erróneo";
                    default:
                        throw new InvalidOperationException($"Estado inválido: {Status}"); 
                }
            }
        }

        // Constructor opcional para inicializar la lista
        public IntegrationLog()
        {
            Details = new List<IntegrationLogDetail>();
        }

        private const string MODULE = "Integration";

        public string SourceName
        {
            get
            {
                string value = ResourceManager.get_GetString(this.Source, MODULE);
                return string.IsNullOrEmpty(value) ? this.Source : value;
            }
        }
    }
}
