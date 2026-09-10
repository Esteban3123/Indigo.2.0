//'************************************************************
//' Assembly         : Application.Inventory
//' Author           : Carlos Ernesto Cordoba
//' Created          : 08/02/2016
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Inventory.EntranceVoucher;
using Application.Inventory.InventoryAdjustment;
using Application.Inventory.LoanMerchandiseDevolution;
using Application.Inventory.PharmaceuticalDispensing;
using Application.Inventory.PharmaceuticalDispensingDevolution;
using Application.Inventory.RemissionDevolution;
using Application.Inventory.RemissionEntrance;
using Application.Inventory.TransferOrder;
using Application.Inventory.TransferOrderDevolution;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Inventory.InventoryMassiveConfirm
{
    public class InventoryMassiveConfirmAdminService : IInventoryMassiveConfirmAdminService
    {
        private IRemissionEntranceAdminService _remissionEntranceAdminService;
        private IRemissionEntranceRepository _remissionEntranceRepository;
        private IInventoryAdjustmentAdminService _inventoryAdjustmentAdminService;
        private IInventoryAdjustmentRepository _inventoryAdjustmentRepository;
        private IPharmaceuticalDispensingAdminService _pharmaceuticalDispensingAdminService;
        private IPharmaceuticalDispensingRepository _pharmaceuticalDispensingRepository;
        private IRemissionDevolutionAdminService _remissionDevolutionAdminService;
        private IRemissionDevolutionRepository _remissionDevolutionRepository;
        private ITransferOrderDevolutionAdminService _transferOrderDevolutionAdminService;
        private ITransferOrderDevolutionRepository _transferOrderDevolutionRepository;
        private IEntranceVoucherAdminService _entranceVoucherAdminService;
        private IEntranceVoucherRepository _entranceVoucherRepository;
        private IPharmaceuticalDispensingDevolutionAdminService _pharmaceuticalDispensingDevolutionAdminService;
        private IPharmaceuticalDispensingDevolutionRepository _pharmaceuticalDispensingDevolutionRepository;
        private ILoanMerchandiseDevolutionAdminService _loanMerchandiseDevolutionAdminService;
        private ILoanMerchandiseDevolutionRepository _loanMerchandiseDevolutionRepository;
        private ITransferOrderAdminService _transferOrderAdminService;
        private ITransferOrderRepository _transferOrderRepository;

        public InventoryMassiveConfirmAdminService(IRemissionEntranceAdminService remissionEntranceAdminService, IRemissionEntranceRepository remissionEntranceRepository,
            IInventoryAdjustmentAdminService inventoryAdjustmentAdminService, IInventoryAdjustmentRepository inventoryAdjustmentRepository, IPharmaceuticalDispensingAdminService pharmaceuticalDispensingAdminService,
            IPharmaceuticalDispensingRepository pharmaceuticalDispensingRepository, ITransferOrderDevolutionAdminService transferOrderDevolutionAdminService, ITransferOrderDevolutionRepository transferOrderDevolutionRepository,
            IEntranceVoucherAdminService entranceVoucherAdminService, IEntranceVoucherRepository entranceVoucherRepository, IPharmaceuticalDispensingDevolutionRepository pharmaceuticalDispensingDevolutionRepository,
            IPharmaceuticalDispensingDevolutionAdminService pharmaceuticalDispensingDevolutionAdminService, ILoanMerchandiseDevolutionAdminService loanMerchandiseDevolutionAdminService,
            ILoanMerchandiseDevolutionRepository loanMerchandiseDevolutionRepository, ITransferOrderAdminService transferOrderAdminService, ITransferOrderRepository transferOrderRepository,
            IRemissionDevolutionAdminService remissionDevolutionAdminService, IRemissionDevolutionRepository remissionDevolutionRepository)
        {
            _remissionEntranceAdminService = remissionEntranceAdminService;
            _remissionEntranceRepository = remissionEntranceRepository;
            _inventoryAdjustmentAdminService = inventoryAdjustmentAdminService;
            _inventoryAdjustmentRepository = inventoryAdjustmentRepository;
            _pharmaceuticalDispensingAdminService = pharmaceuticalDispensingAdminService;
            _pharmaceuticalDispensingRepository = pharmaceuticalDispensingRepository;
            _transferOrderDevolutionAdminService = transferOrderDevolutionAdminService;
            _transferOrderDevolutionRepository = transferOrderDevolutionRepository;
            _entranceVoucherAdminService = entranceVoucherAdminService;
            _entranceVoucherRepository = entranceVoucherRepository;
            _pharmaceuticalDispensingDevolutionAdminService = pharmaceuticalDispensingDevolutionAdminService;
            _pharmaceuticalDispensingDevolutionRepository = pharmaceuticalDispensingDevolutionRepository;
            _loanMerchandiseDevolutionAdminService = loanMerchandiseDevolutionAdminService;
            _loanMerchandiseDevolutionRepository = loanMerchandiseDevolutionRepository;
            _transferOrderAdminService = transferOrderAdminService;
            _transferOrderRepository = transferOrderRepository;
            _remissionDevolutionAdminService = remissionDevolutionAdminService;
            _remissionDevolutionRepository = remissionDevolutionRepository;
        }

        public async Task<ActionResult<Tuple<string, int>>> ConfirmInventoryDocument(int processId, string code, AuditMessage audit)
        {
            Tuple<string, int> objectEmbbeded = null;

            switch (processId)
            {
                case 317://Remision de entrada
                    return ConfirmRemissionEntrance(code, audit);

                case 1610://ajuste de inventario
                    return await ConfirmInventoryAdjustment(code, audit);

                case 322://dispensacion farmaceutica
                    return ConfirmPharmaceuticalDispensing(code, audit);

                case 329://devolucion de remisiones
                    return await ConfirmRemissionDevolutionAsync(code, audit);

                case 332://devolucion de orden de traslado
                    return ConfirmTransferOrderDevolution(code, audit);

                case 1402://comprobante de entrada
                    return await ConfirmEntranceVoucherAsync(code, audit);

                case 1516://devolucion de dispensacion
                    return ConfirmPharmaceuticalDispensingDevolution(code, audit);

                case 1518://devolucion de prestamo
                    return await ConfirmLoanMerchandiseDevolution(code, audit);

                case 1519://orden de traslado
                    return ConfirmTransferOrder(code, audit);

                default:
                    objectEmbbeded = new Tuple<string, int>("No se encontraron documentos para procesar ", 2);
                    return new ActionResult<Tuple<string, int>> { ObjectEmbbeded = objectEmbbeded };
            }
        }

        public ActionResult<List<Tuple<string, int>>> ConfirmInventoryDocuments(int processId, List<string> listDocuments, AuditMessage audit)
        {
            switch (processId)
            {
                case 317://Remision de entrada
                    return ConfirmRemissionEntrance(listDocuments, audit);

                case 1610://ajuste de inventario
                    return ConfirmInventoryAdjustment(listDocuments, audit);

                case 322://dispensacion farmaceutica
                    return ConfirmPharmaceuticalDispensing(listDocuments, audit);

                case 329://devolucion de remisiones
                    return ConfirmRemissionDevolutionAsync(listDocuments, audit);

                case 332://devolucion de orden de traslado
                    return ConfirmTransferOrderDevolution(listDocuments, audit);

                case 1402://comprobante de entrada
                    return ConfirmEntranceVoucherAsync(listDocuments, audit);

                case 1516://devolucion de dispensacion
                    return ConfirmPharmaceuticalDispensingDevolution(listDocuments, audit);

                case 1518://devolucion de prestamo
                    return ConfirmLoanMerchandiseDevolution(listDocuments, audit);

                case 1519://orden de traslado
                    return ConfirmTransferOrder(listDocuments, audit);

                default:
                    List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();
                    listResult.Add(new Tuple<string, int>("No se encontraron documentos para procesar ", 2));
                    return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult };
            }
        }

        private ActionResult<Tuple<string, int>> ConfirmRemissionEntrance(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _remissionEntranceRepository.ListRemissionEntranceMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = _remissionEntranceAdminService.ConfirmRemissionEntrance(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la remisión de entrada " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la remisión de entrada " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmRemissionEntrance(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(code =>
            {
                var result = ConfirmRemissionEntrance(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private async Task<ActionResult<Tuple<string, int>>> ConfirmInventoryAdjustment(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _inventoryAdjustmentRepository.ListInventoryAdjustmentMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = await _inventoryAdjustmentAdminService.ConfirmInventoryAdjustment(document, audit, document.OperatingUnitId);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente el ajuste de inventario " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo el ajuste de inventario " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmInventoryAdjustment(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(async code =>
            {
                var result = await ConfirmInventoryAdjustment(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private ActionResult<Tuple<string, int>> ConfirmPharmaceuticalDispensing(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _pharmaceuticalDispensingRepository.ListPharmaceuticalDispensingMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = _pharmaceuticalDispensingAdminService.SavePharmaceuticalDispensing(document, audit, document.AffectInventory);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la dispensación farmaceutica " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la dispensación farmaceutica " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmPharmaceuticalDispensing(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(code =>
            {
                var result = ConfirmPharmaceuticalDispensing(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private async Task<ActionResult<Tuple<string, int>>> ConfirmRemissionDevolutionAsync(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _remissionDevolutionRepository.ListRemissionDevolutionMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = await _remissionDevolutionAdminService.ConfirmRemissionDevolutionAsync(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la devolución de remisión " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la devolución de remisión " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }
        
        private ActionResult<List<Tuple<string, int>>> ConfirmRemissionDevolutionAsync(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(async code =>
            {
                var result = await ConfirmRemissionDevolutionAsync(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private ActionResult<Tuple<string, int>> ConfirmTransferOrderDevolution(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var document = _transferOrderDevolutionRepository.GetTransferOrderDevolutionByCode(code);

            if (document != null)
            {   
                document.Status = 2;
                var result = _transferOrderDevolutionAdminService.SaveTransferOrderDevolution(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la devolución de orden de traslado " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la devolución de orden de traslado " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }
        
        private ActionResult<List<Tuple<string, int>>> ConfirmTransferOrderDevolution(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(code =>
            {
                var result = ConfirmTransferOrderDevolution(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }
        
        private async Task<ActionResult<Tuple<string, int>>> ConfirmEntranceVoucherAsync(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _entranceVoucherRepository.ListEntranceVoucherMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = await _entranceVoucherAdminService.ConfirmEntranceVoucherAsync(document, audit, "");
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente el comprobante de entrada " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo el comprobante de entrada " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmEntranceVoucherAsync(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(async code =>
            {
                var result = await ConfirmEntranceVoucherAsync(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private ActionResult<Tuple<string, int>> ConfirmPharmaceuticalDispensingDevolution(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _pharmaceuticalDispensingDevolutionRepository.ListPharmaceuticalDispensingDevolutionMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = _pharmaceuticalDispensingDevolutionAdminService.SavePharmaceuticalDispensingDevolution(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la devolución de dispensación " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la devolución de dispensación " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmPharmaceuticalDispensingDevolution(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(code =>
            {
                var result = ConfirmPharmaceuticalDispensingDevolution(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private async Task <ActionResult<Tuple<string, int>>> ConfirmLoanMerchandiseDevolution(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var documents = _loanMerchandiseDevolutionRepository.ListLoanMerchandiseDevolutionMassiveConfirm(new List<string>(new string[] { code }));

            if (documents != null && documents.Count > 0)
            {
                var document = documents.First();
                document.Status = 2;
                var result = await _loanMerchandiseDevolutionAdminService.ConfirmLoandMerchadiseDevolution(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la devolución de prestamo de mercancia " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la devolución de prestamo de mercancia " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmLoanMerchandiseDevolution(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(async code =>
            {
                var result = await ConfirmLoanMerchandiseDevolution(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
        }

        private ActionResult<Tuple<string, int>> ConfirmTransferOrder(string code, AuditMessage audit)
        {
            bool stateResult = false;
            Tuple<string, int> objectEmbbeded = null;
            var document = _transferOrderRepository.GetTransferOrderByCode(code);
            if (document != null)
            {
                document.Status = 2;
                var result = _transferOrderAdminService.SaveTrasnferOrder(document, audit);
                if (result.StateResult)
                {
                    stateResult = true;
                    objectEmbbeded = new Tuple<string, int>("Se confirmo correctamente la orden de traslado " + code, 1);
                }                    
                else
                {
                    objectEmbbeded = new Tuple<string, int>("No se confirmo la orden de traslado " + code, 2);
                 }
            }
            else
            {
                objectEmbbeded = new Tuple<string, int>(String.Format("No se encontro el documento con código '{0}'", code), 2);
            }

            return new ActionResult<Tuple<string, int>> { StateResult = stateResult, ObjectEmbbeded = objectEmbbeded };
        }

        private ActionResult<List<Tuple<string, int>>> ConfirmTransferOrder(List<string> listDocuments, AuditMessage audit)
        {
            List<Tuple<string, int>> listResult = new List<Tuple<string, int>>();            
            List<string> listDocumentsConfirmReturn = new List<string>();

            listDocuments.ForEach(code =>
            {
                var result = ConfirmTransferOrder(code, audit);

                listResult.Add(result.ObjectEmbbeded);
                if (result.StateResult)
                {
                    listDocumentsConfirmReturn.Add(code);
                }
            });
            
            return new ActionResult<List<Tuple<string, int>>> { ObjectEmbbeded = listResult, MessageResult = listDocumentsConfirmReturn };
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
                    _remissionEntranceAdminService.Dispose();
                    _inventoryAdjustmentAdminService.Dispose();
                    _pharmaceuticalDispensingAdminService.Dispose();
                    _transferOrderDevolutionAdminService.Dispose();
                    _entranceVoucherAdminService.Dispose();
                    _pharmaceuticalDispensingDevolutionAdminService.Dispose();
                    _loanMerchandiseDevolutionAdminService.Dispose();
                    _transferOrderAdminService.Dispose();
                    _remissionDevolutionAdminService.Dispose();
                }
                _remissionEntranceAdminService = null;
                _remissionEntranceRepository = null;
                _inventoryAdjustmentAdminService = null;
                _inventoryAdjustmentRepository = null;
                _pharmaceuticalDispensingAdminService = null;
                _pharmaceuticalDispensingRepository = null;
                _transferOrderDevolutionAdminService = null;
                _transferOrderDevolutionRepository = null;
                _entranceVoucherAdminService = null;
                _entranceVoucherRepository = null;
                _pharmaceuticalDispensingDevolutionAdminService = null;
                _pharmaceuticalDispensingDevolutionRepository = null;
                _loanMerchandiseDevolutionAdminService = null;
                _loanMerchandiseDevolutionRepository = null;
                _transferOrderAdminService = null;
                _transferOrderRepository = null;
                _remissionDevolutionAdminService = null;
                _remissionDevolutionRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion IDisposable Support
    }
}