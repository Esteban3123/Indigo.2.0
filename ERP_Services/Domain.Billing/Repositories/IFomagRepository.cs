using Domain.Billing.POCO;
using System;

namespace Domain.Billing.Repositories
{
    public interface IFomagRepository
    {
        /// <summary>
        /// Método para obtener la información del paciente usando el código
        /// </summary>
        /// <param name="codigoPaciente"></param>
        /// <returns></returns>
        PatientInfo GetPatientInfo(string codigoPaciente);

        /// <summary>
        /// Método para obtener la información de la integración con el FOMAG
        /// </summary>
        /// <param name="healthAdminId"></param>
        /// <returns></returns>
        IntegrationInfo GetIntegrationFomag(int healthAdminId);
    }
}
