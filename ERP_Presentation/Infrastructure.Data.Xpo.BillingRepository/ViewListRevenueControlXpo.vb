Imports DevExpress.Xpo
Imports Presentation.Billing.Entities

<Persistent("Billing.ViewListRevenueControl")>
Partial Public Class ViewListServiceOrderDetailXpo
    Inherits XPLiteObject
    Implements IFolioDetail

#Region "Members"

    Dim fIdKey As String
    <Key(True)>
    Public Property IdKey() As String
        Get
            Return fIdKey
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdKey", fIdKey, value)
        End Set
    End Property

    Dim fId As Integer
    Public Property Id() As Integer Implements IFolioDetail.Id
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fServiceOrderId As Integer
    Public Property ServiceOrderId() As Integer Implements IFolioDetail.ServiceOrderId
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property

    Dim fServiceCode As String
    Public Property ServiceCode() As String Implements IFolioDetail.ServiceCode
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
        End Set
    End Property

    Dim fServiceName As String
    Public Property ServiceName() As String Implements IFolioDetail.ServiceName
        Get
            Return fServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceName", fServiceName, value)
        End Set
    End Property

    Dim fIsPOSService As Boolean
    Public Property IsPOSService() As Boolean
        Get
            Return fIsPOSService
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsPOSService", fIsPOSService, value)
        End Set
    End Property

    Dim fSurgeryNumber As Byte
    Public Property SurgeryNumber() As Byte Implements IFolioDetail.SurgeryNumber
        Get
            Return fSurgeryNumber
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("SurgeryNumber", fSurgeryNumber, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fInvoiceCategoryId As Integer?
    Public Property InvoiceCategoryId() As Integer?
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("InvoiceCategoryId", fInvoiceCategoryId, value)
        End Set
    End Property

    Dim fInvoiceCategoryCodeName As String
    Public Property InvoiceCategoryCodeName() As String
        Get
            Return fInvoiceCategoryCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("InvoiceCategoryCodeName", fInvoiceCategoryCodeName, value)
        End Set
    End Property

    Dim fPresentation As Byte
    Public Property Presentation() As Byte? Implements IFolioDetail.Presentation
        Get
            Return fPresentation
        End Get
        Set(value As Byte?)
            SetPropertyValue(Of Byte)("Presentation", fPresentation, IIf(value Is Nothing, 0, value))
        End Set
    End Property

    Dim fServiceOrderCode As String
    Public Property ServiceOrderCode() As String Implements IFolioDetail.ServiceOrderCode
        Get
            Return fServiceOrderCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ServiceOrderCode", fServiceOrderCode, value)
        End Set
    End Property

    Dim fCodeAssociateService As String
    Public Property CodeAssociateService() As String Implements IFolioDetail.CodeAssociateService
        Get
            Return fCodeAssociateService
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("CodeAssociateService", fCodeAssociateService, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fThirdPartyPatientId As Integer?
    Public Property ThirdPartyPatientId() As Integer?
        Get
            Return fThirdPartyPatientId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ThirdPartyPatientId", fThirdPartyPatientId, value)
        End Set
    End Property

    Dim fIsPackage As Boolean
    Public Property IsPackage() As Boolean Implements IFolioDetail.IsPackage
        Get
            Return fIsPackage
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsPackage", fIsPackage, value)
        End Set
    End Property

    Dim fServiceOrderDetailId As Integer
    Public Property ServiceOrderDetailId() As Integer Implements IFolioDetail.ServiceOrderDetailId
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property

    Dim fSettlementType As Byte
    Public Property SettlementType() As Byte Implements IFolioDetail.SettlementType
        Get
            Return fSettlementType
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("SettlementType", fSettlementType, value)
        End Set
    End Property

    Dim fRevenueControlDetailId As Integer
    Public Property RevenueControlDetailId() As Integer
        Get
            Return fRevenueControlDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RevenueControlDetailId", fRevenueControlDetailId, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fBillingAuthorizationId As Integer
    Public Property BillingAuthorizationId() As Integer
        Get
            Return fBillingAuthorizationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingAuthorizationId", fBillingAuthorizationId, value)
        End Set
    End Property

    Dim fInvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fInvoicedUser As String
    Public Property InvoicedUser() As String
        Get
            Return fInvoicedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicedUser", fInvoicedUser, value)
        End Set
    End Property

    Dim fRecordType As Byte
    Public Property RecordType() As Byte Implements IFolioDetail.RecordType
        Get
            Return fRecordType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RecordType", fRecordType, value)
        End Set
    End Property

    Dim fIsPOSProduct As Boolean?
    Public Property IsPOSProduct() As Boolean? Implements IFolioDetail.IsPOSProduct
        Get
            Return fIsPOSProduct
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("IsPOSProduct", fIsPOSProduct, value)
        End Set
    End Property

    Dim fFolioOrder As Byte
    Public Property FolioOrder() As Byte
        Get
            Return fFolioOrder
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioOrder", fFolioOrder, value)
        End Set
    End Property

    Dim fFolioType As Byte
    Public Property FolioType() As Byte
        Get
            Return fFolioType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioType", fFolioType, value)
        End Set
    End Property

    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fContractEntityId As Integer
    Public Property ContractEntityId() As Integer
        Get
            Return fContractEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractEntityId", fContractEntityId, value)
        End Set
    End Property

    Dim fContractEntityCodeName As String
    Public Property ContractEntityCodeName() As String
        Get
            Return fContractEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractEntityCodeName", fContractEntityCodeName, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName", fThirdPartyNitName, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fCaregroupEntityType As Byte
    Public Property CaregroupEntityType() As Byte
        Get
            Return fCaregroupEntityType
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("CaregroupEntityType", fCaregroupEntityType, value)
        End Set
    End Property

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fCareGroupCostCenterId As Integer
    Public Property CareGroupCostCenterId() As Integer
        Get
            Return fCareGroupCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupCostCenterId", fCareGroupCostCenterId, value)
        End Set
    End Property

    Dim fTotalFolio As Decimal
    Public Property TotalFolio() As Decimal
        Get
            Return fTotalFolio
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalFolio", fTotalFolio, value)
        End Set
    End Property

    Dim fGrandTotalSalesPrice As Decimal
    Public Property GrandTotalSalesPrice() As Decimal Implements IFolioDetail.GrandTotalSalesPrice
        Get
            Return fGrandTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalSalesPrice", fGrandTotalSalesPrice, value)
        End Set
    End Property

    Dim fDistributionType As Byte
    Public Property DistributionType() As Byte Implements IFolioDetail.DistributionType
        Get
            Return fDistributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DistributionType", fDistributionType, value)
        End Set
    End Property

    Dim fThirdPartySalesPrice As Decimal
    Public Property ThirdPartySalesPrice() As Decimal Implements IFolioDetail.ThirdPartySalesPrice
        Get
            Return fThirdPartySalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartySalesPrice", fThirdPartySalesPrice, value)
        End Set
    End Property

    Dim fThirdPartyPercentage As Decimal
    Public Property ThirdPartyPercentage() As Decimal Implements IFolioDetail.ThirdPartyPercentage
        Get
            Return fThirdPartyPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyPercentage", fThirdPartyPercentage, value)
        End Set
    End Property

    Dim fResponsibleRecoveryFee As Integer
    Public Property ResponsibleRecoveryFee() As Integer
        Get
            Return fResponsibleRecoveryFee
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleRecoveryFee", fResponsibleRecoveryFee, value)
        End Set
    End Property

    Dim fSubTotalPatientSalesPrice As Decimal
    Public Property SubTotalPatientSalesPrice() As Decimal Implements IFolioDetail.SubTotalPatientSalesPrice
        Get
            Return fSubTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalPatientSalesPrice", fSubTotalPatientSalesPrice, value)
        End Set
    End Property

    Dim fPatientPercentage As Decimal
    Public Property PatientPercentage() As Decimal Implements IFolioDetail.PatientPercentage
        Get
            Return fPatientPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientPercentage", fPatientPercentage, value)
        End Set
    End Property

    Dim fPatientDiscountPercentage As Decimal
    Public Property PatientDiscountPercentage() As Decimal
        Get
            Return fPatientDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientDiscountPercentage", fPatientDiscountPercentage, value)
        End Set
    End Property

    Dim fTotalPatientSalesPrice As Decimal
    Public Property TotalPatientSalesPrice() As Decimal
        Get
            Return fTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPatientSalesPrice", fTotalPatientSalesPrice, value)
        End Set
    End Property

    Dim fTotalPatientWithDiscount As Decimal
    Public Property TotalPatientWithDiscount() As Decimal
        Get
            Return fTotalPatientWithDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPatientWithDiscount", fTotalPatientWithDiscount, value)
        End Set
    End Property

    Dim fVoucherValue As Decimal
    Public Property VoucherValue() As Decimal Implements IFolioDetail.VoucherValue
        Get
            Return fVoucherValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("VoucherValue", fVoucherValue, value)
        End Set
    End Property

    Dim fPatientDiscount As Decimal
    Public Property PatientDiscount() As Decimal
        Get
            Return fPatientDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientDiscount", fPatientDiscount, value)
        End Set
    End Property

    Dim fIPSServiceCode As String
    Public Property IPSServiceCode() As String Implements IFolioDetail.IPSServiceCode
        Get
            Return fIPSServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceCode", fIPSServiceCode, value)
        End Set
    End Property

    Dim fIPSServiceName As String
    Public Property IPSServiceName() As String Implements IFolioDetail.IPSServiceName
        Get
            Return fIPSServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceName", fIPSServiceName, value)
        End Set
    End Property

    Dim fProductId As Integer
    Public Property ProductId() As Integer Implements IFolioDetail.ProductId
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String Implements IFolioDetail.ProductCode
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fProductATCCode As String
    Public Property ProductATCCode() As String Implements IFolioDetail.ProductATCCode
        Get
            Return fProductATCCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductATCCode", fProductATCCode, value)
        End Set
    End Property

    Dim fProductName As String
    Public Property ProductName() As String Implements IFolioDetail.ProductName
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fServiceBillingGroupCodeName As String
    Public Property ServiceBillingGroupCodeName() As String Implements IFolioDetail.ServiceBillingGroupCodeName
        Get
            Return fServiceBillingGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceBillingGroupCodeName", fServiceBillingGroupCodeName, value)
        End Set
    End Property

    Dim fProductBillingGroupCodeName As String
    Public Property ProductBillingGroupCodeName() As String Implements IFolioDetail.ProductBillingGroupCodeName
        Get
            Return fProductBillingGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductBillingGroupCodeName", fProductBillingGroupCodeName, value)
        End Set
    End Property

    Dim fGuidHomologation As String
    Public Property GuidHomologation() As String Implements IFolioDetail.GuidHomologation
        Get
            Return fGuidHomologation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GuidHomologation", fGuidHomologation, value)
        End Set
    End Property

    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer Implements IFolioDetail.InvoicedQuantity
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
        End Set
    End Property

    Dim fSupplyQuantity As Integer
    Public Property SupplyQuantity() As Integer Implements IFolioDetail.SupplyQuantity
        Get
            Return fSupplyQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplyQuantity", fSupplyQuantity, value)
        End Set
    End Property

    Dim fDevolutionQuantity As Integer
    Public Property DevolutionQuantity() As Integer Implements IFolioDetail.DevolutionQuantity
        Get
            Return fDevolutionQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DevolutionQuantity", fDevolutionQuantity, value)
        End Set
    End Property

    Dim fRateManualSalePrice As Decimal
    Public Property RateManualSalePrice() As Decimal Implements IFolioDetail.RateManualSalePrice
        Get
            Return fRateManualSalePrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateManualSalePrice", fRateManualSalePrice, value)
        End Set
    End Property

    Dim fSurchargeApply As String
    Public Property SurchargeApply() As String Implements IFolioDetail.SurchargeApply
        Get
            Return fSurchargeApply
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SurchargeApply", fSurchargeApply, value)
        End Set
    End Property

    Dim fRecoveryFeeType As Byte
    Public Property RecoveryFeeType() As Byte Implements IFolioDetail.RecoveryFeeType
        Get
            Return fRecoveryFeeType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RecoveryFeeType", fRecoveryFeeType, value)
        End Set
    End Property

    Dim fApplyRecoveryFee As Byte
    Public Property ApplyRecoveryFee() As Byte Implements IFolioDetail.ApplyRecoveryFee
        Get
            Return fApplyRecoveryFee
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ApplyRecoveryFee", fApplyRecoveryFee, value)
        End Set
    End Property

    Dim fCostValue As Decimal
    Public Property CostValue() As Decimal Implements IFolioDetail.CostValue
        Get
            Return fCostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CostValue", fCostValue, value)
        End Set
    End Property

    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime Implements IFolioDetail.ServiceDate
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property

    Dim fAllowValueChange As Boolean
    Public Property AllowValueChange() As Boolean Implements IFolioDetail.AllowValueChange
        Get
            Return fAllowValueChange
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowValueChange", fAllowValueChange, value)
        End Set
    End Property

    Dim fAuthorizationNumber As String
    Public Property AuthorizationNumber() As String Implements IFolioDetail.AuthorizationNumber
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property

    Dim fPerformsFunctionalUnitCodeName As String
    Public Property PerformsFunctionalUnitCodeName() As String Implements IFolioDetail.PerformsFunctionalUnitCodeName
        Get
            Return fPerformsFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsFunctionalUnitCodeName", fPerformsFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fPerformsHealthProfessionalCode As String
    Public Property PerformsHealthProfessionalCode() As String Implements IFolioDetail.PerformsHealthProfessionalCode
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
        End Set
    End Property

    Dim fPerformsProfessionalSpecialty As String
    Public Property PerformsProfessionalSpecialty() As String Implements IFolioDetail.PerformsProfessionalSpecialty
        Get
            Return fPerformsProfessionalSpecialty
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsProfessionalSpecialty", fPerformsProfessionalSpecialty, value)
        End Set
    End Property

    Dim fIPSServiceGroupCodeName As String
    Public Property IPSServiceGroupCodeName() As String Implements IFolioDetail.IPSServiceGroupCodeName
        Get
            Return fIPSServiceGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceGroupCodeName", fIPSServiceGroupCodeName, value)
        End Set
    End Property

    Dim fCostCenterCodeName As String
    Public Property CostCenterCodeName() As String Implements IFolioDetail.CostCenterCodeName
        Get
            Return fCostCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCodeName", fCostCenterCodeName, value)
        End Set
    End Property

    Dim fSubTotalSalesPrice As Decimal
    Public Property SubTotalSalesPrice() As Decimal Implements IFolioDetail.SubTotalSalesPrice
        Get
            Return fSubTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalSalesPrice", fSubTotalSalesPrice, value)
        End Set
    End Property

    Dim fThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscount() As Decimal Implements IFolioDetail.ThirdPartyDiscount
        Get
            Return fThirdPartyDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscount", fThirdPartyDiscount, value)
        End Set
    End Property

    Dim fGrandTotalDiscount As Decimal
    Public Property GrandTotalDiscount() As Decimal Implements IFolioDetail.GrandTotalDiscount
        Get
            Return fGrandTotalDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalDiscount", fGrandTotalDiscount, value)
        End Set
    End Property

    Dim fThirdPartyDiscountPercentage As Decimal
    Public Property ThirdPartyDiscountPercentage() As Decimal Implements IFolioDetail.ThirdPartyDiscountPercentage
        Get
            Return fThirdPartyDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscountPercentage", fThirdPartyDiscountPercentage, value)
        End Set
    End Property

    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal Implements IFolioDetail.TotalSalesPrice
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fCUPS As String
    Public Property CUPS() As String Implements IFolioDetail.CUPS
        Get
            Return fCUPS
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("CUPS", fCUPS, value)
        End Set
    End Property

    Dim fThirdPartyHealthAdministrator As Integer?
    Public Property ThirdPartyHealthAdministrator() As Integer?
        Get
            Return fThirdPartyHealthAdministrator
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ThirdPartyHealthAdministrator", fThirdPartyHealthAdministrator, value)
        End Set
    End Property

    Dim fAdmissionNumberPatient As String
    Public Property AdmissionNumberPatient() As String
        Get
            Return fAdmissionNumberPatient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumberPatient", fAdmissionNumberPatient, value)
        End Set
    End Property

    Dim fContractCodeName As String
    Public Property ContractCodeName() As String
        Get
            Return fContractCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractCodeName", fContractCodeName, value)
        End Set
    End Property

    Dim fApplyRIAS As Boolean
    Public Property ApplyRIAS() As Boolean Implements IFolioDetail.ApplyRIAS
        Get
            Return fApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyRIAS", fApplyRIAS, value)
        End Set
    End Property

    Dim fRIASCupsId As Integer
    Public Property RIASCupsId() As Integer Implements IFolioDetail.RIASCupsId
        Get
            Return fRIASCupsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RIASCupsId", fRIASCupsId, value)
        End Set
    End Property

    Dim fContractDescriptionCodeName As String
    Public Property ContractDescriptionCodeName() As String Implements IFolioDetail.ContractDescriptionCodeName
        Get
            Return fContractDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCodeName", fContractDescriptionCodeName, value)
        End Set
    End Property

    Dim fHasPathologies As Boolean
    Public Property HasPathologies() As Boolean Implements IFolioDetail.HasPathologies
        Get
            Return fHasPathologies
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ContractDescriptionCodeName", fHasPathologies, value)
        End Set
    End Property

    Dim fStatusFolioId As Integer?
    Public Property StatusFolioId() As Integer?
        Get
            Return fStatusFolioId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("StatusFolioId", fStatusFolioId, value)
        End Set
    End Property

    Dim fStatusFolioName As String
    Public Property StatusFolioName() As String
        Get
            Return fStatusFolioName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusFolioName", fStatusFolioName, value)
        End Set
    End Property

    Dim fIsMasterAccount As Byte
    Public Property IsMasterAccount() As Byte
        Get
            Return fIsMasterAccount
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("IsMasterAccount", fIsMasterAccount, value)
        End Set
    End Property

    Dim fMipres As String
    Public Property Mipres() As String Implements IFolioDetail.MiPres
        Get
            Return fMipres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Mipres", fMipres, value)
        End Set
    End Property

    Dim fIdMipres As String
    Public Property IdMipres() As String Implements IFolioDetail.IdMiPres
        Get
            Return fIdMipres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdMipres", fIdMipres, value)
        End Set
    End Property

    Dim fIsItemProduction As Boolean
    Public Property IsItemProduction() As Boolean Implements IFolioDetail.IsItemProduction
        Get
            Return fIsItemProduction
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsItemProduction", fIsItemProduction, value)
        End Set
    End Property

    Dim fIconType As Byte
    Public Property IconType() As Byte Implements IFolioDetail.IconType
        Get
            Return fIconType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("IconType", fIconType, value)
        End Set
    End Property

    Dim fDistributionTypeName As String
    Public Property DistributionTypeName() As String Implements IFolioDetail.DistributionTypeName
        Get
            Return fDistributionTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DistributionTypeName", fDistributionTypeName, value)
        End Set
    End Property

    Dim fProductLiquidationType As Byte
    Public Property ProductLiquidationType() As Byte Implements IFolioDetail.ProductLiquidationType
        Get
            Return fProductLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProductLiquidationType", fProductLiquidationType, value)
        End Set
    End Property

    Dim fFlagProductServiceDetail As Byte
    Public Property FlagProductServiceDetail() As Byte Implements IFolioDetail.FlagProductServiceDetail
        Get
            Return fFlagProductServiceDetail
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FlagProductServiceDetail", fFlagProductServiceDetail, value)
        End Set
    End Property

    Dim fThirdPartyResponsibleQuotaNitName As String
    Public Property ThirdPartyResponsibleQuotaNitName As String Implements IFolioDetail.ThirdPartyResponsibleQuotaNitName
        Get
            Return fThirdPartyResponsibleQuotaNitName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ThirdPartyResponsibleQuotaNitName", fThirdPartyResponsibleQuotaNitName, value)
        End Set
    End Property

    Dim fPatientQuotaResponsibleThirdPartyId As Integer?
    Public Property PatientQuotaResponsibleThirdPartyId As Integer? Implements IFolioDetail.PatientQuotaResponsibleThirdPartyId
        Get
            Return fPatientQuotaResponsibleThirdPartyId
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("PatientQuotaResponsibleThirdPartyId", fPatientQuotaResponsibleThirdPartyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String Implements IFolioDetail.CurrencyAbbreviation
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Dim fIvaPercentage As String
    Public Property IvaPercentage() As String Implements IFolioDetail.IvaPercentage
        Get
            Return fIvaPercentage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IvaPercentage", fIvaPercentage, value)
        End Set
    End Property

    Dim fIvaTotalValue As Decimal?
    Public Property IvaTotalValue() As Decimal?
        Get
            Return fIvaTotalValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("IvaTotalValue", fIvaTotalValue, value)
        End Set
    End Property

    Dim fGrossValue As Decimal
    Public Property GrossValue() As Decimal Implements IFolioDetail.GrossValue
        Get
            Return fGrossValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal?)("GrossValue", fGrossValue, value)
        End Set
    End Property

    Dim fGrandTotalTaxes As Decimal
    Public Property GrandTotalTaxes() As Decimal Implements IFolioDetail.GrandTotalTaxes
        Get
            Return fGrandTotalTaxes
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalTaxes", fGrandTotalTaxes, value)
        End Set
    End Property

    Dim fFeeNotCollectedId As Integer?
    Public Property FeeNotCollectedId As Integer? Implements IFolioDetail.FeeNotCollectedId
        Get
            Return fFeeNotCollectedId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("FeeNotCollectedId", fFeeNotCollectedId, value)
        End Set
    End Property

    Dim fBasicbillingCopayId As Integer?
    Public Property BasicbillingCopayId As Integer? Implements IFolioDetail.BasicbillingCopayId
        Get
            Return fBasicbillingCopayId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("BasicbillingCopayId", fBasicbillingCopayId, value)
        End Set
    End Property

    Dim fApplyLogicThirdPartyBeneficiary As Boolean
    Public Property ApplyLogicThirdPartyBeneficiary As Boolean
        Get
            Return fApplyLogicThirdPartyBeneficiary
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyLogicThirdPartyBeneficiary", fApplyLogicThirdPartyBeneficiary, value)
        End Set
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class