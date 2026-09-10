Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.CareGroup")> _
Public Class ContractCareGroupReportXpo
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
    '<Indexed(Name:="IX_CareGroup", Unique:=True)> _
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

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property
    Dim fDefaultManual As Byte
    Public Property DefaultManual() As Byte
        Get
            Return fDefaultManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DefaultManual", fDefaultManual, value)
        End Set
    End Property
    Dim fContractId As ContractReportXpo
    <Association("Contract_CareGroupReferencesContract_Contract")> _
        Public Property ContractId() As ContractReportXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As ContractReportXpo)
            SetPropertyValue(Of ContractReportXpo)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterReportXpo
    <Association("Contract_CareGroupReferencesPayroll_CostCenter")> _
    Public Property CostCenterId() As PayrollCostCenterReportXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterReportXpo)
            SetPropertyValue(Of PayrollCostCenterReportXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fBillingPeriod As Byte
    Public Property BillingPeriod() As Byte
        Get
            Return fBillingPeriod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("BillingPeriod", fBillingPeriod, value)
        End Set
    End Property
    Dim fMaximumIndividualBilling As Decimal
    Public Property MaximumIndividualBilling() As Decimal
        Get
            Return fMaximumIndividualBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumIndividualBilling", fMaximumIndividualBilling, value)
        End Set
    End Property
    Dim fPeriodMaximumBilling As Decimal
    Public Property PeriodMaximumBilling() As Decimal
        Get
            Return fPeriodMaximumBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PeriodMaximumBilling", fPeriodMaximumBilling, value)
        End Set
    End Property
    Dim fTypeLiquidationEmergencyStays As Byte
    Public Property TypeLiquidationEmergencyStays() As Byte
        Get
            Return fTypeLiquidationEmergencyStays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeLiquidationEmergencyStays", fTypeLiquidationEmergencyStays, value)
        End Set
    End Property
    Dim fMinimumObservationTime As Byte
    Public Property MinimumObservationTime() As Byte
        Get
            Return fMinimumObservationTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MinimumObservationTime", fMinimumObservationTime, value)
        End Set
    End Property
    Dim fMaximumObservationTime As Byte
    Public Property MaximumObservationTime() As Byte
        Get
            Return fMaximumObservationTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaximumObservationTime", fMaximumObservationTime, value)
        End Set
    End Property
    Dim fHoursOfRecoveryIncluded As Byte
    Public Property HoursOfRecoveryIncluded() As Byte
        Get
            Return fHoursOfRecoveryIncluded
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("HoursOfRecoveryIncluded", fHoursOfRecoveryIncluded, value)
        End Set
    End Property
    Dim fRequirementsTemplateId As ContractRequirementTemplateReportXpo
    <Association("Contract_CareGroupReferencesContract_RequirementTemplate")> _
    Public Property RequirementsTemplateId() As ContractRequirementTemplateReportXpo
        Get
            Return fRequirementsTemplateId
        End Get
        Set(ByVal value As ContractRequirementTemplateReportXpo)
            SetPropertyValue(Of ContractRequirementTemplateReportXpo)("RequirementsTemplateId", fRequirementsTemplateId, value)
        End Set
    End Property
    Dim fInvoiceDeadlines As Integer
    Public Property InvoiceDeadlines() As Integer
        Get
            Return fInvoiceDeadlines
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDeadlines", fInvoiceDeadlines, value)
        End Set
    End Property
    Dim fProcedureTemplateId As ContractProcedureTemplateReportXpo
    <Association("Contract_CareGroupReferencesContract_ProcedureTemplate")> _
    Public Property ProcedureTemplateId() As ContractProcedureTemplateReportXpo
        Get
            Return fProcedureTemplateId
        End Get
        Set(ByVal value As ContractProcedureTemplateReportXpo)
            SetPropertyValue(Of ContractProcedureTemplateReportXpo)("ProcedureTemplateId", fProcedureTemplateId, value)
        End Set
    End Property
    Dim fProductRateId As InventoryProductRateReportXpo
    <Association("Contract_CareGroupReferencesInventory_ProductRate")> _
    Public Property ProductRateId() As InventoryProductRateReportXpo
        Get
            Return fProductRateId
        End Get
        Set(ByVal value As InventoryProductRateReportXpo)
            SetPropertyValue(Of InventoryProductRateReportXpo)("ProductRateId", fProductRateId, value)
        End Set
    End Property
    Dim fConceptToBill As Byte
    Public Property ConceptToBill() As Byte
        Get
            Return fConceptToBill
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptToBill", fConceptToBill, value)
        End Set
    End Property
    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property
    'Dim fAccountRecoveryFeeId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts7")> _
    'Public Property AccountRecoveryFeeId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountRecoveryFeeId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountRecoveryFeeId", fAccountRecoveryFeeId, value)
    '    End Set
    'End Property
    'Dim fAccountParticularId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts8")> _
    'Public Property AccountParticularId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountParticularId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountParticularId", fAccountParticularId, value)
    '    End Set
    'End Property
    'Dim fAccountWithoutRadicateId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts")> _
    'Public Property AccountWithoutRadicateId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountWithoutRadicateId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountWithoutRadicateId", fAccountWithoutRadicateId, value)
    '    End Set
    'End Property
    'Dim fAccountRadicateId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts1")> _
    'Public Property AccountRadicateId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountRadicateId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountRadicateId", fAccountRadicateId, value)
    '    End Set
    'End Property
    'Dim fAccountObjectionRemediedId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts2")> _
    'Public Property AccountObjectionRemediedId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountObjectionRemediedId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountObjectionRemediedId", fAccountObjectionRemediedId, value)
    '    End Set
    'End Property
    'Dim fAccountConciliationId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts3")> _
    'Public Property AccountConciliationId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountConciliationId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountConciliationId", fAccountConciliationId, value)
    '    End Set
    'End Property
    'Dim fAccountLegalCollectionId As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts4")> _
    'Public Property AccountLegalCollectionId() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountLegalCollectionId
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountLegalCollectionId", fAccountLegalCollectionId, value)
    '    End Set
    'End Property
    'Dim fAccountDebtorOrder As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts5")> _
    'Public Property AccountDebtorOrder() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountDebtorOrder
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountDebtorOrder", fAccountDebtorOrder, value)
    '    End Set
    'End Property
    'Dim fAccountCreditorOrder As GeneralLedgerMainAccountsReportXpo
    '<Association("Contract_CareGroupReferencesGeneralLedger_MainAccounts6")> _
    'Public Property AccountCreditorOrder() As GeneralLedgerMainAccountsReportXpo
    '    Get
    '        Return fAccountCreditorOrder
    '    End Get
    '    Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
    '        SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("AccountCreditorOrder", fAccountCreditorOrder, value)
    '    End Set
    'End Property
    Dim fAffectBudget As Boolean
    Public Property AffectBudget() As Boolean
        Get
            Return fAffectBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectBudget", fAffectBudget, value)
        End Set
    End Property
    'Dim fBudgetId As Integer
    'Public Property BudgetId() As Integer
    '    Get
    '        Return fBudgetId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("BudgetId", fBudgetId, value)
    '    End Set
    'End Property
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

    Dim fApplyRIAS As Boolean
    Public Property ApplyRIAS() As Boolean
        Get
            Return fApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyRIAS", fApplyRIAS, value)
        End Set
    End Property

    Dim fAuthorizationRequired As Boolean
    Public Property AuthorizationRequired() As Boolean
        Get
            Return fAuthorizationRequired
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AuthorizationRequired", fAuthorizationRequired, value)
        End Set
    End Property

    Dim fTypeLiquidationOxygen As Byte
    Public Property TypeLiquidationOxygen() As Byte
        Get
            Return fTypeLiquidationOxygen
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeLiquidationOxygen", fTypeLiquidationOxygen, value)
        End Set
    End Property

    <Association("Contract_CareGroupDefinitionRateReferencesContract_CareGroup", GetType(ContractCareGroupDefinitionRateReportXpo))> _
    Public ReadOnly Property Contract_CareGroupDefinitionRates() As XPCollection(Of ContractCareGroupDefinitionRateReportXpo)
        Get
            Return GetCollection(Of ContractCareGroupDefinitionRateReportXpo)("Contract_CareGroupDefinitionRates")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
