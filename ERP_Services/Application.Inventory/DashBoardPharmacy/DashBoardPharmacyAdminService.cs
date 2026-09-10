using Application.Events.Serializers;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Domain.Inventory.POCO;
using Domain.Payroll;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;

namespace Application.Inventory.DashBoardPharmacy
{
    public class DashBoardPharmacyAdminService : IDashBoardPharmacyAdminService
    {
        #region Variables
        private IPharmacyRepository _pharmacyRepository;
        private IAdmissionRepository _admissionRepository;
        private IFunctionalUnitRepository _functionalUnitRepository;
        private ICareGroupRepository _careGroupRepository;
        private IHealthAdministratorRepository _healthAdministratorRepository;
        private IThirdPartyRepository _thirdPartyRepository;
        private IPatientRepository _patientRepository;
        #endregion

        #region Builder
        public DashBoardPharmacyAdminService(IPharmacyRepository pharmacyRepository, IAdmissionRepository admissionRepository, IFunctionalUnitRepository functionalUnitRepository, ICareGroupRepository careGroupRepository,
                                             IHealthAdministratorRepository healthAdministratorRepository, IThirdPartyRepository thirdPartyRepository, IPatientRepository patientRepository)
        {
            if ((pharmacyRepository == null))
            {
                throw new ArgumentNullException("Repositorio de pharmacyRepository vacio");
            }
            if ((admissionRepository == null))
            {
                throw new ArgumentNullException("Repositorio de admissionRepository vacio");
            }
            if ((functionalUnitRepository == null))
            {
                throw new ArgumentNullException("Repositorio de functionalUnitRepository vacio");
            }
            if ((careGroupRepository == null))
            {
                throw new ArgumentNullException("Repositorio de careGroupRepository vacio");
            }
            if ((healthAdministratorRepository == null))
            {
                throw new ArgumentNullException("Repositorio de healthAdministratorRepository vacio");
            }
            if ((thirdPartyRepository == null))
            {
                throw new ArgumentNullException("Repositorio de thirdPartyRepository vacio");
            }
            if ((patientRepository == null))
            {
                throw new ArgumentNullException("Repositorio de patientRepository vacio");
            }
            _admissionRepository = admissionRepository;
            _functionalUnitRepository = functionalUnitRepository;
            _careGroupRepository = careGroupRepository;
            _healthAdministratorRepository = healthAdministratorRepository;
            _thirdPartyRepository = thirdPartyRepository;
            _patientRepository = patientRepository;
            _pharmacyRepository = pharmacyRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtener una solicitud de farmacia por consecutivo
        /// </summary>
        /// <param name="consecutive"></param>
        /// <returns></returns>
        public ActionResult<HCFARMEPC> PharmacyByConsecutive(decimal consecutive)
        {
            try
            {
                HCFARMEPC pharmacy = _pharmacyRepository.GetPharmacyByConsecutive(consecutive);

                return new ActionResult<HCFARMEPC>
                {
                    StateResult = true,
                    ObjectEmbbeded = pharmacy
                };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<HCFARMEPC>
                {
                    StateResult = false,
                    MessageResult = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene GetAdmissionInformation usado para la cargar las solicitudes
        /// </summary>
        /// <param name="dispensingIntegration"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="functionalUnitCode"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<AdmissionInformation> GetAdmissionInformation(int dispensingIntegration, int careGroupIdIntegrationMedilaser, string admissionNumber, string functionalUnitCode, AuditMessage audit)
        {
            try
            {
                AdmissionInformation admissionInformation = new AdmissionInformation();

                if (dispensingIntegration == 0)
                {
                    admissionInformation.aDINGRESO = _admissionRepository.GetAdmissionByCode(admissionNumber);
                    if (String.IsNullOrEmpty(admissionInformation.aDINGRESO.AdmissionCode))
                    {
                        throw new Exception("El ingreso " + admissionNumber + " no se existe.");
                    }
                    admissionInformation.functionalUnit = _functionalUnitRepository.GetFunctionalUnit(functionalUnitCode);
                    if (admissionInformation.functionalUnit.Id == 0)
                    {
                        throw new Exception("La unidad funcional " + functionalUnitCode + " no se encuentra homologada.");
                    }
                    admissionInformation.careGroup = _careGroupRepository.GetCareGroupByIdSimple(Convert.ToInt32(admissionInformation.aDINGRESO.GENCAREGROUP));
                    if (admissionInformation.careGroup.Id == 0)
                    {
                        throw new Exception("El grupo de atención asociado al ingreso no existe en Indigo VIE.");
                    }
                    if (admissionInformation.careGroup.CareGroupType == 3)
                    {
                        INPACIENT patient = _patientRepository.GetPatientByIdentification(admissionInformation.aDINGRESO.IPCODPACI);
                        ThirdParty thirdPatient = _thirdPartyRepository.GetThirdPartyByNit(patient.IPCODPACI);
                        if (thirdPatient.Id == 0)
                        {
                            throw new Exception("El paciente no esta creado como tercero en Indigo VIE.");
                        }
                        admissionInformation.thirdPartyId = thirdPatient.Id;
                        admissionInformation.thirdPartyPatienDate = (thirdPatient.Person == null) ? null : thirdPatient.Person.BirthDate;
                        admissionInformation.genderThirdParty = (thirdPatient.Person == null) ? null : thirdPatient.Person.Gender;
                    }
                    else
                    {
                        if (admissionInformation.aDINGRESO.GENCONENTITY == 0)
                        {
                            throw new Exception("El ingreso no tiene una entidad administradora de salud asociada.");
                        }
                        Domain.Entities.HealthAdministrator healthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(Convert.ToInt32(admissionInformation.aDINGRESO.GENCONENTITY));

                        if (healthAdministrator.Id == 0)
                        {
                            throw new Exception("La entidad administradora de salud asociada al ingreso no existe en Indigo VIE.");
                        }
                        admissionInformation.healthAdministratorId = healthAdministrator.Id;
                        admissionInformation.thirdPartyId = healthAdministrator.ThirdPartyId;
                        admissionInformation.genderThirdParty = (healthAdministrator.ThirdParty == null) ? null : healthAdministrator.ThirdParty.Person.Gender;
                        admissionInformation.thirdPartyPatienDate = (healthAdministrator.ThirdParty == null) ? null : healthAdministrator.ThirdParty.Person.BirthDate;
                    }
                }
                else
                {
                    admissionInformation.careGroup = _careGroupRepository.GetCareGroupByIdSimple(careGroupIdIntegrationMedilaser);
                }

                return new ActionResult<AdmissionInformation> { StateResult = true, ObjectEmbbeded = admissionInformation };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<AdmissionInformation> { StateResult = false, MessageResult = new List<string> { ex.Message } };
            }
        }

        /// <summary>
        /// Obtener una solicitud de paquete QX por consecutivo
        /// </summary>
        /// <param name="consecutive"></param>
        /// <returns></returns>
        public ActionResult<ViewDashBoardPharmacy_SurgicalPackage> SurgicalPackageByConsecutive(decimal consecutive)
        {
            try
            {
                ViewDashBoardPharmacy_SurgicalPackage pharmacy = _pharmacyRepository.SurgicalPackageByConsecutive(consecutive);

                return new ActionResult<ViewDashBoardPharmacy_SurgicalPackage>
                {
                    StateResult = true,
                    ObjectEmbbeded = pharmacy
                };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<ViewDashBoardPharmacy_SurgicalPackage>
                {
                    StateResult = false,
                    MessageResult = new List<string> { ex.Message }
                };
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _admissionRepository = null;
                _functionalUnitRepository = null;
                _careGroupRepository = null;
                _thirdPartyRepository = null;
                _healthAdministratorRepository = null;
                _thirdPartyRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
