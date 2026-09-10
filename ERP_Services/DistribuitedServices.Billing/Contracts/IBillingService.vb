'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
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

<ServiceModel.ServiceContract()>
Public Interface IBillingService
    Inherits IBillingBlockRecordBilling, IBillingSequence, IBillingServiceServiceOrder, IBillingServiceLiquidation, IBillingServiceServiceOrderDetail, IBillingAuthorization, IBillingServiceServiceOrderDetailSurgical,
        IBillingServiceBillingGroup, IBillingServiceRevenueControlDetail, IBillingServiceServiceOrderDetailDistribution, IBillingServiceSettingBilling, IBillingServiceReversalReason,
        IBillingServiceInvoiceEntityCapitated, IBillingServiceAccountControl, IBillingControlOutPatientServices, IBillingServiceInvoiceCategories, IBillingServiceSlipOut, IBillingServiceRecognition,
        IBillingServiceBasicBilling, IBillingElectronicDocument, IBillingServiceInvoiceEntityCapitatedDistribution, IBillingServiceInvoiceEntityCapitatedDistributionDetail,
        IBillingServiceDocumentInvoiceProductSalesDevolution, IBillingServiceQuotation, IBillingServiceDashboardQuoted, IConceptsCausesStatusFolio, ISalesExecutive, IBillingServiceAccountControlJustification, IBillingServiceElectronicSupportDocument,
        IBillingServiceElectronicSupportDocumentAdjustmentNote, IBillingServiceJustificationControl, IBillingServiceNumberingAuthorization, IBillingServiceLiquidationData, IBillingServiceProductAndServiceFee, IBillingServiceConditionSales,
        IBillingServiceRIPSSupportRecord

End Interface
