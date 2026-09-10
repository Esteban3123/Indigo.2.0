#Region "Imports"
Imports DevExpress.Xpo
#End Region

<Persistent("Budget.CCPET")>
Public Class BudgetCCPETXpo
    Inherits XPLiteObject
#Region "Members"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fItemType As Byte
    Public Property ItemType As Byte
        Get
            Return fItemType
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fCCPETOwnerId As Integer?
    Public Property CCPETOwnerId As Integer?
        Get
            Return fCCPETOwnerId
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("CCPETOwnerId", fCCPETOwnerId, value)
        End Set
    End Property

    Dim fAccountType As Boolean
    <Persistent("AccountType")>
    Public Property AccountType() As Byte
        Get
            Return fAccountType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountType", fAccountType, value)
        End Set
    End Property

    Dim fLinkAccount As Boolean
    <Persistent("LinkAccount")>
    Public Property LinkAccount() As Boolean
        Get
            Return fLinkAccount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Byte)("LinkAccount", fLinkAccount, value)
        End Set
    End Property

    Dim fStatus As Byte
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "CustomMembers"

    <PersistentAlias("Iif(ItemType = 1, 'INGRESO', 'GASTO')")>
    Public ReadOnly Property ItemTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ItemTypeName"))
        End Get
    End Property

    <Association("CategoryReferencesCategory", GetType(BudgetCCPETXpo))>
    Public ReadOnly Property BudgetCCPETXpo() As XPCollection(Of BudgetCCPETXpo)
        Get
            Return GetCollection(Of BudgetCCPETXpo)("BudgetCCPETXpo")
        End Get
    End Property

    <PersistentAlias("Iif(AccountType = False, 'Agregación.(A)', 'Cuentas de Captura.(C)')")>
    Public ReadOnly Property AccountTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AccountTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(LinkAccount = 1, 'Si', 'No')")>
    Public ReadOnly Property LinkAccountName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("LinkAccountName"))
        End Get
    End Property

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property NameCode() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NameCode"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property
#End Region

#Region "Associations"
    <Association("Budget_CategoryReferencesBudget_CCPET", GetType(BudgetCategoryXpo))>
    Public ReadOnly Property Budget_Category() As XPCollection(Of BudgetCategoryXpo)
        Get
            Return GetCollection(Of BudgetCategoryXpo)("Budget_Category")
        End Get
    End Property
#End Region

#Region "builder"
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
