'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 06-04-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostDistributionSecondaryRepository
    Inherits IRepository(Of CostDistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDistributionSecondary(ByVal code As String) As CostDistributionSecondary

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetDistributionSecondaryById(id As Integer) As CostDistributionSecondary

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Gets the distribution intermediate by production center identifier and year month.
    ''' </summary>
    Function GetDistributionSecondaryByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional ByVal tracking As Boolean = True) As CostDistributionSecondary

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Function ListDistributionSecondaryByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionSecondary)

    Function SP_CopyPasteCostSecondaryDistribution(XmlObject As String, DistributionSecondaryId As Integer) As List(Of SP_CopyPasteCostSecondaryDistribution_Result)

    ''' <summary>
    ''' Valida el CopyPaste de los detalles a la base del elemento del costo secundario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportDetailsToCostDistributionSecondaryBase(DistributionType As Byte, MeasurementUnit As Byte, xmlListDistributionSecondaryBaseDetail As String, xmlData As String) As List(Of SP_ImportDetailsToCostDistributionSecondaryBase_Result)

End Interface