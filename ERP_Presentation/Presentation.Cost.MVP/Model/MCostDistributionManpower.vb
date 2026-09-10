'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-03-2016
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

Public Class MCostDistributionManpower
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
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Async Function GetDistributionManpowerById(id As Integer) As Task(Of CostDistributionManpower)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionManpowerByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Async Function GetDistributionManpower(ByVal code As String) As Task(Of ActionResult(Of CostDistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionManpowerAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Async Function GetDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As Task(Of ActionResult(Of List(Of CostDistributionManpower)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionManpowerByYearMonthAsync(Year, Month, ManpowerType, EntityId)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id periodo y registro
    ''' </summary>
    Public Async Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As Task(Of ActionResult(Of CostDistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionManpowerByYearMonthManpowerTypeAndEntityIdAsync(Year, Month, ManpowerType, EntityId)
    End Function

    Public Async Function ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportCostDistributionManpowerAsync(Year, Month, OperatingUnitId, ImportIds, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As Task(Of ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SP_ExportExcelCostDistributionManPowerAsync(Year, Month)
    End Function

    ''' <summary>
    ''' Guardar una distribución por mano de obra
    ''' </summary>
    Public Async Function SaveDistributionManpower(ByVal distributionManpower As CostDistributionManpower, ByVal idSequence As Long) As Task(Of ActionResult(Of CostDistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveDistributionManpowerAsync(distributionManpower, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function ConfirmMasive(Year As Integer, Month As Integer, OperatingUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ConfirmMasiveAsync(Year, Month, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function DeleteDistributionManpower(ByVal distributionManpower As CostDistributionManpower) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteDistributionManpowerAsync(distributionManpower, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListPeriodWithDataByMaximumPeriodSimple(ByVal year As Integer, ByVal month As Integer) As List(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriod(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Async Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriodAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Async Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of CostDistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListDistributionManpowerByYearMonthAsync(year, month)
    End Function

#End Region

#Region "Methods XPO"

    Function ViewCostDistributionManpowerByYearMonth(Year As Integer, Month As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ViewCostDistributionManpowerByYearMonth(Year, Month)
    End Function

    Function GetCollectionViewCostDistributionManpowerByYearMonth(year As Integer, month As Integer) As XPCollection(Of CostRepository.ViewCostDistributionManpower)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.GetCollectionViewCostDistributionManpowerByYearMonth(year, month)
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
