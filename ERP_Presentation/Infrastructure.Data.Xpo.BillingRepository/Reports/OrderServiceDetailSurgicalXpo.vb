Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ServiceOrderDetailSurgical")> _
Public Class OrderServiceDetailSurgicalXpo
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
    Dim fServiceOrderDetailId As OrderServiceDetailXpo
    <Association("Billing_ServiceOrderDetailSurgicalReferencesBilling_ServiceOrderDetail")> _
    Public Property ServiceOrderDetailId() As OrderServiceDetailXpo
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As OrderServiceDetailXpo)
            SetPropertyValue(Of OrderServiceDetailXpo)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property
    Dim fIPSServiceId As ServiceIPSXpo
    <Association("Billing_ServiceOrderDetailSurgicalReferencesContract_IPSService")> _
    Public Property IPSServiceId() As ServiceIPSXpo
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ServiceIPSXpo)
            SetPropertyValue(Of ServiceIPSXpo)("IPSServiceId", fIPSServiceId, value)
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
    Dim fLiquidationPercentage As Decimal
    Public Property LiquidationPercentage() As Decimal
        Get
            Return fLiquidationPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LiquidationPercentage", fLiquidationPercentage, value)
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
    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
        End Set
    End Property

    Dim fOnlyMedicalFees As Boolean
    Public Property OnlyMedicalFees() As Boolean
        Get
            Return fOnlyMedicalFees
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("OnlyMedicalFees", fOnlyMedicalFees, value)
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
    Dim fPerformsHealthProfessionalThirdPartyId As ThirdsPartyXpo
    <Association("Billing_ServiceOrderDetailSurgicalReferencesCommon_ThirdParty")> _
    Public Property PerformsHealthProfessionalThirdPartyId() As ThirdsPartyXpo
        Get
            Return fPerformsHealthProfessionalThirdPartyId
        End Get
        Set(ByVal value As ThirdsPartyXpo)
            SetPropertyValue(Of ThirdsPartyXpo)("PerformsHealthProfessionalThirdPartyId", fPerformsHealthProfessionalThirdPartyId, value)
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
    'Dim fIPSServiceGroupId As ConceptReportXpo
    '<Association("Billing_ServiceOrderDetailSurgicalReferencesBilling_BillingConcept")> _
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
    Dim fRateManualDetailSurgicalId As Integer
    Public Property RateManualDetailSurgicalId() As Integer
        Get
            Return fRateManualDetailSurgicalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RateManualDetailSurgicalId", fRateManualDetailSurgicalId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

