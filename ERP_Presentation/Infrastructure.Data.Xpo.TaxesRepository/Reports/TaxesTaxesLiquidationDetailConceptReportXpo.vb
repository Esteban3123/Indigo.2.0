Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesLiquidationDetailConcept")> _
Public Class TaxesTaxesLiquidationDetailConceptReportXpo
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
    Dim fTaxesLiquidationDetailId As TaxesTaxesLiquidationDetailReportXpo
    <Association("Taxes_TaxesLiquidationDetailConceptReferencesTaxes_TaxesLiquidationDetail")> _
    Public Property TaxesLiquidationDetailId() As TaxesTaxesLiquidationDetailReportXpo
        Get
            Return fTaxesLiquidationDetailId
        End Get
        Set(ByVal value As TaxesTaxesLiquidationDetailReportXpo)
            SetPropertyValue(Of TaxesTaxesLiquidationDetailReportXpo)("TaxesLiquidationDetailId", fTaxesLiquidationDetailId, value)
        End Set
    End Property
    Dim fTaxesLiquidationConceptId As TaxesTaxesLiquidationConceptReportXpo
    <Association("Taxes_TaxesLiquidationDetailConceptReferencesTaxes_TaxesLiquidationConcept")> _
    Public Property TaxesLiquidationConceptId() As TaxesTaxesLiquidationConceptReportXpo
        Get
            Return fTaxesLiquidationConceptId
        End Get
        Set(ByVal value As TaxesTaxesLiquidationConceptReportXpo)
            SetPropertyValue(Of TaxesTaxesLiquidationConceptReportXpo)("TaxesLiquidationConceptId", fTaxesLiquidationConceptId, value)
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
