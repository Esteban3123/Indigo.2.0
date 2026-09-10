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

Public Class MCostInventoryGroup
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

    Public Async Function GetCostInventoryGroup(code As String) As Task(Of ActionResult(Of CostInventoryGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostInventoryGroupByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveCostInventoryGroup(costInventoryGroup As CostInventoryGroup, listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail)) As Task(Of ActionResult(Of CostInventoryGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostInventoryGroupAsync(costInventoryGroup, listCostInventoryGroupDetail, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeStateCostInventoryGroup(costInventoryGroup As CostInventoryGroup) As Task(Of ActionResult(Of CostInventoryGroup))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ChangeStateCostInventoryGroupAsync(costInventoryGroup, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    #Region "Copy & Paste"

    Public Async Function CopyAndPasteCostInventoryGroupDetail(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostInventoryGroupDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CopyAndPasteCostInventoryGroupAsync(data)
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