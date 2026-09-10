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

#End Region

Public Class MDistributionManpower
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
    ''' Guardar una distribución por mano de obra
    ''' </summary>
    Public Async Function SaveDistributionManpower(ByVal distributionManpower As DistributionManpower, ByVal idSequence As Long) As Task(Of ActionResult(Of DistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveDistributionManpowerAsync(distributionManpower, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function DeleteDistributionManpower(ByVal distributionManpower As DistributionManpower) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteDistributionManpowerAsync(distributionManpower, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function ConfirmMasiveInteropcost(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ConfirmMasiveInteropCostAsync(ListIds, Year, Month, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una distribución por mano de obra
    ''' </summary>
    Public Async Function SP_ExportExcelInteropCostDistributionManPower(ListIds As List(Of Tuple(Of Integer, Integer)), Year As Integer, Month As Integer, OperatingUnitId As Integer) As Task(Of ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SP_ExportExcelInteropCostDistributionManPowerAsync(ListIds, Year, Month, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    Public Async Function UpdateStateDistributionManpower(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateDistributionManpowerAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Async Function GetDistributionManpower(ByVal code As String) As Task(Of ActionResult(Of DistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionManpowerAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Async Function GetDistributionManpowerByEmployeeIdAndYearMonth(ByVal employeeId As Integer, ByVal year As Integer, ByVal month As Integer) As Task(Of ActionResult(Of DistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionManpowerByEmployeeIdAndYearMonthAsync(employeeId, year, month)
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Async Function GetDistributionManpowerById(id As Integer) As Task(Of DistributionManpower)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionManpowerByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Async Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriodAsync(year, month)
    End Function

    Public Function ListPeriodWithDataByMaximumPeriodSimple(ByVal year As Integer, ByVal month As Integer) As List(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriod(year, month)
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Async Function ListDistributionManpowerByYearMonth(ByVal year As Integer, ByVal month As Integer) As Task(Of List(Of DistributionManpower))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListDistributionManpowerByYearMonthAsync(year, month)
    End Function

    Function ListEmployeeWithActiveContract() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListEmployeeWithActiveContract()
    End Function

    Function GetCollectionLiquidationByYearMont(year As Integer, month As Integer) As XPCollection(Of PayrollRepository.PayrollLiquidation)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetCollectionLiquidationByYearMont(year, month)
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
