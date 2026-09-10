Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.PaymentsRepository

<Persistent("GeneralLedger.GeneralLedgerIVA")>
Public Class GeneralLedgerIVAXpo
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

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property

    Dim fAccountPurchaseService As GeneralLedgerMainAccountsXpo
    <Persistent("IdAccountPurchaseService")>
    <Association("GeneralLedgerIVAXpo_ReferencesMainAccounts")>
    Public Property AccountPurchaseService() As GeneralLedgerMainAccountsXpo
        Get
            Return fAccountPurchaseService
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("AccountPurchaseService", fAccountPurchaseService, value)
        End Set
    End Property

    <PersistentAlias("AccountPurchaseService.Id")>
    Public ReadOnly Property IdAccountPurchaseService() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("IdAccountPurchaseService"))
        End Get
    End Property

    Dim fAccountDebitControlFiscal As GeneralLedgerMainAccountsXpo
    <Persistent("IdAccountDebitControlFiscal")>
    <Association("GeneralLedgerIVAXpo_MainAccountDebit_ReferencesMainAccounts")>
    Public Property AccountDebitControlFiscal() As GeneralLedgerMainAccountsXpo
        Get
            Return fAccountDebitControlFiscal
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("AccountDebitControlFiscal", fAccountDebitControlFiscal, value)
        End Set


    End Property

    <PersistentAlias("AccountDebitControlFiscal.Id")>
    Public ReadOnly Property IdAccountDebitControlFiscal() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("IdAccountDebitControlFiscal"))
        End Get
    End Property

    Dim fAccountCreditControlFiscal As GeneralLedgerMainAccountsXpo
    <Persistent("IdAccountCreditControlFiscal")>
    <Association("GeneralLedgerIVAXpo_MainAccountCredit_ReferencesMainAccounts")>
    Public Property AccountCreditControlFiscal() As GeneralLedgerMainAccountsXpo
        Get
            Return fAccountCreditControlFiscal
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("AccountCreditControlFiscal", fAccountCreditControlFiscal, value)
        End Set
    End Property

    <PersistentAlias("AccountCreditControlFiscal.Id")>
    Public ReadOnly Property IdAccountCreditControlFiscal() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("IdAccountCreditControlFiscal"))
        End Get
    End Property

    <Association("GeneralLedger_References_PaymentsPaymentsNoteDetailsXpo", GetType(PaymentsPaymentsNoteDetailsXpo))>
    Public ReadOnly Property PaymentsPaymentsNoteDetailsXpo() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("PaymentsPaymentsNoteDetailsXpo")
        End Get
    End Property

    <Association("GeneralLedger_References_GeneralLedgerIVAXpo", GetType(PaymentsAccountPayableDetailConceptXpoP))>
    Public ReadOnly Property PaymentsAccountPayableDetailConceptXpoP() As XPCollection(Of PaymentsAccountPayableDetailConceptXpoP)
        Get
            Return GetCollection(Of PaymentsAccountPayableDetailConceptXpoP)("PaymentsAccountPayableDetailConceptXpoP")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class