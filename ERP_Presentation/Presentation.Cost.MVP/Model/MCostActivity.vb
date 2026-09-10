#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Class MCostActivity
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetInvoiceEntityCapitatedDistributionById(costActivityId As Integer) As Task(Of ActionResult(Of CostActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostActivityByIdAsync(costActivityId)
    End Function

    Public Async Function GetInvoiceEntityCapitatedDistribution(code As String) As Task(Of ActionResult(Of CostActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostActivityByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveCostActivity(costActivity As CostActivity, 
                                listCostActivityProductionCenter As List(Of CostActivityProductionCenter), listCostActivityStep As List(Of CostActivityStep), 
                                listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset), listCostActivityStepPayroll As List(Of CostActivityStepPayroll),
                                listCostActivityStepInventory As List(Of CostActivityStepInventory), listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost)) As Task(Of ActionResult(Of CostActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostActivityAsync(costActivity, listCostActivityProductionCenter, listCostActivityStep, listCostActivityStepFixedAsset, listCostActivityStepPayroll, listCostActivityStepInventory, listCostActivityStepAddictionalCost, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeStateCostActivity(costActivity As CostActivity) As Task(Of ActionResult(Of CostActivity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ChangeStateCostActivityAsync(costActivity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Function ListCostActivityXPO() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostActivities()
    End Function

    ''' <summary>
    ''' Obtiene la lista de los detalles del Costo Estándar Promedio por Actividad
    ''' </summary>
    ''' <param name="activityId"></param>
    ''' <returns></returns>
    Public Async Function GetStandardCostValuesByActivityId(ByVal activityId As Integer) As Task(Of List(Of StandardCostDetailsXpo))
        Dim Filter As String = "CostActivityId.Id = " & activityId
        Return Await Task.Run(Function() XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetCollectionAsList(Of StandardCostDetailsXpo)(Nothing, Filter))
    End Function


#Region "Copy & Paste"

    Public Async Function CostActivityCopyAndPasteProductionCenter(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostActivityProductionCenter)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CostActivityCopyAndPasteProductionCenterAsync(data)
    End Function

    Public Async Function CostActivityCopyAndPasteFixedAsset(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostActivityStepFixedAsset)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CostActivityCopyAndPasteFixedAssetAsync(data)
    End Function

    Public Async Function CostActivityCopyAndPastePayroll(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostActivityStepPayroll)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CostActivityCopyAndPastePayrollAsync(data)
    End Function

    Public Async Function CostActivityCopyAndPasteInventory(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostActivityStepInventory)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CostActivityCopyAndPasteInventoryAsync(data)
    End Function

    #End Region

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class