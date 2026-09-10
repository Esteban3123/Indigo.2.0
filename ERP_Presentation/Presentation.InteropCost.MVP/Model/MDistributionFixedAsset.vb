'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
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

Public Class MDistributionFixedAsset
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
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    Public Async Function SaveDistributionFixedAsset(ByVal distributionFixedAsset As DistributionFixedAsset, ByVal idSequence As Long) As Task(Of ActionResult(Of DistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveDistributionFixedAssetAsync(distributionFixedAsset, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Public Async Function DeleteDistributionFixedAsset(ByVal distributionFixedAsset As DistributionFixedAsset) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteDistributionFixedAssetAsync(distributionFixedAsset, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ' ''' <summary>
    ' ''' Actualiza el estado del registro
    ' ''' </summary>
    Public Async Function UpdateStateDistributionFixedAsset(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateDistributionFixedAssetAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Public Async Function GetDistributionFixedAsset(ByVal code As String) As Task(Of ActionResult(Of DistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionFixedAssetAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As Task(Of ActionResult(Of DistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionFixedAssetByActivoAndYearMonthAsync(activoOid, year, month)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Public Async Function GetDistributionFixedAssetById(id As Integer) As Task(Of DistributionFixedAsset)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionFixedAssetByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Public Async Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of DistributionFixedAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListDistributionFixedAssetByYearMonthAsync(year, month)
    End Function

    Public Async Function GetDeprecationValue(oidAfnActivo As Integer, year As Integer, month As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDeprecationValueAsync(oidAfnActivo, year, month)
    End Function

    ''' <summary>
    ''' Lists the period with data by maximum period simple.
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodSimple(ByVal year As Integer, ByVal month As Integer) As List(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriodFixedAsset(year, month)
    End Function

    ''' <summary>
    ''' Lists the period with data by maximum period simple.
    ''' </summary>
    Public Async Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriodFixedAssetAsync(year, month)
    End Function

    Function ListFixedAssetInterfaceErp() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListFixedAssetInterfaceErp()
    End Function

    Function ListDepreciationByYearMonth(year As Integer, month As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListDepreciationByYearMonth(year, month)
    End Function

    Function GetCollectionDepreciacionByYearMont(year As Integer, month As Integer) As XPCollection(Of InteropCostRepository.AFNCALDEPXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.GetCollectionDepreciacionByYearMont(year, month)
    End Function

    Function GetRegistroDepreciacionByYearMont(year As Integer, month As Integer) As XPCollection(Of InteropCostRepository.AFNDEPRECIXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.GetRegistroDepreciacionByYearMont(year, month)
    End Function

    Public Async Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SP_ConfirmMasiveDistributionFixedAssetAsync(Container, Year, Month, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As Task(Of ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SP_ExportExcelDistributionFixedAssetAsync(Container, Year, Month)
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