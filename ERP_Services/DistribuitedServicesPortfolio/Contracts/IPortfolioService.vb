'***********************************************************************
' Assembly         : DistributedService.Portfolio
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
Public Interface IPortfolioService
    Inherits IPortfolioSequense, IPortfolioEconomicIndicator, IAccountReceivableConcept, IPortfolioNoteConcept, IPortfolioBlockRecordPortfolio, IPortfolioProvisionRanges
    Inherits IPortfolioServicePortfolioAdvance, IPortfolioServiceAccountReceivableShare, IPortfolioServiceAgePortfolio, IPortfolioServicePortfolioNote, IPortfolioServiceSettingPortfolio
    Inherits IPortfolioServiceAccountReceivable, IPortfolioServicePortfolioInitialBalance, IPortfolioServicePortfolioTransfers, IPortfolioServiceAccountReceivableDocument
    Inherits IPortfolioServicePortfolioReclassification, IPortfolioServiceBudgetAllocationInitialBalances, IPortfolioServicePortfolioMassiveConfirm, IPortfolioServiceHardCollection
    Inherits IPortfolioServiceCircularZeroThirty, IPortfolioServicePortfolioProvision, IPortfolioLawyer, IPortfolioServiceReports, IPortfolioServiceDemandStatus, IPortfolioConciliationConcepts, IPortfolioConciliationService
    Inherits IPortfolioServiceRevaluation, IPortfolioDeteriorationClassification
End Interface
