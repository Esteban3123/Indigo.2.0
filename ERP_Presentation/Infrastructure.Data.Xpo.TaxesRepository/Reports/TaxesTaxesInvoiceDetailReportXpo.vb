Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesInvoiceDetail")> _
Public Class TaxesTaxesInvoiceDetailReportXpo
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
    Dim fTaxesInvoiceId As TaxesTaxesInvoiceReportXpo
    <Association("Taxes_TaxesInvoiceDetailReferencesTaxes_TaxesInvoice")> _
    Public Property TaxesInvoiceId() As TaxesTaxesInvoiceReportXpo
        Get
            Return fTaxesInvoiceId
        End Get
        Set(ByVal value As TaxesTaxesInvoiceReportXpo)
            SetPropertyValue(Of TaxesTaxesInvoiceReportXpo)("TaxesInvoiceId", fTaxesInvoiceId, value)
        End Set
    End Property
    Dim fLiquidationConceptId As TaxesTaxesLiquidationConceptReportXpo
    <Association("Taxes_TaxesInvoiceDetailReferencesTaxes_TaxesLiquidationConcept")> _
    Public Property LiquidationConceptId() As TaxesTaxesLiquidationConceptReportXpo
        Get
            Return fLiquidationConceptId
        End Get
        Set(ByVal value As TaxesTaxesLiquidationConceptReportXpo)
            SetPropertyValue(Of TaxesTaxesLiquidationConceptReportXpo)("LiquidationConceptId", fLiquidationConceptId, value)
        End Set
    End Property
    Dim fPercentageConcept As Decimal
    Public Property PercentageConcept() As Decimal
        Get
            Return fPercentageConcept
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageConcept", fPercentageConcept, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
