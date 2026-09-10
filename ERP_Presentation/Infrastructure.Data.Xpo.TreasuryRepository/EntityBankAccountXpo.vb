'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' cuentas bancarias usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.EntityBankAccounts")> _
Public Class EntityBankAccountXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
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
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdBank As BankXpo
    <Association("TreasuryEntityAccountReferencesTreasuryBank")> _
    Public Property IdBank() As BankXpo
        Get
            Return fIdBank
        End Get
        Set(ByVal value As BankXpo)
            SetPropertyValue(Of BankXpo)("IdBank", fIdBank, value)
        End Set
    End Property
    Dim fIdCity As CityXpo
    <Association("TreasuryEntityAccountReferencesTreasuryCity")> _
    Public Property IdCity() As CityXpo
        Get
            Return fIdCity
        End Get
        Set(ByVal value As CityXpo)
            SetPropertyValue(Of CityXpo)("IdCity", fIdCity, value)
        End Set
    End Property
    Dim fType As Byte
    <Persistent("Type")> _
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property

    Dim fPrefix As String
    <Persistent("Prefix")> _
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property

    Dim fTypeName As String
    <PersistentAlias("Iif(Type = 1, 'Ahorro', 'Corriente')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    Dim fNumber As String
    <Persistent("Number")> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fInitialBalance As Decimal
    <Persistent("InitialBalance")> _
    Public Property InitialBalance() As Decimal
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    <Persistent("CurrentBalance")> _
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property
    Dim fRate As Decimal
    <Persistent("Rate")> _
    Public Property Rate() As Decimal
        Get
            Return fRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Rate", fRate, value)
        End Set
    End Property
    Dim fQuota As Decimal
    <Persistent("Quota")> _
    Public Property Quota() As Decimal
        Get
            Return fQuota
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quota", fQuota, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    <Persistent("InitialDate")>
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fIdMainAccount As PUCServiceXpo
    <Association("TreasuryEntityAccountReferencesTreasuryPUC")> _
    Public Property IdMainAccount() As PUCServiceXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer?
    <Persistent("IdCostCenter")>
    Public Property IdCostCenter() As Integer?
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property

    Dim fFinancialSourceId As BudgetFinancialSourceReportXpo
    <Association("EntityBankAccountReferencesFinancialSource")>
    Public Property FinancialSourceId() As BudgetFinancialSourceReportXpo
        Get
            Return fFinancialSourceId
        End Get
        Set(ByVal value As BudgetFinancialSourceReportXpo)
            SetPropertyValue(Of BudgetFinancialSourceReportXpo)("FinancialSourceId", fFinancialSourceId, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(Code,' - ',IdBank.Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Size(50)>
    <PersistentAlias("concat(Code,' - ', IdBank.Name, ' CTA. ', TypeName, ' ', Number)")>
    Public ReadOnly Property CodeBankAccount() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeBankAccount"))
        End Get
    End Property

    <Size(50)>
    <PersistentAlias("concat(IdBank.Name, ' CTA. ', TypeName, ' ', Number)")>
    Public ReadOnly Property CodeBankName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeBankAccount"))
        End Get
    End Property

    <Association("TreasuryCashReceiptsReferenceEntityBankAccount", GetType(CashReceiptsXpo))>
    Public ReadOnly Property CashReceiptsXpo() As XPCollection(Of CashReceiptsXpo)
        Get
            Return GetCollection(Of CashReceiptsXpo)("CashReceiptsXpo")
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceEntityBankAccounts")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

#End Region

#Region "Navigation Fields"

    <Association("TreasuryCancellationCheckReferencesTreasuryEntityAccount", GetType(CancellationCheckXpo))>
    Public ReadOnly Property CancellationCheckXpo() As XPCollection(Of CancellationCheckXpo)
        Get
            Return GetCollection(Of CancellationCheckXpo)("CancellationCheckXpo")
        End Get
    End Property

    <Association("TreasuryBankReconciliation_Reference_TreasuryEntityBankAccounts", GetType(BankReconciliationXpo))>
    Public ReadOnly Property BankReconciliationXpo() As XPCollection(Of BankReconciliationXpo)
        Get
            Return GetCollection(Of BankReconciliationXpo)("CancellationCheckXpo")
        End Get
    End Property

    <Association("Treasury_ConsignmentReferencesTreasury_EntityBankAccount", GetType(ConsignmentXpo))>
    Public ReadOnly Property EntityBankAccountXpo() As XPCollection(Of ConsignmentXpo)
        Get
            Return GetCollection(Of ConsignmentXpo)("EntityBankAccountXpo")
        End Get
    End Property

    <Association("Treasury_UploadBankStatementsReferencesTreasury_EntityBankAccount", GetType(UploadBankStatementsXpo))>
    Public ReadOnly Property EntityBankAccountUploadXpo() As XPCollection(Of UploadBankStatementsXpo)
        Get
            Return GetCollection(Of UploadBankStatementsXpo)("EntityBankAccountUploadXpo")
        End Get
    End Property


    <Association("TreasuryBankReconciliationAutomatic_Reference_TreasuryEntityBankAccounts", GetType(BankReconciliationAutomaticXpo))>
    Public ReadOnly Property BankReconciliationAutomaticXpo() As XPCollection(Of BankReconciliationAutomaticXpo)
        Get
            Return GetCollection(Of BankReconciliationAutomaticXpo)("CancellationCheckXpo")
        End Get
    End Property

    <Association("EntityBank_Reference_TreasuryRevaluationXpo", GetType(TreasuryRevaluationXpo))>
    Public ReadOnly Property TreasuryRevaluationXpo() As XPCollection(Of TreasuryRevaluationXpo)
        Get
            Return GetCollection(Of TreasuryRevaluationXpo)("TreasuryRevaluationXpo")
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
