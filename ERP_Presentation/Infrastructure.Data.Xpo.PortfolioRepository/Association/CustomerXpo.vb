
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.Customer")> _
Public Class CustomerXpo
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
    Dim fNit As String
    <Indexed(Name:="IX_Customer", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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
    Dim fEPSCode As String
    <Size(20)> _
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
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
    <Association("Portfolio_PortfolioNoteReferencesCommon_Customer", GetType(PortfolioNoteXpo))> _
    Public ReadOnly Property Portfolio_PortfolioNote() As XPCollection(Of PortfolioNoteXpo)
        Get
            Return GetCollection(Of PortfolioNoteXpo)("Portfolio_PortfolioNote")
        End Get
    End Property

    <Association("Portfolio_PortfolioTransferReferencesCommon_Customer", GetType(PortfolioTransferXpo))> _
    Public ReadOnly Property Portfolio_PortfolioTransfers() As XPCollection(Of PortfolioTransferXpo)
        Get
            Return GetCollection(Of PortfolioTransferXpo)("Portfolio_PortfolioTransfers")
        End Get
    End Property
    <Size(50)> _
<PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

    <Association("AccountReceivableDocumentXpoReferencesCustomerXpo", GetType(AccountReceivableDocumentXpo))> _
    Public ReadOnly Property AccountReceivableDocumentXpo() As XPCollection(Of AccountReceivableDocumentXpo)
        Get
            Return GetCollection(Of AccountReceivableDocumentXpo)("AccountReceivableDocumentXpo")
        End Get
    End Property

    <Association("Portfolio_PortfolioInitialBalanceAccountReceivableReferencesCommon_Customer", GetType(PortfolioInitialBalanceAccountReceivableXpo))> _
    Public ReadOnly Property Portfolio_PortfolioInitialBalanceAccountReceivables() As XPCollection(Of PortfolioInitialBalanceAccountReceivableXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableXpo)("Portfolio_PortfolioInitialBalanceAccountReceivables")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
