'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
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
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Class MCostDirectDistributionSecondary
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

    ''' <summary>
    ''' Calcula la distribución secundaria
    ''' </summary>
    ''' <param name="CostDistributionSecondaryId">Elemento del costo</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de costos</returns>
    Public Async Function CalculateDistributionSecondary(CostDistributionSecondaryId As Integer, ByVal value As Decimal, ByVal year As Integer, ByVal month As Integer) As Task(Of ActionResult(Of List(Of CostDirectDistributionSecondaryDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CalculateDistributionSecondaryAsync(CostDistributionSecondaryId, value, year, month)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Async Function GetCostDirectDistributionSecondary(code As String) As Task(Of CostDirectDistributionSecondary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDirectDistributionSecondaryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Async Function GetCostDirectDistributionSecondaryById(id As Integer) As Task(Of CostDirectDistributionSecondary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDirectDistributionSecondaryByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    Public Async Function SaveCostDirectDistributionSecondary(distributionSecondary As CostDirectDistributionSecondary, listDistributionSecondaryDetailForDelete As List(Of Integer), ListCostLogisticsProductionCenterDetail As List(Of Integer)) As Task(Of ActionResult(Of CostDirectDistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostDirectDistributionSecondaryAsync(distributionSecondary, listDistributionSecondaryDetailForDelete, ListCostLogisticsProductionCenterDetail, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GenerateDistributionSecondaryAsync(Year, Month, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetDistributionSecondary() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostDistributionSecondaryByStatus(True)
    End Function

    Public Function GetCostEstimationXpo(ParamArray parameters As Object()) As CostEstimationNativeXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetCostEstimationXpo(parameters)
    End Function

    Public Function ListProductionCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListCostProductionCenterByStatus(True)
    End Function

    Public Function GetDistributionSecondaryProductionCenterBySecundaryId(id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetDistributionSecondaryProductionCenterBySecundaryId(id)
    End Function

    Public Function GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(id)
    End Function

    Public Function GetInventoryMeasurementUnitById(id As Integer) As Infrastructure.Data.Xpo.CostRepository.InventoryInventoryMeasurementUnitReportXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetInventoryMeasurementUnitById(id)
    End Function

    Public Async Function ExecuteQueryDt(query As String) As Task(Of DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDtAsync(query, _indigoSessionValues.TransactionalContainer)
    End Function

#End Region

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
