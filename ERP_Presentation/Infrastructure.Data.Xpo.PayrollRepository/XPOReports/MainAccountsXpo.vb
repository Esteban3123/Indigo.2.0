Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("GeneralLedger.MainAccounts")>
Public Class MainAccountsXpo
    Inherits XPLiteObject
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

    Dim fIdAccountLevel As Integer
    Public Property IdAccountLevel() As Integer
        Get
            Return fIdAccountLevel
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountLevel", fIdAccountLevel, value)
        End Set
    End Property
    Dim fIdAccountClass As Integer
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccountClasses")>
    Public Property IdAccountClass() As Integer
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountClass", fIdAccountClass, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(50)>
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

    'Dim fIdParent As MainAccountsXpo
    '<Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccounts")>
    'Public Property IdParent() As MainAccountsXpo
    '    Get
    '        Return fIdParent
    '    End Get
    '    Set(ByVal value As MainAccountsXpo)
    '        SetPropertyValue(Of MainAccountsXpo)("IdParent", fIdParent, value)
    '    End Set
    'End Property

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
    <Size(20)>
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
    <Size(20)>
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

	Dim fHandlesCostCenterRestriction As Boolean
	Public Property HandlesCostCenterRestriction() As Boolean
		Get
			Return fHandlesCostCenterRestriction
		End Get
		Set(ByVal value As Boolean)
			SetPropertyValue(Of Boolean)("HandlesCostCenterRestriction", fHandlesCostCenterRestriction, value)
		End Set
	End Property

	<Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccounts", GetType(MainAccountsXpo))>
    Public ReadOnly Property GeneralLedger_MainAccountsCollection() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("GeneralLedger_MainAccountsCollection")
        End Get
    End Property

    <Association("Payroll_CostDistributionsReferencesGeneralLedger_MainAccounts", GetType(PayrollCostDistributionsReportXpo))>
    Public ReadOnly Property PayrollCostDistributionsReportXpo() As XPCollection(Of PayrollCostDistributionsReportXpo)
        Get
            Return GetCollection(Of PayrollCostDistributionsReportXpo)("PayrollCostDistributionsReportXpo")
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