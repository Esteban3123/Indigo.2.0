'***********************************************************************
' Assembly         : DistributedService.Payments
' Author           : Carlos Mario Arias Rubiano
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
Public Interface IPaymentsService
    Inherits IPaymentsSequense, IPaymentsPaymentsConcept, IPaymentsPaymentsNoteConcept, IPaymentsBlockRecordPayments, IPaymentsAccountPayable, IPaymentsSettingPayments, IPaymentsOpeningBalance,
        IPaymentsMoneyAdvance, IPaymentsNotesDebitCredit, IPaymentsDeferredCausation, IPaymentsServiceMovementAccountPayable, IPaymentsPaymentNotesAccountPayableAdvance, IPaymentsTransfers, IPaymentsServiceAgesPayments,
        IPaymentsMonthlyAmortization, IPaymentsDeferredCausationShare, IPaymentSupplier, IPaymentsFilingUnit, IPaymentsSupplierType, IPaymentsAccountPayableRejectionReason, IPaymentsAccountPayableTransfer,
        IPaymentsServicePaymentsMassiveConfirm, IPaymentReports, IPaymentsLoadMassive, IPaymentsDocumentSupport, IPaymentsServiceRevaluation
End Interface
