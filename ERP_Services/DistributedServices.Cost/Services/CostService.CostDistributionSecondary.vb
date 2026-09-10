'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports DistributedServices.Cost

Partial Class CostService
    Implements ICostServiceCostDistributionSecondary

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Function DeleteDistributionSecondary(distributionSecondary As CostDistributionSecondary, audit As AuditMessage) As ActionResult Implements ICostServiceCostDistributionSecondary.DeleteDistributionSecondary
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.DeleteDistributionSecondary(distributionSecondary, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetDistributionSecondary(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionSecondary) Implements ICostServiceCostDistributionSecondary.GetDistributionSecondary
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.GetDistributionSecondary(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetDistributionSecondaryById(id As Integer) As CostDistributionSecondary Implements ICostServiceCostDistributionSecondary.GetDistributionSecondaryById
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.GetDistributionSecondaryById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Function SaveDistributionSecondary(distributionSecondary As CostDistributionSecondary, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CostDistributionSecondary) Implements ICostServiceCostDistributionSecondary.SaveDistributionSecondary
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.SaveDistributionSecondary(distributionSecondary, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function UpdateStateDistributionSecondary(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionSecondary) Implements ICostServiceCostDistributionSecondary.UpdateStateDistributionSecondary
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.UpdateStateDistributionSecondary(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionSecondary) Implements ICostServiceCostDistributionSecondary.ListDistributionSecondaryByYearMonth
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.ListDistributionSecondaryByYearMonth(year, month)
        End Using
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionSecondary(year As Integer, month As Integer) As List(Of String) Implements ICostServiceCostDistributionSecondary.ListPeriodWithDataByMaximumPeriodDistributionSecondary
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriod(year, month)
        End Using
    End Function

    Public Function SP_CopyPasteCostSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) Implements ICostServiceCostDistributionSecondary.SP_CopyPasteCostSecondaryDistribution
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.SP_CopyPasteCostSecondaryDistribution(data, DistributionSecondaryId, InitialDistribution)
        End Using
    End Function

    ''' <summary>
    ''' Importa detalles al elemento de costo de distribución secundaria
    ''' </summary>
    ''' <param name="DistributionType"></param>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="ListDistributionSecondaryBaseDetail"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Function ImportDetailsToCostDistributionSecondaryBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionSecondaryBaseDetail As List(Of CostDistributionSecondaryBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) Implements ICostServiceCostDistributionSecondary.ImportDetailsToCostDistributionSecondaryBase
        Using service As ICostDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDistributionSecondaryAdminService)()
            Return service.ImportDetailsToCostDistributionSecondaryBase(DistributionType, MeasurementUnit, ListDistributionSecondaryBaseDetail, Data)
        End Using
    End Function
End Class