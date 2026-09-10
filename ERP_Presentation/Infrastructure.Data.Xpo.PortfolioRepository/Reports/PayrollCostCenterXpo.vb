Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.CostCenter")> _
Public Class PayrollCostCenterXpo
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
    Dim fCodeName As String
    'columna que devuelve el código y el nombre concatenado
    <Size(220)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
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
    <Association("PortfolioAccountReceivableAccountingReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingReportXpo)("PortfolioAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioTransferReportXpo))> _
    Public ReadOnly Property PortfolioTransferReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioTransferDetailReportXpo))> _
    Public ReadOnly Property PortfolioTransferDetailReportXpo() As XPCollection(Of PortfolioTransferDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailReportXpo)("PortfolioTransferDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteDetailReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioNoteDetailReportXpo))> _
    Public ReadOnly Property PortfolioNoteDetailReportXpo() As XPCollection(Of PortfolioNoteDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteDetailReportXpo)("PortfolioNoteDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioInitialBalanceAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)("PortfolioInitialBalanceAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAdvanceReportXpoReferencesPayrollCostCenterXpo", GetType(PortfolioInitialBalanceAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAdvanceReportXpo() As XPCollection(Of PortfolioInitialBalanceAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAdvanceReportXpo)("PortfolioInitialBalanceAdvanceReportXpo")
        End Get
    End Property
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPayroll_CostCenter")> _
    Public ReadOnly Property PortfolioAccountReceivableDocumentDetail() As XPCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)("PortfolioAccountReceivableDocumentDetail")
        End Get
    End Property
    <Association("PortfolioTransferOtherConceptReportXpoReferencesPayrollCostCenterXpo")> _
    Public ReadOnly Property PortfolioTransferOtherConceptReportXpo() As XPCollection(Of PortfolioTransferOtherConceptReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferOtherConceptReportXpo)("PortfolioTransferOtherConceptReportXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableDocumentReportXpoReferencesPayrollCostCenterXpo")> _
    Public ReadOnly Property PortfolioAccountReceivableDocumentReportXpo() As XPCollection(Of PortfolioAccountReceivableDocumentReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentReportXpo)("PortfolioAccountReceivableDocumentReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
