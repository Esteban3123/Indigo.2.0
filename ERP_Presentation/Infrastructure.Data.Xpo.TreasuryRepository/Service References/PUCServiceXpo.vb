'************************************************************
' Assembly         : Infraestructure.Accounting.AddressServiceXpo
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 05-05-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region
<Persistent("GeneralLedger.MainAccounts")> _
Public Class PUCServiceXpo
    Inherits XPLiteObject

#Region "Members"
    Private _RetencionType As Integer
    Private _HandlesCostCenter As Boolean
    Private _HandlesThirdParty As Boolean
    Private _IdParent As String
    Private _Name As String
    Private _Number As String
    Private _Id As Object
    Private _ReconcileAccount As Boolean
#End Region

#Region "Fields"

    ''' <summary>
    ''' Gets or sets the identifier.
    ''' </summary>
    ''' <value>
    ''' The identifier.
    ''' </value>
    <Key(True)>
    <Persistent("Id")> _
    Public Property Id As Integer
        Get
            Return _Id
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim _IdAccountLevel As Integer
    ''' <summary>
    ''' Gets or sets the parent identifier.
    ''' </summary>
    ''' <value>
    ''' The parent identifier.
    ''' </value>
    <Persistent("IdAccountLevel")> _
    Public Property IdAccountLevel As Integer
        Get
            Return _IdAccountLevel
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("IdAccountLevel", _IdAccountLevel, value)
        End Set
    End Property

    Dim _IdAccountClass As Integer
    ''' <summary>
    ''' Gets or sets the parent identifier.
    ''' </summary>
    ''' <value>
    ''' The parent identifier.
    ''' </value>
    <Persistent("IdAccountClass")> _
    Public Property IdAccountClass As Integer
        Get
            Return _IdAccountClass
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("IdAccountClass", _IdAccountClass, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the account code.
    ''' </summary>
    ''' <value>
    ''' The account code.
    ''' </value>
    <Persistent("Number")> _
    Public Property Number As String
        Get
            Return _Number
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Number", _Number, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the account.
    ''' </summary>
    ''' <value>
    ''' The name of the account.
    ''' </value>
    <Persistent("Name")> _
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the parent identifier.
    ''' </summary>
    ''' <value>
    ''' The parent identifier.
    ''' </value>
    <Persistent("IdParent")> _
    Public Property IdParent As Integer
        Get
            Return _IdParent
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("IdParent", _IdParent, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [handles third].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [handles third]; otherwise, <c>false</c>.
    ''' </value>
    <Persistent("HandlesThirdParty")> _
    Public Property HandlesThirdParty As Boolean
        Get
            Return _HandlesThirdParty
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", _HandlesThirdParty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [handles center].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [handles center]; otherwise, <c>false</c>.
    ''' </value>
    <Persistent("HandlesCostCenter")> _
    Public Property HandlesCostCenter As Boolean
        Get
            Return _HandlesCostCenter
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", _HandlesCostCenter, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the type retencion.
    ''' </summary>
    ''' <value>
    ''' The type retencion.
    ''' </value>
    <Persistent("RetencionType")> _
    Public Property RetencionType As Integer
        Get
            Return _RetencionType
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("RetencionType", _RetencionType, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the type retencion.
    ''' </summary>
    ''' <value>
    ''' The type retencion.
    ''' </value>
    <Persistent("ReconcileAccount")>
    Public Property ReconcileAccount As Boolean
        Get
            Return _ReconcileAccount
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ReconcileAccount", _ReconcileAccount, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets the name of the code.
    ''' </summary>
    <PersistentAlias("concat(Number,' - ',Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property

    <Association("TreasuryExpenseConceptReferencesPUC", GetType(ExpenseConceptXpo))> _
    Public ReadOnly Property ExpenseConceptXpo() As XPCollection(Of ExpenseConceptXpo)
        Get
            Return GetCollection(Of ExpenseConceptXpo)("ExpenseConceptXpo")
        End Get
    End Property

    <Association("TreasuryEntityAccountReferencesTreasuryPUC", GetType(EntityBankAccountXpo))> _
    Public ReadOnly Property EntityBankAccountXpo() As XPCollection(Of EntityBankAccountXpo)
        Get
            Return GetCollection(Of EntityBankAccountXpo)("EntityBankAccountXpo")
        End Get
    End Property

    <Association("TreasuryNoteConceptReferencesTreasuryPUC", GetType(NoteConceptXpo))> _
    Public ReadOnly Property NoteConceptXpo() As XPCollection(Of NoteConceptXpo)
        Get
            Return GetCollection(Of NoteConceptXpo)("NoteConceptXpo")
        End Get
    End Property

    <Association("TreasuryCashReceiptConceptRelationPUC", GetType(CashReceiptConceptXpo))> _
    Public ReadOnly Property CashReceiptConceptXpo() As XPCollection(Of CashReceiptConceptXpo)
        Get
            Return GetCollection(Of CashReceiptConceptXpo)("CashReceiptConceptXpo")
        End Get
    End Property

    <Association("TreasuryCashRegisterReferencesTreasuryPUC", GetType(CashRegisterXpo))> _
    Public ReadOnly Property CashRegisterXpo() As XPCollection(Of CashRegisterXpo)
        Get
            Return GetCollection(Of CashRegisterXpo)("CashRegisterXpo")
        End Get
    End Property
#End Region

#Region "Constructors"
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
