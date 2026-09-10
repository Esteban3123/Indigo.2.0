'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-07-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Portfolio.PortfolioAdvance")> _
Public Class PortfolioAdvanceXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    <Persistent("ThirdPartyId")>
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fCustomerId As Integer
    <Persistent("CustomerId")>
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fMainAccountId As MainAccountsXpo
    <Association("PortfolioAdvanceReferencesMainAccount")>
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    <Persistent("DocumentDate")> _
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fPayValue As Decimal
    <NonPersistent()>
    Public Property PayValue() As Decimal
        Get
            Return fPayValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PayValue", fValue, value)
        End Set
    End Property

    Dim fPercentValue As Decimal
    <NonPersistent()>
    Public Property PercentValue() As Decimal
        Get
            Return fPercentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentValue", fValue, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")>
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fDistributionValue As Decimal
    <Persistent("DistributionValue")>
    Public Property DistributionValue() As Decimal
        Get
            Return fDistributionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DistributionValue", fDistributionValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    <Persistent("Balance")> _
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fStatus As Decimal
    <Persistent("Status")>
    Public Property Status() As Decimal
        Get
            Return fStatus
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Status", fStatus, value)
        End Set
    End Property

    Dim fCashReceiptId As CashReceiptsXpo
    <Association("PortfolioAdvanceReferenceCashReceipt")>
    Public Property CashReceiptId() As CashReceiptsXpo
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As CashReceiptsXpo)
            SetPropertyValue(Of CashReceiptsXpo)("CashReceiptId", fCashReceiptId, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fTRMValue As Decimal?
    Public Property TRMValue() As Decimal?
        Get
            Return fTRMValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TRMValue", fTRMValue, value)
        End Set
    End Property

    Dim fValueInCurrencyHeader As Decimal?
    Public Property ValueInCurrencyHeader() As Decimal?
        Get
            Return fValueInCurrencyHeader
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("ValueInCurrencyHeader", fValueInCurrencyHeader, value)
        End Set
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferencePortfolioAdvanceXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

#End Region

#Region "PersistentAlias"
    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property Abbreviation As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(Me.EvaluateAlias("Abbreviation"))) _
                , CrossCutting.Base.SessionValues.Instance.CurrencyISO4217,
                Convert.ToString(Me.EvaluateAlias("Abbreviation")))
        End Get
    End Property
#End Region

#Region "Navigations Properties"

    <Association("Portfolio_PortfolioNoteReferences_PortfolioAdvance", GetType(PortfolioNoteXpo))>
    Public ReadOnly Property Portfolio_PortfolioNote() As XPCollection(Of PortfolioNoteXpo)
        Get
            Return GetCollection(Of PortfolioNoteXpo)("Portfolio_PortfolioNote")
        End Get
    End Property

    <Association("Portfolio_PortfolioTransferReferences_PortfolioAdvance", GetType(PortfolioTransferXpo))>
    Public ReadOnly Property Portfolio_PortfolioTransfer() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("Portfolio_PortfolioTransfer")
        End Get
    End Property

    <Association("PortfolioRevaluationDetailReferencesPortfolioAdvance", GetType(PortfolioRevaluationDetailXpo))>
    Public ReadOnly Property Portfolio_PortfolioRevaluationDetail() As XPCollection(Of PortfolioRevaluationDetailXpo)
        Get
            Return GetCollection(Of PortfolioRevaluationDetailXpo)("Portfolio_PortfolioRevaluationDetail")
        End Get
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