'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 06-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDistributionSecondaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    Function SaveDistributionSecondary(ByVal distributionSecondary As CostDistributionSecondary, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostDistributionSecondary)

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    Function DeleteDistributionSecondary(ByVal distributionSecondary As CostDistributionSecondary, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    Function UpdateStateDistributionSecondary(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDistributionSecondary(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionSecondary)

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
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Function ListDistributionSecondaryByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionSecondary)

    Function SP_CopyPasteCostSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Importa detalles al elemento de costo de distribución secundaria
    ''' </summary>
    ''' <param name="DistributionType"></param>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="ListDistributionSecondaryBaseDetail"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function ImportDetailsToCostDistributionSecondaryBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionSecondaryBaseDetail As List(Of CostDistributionSecondaryBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionSecondaryBaseDetail))

End Interface