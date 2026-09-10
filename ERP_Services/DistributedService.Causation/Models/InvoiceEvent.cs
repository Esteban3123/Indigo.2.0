using System.Collections.Generic;

namespace DistributedService.Causation.Models
{
    public class InvoiceEvent
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public byte CutType { get; set; }
        public string Observation { get; set; }
        public byte DocumentType { get; set; }
        public decimal SubTotalService { get; set; }
        public decimal ThirdPartyDiscountValue { get; set; }
        public decimal TotalPatientSalesPrice { get; set; }
        public decimal PatientDiscount { get; set; }
        public decimal TotalPatientAccountReceivable { get; set; }
        public decimal ThirdPartySalesValue { get; set; }
        public byte Status { get; set; }
        public InvoiceCategoryEvent InvoiceCategory { get; set; }
        public UserEvent User { get; set; }
        public ClientEvent Client { get; set; }
        public CareGroupEvent CareGroup { get; set; }
        public HealthAdministratorEvent HealthAdministrator { get; set; }
        public ContractEvent Contract { get; set; }
        public PatientEvent Patient { get; set; }
        public AdmissionEvent Admission { get; set; }
        public ElectronicDocumentEvent ElectronicDocument { get; set; }
        public List<InvoiceDetailEvent> InvoiceDetails { get; set; } = new List<InvoiceDetailEvent>();
    }

    public class InvoiceDetailEvent
    {
        public BillingGroupEvent BillingGroup { get; set; }
        public CUPsEntityEvent CUPsEntity { get; set; }
        public IpsServiceEvent IpsService { get; set; }
        public ProductEvent Product { get; set; }
        public string ContractDescriptionName { get; set; }
        public string AuthorizationNumber { get; set; }
        public string ServiceDate { get; set; }
        public byte RecordType { get; set; }
        public int InvoicedQuantity { get; set; }
        public decimal? TotalSalesPrice { get; set; }
        public decimal? SubTotalPatientSalesPrice { get; set; }
        public decimal? ThirdPartySalesPrice { get; set; }
        public SurgicalEvent Surgical { get; set; }
    }

    public class SurgicalEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalSalesPrice { get; set; }
    }

    public class ProductEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CodeAlternative { get; set; }
        public string CodeAlternativeTwo { get; set; }
        public string CodeCUM { get; set; }
    }

    public class IpsServiceEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CUPsEntityEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RIPSCode { get; set; }
        public string RIPSDescription { get; set; }
    }

    public class BillingGroupEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ElectronicDocumentEvent
    {
        public byte PaymentMeans { get; set; }
        public byte PaymentMethod { get; set; }
        public string CUFE { get; set; }
        public string QR { get; set; }
        public byte StatusDIAN { get; set; }        
        public string ValidationDate { get; set; }
    }

    public class AdmissionEvent
    {
        public byte TypeAdmission { get; set; }
        public string Number { get; set; }
        public string InputDate { get; set; }
        public string EgressDate { get; set; }
        public string AuthorizationNumber { get; set; }
        public CenterAttentionEvent CenterAttention { get; set; }
        public FunctionalUnitEvent AdmissionFunctionalUnit { get; set; }
        public FunctionalUnitEvent EgressFunctionalUnit { get; set; }
    }

    public class FunctionalUnitEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CenterAttentionEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class PatientLevelEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class PatientEvent
    {
        public byte IdentificationType { get; set; }
        public string Identification { get; set; }
        public string FullName { get; set; }
        public byte PatientType { get; set; }
        public PatientLevelEvent PatientLevel { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PhoneMovil { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class ContractEvent
    {
        public string ContractNumber { get; set; }
        public string Name { get; set; }
    }

    public class HealthAdministratorEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string HealthEntityCode { get; set; }
    }

    public class CareGroupEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
    }

    public class ClientEvent
    {
        public byte IdentificationType { get; set; }
        public string Identification { get; set; }
        public string DigitVerification { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class InvoiceCategoryEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class UserEvent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
