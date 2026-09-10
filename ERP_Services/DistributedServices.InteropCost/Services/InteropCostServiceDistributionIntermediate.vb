'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 15-01-2015
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
    Implements IInteropCostServiceDistributionIntermediate

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Public Function DeleteDistributionIntermediate(distributionIntermediate As DistributionIntermediate, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionIntermediate.DeleteDistributionIntermediate
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.DeleteDistributionIntermediate(distributionIntermediate, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.DeleteDistributionIntermediate(distributionIntermediate, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of DistributionIntermediate) Implements IInteropCostServiceDistributionIntermediate.GetDistributionIntermediate
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.GetDistributionIntermediate(code, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.GetDistributionIntermediate(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As DistributionIntermediate Implements IInteropCostServiceDistributionIntermediate.GetDistributionIntermediateById
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.GetDistributionIntermediateById(id)
        End Using
        'Return Me._distributionIntermediateAdminService.GetDistributionIntermediateById(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of DistributionIntermediate) Implements IInteropCostServiceDistributionIntermediate.ListDistributionIntermediateByYearMonth
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.ListDistributionIntermediateByYearMonth(year, month)
        End Using
        'Return Me._distributionIntermediateAdminService.ListDistributionIntermediateByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements IInteropCostServiceDistributionIntermediate.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
        End Using
        'Return Me._distributionIntermediateAdminService.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
    End Function

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Public Function SaveDistributionIntermediate(distributionIntermediate As DistributionIntermediate, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionIntermediate) Implements IInteropCostServiceDistributionIntermediate.SaveDistributionIntermediate
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.SaveDistributionIntermediate(distributionIntermediate, audit, idSequence)
        End Using
        'Return Me._distributionIntermediateAdminService.SaveDistributionIntermediate(distributionIntermediate, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state distribution intermediate.
    ''' </summary>
    Public Function UpdateStateDistributionIntermediate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionIntermediate) Implements IInteropCostServiceDistributionIntermediate.UpdateStateDistributionIntermediate
        Using service As IDistributionIntermediateAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionIntermediateAdminService)()
            Return service.UpdateStateDistributionIntermediate(code, state, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.UpdateStateDistributionIntermediate(code, state, audit)
    End Function

End Class