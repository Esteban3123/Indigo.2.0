'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IDistributionSecondaryRepository
    Inherits IRepository(Of DistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDistributionSecondary(ByVal code As String) As DistributionSecondary

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetDistributionSecondaryById(id As Integer) As DistributionSecondary

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Gets the distribution intermediate by production center identifier and year month.
    ''' </summary>
    Function GetDistributionSecondaryByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional ByVal tracking As Boolean = True) As DistributionSecondary

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Function ListDistributionSecondaryByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionSecondary)

    Function SP_CopyPasteSecondaryDistribution(XmlObject As String, DistributionSecondaryId As Integer) As List(Of SP_CopyPasteSecondaryDistribution_Result)

    ''' <summary>
    ''' Obtiene un elemento de distribucion secundaria por id de centro de produccion
    ''' </summary>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    Function GetDistributionSecondaryByProductionCenterId(ProductionCenterId As Integer, DistributionSecondaryId As Integer) As DistributionSecondary

End Interface
