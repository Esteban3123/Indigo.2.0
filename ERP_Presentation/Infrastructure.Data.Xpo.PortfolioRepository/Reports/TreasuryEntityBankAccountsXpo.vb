Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Treasury.EntityBankAccounts")> _
Public Class TreasuryEntityBankAccountsXpo
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
    '<Indexed(Name:="IX_EntityBankAccount", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdBank As PayrollBankXpo
    <Association("Treasury_EntityBankAccountsReferencesPayroll_Bank")> _
    Public Property IdBank() As PayrollBankXpo
        Get
            Return fIdBank
        End Get
        Set(ByVal value As PayrollBankXpo)
            SetPropertyValue(Of PayrollBankXpo)("IdBank", fIdBank, value)
        End Set
    End Property
    Dim fIdCity As Integer
    Public Property IdCity() As Integer
        Get
            Return fIdCity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCity", fIdCity, value)
        End Set
    End Property
    Dim fType As Byte
    Public Property Type() As Byte
        Get
            Return fType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Type", fType, value)
        End Set
    End Property
    Dim fPrefix As String
    <Size(4)> _
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property
    Dim fNumber As String
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fInitialBalance As Decimal
    Public Property InitialBalance() As Decimal
        Get
            Return fInitialBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialBalance", fInitialBalance, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property
    Dim fRate As Decimal
    Public Property Rate() As Decimal
        Get
            Return fRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Rate", fRate, value)
        End Set
    End Property
    Dim fQuota As Decimal
    Public Property Quota() As Decimal
        Get
            Return fQuota
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quota", fQuota, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Treasury_EntityBankAccountsReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fFMGCounterpartMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts1")> _
    Public Property FMGCounterpartMainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fFMGCounterpartMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("FMGCounterpartMainAccountId", fFMGCounterpartMainAccountId, value)
        End Set
    End Property
    Dim fFMGExpenseMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts2")> _
    Public Property FMGExpenseMainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fFMGExpenseMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("FMGExpenseMainAccountId", fFMGExpenseMainAccountId, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    <Association("Treasury_CashReceiptsReferencesTreasury_EntityBankAccount", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsEntityBankAccount() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsEntityBankAccount")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
