Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesLiquidationDetail")> _
Public Class TaxesTaxesLiquidationDetailReportXpo
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
    Dim fTaxesLiquidationId As TaxesTaxesLiquidationReportXpo
    <Association("Taxes_TaxesLiquidationDetailReferencesTaxes_TaxesLiquidation")> _
    Public Property TaxesLiquidationId() As TaxesTaxesLiquidationReportXpo
        Get
            Return fTaxesLiquidationId
        End Get
        Set(ByVal value As TaxesTaxesLiquidationReportXpo)
            SetPropertyValue(Of TaxesTaxesLiquidationReportXpo)("TaxesLiquidationId", fTaxesLiquidationId, value)
        End Set
    End Property
    Dim fTaxesPropertyId As Integer
    Public Property TaxesPropertyId() As Integer
        Get
            Return fTaxesPropertyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TaxesPropertyId", fTaxesPropertyId, value)
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
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
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
    Dim fPercentageTax As Decimal
    Public Property PercentageTax() As Decimal
        Get
            Return fPercentageTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageTax", fPercentageTax, value)
        End Set
    End Property
    Dim fTaxesInvoiceId As Integer
    Public Property TaxesInvoiceId() As Integer
        Get
            Return fTaxesInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TaxesInvoiceId", fTaxesInvoiceId, value)
        End Set
    End Property
    <Association("Taxes_TaxesLiquidationDetailConceptReferencesTaxes_TaxesLiquidationDetail", GetType(TaxesTaxesLiquidationDetailConceptReportXpo))> _
    Public ReadOnly Property TaxesTaxesLiquidationDetailConceptReportXpo() As XPCollection(Of TaxesTaxesLiquidationDetailConceptReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesLiquidationDetailConceptReportXpo)("TaxesTaxesLiquidationDetailConceptReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
