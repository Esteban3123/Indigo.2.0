///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Diego A. Roldán
/// Created          : 2023-05-08
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using Application.Inventory.ConsignmentInventoryRemission;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Application.Inventory.ConsignmentTransfer
{
    public class ConsignmentTransferAdminService : IConsignmentTransferAdminService
    {

        private const string FORM_NAME = "FrmConsigmentTransfer";
        private readonly IInventoryConsignmentTransferRepository _consignmentTransferRepository;
        private readonly IConsignmentInventoryRemissionRepository _consignmentInventoryRemissionRepository;
        private readonly IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryRemissionDetailBatchSerialRepository;
        private readonly IConsignmentInventoryRemissionDetailRepository _consignmentInventoryRemissionDetailRepository;
        private readonly IConsignmentInventoryRemissionAdminService _consignmentInventoryRemissionAdminService;
        private readonly IInventorySequenceDetailRepository _sequenseRepository;
        private readonly IPhysicalInventoryRepository _physicalInventoryRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IInventorySequenceRepository _inventorySequenceRepository;
        private readonly IInventoryProductRepository _inventoryProductRepository;
        private readonly ISuppliersDistributionLinesRepository _suppliersDistributionLinesRepository;

        public ConsignmentTransferAdminService(IInventoryConsignmentTransferRepository consignmentTransferRepository,
            IConsignmentInventoryRemissionRepository consignmentInventoryRemissionRepository,
            IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryRemissionDetailBatchSerialRepository,
            IConsignmentInventoryRemissionDetailRepository consignmentInventoryRemissionDetailRepository,
            IConsignmentInventoryRemissionAdminService consignmentInventoryRemissionAdminService,
            IPhysicalInventoryRepository physicalInventoryRepository,
            IWarehouseRepository warehouseRepository,
            IInventorySequenceRepository inventorySequenceRepository,
            IInventoryProductRepository inventoryProductRepository,
            ISuppliersDistributionLinesRepository suppliersDistributionLinesRepository,
            IInventorySequenceDetailRepository sequenseRepository)
        {
            _consignmentInventoryRemissionDetailBatchSerialRepository = consignmentInventoryRemissionDetailBatchSerialRepository;
            _consignmentInventoryRemissionDetailRepository = consignmentInventoryRemissionDetailRepository;
            _consignmentInventoryRemissionAdminService = consignmentInventoryRemissionAdminService;
            _consignmentInventoryRemissionRepository = consignmentInventoryRemissionRepository;
            _suppliersDistributionLinesRepository = suppliersDistributionLinesRepository;
            _consignmentTransferRepository = consignmentTransferRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _inventorySequenceRepository = inventorySequenceRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _warehouseRepository = warehouseRepository;
            _sequenseRepository = sequenseRepository;
        }

        /// <summary>
        /// Genera el xml para la remisión de salida del traslado
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <returns></returns>
        private string GetRemissionXml(Domain.Entities.ConsignmentTransfer consignmentTransfer)
        {
            var xml = new StringBuilder();

            foreach (var detail in consignmentTransfer.ConsignmentTransferDetail)
            {
                foreach (var batch in detail.ConsignmentTransferDetailBatchSerial)
                {
                    xml.AppendLine("<Remission>");
                    var batchSerialId = _physicalInventoryRepository.Query(m => m.Id == batch.PhysicalInventoryId).Select(m => m.BatchSerialId).FirstOrDefault();
                    xml.AppendLine($"<EntityDetailId>{detail.Id}</EntityDetailId>");
                    xml.AppendLine($"<OperatingUnitId>{consignmentTransfer.OperatingUnitId}</OperatingUnitId>");
                    xml.AppendLine($"<ProductId>{detail.ProductId}</ProductId>");
                    xml.AppendLine($"<BatchSerialId>{batchSerialId}</BatchSerialId>");
                    xml.AppendLine("<MovementType>2</MovementType>");
                    xml.AppendLine($"<WarehouseId>{consignmentTransfer.WarehouseId}</WarehouseId>");
                    xml.AppendLine($"<Quantity>{batch.Quantity}</Quantity>");
                    xml.AppendLine($"<Value>{detail.ProductCost.ToString().Replace(",", ".")}</Value>");
                    xml.AppendLine("</Remission>");
                }
            }

            return xml.ToString();
        }

        /// <summary>
        /// Genera la salida del almacén de origen del traslado
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <returns></returns>
        private ActionResult GenerateOutputTransfer(Domain.Entities.ConsignmentTransfer consignmentTransfer, AuditMessage audit)
        {
            var conx = string.Format(ConfigurationManager.ConnectionStrings[ConfigurationFile.CONX_GENESIS].ConnectionString, "", ServerSessionValues.Current.CurrentContainer);
            using (var conn = new System.Data.SqlClient.SqlConnection(conx))
            {
                var command = new System.Data.SqlClient.SqlCommand("[Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission]", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Remission", GetRemissionXml(consignmentTransfer)));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EntityId", consignmentTransfer.Id));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EntityCode", consignmentTransfer.Code));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EntityName", nameof(Domain.Entities.ConsignmentTransfer)));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@User", audit.CodeUser));
                var outputParam = new System.Data.SqlClient.SqlParameter();
                outputParam.ParameterName = "@MessageReturn";
                outputParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outputParam.Size = -1;
                outputParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(outputParam);

                conn.Open();
                command.ExecuteNonQuery();
                var result = command.Parameters["@MessageReturn"].Value;
                string outputValue = result == DBNull.Value ? "" : result.ToString();

                if (outputValue == "")
                    return new ActionResult { StateResult = true };
                else
                    return new ActionResult { StateResult = false, Message = outputValue };
            }
        }

        /// <summary>
        /// Remisión de Entrada a almacenes de destino
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <returns></returns>
        private ActionResult<List<Domain.Entities.ConsignmentInventoryRemission>> GenerateReturnedRemissionConsignment(Domain.Entities.ConsignmentTransfer consignmentTransfer)
        {
            var documentsToGenerate = new List<Domain.Entities.ConsignmentInventoryRemission>();
            foreach (var transferDetailByWarehouse in consignmentTransfer.ConsignmentTransferDetail.GroupBy(m => m.WarehouseId).ToList())
            {
                var wh = _warehouseRepository.FirstOrDefault(m => m.Id == transferDetailByWarehouse.Key);
                var supplierDistributionLineId = _suppliersDistributionLinesRepository.Query(m => m.IdSupplier == wh.SupplierId).Select(m => m.Id).FirstOrDefault();

                // remisión de incremento
                var increasedRemission = new Domain.Entities.ConsignmentInventoryRemission
                {
                    Code = "",
                    RemissionDate = consignmentTransfer.DocumentDate,
                    OperatingUnitId = consignmentTransfer.OperatingUnitId,
                    SupplierId = wh.SupplierId,
                    SupplierDistributionLineId = supplierDistributionLineId,
                    WarehouseId = transferDetailByWarehouse.Key,
                    MovementType = 2, // Esto es lo que se debe consultar haber si hace 1: Apertura, 2: Incremento, 3: Reposición
                    RemissionNumber = consignmentTransfer.Code,
                    Description = consignmentTransfer.Description,
                    IvaValue = 0,
                    ProductStatus = 1,//???????????
                    CurrencyId = consignmentTransfer.CurrencyId,
                    Status = 1,
                    EntityName = nameof(Domain.Entities.ConsignmentTransfer),
                    EntityCode = consignmentTransfer.Code,
                    EntityId = consignmentTransfer.Id
                };

                // remisión de reposición
                var repositionRemission = new Domain.Entities.ConsignmentInventoryRemission
                {
                    Code = "",
                    RemissionDate = consignmentTransfer.DocumentDate,
                    OperatingUnitId = consignmentTransfer.OperatingUnitId,
                    SupplierId = wh.SupplierId,
                    SupplierDistributionLineId = supplierDistributionLineId,
                    WarehouseId = transferDetailByWarehouse.Key,
                    MovementType = 3, // Esto es lo que se debe consultar haber si hace 1: Apertura, 2: Incremento, 3: Reposición
                    RemissionNumber = consignmentTransfer.Code,
                    Description = consignmentTransfer.Description,
                    IvaValue = 0,
                    ProductStatus = 1,//???????????
                    CurrencyId = consignmentTransfer.CurrencyId,
                    Status = 1,
                    EntityName = nameof(Domain.Entities.ConsignmentTransfer),
                    EntityCode = consignmentTransfer.Code,
                    EntityId = consignmentTransfer.Id
                };

                foreach (var consigmentTransderDetail in transferDetailByWarehouse)
                {
                    foreach (var transferDetailBatchSerial in consigmentTransderDetail.ConsignmentTransferDetailBatchSerial)
                    {
                        // aca recorro los registros a los cuales le van a hacer el incremento o reposición
                        var batchSerialId = _physicalInventoryRepository.Query(m => m.Id == transferDetailBatchSerial.PhysicalInventoryId).Select(m => m.BatchSerialId).FirstOrDefault();
                        // consulto si existe este mismo producto con cantidades pendientes para hacer reposición
                        var itemsWithPendingReposition = _consignmentInventoryRemissionDetailBatchSerialRepository
                            .GetConsignmentInventoryRemissionToReposition(transferDetailByWarehouse.Key, consigmentTransderDetail.ProductId, batchSerialId);
                        // cantidades por reponer para el producto
                        var pendingTransfer = transferDetailBatchSerial.Quantity;

                        if (itemsWithPendingReposition.Any())
                        {
                            // reposición
                            foreach (var repoItem in itemsWithPendingReposition)
                            {
                                var toReposition = repoItem.UsedQuantity - repoItem.ReturnedQuantity;
                                var quantity = toReposition > pendingTransfer ? pendingTransfer : toReposition;
                                pendingTransfer -= quantity;
                                Domain.Entities.ConsignmentInventoryRemissionDetail consignmentDetailReposition = null;

                                if (repositionRemission.ConsignmentInventoryRemissionDetail.Any(m => m.ProductId == consigmentTransderDetail.ProductId))
                                {
                                    consignmentDetailReposition = repositionRemission.ConsignmentInventoryRemissionDetail.FirstOrDefault(m => m.ProductId == consigmentTransderDetail.ProductId);
                                    consignmentDetailReposition.Quantity += quantity;
                                    consignmentDetailReposition.SubTotalValue = consigmentTransderDetail.ProductCost * consignmentDetailReposition.Quantity;
                                    consignmentDetailReposition.TotalValue = consigmentTransderDetail.ProductCost * consignmentDetailReposition.Quantity;
                                }
                                else
                                {
                                    consignmentDetailReposition = new Domain.Entities.ConsignmentInventoryRemissionDetail
                                    {
                                        RemissionSource = 1, //Ninguna
                                        SourceCode = "",
                                        ProductId = consigmentTransderDetail.ProductId,
                                        Quantity = quantity,
                                        UnitValue = consigmentTransderDetail.ProductCost,
                                        LastValue = consigmentTransderDetail.ProductCost,
                                        SubTotalValue = consigmentTransderDetail.ProductCost * quantity,
                                        IvaPercentage = 0,
                                        IvaValue = 0,
                                        TotalValue = consigmentTransderDetail.ProductCost * quantity,
                                        ConsignmentInventoryRemissionDetailId = repoItem.ConsignmentInventoryRemissionDetailId
                                    };

                                    repositionRemission.ConsignmentInventoryRemissionDetail.Add(consignmentDetailReposition);
                                }

                                var consignmentBatch = new Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial
                                {
                                    BatchSerialId = repoItem.BatchSerialId,
                                    Quantity = quantity,
                                    OutstandingQuantity = quantity,
                                    ConsignmentInventoryRemissionDetailBatchSerialId = repoItem.Id,
                                };

                                consignmentDetailReposition.ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentBatch);

                                if (pendingTransfer == 0) break;
                            }

                        }

                        // incremento
                        if (pendingTransfer > 0)
                        {
                            var increasedDetailReposition = new Domain.Entities.ConsignmentInventoryRemissionDetail
                            {
                                RemissionSource = 1, //Ninguna
                                SourceCode = "",
                                ProductId = consigmentTransderDetail.ProductId,
                                Quantity = pendingTransfer,
                                UnitValue = consigmentTransderDetail.ProductCost,
                                LastValue = consigmentTransderDetail.ProductCost,
                                SubTotalValue = consigmentTransderDetail.ProductCost * pendingTransfer,
                                IvaPercentage = 0,
                                IvaValue = 0,
                                TotalValue = consigmentTransderDetail.ProductCost * pendingTransfer
                            };

                            var increasedBatch = new Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial
                            {
                                BatchSerialId = batchSerialId,
                                Quantity = pendingTransfer,
                                OutstandingQuantity = pendingTransfer
                            };

                            increasedDetailReposition.ConsignmentInventoryRemissionDetailBatchSerial.Add(increasedBatch);
                            increasedRemission.ConsignmentInventoryRemissionDetail.Add(increasedDetailReposition);
                        }
                    }
                }

                if (repositionRemission.ConsignmentInventoryRemissionDetail.Any())
                {
                    repositionRemission.Value = repositionRemission.ConsignmentInventoryRemissionDetail.Sum(m => m.Quantity * m.UnitValue);
                    repositionRemission.TotalValue = repositionRemission.ConsignmentInventoryRemissionDetail.Sum(m => m.Quantity * m.UnitValue);
                    documentsToGenerate.Add(repositionRemission);
                }

                if (increasedRemission.ConsignmentInventoryRemissionDetail.Any())
                {
                    increasedRemission.Value = increasedRemission.ConsignmentInventoryRemissionDetail.Sum(m => m.Quantity * m.UnitValue);
                    increasedRemission.TotalValue = increasedRemission.ConsignmentInventoryRemissionDetail.Sum(m => m.Quantity * m.UnitValue);
                    documentsToGenerate.Add(increasedRemission);
                }
            }

            return new ActionResult<List<Domain.Entities.ConsignmentInventoryRemission>> { StateResult = true, ObjectEmbbeded = documentsToGenerate };
        }

        /// <summary>
        /// Obtiene el id de la secuencia numérica por formulario
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="operativeUnitId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private long GetSequenceIdByFormId(string formId, int operativeUnitId)
        {
            int sequenceId = 0;
            var sequence = _inventorySequenceRepository.GetSequenseByIdForm(formId);
            if (sequence?.Id > 0)
            {
                if (sequence.Scope.Equals("O"))
                    sequenceId = sequence.InventorySequenceDetail[0].Id;
                else if (sequence.Scope.Equals("OU"))
                {
                    var seq = sequence.InventorySequenceDetail.FirstOrDefault(m => m.IdOperatingUnit == operativeUnitId);
                    sequenceId = seq?.Id ?? throw new ArgumentException($"No se encontró secuencia para el formulario ({formId})");
                }
            }
            else
                throw new ArgumentException($"No se encontró secuencia para el formulario ({formId})");

            return sequenceId;
        }

        /// <summary>
        /// Confirma el documento de traslado en consignación
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ConsignmentTransfer> ConfirmConsignmentTransfer(Domain.Entities.ConsignmentTransfer consignmentTransfer, AuditMessage audit)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted
                }))
                {
                    var generatedDocuments = new List<string>();
                    var resOutput = GenerateOutputTransfer(consignmentTransfer, audit);
                    if (!resOutput.StateResult)
                    {
                        // No llamar a scope.Dispose(), simplemente salir sin Complete()
                        return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resOutput.Message };
                    }
                    var resJournalVoucher = GenerateOutputJournalVoucher(consignmentTransfer.Id, audit.CodeUser);
                    if (!resJournalVoucher.StateResult)
                    {
                        // No llamar a scope.Dispose(), simplemente salir sin Complete()
                        return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resJournalVoucher.Message };
                    }

                    var resRemission = GenerateReturnedRemissionConsignment(consignmentTransfer);
                    if (!resRemission.StateResult)
                    {
                        // No llamar a scope.Dispose(), simplemente salir sin Complete()
                        return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resRemission.Message };
                    }

                    var sequenceId = GetSequenceIdByFormId("1977", consignmentTransfer.OperatingUnitId);
                    

                    foreach (var item in resRemission.ObjectEmbbeded)
                    {
                        var res = _consignmentInventoryRemissionAdminService.SaveAndConfirmbConsignmentInventoryRemission(item, audit, sequenceId, Infrastructure.CrossCutting.Audit.Actions.Insert, null, true);
                        if (!res.StateResult || !res.StateResultAux)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, Message = res.Message };
                        }
                        generatedDocuments.Add(res.ObjectEmbbeded.Code);
                    }

                    generatedDocuments.Add(resJournalVoucher.Message);

                    consignmentTransfer.ModificationDate = DateTime.Now;
                    consignmentTransfer.ModificationUser = audit.CodeUser;
                    consignmentTransfer.ConfirmationDate = DateTime.Now;
                    consignmentTransfer.ConfirmationUser = audit.CodeUser;
                    consignmentTransfer.Status = 2;
                    _consignmentTransferRepository.SaveEntity(consignmentTransfer);
                    _consignmentTransferRepository.UnitWork.Commit();

                    scope.Complete();
                    return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = true, MessageResult = generatedDocuments };
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = ex.ToDetailString() };
            }
        }

        /// <summary>
        /// Genera el comprobante contable de salida para los productos en el almacén de origen
        /// </summary>
        /// <param name="consignmentTransferId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        private ActionResult GenerateOutputJournalVoucher(int consignmentTransferId, string userCode)
        {
            var res = _consignmentTransferRepository
                .ExecuteStoredProcedure<SPResultModel>("[Inventory].[SP_GenerateJournalVoucherByConsignmentTransfer]", new List<(string, object)> { ("Id", consignmentTransferId), ("CodeUser", userCode) }).FirstOrDefault();
            if (res?.CodeMessage != "0")
                return new ActionResult { StateResult = false, Message = res.Message };

            return new ActionResult { StateResult = true, Message = res.Message };
            //var conx = string.Format(ConfigurationManager.ConnectionStrings[ConfigurationFile.CONX_GENESIS].ConnectionString, "", ServerSessionValues.Current.CurrentContainer);
            //using (var conn = new System.Data.SqlClient.SqlConnection(conx))
            //{
            //    var command = new System.Data.SqlClient.SqlCommand("[Inventory].[SP_GenerateJournalVoucherByConsignmentTransfer]", conn);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", consignmentTransferId));
            //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@User", userCode));                

            //    conn.Open();
            //    command.ExecuteNonQueryAsync();

            //    if (outputValue == "")
            //        return new ActionResult { StateResult = true };
            //    else
            //        return new ActionResult { StateResult = false, Message = outputValue };
            //}
        }

        /// <summary>
        /// Consulta las cantidades en el almacén de consignación
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ConsignmentMovementInventory GetConsignmentInventoryQuantities(int warehouseId, int productId)
        {
            try
            {
                return _consignmentInventoryRemissionRepository.GetConsignmentInventoryQuantities(warehouseId, productId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                throw ex;
            }
        }

        /// <summary>
        /// Consulta por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentTransfer GetConsignmentTransferByCode(string code)
        {
            try
            {
                var consignmentTransfer = _consignmentTransferRepository.GetByCode(code);

                if (consignmentTransfer.ConsignmentTransferDetail.Any())
                {
                    foreach (var item in consignmentTransfer.ConsignmentTransferDetail)
                    {
                        var physicalQuantity = _physicalInventoryRepository.Query(m => m.ProductId == item.ProductId 
                                                                                    && m.WarehouseId == consignmentTransfer.WarehouseId 
                                                                                    && m.Quantity > 0).Sum(m => (int?)m.Quantity) ?? 0;
                        item.PhysicalQuantity = physicalQuantity;
                    }
                }

                return consignmentTransfer;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Consultar por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentTransfer GetConsignmentTransferById(int id)
        {
            try
            {
                return _consignmentTransferRepository.FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        public ActionResult<Domain.Entities.ConsignmentTransfer> SaveAndConfirmConsignmentTransfer(Domain.Entities.ConsignmentTransfer consignmentTransfer, AuditMessage audit, long idSequence = 0)
        {
            try
            {
                if (consignmentTransfer == null) throw new ArgumentNullException("consignmentTransfer");

                bool isSave = consignmentTransfer.Id == 0;

                var resSave = SaveConsignmentTransfer(consignmentTransfer, audit, idSequence);
                if (resSave != null && !resSave.StateResult)
                {
                    return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StateResultAux = false, StatusCode = resSave.StatusCode, Message = resSave.Message };
                }

                var resConfirm = ConfirmConsignmentTransfer(resSave.ObjectEmbbeded, audit);
                if (resConfirm != null && !resConfirm.StateResult)
                {
                    var message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resSave.ObjectEmbbeded.Code, Environment.NewLine + resConfirm.Message);
                    if (!isSave) message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), Environment.NewLine + resConfirm.Message);

                    return new ActionResult<Domain.Entities.ConsignmentTransfer>
                    {
                        StateResult = true,
                        ObjectEmbbeded = resSave.ObjectEmbbeded,
                        StateResultAux = false,
                        Message = message
                    };
                }

                var messageResult = $"Se guardó y confirmó correctamente el registro generando los siguientes documentos de inventario en consignación: {string.Join(",", resConfirm.MessageResult)}";
                return new ActionResult<Domain.Entities.ConsignmentTransfer>
                {
                    StateResult = true,
                    StateResultAux = true,
                    ObjectEmbbeded = GetConsignmentTransferByCode(consignmentTransfer.Code),
                    Message = messageResult
                };
            }
            catch (OptimisticConcurrencyException)
            {
                _consignmentTransferRepository.UnitWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ConsignmentTransfer>
                {
                    StateResult = false,
                    StatusCode = eStatusResult.WARNING,
                    MessageResult = new List<string> { "-999" },
                    Message = ResourceManager.get_GetString("ErrorConcurrence")
                };
            }
            catch (Exception ex)
            {
                _consignmentTransferRepository.UnitWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ConsignmentTransfer>
                {
                    StateResult = false,
                    StatusCode = eStatusResult.EXCEPTION,
                    MessageResult = new List<string> { ex.Message },
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
                };
            }
        }

        public ActionResult<Domain.Entities.ConsignmentTransfer> SaveConsignmentTransfer(Domain.Entities.ConsignmentTransfer consignmentTransfer, AuditMessage audit, long idSequence = 0)
        {
            if (consignmentTransfer == null) throw new ArgumentNullException("consignmentTransfer");

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted
                }))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(consignmentTransfer.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSequence);
                        if (seq != null && seq.Id > 0 && seq.InventorySequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                consignmentTransfer.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.ConsignmentTransfer> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }

                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), consignmentTransfer.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.ConsignmentTransfer> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    if (consignmentTransfer.ChangeTracker.State == ObjectState.Added)
                    {
                        consignmentTransfer.CreationUser = audit.CodeUser;
                        consignmentTransfer.CreationDate = DateTime.Now;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        consignmentTransfer.ModificationUser = audit.CodeUser;
                        consignmentTransfer.ModificationDate = DateTime.Now;
                    }

                    if (consignmentTransfer.Status == 3)
                    {
                        consignmentTransfer.AnnulmentDate = DateTime.Now;
                        consignmentTransfer.AnnulmentUser = audit.CodeUser;
                    }

                    _consignmentTransferRepository.SaveEntity(consignmentTransfer);
                    _consignmentTransferRepository.UnitWork.Commit();
                    _sequenseRepository.UnitWork.Commit();
                    consignmentTransfer.MarkAsUnchanged();
                    scope.Complete();

                    return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = consignmentTransfer, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException)
            {
                _consignmentTransferRepository.UnitWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                _consignmentTransferRepository.UnitWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ConsignmentTransfer> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = new List<string> { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

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

                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion   
    }
}
