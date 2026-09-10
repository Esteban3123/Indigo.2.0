'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostDirectDistributionSecondary

    ''' <summary>
    ''' Calcular la distribución secundaria
    ''' </summary>
    ''' <param name="CostDistributionSecondaryId"></param>
    ''' <param name="Value"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CalculateDistributionSecondary(CostDistributionSecondaryId As Integer, ByVal Value As Decimal, ByVal Year As Integer, ByVal Month As Integer) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail))

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDirectDistributionSecondary(code As String, audit As AuditMessage) As Domain.Entities.CostDirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDirectDistributionSecondaryById(id As Integer) As CostDirectDistributionSecondary

    ''' <summary>
    ''' Guarda una distribución secundaria
    ''' </summary>
    <OperationContract()>
    Function SaveCostDirectDistributionSecondary(distributionSecondary As CostDirectDistributionSecondary, listDistributionSecondaryDetailForDelete As List(Of Integer), ListCostLogisticsProductionCenterDetail As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDirectDistributionSecondary)

    ''' <summary>
    ''' Generar todas las distribuciones secundarias de un periodo
    ''' </summary>
    <OperationContract()>
    Function GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult

End Interface