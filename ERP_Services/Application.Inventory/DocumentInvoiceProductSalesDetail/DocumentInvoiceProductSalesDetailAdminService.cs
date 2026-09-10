///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Domain.Entities.Service;

namespace Application.Inventory.DocumentInvoiceProductSalesDetail
{
    public class DocumentInvoiceProductSalesDetailAdminService : IDocumentInvoiceProductSalesDetailAdminService
    {
        private IDocumentInvoiceProductSalesDetailRepository _documentInvoiceProductSalesDetailRepository;
        private IRemissionOutputDetailPhysicalRepository _remissionOutputDetailPhysicalRepository;
        private IDocumentInvoiceProductSalesRepository _documentInvoiceProductSalesRepository;
        private IProductRateDetailRepository _productRateDetailRepository;
        private IBillingServices _billingServices;

        public DocumentInvoiceProductSalesDetailAdminService(IDocumentInvoiceProductSalesDetailRepository documentInvoiceProductSalesDetailRepository,
            IRemissionOutputDetailPhysicalRepository RemissionOutputDetailPhysicalRepository, IDocumentInvoiceProductSalesRepository DocumentInvoiceProductSalesRepository,
            IProductRateDetailRepository ProductRateDetailRepository, IBillingServices BillingServices)
        {
            if (documentInvoiceProductSalesDetailRepository == null)
            {
                throw new ArgumentNullException("documentInvoiceProductSalesDetailRepository");
            }
            _documentInvoiceProductSalesDetailRepository = documentInvoiceProductSalesDetailRepository;
            _remissionOutputDetailPhysicalRepository = RemissionOutputDetailPhysicalRepository;
            _documentInvoiceProductSalesRepository = DocumentInvoiceProductSalesRepository;
            _productRateDetailRepository = ProductRateDetailRepository;
            _billingServices = BillingServices;
        }


