Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesLiquidationDetail")> _
Public Class TaxesLiquidationDetailXpo
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

    Dim fTaxesLiquidationId As TaxesLiquidationXpo
    <Association("TaxesLiquidationDetailItemReferenceTaxesLiquidation")> _
    Public Property TaxesLiquidationId() As TaxesLiquidationXpo
        Get
            Return fTaxesLiquidationId
        End Get
        Set(ByVal value As TaxesLiquidationXpo)
            SetPropertyValue(Of TaxesLiquidationXpo)("TaxesLiquidationId", fTaxesLiquidationId, value)
        End Set
    End Property

    Dim fTaxesPropertyId As TaxesPropertyXpo
    <Association("TaxesLiquidationDetailItemReferenceTaxesProperty")> _
    Public Property TaxesPropertyId() As TaxesPropertyXpo
        Get
            Return fTaxesPropertyId
        End Get
        Set(ByVal value As TaxesPropertyXpo)
            SetPropertyValue(Of TaxesPropertyXpo)("TaxesPropertyId", fTaxesPropertyId, value)
        End Set
    End Property

    Dim fAppraisal As Decimal
    Public Property Appraisal() As Decimal
        Get
            Return fAppraisal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Appraisal", fAppraisal, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("TaxesLiquidationDetailItemReferenceThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fRateOwner As Decimal
    Public Property RateOwner() As Decimal
        Get
            Return fRateOwner
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateOwner", fRateOwner, value)
        End Set
    End Property

    Dim fPercentageOwner As Decimal
    Public Property PercentageOwner() As Decimal
        Get
            Return fPercentageOwner
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageOwner", fPercentageOwner, value)
        End Set
    End Property

    Dim fTotalValueTax As Decimal
    Public Property TotalValueTax() As Decimal
        Get
            Return fTotalValueTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValueTax", fTotalValueTax, value)
        End Set
    End Property

    Dim fValueOwner As Decimal
    Public Property ValueOwner() As Decimal
        Get
            Return fValueOwner
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueOwner", fValueOwner, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
