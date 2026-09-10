Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Taxes.LowTaxLiquidationDetail")> _
Public Class TaxesLowTaxLiquidationDetailReportXpo
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
    Dim fLowTaxLiquidationId As TaxesLowTaxLiquidationReportXpo
    <Association("Taxes_LowTaxLiquidationDetailReferencesTaxes_LowTaxLiquidation")> _
    Public Property LowTaxLiquidationId() As TaxesLowTaxLiquidationReportXpo
        Get
            Return fLowTaxLiquidationId
        End Get
        Set(ByVal value As TaxesLowTaxLiquidationReportXpo)
            SetPropertyValue(Of TaxesLowTaxLiquidationReportXpo)("LowTaxLiquidationId", fLowTaxLiquidationId, value)
        End Set
    End Property
    Dim fConcept As Byte
    Public Property Concept() As Byte
        Get
            Return fConcept
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Concept", fConcept, value)
        End Set
    End Property
    Dim fNumberOfDays As Integer
    Public Property NumberOfDays() As Integer
        Get
            Return fNumberOfDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberOfDays", fNumberOfDays, value)
        End Set
    End Property
    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
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


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
