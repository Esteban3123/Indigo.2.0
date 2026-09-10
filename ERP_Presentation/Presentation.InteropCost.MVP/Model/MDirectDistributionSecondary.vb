'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class MDirectDistributionSecondary
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

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Async Function GetDirectDistributionSecondary(code As String) As Task(Of DirectDistributionSecondary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDirectDistributionSecondaryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Async Function GetDirectDistributionSecondaryById(id As Integer) As Task(Of DirectDistributionSecondary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDirectDistributionSecondaryByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Async Function SaveDirectDistributionSecondary(DirectDistributionSecondary As DirectDistributionSecondary, ListUpdateIds As List(Of Integer), ByVal idSequence As Long) As Task(Of ActionResult(Of DirectDistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveDirectDistributionSecondaryAsync(DirectDistributionSecondary, ListUpdateIds, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetDistributionSecondary() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.ListDistributionSecondaryByStatus(True)
    End Function

    Public Function GetCostEstimationXpo(ParamArray parameters As Object()) As CostEstimationXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.GetCostEstimationXpo(parameters)
    End Function

    Public Function ListProductionCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
    End Function

    Public Function GetDistributionSecondaryProductionCenterBySecundaryId(id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.GetDistributionSecondaryProductionCenterBySecundaryId(id)
    End Function

    Public Function GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id)
    End Function

    Public Function GetInventoryMeasurementUnitById(id As Integer) As InventoryMeasurementUnitXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.GetInventoryMeasurementUnitById(id)
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
