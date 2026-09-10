Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.MainAccounts")> _
Public Class GeneralLedgerMainAccountsXpo
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
    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Dim fIdAccountLevel As GeneralLedgerMainAccountLevelsXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountLevelsXpo")> _
    Public Property IdAccountLevel() As GeneralLedgerMainAccountLevelsXpo
        Get
            Return fIdAccountLevel
        End Get
        Set(ByVal value As GeneralLedgerMainAccountLevelsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountLevelsXpo)("IdAccountLevel", fIdAccountLevel, value)
        End Set
    End Property
    Dim fIdAccountClass As GeneralLedgerMainAccountClassesXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountClassesXpo")> _
    Public Property IdAccountClass() As GeneralLedgerMainAccountClassesXpo
        Get
            Return fIdAccountClass
        End Get
        Set(ByVal value As GeneralLedgerMainAccountClassesXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountClassesXpo)("IdAccountClass", fIdAccountClass, value)
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
    Dim fNumberName As String
    'columna que devuelve el Número y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Number,' - '),Name)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property
    Dim fIdParent As GeneralLedgerMainAccountsXpo
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdParent() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdParent
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdParent", fIdParent, value)
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
    <Association("GeneralLedgerMainAccountsXpoReferencesCommonThirdPartyXpo")> _
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
    Dim fAllowsMovement As Boolean
    Public Property AllowsMovement() As Boolean
        Get
            Return fAllowsMovement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowsMovement", fAllowsMovement, value)
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
    <Association("GeneralLedgerMainAccountsXpoReferencesGeneralLedgerMainAccountsXpo", GetType(GeneralLedgerMainAccountsXpo))> _
    Public ReadOnly Property GeneralLedgerMainAccountsXpo() As XPCollection(Of GeneralLedgerMainAccountsXpo)
        Get
            Return GetCollection(Of GeneralLedgerMainAccountsXpo)("GeneralLedgerMainAccountsXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioAccountReceivableReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableReportXpo() As XPCollection(Of PortfolioAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableReportXpo)("PortfolioAccountReceivableReportXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableAccountingReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableAccountingReportXpo)("PortfolioAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioTransferReportXpo))> _
    Public ReadOnly Property PortfolioTransferReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferReportXpo")
        End Get
    End Property
    <Association("PortfolioTransferDetailReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioTransferDetailReportXpo))> _
    Public ReadOnly Property PortfolioTransferDetailReportXpo() As XPCollection(Of PortfolioTransferDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailReportXpo)("PortfolioTransferDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteDetailReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioNoteDetailReportXpo))> _
    Public ReadOnly Property PortfolioNoteDetailReportXpo() As XPCollection(Of PortfolioNoteDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteDetailReportXpo)("PortfolioNoteDetailReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteConceptReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioNoteConceptReportXpo))> _
    Public ReadOnly Property PortfolioNoteConceptReportXpo() As XPCollection(Of PortfolioNoteConceptReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteConceptReportXpo)("PortfolioNoteConceptReportXpo")
        End Get
    End Property
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioNoteAccountReceivableAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioNoteAccountReceivableAdvanceReportXpo() As XPCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)("PortfolioNoteAccountReceivableAdvanceReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioInitialBalanceAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)("PortfolioInitialBalanceAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo", GetType(PortfolioInitialBalanceAdvanceReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAdvanceReportXpo() As XPCollection(Of PortfolioInitialBalanceAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAdvanceReportXpo)("PortfolioInitialBalanceAdvanceReportXpo")
        End Get
    End Property
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountMainAccount() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountMainAccount")
        End Get
    End Property
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts1", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountMainAccount1() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountMainAccount1")
        End Get
    End Property
    <Association("Treasury_EntityBankAccountsReferencesGeneralLedger_MainAccounts2", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountMainAccount2() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountMainAccount2")
        End Get
    End Property
    <Association("Treasury_CashReceiptsReferencesGeneralLedger_MainAccounts", GetType(TreasuryCashReceiptsXpo))> _
    Public ReadOnly Property TreasuryCashReceiptsAccountMainAccount() As XPCollection(Of TreasuryCashReceiptsXpo)
        Get
            Return GetCollection(Of TreasuryCashReceiptsXpo)("TreasuryCashReceiptsAccountMainAccount")
        End Get
    End Property
    <Association("Portfolio_AccountReceivableDocumentDetailReferencesPortfolio_GeneralLedgerMainAccounts")> _
    Public ReadOnly Property PortfolioAccountReceivableDocumentDetail() As XPCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)("PortfolioAccountReceivableDocumentDetail")
        End Get
    End Property
    <Association("PortfolioTransferOtherConceptReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public ReadOnly Property PortfolioTransferOtherConceptReportXpo() As XPCollection(Of PortfolioTransferOtherConceptReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferOtherConceptReportXpo)("PortfolioTransferOtherConceptReportXpo")
        End Get
    End Property
    <Association("PortfolioAccountReceivableDocumentReportXpoReferencesGeneralLedgerMainAccountsXpo")> _
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
