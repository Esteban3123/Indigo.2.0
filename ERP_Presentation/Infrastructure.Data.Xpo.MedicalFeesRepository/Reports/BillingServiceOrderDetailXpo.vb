Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ServiceOrderDetail")> _
Public Class BillingServiceOrderDetailXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fServiceOrderId As BillingServiceOrderXpo
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrder")> _
    Public Property ServiceOrderId() As BillingServiceOrderXpo
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As BillingServiceOrderXpo)
            SetPropertyValue(Of BillingServiceOrderXpo)("ServiceOrderId", fServiceOrderId, value)
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
    Dim fRecordType As Byte
    Public Property RecordType() As Byte
        Get
            Return fRecordType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RecordType", fRecordType, value)
        End Set
    End Property
    Dim fCUPSEntityId As ContractCUPSEntityXpo
    <Association("Billing_ServiceOrderDetailReferencesContract_CUPSEntity")> _
    Public Property CUPSEntityId() As ContractCUPSEntityXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As ContractCUPSEntityXpo)
            SetPropertyValue(Of ContractCUPSEntityXpo)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property
    Dim fIPSServiceId As ContractIPSServiceXpo
    <Association("Billing_ServiceOrderDetailReferencesContract_IPSService")> _
    Public Property IPSServiceId() As ContractIPSServiceXpo
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXpo)
            SetPropertyValue(Of ContractIPSServiceXpo)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property
    Dim fHospitalStayId As Integer
    Public Property HospitalStayId() As Integer
        Get
            Return fHospitalStayId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HospitalStayId", fHospitalStayId, value)
        End Set
    End Property
    Dim fCUPSAssociateService As Boolean
    Public Property CUPSAssociateService() As Boolean
        Get
            Return fCUPSAssociateService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CUPSAssociateService", fCUPSAssociateService, value)
        End Set
    End Property
    Dim fCodeAssociateService As String
    <Size(50)> _
    Public Property CodeAssociateService() As String
        Get
            Return fCodeAssociateService
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAssociateService", fCodeAssociateService, value)
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
    Dim fPresentation As Byte
    Public Property Presentation() As Byte
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Presentation", fPresentation, value)
        End Set
    End Property
    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property
    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
        End Set
    End Property
    Dim fSupplyQuantity As Integer
    Public Property SupplyQuantity() As Integer
        Get
            Return fSupplyQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplyQuantity", fSupplyQuantity, value)
        End Set
    End Property
    Dim fDevolutionQuantity As Integer
    Public Property DevolutionQuantity() As Integer
        Get
            Return fDevolutionQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DevolutionQuantity", fDevolutionQuantity, value)
        End Set
    End Property
    Dim fRateManualSalePrice As Decimal
    Public Property RateManualSalePrice() As Decimal
        Get
            Return fRateManualSalePrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateManualSalePrice", fRateManualSalePrice, value)
        End Set
    End Property
    Dim fCostValue As Decimal
    Public Property CostValue() As Decimal
        Get
            Return fCostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CostValue", fCostValue, value)
        End Set
    End Property
    Dim fServiceDate As DateTime
    Public Property ServiceDate() As DateTime
        Get
            Return fServiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ServiceDate", fServiceDate, value)
        End Set
    End Property
    Dim fAuthorizationNumber As String
    <Size(20)> _
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property
    Dim fPerformsFunctionalUnitId As PayrollFunctionalUnitXpo
    <Association("Billing_ServiceOrderDetailReferencesPayroll_FunctionalUnit")> _
    Public Property PerformsFunctionalUnitId() As PayrollFunctionalUnitXpo
        Get
            Return fPerformsFunctionalUnitId
        End Get
        Set(ByVal value As PayrollFunctionalUnitXpo)
            SetPropertyValue(Of PayrollFunctionalUnitXpo)("PerformsFunctionalUnitId", fPerformsFunctionalUnitId, value)
        End Set
    End Property
    Dim fPerformsHealthProfessionalCode As String
    <Size(20)> _
    Public Property PerformsHealthProfessionalCode() As String
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
        End Set
    End Property
    Dim fPerformsProfessionalSpecialty As String
    <Size(3)> _
    Public Property PerformsProfessionalSpecialty() As String
        Get
            Return fPerformsProfessionalSpecialty
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsProfessionalSpecialty", fPerformsProfessionalSpecialty, value)
        End Set
    End Property
    Dim fPerformsHealthProfessionalThirdPartyId As Integer
    Public Property PerformsHealthProfessionalThirdPartyId() As Integer
        Get
            Return fPerformsHealthProfessionalThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PerformsHealthProfessionalThirdPartyId", fPerformsHealthProfessionalThirdPartyId, value)
        End Set
    End Property
    Dim fBillingConceptId As BillingConceptReportXpo
    <Association("Billing_ServiceOrderDetailAccountReferencesBilling_BillingConcept")> _
    Public Property BillingConceptId() As BillingConceptReportXpo
        Get
            Return fBillingConceptId
        End Get
        Set(ByVal value As BillingConceptReportXpo)
            SetPropertyValue(Of BillingConceptReportXpo)("BillingConceptId", fBillingConceptId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("Billing_ServiceOrderDetailReferencesPayroll_CostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fSettlementType As Byte
    Public Property SettlementType() As Byte
        Get
            Return fSettlementType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SettlementType", fSettlementType, value)
        End Set
    End Property
    Dim fIncludeServiceOrderDetailId As BillingServiceOrderDetailXpo
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrderDetail")> _
    Public Property IncludeServiceOrderDetailId() As BillingServiceOrderDetailXpo
        Get
            Return fIncludeServiceOrderDetailId
        End Get
        Set(ByVal value As BillingServiceOrderDetailXpo)
            SetPropertyValue(Of BillingServiceOrderDetailXpo)("IncludeServiceOrderDetailId", fIncludeServiceOrderDetailId, value)
        End Set
    End Property
    Dim fRecoveryRatio As Decimal
    Public Property RecoveryRatio() As Decimal
        Get
            Return fRecoveryRatio
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RecoveryRatio", fRecoveryRatio, value)
        End Set
    End Property
    Dim fRateManualId As Integer
    Public Property RateManualId() As Integer
        Get
            Return fRateManualId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualId", fRateManualId, value)
        End Set
    End Property
    Dim fRateManualDetailId As Integer
    Public Property RateManualDetailId() As Integer
        Get
            Return fRateManualDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualDetailId", fRateManualDetailId, value)
        End Set
    End Property
    'Dim fRateByFixedId As Integer
    'Public Property RateByFixedId() As Integer
    '    Get
    '        Return fRateByFixedId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("RateByFixedId", fRateByFixedId, value)
    '    End Set
    'End Property
    'Dim fCareGroupRateId As Integer
    'Public Property CareGroupRateId() As Integer
    '    Get
    '        Return fCareGroupRateId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("CareGroupRateId", fCareGroupRateId, value)
    '    End Set
    'End Property
    Dim fSubTotalSalesPrice As Decimal
    Public Property SubTotalSalesPrice() As Decimal
        Get
            Return fSubTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalSalesPrice", fSubTotalSalesPrice, value)
        End Set
    End Property
    Dim fThirdPartyDiscount As Decimal
    Public Property ThirdPartyDiscount() As Decimal
        Get
            Return fThirdPartyDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscount", fThirdPartyDiscount, value)
        End Set
    End Property
    Dim fThirdPartyDiscountPercentage As Decimal
    Public Property ThirdPartyDiscountPercentage() As Decimal
        Get
            Return fThirdPartyDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscountPercentage", fThirdPartyDiscountPercentage, value)
        End Set
    End Property
    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property
    Dim fGrandTotalSalesPrice As Decimal
    Public Property GrandTotalSalesPrice() As Decimal
        Get
            Return fGrandTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GrandTotalSalesPrice", fGrandTotalSalesPrice, value)
        End Set
    End Property
    Dim fSurchargeApply As Boolean
    Public Property SurchargeApply() As Boolean
        Get
            Return fSurchargeApply
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SurchargeApply", fSurchargeApply, value)
        End Set
    End Property
    Dim fSurgicalInterventionType As Byte
    Public Property SurgicalInterventionType() As Byte
        Get
            Return fSurgicalInterventionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SurgicalInterventionType", fSurgicalInterventionType, value)
        End Set
    End Property
    Dim fSurgeryNumber As Byte
    Public Property SurgeryNumber() As Byte
        Get
            Return fSurgeryNumber
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SurgeryNumber", fSurgeryNumber, value)
        End Set
    End Property
    Dim fIsFirstEvent As Boolean
    Public Property IsFirstEvent() As Boolean
        Get
            Return fIsFirstEvent
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFirstEvent", fIsFirstEvent, value)
        End Set
    End Property
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrderDetail", GetType(BillingServiceOrderDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailCollection() As XPCollection(Of BillingServiceOrderDetailXpo)
        Get
            Return GetCollection(Of BillingServiceOrderDetailXpo)("Billing_ServiceOrderDetailCollection")
        End Get
    End Property
    <Association("MedicalFees_MedicalFeesCausationReferencesBilling_ServiceOrderDetail", GetType(MedicalFeesCausationReportXpo))> _
    Public ReadOnly Property MedicalFees_MedicalFeesCausations() As XPCollection(Of MedicalFeesCausationReportXpo)
        Get
            Return GetCollection(Of MedicalFeesCausationReportXpo)("MedicalFees_MedicalFeesCausations")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
