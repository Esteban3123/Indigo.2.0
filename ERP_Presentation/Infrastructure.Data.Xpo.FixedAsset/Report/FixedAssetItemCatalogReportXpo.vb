#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
#End Region


<Persistent("FixedAsset.FixedAssetItemCatalog")> _
Public Class FixedAssetItemCatalogReportXpo
    Inherits XPLiteObject

#Region "Members"

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
    Dim fCode As String
    <Indexed(Name:="IX_FixedAssetItemCatalog", Unique:=True)>
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(50)>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fActive As Boolean
    Public Property Active() As Boolean
        Get
            Return fActive
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Active", fActive, value)
        End Set
    End Property
    Dim fIncomeAccountPayableConceptId As Integer
    Public Property IncomeAccountPayableConceptId() As Integer
        Get
            Return fIncomeAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncomeAccountPayableConceptId", fIncomeAccountPayableConceptId, value)
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

    Dim fDeclarantRetentionAccountPayableConceptId As Integer
    Public Property DeclarantRetentionAccountPayableConceptId() As Integer
        Get
            Return fDeclarantRetentionAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DeclarantRetentionAccountPayableConceptId", fDeclarantRetentionAccountPayableConceptId, value)
        End Set
    End Property
    Dim fNotDeclarantRetentionAccountPayableConceptId As Integer
    Public Property NotDeclarantRetentionAccountPayableConceptId() As Integer
        Get
            Return fNotDeclarantRetentionAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotDeclarantRetentionAccountPayableConceptId", fNotDeclarantRetentionAccountPayableConceptId, value)
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

#End Region

#Region "Association"

    Dim fIncomeAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts2")>
    Public Property IncomeAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fIncomeAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("IncomeAccountId", fIncomeAccountId, value)
        End Set
    End Property
    Dim fIncomeLeasingAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts3")>
    Public Property IncomeLeasingAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fIncomeLeasingAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("IncomeLeasingAccountId", fIncomeLeasingAccountId, value)
        End Set
    End Property
    Dim fDebitLoanAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts1")>
    Public Property DebitLoanAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fDebitLoanAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("DebitLoanAccountId", fDebitLoanAccountId, value)
        End Set
    End Property
    Dim fCreditLoanAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts")>
    Public Property CreditLoanAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fCreditLoanAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("CreditLoanAccountId", fCreditLoanAccountId, value)
        End Set
    End Property
    Dim fDepreciationAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts4")>
    Public Property DepreciationAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fDepreciationAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("DepreciationAccountId", fDepreciationAccountId, value)
        End Set
    End Property
    Dim fDebitValorizationAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts5")>
    Public Property DebitValorizationAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fDebitValorizationAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("DebitValorizationAccountId", fDebitValorizationAccountId, value)
        End Set
    End Property
    Dim fCreditValorizationAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts6")>
    Public Property CreditValorizationAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fCreditValorizationAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("CreditValorizationAccountId", fCreditValorizationAccountId, value)
        End Set
    End Property
    Dim fDebitDevaluationAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts7")>
    Public Property DebitDevaluationAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fDebitDevaluationAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("DebitDevaluationAccountId", fDebitDevaluationAccountId, value)
        End Set
    End Property
    Dim fCreditDevaluationAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesGeneralLedger_MainAccounts8")>
    Public Property CreditDevaluationAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fCreditDevaluationAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("CreditDevaluationAccountId", fCreditDevaluationAccountId, value)
        End Set
    End Property

    <Association("FixedAsset_FixedAssetItemCatalogReferencesFixedAsset_FixedAssetItemCatalog", GetType(FixedAssetItemReportXpo))>
    Public ReadOnly Property FixedAssetItemReportXpo() As XPCollection(Of FixedAssetItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetItemReportXpo)("FixedAssetItemReportXpo")
        End Get
    End Property
    <Association("FK_FixedAssetReclassification_FixedAssetItemCatalogPrevious", GetType(FixedAssetReclassificationReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationWithItemCatalogPreviousReportXpo() As XPCollection(Of FixedAssetReclassificationReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationReportXpo)("FixedAssetReclassificationWithItemCatalogPreviousReportXpo")
        End Get
    End Property
    <Association("FK_FixedAssetReclassification_FixedAssetItemCatalog", GetType(FixedAssetReclassificationReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationWithItemCatalogReportXpo() As XPCollection(Of FixedAssetReclassificationReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationReportXpo)("FixedAssetReclassificationWithItemCatalogReportXpo")
        End Get
    End Property

#End Region

#Region "CustomMembers"

    <PersistentAlias("IIF(Status, 'Activo','Inactivo' )")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
