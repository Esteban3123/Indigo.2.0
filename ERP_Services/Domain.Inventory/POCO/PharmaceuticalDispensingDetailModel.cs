using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class PharmaceuticalDispensingDetailModel
    {
        public int CareGroupId { get; set; }
        public int? HealthAdministratorId { get; set; }
        public int ThirdPartyId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public int ReturnedQuantity { get; set; }
        public string ServiceDate { get; set; }
        public int FunctionalUnitId { get; set; }
        public string OrderedHealthProfessionalCode { get; set; }
        public string OrderedProfessionalSpecialty { get; set; }
        public int OrderedHealthProfessionalThirdPartyId { get; set; }
        public string AuthorizationNumber { get; set; }
        public int LiquidationType { get; set; }
        public int? CupsEntityId { get; set; }
        public bool SurchargeApply { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AverageCost { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public decimal GrandTotalSalesPrice { get; set; }
        public int? QuotationPharmaceuticalDispensingDetailId { get; set; }
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        public decimal? FinalProductCost { get; set; }
        public decimal GrossValue { get; set; }
        public decimal TaxValue { get; set; }
        public int? IvaId { get; set; }
        public string FunctionalUnit { get; set; }
        public PharmaceuticalDispensingDetailBatchSerialModel[] PharmaceuticalDispensingDetailBatchSerial { get; set; }
        public object GeneralLedgerIVA { get; set; }
        public string CodeProduct { get; set; }
        public string MedicamentCode { get; set; }
        public string NameProduct { get; set; }
        public string UnidadMedida { get; set; }
        public int CantidadPendiente { get; set; }
        public int CantidadSolicitada { get; set; }
        public string ProductType { get; set; }
        public int idProductoHeon { get; set; }
        public string recetarioOMedica { get; set; }
        public bool GuardaGastoQX { get; set; }
        public int? IdProgramacionQXPrincipal { get; set; }
        public bool Extramural { get; set; }
        public bool Custody { get; set; }
        public int QuotationId { get; set; }
        public int AuthorizationOutsourcedServicesId { get; set; }
        public int TypeProduct { get; set; }
        public string Note { get; set; }
        public int CantidadMezcla { get; set; }

    }
}
