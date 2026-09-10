Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent

Public Class MProtocolMaintenance
    Inherits MBlockRecordAndSequenseMaintenance
    Implements IDisposable

    Dim Indigo As SessionValues
    Public Sub New(tag As String)
        MyBase.New(tag)
        Indigo = SessionValues.Instance
        Indigo.AuditMessageWcf.Functional = tag
    End Sub

    Public Async Function SaveMaintenanceProtocolAsync(maintenanceProtocol As MaintenanceProtocol, idCurrentSequence As Long) As Task(Of ActionResult(Of MaintenanceProtocol))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveMaintenanceProtocolAsync(maintenanceProtocol, Indigo.AuditMessageWcf, idCurrentSequence)
    End Function

    Public Async Function DeleteMaintenanceProtocol(maintenanceProtocol As MaintenanceProtocol) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteMaintenanceProtocolAsync(maintenanceProtocol, Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetMaintenanceProtocol(code As String) As Task(Of MaintenanceProtocol)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetMaintenanceProtocolAsync(code, Indigo.AuditMessageWcf)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of MaintenanceProtocol))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateMaintenanceProtocolAsync(code, state, Indigo.AuditMessageWcf)
    End Function

    Public Function ListMaintenanceProtocolByEquipment(FixedAssetItemId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListMaintenanceProtocolByStatusAndFixedAssetItemId(FixedAssetItemId, True)
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
