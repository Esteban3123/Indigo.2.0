Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesLiquidationConcept")> _
Public Class TaxesTaxesLiquidationConceptReportXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    <Association("Taxes_TaxesInvoiceDetailReferencesTaxes_TaxesLiquidationConcept", GetType(TaxesTaxesInvoiceDetailReportXpo))> _
    Public ReadOnly Property TaxesTaxesInvoiceDetailReportXpo() As XPCollection(Of TaxesTaxesInvoiceDetailReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesInvoiceDetailReportXpo)("TaxesTaxesInvoiceDetailReportXpo")
        End Get
    End Property
    <Association("Taxes_TaxesLiquidationDetailConceptReferencesTaxes_TaxesLiquidationConcept", GetType(TaxesTaxesLiquidationDetailConceptReportXpo))> _
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
