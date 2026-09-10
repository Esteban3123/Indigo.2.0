'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.ViewListCrossingAccountDetailCxC")>
Public Class ViewListCrossingAccountDetailCxCXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCrossingAccountId As Integer
    Public Property CrossingAccountId() As Integer
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property

    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fAccountReceivableAccountingId As Integer
    Public Property AccountReceivableAccountingId() As Integer
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fCrossingValue As Decimal
    Public Property CrossingValue() As Decimal
        Get
            Return fCrossingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CrossingValue", fCrossingValue, value)
        End Set
    End Property

    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fIdCashFlowConcept As Integer?
    Public Property IdCashFlowConcept() As Integer?
        Get
            Return fIdCashFlowConcept
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdCashFlowConcept", fIdCashFlowConcept, value)
        End Set
    End Property

    Dim fMainAccountDescription As String
    Public Property MainAccountDescription() As String
        Get
            Return fMainAccountDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountDescription", fMainAccountDescription, value)
        End Set
    End Property

    Dim fBillNumber As String
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
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

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fThirdPartyDescription As String
    Public Property ThirdPartyDescription() As String
        Get
            Return fThirdPartyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyDescription", fThirdPartyDescription, value)
        End Set
    End Property

    Dim fCodeNameCashFlowConcept As String
    Public Property CodeNameCashFlowConcept() As String
        Get
            Return fCodeNameCashFlowConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameCashFlowConcept", fCodeNameCashFlowConcept, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
