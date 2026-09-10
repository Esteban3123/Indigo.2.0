using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract]
    public class AgeValidationResult
    {
        /// <summary>
        /// Indica si el paciente o responsable es menor de edad y requiere asignar un responsable
        /// </summary>
        [DataMember]
        public bool RequiresResponsible { get; set; }

        /// <summary>
        /// Indica si la validación fue exitosa (paciente/responsable es mayor de edad o es persona jurídica)
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }

        /// <summary>
        /// Id del tercero responsable sugerido desde la información del ingreso
        /// </summary>
        [DataMember]
        public int? SuggestedResponsibleThirdPartyId { get; set; }

        /// <summary>
        /// Nombre del tercero responsable sugerido
        /// </summary>
        [DataMember]
        public string SuggestedResponsibleName { get; set; }

        /// <summary>
        /// NIT/Identificación del tercero responsable sugerido
        /// </summary>
        [DataMember]
        public string SuggestedResponsibleNit { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la validación
        /// </summary>
        [DataMember]
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Edad del paciente o responsable validado
        /// </summary>
        [DataMember]
        public int? Age { get; set; }

        /// <summary>
        /// Indica si es persona jurídica (no aplica validación de edad)
        /// </summary>
        [DataMember]
        public bool IsLegalPerson { get; set; }
    }
}

