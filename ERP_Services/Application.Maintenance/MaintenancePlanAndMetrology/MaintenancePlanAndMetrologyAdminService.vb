Imports System.Transactions
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class MaintenancePlanAndMetrologyAdminService
    Implements IMaintenancePlanAndMetrologyAdminService

    Private ReadOnly _maintenancePlanRepository As IMaintenancePlanAndMetrologyRepository
    Private ReadOnly _maintenancePlanProgramated As IMaintenancePlanProgramatedRepository

    Public Sub New(maintenancePlanRepository As IMaintenancePlanAndMetrologyRepository,
                   maintenancePlanProgramated As IMaintenancePlanProgramatedRepository)
        Me._maintenancePlanRepository = maintenancePlanRepository
        Me._maintenancePlanProgramated = maintenancePlanProgramated
    End Sub

    ''' <summary>
    ''' Obtiene la programacion para un activo
    ''' </summary>
    ''' <param name="PhysicalAssetId"></param>
    ''' <returns></returns>
    Public Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, protocolId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyAdminService.GetMaintenancePlanAndMetrology
        Try
            Return _maintenancePlanRepository.GetMaintenancePlanAndMetrology(PhysicalAssetId, protocolId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyAdminService.GetMaintenancePlanAndMetrologyById
        Try
            Return _maintenancePlanRepository.GetMaintenancePlanAndMetrologyById(PlanMaintenanceId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteMaintenancePlanAndMetrology(id As List(Of Integer)) As ActionResult Implements IMaintenancePlanAndMetrologyAdminService.DeleteMaintenancePlanAndMetrology
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                New TransactionOptions() With {.Timeout = TransactionManager.DefaultTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If id Is Nothing OrElse Not id.Any() Then
                    scope.Dispose()
                    Return New ActionResult() With {.StateResult = False, .Message = "Debe seleccionar algún elemento a eliminar"}
                End If

                Dim xml As String = $"<ProgramatedDateIds>{String.Join(vbCrLf, id.Select(Function(m)
                                                                                             Return $"<Item><Id>{m}</Id></Item>"
                                                                                         End Function))}</ProgramatedDateIds>"
                Dim deletedResult As SP_DeleteMaintenanceProgramed_Result = _maintenancePlanRepository.SP_DeleteMaintenanceProgramed(xml)
                If Not deletedResult.StatusResult Then
                    scope.Dispose()
                    Return New ActionResult() With {.StateResult = False, .Message = deletedResult.MessageResult}
                End If

                '_maintenancePlanRepository.RemoveRange(id)

                ''Dim maintenancePlan As MaintenancePlanAndMetrology = _maintenancePlanRepository.GetMaintenancePlanAndMetrologyById(id)
                ''If maintenancePlan Is Nothing Then
                ''    scope.Dispose()
                ''    Return New ActionResult() With {.StateResult = False, .Message = "El plan de mantenimiento no existe o ya ha sido eliminado"}
                ''End If

                ''_maintenancePlanRepository.DeleteEntity(maintenancePlan)
                '_maintenancePlanRepository.UnitWork.Commit()

                scope.Complete()
                Return New ActionResult() With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveMaintenancePlanAndMetrology(maintenancePlan As MaintenancePlanAndMetrology) As ActionResult Implements IMaintenancePlanAndMetrologyAdminService.SaveMaintenancePlanAndMetrology
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                New TransactionOptions() With {.Timeout = TransactionManager.DefaultTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                _maintenancePlanRepository.SaveEntity(maintenancePlan)
                _maintenancePlanRepository.UnitWork.Commit()

                scope.Complete()
                Return New ActionResult() With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
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
