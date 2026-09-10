'***********************************************************************
' Assembly         : DistributedService.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
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
Public Interface IInteropCostService
    Inherits IInteropCostServiceOrganizationalStructure, IInteropCostServiceBlockRecord, IInteropCostServiceSequence, IInteropCostSeriveProductionCenter, IInteropCostServiceGeneralExpense
    Inherits IInteropCostServiceDistributionDirectCost, IInteropCostServiceInteropCostSetting, IInteropCostServiceDistributionManpower, IInteropCostServiceDistributionFixedAsset
    Inherits IInteropCostServiceDistributionIntermediate, IInteropCostServiceDistributionSecondary, IInteropCostServiceCostEstimation, IInteropCostServiceDirectDistributionSecondary
    Inherits IInteropCostServiceLogisticsProductionCenterRecord, IInteropCostServiceGeneralExpenseCategory


End Interface