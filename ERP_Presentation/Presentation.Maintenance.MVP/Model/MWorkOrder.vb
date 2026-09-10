Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

Public Class MWorkOrder
    Inherits MBlockRecordAndSequenseMaintenance
    Implements IDisposable

    Dim Indigo As SessionValues
    Public Sub New(tag As String)
        MyBase.New(tag)
        Indigo = SessionValues.Instance
        Indigo.AuditMessageWcf.Functional = tag
    End Sub

    Public Async Function GetWorkOrder(code As String) As Task(Of WorkOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetWorkOrderByCodeAsync(code)
    End Function

    Public Async Function SaveWorkOrderAsync(listWorkOrder As List(Of WorkOrder)) As Task(Of ActionResult(Of List(Of WorkOrder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveWorkOrderAsync(listWorkOrder, Indigo.AuditMessageWcf)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
