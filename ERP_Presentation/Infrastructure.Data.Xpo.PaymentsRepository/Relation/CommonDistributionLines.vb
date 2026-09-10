Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.DistributionLines")> _
Public Class CommonDistributionLines
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
    '<Indexed("IX_DistributionLines")> _
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
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(500)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("CommonDistributionLinesReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fExpensesConceptId As Integer
    Public Property ExpensesConceptId() As Integer
        Get
            Return fExpensesConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExpensesConceptId", fExpensesConceptId, value)
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
    <Association("CommonDistributionLinesDetailReferencesCommonDistributionLines", GetType(CommonDistributionLinesDetail))> _
    Public ReadOnly Property CommonDistributionLinesDetail() As XPCollection(Of CommonDistributionLinesDetail)
        Get
            Return GetCollection(Of CommonDistributionLinesDetail)("CommonDistributionLinesDetail")
        End Get
    End Property
    <Association("CommonSuppliersDistributionLinesXpoReferencesCommonDistributionLines", GetType(CommonSuppliersDistributionLinesXpo))> _
    Public ReadOnly Property CommonSuppliersDistributionLinesXpo() As XPCollection(Of CommonSuppliersDistributionLinesXpo)
        Get
            Return GetCollection(Of CommonSuppliersDistributionLinesXpo)("CommonSuppliersDistributionLinesXpo")
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
