/************************************************************************
Assembly         : Domain.Inventory
Author           : Oscar Stiven Astudillo
Created          : 2024-02-15
Copyright        : (c) . All rights reserved.
***********************************************************************
*/

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Resources;
using System.Transactions;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Application.Inventory.ConsignmentCostList
{
    public class ConsignmentCostListAdminService : IConsignmentCostListAdminService
    {

        #region Variables
        /// <summary>
        ///  Variables
        /// </summary>
        private const string FORM_NAME = "FrmConsignmentCostList";
        private IConsignmentCostListRepository _consignmentCostListRepository;
        private IConsignmentCostListDetailRecordRepository _consignmentCostListDetailRecordRepository;
        private ISupplierRepository _SupplierRepository;
        private IKardexRepository _kardexRepository;
        private IWarehouseRepository _warehouseRepository;
        #endregion



        #region Builder
        /// <summary>
        /// inicia el repositorio de lista de costos consignacion
        /// </summary>
        public ConsignmentCostListAdminService(IConsignmentCostListRepository consignmentCostListRepository, IConsignmentCostListDetailRecordRepository consignmentCostListDetailRecordRepository
                 , ISupplierRepository SupplierRepository, IKardexRepository kardexRepository, IWarehouseRepository WarehouseRepository)
        {
            if (consignmentCostListRepository == null)
            {
                throw new ArgumentNullException("Repositorio de ConsignmentCostListRepository vacio");
            }
            _consignmentCostListRepository = consignmentCostListRepository;
            _consignmentCostListDetailRecordRepository = consignmentCostListDetailRecordRepository;
            _SupplierRepository = SupplierRepository;
            _kardexRepository = kardexRepository;
            _warehouseRepository = WarehouseRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        ///  Guarda un registro lista de costos consignacion
        /// </summary>
        /// <param name="productTemplate"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ConsignmentCostList> SaveConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit)
        {
            if (consignmentCostList == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._consignmentCostListRepository.UnitWork;

            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;

                     MessageResult = ResourceManager.get_GetString("SaveMessage");
                    Domain.Entities.ConsignmentCostList auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ConsignmentCostList> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (consignmentCostList.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        consignmentCostList.CreationUser = audit.CodeUser;
                        consignmentCostList.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = consignmentCostList.OriginalValue;
                        consignmentCostList.ModificationDate = DateTime.Now;
                        consignmentCostList.ModificationUser = audit.CodeUser;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    ConsignmentCostListDetailRecord consignmentCostListDetailRecord;

                    var ConsignmentCostListDetailModified = new List<ConsignmentCostListDetail>();
                    var Supplier = _SupplierRepository.GetSupplierById(consignmentCostList.SupplierId);
                    var warehouseIds = _warehouseRepository.GetByFilter(x => consignmentCostList.SupplierId == x.SupplierId).Select(x => x.Id).ToList();
                    for (int i = 0; i < consignmentCostList.ConsignmentCostListDetail.Count; i++)
                    {
                        consignmentCostList.ConsignmentCostListDetail[i].CreationUser = audit.CodeUser;
                        consignmentCostList.ConsignmentCostListDetail[i].CreationDate = DateTime.Now;

                        if (consignmentCostList.ConsignmentCostListDetail[i].Id > 0)
                        {
                            consignmentCostList.ConsignmentCostListDetail[i].ModificationUser = audit.CodeUser;
                            consignmentCostList.ConsignmentCostListDetail[i].ModificationDate = DateTime.Now;
                            // Si el costo diferente al actualizado, crea un nuevo registro para guardar el historico
                            // Valida la cantidad en el kardex (debe ser >0 )para crear el comprobante
                            var detailOld = _consignmentCostListRepository.GetDetailById(consignmentCostList.ConsignmentCostListDetail[i].Id);
                            if (detailOld != null)
                            {
                                if (detailOld.CostNew != consignmentCostList.ConsignmentCostListDetail[i].CostNew && consignmentCostList.ConsignmentCostListDetail[i].CostNew > 0)
                                {
                                    if (Supplier.ConsignmentInventoryCosting == 1)
                                    {
                                        var ValidityProduct = _kardexRepository.GetQuantityKardexPoductIdAndWareHouseId(warehouseIds, consignmentCostList.ConsignmentCostListDetail[i].ProductId);
                                        if (ValidityProduct > 0)
                                        {
                                            ConsignmentCostListDetailModified.Add(consignmentCostList.ConsignmentCostListDetail[i]);
                                        }
                                    }
                                    //obtiene los registros a eliminar, para que no se registran mas de 5 por detalle
                                    var listRecordsDelete = _consignmentCostListDetailRecordRepository.LimitHistoricalRecords(consignmentCostList.ConsignmentCostListDetail[i].Id);
                                    foreach (var entity in listRecordsDelete)
                                    {
                                       entity.MarkAsDeleted();
                                       consignmentCostList.ConsignmentCostListDetail[i].ConsignmentCostListDetailRecord.Add(entity);
                                    }
                                    consignmentCostListDetailRecord = new ConsignmentCostListDetailRecord
                                    {
                                        ConsignmentCostListDetailId = consignmentCostList.ConsignmentCostListDetail[i].Id,
                                        Cost = detailOld.CostNew ?? 0,
                                        CreationUser = audit.CodeUser,
                                        CreationDate = DateTime.Now
                                    };
                                    consignmentCostList.ConsignmentCostListDetail[i].ConsignmentCostListDetailRecord.Add(consignmentCostListDetailRecord);
                                }
                            }
                        }
                    }
                    this._consignmentCostListRepository.SaveEntity(consignmentCostList);
                    unitOfWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ConsignmentCostList>(consignmentCostList, audit, status, auxObjEntity);
                    if (ConsignmentCostListDetailModified.Count > 0)
                    {
                        var ResultSP = GenerateJournalVourcher(ConsignmentCostListDetailModified, audit.CodeUser);
                        if (ResultSP.StateResult)
                        {
                            MessageResult += "\n" + ResultSP.Message;
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.ConsignmentCostList> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = consignmentCostList, Message = MessageResult };
                        }
                    }
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    consignmentCostList.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.ConsignmentCostList> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = consignmentCostList, Message = MessageResult };
                }
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ConsignmentCostList> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ex.Message };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ConsignmentCostList> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = ex.Message };
            }
        }

        /// <summary>
        /// Se Hace el Xml para hacer la consulta
        /// </summary>
        /// <param name="ConsignmentCostListDetailModified"></param>
        /// <returns></returns>
        private String GenerateXmlNewConsignmentCost(List<ConsignmentCostListDetail> ConsignmentCostListDetailModified)
        {
            StringBuilder builder = new StringBuilder();
            String formatString = "<{0}>{1}</{0}>";

            foreach (ConsignmentCostListDetail NewConsignmentCostListDetail in ConsignmentCostListDetailModified)
            {
                builder.Append("<ConsignmentCostListDetail>");

                var costOld = NewConsignmentCostListDetail.ConsignmentCostListDetailRecord.OrderByDescending(c => c.CreationDate).FirstOrDefault().Cost;

                builder.Append(String.Format(formatString, "ProductId", NewConsignmentCostListDetail.ProductId));
                builder.Append(String.Format(formatString, "CostOld", costOld));
                builder.Append(String.Format(formatString, "CostNew", NewConsignmentCostListDetail.CostNew));
                builder.Append(String.Format(formatString, "ConsignmentCostListId", NewConsignmentCostListDetail.ConsignmentCostListId));


                builder.Append("</ConsignmentCostListDetail>");
            }

            return builder.ToString();
        }
        /// <summary>
        /// Guarda el comprobante contable 
        /// </summary>
        /// <param name="ConsignmentCostListDetailModified"></param>
        /// <param name="CodeUser"></param>
        /// <returns></returns>
        public ActionMessageResult GenerateJournalVourcher(List<ConsignmentCostListDetail> ConsignmentCostListDetailModified, string CodeUser)
        {
            String xml = GenerateXmlNewConsignmentCost(ConsignmentCostListDetailModified);
            SP_GenerateJournalVoucherByConsignmentCostListDetail_Result resultSP = _consignmentCostListRepository.SaveJournalVoucherConsignmentCostList(xml, CodeUser);
            bool CodeResult = (resultSP.CodeMessageSP == 0) ? true : false;
            ActionMessageResult result = new ActionMessageResult { StateResult = CodeResult, Message = resultSP.MessageSP };
            return result;
        }
        /// <summary>
        /// Obtiene inforamcion del encabezado y detalle
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentCostList GetConsignmentCostListBySupplierId(int SupplierId, int OperatingUnitId)
        {
            if (SupplierId == 0 || OperatingUnitId == 0 )
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _consignmentCostListRepository.GetConsignmentCostListBySupplierId(SupplierId, OperatingUnitId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }


        /// <summary>
        /// Elimina un registro
        /// </summary>
        public ActionResult DeleteConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit)
        {
            if (consignmentCostList == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }

            IUnitWork unitOfWork = this._consignmentCostListRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    consignmentCostList.ModificationUser = audit.CodeUser;
                    consignmentCostList.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;

                    while (consignmentCostList.ConsignmentCostListDetail.Count > 0)
                    {
                        consignmentCostList.ConsignmentCostListDetail[consignmentCostList.ConsignmentCostListDetail.Count - 1].MarkAsDeleted();
                    }
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ConsignmentCostList>(consignmentCostList, audit, status);
                    consignmentCostList.MarkAsDeleted();
                    _consignmentCostListRepository.SaveEntity(consignmentCostList);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }


        /// <summary>
        ///  Valida informacion extraida de excel o del copypaste mediante SP
        /// </summary>
        /// <param name="dataimport"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult<List<ConsignmentCostListDetail>> SetConsignmentConsListDetailFromFile(List<ImportFileRow> dataimport, List<List<string>> data)
        {
            try
            {
                string xmlObject = string.Empty;
                List<ConsignmentCostListDetail> listConsignmentCostListDetail = new List<ConsignmentCostListDetail>();
                List<string[]> listRecordsErrors = new List<string[]>();
                List<string> listErrors = new List<string>();

                if (dataimport != null)
                {
                    xmlObject = ConvertToXmlImportFile(dataimport, false);
                }
                else
                {
                    xmlObject = ConvertToXmlCopyPaste(data, false);
                }

                //Se consume el procedimiento almacenado que realiza las validaciones del archivo excel
                var resultStore = _consignmentCostListRepository.SetConsignmentCostListDetailFromFile(xmlObject);

                // Se crean los objetos para devolver y pegar en la rejilla
                if (resultStore != null && resultStore.Count > 0)
                {
                    foreach (var itemXml in resultStore)
                    {
                        if (itemXml.StatusField == 0)
                        {
                            // Se crea el nuevo objeto para agregarlo al listado
                            listConsignmentCostListDetail.Add(new ConsignmentCostListDetail
                            {
                                ProductId = itemXml.ProductId ?? 0,
                                ProductCodeName = itemXml.Product,
                                CostNew = itemXml.CostNew,
                                ProductType = itemXml.ProductType == 2 ? "Producto" : "Insumo"
                        });
                        }
                        else // Si el estado del item es False y no pasó alguna validación
                        {
                            if (itemXml.Product != null)
                            {
                                if (itemXml.Product.Contains("-"))
                                {
                                    var _Product = itemXml.Product.Split('-');
                                    itemXml.Product = _Product[0];
                                }
                            }
                            string[] datos = { itemXml.Product,  itemXml.Cost.ToString(), itemXml.MessageField };
                            listRecordsErrors.Add(datos);
                            listErrors.Add(itemXml.MessageField);
                        }
                    }
                }

                // Se devuelve el mensaje
                return new ActionResult<List<ConsignmentCostListDetail>>
                {
                    StateResult = true,
                    ObjectEmbbeded = listConsignmentCostListDetail,
                    MessageResult = listErrors,
                    ListMessageResult = listRecordsErrors
                };

            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == -2146232060)
                {
                    return new ActionResult<List<ConsignmentCostListDetail>>
                    {
                        StateResult = false,
                        Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"
                    };
                }
                else
                {
                    return new ActionResult<List<ConsignmentCostListDetail>>
                    {
                        StateResult = false,
                        Message = Utils.GetInnerExceptionMessageToString(ex)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<List<ConsignmentCostListDetail>>
                {
                    StateResult = false,
                    Message = Utils.GetInnerExceptionMessageToString(ex)
                };
            }
        }

        /// <summary>
        /// Convierte Xml archivo importado
        /// </summary>
        /// <param name="data"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private string ConvertToXmlImportFile(List<ImportFileRow> data, bool flag)
        {
            StringBuilder builder = new StringBuilder();

            if (flag)
            {
                builder.Append("<Data>");

                foreach (var item in data)
                {
                    int indexRow = item.IndexRow;
                    int columns = item.Row.Count;
                    //Se crean columnas con nombres para indentificar en el SP
                    builder.Append("<Row>");
                    builder.Append("<RowIndex>" + indexRow + "</RowIndex>");
                    builder.Append("<RowColumns>" + columns + "</RowColumns>");
                    builder.Append("<Product>" + (columns > 0 ? item.Row[0] : string.Empty) + "</Product>");
                    builder.Append("<Cost>" + (columns > 1 ? item.Row[1]?.ToString().Replace(",", ".") : string.Empty) + "</Cost>");
                    builder.Append("</Row>");
                }
                builder.Append("</Data>");
            }
            else
            {
                builder.Append("<Data>");
                foreach (var item in data)
                {
                    int indexRow = item.IndexRow;
                    int columns = item.Row.Count;
                    builder.Append("<Row>");
                    builder.Append("<RowIndex>" + indexRow + "</RowIndex>");
                    builder.Append("<RowColumns>" + columns + "</RowColumns>");
                    builder.Append("<Product>" + (columns > 0 ? item.Row[0] : string.Empty) + "</Product>");
                    builder.Append("<Cost>" + (columns > 1 ? item.Row[1]?.ToString().Replace(",", ".") : string.Empty) + "</Cost>");
                    builder.Append("</Row>");
                }
                builder.Append("</Data>");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Convierte Xml de la informacion copiada
        /// </summary>
        /// <param name="data"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private string ConvertToXmlCopyPaste(List<List<string>> data, bool flag)
        {
            StringBuilder builder = new StringBuilder();

            if (flag)
            {
                builder.Append("<Data>");
                int indexRow = 0;
                foreach (var item in data)
                {
                    indexRow++;
                    int columns = item.Count;

                    builder.Append("<Row>");
                    builder.Append("<RowIndex>" + indexRow + "</RowIndex>");
                    builder.Append("<RowColumns>" + columns + "</RowColumns>");
                    builder.Append("<Product>" + (columns > 0 ? item[0] : string.Empty) + "</Product>");
                    builder.Append("<Cost>" + (columns > 1 ? item[1].Replace(",", ".") : string.Empty) + "</Cost>");
                    builder.Append("</Row>");
                }

                builder.Append("</Data>");
            }
            else
            {
                builder.Append("<Data>");

                int indexRow = 0;
                foreach (var item in data)
                {
                    indexRow++;
                    int columns = item.Count;

                    builder.Append("<Row>");
                    builder.Append("<RowIndex>" + indexRow + "</RowIndex>");
                    builder.Append("<RowColumns>" + columns + "</RowColumns>");
                    builder.Append("<Product>" + (columns > 0 ? item[0] : string.Empty) + "</Product>");
                    builder.Append("<Cost>" + (columns > 1 ? item[1].Replace(",", ".") : string.Empty) + "</Cost>");
                    builder.Append("</Row>");
                }

                builder.Append("</Data>");
            }

            return builder.ToString();
        }


        /// <summary>
        /// Actualiza estado del registro
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ConsignmentCostList> UpdateStateConsignmentCostList(int Id, bool state, AuditMessage audit)
        {
            if (Id == 0)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            if (audit == null)
            {
                throw new ArgumentNullException(nameof(audit));
            }

            try
            {
                var consignmentCostList = this._consignmentCostListRepository.GetConsignmentCostListById(Id);
                if (consignmentCostList != null && consignmentCostList.Id > 0)
                {
                    consignmentCostList.Status = state;
                }
                return SaveConsignmentCostList(consignmentCostList, audit);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ConsignmentCostList>
                {
                    StateResult = false,
                    MessageResult = new List<string> { "-999" },
                    Message = ex.Message
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
                _consignmentCostListRepository = null;
                _consignmentCostListDetailRecordRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
