'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IDistributionIntermediateRepository
    Inherits IRepository(Of DistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Function GetDistributionIntermediate(ByVal code As String) As DistributionIntermediate

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Function GetDistributionIntermediateById(id As Integer) As DistributionIntermediate

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por el id del activo fijo, año y mes
    ''' </summary>
    Function GetDistributionIntermediateByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional ByVal tracking As Boolean = True) As DistributionIntermediate

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionIntermediate)

End Interface