'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDistributionSecondaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    Function SaveDistributionSecondary(ByVal distributionSecondary As DistributionSecondary, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DistributionSecondary)

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    Function DeleteDistributionSecondary(ByVal distributionSecondary As DistributionSecondary, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    Function UpdateStateDistributionSecondary(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DistributionSecondary)

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDistributionSecondary(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionSecondary)

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
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Function ListDistributionSecondaryByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionSecondary)

    Function SP_CopyPasteSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer)))

End Interface