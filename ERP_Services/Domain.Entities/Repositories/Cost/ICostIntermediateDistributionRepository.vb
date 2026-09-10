'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-06-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICostIntermediateDistributionRepository
    Inherits IRepository(Of CostIntermediateDistribution)

    Function GetIntermediateDistribution(code As String) As CostIntermediateDistribution
    Function SP_ImportDetailsToCostIntermediateDistributionBase(distributionType As Byte, measurementUnit As Byte, xmlListIntermediateDistributionBaseDetail As Object, xmlData As Object) As Object
    Function SP_CopyAndPasteCostIntermediateDistribution(xmlObject As String, intermediateDistributionId As Integer) As List(Of SP_CopyAndPasteCostIntermediateDistribution_Result)
End Interface
