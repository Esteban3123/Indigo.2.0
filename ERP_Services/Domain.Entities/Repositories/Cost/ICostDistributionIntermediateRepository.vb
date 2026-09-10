'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ICostDistributionIntermediateRepository
    Inherits IRepository(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Function GetDistributionIntermediate(ByVal code As String) As CostDistributionIntermediate

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por el id del activo fijo, año y mes
    ''' </summary>
    Function GetDistributionIntermediateByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional ByVal tracking As Boolean = True) As CostDistributionIntermediate

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Calcula la distribución intermedia basándose en las bases de distribución configuradas
    ''' </summary>
    ''' <param name="costIntermediateDistributionId">Id del elemento de distribución intermedia</param>
    ''' <param name="year">Año del periodo</param>
    ''' <param name="month">Mes del periodo</param>
    ''' <returns>Lista de detalles de distribución calculados</returns>
    Function CalculateDistributionIntermediate(costIntermediateDistributionId As Integer, year As Integer, month As Integer) As List(Of SP_CalculateDistributionIntermediate_Result)

    ''' <summary>
    ''' Guarda las cantidades de servicios agrupadas para distribución de tipo "Cantidades Procesadas"
    ''' </summary>
    ''' <param name="distributionIntermediateId">Id de la distribución intermedia guardada</param>
    ''' <param name="intermediateDistributionElementId">Id del elemento de distribución intermedia</param>
    ''' <param name="year">Año del periodo</param>
    ''' <param name="month">Mes del periodo</param>
    ''' <param name="userCode">Usuario que crea el registro</param>
    Sub SaveServiceQuantitiesForDistribution(distributionIntermediateId As Integer, intermediateDistributionElementId As Integer, year As Integer, month As Integer, userCode As String)

End Interface