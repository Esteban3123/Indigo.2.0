//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 07/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.ProductSubGroups;
using Domain.Entities;
using System.Data;
using Infrastructure.CrossCutting.Resources;
using System.Data.Entity.Infrastructure;
using Domain.Entities.Service;
using System.Transactions;
using System.ComponentModel;
//using System.Collections;
using Domain.Inventory.POCO;
using System.Threading;
using System.Collections.Concurrent;

namespace Application.Inventory.SettingInventory
{
    public class SettingInventoryAdminService : ISettingInventoryAdminService
    {
        #region Variables
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IInventoryService _inventoryServiceRepository;
        private IKardexRepository _kardexRepository;
        private IPhysicalInventoryRepository _PhysicalInventoryRepository;
        private IClosedMonthHeaderRepository _closedMonthHeaderRepository;
        private IClosedMonthInventoryRepository _closedMonthInventoryRepository;
        private IInventorySequenceRepository _inventorySequenseRepository;
        private List<SP_ConciliationInventoryVsAccounting_Result> ConcilationModulesData;
        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="settingInventoryRepository"></param>
        /// <param name="sequenseRepository"></param>IInventoryContractAdminService
        public SettingInventoryAdminService(ISettingInventoryRepository settingInventoryRepository,
            IInventorySequenceDetailRepository sequenseRepository, IInventoryService inventoryServiceRepository,
            IKardexRepository kardexRepository,
            IPhysicalInventoryRepository physicalInventoryRepository, IClosedMonthHeaderRepository closedMonthHeaderRepository,
            IClosedMonthInventoryRepository closedMonthInventoryRepository, IInventorySequenceRepository inventorySequenseRepository)
        {
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de settingInventoryRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (inventoryServiceRepository == null)
            {
                throw new ArgumentNullException("Repositorio de inventoryServiceRepository vacio");
            }
            if (kardexRepository == null)
            {
                throw new ArgumentNullException("Repositorio de kardexRepository vacio");
            }
            if (physicalInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de physicalInventoryRepository vacio");
            }
            if (closedMonthHeaderRepository == null)
            {
                throw new ArgumentNullException("Repositorio de closedMonthHeaderRepository vacio");
            }
            if (closedMonthInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de closedMonthInventoryRepository vacio");
            }
            if (inventorySequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de inventorySequenseRepository vacio");
            }
            _settingInventoryRepository = settingInventoryRepository;
            _sequenseRepository = sequenseRepository;
            _inventoryServiceRepository = inventoryServiceRepository;
            _kardexRepository = kardexRepository;
            _PhysicalInventoryRepository = physicalInventoryRepository;
            _closedMonthHeaderRepository = closedMonthHeaderRepository;
            _closedMonthInventoryRepository = closedMonthInventoryRepository;
            _inventorySequenseRepository = inventorySequenseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Domain.Entities.SettingInventory GetSettingInventory(int OperatingUnitId)
        {
            try
            {
                return _settingInventoryRepository.GetSettingInventory(OperatingUnitId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el registro de parametros de inventario para el formulario
        /// </summary>
        /// <returns></returns>
        public ActionResult<Domain.Entities.SettingInventory> GetInventorySettingsRegister(int OperatingUnitId)
        {
            try
            {
                Domain.Entities.SettingInventory settingInventory = _settingInventoryRepository.GetInventorySettingsRegister(OperatingUnitId);
                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = true, ObjectEmbbeded = settingInventory };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Guarda o actualiza el parametro de inventario
        /// </summary>
        /// <param name="settingInventory"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.SettingInventory> SaveSettingInventory(Domain.Entities.SettingInventory settingInventory, AuditMessage audit)
        {
            if (settingInventory == null)
            {
                throw new ArgumentNullException("settingInventory");
            }
            IUnitWork unitOfWork = _settingInventoryRepository.UnitWork;

            try
            {
                Domain.Entities.SettingInventory auxSettingInventory = null;
                IndigoAuditSimpleEntity<Domain.Entities.SettingInventory> auditProcess;
                Infrastructure.CrossCutting.Audit.Actions status;

                if (settingInventory.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                {
                    settingInventory.CreationUser = audit.CodeUser;
                    settingInventory.CreationDate = DateTime.Now;
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                }
                else
                {
                    auxSettingInventory = settingInventory.OriginalValue;
                    settingInventory.ModificationUser = audit.CodeUser;
                    settingInventory.ModificationDate = DateTime.Now;
                    status = Infrastructure.CrossCutting.Audit.Actions.Update;
                }


                var settings = _settingInventoryRepository.GetByFilter(m => m.Id != settingInventory.Id);

                if (settingInventory.Id > 0 && settings != null && settings.Any())
                {
                    foreach (var item in settings)
                    {
                        item.DispensingWithoutAuthorization = settingInventory.DispensingWithoutAuthorization;
                        item.WithoutCurrentAuthorizationColor = settingInventory.WithoutCurrentAuthorizationColor;
                        item.ChangeAfter = settingInventory.ChangeAfter;
                        item.ChangeAfterTimeUnit = settingInventory.ChangeAfterTimeUnit;
                        item.WithoutAuthorizationManagementColor = settingInventory.WithoutAuthorizationManagementColor;
                        item.AllowDispensingWithExhaustedAuthorization = settingInventory.AllowDispensingWithExhaustedAuthorization;
                        item.AllowBillingWithoutAuthorization = settingInventory.AllowBillingWithoutAuthorization;

                        _settingInventoryRepository.SaveEntity(item);
                    }
                }

                _settingInventoryRepository.SaveEntity(settingInventory);
                unitOfWork.Commit();
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.SettingInventory>(settingInventory, audit, status, auxSettingInventory);
                auditProcess.Execute();

                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = true, ObjectEmbbeded = settingInventory };

            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = false, MessageResult = new List<string> { "-999" } };
            }
            catch (UpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = false, MessageResult = new List<string> { "-000" } };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.SettingInventory> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Metodo para obtener los documentos que se encuentran sin confirmar
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> VerifiyHasConfirmAllDocuments(int MonthClosed, int YearClosed)
        {
            try
            {
                var verifiyHasConfirmAllDocumentsData = new List<SP_VerifiyHasConfirmAllDocuments_Result>();
                verifiyHasConfirmAllDocumentsData = _settingInventoryRepository.SP_VerifiyHasConfirmAllDocuments(MonthClosed, YearClosed);

                var message = "No se encontraron documentos pendientes por confirmar.";
                if (verifiyHasConfirmAllDocumentsData.Count > 0)
                {
                    message = string.Format(ResourceManager.get_GetString("NotClosedMonth", "Inventory"));
                }

                return new ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> { 
                    StateResult = true,
                    ObjectEmbbeded = verifiyHasConfirmAllDocumentsData,
                    Message =  message
                };
            }
            catch (Exception ex)
            {
                return new ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> { 
                    StateResult = false, 
                    ObjectEmbbeded = null,
                    MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        ///  Realiza el proceso de cierre mensual
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <param name="OperatingUnitId"></param>
        /// <param name="audit"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<InventoryClosedMonth> ClosedMonthInventory(int MonthClosed, int YearClosed, int OperatingUnitId, AuditMessage audit, bool confirm = true)
        {
            if (MonthClosed < 0 || YearClosed < 0)
            {
                throw new ArgumentNullException(MonthClosed < 0 ? "MonthClosed" : "YearClosed");
            }

            try
            {
                var inventoryCloseMonth = new InventoryClosedMonth();
                
                // verificamos que no existan documentos pendientes por confirmar
                inventoryCloseMonth.Documents = VerifiyHasConfirmAllDocuments(MonthClosed, YearClosed).ObjectEmbbeded;

                if (inventoryCloseMonth.Documents.Count == 0)
                {
                     var dateNow = DateTime.Now;

                    if (YearClosed > dateNow.Year ||
                        (YearClosed == dateNow.Year && MonthClosed > dateNow.Month) ||
                        (YearClosed == dateNow.Year && MonthClosed == dateNow.Month && dateNow.Day != new DateTime(dateNow.Year, dateNow.Month, 1).AddMonths(1).AddDays(-1).Day))
                    {
                        return new ActionResult<InventoryClosedMonth>
                        {
                            StateResult = false,
                            Message = "No se puede cerrar el mes, ya que no ha finalizado!!"
                        };
                    }
                    //Validamos que no se pueda ejecutar mas de una vez
                    var monthClosedRecord = GetMonthlyClosed(YearClosed, MonthClosed);
                    if (monthClosedRecord.Id > 0)
                    {
                        return new ActionResult<InventoryClosedMonth>
                        {
                            StateResult = false,
                            Message = $"El mes {MonthClosed:D2} del año {YearClosed} ya ha sido cerrado. No se puede realizar la operación."
                        };
                    }



                    TransactionOptions txSettings = new TransactionOptions();
                    txSettings.Timeout = TransactionManager.MaximumTimeout;
                    txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                    {
                        try
                        {
                            // Cambiar el estado para bloquear transacciones
                            var changeStatusResult = ChangeStatus(true);
                            if (!changeStatusResult.StateResult)
                            {
                                return new ActionResult<InventoryClosedMonth> { StateResult = false, Message = "Error al cambiar el estado de las transacciones. Detalles del error:" + changeStatusResult.Message };
                            }

                            //validamos que no existan comprobantes pendientes por confirmar
                            var validateResult = validateAccountingMovementPending(MonthClosed, YearClosed);
                            if (!validateResult.StateResult)
                            {
                                return new ActionResult<InventoryClosedMonth> { StateResult = false, Message = validateResult.Message };
                            }

                            // Generar datos de cierre mensual de inventario
                            var resultClosedMonth = GenerateDataClosedMonthInventory(MonthClosed, YearClosed, audit);

                            if (!resultClosedMonth.StateResult)
                            {
                                return new ActionResult<InventoryClosedMonth> { StateResult = false, Message = resultClosedMonth.Message };
                            }

                            //Actualiza el siguiente periodo a cerrar
                            var resultChangeMonth = ChangeMonthSettingInventory(MonthClosed, YearClosed, audit.CodeUser);
                            if (!resultChangeMonth.StateResult)
                            {
                                return new ActionResult<InventoryClosedMonth> { StateResult = false, Message = resultChangeMonth.Message };
                            }


                            transaction.Complete();
                            return new ActionResult<InventoryClosedMonth> { StateResult = true,  ObjectEmbbeded = inventoryCloseMonth, Message = "Cierre mensual ejecutado correctamente" };
                        }
                        catch (Exception ex)
                        {
                            transaction.Dispose();
                            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                            return new ActionResult<InventoryClosedMonth> { StateResult = false, ObjectEmbbeded = null, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                        }
                        finally { 
                            ChangeStatus(false);
                        }
                    }
                }
                else
                {
                    return new ActionResult<InventoryClosedMonth> { StateResult = true, ObjectEmbbeded = inventoryCloseMonth, Message = string.Format(ResourceManager.get_GetString("NotClosedMonth", "Inventory")) };
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<InventoryClosedMonth> { StateResult = false, MessageResult = { ex.Message } };
            }

        }

        /// <summary>
        /// Cambia el estado de las banderas para bloquear transacciones mientras se realiza el proceso de cierre mensual o para 
        /// activarlas una ves ya el cierre no se este ejecutando
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult<string> ChangeStatus(Boolean status) 
        {
            IUnitWork unitWork = _settingInventoryRepository.UnitWork;
            TransactionOptions txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    //cambiamos la bandera en SettingInventory la cual es la encargada de bloquear las transacciones realizadas
                    //desde cualquier modulo de VIE
                    _settingInventoryRepository.ExecuteNonQuery("update Inventory.SettingInventory set isClosedMonth = {0}", status);


                    //cambiamos la bandera BotControl la cual es la encargada de bloquear las transacciones que se ejcutan mediante 
                    //el bot Walle o cualquier otro bot o proceso ejecutado fuera de VIE
                    _settingInventoryRepository.ExecuteNonQuery("update Common.BotControl set Disabled = {0}", status);

                    unitWork.Commit();
                    transaction.Complete();
                    return new ActionResult<string> { StateResult = true, ObjectEmbbeded = null, Message = "Estados actualizados correctamente" };
                }
            }
            catch (Exception ex)
            {
                unitWork.RollbackChangesUnitOfWork();
                // Manejo de excepción
                return new ActionResult<string> { StateResult = false, ObjectEmbbeded = null, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        public ActionResult<string> ChangeMonthSettingInventory(int monthClosed, int yearClosed, string codeUser)
        {
            IUnitWork unitWork = _settingInventoryRepository.UnitWork;
            TransactionOptions txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    // Construye la fecha y los valores para el siguiente mes
                    string query = @"
                DECLARE @Date DATE = DATEFROMPARTS({0}, {1}, 1);
                DECLARE @DateNext DATE = DATEADD(MONTH, 1, @Date);
                DECLARE @MonthNext INT = MONTH(@DateNext);
                DECLARE @YearNext INT = YEAR(@DateNext);

                UPDATE Inventory.SettingInventory
                SET Year = @YearNext,
                    Month = @MonthNext,
                    ModificationUser = {2},
                    ModificationDate = GETDATE() ";

                    var parameters = new
                    {
                        YearClosed = yearClosed,
                        MonthClosed = monthClosed,
                        CodeUser = codeUser
                    };

                    _settingInventoryRepository.ExecuteNonQuery(query, yearClosed, monthClosed, codeUser);

                    unitWork.Commit();
                    transaction.Complete();

                    return new ActionResult<string>
                    {
                        StateResult = true,
                        ObjectEmbbeded = null,
                        Message = "Periodo actualizado correctamente"
                    };
                }
            }
            catch (Exception ex)
            {
                unitWork.RollbackChangesUnitOfWork();
                // Manejo de excepción
                return new ActionResult<string>
                {
                    StateResult = false,
                    ObjectEmbbeded = null,
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
                };
            }
        }

        /// <summary>
        /// Valida que no existan comprobantes pendientes por crearce y si existen los ejecuta
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        public ActionResult<string> validateAccountingMovementPending(int MonthClosed, int YearClosed) 
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            try
            {
                var result = getMovementsPending(MonthClosed, YearClosed);

                if (!result.StateResult)
                {
                    return new ActionResult<string>
                    {
                        StateResult = false,
                        ObjectEmbbeded = null,
                        Message = result.Message
                    };
                }

                var errors = new ConcurrentBag<string>();
                var cts = new CancellationTokenSource();
                var token = cts.Token;

                if (result.ObjectEmbbeded.Count > 0)
                {
                    try
                    {
                        Parallel.ForEach(result.ObjectEmbbeded, new ParallelOptions { CancellationToken = token }, movement =>
                        {
                            token.ThrowIfCancellationRequested();
                            try
                            {
                                var response = executeMovementsPending(movement.Id, token);
                                if (!response.StateResult)
                                {
                                    var errorMessage = string.IsNullOrWhiteSpace(response.Message) ? "No se obtuvo detalle del error." : response.Message;
                                    errors.Add($"Movimiento contable {movement.Id}: {errorMessage}");
                                    cts.Cancel(); // Cancelar tareas paralelas
                                }
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception ex)
                            {
                                errors.Add($"Movimiento contable {movement.Id}: {ex.Message}");
                                cts.Cancel();
                            }
                        });
                    }
                    catch (OperationCanceledException)
                    {
                        var message = errors.Any()
                            ? "El proceso de cierre mensual ha sido cancelado." + Environment.NewLine + string.Join(Environment.NewLine, errors.Where(error => !string.IsNullOrWhiteSpace(error)).Distinct())
                            : "El proceso de cierre mensual ha sido cancelado.";

                        return new ActionResult<string>
                        {
                            StateResult = false,
                            ObjectEmbbeded = null,
                            Message = message
                        };
                    }
                }

                using (TransactionScope transaction = new TransactionScope())
                {
                    if (errors.Any())
                    {
                        return new ActionResult<string> { StateResult = false, ObjectEmbbeded = null, Message = string.Join(Environment.NewLine, errors.Where(error => !string.IsNullOrWhiteSpace(error)).Distinct()) };
                    }
                    transaction.Complete(); // Confirmar la transacción
                    return new ActionResult<string> { StateResult = true, ObjectEmbbeded = null, Message = "" };
                }

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                // Manejo de errores en la transacción
                return new ActionResult<string> { StateResult = false, ObjectEmbbeded = null, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }

            
         
        }

        /// <summary>
        /// Obtiene los comprobantes que faltan por crear
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        public ActionResult<List<AccountingMovement>> getMovementsPending(int MonthClosed, int YearClosed)
        {
            try {
                var dataAccountingMovementPending = _settingInventoryRepository.ExecuteQuery<AccountingMovement>("SELECT am.Id, am.JournalVoucherTypeId  " +
                        "FROM GeneralLedger.AccountingMovement as am " +
                        "LEFT JOIN GeneralLedger.JournalVouchers as jv on jv.AccountingMovementId = am.Id  " +
                        "WHERE jv.Id is NULL and MONTH(am.VoucherDate) = {0} and YEAR(am.VoucherDate) = {1} ", MonthClosed, YearClosed).ToList();

                return new ActionResult<List<AccountingMovement>> { StateResult = true, ObjectEmbbeded = dataAccountingMovementPending, Message = "" };
            }
            catch (Exception ex) {
                return new ActionResult<List<AccountingMovement>> { StateResult = false, ObjectEmbbeded = null, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Ejecuta el comprobante para crearlo
        /// </summary>
        /// <param name="movementId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult<SP_SaveJournalVoucherOffline_ByMovementID_Result> executeMovementsPending(int movementId, CancellationToken token)
        {
            try
            {
                // Verificar si se ha solicitado la cancelación
                token.ThrowIfCancellationRequested();

                var result = _settingInventoryRepository.ExecuteStoredProcedure<SP_SaveJournalVoucherOffline_ByMovementID_Result>("[GeneralLedger].[SP_ProcessJournalVoucherMovement]", new List<(string, object)> {
                    ("@MovementId", movementId)
                }).FirstOrDefault();

                if (result == null)
                {
                    return new ActionResult<SP_SaveJournalVoucherOffline_ByMovementID_Result> { StateResult = false, ObjectEmbbeded = null, Message = "El procedimiento no retornó resultado." };
                }

                if (result.CodeMessage == 999)
                {
                    return new ActionResult<SP_SaveJournalVoucherOffline_ByMovementID_Result> { StateResult = false, ObjectEmbbeded = result, Message = result.Message };
                }

                return new ActionResult<SP_SaveJournalVoucherOffline_ByMovementID_Result> { StateResult = true, ObjectEmbbeded = result, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new ActionResult<SP_SaveJournalVoucherOffline_ByMovementID_Result> { StateResult = false, ObjectEmbbeded = null, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Generar la informacion de cierre mensual
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult GenerateDataClosedMonthInventory(int MonthClosed, int YearClosed, AuditMessage audit ) {

            //ejecutamos el sp para la conciliacion de modulos en una tarea
           var dataConcilationModules = _settingInventoryRepository.SP_ConciliationInventoryVsAccounting(MonthClosed, YearClosed);

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    //guardamos la informacion de la cabecera del cierre mensual
                    var closeMonthData = GenerateClosedMonthHeader(YearClosed, MonthClosed, audit.CodeUser);
                    if (closeMonthData.StateResult == false)
                    {
                        return new ActionResult
                        {
                            StateResult = false,
                            Message = closeMonthData.Message
                        };
                    }

                    ActionResult resultValuedInventory = new ActionResult();

                    //generamos el inventario valorizado
                    resultValuedInventory = GenerateValuedInventory(closeMonthData.ObjectEmbbeded.Id);

                    //se espera a que la tarea de la conciliacion de modulos termine
                    Task.WaitAll(dataConcilationModules);
                    var resultConcilationModules = dataConcilationModules.Result;

                    //Se actualiza los saldos pendientes legalizar de inventarios y contabilidad en la tabla cabecera
                    UpdateLegalizedBalancesForClosedMonth(MonthClosed, YearClosed, closeMonthData.ObjectEmbbeded.Id);

                    //si alguna de las tareas genero un error se cancela el proceso
                    if (!resultValuedInventory.StateResult || resultConcilationModules is null || !resultConcilationModules.Any())
                    {
                        transaction.Dispose();
                        return new ActionResult
                        {
                            StateResult = false,
                            Message = "Error"
                        };
                    }

                    transaction.Complete();
                    return new ActionResult { StateResult = true, Message = "Cierre mensual ejecutado correctamente" };

                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false , Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

        /// <summary>
        /// Guarda la informacion de la cabecera del cierre mensual de inventario
        /// </summary>
        /// <returns></returns>
        public ActionResult<ClosedMonthInventoryHeader> GenerateClosedMonthHeader(int YearClosed,int MonthClosed,string codUser)
        { 
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
            IUnitWork unitOfWorkCloseMonthHeader = _closedMonthHeaderRepository.UnitWork;
            var closedMonthHeader = new ClosedMonthInventoryHeader();

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try {
                    //Obtenemos la secuencia para asignar el codigo
                    var inventorySequense = _inventorySequenseRepository.GetSequenseByIdForm("1525");
                    var inventorySequenseDetail = inventorySequense.InventorySequenceDetail.FirstOrDefault();

                    if (inventorySequenseDetail == null || inventorySequense == null)
                    {
                        return new ActionResult<ClosedMonthInventoryHeader>
                        {
                            StateResult = false,
                            ObjectEmbbeded = null,
                            Message = "No se encuentra parametrizada la secuencia numérica para cierre mensual de inventarios"
                        };
                    }

                    InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById(inventorySequenseDetail.Id);
                    if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                    {
                        var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                        if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                        {
                            closedMonthHeader.Code = res;
                            seq.Next += 1;
                            this._sequenseRepository.SaveEntity(seq);
                        }
                        else
                        {
                            return new ActionResult<ClosedMonthInventoryHeader>
                            {
                                StateResult = false,
                                ObjectEmbbeded = null,
                                Message = "No existe patron de secuencia"
                            };
                        }
                    }
                    //Obtener los salidas y entradas
                    closedMonthHeader.Year = YearClosed;
                    closedMonthHeader.Month = MonthClosed;
                    closedMonthHeader.InventoryNoLegalizedBalance = 0;
                    closedMonthHeader.InventoryLegalizedBalance = 0;
                    closedMonthHeader.AccountingNoLegalizedBalance = 0;
                    closedMonthHeader.AccountingLegalizedBalance = 0;
                    closedMonthHeader.CreationUser = codUser;
                    closedMonthHeader.CreationDate = DateTime.Now;
                    _closedMonthHeaderRepository.SaveEntity(closedMonthHeader);

                    unitOfWorkSequense.Commit();
                    unitOfWorkCloseMonthHeader.Commit();
                    transaction.Complete();

                    return new ActionResult<ClosedMonthInventoryHeader>
                    {
                        StateResult = true,
                        ObjectEmbbeded = closedMonthHeader,
                        Message = ""
                    };
                }
                catch (Exception ex)
                {
                    unitOfWorkCloseMonthHeader.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<ClosedMonthInventoryHeader>
                    {
                        StateResult = false,
                        ObjectEmbbeded = null,
                        Message = ex.Message
                    };
                }
            }
        }


        /// <summary>
        /// Guarda el inventario valorizado en la tabla Inventory.ClosedMonthInventory y en la tabla de lotes ClosedMonthInventoryDetail
        /// </summary>
        /// <param name="closeMonthId"></param>
        /// <returns></returns>
        public ActionResult GenerateValuedInventory(int closeMonthId)
        {
            IUnitWork unitWork = _settingInventoryRepository.UnitWork;
            TransactionOptions txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            try
                {
               // se obtiene la informacion de PhysicalInventory excluyendo los tipos de almacenes
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    string insertClosedMonthInventoryQuery = @"
                        INSERT INTO Inventory.ClosedMonthInventory 
                            ([Quantity], [ProductCost], [FinalProductCost], [SellingPrice], [ProductId], [ClosedMonthId], [WareHouseId])
                        SELECT
                            pi.Quantity,
                            ip.ProductCost,
                            ip.FinalProductCost,
                            ip.SellingPrice,
                            pi.ProductId,
                            {0},  
                            pi.WarehouseId AS WareHouseId
                        FROM
                            Inventory.PhysicalInventory pi
                        INNER JOIN Inventory.Warehouse w ON pi.WarehouseId = w.Id
                        INNER JOIN Inventory.InventoryProduct ip ON pi.ProductId = ip.Id
                        WHERE pi.Quantity > 0 AND NOT w.WareHouseType IN(1, 2, 3, 4, 5) 
                          ";

                    string insertClosedMonthInventoryDetailQuery = @"
                        INSERT INTO [Inventory].[ClosedMonthInventoryDetail]
                            ([Quantity], [WarehouseId], [BatchSerialId], [ClosedMonthInventoryId], [CostTotal])
                        SELECT 
                            [pi].Quantity,
                            [pi].WarehouseId,
                            [pi].BatchSerialId,
                            cmi.Id,
                            ip.FinalProductCost * [pi].Quantity
                        FROM Inventory.PhysicalInventory [pi]
                        INNER JOIN Inventory.Warehouse w ON [pi].WarehouseId = w.Id 
                        INNER JOIN Inventory.ClosedMonthInventory cmi ON [pi].ProductId = cmi.ProductId AND cmi.ClosedMonthId = {0}
                        INNER JOIN Inventory.InventoryProduct AS ip on ip.Id = [pi].ProductId
                        INNER JOIN Inventory.ProductSubGroup as psg on psg.Id = ip.ProductSubGroupId
                        WHERE [pi].Quantity > 0  AND NOT w.WareHouseType IN (1, 2, 3, 4, 5) AND psg.HandlesBatch = 1";

                    _settingInventoryRepository.ExecuteNonQuery(insertClosedMonthInventoryQuery, closeMonthId);
                    _settingInventoryRepository.ExecuteNonQuery(insertClosedMonthInventoryDetailQuery, closeMonthId);
                    unitWork.Commit();
                    transaction.Complete();
                    return new ActionResult
                    {
                        StateResult = true,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false, Message = ex.Message };
                }
        }

        /// <summary>
        /// Metodo para validar el stock del producto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="operatingUnitId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryProduct> ValidateStock(int productId, int operatingUnitId, int warehouseId = 0, int quantity = 0, InventoryStaticServices.MovementType movement = 0)
        {

            try
            {
                return _inventoryServiceRepository.ValidateStockProducts(productId, operatingUnitId, warehouseId, quantity, movement);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryProduct> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [Inventory.SP_ReportCloseMonth]
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="WarehouseInitial"></param>
        /// <param name="WarehouseEnd"></param>
        /// <param name="GroupInitial"></param>
        /// <param name="GroupEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportCloseMonth(Dictionary<string, string> filters, SessionValues session)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();

                var xmlFilters = Utils.DictionaryToXML(filters).ToString();

                string query1 = "EXEC [Inventory].[SP_ReportCloseMonth] '" + xmlFilters + "' ";

                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportCloseMonth");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        public System.Data.DataTable GetDatatable(string Comando, SessionValues session, string nameDt)
        {
            System.Data.DataTable functionReturnValue = default(System.Data.DataTable);
            System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection();


            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, session.TransactionalContainer, false));
                    conexion.Open();
                }
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                functionReturnValue = ds.Tables[nameDt];
                da = null;
                ds = null;
                conexion.Close();
                return functionReturnValue;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Metodo para obtener el resumen del mes a cerrar
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<List<SP_GetMonthlyClosureSummary_Result>> GetMonthlyClosureSummary( int YearClosed, int MonthClosed)
        {
            try
            {
                var res = new List<SP_GetMonthlyClosureSummary_Result>();
                res = _settingInventoryRepository.SP_GetMonthlyClosureSummary(YearClosed, MonthClosed);
    
                return new ActionResult<List<SP_GetMonthlyClosureSummary_Result>>
                {
                    StateResult = true,
                    ObjectEmbbeded = res,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                return new ActionResult<List<SP_GetMonthlyClosureSummary_Result>>
                {
                    StateResult = false,
                    ObjectEmbbeded = null,
                    MessageResult = { ex.Message }
                };
            }
        }



        /// <summary>
        /// Obtiene informacion de tabla cabecera cierre mensual
        /// </summary>
        /// <param name="yearClosed"></param>
        /// <param name="monthClosed"></param>
        /// <returns></returns>
        public Domain.Entities.ClosedMonthInventoryHeader GetMonthlyClosed(int yearClosed, int monthClosed)
        {
            try
            {
                return _settingInventoryRepository.GetMonthlyClosed(yearClosed, monthClosed);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Actualiza los valores legalizados o no legalizado de inventario/contabilidad
        /// </summary>
        /// <param name="monthClosed"></param>
        /// <param name="yearClosed"></param>
        /// <param name="idClosedMonth"></param>
        /// <returns></returns>
        public ActionResult<string> UpdateLegalizedBalancesForClosedMonth(int monthClosed, int yearClosed, int idClosedMonth)
        {
            IUnitWork unitWork = _settingInventoryRepository.UnitWork;
            TransactionOptions txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    string query = @"
                    DECLARE @ILegalizedBalance DECIMAL(32,4) = 0, --Inventario
                            @ALegalizedBalance DECIMAL(32,4) = 0, --Contabilidad
                            @INoLegalizedBalance DECIMAL(32,4) = 0, --Inventario
                            @ANoLegalizedBalance DECIMAL(32,4) = 0; --Contabilidad

                    -- Calcular balances legalizados
                    SELECT 
                        @ILegalizedBalance = ISNULL((SUM(t.TotalDebitInventory) - SUM(t.TotalCreditInventory)), 0),
                        @ALegalizedBalance = ISNULL((SUM(t.TotalDebitAccounting) - SUM(t.TotalCreditAccounting)), 0)
                    FROM Inventory.ClosedMonthModulesConciliation t
                    WHERE t.EntityName NOT IN ('REMISION DE ENTRADA', 'RECLASIFICACION REMISIONES', 'DEVOLUCION REMISION DE ENTRADA')
                    AND  t.Year = {2} AND t.Month = {1};

                    -- Calcular balances no legalizados
                    SELECT 
                        @INoLegalizedBalance = ISNULL((SUM(t.TotalDebitInventory) - SUM(t.TotalCreditInventory)), 0),
                        @ANoLegalizedBalance = ISNULL((SUM(t.TotalDebitAccounting) - SUM(t.TotalCreditAccounting)), 0)
                    FROM Inventory.ClosedMonthModulesConciliation t
                    WHERE t.EntityName IN ('REMISION DE ENTRADA', 'RECLASIFICACION REMISIONES', 'DEVOLUCION REMISION DE ENTRADA')
                    AND t.Year = {2} AND t.Month = {1};

                    -- Actualizar la tabla Inventory.ClosedMonth con los balances calculados
                    UPDATE Inventory.ClosedMonth
                    SET 
                        InventoryNoLegalizedBalance = @INoLegalizedBalance,
                        InventoryLegalizedBalance = @ILegalizedBalance, 
                        AccountingNoLegalizedBalance = @ANoLegalizedBalance,
                        AccountingLegalizedBalance = @ALegalizedBalance
                    WHERE Id = {0};
                ";
                    _settingInventoryRepository.ExecuteNonQuery(query, idClosedMonth, monthClosed, yearClosed);
                    unitWork.Commit();
                    transaction.Complete();
                    return new ActionResult<string>
                    {
                        StateResult = true,
                        ObjectEmbbeded = null,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                unitWork.RollbackChangesUnitOfWork();
                // Manejo de excepción
                return new ActionResult<string>
                {
                    StateResult = false,
                    ObjectEmbbeded = null,
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
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
                if (disposing)
                {

                }
                _settingInventoryRepository = null;
                _sequenseRepository = null;
                _inventoryServiceRepository = null;
                _inventorySequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
