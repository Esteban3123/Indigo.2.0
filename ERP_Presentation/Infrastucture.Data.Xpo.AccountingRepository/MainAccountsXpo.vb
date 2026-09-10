Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.MainAccounts")> _
Public Class MainAccountsXpo
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
    Dim fLegalBookId As BookXpo
    <Association("MainAccountsXpoReferencesBookXpo")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Dim fIdAccountLevel As GeneralLedgerMainAccountsLevelsXpo
    <Association("MainAccountsXpoReferencesGeneralLedgerMainAccountsLevelsXpo")> _
    Public Property IdAccountLevel() As GeneralLedgerMainAccountsLevelsXpo
        Get
            Return fIdAccountLevel
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsLevelsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsLevelsXpo)("IdAccountLevel", fIdAccountLevel, value)
        End Set
    End Property
    Dim fIdAccountClass As AccountClassXpo
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccountClasses")> _
    Public Property IdAccountClass() As AccountClassXpo
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As AccountClassXpo)
            SetPropertyValue(Of AccountClassXpo)("IdAccountClass", fIdAccountClass, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(50)> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
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

    ''' <summary>
    ''' Gets the name of the code.
    ''' </summary>
    <PersistentAlias("concat(Number,' - ',Name)")>
    Public Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Dim accountSplit = value.Split("-")
                fNumber = accountSplit(0).Trim()
                fName = accountSplit(1).Trim()
            End If
        End Set
    End Property

    Dim fIdParent As MainAccountsXpo
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccounts")> _
    Public Property IdParent() As MainAccountsXpo
        Get
            Return fIdParent
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("IdParent", fIdParent, value)
        End Set
    End Property
    Dim fHandlesThirdParty As Boolean
    Public Property HandlesThirdParty() As Boolean
        Get
            Return fHandlesThirdParty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", fHandlesThirdParty, value)
        End Set
    End Property
    Dim fCloseThirdParty As Boolean
    Public Property CloseThirdParty() As Boolean
        Get
            Return fCloseThirdParty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CloseThirdParty", fCloseThirdParty, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("GeneralLedger_MainAccountsReferencesCommon_ThirdParty")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fReconcileAccount As Boolean
    Public Property ReconcileAccount() As Boolean
        Get
            Return fReconcileAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ReconcileAccount", fReconcileAccount, value)
        End Set
    End Property
    Dim fAvailability As Byte
    Public Property Availability() As Byte
        Get
            Return fAvailability
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Availability", fAvailability, value)
        End Set
    End Property
    Dim fHandlesCostCenter As Boolean
    Public Property HandlesCostCenter() As Boolean
        Get
            Return fHandlesCostCenter
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", fHandlesCostCenter, value)
        End Set
    End Property
    Dim fRetencionType As Byte
    Public Property RetencionType() As Byte
        Get
            Return fRetencionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetencionType", fRetencionType, value)
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
    Dim fAllowsMovement As Boolean
    Public Property AllowsMovement() As Boolean
        Get
            Return fAllowsMovement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowsMovement", fAllowsMovement, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_MainAccounts", GetType(JournalVoucherDetailsXpo))> _
    Public ReadOnly Property GeneralLedger_JournalVoucherDetailsCollection() As XPCollection(Of JournalVoucherDetailsXpo)
        Get
            Return GetCollection(Of JournalVoucherDetailsXpo)("GeneralLedger_JournalVoucherDetailsCollection")
        End Get
    End Property
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccounts", GetType(MainAccountsXpo))> _
    Public ReadOnly Property GeneralLedger_MainAccountsCollection() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("GeneralLedger_MainAccountsCollection")
        End Get
    End Property
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesGeneralLedger_MainAccounts", GetType(GeneralLedgerBalanceXpo))>
    Public ReadOnly Property GeneralLedger_GeneralLedgerBalanceCollection() As XPCollection(Of GeneralLedgerBalanceXpo)
        Get
            Return GetCollection(Of GeneralLedgerBalanceXpo)("GeneralLedger_GeneralLedgerBalanceCollection")
        End Get
    End Property

    <Association("GeneralLedger_MainAccountRestrictionsReferencesGeneralLedger_MainAccounts", GetType(MainAccountRestrictionsXpo))>
    Public ReadOnly Property GeneralLedger_MainAccountRestrictionsCollection() As XPCollection(Of MainAccountRestrictionsXpo)
        Get
            Return GetCollection(Of MainAccountRestrictionsXpo)("GeneralLedger_MainAccountRestrictionsCollection")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
