Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.BudgetaryValidity")> _
Public Class BudgetBudgetaryValidityReportXpo
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
    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property
    Dim fBudgetaryEntityId As BudgetBudgetaryEntityReportXpo
    <Association("Budget_BudgetaryValidityReferencesBudget_BudgetaryEntity")> _
    Public Property BudgetaryEntityId() As BudgetBudgetaryEntityReportXpo
        Get
            Return fBudgetaryEntityId
        End Get
        Set(ByVal value As BudgetBudgetaryEntityReportXpo)
            SetPropertyValue(Of BudgetBudgetaryEntityReportXpo)("BudgetaryEntityId", fBudgetaryEntityId, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fIncomeMonth As Integer
    Public Property IncomeMonth() As Integer
        Get
            Return fIncomeMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncomeMonth", fIncomeMonth, value)
        End Set
    End Property
    Dim fExpenseMonth As Integer
    <Size(2)> _
    Public Property ExpenseMonth() As Integer
        Get
            Return fExpenseMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ExpenseMonth", fExpenseMonth, value)
        End Set
    End Property
    Dim fResolutionNumber As String
    <Size(20)> _
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property
    Dim fResolutionValue As Decimal
    Public Property ResolutionValue() As Decimal
        Get
            Return fResolutionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResolutionValue", fResolutionValue, value)
        End Set
    End Property
    Dim fLegalRepresentativeId As CommonThirdPartyReportXpo
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty2")> _
    Public Property LegalRepresentativeId() As CommonThirdPartyReportXpo
        Get
            Return fLegalRepresentativeId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("LegalRepresentativeId", fLegalRepresentativeId, value)
        End Set
    End Property
    Dim fChiefBudgetOfficerId As CommonThirdPartyReportXpo
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty")> _
    Public Property ChiefBudgetOfficerId() As CommonThirdPartyReportXpo
        Get
            Return fChiefBudgetOfficerId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ChiefBudgetOfficerId", fChiefBudgetOfficerId, value)
        End Set
    End Property
    Dim fChiefFinancialOfficerId As CommonThirdPartyReportXpo
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty1")> _
    Public Property ChiefFinancialOfficerId() As CommonThirdPartyReportXpo
        Get
            Return fChiefFinancialOfficerId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ChiefFinancialOfficerId", fChiefFinancialOfficerId, value)
        End Set
    End Property
    Dim fInitialStateIncome As Integer
    Public Property InitialStateIncome() As Integer
        Get
            Return fInitialStateIncome
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialStateIncome", fInitialStateIncome, value)
        End Set
    End Property
    Dim fInitialStateExpenses As Integer
    Public Property InitialStateExpenses() As Integer
        Get
            Return fInitialStateExpenses
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialStateExpenses", fInitialStateExpenses, value)
        End Set
    End Property
    Dim fDateIncomeRecord As DateTime
    Public Property DateIncomeRecord() As DateTime
        Get
            Return fDateIncomeRecord
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateIncomeRecord", fDateIncomeRecord, value)
        End Set
    End Property
    Dim fDateExpensesRecord As DateTime
    Public Property DateExpensesRecord() As DateTime
        Get
            Return fDateExpensesRecord
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateExpensesRecord", fDateExpensesRecord, value)
        End Set
    End Property
    Dim fPACControl As Boolean
    Public Property PACControl() As Boolean
        Get
            Return fPACControl
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PACControl", fPACControl, value)
        End Set
    End Property
    Dim fConsecutiveModPTOIncome As Integer
    Public Property ConsecutiveModPTOIncome() As Integer
        Get
            Return fConsecutiveModPTOIncome
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModPTOIncome", fConsecutiveModPTOIncome, value)
        End Set
    End Property
    Dim fConsecutiveTrasPTOIncome As Integer
    Public Property ConsecutiveTrasPTOIncome() As Integer
        Get
            Return fConsecutiveTrasPTOIncome
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveTrasPTOIncome", fConsecutiveTrasPTOIncome, value)
        End Set
    End Property
    Dim fConsecutiveModPACIncome As Integer
    Public Property ConsecutiveModPACIncome() As Integer
        Get
            Return fConsecutiveModPACIncome
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModPACIncome", fConsecutiveModPACIncome, value)
        End Set
    End Property
    Dim fConsecutiveTrasPACIncome As Integer
    Public Property ConsecutiveTrasPACIncome() As Integer
        Get
            Return fConsecutiveTrasPACIncome
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveTrasPACIncome", fConsecutiveTrasPACIncome, value)
        End Set
    End Property
    Dim fConsecutiveRecognition As Integer
    Public Property ConsecutiveRecognition() As Integer
        Get
            Return fConsecutiveRecognition
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveRecognition", fConsecutiveRecognition, value)
        End Set
    End Property
    Dim fConsecutiveModRecognition As Integer
    Public Property ConsecutiveModRecognition() As Integer
        Get
            Return fConsecutiveModRecognition
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModRecognition", fConsecutiveModRecognition, value)
        End Set
    End Property
    Dim fConsecutiveCollection As Integer
    Public Property ConsecutiveCollection() As Integer
        Get
            Return fConsecutiveCollection
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveCollection", fConsecutiveCollection, value)
        End Set
    End Property
    Dim fConsecutiveModCollection As Integer
    Public Property ConsecutiveModCollection() As Integer
        Get
            Return fConsecutiveModCollection
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModCollection", fConsecutiveModCollection, value)
        End Set
    End Property
    Dim fConsecutiveModPTOExpense As Integer
    Public Property ConsecutiveModPTOExpense() As Integer
        Get
            Return fConsecutiveModPTOExpense
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModPTOExpense", fConsecutiveModPTOExpense, value)
        End Set
    End Property
    Dim fConsecutiveTrasPTOExpense As Integer
    Public Property ConsecutiveTrasPTOExpense() As Integer
        Get
            Return fConsecutiveTrasPTOExpense
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveTrasPTOExpense", fConsecutiveTrasPTOExpense, value)
        End Set
    End Property
    Dim fConsecutiveModPACExpense As Integer
    Public Property ConsecutiveModPACExpense() As Integer
        Get
            Return fConsecutiveModPACExpense
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModPACExpense", fConsecutiveModPACExpense, value)
        End Set
    End Property
    Dim fConsecutiveTrasPACExpense As Integer
    Public Property ConsecutiveTrasPACExpense() As Integer
        Get
            Return fConsecutiveTrasPACExpense
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveTrasPACExpense", fConsecutiveTrasPACExpense, value)
        End Set
    End Property
    Dim fConsecutiveCDP As Integer
    Public Property ConsecutiveCDP() As Integer
        Get
            Return fConsecutiveCDP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveCDP", fConsecutiveCDP, value)
        End Set
    End Property
    Dim fConsecutiveModCDP As Integer
    Public Property ConsecutiveModCDP() As Integer
        Get
            Return fConsecutiveModCDP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModCDP", fConsecutiveModCDP, value)
        End Set
    End Property
    Dim fConsecutiveRP As Integer
    Public Property ConsecutiveRP() As Integer
        Get
            Return fConsecutiveRP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveRP", fConsecutiveRP, value)
        End Set
    End Property
    Dim fConsecutiveModRP As Integer
    Public Property ConsecutiveModRP() As Integer
        Get
            Return fConsecutiveModRP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModRP", fConsecutiveModRP, value)
        End Set
    End Property
    Dim fConsecutiveLiabilities As Integer
    Public Property ConsecutiveLiabilities() As Integer
        Get
            Return fConsecutiveLiabilities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveLiabilities", fConsecutiveLiabilities, value)
        End Set
    End Property
    Dim fConsecutiveModLiabilities As Integer
    Public Property ConsecutiveModLiabilities() As Integer
        Get
            Return fConsecutiveModLiabilities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveModLiabilities", fConsecutiveModLiabilities, value)
        End Set
    End Property
    Dim fConsecutiveODP As Integer
    Public Property ConsecutiveODP() As Integer
        Get
            Return fConsecutiveODP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveODP", fConsecutiveODP, value)
        End Set
    End Property
    Dim fConsecutiveExtendedCDP As Integer
    Public Property ConsecutiveExtendedCDP() As Integer
        Get
            Return fConsecutiveExtendedCDP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveExtendedCDP", fConsecutiveExtendedCDP, value)
        End Set
    End Property
    Dim fConsecutiveResourceRelease As Integer
    Public Property ConsecutiveResourceRelease() As Integer
        Get
            Return fConsecutiveResourceRelease
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveResourceRelease", fConsecutiveResourceRelease, value)
        End Set
    End Property
    Dim fConsecutiveReinstatement As Integer
    Public Property ConsecutiveReinstatement() As Integer
        Get
            Return fConsecutiveReinstatement
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveReinstatement", fConsecutiveReinstatement, value)
        End Set
    End Property
    Dim fConsecutiveReservation As Integer
    Public Property ConsecutiveReservation() As Integer
        Get
            Return fConsecutiveReservation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveReservation", fConsecutiveReservation, value)
        End Set
    End Property
    Dim fConsecutiveCXP As Integer
    Public Property ConsecutiveCXP() As Integer
        Get
            Return fConsecutiveCXP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveCXP", fConsecutiveCXP, value)
        End Set
    End Property
    Dim fConsecutiveCDPVFT As Integer
    Public Property ConsecutiveCDPVFT() As Integer
        Get
            Return fConsecutiveCDPVFT
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveCDPVFT", fConsecutiveCDPVFT, value)
        End Set
    End Property
    Dim fConsecutiveRPVFT As Integer
    Public Property ConsecutiveRPVFT() As Integer
        Get
            Return fConsecutiveRPVFT
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveRPVFT", fConsecutiveRPVFT, value)
        End Set
    End Property
    Dim fConsecutiveLiabilitiesVFT As Integer
    Public Property ConsecutiveLiabilitiesVFT() As Integer
        Get
            Return fConsecutiveLiabilitiesVFT
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveLiabilitiesVFT", fConsecutiveLiabilitiesVFT, value)
        End Set
    End Property
    Dim fConsecutiveODPVFT As Integer
    Public Property ConsecutiveODPVFT() As Integer
        Get
            Return fConsecutiveODPVFT
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveODPVFT", fConsecutiveODPVFT, value)
        End Set
    End Property
    Dim fConsecutiveSuspensionPTO As Integer
    Public Property ConsecutiveSuspensionPTO() As Integer
        Get
            Return fConsecutiveSuspensionPTO
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveSuspensionPTO", fConsecutiveSuspensionPTO, value)
        End Set
    End Property
    Dim fConsecutiveLiftingPTO As Integer
    Public Property ConsecutiveLiftingPTO() As Integer
        Get
            Return fConsecutiveLiftingPTO
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveLiftingPTO", fConsecutiveLiftingPTO, value)
        End Set
    End Property
    Dim fControlDateConsecutive As Boolean
    Public Property ControlDateConsecutive() As Boolean
        Get
            Return fControlDateConsecutive
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ControlDateConsecutive", fControlDateConsecutive, value)
        End Set
    End Property
    Dim fConsecutiveCXC As Integer
    Public Property ConsecutiveCXC() As Integer
        Get
            Return fConsecutiveCXC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveCXC", fConsecutiveCXC, value)
        End Set
    End Property
    Dim fConsecutiveDocumentExtension As Integer
    Public Property ConsecutiveDocumentExtension() As Integer
        Get
            Return fConsecutiveDocumentExtension
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConsecutiveDocumentExtension", fConsecutiveDocumentExtension, value)
        End Set
    End Property
    Dim fAnnualClosureOfIncome As Boolean
    Public Property AnnualClosureOfIncome() As Boolean
        Get
            Return fAnnualClosureOfIncome
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AnnualClosureOfIncome", fAnnualClosureOfIncome, value)
        End Set
    End Property
    Dim fAnnualClosureOfExpenses As Boolean
    Public Property AnnualClosureOfExpenses() As Boolean
        Get
            Return fAnnualClosureOfExpenses
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AnnualClosureOfExpenses", fAnnualClosureOfExpenses, value)
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
    Dim fActivateUser As String
    <Size(20)> _
    Public Property ActivateUser() As String
        Get
            Return fActivateUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivateUser", fActivateUser, value)
        End Set
    End Property
    Dim fActivateDate As DateTime
    Public Property ActivateDate() As DateTime
        Get
            Return fActivateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ActivateDate", fActivateDate, value)
        End Set
    End Property
    Dim fClosureUser As String
    <Size(20)> _
    Public Property ClosureUser() As String
        Get
            Return fClosureUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ClosureUser", fClosureUser, value)
        End Set
    End Property
    Dim fClosureDate As DateTime
    Public Property ClosureDate() As DateTime
        Get
            Return fClosureDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ClosureDate", fClosureDate, value)
        End Set
    End Property
    <Association("Budget_BudgetaryEntityReferencesBudget_BudgetaryValidity", GetType(BudgetBudgetaryEntityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryEntitys() As XPCollection(Of BudgetBudgetaryEntityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryEntityReportXpo)("Budget_BudgetaryEntitys")
        End Get
    End Property
    <Association("Budget_RevenueTypeReferencesBudget_BudgetaryValidity", GetType(BudgetRevenueTypeReportXpo))> _
    Public ReadOnly Property Budget_RevenueType() As XPCollection(Of BudgetRevenueTypeReportXpo)
        Get
            Return GetCollection(Of BudgetRevenueTypeReportXpo)("Budget_RevenueType")
        End Get
    End Property
    <Association("Budget_FinancialSourceReferencesBudget_BudgetaryValidity", GetType(BudgetFinancialSourceReportXpo))> _
    Public ReadOnly Property Budget_FinancialSource() As XPCollection(Of BudgetFinancialSourceReportXpo)
        Get
            Return GetCollection(Of BudgetFinancialSourceReportXpo)("Budget_FinancialSource")
        End Get
    End Property
    <Association("Budget_DependencyReferencesBudget_BudgetaryValidity", GetType(BudgetDependencyReportXpo))> _
    Public ReadOnly Property Budget_Dependency() As XPCollection(Of BudgetDependencyReportXpo)
        Get
            Return GetCollection(Of BudgetDependencyReportXpo)("Budget_Dependency")
        End Get
    End Property
    <Association("Budget_ConceptReferencesBudget_BudgetaryValidity", GetType(BudgetConceptReportXpo))> _
    Public ReadOnly Property Budget_Concept() As XPCollection(Of BudgetConceptReportXpo)
        Get
            Return GetCollection(Of BudgetConceptReportXpo)("Budget_Concept")
        End Get
    End Property
    <Association("Budget_ModificationBudget_BudgetaryValidity", GetType(BudgetModificationReportXpo))> _
    Public ReadOnly Property Budget_Modification() As XPCollection(Of BudgetModificationReportXpo)
        Get
            Return GetCollection(Of BudgetModificationReportXpo)("Budget_Modification")
        End Get
    End Property
    <Association("Budget_BudgetHeaderReferencesBudget_BudgetaryValidity", GetType(BudgetHeaderReportXpo))> _
    Public ReadOnly Property Budget_BudgetHeader() As XPCollection(Of BudgetHeaderReportXpo)
        Get
            Return GetCollection(Of BudgetHeaderReportXpo)("Budget_BudgetHeader")
        End Get
    End Property
    <Association("Budget_TransferReferencesBudget_BudgetaryValidity", GetType(BudgetTransferReportXpo))> _
    Public ReadOnly Property Budget_BudgetTransfer() As XPCollection(Of BudgetTransferReportXpo)
        Get
            Return GetCollection(Of BudgetTransferReportXpo)("Budget_BudgetTransfer")
        End Get
    End Property
    <Association("Budget_AnnualizedCashFlowModificationReferencesBudget_BudgetaryValidity", GetType(BudgetAnnualizedCashFlowModificationReportXpo))> _
    Public ReadOnly Property Budget_AnnualizedCashFlowModification() As XPCollection(Of BudgetAnnualizedCashFlowModificationReportXpo)
        Get
            Return GetCollection(Of BudgetAnnualizedCashFlowModificationReportXpo)("Budget_AnnualizedCashFlowModification")
        End Get
    End Property
    <Association("Budget_CategoryReportReferencesBudget_BudgetaryValidity", GetType(BudgetCategoryReportXpo))> _
    Public ReadOnly Property Budget_CategoryReport() As XPCollection(Of BudgetCategoryReportXpo)
        Get
            Return GetCollection(Of BudgetCategoryReportXpo)("Budget_CategoryReport")
        End Get
    End Property
    <Association("Budget_AnnualizedCashFlowTransferReferencesBudget_BudgetaryValidity", GetType(BudgetAnnualizedCashFlowTransferReportXpo))> _
    Public ReadOnly Property Budget_AnnualizedCashFlowTransfer() As XPCollection(Of BudgetAnnualizedCashFlowTransferReportXpo)
        Get
            Return GetCollection(Of BudgetAnnualizedCashFlowTransferReportXpo)("Budget_AnnualizedCashFlowTransfer")
        End Get
    End Property
    <Association("Budget_RecognitionReferencesBudget_BudgetaryValidity", GetType(BudgetRecognitionReportXpo))> _
    Public ReadOnly Property Budget_Recognition() As XPCollection(Of BudgetRecognitionReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionReportXpo)("Budget_Recognition")
        End Get
    End Property
    <Association("Budget_RecognitionModificationReferencesBudget_BudgetaryValidity", GetType(BudgetRecognitionModificationReportXpo))> _
    Public ReadOnly Property Budget_RecognitionModification() As XPCollection(Of BudgetRecognitionModificationReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionModificationReportXpo)("Budget_RecognitionModification")
        End Get
    End Property
    <Association("Budget_CollectionReferencesBudget_BudgetaryValidity", GetType(BudgetCollectionReportXpo))> _
    Public ReadOnly Property Budget_Collection() As XPCollection(Of BudgetCollectionReportXpo)
        Get
            Return GetCollection(Of BudgetCollectionReportXpo)("Budget_Collection")
        End Get
    End Property
    <Association("Budget_CollectionModificationReferencesBudget_BudgetaryValidity", GetType(BudgetCollectionModificationReportXpo))> _
    Public ReadOnly Property Budget_CollectionModification() As XPCollection(Of BudgetCollectionModificationReportXpo)
        Get
            Return GetCollection(Of BudgetCollectionModificationReportXpo)("Budget_CollectionModification")
        End Get
    End Property
    <Association("Budget_AvailabilityReferencesBudget_BudgetaryValidity", GetType(BudgetAvailabilityReportXpo))> _
    Public ReadOnly Property Budget_Availability() As XPCollection(Of BudgetAvailabilityReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityReportXpo)("Budget_Availability")
        End Get
    End Property
    <Association("Budget_AvailabilityModificationReferencesBudget_BudgetaryValidity", GetType(BudgetAvailabilityModificationReportXpo))> _
    Public ReadOnly Property Budget_AvailabilityModification() As XPCollection(Of BudgetAvailabilityModificationReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityModificationReportXpo)("Budget_AvailabilityModification")
        End Get
    End Property
    <Association("Budget_CommitmentReferencesBudget_BudgetaryValidity", GetType(BudgetCommitmentReportXpo))> _
    Public ReadOnly Property Budget_Commitment() As XPCollection(Of BudgetCommitmentReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentReportXpo)("Budget_Commitment")
        End Get
    End Property
    <Association("Budget_CommitmentModificationReferencesBudget_BudgetaryValidity", GetType(BudgetCommitmentModificationReportXpo))> _
    Public ReadOnly Property Budget_CommitmentModification() As XPCollection(Of BudgetCommitmentModificationReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentModificationReportXpo)("Budget_CommitmentModification")
        End Get
    End Property
    <Association("Budget_ObligationReferencesBudget_BudgetaryValidity", GetType(BudgetObligationReportXpo))> _
    Public ReadOnly Property Budget_Obligation() As XPCollection(Of BudgetObligationReportXpo)
        Get
            Return GetCollection(Of BudgetObligationReportXpo)("Budget_Obligation")
        End Get
    End Property
    <Association("Budget_ObligationModificationReferencesBudget_BudgetaryValidity", GetType(BudgetObligationModificationReportXpo))> _
    Public ReadOnly Property Budget_ObligationModification() As XPCollection(Of BudgetObligationModificationReportXpo)
        Get
            Return GetCollection(Of BudgetObligationModificationReportXpo)("Budget_ObligationModification")
        End Get
    End Property
    <Association("Budget_PaymentOrderReferencesBudget_BudgetaryValidity", GetType(BudgetPaymentOrderReportXpo))> _
    Public ReadOnly Property Budget_PaymentOrder() As XPCollection(Of BudgetPaymentOrderReportXpo)
        Get
            Return GetCollection(Of BudgetPaymentOrderReportXpo)("Budget_PaymentOrder")
        End Get
    End Property
    <Association("Budget_ReimbursementResourceReferencesBudget_BudgetaryValidity", GetType(BudgetReimbursementResourceReportXpo))> _
    Public ReadOnly Property Budget_ReimbursementResource() As XPCollection(Of BudgetReimbursementResourceReportXpo)
        Get
            Return GetCollection(Of BudgetReimbursementResourceReportXpo)("Budget_ReimbursementResource")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
