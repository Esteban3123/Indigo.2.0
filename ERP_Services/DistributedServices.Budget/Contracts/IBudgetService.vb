'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IBudgetService
    Inherits IBudgetServiceBudgetItem, IBudgetSequense, IBudgetServiceFinancialSource, IBudgetServiceBudgetInstitution, IBudgetServiceValidity, IBudgetServiceBlockRecordBudget, IBudgetServiceEarningsType,
        IBudgetServiceExpenseType, IBudgetServiceBudgetDependency, IBudgetServiceBudgetConcept, IButgetServiceSettingBudget, IBudgetServiceLevelCategory, IBudgetServiceEntry, IBudgetServiceBudgetModification,
        IBudgetServiceAnnualizedCashFlow, IBudgetServiceBudgetTransfer, IBudgetServiceBudgetTransferDetail, IBudgetServiceRecognition, IBudgetServiceAnnualizedCashFlowModification,
        IBudgetServiceAnnualizedCashFlowModificationDetail, IBudgetServiceAnnualizedCashFlowTransfer, IBudgetServiceRecognitionModification, IBudgetServiceCollection, IBudgetServiceCollectionDetail,
        IBudgetServiceCollectionModification, IBudgetServiceCollectionModificationDetail, IBudgetServiceAvailability, IBudgetServiceBudget, IBudgetServiceAvailabilityModification, IBudgetServiceConfirmationDocuments,
        IBudgetServiceCommitment, IBudgetServiceCommitmentModification, IBudgetServiceObligation, IBudgetServiceObligationDetail, IBudgetServiceObligationModification, IBudgetServiceObligationModificationDetail,
        IBudgetServicePaymentOrder, IBudgetServiceReimbursementResource, IBudgetServiceSuspension, IBudgetServiceSuspensionCancellation, IBudgetServiceCopyBase, IBudgetServiceAvailabilityExtension,
        IBudgetServicePrivateBudgetItemsStructure, IBudgetServicePrivateBudget, IBudgetServiceReports, IBudgetServiceCCPET, IBudgetServiceCPCCatalog, IBudgetServicePublicPolicy
End Interface
