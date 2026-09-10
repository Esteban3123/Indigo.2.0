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
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceDistributionSecondary

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Function DeleteDistributionSecondary(distributionSecondary As DistributionSecondary, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionSecondary.DeleteDistributionSecondary
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.DeleteDistributionSecondary(distributionSecondary, audit)
        End Using
        'Return Me._distributionSecondaryAdminService.DeleteDistributionSecondary(distributionSecondary, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetDistributionSecondary(code As String, audit As AuditMessage) As ActionResult(Of DistributionSecondary) Implements IInteropCostServiceDistributionSecondary.GetDistributionSecondary
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.GetDistributionSecondary(code, audit)
        End Using
        'Return Me._distributionSecondaryAdminService.GetDistributionSecondary(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetDistributionSecondaryById(id As Integer) As DistributionSecondary Implements IInteropCostServiceDistributionSecondary.GetDistributionSecondaryById
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.GetDistributionSecondaryById(id)
        End Using
        'Return Me._distributionSecondaryAdminService.GetDistributionSecondaryById(id)
    End Function

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Function SaveDistributionSecondary(distributionSecondary As DistributionSecondary, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionSecondary) Implements IInteropCostServiceDistributionSecondary.SaveDistributionSecondary
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.SaveDistributionSecondary(distributionSecondary, audit, idSequence)
        End Using
        'Return Me._distributionSecondaryAdminService.SaveDistributionSecondary(distributionSecondary, audit, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function UpdateStateDistributionSecondary(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionSecondary) Implements IInteropCostServiceDistributionSecondary.UpdateStateDistributionSecondary
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.UpdateStateDistributionSecondary(code, state, audit)
        End Using
        'Return Me._distributionSecondaryAdminService.UpdateStateDistributionSecondary(code, state, audit)
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As List(Of DistributionSecondary) Implements IInteropCostServiceDistributionSecondary.ListDistributionSecondaryByYearMonth
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.ListDistributionSecondaryByYearMonth(year, month)
        End Using
        'Return Me._distributionSecondaryAdminService.ListDistributionSecondaryByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionSecondary(year As Integer, month As Integer) As List(Of String) Implements IInteropCostServiceDistributionSecondary.ListPeriodWithDataByMaximumPeriodDistributionSecondary
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriod(year, month)
        End Using
        'Return Me._distributionSecondaryAdminService.ListPeriodWithDataByMaximumPeriod(year, month)
    End Function

    Public Function SP_CopyPasteSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) Implements IInteropCostServiceDistributionSecondary.SP_CopyPasteSecondaryDistribution
        Using service As IDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionSecondaryAdminService)()
            Return service.SP_CopyPasteSecondaryDistribution(data, DistributionSecondaryId, InitialDistribution)
        End Using
        'Return Me._distributionSecondaryAdminService.SP_CopyPasteSecondaryDistribution(data, DistributionSecondaryId, InitialDistribution)
    End Function

End Class