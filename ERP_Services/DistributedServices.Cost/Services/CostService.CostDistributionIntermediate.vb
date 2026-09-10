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
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Class CostService
    Implements ICostServiceCostDistributionIntermediate

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Public Function DeleteDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage) As ActionResult Implements ICostServiceCostDistributionIntermediate.DeleteDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.DeleteDistributionIntermediate(distributionIntermediate, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.DeleteDistributionIntermediate(distributionIntermediate, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.GetDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.GetDistributionIntermediate(code, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.GetDistributionIntermediate(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate Implements ICostServiceCostDistributionIntermediate.GetDistributionIntermediateById
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.GetDistributionIntermediateById(id)
        End Using
        'Return Me._distributionIntermediateAdminService.GetDistributionIntermediateById(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.ListDistributionIntermediateByYearMonth
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.ListDistributionIntermediateByYearMonth(year, month)
        End Using
        'Return Me._distributionIntermediateAdminService.ListDistributionIntermediateByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements ICostServiceCostDistributionIntermediate.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
        End Using
        'Return Me._distributionIntermediateAdminService.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
    End Function

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Public Function SaveDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.SaveDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.SaveDistributionIntermediate(distributionIntermediate, audit, idSequence)
        End Using
        'Return Me._distributionIntermediateAdminService.SaveDistributionIntermediate(distributionIntermediate, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state distribution intermediate.
    ''' </summary>
    Public Function UpdateStateDistributionIntermediate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.UpdateStateDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.UpdateStateDistributionIntermediate(code, state, audit)
        End Using
        'Return Me._distributionIntermediateAdminService.UpdateStateDistributionIntermediate(code, state, audit)
    End Function

    ''' <summary>
    ''' Confirma una distribución intermedia
    ''' </summary>
    Public Function ConfirmDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.ConfirmDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.ConfirmDistributionIntermediate(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Anula una distribución intermedia
    ''' </summary>
    Public Function AnnulDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostServiceCostDistributionIntermediate.AnnulDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.AnnulDistributionIntermediate(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Calcula la distribución intermedia basándose en las bases de distribución configuradas
    ''' </summary>
    Public Function CalculateDistributionIntermediate(costIntermediateDistributionId As Integer, year As Integer, month As Integer) As ActionResult(Of List(Of CostDistributionIntermediateDetail)) Implements ICostServiceCostDistributionIntermediate.CalculateDistributionIntermediate
        Using service As ICostDistributionIntermediateAdminService = Container.Current.Resolve(Of ICostDistributionIntermediateAdminService)()
            Return service.CalculateDistributionIntermediate(costIntermediateDistributionId, year, month)
        End Using
    End Function

End Class