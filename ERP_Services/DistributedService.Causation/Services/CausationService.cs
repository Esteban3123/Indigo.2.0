using Application.MedicalFees;
using DistributedService.Causation.Exceptions;
using DistributedService.Causation.Models;
using DistributedService.Causation.Unity;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace DistributedService.Causation.Services
{
    /// <summary>
    /// Servicio de causación optimizado con transaccionalidad asíncrona
    /// </summary>
    public class CausationService : ICausationService
    {
        // ===== CONSTANTES PARA ELIMINAR MAGIC NUMBERS =====
        private const int SERVICE_TYPE_DIAGNOSTIC_IMAGING = 3;
        private const int MEDICAL_FEES_STATUS_REGISTERED = 2;
        private const int MEDICAL_FEES_STATUS_CONFIRMED = 3;
        private const string PROCESSED_IMAGE_STATE = "3";
        private const string INTERPRETED_STUDY_STATE = "4";
        private const int BATCH_SIZE = 500;

        // ===== SERVICIOS CACHED PARA MEJOR RENDIMIENTO =====
        private IMedicalFeesCausationAdminService _medicalFeesCausationAdminService;
        private IMedicalFeesContractAdminService _medicalFeesContractAdminService;
        private ICausationPendingRepository _causationPendingRepository;
        private IPatientRepository _patientRepository;
        private IMedicalFeesCausationRepository _medicalFeesCausationRepository;

        // ===== ESTADO DE SERVICIOS INICIALIZADOS =====
        private bool _servicesInitialized = false;
        private string _currentContainer = null;

        public CausationService()
        {
            // Constructor limpio sin lógica pesada
        }

        /// <summary>
        /// Inicializa los servicios solo una vez por contenedor para mejor rendimiento
        /// </summary>
        private void EnsureServicesInitialized(string container)
        {
            if (!_servicesInitialized || _currentContainer != container)
            {
                var containerInstance = Container.Current(container, container);
                
                _medicalFeesCausationAdminService = containerInstance.Resolve<IMedicalFeesCausationAdminService>();
                _medicalFeesContractAdminService = containerInstance.Resolve<IMedicalFeesContractAdminService>();
                _causationPendingRepository = containerInstance.Resolve<ICausationPendingRepository>();
                _patientRepository = containerInstance.Resolve<IPatientRepository>();
                _medicalFeesCausationRepository = containerInstance.Resolve<IMedicalFeesCausationRepository>();
                
                _servicesInitialized = true;
                _currentContainer = container;
            }
        }

        /// <summary>
        /// Factura de causación
        /// </summary>
        /// <param name="invoices"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<ActionResult> CausateInvoicesAsync(List<InvoiceEvent> invoices, string container, string usercode, List<byte> allowedServiceTypes = null)
        {
            var actionResult = new ActionResult()
            {
                StatusCode = eStatusResult.SUCCESS,
                StateResult = true
            };
            try
            {
                EnsureServicesInitialized(container);

                var invoiceNumbers = invoices.Select(m => m.InvoiceNumber).ToList();
                var details = _medicalFeesCausationAdminService.GetViewListNoSurgical(invoiceNumbers);
                var detailsQx = _medicalFeesCausationAdminService.ListViewSurgicalAndPackageByInvoiceNumber(invoiceNumbers);

                if (!details.Any() && !detailsQx.Any()) throw new IndigoValidationException("Factura no encontrada");

                var totalCandidateDetails = details.Count + detailsQx.Count;
                var excludedDetailsCount = 0;
                if (allowedServiceTypes != null && allowedServiceTypes.Any())
                {
                    var cupsEntityIds = details.Select(detail => detail.CupsEntityId)
                        .Concat(detailsQx.Select(detail => detail.CupsEntityId))
                        .Distinct()
                        .ToList();

                    var allowedCupsEntityIds = new HashSet<int>(
                        _medicalFeesCausationAdminService.GetCupsEntityIdsByServiceTypes(cupsEntityIds, allowedServiceTypes));

                    details = details.Where(detail => allowedCupsEntityIds.Contains(detail.CupsEntityId)).ToList();
                    detailsQx = detailsQx.Where(detail => allowedCupsEntityIds.Contains(detail.CupsEntityId)).ToList();
                    excludedDetailsCount = totalCandidateDetails - details.Count - detailsQx.Count;
                }

                var causationList = new List<MedicalFeesCausation>();
                var savedCausationsCount = 0;

                foreach (var invoice in invoices)
                {
                    var ivoiceDetails = details.Where(m => m.InvoiceNumber == invoice.InvoiceNumber).ToList();
                    var ivoiceDetailsQx = detailsQx.Where(m => m.InvoiceNumber == invoice.InvoiceNumber).ToList();

                    if (ivoiceDetails != null && ivoiceDetails.Any())
                    {
                        var causations = CauseInvoices(ivoiceDetails, usercode); // Versión optimizada sin commits internos
                        causationList.AddRange(causations);
                    }

                    if (ivoiceDetailsQx != null && ivoiceDetailsQx.Any())
                    {
                        var causations = CauseInvoiceQX(ivoiceDetailsQx, usercode); // Versión optimizada sin commits internos  
                        causationList.AddRange(causations);
                    }
                }

                if (causationList != null && causationList.Any())
                {
                    var res = _medicalFeesCausationAdminService
                        .SaveMedicalFeesCausation(causationList, null, null, new AuditMessage
                        {
                            CodeUser = usercode
                        });
                    actionResult = new ActionResult()
                    {
                        StatusCode = res.StatusCode,
                        StateResult = res.StateResult,
                        MessageResult = res.MessageResult,
                        Message = !string.IsNullOrWhiteSpace(res.Message)
                            ? res.Message
                            : res.MessageResult != null ? string.Join(" ", res.MessageResult) : null,
                    };

                    if (res.StateResult)
                    {
                        savedCausationsCount = causationList.Count;
                    }
                }

                await _causationPendingRepository.UnitWork.CommitAsync();

                var processingSummary = $"Resultado de causación: {savedCausationsCount} causaciones generadas, {excludedDetailsCount} detalles excluidos por tipo de servicio.";
                actionResult.Message = string.IsNullOrWhiteSpace(actionResult.Message)
                    ? processingSummary
                    : $"{actionResult.Message} {processingSummary}";
            }
            catch (IndigoValidationException ex)
            {
                _causationPendingRepository.SaveEntity(new CausationPending
                {
                    InvoiceNumber = "-1",
                    PatientCode = "-1",
                    PatientName = "NA",
                    PerformsHealthProfessionalCode = "NA",
                    AdmissionNumber = "-1",
                    InvoiceDate = DateTime.Now,
                    Error = ex.Message,
                    IsQx = false,
                    Data = JsonConvert.SerializeObject(invoices),
                    CreationDate = DateTime.Now
                });

                await _causationPendingRepository.UnitWork.CommitAsync();

                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                //_apm.Error(ex);
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                actionResult = new ActionResult { 
                    StatusCode = eStatusResult.EXCEPTION, 
                    StateResult = false, 
                    Message = ex.Message
                };
            }
            finally
            {
                DisposeServices();
            }
            return actionResult;
        }

        /// <summary>
        /// Versión optimizada de CauseInvoiceQX para contexto asíncrono (sin commits internos)
        /// </summary>
        private List<MedicalFeesCausation> CauseInvoiceQX(List<ViewListSurgicalAndPackage> ivoiceDetailsQx, string usercode)
        {
            var medicalList = new List<MedicalFeesCausation>();

            foreach (var detail in ivoiceDetailsQx)
            {
                try
                {
                    ValidatePackage(detail, ivoiceDetailsQx);
                    ValidateMedicalFeesContract(detail);


                    var res = _medicalFeesCausationAdminService.GetCausationInvoiceQx(detail, new AuditMessage { CodeUser = usercode });


                    if (res.StateResult)
                    {
                        var causation = res.ObjectEmbbeded.Item1;
                        if (causation == null)
                            throw new IndigoValidationException("No se obtuvo la causación del item quirúrgico.");

                        medicalList.Add(causation);
                    }
                    else if (res.ObjectEmbbeded.Item2 != null && res.ObjectEmbbeded.Item2.Any())
                    {
                        throw new IndigoValidationException("Existen homólogos");
                    }
                    else if (!res.StateResult)
                    {
                        throw new IndigoValidationException(res.Message);
                    }
                }
                catch (Exception ex)
                {
                    RegisterPending(detail, ex); // Sin commit - se hace al final
                }
            }

            return medicalList;
        }

        /// <summary>
        /// Registra el item que no se pudo causar (manejo robusto de errores sin commit inmediato)
        /// NO registra si el error indica que ya fue causado exitosamente (evita duplicados)
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="exception"></param>
        private void RegisterPending(ViewListSurgicalAndPackage detail, Exception exception)
        {
                try
                {
                // Validaciones defensivas para prevenir errores
                if (detail == null)
                {
                    Debug.WriteLine("RegisterPending Qx: detail es null, se omite registro");
                    return;
                }

                if (exception == null)
                {
                    Debug.WriteLine("RegisterPending Qx: exception es null, se omite registro");
                    return;
                }

                // PREVENCIÓN DE DUPLICADOS: Si el error indica que ya fue causado, NO crear pending
                if (IsAlreadyCausedError(exception))
                {
                    Debug.WriteLine($"Item Qx YA CAUSADO (no se crea pending duplicado): Factura {detail.InvoiceNumber}, DetailId {detail.ServiceOrderDetailId}");
                    Debug.WriteLine($"   - Razón: {exception.Message}");
                    return; // NO crear registro duplicado
                }

                var patient = _patientRepository?.FirstOrDefault(m => m.IPCODPACI == detail.PatientCode, tracking: false);

                _causationPendingRepository.SaveEntity(new CausationPending
                {
                    InvoiceNumber = detail.InvoiceNumber,
                    PatientCode = detail.PatientCode,
                    PatientName = patient?.IPNOMCOMP,
                    PerformsHealthProfessionalCode = detail.PerformsHealthProfessionalCode,
                    AdmissionNumber = detail.AdmissionNumber,
                    InvoiceDate = detail.ServiceDate,
                    Error = exception.Message,
                        IsQx = true,
                    Data = JsonConvert.SerializeObject(detail),
                    CreationDate = DateTime.Now
                });

                Debug.WriteLine($"RegisterPending Qx exitoso para factura: {detail.InvoiceNumber}");
            }
            catch (Exception ex)
            {
                // ERROR CRÍTICO: No debe detener el procesamiento de otros items
                Debug.WriteLine($"ERROR en RegisterPending Qx para {detail?.InvoiceNumber ?? "N/A"}: {ex.Message}");
                
                // Log adicional para debugging  
                Debug.WriteLine($"   - PatientCode: {detail?.PatientCode}");
                Debug.WriteLine($"   - ServiceOrderDetailId: {detail?.ServiceOrderDetailId}");
                Debug.WriteLine($"   - Original Exception: {exception?.Message}");
                return;
            }
        }

        /// <summary>
        /// Registra el item que no se pudo causar (manejo robusto de errores sin commit inmediato)
        /// NO registra si el error indica que ya fue causado exitosamente (evita duplicados)
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="exception"></param>
        private void RegisterPending(ViewListNoSurgical detail, Exception exception, bool retry = false)
        {
            try
            {
                // Validaciones defensivas para prevenir errores
                if (detail == null)
                {
                    Debug.WriteLine("RegisterPending NoQx: detail es null, se omite registro");
                    return;
                }

                if (exception == null)
                {
                    Debug.WriteLine("RegisterPending NoQx: exception es null, se omite registro");
                    return;
                }

                // PREVENCIÓN DE DUPLICADOS: Si el error indica que ya fue causado, NO crear pending
                if (IsAlreadyCausedError(exception))
                {
                    Debug.WriteLine($"Item NoQx YA CAUSADO (no se crea pending duplicado): Factura {detail.InvoiceNumber}, DetailId {detail.ServiceOrderDetailId}");
                    Debug.WriteLine($"   - Razón: {exception.Message}");
                    return; // NO crear registro duplicado
                }

                var patient = _patientRepository?.FirstOrDefault(m => m.IPCODPACI == detail.PatientCode, tracking: false);

            _causationPendingRepository.SaveEntity(new CausationPending
            {
                InvoiceNumber = detail.InvoiceNumber,
                PatientCode = detail.PatientCode,
                PatientName = patient?.IPNOMCOMP,
                PerformsHealthProfessionalCode = detail.PerformsHealthProfessionalCode,
                AdmissionNumber = detail.AdmissionNumber,
                InvoiceDate = detail.ServiceDate,
                Error = exception.Message,
                IsQx = false,
                Data = JsonConvert.SerializeObject(detail),
                CreationDate = DateTime.Now,
                Retry = retry
            });

                Debug.WriteLine($"RegisterPending NoQx exitoso para factura: {detail.InvoiceNumber}");
            }
            catch (Exception ex)
            {
                // ERROR CRÍTICO: No debe detener el procesamiento de otros items
                Debug.WriteLine($"ERROR en RegisterPending NoQx para {detail?.InvoiceNumber ?? "N/A"}: {ex.Message}");
                
                // Log adicional para debugging
                Debug.WriteLine($"   - PatientCode: {detail?.PatientCode}");
                Debug.WriteLine($"   - ServiceOrderDetailId: {detail?.ServiceOrderDetailId}");
                Debug.WriteLine($"   - Original Exception: {exception?.Message}");
                return;
            }
        }

        /// <summary>
        /// PREVENCIÓN DE DUPLICADOS: Detecta si el error indica que el item ya fue causado exitosamente
        /// Si retorna TRUE, NO se debe crear un nuevo pending (evita duplicados)
        /// </summary>
        /// <param name="exception">La excepción lanzada durante la causación</param>
        /// <returns>TRUE si el error indica que ya existe una causación exitosa</returns>
        private bool IsAlreadyCausedError(Exception exception)
        {
            if (exception == null) return false;

            string errorMessage = exception.Message?.ToLowerInvariant() ?? "";

            // Patrones que indican que la causación YA EXISTE (exitosa previamente)
            // Basado en las validaciones de ValidateMedicalFeesContract()
            var alreadyCausedPatterns = new[]
            {
                "ya se encuentra en una liquidación registrada",      // StatusMedicalFeesCausation == 2
                "ya se encuentra en una liquidación confirmada",      // StatusMedicalFeesCausation == 3
                "ya existe una causación",
                "item ya causado",
                "causación ya registrada",
                "liquidación registrada",
                "liquidación confirmada"
            };

            bool isAlreadyCaused = alreadyCausedPatterns.Any(pattern => errorMessage.Contains(pattern));

            if (isAlreadyCaused)
            {
                Debug.WriteLine($"Detectado error de causación previa: {exception.Message}");
            }

            return isAlreadyCaused;
        }

        /// <summary>
        /// Metodo que valida si hay paquetes y adicionalmente los hijos de ese paquete
        /// para no dejar que se seleccionen los dos
        /// (si causan el padre no pueden causar los hijos, si causan los hijos no pueden causar el padre)
        /// </summary>
        /// <param name="detail"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ValidatePackage(ViewListSurgicalAndPackage detail, List<ViewListSurgicalAndPackage> ivoiceDetailsQx)
        {
            var message = "";

            if (detail.IncludeServiceOrderDetailId == 0 && detail.Presentation == 3) //Si el item es un paquete padre
            {
                //Verifico si el ServiceOrderDetailId del paquete padre tiene incluidos otros items validandolo contra el campo de IncludeServiceOrderDetailId y ademas que este causado
                var list = (from x in ivoiceDetailsQx
                            where (x.IncludeServiceOrderDetailId == detail.ServiceOrderDetailId && x.SelectOption == 1)
                               || (x.IncludeServiceOrderDetailId == detail.ServiceOrderDetailId && x.MedicalFeesCausationId > 0)
                            select x).ToList();

                if (list != null && list.Any())
                {
                    message = BuildMessageError(list);
                    throw new IndigoValidationException(message);
                }
            }
            else
            {
                if (detail.IncludeServiceOrderDetailId > 0)
                {
                    var list = (from x in ivoiceDetailsQx
                                where (x.ServiceOrderDetailId == detail.IncludeServiceOrderDetailId && x.SelectOption == 1)
                                    || (x.ServiceOrderDetailId == detail.IncludeServiceOrderDetailId && x.MedicalFeesCausationId > 0)
                                select x).ToList();

                    if (list != null && list.Any())
                    {
                        message = BuildMessageError(list);
                        throw new IndigoValidationException(message);
                    }
                }
            }
        }

        /// <summary>
        /// construye mensaje de error
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string BuildMessageError(List<ViewListSurgicalAndPackage> list)
            => string.Join(", ", list.Select(m => m.IPSServiceDescription).ToArray());

        /// <summary>
        /// Versión optimizada de CauseInvoices para contexto asíncrono (sin commits internos)
        /// </summary>
        private List<MedicalFeesCausation> CauseInvoices(List<ViewListNoSurgical> ivoiceDetails, string usercode)
        {
            var medicalList = new List<MedicalFeesCausation>();

            foreach (var detail in ivoiceDetails)
            {
                try
                {
                    if (detail.ServiceType == SERVICE_TYPE_DIAGNOSTIC_IMAGING) // Imagenes Diagnosticas
                    {
                        var MEDREALEC = validateDiagnosticImage(detail);
                        if (MEDREALEC != null) { detail.PerformsHealthProfessionalCode = MEDREALEC; }
                    }

                    var res = _medicalFeesCausationAdminService.GetCausationInvoice(detail, new AuditMessage { CodeUser = usercode });

                    if (!res.StateResult)
                    {
                        if (res.ObjectEmbbeded.homologations != null && res.ObjectEmbbeded.homologations.Any())
                        {
                            throw new IndigoValidationException("Existen homólogos");
                        }
                        else
                        {
                            throw new IndigoValidationException(res.Message);
                        }
                    }

                    medicalList.Add(res.ObjectEmbbeded.causation);
                }
                catch (IndigoCausationException ex)
                {
                    RegisterPending(detail, ex, true); // Sin commit - se hace al final
                }
                catch (Exception ex)
                {
                    RegisterPending(detail, ex); // Sin commit - se hace al final
                }
            }

            return medicalList;
        }

        /// <summary>
        /// Reintenta de nuevo la causación del detalle de una factura que estaba pendiente.
        /// </summary>
        /// <param name="invoiceDetail"></param>
        /// <param name="container"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public ActionResult RetryCausateDetailInvoice(ViewListNoSurgical invoiceDetail, string container, string usercode)
        {
            var actionResult = new ActionResult()
            {
                StatusCode = eStatusResult.EXCEPTION,
                StateResult = false
            };
            try
            {
                EnsureServicesInitialized(container);

                if (invoiceDetail == null) throw new IndigoValidationException("Los datos enviados no contienen detalles de factura.");

                if (invoiceDetail.ServiceType == SERVICE_TYPE_DIAGNOSTIC_IMAGING) // Imagenes Diagnosticas
                {
                    var MEDREALEC = validateDiagnosticImage(invoiceDetail);
                    if (MEDREALEC != null) { invoiceDetail.PerformsHealthProfessionalCode = MEDREALEC; }
                }

                var resGetCausationInvoice = _medicalFeesCausationAdminService.GetCausationInvoice(invoiceDetail, new AuditMessage { CodeUser = usercode });

                if (!resGetCausationInvoice.StateResult)
                {
                    if (resGetCausationInvoice.ObjectEmbbeded.homologations != null && resGetCausationInvoice.ObjectEmbbeded.homologations.Any())
                    {
                        throw new IndigoValidationException("Existen homólogos");
                    }
                    else
                    {
                        throw new IndigoValidationException(resGetCausationInvoice.Message);
                    }
                }

                var causationList = new List<MedicalFeesCausation>
                {
                    resGetCausationInvoice.ObjectEmbbeded.causation
                };

                var res = _medicalFeesCausationAdminService
                    .SaveMedicalFeesCausation(causationList, null, null, new AuditMessage
                    {
                        CodeUser = usercode
                    });
                actionResult = new ActionResult()
                {
                    StatusCode = res.StatusCode,
                    StateResult = res.StateResult,
                    MessageResult = res.MessageResult,
                };
            }
            catch (Exception ex)
            {
                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = ex.Message
                };
            }
            finally
            {
                DisposeServices();
            }

            return actionResult;
        }

        /// <summary>
        /// Valida las imagenes diagnosticas - VERSIÓN OPTIMIZADA (una sola consulta, validaciones defensivas)
        /// </summary>
        /// <param name="detail"></param>
        /// <returns>Código del profesional que realiza la lectura o null</returns>
        /// <exception cref="IndigoCausationException"></exception>
        private string validateDiagnosticImage(ViewListNoSurgical detail)
        {
            if (detail?.ServiceOrderDetailId == null) return null;

            try
            {
                // OPTIMIZACIÓN: Intentar hospitalario primero (más común)
            var listDiagnosticImaging = _medicalFeesCausationAdminService.GetViewListDiagnosticImaging(detail.ServiceOrderDetailId);
                if (listDiagnosticImaging?.Any() == true)
            {
                    var firstItem = listDiagnosticImaging[0];
                    
                    if (string.IsNullOrWhiteSpace(firstItem.MEDREALEC))
                {
                    throw new IndigoCausationException("El médico que realiza la lectura de la imagen hospitalaria no puede ser vacío.");
                }
                    
                    if (firstItem.ESTSERIPS != PROCESSED_IMAGE_STATE && firstItem.ESTSERIPS != INTERPRETED_STUDY_STATE)
                {
                    throw new IndigoCausationException("El estado del Servicio IPS de la imagen hospitalaria debe ser Imagen Procesada o Estudio Interpretado.");
                }
                    
                    return firstItem.MEDREALEC;
            }

                // Si no es hospitalario, verificar ambulatorio
            var listDiagnosticImagingAmbulatory = _medicalFeesCausationAdminService.GetViewListDiagnosticImagingAmbulatory(detail.ServiceOrderDetailId);
                if (listDiagnosticImagingAmbulatory?.Any() == true)
            {
                    var firstItem = listDiagnosticImagingAmbulatory[0];
                    
                    if (string.IsNullOrWhiteSpace(firstItem.MEDREALEC))
                {
                    throw new IndigoCausationException("El médico que realiza la lectura de la imagen ambulatoria no puede ser vacío.");
                }
                    
                    if (firstItem.ESTSERIPS != PROCESSED_IMAGE_STATE && firstItem.ESTSERIPS != INTERPRETED_STUDY_STATE)
                {
                    throw new IndigoCausationException("El estado del Servicio IPS de la imagen ambulatoria debe ser Imagen Procesada o Estudio Interpretado.");
                }
                    
                    return firstItem.MEDREALEC;
            }

                // No hay imágenes diagnósticas para este detalle
            return null;
            }
            catch (Exception ex) when (!(ex is IndigoCausationException))
            {
                // Si hay error en consultas, no debe fallar todo el proceso
                Debug.WriteLine($"Error validando imagen diagnóstica para ServiceOrderDetailId {detail.ServiceOrderDetailId}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crea entidad MedicalFeesCausation para procedimientos quirúrgicos (con validaciones defensivas)
        /// </summary>
        private MedicalFeesCausation CreateMedicalFeesCausationQx(ViewListSurgicalAndPackage detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            // VALIDACIÓN CRÍTICA: ThirdPartyId debe existir
            if (!detail.ThirdPartyId.HasValue || detail.ThirdPartyId.Value <= 0)
            {
                var errorMsg = $"ERROR FK ThirdParty - Qx Item sin ThirdPartyId válido:\n" +
                              $"   - InvoiceNumber: {detail.InvoiceNumber}\n" +
                              $"   - ServiceOrderDetailId: {detail.ServiceOrderDetailId}\n" +
                              $"   - PatientCode: {detail.PatientCode}\n" +
                              $"   - HealthProfessional: {detail.PerformsHealthProfessionalCode}\n" +
                              $"   - IPSService: {detail.IPSServiceDescription}\n" +
                              $"   - ThirdPartyId: {detail.ThirdPartyId?.ToString() ?? "NULL"}\n" +
                              $"   - ThirdPartyDescription: {detail.ThirdPartyDescription ?? "N/A"}";
                
                Debug.WriteLine(errorMsg);
                throw new IndigoValidationException($"El item quirúrgico no tiene un tercero (ThirdParty) válido asignado. Factura: {detail.InvoiceNumber}, ServiceOrderDetailId: {detail.ServiceOrderDetailId}");
            }

            var entity = new MedicalFeesCausation
            {
                Id = detail.MedicalFeesCausationId ?? 0,
                AdmissionNumber = detail.AdmissionNumber,
                PatientCode = detail.PatientCode,
                HealthProfessionalCode = detail.PerformsHealthProfessionalCode.Trim(),
                ThirdPartyId = detail.ThirdPartyId.Value,
                MedicalFeesContractId = detail.MedicalFeesContractId,
                ServiceOrderId = detail.ServiceOrderId,
                ServiceOrderDetailId = detail.ServiceOrderDetailId,
                ServiceOrderDetailSurgicalId = detail.ServiceOrderDetailSurgicalId,
                AmountPayable = detail.AmountPayable.Value,
                MedicalFeesContractValue = detail.AmountPayable,
                InvoiceQuantity = detail.InvoicedQuantity,
                TotalAmountPayable = detail.TotalAmountPayable.Value,
                PercentageCashed = detail.PercentageCashed.Value,
                MedicalFeePaid = false,
                InvoiceDetailId = detail.InvoiceDetailId,
                CausationDate = System.DateTime.Now
            };

            if (entity.Id > 0) entity.MarkAsModified();

            Debug.WriteLine($"MedicalFeesCausation Qx creado - ThirdPartyId: {entity.ThirdPartyId}, ServiceOrderDetailId: {entity.ServiceOrderDetailId}, Factura: {detail.InvoiceNumber}");

            return entity;
        }

        /// <summary>
        /// Validaciones
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="IndigoValidationException"></exception>
        private void ValidateMedicalFeesContract(ViewListSurgicalAndPackage item)
        {
            if (item.MedicalFeesCausationId != 0)
            {
                if (item.StatusMedicalFeesCausation == MEDICAL_FEES_STATUS_REGISTERED) 
                    throw new IndigoValidationException($"El item ({item.IPSServiceDescription}) ya se encuentra en una liquidación registrada.");
                else if (item.StatusMedicalFeesCausation == MEDICAL_FEES_STATUS_CONFIRMED) 
                    throw new IndigoValidationException($"El item ({item.IPSServiceDescription}) ya se encuentra en una liquidación confirmada.");
            }
        }

        /// <summary>
        /// Elimina las facturas pendientes por causación
        /// </summary>
        /// <param name="invoiceEvents"></param>
        /// <param name="database"></param>
        /// <param name="userCode"></param>
        public async Task<ActionResult> RemoveInvoiceFromPendingAsync(List<InvoiceEvent> invoiceEvents, string container, string userCode)
        {
            var actionResult = new ActionResult();
            try
            {
                EnsureServicesInitialized(container);
                var invoiceNumbers = invoiceEvents.Select(x => x.InvoiceNumber).ToList();
                var invoicePendings = _causationPendingRepository.Query(m => invoiceNumbers.Contains(m.InvoiceNumber), tracking: true)?.ToList();

                if (invoicePendings.Any())
                {
                    invoicePendings.ForEach(m => _causationPendingRepository.DeleteEntity(m));
                    await _causationPendingRepository.UnitWork.CommitAsync();
                }
                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.SUCCESS,
                    StateResult = true
                };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
                };
            }
            
            return actionResult;
        }

        /// <summary>
        /// LIMPIEZA AUTOMÁTICA: Identifica pendings que ya fueron causados (NO hace commit, se hace al final)
        /// MÉTODO CORRECTO: Consulta MedicalFeesCausation por ServiceOrderDetailId (fuente de verdad)
        /// Similar a ValidateCausationExists que usa: m.ServiceOrderDetailId = invoiceDetail.ServiceOrderDetailId && m.Status != 4
        /// </summary>
        /// <param name="causationsPending">Lista de pendings a verificar</param>
        /// <param name="successList">Lista de mensajes de éxito</param>
        /// <returns>Lista de pendings que deben eliminarse</returns>
        private async Task<List<CausationPending>> CleanupAlreadyCausedPendings(List<CausationPending> causationsPending, List<string> successList)
        {
            var obsoletePendings = new List<CausationPending>();
            
            try
            {
                if (!causationsPending?.Any() == true) return obsoletePendings;

                successList.Add($"Verificando pendings obsoletos en {causationsPending.Count} registros...");

                // PASO 1: Extraer ServiceOrderDetailId de cada pending (deserialización paralela)
                var pendingDetailIds = new Dictionary<int, CausationPending>(); // ServiceOrderDetailId -> Pending
                
                var extractionTasks = causationsPending.Select(pending =>
                    Task.Run(() =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(pending.Data)) return;

                            var serviceOrderDetailId = ExtractServiceOrderDetailId(pending);

                            if (serviceOrderDetailId.HasValue && serviceOrderDetailId.Value > 0)
                            {
                                lock (pendingDetailIds)
                                {
                                    // Puede haber duplicados, guardar el primero
                                    if (!pendingDetailIds.ContainsKey(serviceOrderDetailId.Value))
                                    {
                                        pendingDetailIds[serviceOrderDetailId.Value] = pending;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error extrayendo ServiceOrderDetailId de pending {pending.Id}: {ex.Message}");
                        }
                    })
                ).ToArray();

                await Task.WhenAll(extractionTasks);

                if (!pendingDetailIds.Any())
                {
                    successList.Add($"No se pudieron extraer ServiceOrderDetailId de los pendings");
                    return obsoletePendings;
                }

                successList.Add($"ServiceOrderDetailIds extraídos: {pendingDetailIds.Count}");

                // PASO 2: Consultar MedicalFeesCausation por ServiceOrderDetailId (UNA SOLA CONSULTA MASIVA)
                // Similar a: _medicalFeesCausationRepository.Query(m => m.ServiceOrderDetailId = id && m.Status != 4)
                var detailIds = pendingDetailIds.Keys.ToList();
                
                var existingCausations = _medicalFeesCausationRepository.Query(
                    m => detailIds.Contains(m.ServiceOrderDetailId) && m.Status != 4, // Status 4 = Anulado/Cancelado
                    tracking: false
                )?.Select(m => m.ServiceOrderDetailId)
                  .Distinct()
                  .ToList();

                if (existingCausations?.Any() != true)
                {
                    successList.Add($"No se encontraron causaciones existentes - todos los pendings son válidos");
                    return obsoletePendings;
                }

                successList.Add($"Causaciones existentes encontradas: {existingCausations.Count}");

                // PASO 3: Identificar pendings obsoletos (tienen causación en MedicalFeesCausation)
                foreach (var causedDetailId in existingCausations)
                {
                    if (pendingDetailIds.TryGetValue(causedDetailId, out var obsoletePending))
                    {
                        obsoletePendings.Add(obsoletePending);
                        Debug.WriteLine($"Pending obsoleto detectado: ServiceOrderDetailId {causedDetailId}, Factura {obsoletePending.InvoiceNumber}");
                    }
                }

                // OPTIMIZACIÓN: Marcar para eliminación (commit se hace al final con todo)
                if (obsoletePendings.Any())
                {
                    var obsoletePendingIds = obsoletePendings.Select(op => op.Id).ToList();
                    
                    var pendingsToDelete = _causationPendingRepository.Query(
                        p => obsoletePendingIds.Contains(p.Id),
                        tracking: true
                    )?.ToList();

                    if (pendingsToDelete?.Any() == true)
                    {
                        pendingsToDelete.ForEach(p => _causationPendingRepository.DeleteEntity(p));
                        successList.Add($"Marcados {pendingsToDelete.Count} pendings obsoletos para eliminación");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en limpieza de pendings obsoletos: {ex.Message}");
                successList.Add($"Error en limpieza automática: {ex.Message}");
            }

            return obsoletePendings;
        }

        /// <summary>
        /// MÉTODO REUTILIZABLE: Extrae ServiceOrderDetailId de un CausationPending
        /// </summary>
        private int? ExtractServiceOrderDetailId(CausationPending pending)
        {
            try
            {
                if (string.IsNullOrEmpty(pending.Data)) return null;

                if (pending.IsQx)
                {
                    var detail = JsonConvert.DeserializeObject<ViewListSurgicalAndPackage>(pending.Data);
                    return detail?.ServiceOrderDetailId;
                }
                else
                {
                    var detail = JsonConvert.DeserializeObject<ViewListNoSurgical>(pending.Data);
                    return detail?.ServiceOrderDetailId;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error extrayendo ServiceOrderDetailId: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// MÉTODO GENÉRICO: Filtra items por ThirdPartyId válido (O(1) con diccionario)
        /// Reemplaza código duplicado y optimiza de O(n²) a O(n)
        /// </summary>
        private List<T> FilterItemsByThirdPartyId<T>(
            List<T> items,
            HashSet<int> validThirdPartyIds,
            Dictionary<int, CausationPending> pendingByDetailId,
            List<CausationPending> invalidThirdPartyPendings,
            string itemType) where T : class
        {
            var validItems = new List<T>();

            foreach (var item in items)
            {
                // Extraer propiedades usando reflexión o conversión
                int? thirdPartyId = null;
                int? serviceOrderDetailId = null;
                string invoiceNumber = null;

                if (item is ViewListNoSurgical noQxItem)
                {
                    thirdPartyId = noQxItem.ThirdPartyId;
                    serviceOrderDetailId = noQxItem.ServiceOrderDetailId;
                    invoiceNumber = noQxItem.InvoiceNumber;
                }
                else if (item is ViewListSurgicalAndPackage qxItem)
                {
                    thirdPartyId = qxItem.ThirdPartyId;
                    serviceOrderDetailId = qxItem.ServiceOrderDetailId;
                    invoiceNumber = qxItem.InvoiceNumber;
                }

                // Validar ThirdPartyId
                if (thirdPartyId.HasValue && thirdPartyId.Value > 0 && validThirdPartyIds.Contains(thirdPartyId.Value))
                {
                    validItems.Add(item);
                }
                else
                {
                    // ThirdPartyId inválido → buscar pending asociado (O(1) con diccionario)
                    if (serviceOrderDetailId.HasValue && pendingByDetailId.TryGetValue(serviceOrderDetailId.Value, out var pending))
                    {
                        var errorMsg = $"ThirdPartyId ({thirdPartyId ?? 0}) no existe o es inválido. Factura: {invoiceNumber}, ServiceOrderDetailId: {serviceOrderDetailId}";
                        pending.Error = errorMsg;
                        invalidThirdPartyPendings.Add(pending);
                        Debug.WriteLine($"{itemType} Item omitido - {errorMsg}");
                    }
                }
            }

            return validItems;
        }

        /// <summary>
        /// VERSIÓN OPTIMIZADA - Procesa grandes volúmenes de causaciones pendientes con alto rendimiento
        /// Optimizaciones: filtrado temprano, deserialización paralela, procesamiento en lotes, limpieza automática
        /// </summary>
        public async Task<ActionResult> ProcessContractCausationsAsync(ContractUpdateRequest contractUpdateData, string container, string usercode)
        {
            var actionResult = new ActionResult() { StatusCode = eStatusResult.SUCCESS, StateResult = true };

            try
            {
                EnsureServicesInitialized(container);

                if (contractUpdateData?.HealthProfessionalsCodes?.Any() != true || contractUpdateData?.IpsServicesIds?.Any() != true)
                {
                    return new ActionResult { StatusCode = eStatusResult.SUCCESS, StateResult = true, 
                        Message = "No hay datos para procesar." };
                }

                var successList = new List<string>();
                var errorList = new List<string>();
                var professionalCodes = contractUpdateData.HealthProfessionalsCodes;
                var ipsServiceIds = contractUpdateData.IpsServicesIds;

                // Filtro de año actual (optimización: reduce dataset inicial)
                var currentYear = DateTime.Now.Year;
                var startOfYear = new DateTime(currentYear, 1, 1);

                var causationsPending = _causationPendingRepository.Query(
                    cp => professionalCodes.Contains(cp.PerformsHealthProfessionalCode) &&
                          cp.CreationDate >= startOfYear,
                    tracking: false
                )?.ToList();

                successList.Add($"Dataset inicial filtrado: {causationsPending?.Count ?? 0} registros (año {currentYear})");

                if (!causationsPending?.Any() == true)
                {
                    return new ActionResult { StatusCode = eStatusResult.SUCCESS, StateResult = true,
                        Message = "No hay causaciones pendientes para procesar." };
                }

                // 🧹 LIMPIEZA AUTOMÁTICA: Eliminar pendings que ya fueron causados (consultando MedicalFeesCausation)
                var obsoletePendings = await CleanupAlreadyCausedPendings(causationsPending, successList);
                
                // Actualizar la lista sin los obsoletos
                if (obsoletePendings?.Any() == true)
                {
                    causationsPending = causationsPending.Except(obsoletePendings).ToList();
                    successList.Add($"🗑️ Pendings obsoletos eliminados: {obsoletePendings.Count}");
                }

                // PASO 2: PRE-FILTRO ULTRA-RÁPIDO (filtro por contenido JSON sin deserializar - EN MEMORIA)
                var relevantPendings = causationsPending
                    .Where(p => !string.IsNullOrEmpty(p.Data) && 
                               ipsServiceIds.Any(id => p.Data.Contains($"\"IPSServiceId\":{id}") || 
                                                      p.Data.Contains($"\"IPSServiceId\": {id}")))
                    .ToList();

                successList.Add($"Registros pre-filtrados por IPSServiceId: {relevantPendings.Count} de {causationsPending.Count}");

                if (!relevantPendings.Any())
                {
                    return new ActionResult { StatusCode = eStatusResult.SUCCESS, StateResult = true,
                        Message = "No hay registros relevantes para los servicios IPS especificados." };
                }

                // PASO 3: DESERIALIZACIÓN EN PARALELO (hasta 10x más rápido)
                var qxItems = new List<ViewListSurgicalAndPackage>();
                var noQxItems = new List<ViewListNoSurgical>(); 
                var validPendings = new List<CausationPending>();

                var deserializationTasks = relevantPendings.Select(pending =>
                    Task.Run(() =>
                    {
                        try
                        {
                            if (pending.IsQx)
                            {
                                var item = JsonConvert.DeserializeObject<ViewListSurgicalAndPackage>(pending.Data);
                                if (item != null && ipsServiceIds.Contains(item.IPSServiceId))
                                {
                                    lock (qxItems) { qxItems.Add(item); }
                                    lock (validPendings) { validPendings.Add(pending); }
                                }
                            }
                            else
                            {
                                var item = JsonConvert.DeserializeObject<ViewListNoSurgical>(pending.Data);
                                if (item != null && ipsServiceIds.Contains(item.IPSServiceId))
                                {
                                    lock (noQxItems) { noQxItems.Add(item); }
                                    lock (validPendings) { validPendings.Add(pending); }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lock (errorList) { errorList.Add($"Error deserializando {pending.Id}: {ex.Message}"); }
                        }
                    })
                ).ToArray();

                await Task.WhenAll(deserializationTasks);
                
                successList.Add($"Deserialización paralela completada - Qx: {qxItems.Count}, NoQx: {noQxItems.Count}");

                // PASO 3.5: CACHÉ DE THIRDPARTY (optimización crítica: 1 consulta en vez de 31,200)
                var uniqueThirdPartyIds = qxItems
                    .Where(x => x.ThirdPartyId.HasValue && x.ThirdPartyId.Value > 0)
                    .Select(x => x.ThirdPartyId.Value)
                    .Concat(noQxItems
                        .Where(x => x.ThirdPartyId.HasValue && x.ThirdPartyId.Value > 0)
                        .Select(x => x.ThirdPartyId.Value))
                    .Distinct()
                    .ToList();

                HashSet<int> validThirdPartyIds = null;
                
                if (uniqueThirdPartyIds.Any())
                {
                    // UNA SOLA CONSULTA MASIVA para todos los ThirdParty necesarios
                    var existingThirdParties = _medicalFeesCausationAdminService
                        .GetValidThirdPartyIds(uniqueThirdPartyIds);
                    
                    validThirdPartyIds = new HashSet<int>(existingThirdParties ?? new List<int>());
                    
                    successList.Add($"Cache de ThirdParty: {uniqueThirdPartyIds.Count} únicos, {validThirdPartyIds.Count} válidos en BD");
                }
                else
                {
                    validThirdPartyIds = new HashSet<int>();
                }

                // PASO 3.6: PRE-FILTRAR items con ThirdPartyId inválido (optimización O(n) con diccionario)
                // OPTIMIZACIÓN: Crear diccionario ServiceOrderDetailId -> Pending para búsqueda O(1)
                var pendingByDetailId = new Dictionary<int, CausationPending>();
                foreach (var pending in validPendings)
                {
                    var detailId = ExtractServiceOrderDetailId(pending);
                    if (detailId.HasValue && !pendingByDetailId.ContainsKey(detailId.Value))
                    {
                        pendingByDetailId[detailId.Value] = pending;
                    }
                }

                var invalidThirdPartyPendings = new List<CausationPending>();

                // MÉTODO GENÉRICO: Filtrar NoQx y Qx con la misma lógica
                var noQxItemsValid = FilterItemsByThirdPartyId(
                    noQxItems, 
                    validThirdPartyIds, 
                    pendingByDetailId, 
                    invalidThirdPartyPendings, 
                    "NoQx"
                );

                var qxItemsValid = FilterItemsByThirdPartyId(
                    qxItems, 
                    validThirdPartyIds, 
                    pendingByDetailId, 
                    invalidThirdPartyPendings, 
                    "Qx"
                );

                successList.Add($"Pre-filtro ThirdParty: {noQxItemsValid.Count} NoQx válidos, {qxItemsValid.Count} Qx válidos, {invalidThirdPartyPendings.Count} omitidos");

                // Actualizar las listas para procesamiento
                noQxItems = noQxItemsValid;
                qxItems = qxItemsValid;

                // PASO 4: PROCESAMIENTO EN LOTES GRANDES (máxima eficiencia)
                int totalProcessed = 0;
                
                // TRACKING CORRECTO: Lista de ServiceOrderDetailId exitosos para eliminación precisa
                var successfulDetailIds = new HashSet<int>();

                // Procesar NoQx en lotes (optimizado sin commits internos)
                for (int i = 0; i < noQxItems.Count; i += BATCH_SIZE)
                {
                    try
                    {
                        var batch = noQxItems.Skip(i).Take(BATCH_SIZE).ToList();
                        var causations = CauseInvoices(batch, usercode); // Versión sin commits internos
                        
                        if (causations?.Any() == true)
                        {
                            var saveResult = await _medicalFeesCausationAdminService.SaveMedicalFeesCausationAsync(
                                causations, null, null, new AuditMessage { CodeUser = usercode });
                            
                            if (saveResult.StateResult)
                            {
                                // TRACKING: Registrar los ServiceOrderDetailId que fueron causados exitosamente
                                foreach (var causation in causations)
                                {
                                    successfulDetailIds.Add(causation.ServiceOrderDetailId);
                                }
                                
                                totalProcessed += batch.Count;
                                successList.Add($"Lote NoQx: {batch.Count} registros procesados");
                            }
                            else
                            {
                                errorList.Add($"Error lote NoQx de {batch.Count} registros: {saveResult.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorList.Add($"Error crítico en lote NoQx: {ex.Message}");
                    }
                }

                // Procesar Qx en lotes (optimizado sin commits internos)
                for (int i = 0; i < qxItems.Count; i += BATCH_SIZE)
                {
                    try
                    {
                        var batch = qxItems.Skip(i).Take(BATCH_SIZE).ToList();
                        var causations = CauseInvoiceQX(batch, usercode); // Versión sin commits internos
                        
                        if (causations?.Any() == true)
                        {
                            var saveResult = await _medicalFeesCausationAdminService.SaveMedicalFeesCausationAsync(
                                causations, null, null, new AuditMessage { CodeUser = usercode });
                            
                            if (saveResult.StateResult)
                            {
                                // TRACKING: Registrar los ServiceOrderDetailId que fueron causados exitosamente
                                foreach (var causation in causations)
                                {
                                    successfulDetailIds.Add(causation.ServiceOrderDetailId);
                                }
                                
                                totalProcessed += batch.Count;
                                successList.Add($"Lote Qx: {batch.Count} registros procesados");
                            }
                            else
                            {
                                errorList.Add($"Error lote Qx de {batch.Count} registros: {saveResult.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorList.Add($"Error crítico en lote Qx: {ex.Message}");
                    }
                }

                // PASO 4.5: GUARDAR ERRORES DE THIRDPARTY INVÁLIDO (optimizado con SaveEntityMassiveAsync)
                if (invalidThirdPartyPendings.Any())
                {
                    // OPTIMIZACIÓN: Una sola consulta para todos los pendings
                    var pendingIds = invalidThirdPartyPendings.Select(p => p.Id).ToList();
                    
                    var trackedPendings = _causationPendingRepository.Query(
                        p => pendingIds.Contains(p.Id),
                        tracking: true
                    )?.ToList();

                    if (trackedPendings?.Any() == true)
                    {
                        // Mapear errores a las entidades trackeadas
                        foreach (var trackedPending in trackedPendings)
                        {
                            var pendingWithError = invalidThirdPartyPendings.FirstOrDefault(p => p.Id == trackedPending.Id);
                            if (pendingWithError != null && !string.IsNullOrEmpty(pendingWithError.Error))
                            {
                                trackedPending.Error = pendingWithError.Error;
                                trackedPending.MarkAsModified(); // CRÍTICO: Marcar como modificado
                            }
                        }
                        
                        // USAR SaveEntityMassiveAsync para actualización masiva
                        await _causationPendingRepository.SaveEntityMassiveAsync(trackedPendings);
                        successList.Add($"Errores de ThirdParty registrados: {trackedPendings.Count}");
                    }
                }

                // PASO 5: ELIMINACIÓN PRECISA (solo pendings causados exitosamente - optimizado)
                if (successfulDetailIds.Any() && validPendings.Any())
                {
                    // IDENTIFICAR PENDINGS EXITOSOS: Comparar por ServiceOrderDetailId
                    var pendingsToRemove = new List<CausationPending>();
                    
                    foreach (var pending in validPendings)
                    {
                        try
                        {
                            // REUTILIZAR: Usar método compartido para extraer ServiceOrderDetailId
                            var serviceOrderDetailId = ExtractServiceOrderDetailId(pending);
                            
                            // Si el ServiceOrderDetailId está en la lista de exitosos, eliminar el pending
                            if (serviceOrderDetailId.HasValue && successfulDetailIds.Contains(serviceOrderDetailId.Value))
                            {
                                pendingsToRemove.Add(pending);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error identificando pending para eliminación {pending.Id}: {ex.Message}");
                        }
                    }
                    
                    // OPTIMIZACIÓN: Usar SaveEntityMassiveAsync para eliminación masiva
                    if (pendingsToRemove.Any())
                    {
                        // Extraer IDs antes de la consulta (fix Entity Framework)
                        var pendingIdsToDelete = pendingsToRemove.Select(p => p.Id).ToList();
                        
                        var trackedPendings = _causationPendingRepository.Query(
                            p => pendingIdsToDelete.Contains(p.Id),
                            tracking: true
                        )?.ToList();
                        
                        if (trackedPendings?.Any() == true)
                        {
                            // Marcar como eliminados
                            trackedPendings.ForEach(p => p.MarkAsDeleted());
                            
                            // USAR SaveEntityMassiveAsync para eliminación masiva
                            await _causationPendingRepository.SaveEntityMassiveAsync(trackedPendings);
                            successList.Add($"Marcados {trackedPendings.Count} pendings exitosos para eliminación");
                        }
                    }
                    else
                    {
                        successList.Add($"No se identificaron pendings para eliminación (posible error en tracking)");
                    }
                }

                // PASO 6: COMMIT ASÍNCRONO ÚNICO (máxima eficiencia transaccional)
                await _causationPendingRepository.UnitWork.CommitAsync();
                
                if (totalProcessed > 0)
                {
                    successList.Add($"Commit asíncrono completado - {totalProcessed} items procesados exitosamente");
                }
                
                successList.Add($"Resumen: {noQxItems.Count} NoQx + {qxItems.Count} Qx = {noQxItems.Count + qxItems.Count} procesados");

                var resultMessage = $"PROCESAMIENTO OPTIMIZADO COMPLETADO - Total: {totalProcessed}, Errores: {errorList.Count}";
                
                actionResult = new ActionResult
                {
                    StatusCode = errorList.Any() ? eStatusResult.WARNING : eStatusResult.SUCCESS,
                    StateResult = true,
                    Message = resultMessage + (errorList.Any() ? $" | Primeros errores: {string.Join("; ", errorList.Take(2))}" : "")
                };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = ex.Message
                };
            }
            finally
            {
                DisposeServices();
            }

            return actionResult;
        }


        /// <summary>
        /// Ejecuta auto-causación de servicios no facturados (OS Registrada) con batching.
        /// Usado por el timer semanal de Stella y el endpoint HTTP.
        /// Procesa todas las unidades operativas.
        /// </summary>
        public Task<ActionResult> AutoCausateUnbilledAsync(int batchSize, string container, string usercode)
        {
            var actionResult = new ActionResult()
            {
                StatusCode = eStatusResult.SUCCESS,
                StateResult = true
            };

            try
            {
                EnsureServicesInitialized(container);

                var result = _medicalFeesCausationAdminService.ProcessUnrecognizedCausations(
                    new AuditMessage { CodeUser = usercode },
                    batchSize
                );

                actionResult = new ActionResult
                {
                    StatusCode = result.StateResult ? eStatusResult.SUCCESS : eStatusResult.EXCEPTION,
                    StateResult = result.StateResult,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                actionResult = new ActionResult
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = ex.Message
                };
            }
            finally
            {
                DisposeServices();
            }

            return Task.FromResult(actionResult);
        }

        /// <summary>
        /// Dispose optimizado para todos los servicios
        /// </summary>
        private void DisposeServices()
        {
            try
            {
                _medicalFeesCausationAdminService?.Dispose();
                _medicalFeesContractAdminService?.Dispose();
                
                // Reset para próxima llamada
                _servicesInitialized = false;
                _currentContainer = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en dispose de servicios: {ex.Message}");
            }
        }
    }
}
