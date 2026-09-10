'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-05-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class BatchSerialSettingAdminService
    Implements IBatchSerialSettingAdminService, Inject

    ''' <summary>
    ''' repository
    ''' </summary>
    Private ReadOnly _batchSerialSettingRepository As IBatchSerialSettingRepository
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="BatchSerialSettingRepository"></param>
    Public Sub New(BatchSerialSettingRepository As IBatchSerialSettingRepository)
        _batchSerialSettingRepository = BatchSerialSettingRepository
    End Sub


    ''' <summary>
    ''' Guarda  parámetro de Lotes
    ''' </summary>
    ''' <param name="BatchSerialSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveBatchSerialSetting(BatchSerialSetting As BatchSerialSetting, audit As AuditMessage) As ActionResult Implements IBatchSerialSettingAdminService.SaveBatchSerialSetting

        If BatchSerialSetting Is Nothing Then
            Throw New Exception("El objeto a guardar viene vacio")
        End If

        Dim uow As IUnitWork = Me._batchSerialSettingRepository.UnitWork

        Try
            Dim auxObjEntity As BatchSerialSetting = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of BatchSerialSetting)
            Dim status As Integer

            If BatchSerialSetting.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                BatchSerialSetting.CreationUser = audit.CodeUser
                BatchSerialSetting.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxObjEntity = BatchSerialSetting.OriginalValue
                BatchSerialSetting.ModificationUser = audit.CodeUser
                BatchSerialSetting.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            _batchSerialSettingRepository.SaveEntity(BatchSerialSetting)
            uow.Commit()

            auditProcess = New IndigoAuditSimpleEntity(Of BatchSerialSetting)(BatchSerialSetting, audit, status, auxObjEntity)
            auditProcess.Execute()
            BatchSerialSetting.MarkAsUnchanged()

            Dim Message = IIf(status = Infrastructure.CrossCutting.Audit.Actions.Insert, ResourceManager.GetString("SaveMessage"), ResourceManager.GetString("UpdateMessage"))

            Return New ActionResult With {.StateResult = True, .Message = Message}
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' consulta el parametro de configuracion de lote
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBatchSerialSettings() As ActionResult(Of BatchSerialSetting) Implements IBatchSerialSettingAdminService.GetBatchSerialSettings
        Try
            Dim Message As String = String.Empty
            Dim BatchSerialSetting = _batchSerialSettingRepository.GetBatchSerialSetting()

            If BatchSerialSetting Is Nothing OrElse BatchSerialSetting.Id = 0 Then
                Throw New Exception("No existe Registro")
            End If

            Return New ActionResult(Of BatchSerialSetting) With {.StateResult = True, .Message = "Se ejecutó la consulta correctamente", .ObjectEmbbeded = BatchSerialSetting}
        Catch ex As Exception
            Return New ActionResult(Of BatchSerialSetting) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
