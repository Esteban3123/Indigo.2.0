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
    Public Property IdAccountClass() As Integer
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountClass", fIdAccountClass, value)
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
    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
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

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
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
    ''' <summary>
    ''' Gets the name of the code.
    ''' </summary>
    ''' <value>
    ''' The name of the code.
    ''' </value>
    <Size(50)> _
    <PersistentAlias("concat(Number,' - ',Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_MainAccounts", GetType(MainAccountsXpo))> _
    Public ReadOnly Property GeneralLedger_MainAccountsCollection() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("GeneralLedger_MainAccountsCollection")
        End Get
    End Property
    <Association("Portfolio_PortfolioNoteConceptReferencesGeneralLedger_MainAccounts", GetType(PortfolioPortfolioNoteConceptXpo))> _
    Public ReadOnly Property Portfolio_PortfolioNoteConcept() As XPCollection(Of PortfolioPortfolioNoteConceptXpo)
        Get
            Return GetCollection(Of PortfolioPortfolioNoteConceptXpo)("Portfolio_PortfolioNoteConcept")
        End Get
    End Property
    <Association("cuentaContable", GetType(PortfolioAccountReceivableAccountingXpo))> _
    Public ReadOnly Property Portfolio_AccountReceivableAccounting() As XPCollection(Of PortfolioAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingXpo)("Portfolio_AccountReceivableAccounting")
        End Get
    End Property
    <Association("AccountReceivableDocumentXpoReferencesMainAccountsXpo", GetType(AccountReceivableDocumentXpo))> _
    Public ReadOnly Property AccountReceivableDocumentXpo() As XPCollection(Of AccountReceivableDocumentXpo)
        Get
            Return GetCollection(Of AccountReceivableDocumentXpo)("AccountReceivableDocumentXpo")
        End Get
    End Property

    <Association("Portfolio_PortfolioInitialBalanceAccountReceivableAccountingReferencesGeneralLedger_MainAccounts", GetType(PortfolioInitialBalanceAccountReceivableAccountingXpo))> _
    Public ReadOnly Property Portfolio_PortfolioInitialBalanceAccountReceivableAccountings() As XPCollection(Of PortfolioInitialBalanceAccountReceivableAccountingXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableAccountingXpo)("Portfolio_PortfolioInitialBalanceAccountReceivableAccountings")
        End Get
    End Property

    <Association("PortfolioTransferDetailReferencesMainAccount", GetType(PortfolioTransferDetailXpo))> _
    Public ReadOnly Property PortfolioTransferDetailXpo() As XPCollection(Of PortfolioTransferDetailXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailXpo")
        End Get
    End Property

    <Association("PortfolioAdvanceReferencesMainAccount", GetType(PortfolioAdvanceXpo))>
    Public ReadOnly Property PortfolioAdvanceXpo() As XPCollection(Of PortfolioAdvanceXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceXpo)("PortfolioAdvanceXpo")
        End Get
    End Property

    <Association("PortfolioRevaluationDetailReferencesMainAccounts", GetType(PortfolioRevaluationDetailXpo))>
    Public ReadOnly Property PortfolioRevaluationDetailXpo() As XPCollection(Of PortfolioRevaluationDetailXpo)
        Get
            Return GetCollection(Of PortfolioRevaluationDetailXpo)("PortfolioRevaluationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
