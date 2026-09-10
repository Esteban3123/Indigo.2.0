Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ServiceOrderDetail")> _
Public Class OrderServiceDetailXpo
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
    Dim fServiceOrderId As OrderServiceXpo
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrder")> _
    Public Property ServiceOrderId() As OrderServiceXpo
        Get
            Return fServiceOrderId
        End Get
        Set(ByVal value As OrderServiceXpo)
            SetPropertyValue(Of OrderServiceXpo)("ServiceOrderId", fServiceOrderId, value)
        End Set
    End Property
    Dim fCareGroupId As CareGroupReportXpo
    <Association("Billing_ServiceOrderDetailReferencesContract_CareGroup")> _
    Public Property CareGroupId() As CareGroupReportXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As CareGroupReportXpo)
            SetPropertyValue(Of CareGroupReportXpo)("CareGroupId", fCareGroupId, value)
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
    Dim fCUPSEntityId As EntityCUPSXpo
    <Association("Billing_ServiceOrderDetailReferencesContract_CUPSEntity")> _
    Public Property CUPSEntityId() As EntityCUPSXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As EntityCUPSXpo)
            SetPropertyValue(Of EntityCUPSXpo)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property
    Dim fIPSServiceId As ServiceIPSXpo
    <Association("Billing_ServiceOrderDetailReferencesContract_IPSService")> _
    Public Property IPSServiceId() As ServiceIPSXpo
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ServiceIPSXpo)
            SetPropertyValue(Of ServiceIPSXpo)("IPSServiceId", fIPSServiceId, value)
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
    Dim fProductId As InventoryProductReportXpo
    <Association("Billing_OrderServiceDetailReferencesInventory_InventoryProduct")> _
    Public Property ProductId() As InventoryProductReportXpo
        Get
            Return fProductId
        End Get
        Set(ByVal value As InventoryProductReportXpo)
            SetPropertyValue(Of InventoryProductReportXpo)("ProductId", fProductId, value)
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

    Dim fIsDelete As Boolean
    Public Property IsDelete() As Boolean
        Get
            Return fIsDelete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDelete", fIsDelete, value)
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
    Dim fPerformsFunctionalUnitId As Integer
    Public Property PerformsFunctionalUnitId() As Integer
        Get
            Return fPerformsFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PerformsFunctionalUnitId", fPerformsFunctionalUnitId, value)
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
    Dim fPerformsHealthProfessionalThirdPartyId As ThirdsPartyXpo
    <Association("Billing_ServiceOrderDetailReferencesCommon_ThirdParty")> _
    Public Property PerformsHealthProfessionalThirdPartyId() As ThirdsPartyXpo
        Get
            Return fPerformsHealthProfessionalThirdPartyId
        End Get
        Set(ByVal value As ThirdsPartyXpo)
            SetPropertyValue(Of ThirdsPartyXpo)("PerformsHealthProfessionalThirdPartyId", fPerformsHealthProfessionalThirdPartyId, value)
        End Set
    End Property
    'Dim fIPSServiceGroupId As ConceptReportXpo
    '<Association("Billing_ServiceOrderDetailReferencesBilling_BillingConcept")> _
    'Public Property IPSServiceGroupId() As ConceptReportXpo
    '    Get
    '        Return fIPSServiceGroupId
    '    End Get
    '    Set(ByVal value As ConceptReportXpo)
    '        SetPropertyValue(Of ConceptReportXpo)("IPSServiceGroupId", fIPSServiceGroupId, value)
    '    End Set
    'End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
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
    Dim fIncludeServiceOrderDetailId As OrderServiceDetailXpo
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrderDetail")> _
    Public Property IncludeServiceOrderDetailId() As OrderServiceDetailXpo
        Get
            Return fIncludeServiceOrderDetailId
        End Get
        Set(ByVal value As OrderServiceDetailXpo)
            SetPropertyValue(Of OrderServiceDetailXpo)("IncludeServiceOrderDetailId", fIncludeServiceOrderDetailId, value)
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
    <Association("Billing_ServiceOrderDetailReferencesBilling_ServiceOrderDetail", GetType(OrderServiceDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailCollection() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_ServiceOrderDetailCollection")
        End Get
    End Property
    <Association("Billing_ServiceOrderDetailSurgicalReferencesBilling_ServiceOrderDetail", GetType(OrderServiceDetailSurgicalXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailSurgicals() As XPCollection(Of OrderServiceDetailSurgicalXpo)
        Get
            Return GetCollection(Of OrderServiceDetailSurgicalXpo)("Billing_ServiceOrderDetailSurgicals")
        End Get
    End Property
    <Association("Billing_ServiceOrderDetailDistributionReferencesBilling_ServiceOrderDetail", GetType(OrderServiceDetailDistributionXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailDistributions() As XPCollection(Of OrderServiceDetailDistributionXpo)
        Get
            Return GetCollection(Of OrderServiceDetailDistributionXpo)("Billing_ServiceOrderDetailDistributions")
        End Get
    End Property
    <Association("Billing_InvoiceDetailReferencesBilling_ServiceOrderDetail", GetType(DetailInvoicesXpo))> _
    Public ReadOnly Property Billing_InvoiceDetails() As XPCollection(Of DetailInvoicesXpo)
        Get
            Return GetCollection(Of DetailInvoicesXpo)("Billing_InvoiceDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
