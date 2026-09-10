'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-03-2016
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

#End Region

Public Class MCostDistributionFixedAsset
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
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Public Async Function GetCostDistributionFixedAssetById(id As Integer) As Task(Of ActionResult(Of CostDistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDistributionFixedAssetByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Public Async Function GetCostDistributionFixedAsset(ByVal code As String) As Task(Of ActionResult(Of CostDistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDistributionFixedAssetAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de activos fijos por id periodo y registro
    ''' </summary>
    Public Async Function GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As Task(Of ActionResult(Of List(Of CostDistributionFixedAsset)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonthAsync(year, month, physicalId)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de activos fijos por id periodo y registro
    ''' </summary>
    Public Async Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As Task(Of ActionResult(Of CostDistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostDistributionFixedAssetByPhysicalIdAndYearMonthAsync(physicalId, year, month)
    End Function

    Public Async Function SP_ExportExcelDistributionFixedAsset(Year As Integer, Month As Integer) As Task(Of ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SP_ExportExcelCostDistributionFixedAssetAsync(Year, Month)
    End Function

    Public Async Function ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportCostDistributionFixedAssetAsync(Year, Month, OperatingUnitId, ImportIds, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    Public Async Function SaveCostDistributionFixedAsset(ByVal CostDistributionFixedAsset As CostDistributionFixedAsset, ByVal idSequence As Long) As Task(Of ActionResult(Of CostDistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostDistributionFixedAssetAsync(CostDistributionFixedAsset, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Async Function SP_ConfirmMasiveDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SP_ConfirmMasiveCostDistributionFixedAssetAsync(Year, Month, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Public Async Function DeleteCostDistributionFixedAsset(ByVal CostDistributionFixedAsset As CostDistributionFixedAsset) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostDistributionFixedAssetAsync(CostDistributionFixedAsset, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Async Function ListPeriodWithDistributionFixedDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDistributionFixedDataByMaximumPeriodAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Async Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of CostDistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListDistributionFixedAssetByYearMonthAsync(year, month)
    End Function

#End Region

#Region "Methods XPO"

    Function ListPhysicalAssetByMonthAndYear(year As Integer, month As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ViewCostDistributionFixedAssetByYearMonth(year, month)
    End Function

    Function GetCollectionViewCostDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As XPCollection(Of CostRepository.ViewCostDistributionFixedAsset)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetCollectionViewCostDistributionFixedAssetByYearMonth(year, month)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
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

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class