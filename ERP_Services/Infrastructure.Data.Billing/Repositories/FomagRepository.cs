using System;
using System.Linq;
using System.Collections.Generic;
using Domain.Billing.POCO;
using Domain.Billing.Repositories;
using Infrastructure.Data.Base;
using Infrastructure.Data.ModelRepository;

namespace Infrastructure.Data.Billing.Repositories
{
    public class FomagRepository: BaseRepository, IFomagRepository
    {
        private IGlobalModelUnitOfWork _context;

        public FomagRepository(IGlobalModelUnitOfWork context): base(context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Obtiene los datos del paciente usando el código
        /// </summary>
        /// <param name="codigoPaciente"></param>
        /// <returns></returns>
        public PatientInfo GetPatientInfo(string codigoPaciente)
        {
            List<(String, Object)> param = new List<(string, object)>() { ("@CodigoPaciente", codigoPaciente) };
            IEnumerable<PatientInfo> patients = ExecuteQueryDR<PatientInfo>(
                @"SELECT 
	                t.CODIGO as Code,
	                DATEDIFF(day,IPFECNACI,common.GETDATE()) as BornDays,
	                p.IPTIPODOC DocumentType,
	                p.CODIGONIT DocumentNumber
                  FROM INPACIENT p
                  INNER JOIN ADTIPOIDENTIFICA t ON t.Id = p.IPTIPODOC
                  WHERE p.IPCODPACI = @CodigoPaciente"
                , param);

            PatientInfo patient = patients.First();

            return patient;
        }

        /// <summary>
        /// Obtiene los datos de la integración usando el Id
        /// </summary>
        /// <param name="healthAdminId"></param>
        /// <returns></returns>
        public IntegrationInfo GetIntegrationFomag(int healthAdminId) 
        {
            List<(String, Object)> param = new List<(string, object)>() { ("@Id", healthAdminId) };
            IEnumerable<IntegrationInfo> integrations = ExecuteQueryDR<IntegrationInfo>(
                @"SELECT Inte.* 
                  FROM Integrations.FomagAssurance Inte 
                  INNER JOIN Contract.HealthAdministrator HA on Inte.CodeIndigo = HA.code 
                  WHERE HA.Id = @Id"
                , param);

            IntegrationInfo integrationInfo = integrations.First();

            return integrationInfo;
        }
    }
}
