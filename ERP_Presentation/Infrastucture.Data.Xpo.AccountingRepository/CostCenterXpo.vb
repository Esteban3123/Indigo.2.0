Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.CostCenter")> _
Public Class CostCenterXpo
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

    ''' <summary>
    ''' Gets the name of the code.
    ''' </summary>
    ''' <value>
    ''' The name of the code.
    ''' </value>
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Dim costCenterSplit = value.Split("-")
                fCode = costCenterSplit(0).Trim()
                fName = costCenterSplit(1).Trim()
            End If
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesPayroll_CostCenter", GetType(GeneralLedgerBalanceXpo))> _
    Public ReadOnly Property GeneralLedger_GeneralLedgerBalances() As XPCollection(Of GeneralLedgerBalanceXpo)
        Get
            Return GetCollection(Of GeneralLedgerBalanceXpo)("GeneralLedger_GeneralLedgerBalances")
        End Get
    End Property
    <Association("GeneralLedger_JournalVoucherDetailsReferencesPayroll_CostCenter", GetType(JournalVoucherDetailsXpo))>
    Public ReadOnly Property GeneralLedger_JournalVoucherDetailsCollection() As XPCollection(Of JournalVoucherDetailsXpo)
        Get
            Return GetCollection(Of JournalVoucherDetailsXpo)("GeneralLedger_JournalVoucherDetailsCollection")
        End Get
    End Property

    <Association("GeneralLedger_MainAccountRestrictionsReferencesPayroll_CostCenter", GetType(MainAccountRestrictionsXpo))>
    Public ReadOnly Property GeneralLedger_MainAccountRestrictions() As XPCollection(Of MainAccountRestrictionsXpo)
        Get
            Return GetCollection(Of MainAccountRestrictionsXpo)("GeneralLedger_MainAccountRestrictions")
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
