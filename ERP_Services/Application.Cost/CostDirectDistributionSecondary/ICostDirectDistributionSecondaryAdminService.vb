'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDirectDistributionSecondaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Calcular la distribución secundaria
    ''' </summary>
    ''' <param name="CostDistributionSecondaryId"></param>
    ''' <param name="Value"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Function CalculateDistributionSecondary(CostDistributionSecondaryId As Integer, ByVal Value As Decimal, ByVal Year As Integer, ByVal Month As Integer) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail))

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCostDirectDistributionSecondary(ByVal code As String, ByVal audit As AuditMessage) As CostDirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCostDirectDistributionSecondaryById(id As Integer) As CostDirectDistributionSecondary

    ''' <summary>
    ''' Guarda una distribución secundaria
    ''' </summary>
    Function SaveCostDirectDistributionSecondary(distributionSecondary As CostDirectDistributionSecondary, listDistributionSecondaryDetailForDelete As List(Of Integer), ListCostLogisticsProductionCenterDetail As List(Of Integer), audit As AuditMessage) As ActionResult(Of CostDirectDistributionSecondary)

    ''' <summary>
    ''' Generar Distribuciones Secundarias
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult

End Interface
