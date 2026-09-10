'***********************************************************************
' Assembly         : DistributedService.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region


<ServiceContract()>
Public Interface ITreasuryService
    Inherits ITreasuryServiceCashRegister, ITreasuryServiceCard, ITreasuryServiceCardCollections, ITreasuryServiceCashReceiptConcept, ITreasuryServiceExpenseConcept
    Inherits ITreasuryServiceNoteConcept, ITreasuryServiceBlockRecordTreasury, ITreasuryServiceEntityBankAccount, ITreasurySequense, ITreasuryServiceCashRegisterUser, ITreasuryServicePaymentConcept
    Inherits ITreasuryServiceCancellationCheck, ITreasuryServiceCheck, ITreasuryServiceVoucherTransaction, ITreasuryServiceCheckBlock, ITreasuryServiceCashReceipts, ITreasuryServiceDischargeBill
    Inherits ITreasuryServiceSettingsTreasury, ITreasuryServiceRefund, ITreasuryServiceOutstandingChecks, ITreasuryServiceVoucherTransactionDetail, ITreasuryServiceTreasuryControl
    Inherits ITreasuryServiceSchedulePayment, ITreasuryServiceDispersionFund, ITreasuryServiceCrossingAccount, ITreasuryServiceCrossingAccountDetailCxP, ITreasuryServiceCrossingAccountDetailCxC
    Inherits ITreasuryServiceConsignmentTransfer, ITreasuryServiceCashing, ITreasuryServiceTreasuryNote, ITreasuryServiceVReportConsignmentTransfer, ITreasuryServiceTreasuryMassiveConfirm
    Inherits ITreasuryServiceConstitutionCashSmaller, ITreasuryServiceCheckCashingControl, ITreasuryServiceCashFlowConcept, ITreasuryServiceReports, ITreasuryServiceCashFlowReclassification
    Inherits ITreasuryServiceBankReconciliation, IBankConciliationConceptsService, ITreasuryServiceUploadBankStatements, ITreasuryServiceBankReconciliationAutomatic, ITreasuryServiceRevaluation, IAgreementsRedemptionPoints
End Interface