        public List<Domain.Entities.DocumentInvoiceProductSalesDetail> GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(int DocumentInvoiceProductSalesId)
        {
            try
            {
                return _documentInvoiceProductSalesDetailRepository.ListDocumentInvoiceProductSalesDetailByIdDocumentInvoiceProductSales(DocumentInvoiceProductSalesId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.DocumentInvoiceProductSalesDetail>();
            }
        }

        /// <summary>
        /// Funcion para Importar Remssiones de salida al detalle del formulario de factura de producto
        /// </summary>
        /// <param name="ListIds"></param>
        /// <param name="ContractExternalClientId"></param>
        /// <param name="DocumentInvoiceProductSalesId"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> ImportRemissionOutput(List<int> ListIds,int ContractExternalClientId, int FunctionalUnitId, int? DocumentInvoiceProductSalesId = null)
        {
            try
            {// se valida que no venga vacia la lista de Ids a consultar para hacer la importación
                if(ListIds is null || ListIds.Count == 0)
                {
                    throw new Exception(message:"La Lista de Ids esta vacia");
                }

                // Variables necesarias para retornar
                List<string> errors = new List<string>();
                List<Domain.Entities.RemissionOutputDetailPhysical> ListRemissionOutputDetailPhysical = new List<Domain.Entities.RemissionOutputDetailPhysical>();
                List<Domain.Entities.DocumentInvoiceProductSalesDetail> ListDocumentInvoiceProductSalesDetail = new List<Domain.Entities.DocumentInvoiceProductSalesDetail>();
                Object lockMe = new Object();

                // se consulta la entidad RemssionOutputDetailPhysical y sus entidades añadidas
                ListRemissionOutputDetailPhysical = _remissionOutputDetailPhysicalRepository.GetByFilter(x => ListIds.Contains(x.Id),false,new List<string> { "RemissionOutputDetail.RemissionOutput", "PhysicalInventory.InventoryProduct.GeneralLedgerIVA", "PhysicalInventory.BatchSerial" }).ToList();

                // se ejecuta funcion de validación
                var ResultValidation = ValidationImportRemissionOutput(ListRemissionOutputDetailPhysical, DocumentInvoiceProductSalesId);
                if (ResultValidation == null) { throw new Exception(message: "Fallo el proceso de validación"); };
                if (ResultValidation.StateResult == true ) {
                    if (ResultValidation.MessageResult.Count > 0)
                    {
                        errors = ResultValidation.MessageResult;
                    }
                    ListRemissionOutputDetailPhysical = ResultValidation.ObjectEmbbeded.ToList();
                }
                else
                {
                    return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> { StateResult = false, StateResultAux = false, MessageResult = ResultValidation.MessageResult };
                }

                //se ejecuta la importacion de una entidad a otra
                Parallel.ForEach(ListRemissionOutputDetailPhysical, p =>
                {

                    var DocumentInvoiceProductSalesDetailBatchSerial = new Domain.Entities.DocumentInvoiceProductSalesDetailBatchSerial() {
                        PhysicalInventoryId = p.PhysicalInventoryId,
                        Quantity = p.OutstandingQuantity,
                        OutstandingQuantity = p.OutstandingQuantity,
                        BatchCode = p.PhysicalInventory.BatchSerial !=null ? p.PhysicalInventory.BatchSerial.BatchCode : string.Empty,
                        RemissionOutputPhysicalId = p.Id
                    };
                    lock (lockMe)
                    {

                        if (ListDocumentInvoiceProductSalesDetail.Count > 0 && ListDocumentInvoiceProductSalesDetail.Any(h => h.SourceCode == p.RemissionOutputDetail.RemissionOutput.Code && h.ProductId == p.PhysicalInventory.ProductId))
                        {
                            var DocumentInvoiceProductSalesDetail = (from l in ListDocumentInvoiceProductSalesDetail where l.SourceCode == p.RemissionOutputDetail.RemissionOutput.Code && l.ProductId == p.PhysicalInventory.ProductId select l).FirstOrDefault();

                            DocumentInvoiceProductSalesDetail.Quantity += p.OutstandingQuantity;
                            
                            DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Add(DocumentInvoiceProductSalesDetailBatchSerial);
                            
                        }
                        else
                        {
                            var DocumentInvoiceProductSalesDetail = new Domain.Entities.DocumentInvoiceProductSalesDetail()
                            {
                                ProductId = p.PhysicalInventory.ProductId,
                                Quantity = p.OutstandingQuantity,
                                HandlesBatch = p.PhysicalInventory.BatchSerialId is null ? false : true,
                                ImportSource = 1,
                                SourceCode = p.RemissionOutputDetail.RemissionOutput.Code,
                                RemissionDate = p.RemissionOutputDetail.RemissionOutput.RemissionDate,
                                CodeNameProduct = p.PhysicalInventory.InventoryProduct.Code,
                                IvaPercentage = p.PhysicalInventory.InventoryProduct.GeneralLedgerIVA.Percentage
                            };

                            DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Add(DocumentInvoiceProductSalesDetailBatchSerial);
                            ListDocumentInvoiceProductSalesDetail.Add(DocumentInvoiceProductSalesDetail);
                        };

                    }
                });

                // Trae el valor de los productos a importar
                var ResultValue = CalculateRateProduct(ListDocumentInvoiceProductSalesDetail, ContractExternalClientId, FunctionalUnitId);
                if (ResultValue != null &&  !string.IsNullOrEmpty(ResultValue.Message))
                {
                    var ListMessage = ResultValue.Message.Trim().Split('\n').ToList();
                    ListMessage.ForEach(x => { errors.Add(x); });
                };
                if (ResultValue != null)
                {
                    return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>>
                    {
                        StateResult = ResultValue.StateResult,
                        ObjectEmbbeded = ResultValue.ObjectEmbbeded,
                        MessageResult = new List<string>(errors)
                    };
                }
                else {
                    return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>>
                    {
                        StateResult = false,
                        ObjectEmbbeded = new List<Domain.Entities.DocumentInvoiceProductSalesDetail>(),
                        MessageResult = new List<string>(errors)
                    };
                };
            }
            catch (Exception ex)
            {
                return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> { StateResult = false, StateResultAux = false, Message = ex.Message };
            }

        }

        /// <summary>
        /// Funcion para validar la lista a importar
        /// </summary>
        /// <param name="ListRemissionOutputDetailPhysical"></param>
        /// <param name="DocumentInvoiceProductSalesId"></param>
        /// <returns></returns>
        private ActionResult<List<Domain.Entities.RemissionOutputDetailPhysical>> ValidationImportRemissionOutput(List<Domain.Entities.RemissionOutputDetailPhysical> ListRemissionOutputDetailPhysical, int? DocumentInvoiceProductSalesId = null) {
            List<string> errors = new List<string>();

            if (DocumentInvoiceProductSalesId != null) {
                var DocumentInvoiceProductSales = _documentInvoiceProductSalesRepository.Query(x => x.Id != DocumentInvoiceProductSalesId && (x.DocumentInvoiceProductSalesDetail.Any(i=> i.ImportSource==1 )), false, new List<string> { "DocumentInvoiceProductSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial" }).ToList();
                Parallel.ForEach(ListRemissionOutputDetailPhysical, x =>
                {
                    var Validate = (from item in DocumentInvoiceProductSales where item.DocumentInvoiceProductSalesDetail.Any(g=> g.SourceCode==x.RemissionOutputDetail.RemissionOutput.Code
                                    && g.ProductId==x.PhysicalInventory.ProductId && g.DocumentInvoiceProductSalesDetailBatchSerial.Any(h=> h.PhysicalInventoryId == x.PhysicalInventoryId)) select item).ToList();
                    if(Validate != null && Validate.Count > 0)
                    {
                        errors.Add($"El producto {x.PhysicalInventory.InventoryProduct.Code} ya se encuentra importado en otra Factura de producto");
                        ListRemissionOutputDetailPhysical.Remove(x);
                    };                    
                });
            }
            if (ListRemissionOutputDetailPhysical.Count == 0) {
                return new ActionResult<List<Domain.Entities.RemissionOutputDetailPhysical>> { StateResult = false, ObjectEmbbeded = ListRemissionOutputDetailPhysical.ToList(), MessageResult = errors };
            }
            return new ActionResult<List<Domain.Entities.RemissionOutputDetailPhysical>> { StateResult = true, ObjectEmbbeded = ListRemissionOutputDetailPhysical.ToList(), MessageResult = errors };
        }

        /// <summary>
        /// Calcula el valor de los productos a importar
        /// </summary>
        /// <param name="ListDocumentInvoiceProductSalesDetail"></param>
        /// <param name="ContractExternalClientId"></param>
        /// <param name="FunctionalUnitId"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> CalculateRateProduct(List<Domain.Entities.DocumentInvoiceProductSalesDetail> ListDocumentInvoiceProductSalesDetail, int ContractExternalClientId,int FunctionalUnitId) 
        {
            StringBuilder Errors = new StringBuilder() ;
            if (ListDocumentInvoiceProductSalesDetail == null || ContractExternalClientId ==0 || ListDocumentInvoiceProductSalesDetail.Count == 0) { throw new Exception(message: "El detalle de la factura de producto no puede estar vacio para calcular la tarifa"); };
            var ListDocumentIPSDetailRemove = new List<Domain.Entities.DocumentInvoiceProductSalesDetail>();

            ListDocumentInvoiceProductSalesDetail.ForEach( k =>
            {
                //Domain.Entities.ProductRateDetail ProductRateDetail = new Domain.Entities.ProductRateDetail();
                var ProductRateDetail = _productRateDetailRepository.GetListProductRateDetailByContractExternalClientIdProductIdServiceDate(ContractExternalClientId, k.ProductId, k.RemissionDate);
                if (ProductRateDetail is null )
                { Errors.AppendLine($"No se ha encontrado ningun detalle de la tarifa del producto con codigo :{k.CodeNameProduct}");
                    ListDocumentIPSDetailRemove.Add(k);
                    return; 
                };
                 decimal SalePrice = 0;
                switch (ProductRateDetail.RateType)
                {
                    case 1:
                        SalePrice = ProductRateDetail.SalesValue == 0 ? ProductRateDetail.SalesValueWithSurcharge : ProductRateDetail.SalesValue;
                        break;
                    case 2:
                        if (ProductRateDetail.PercentageBasedOn == 1)
                        {
                            SalePrice = (ProductRateDetail.InventoryProduct.ProductCost * (ProductRateDetail.Percentage.Value / 100)) + (ProductRateDetail.InventoryProduct.ProductCost);
                        }
                        else if (ProductRateDetail.PercentageBasedOn == 2)
                        {
                            SalePrice = (ProductRateDetail.InventoryProduct.FinalProductCost.Value * (ProductRateDetail.Percentage.Value / 100)) + (ProductRateDetail.InventoryProduct.FinalProductCost.Value);
                        }
                        else { SalePrice = 0; };
                        break;
                }

                if (new List<int> {2,3}.Contains(ProductRateDetail.LiquidationType))
                {//Calcula el precio del cups
                        var Result = _billingServices.GetServiceValueToMS(ContractExternalClientId, ProductRateDetail.CupsId.Value, FunctionalUnitId, null, k.RemissionDate, 0,0,null,ProductRateDetail.ContractDescriptionId);
                        if (Result != null && Result.StateResult == false)
                        {
                            if (string.IsNullOrEmpty(Result.Message)) { Errors.AppendLine(Result.Message); ListDocumentInvoiceProductSalesDetail.Remove(k); };
                        return;
                        };
                    if (Result.ObjectEmbbeded != null && Result.ObjectEmbbeded.Count == 1)
                    {
                        var Homologation = Result.ObjectEmbbeded.FirstOrDefault();
                        SalePrice += Homologation.SubTotalSalesValue != 0 ? Homologation.SubTotalSalesValue : Homologation.SubTotalSalesValueWithSurcharge;
                    }
                    else
                    {
                        Errors.AppendLine($"Se ha encontrado más de un Homologo para Cups parametrizado en el producto {k.CodeNameProduct}");
                        ListDocumentIPSDetailRemove.Add(k);
                        return;
                    };

                };

                k.SalePrice = SalePrice;
                k.SubTotalValue = SalePrice * k.Quantity;
                if (k.IvaPercentage > 0)
                {
                    k.IvaValue = (k.SubTotalValue * k.IvaValue / 100);
                };
                k.TotalValue = k.SubTotalValue + k.IvaValue;
            });

            Parallel.ForEach(ListDocumentIPSDetailRemove, x => {
                ListDocumentInvoiceProductSalesDetail.Remove(x);
            });

            if (ListDocumentInvoiceProductSalesDetail.Count == 0)
            {
                return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> { StateResult = false, Message = Errors.ToString(), ObjectEmbbeded = new List<Domain.Entities.DocumentInvoiceProductSalesDetail>() };
            }
            else { return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> { StateResult = true, Message = Errors.ToString(), ObjectEmbbeded = ListDocumentInvoiceProductSalesDetail }; };
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
                _documentInvoiceProductSalesDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }
}
