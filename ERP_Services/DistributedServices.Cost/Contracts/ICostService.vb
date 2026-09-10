'***********************************************************************
' Assembly         : DistributedService.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel

#End Region

<ServiceContract()> _
Public Interface ICostService
    Inherits ICostServiceBlockRecordCost, ICostServiceCostOrganizationalStructure, ICostServiceCostSequence, ICostServiceCostSetting,
        ICostServiceCostProductionCenter, ICostServiceCostGeneralExpense, ICostServiceCostDistributionDirectCost, ICostServiceCostDistributionManpower,
        ICostServiceCostDistributionSecondary, ICostServiceCostDistributionIntermediate, ICostServiceCostEstimationNative, ICostServiceCostDistributionFixedAsset,
        ICostServiceCostGeneralExpenseCategory, ICostServiceLogisticsProductionCenterRecord, ICostServiceCostDirectDistributionSecondary,
        ICostServiceCostActivity, ICostServiceCostInventoryGroup, ICostServiceReports, ICostServiceCostProductionCenterCategory, ICostServiceAverageStandardCost,
        ICostServiceCostIntermediateDistribution

End Interface
