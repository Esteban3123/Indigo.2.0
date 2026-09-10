#Region "imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region


<Persistent("Budget.CPCCatalog")>
Public Class BudgetCPCCatalogXpo
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

    Dim fCPCCatalogOwnerId As Integer?
    Public Property CPCCatalogOwnerId As Integer?
        Get
            Return fCPCCatalogOwnerId
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("CPCCatalogOwnerId", fCPCCatalogOwnerId, value)
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

#Region "Custom Members"
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

    <Association("CategoryReferencesCategory", GetType(BudgetCPCCatalogXpo))>
    Public ReadOnly Property BudgetCPCCatalogXpo() As XPCollection(Of BudgetCPCCatalogXpo)
        Get
            Return GetCollection(Of BudgetCPCCatalogXpo)("BudgetCPCCatalogXpo")
        End Get
    End Property

    <Association("Budget_CategoryReferencesBudget_CPCCatalog", GetType(BudgetCategoryXpo))>
    Public ReadOnly Property Budget_Category() As XPCollection(Of BudgetCategoryXpo)
        Get
            Return GetCollection(Of BudgetCategoryXpo)("Budget_Category")
        End Get
    End Property

    <Association("Budget_BudgetAvailabilityDetailXpoReferencesBudget_CPCCatalog", GetType(BudgetAvailabilityDetailReportXpo))>
    Public ReadOnly Property BudgetAvailabilityDetailReportXpo() As XPCollection(Of BudgetAvailabilityDetailReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityDetailReportXpo)("BudgetAvailabilityDetailReportXpo")
        End Get
    End Property

    <Association("Budget_BudgetAvailabilityModificationDetailXpoReferencesBudget_CPCCatalog", GetType(BudgetAvailabilityDetailXpo))>
    Public ReadOnly Property BudgetAvailabilityDetailXpo() As XPCollection(Of BudgetAvailabilityDetailXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityDetailXpo)("BudgetAvailabilityDetailXpo")
        End Get
    End Property

    <Association("Budget_BudgetXpoReferencesBudget_CPCCatalog", GetType(BudgetXpo))>
    Public ReadOnly Property BudgetXpo() As XPCollection(Of BudgetXpo)
        Get
            Return GetCollection(Of BudgetXpo)("BudgetXpo")
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
