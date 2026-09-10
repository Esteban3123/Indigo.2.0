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
Imports Microsoft.Practices.Unity
Imports Application.InteropCost


Partial Class InteropCostService
    Implements IInteropCostServiceDistributionDirectCost

    ''' <summary>
    ''' Calcula la distribución del valor entre los distintos centros de producción
    ''' </summary>
    ''' <param name="GeneralExpenseId">Elemento del gasto</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de detalles de distribución de costos</returns>
    Public Function CalculateDistribution(GeneralExpenseId As Integer, value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of DistributionDirectCostDetail)) Implements IInteropCostServiceDistributionDirectCost.CalculateDistribution
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.CalculateDistribution(GeneralExpenseId, value, year, month, containerName)
        End Using
        'Return Me._directCostAdminService.CalculateDistribution(GeneralExpenseId, value, year, month, containerName)
    End Function

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Public Function DeleteDistributionDirectCost(distributionDirectCost As DistributionDirectCost, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionDirectCost.DeleteDistributionDirectCost
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.DeleteDistributionDirectCost(distributionDirectCost, audit)
        End Using
        'Return Me._directCostAdminService.DeleteDistributionDirectCost(distributionDirectCost, audit)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Function GetDistributionDirectCost(code As String, audit As AuditMessage) As ActionResult(Of DistributionDirectCost) Implements IInteropCostServiceDistributionDirectCost.GetDistributionDirectCost
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetDistributionDirectCost(code, audit)
        End Using
        'Return Me._directCostAdminService.GetDistributionDirectCost(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Function GetDistributionDirectCostById(id As Integer) As DistributionDirectCost Implements IInteropCostServiceDistributionDirectCost.GetDistributionDirectCostById
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetDistributionDirectCostById(id)
        End Using
        'Return Me._directCostAdminService.GetDistributionDirectCostById(id)
    End Function

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Public Function SaveDistributionDirectCost(distributionDirectCost As DistributionDirectCost, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionDirectCost) Implements IInteropCostServiceDistributionDirectCost.SaveDistributionDirectCost
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.SaveDistributionDirectCost(distributionDirectCost, audit, idSequence)
        End Using
        'Return Me._directCostAdminService.SaveDistributionDirectCost(distributionDirectCost, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state distribution direct cost.
    ''' </summary>
    Public Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionDirectCost) Implements IInteropCostServiceDistributionDirectCost.UpdateStateDistributionDirectCost
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.UpdateStateDistributionDirectCost(code, state, audit)
        End Using
        'Return Me._directCostAdminService.UpdateStateDistributionDirectCost(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene el valor contable por contenedor, número de cuenta contable, año y mes
    ''' </summary>
    Public Function GetMainAccountValueByContainerNumberAccountYearAndMotn(container As String, numberaccount As String, year As String, month As Integer) As ActionResult(Of SP_MainAccountValue_Result) Implements IInteropCostServiceDistributionDirectCost.GetMainAccountValueByContainerNumberAccountYearAndMotn
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetMainAccountValueByContainerNumberAccountYearAndMotn(container, numberaccount, year, month)
        End Using
        'Return Me._directCostAdminService.GetMainAccountValueByContainerNumberAccountYearAndMotn(container, numberaccount, year, month)
    End Function

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of DistributionDirectCost) Implements IInteropCostServiceDistributionDirectCost.ListDistributionDirectCostByYearMonth
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.ListDistributionDirectCostByYearMonth(year, month)
        End Using
        'Return Me._directCostAdminService.ListDistributionDirectCostByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result) Implements IInteropCostServiceDistributionDirectCost.GetPrimaryEstimateOrFinal
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetPrimaryEstimateOrFinal(InitialMonth, InitialYear, LastMonth, LastYear)
        End Using
        'Return Me._directCostAdminService.GetPrimaryEstimateOrFinal(InitialMonth, InitialYear, LastMonth, LastYear)
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result) Implements IInteropCostServiceDistributionDirectCost.GetPrimaryEstimateOrFinalB
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetPrimaryEstimateOrFinalB(InitialMonth, InitialYear, LastMonth, LastYear)
        End Using
        'Return Me._directCostAdminService.GetPrimaryEstimateOrFinalB(InitialMonth, InitialYear, LastMonth, LastYear)
    End Function

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "InteropCost.SP_ReportGeneralProfitabilityTotalCostC"
    ''' </summary>   
    ''' <returns></returns>
    Function GetPrimaryEstimateOrFinalC(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, session As SessionValues) As DataSet Implements IInteropCostServiceDistributionDirectCost.GetPrimaryEstimateOrFinalC
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.GetPrimaryEstimateOrFinalC(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, session)
        End Using
        'Return Me._directCostAdminService.GetPrimaryEstimateOrFinalC(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, session)
    End Function

    Public Function ListGeneralExpenseByPeriod(year As Integer, month As Integer) As List(Of GeneralExpense) Implements IInteropCostServiceDistributionDirectCost.ListGeneralExpenseByPeriod
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.ListGeneralExpenseByPeriod(year, month)
        End Using
        'Return Me._directCostAdminService.ListGeneralExpenseByPeriod(year, month)
    End Function

    Public Function ListDistributionDirectCostToReport(containerCost As String, distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result) Implements IInteropCostServiceDistributionDirectCost.ListDistributionDirectCostToReport
        Using service As IDistributionDirectCostAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionDirectCostAdminService)()
            Return service.ListDistributionDirectCostToReport(containerCost, distributionDirectCostId)
        End Using
        'Return Me._directCostAdminService.ListDistributionDirectCostToReport(containerCost, distributionDirectCostId)
    End Function
End Class
