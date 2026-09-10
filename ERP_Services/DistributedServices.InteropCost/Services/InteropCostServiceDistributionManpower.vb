'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 30-12-2014
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
    Implements IInteropCostServiceDistributionManpower

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Function DeleteDistributionManpower(distributionManpower As DistributionManpower, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionManpower.DeleteDistributionManpower
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.DeleteDistributionManpower(distributionManpower, audit)
        End Using
        'Return Me._distributionManpowerAdminService.DeleteDistributionManpower(distributionManpower, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Function GetDistributionManpower(code As String, audit As AuditMessage) As ActionResult(Of DistributionManpower) Implements IInteropCostServiceDistributionManpower.GetDistributionManpower
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.GetDistributionManpower(code, audit)
        End Using
        'Return Me._distributionManpowerAdminService.GetDistributionManpower(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Function GetDistributionManpowerById(id As Integer) As DistributionManpower Implements IInteropCostServiceDistributionManpower.GetDistributionManpowerById
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.GetDistributionManpowerById(id)
        End Using
        'Return Me._distributionManpowerAdminService.GetDistributionManpowerById(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of DistributionManpower) Implements IInteropCostServiceDistributionManpower.ListDistributionManpowerByYearMonth
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.ListDistributionManpowerByYearMonth(year, month)
        End Using
        'Return Me._distributionManpowerAdminService.ListDistributionManpowerByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Guarda una distribución por mano de obra
    ''' </summary>
    Public Function SaveDistributionManpower(distributionManpower As DistributionManpower, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionManpower) Implements IInteropCostServiceDistributionManpower.SaveDistributionManpower
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.SaveDistributionManpower(distributionManpower, audit, idSequence)
        End Using
        'Return Me._distributionManpowerAdminService.SaveDistributionManpower(distributionManpower, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state distribution manpower.
    ''' </summary>
    Public Function UpdateStateDistributionManpower(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionManpower) Implements IInteropCostServiceDistributionManpower.UpdateStateDistributionManpower
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.UpdateStateDistributionManpower(code, state, audit)
        End Using
        'Return Me._distributionManpowerAdminService.UpdateStateDistributionManpower(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Function GetDistributionManpowerByEmployeeId(employeeId As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionManpower) Implements IInteropCostServiceDistributionManpower.GetDistributionManpowerByEmployeeIdAndYearMonth
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.GetDistributionManpowerByEmployeeIdAndYearMonth(employeeId, year, month)
        End Using
        'Return Me._distributionManpowerAdminService.GetDistributionManpowerByEmployeeIdAndYearMonth(employeeId, year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IInteropCostServiceDistributionManpower.ListPeriodWithDataByMaximumPeriod
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriod(year, month)
        End Using
        'Return Me._distributionManpowerAdminService.ListPeriodWithDataByMaximumPeriod(year, month)
    End Function

    Public Function ConfirmMasiveInteropCost(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IInteropCostServiceDistributionManpower.ConfirmMasiveInteropCost
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.ConfirmMasiveInteropCost(ListIds, Year, Month, OperatingUnitId, audit)
        End Using
        'Return _distributionManpowerAdminService.ConfirmMasiveInteropCost(ListIds, Year, Month, OperatingUnitId, audit)
    End Function

    Public Function SP_ExportExcelInteropCostDistributionManPower(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ExportExcelInteropCostDistributionManPower_Result)) Implements IInteropCostServiceDistributionManpower.SP_ExportExcelInteropCostDistributionManPower
        Using service As IDistributionManpowerAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionManpowerAdminService)()
            Return service.SP_ExportExcelInteropCostDistributionManPower(ListIds, Year, Month, OperatingUnitId, audit)
        End Using
        'Return _distributionManpowerAdminService.SP_ExportExcelInteropCostDistributionManPower(ListIds, Year, Month, OperatingUnitId, audit)
    End Function

End Class