Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Glosas.TransferJuridicalDebtCollectionD")> _
Public Class GlosasTransferJuridicalDebtCollectionDXpo
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
    Dim fTransferJuridicalDebtCollectionCId As GlosasTransferJuridicalDebtCollectionCXpo
    <Association("GlosasTransferJuridicalDebtCollectionDXpoReferencesGlosasTransferJuridicalDebtCollectionCXpo")> _
    Public Property TransferJuridicalDebtCollectionCId() As GlosasTransferJuridicalDebtCollectionCXpo
        Get
            Return fTransferJuridicalDebtCollectionCId
        End Get
        Set(ByVal value As GlosasTransferJuridicalDebtCollectionCXpo)
            SetPropertyValue(Of GlosasTransferJuridicalDebtCollectionCXpo)("TransferJuridicalDebtCollectionCId", fTransferJuridicalDebtCollectionCId, value)
        End Set
    End Property
    Dim fPortfolioGlosaId As Integer
    Public Property PortfolioGlosaId() As Integer
        Get
            Return fPortfolioGlosaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioGlosaId", fPortfolioGlosaId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableXpo
    <Association("GlosasTransferJuridicalDebtCollectionDXpoReferencesPortfolioAccountReceivableXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableXpo)
            SetPropertyValue(Of PortfolioAccountReceivableXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fLegalTransferValue As Decimal
    Public Property LegalTransferValue() As Decimal
        Get
            Return fLegalTransferValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LegalTransferValue", fLegalTransferValue, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(50)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fAccountReceivableDate As DateTime
    Public Property AccountReceivableDate() As DateTime
        Get
            Return fAccountReceivableDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AccountReceivableDate", fAccountReceivableDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
