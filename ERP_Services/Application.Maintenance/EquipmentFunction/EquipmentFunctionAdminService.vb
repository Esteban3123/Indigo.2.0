#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Maintenance
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class EquipmentFunctionAdminService
    Implements IEquipmentFunctionAdminService

#Region "Builders"

    Private Const FORM_NAME As String = "FrmEquipmentFunction"

    'Repositorios
    Private _MaintenanceSequenceDetailRepository As IMaintenanceSequenceDetailRepository
    Private _EquipmentFunctionRepository As IEquipmentFunctionRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentFunctionRepository">Repositorio de tipo de equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentFunctionRepository As IEquipmentFunctionRepository, ByVal MaintenanceSequenceDetailRepository As IMaintenanceSequenceDetailRepository)
        If (EquipmentFunctionRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del tipo de equipo")
        End If
        _EquipmentFunctionRepository = EquipmentFunctionRepository
        _MaintenanceSequenceDetailRepository = MaintenanceSequenceDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetEquipmentFunctionByCode(code As String, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionAdminService.GetEquipmentFunctionByCode
        Try
            Dim equipmentFunction = _EquipmentFunctionRepository.GetEquipmentFunctionByCode(code)
            Return New ActionResult(Of EquipmentFunction) With {.StateResult = True, .ObjectEmbbeded = equipmentFunction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EquipmentFunction) With {.StateResult = False, .Message = {Utils.GetInnerExceptionMessageToString(ex)}.ToString}
        End Try
    End Function

    Public Function SaveEquipmentFunction(EquipmentFunction As EquipmentFunction, audit As AuditMessage, Optional idSequense As Long = Nothing) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionAdminService.SaveEquipmentFunction
        Try
            Dim MessageResult As String = String.Empty
            Dim unitOfWork As IUnitWork = Me._EquipmentFunctionRepository.UnitWork
            Dim sequenseUnitOfWork As IUnitWork = Me._MaintenanceSequenceDetailRepository.UnitWork
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If String.IsNullOrEmpty(EquipmentFunction.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._MaintenanceSequenceDetailRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            EquipmentFunction.Code = res
                            seq.Next += 1
                            Me._MaintenanceSequenceDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of EquipmentFunction) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), EquipmentFunction.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of EquipmentFunction) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                End If

                Dim auxObjEntity As EquipmentFunction = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of EquipmentFunction)
                Dim status As Integer

                If EquipmentFunction.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    EquipmentFunction.CreationUser = audit.CodeUser
                    EquipmentFunction.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = EquipmentFunction.OriginalValue
                    EquipmentFunction.ModificationUser = audit.CodeUser
                    EquipmentFunction.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._EquipmentFunctionRepository.SaveEntity(EquipmentFunction)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of EquipmentFunction)(EquipmentFunction, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                EquipmentFunction.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of EquipmentFunction) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = EquipmentFunction, .Message = MessageResult}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EquipmentFunction) With {.StateResult = False, .Message = {Utils.GetInnerExceptionMessageToString(ex)}.ToString}
        End Try
    End Function

    Public Function ListAllEquipmentFunction() As List(Of EquipmentFunction) Implements IEquipmentFunctionAdminService.ListAllEquipmentFunction
        Try
            Return _EquipmentFunctionRepository.ListAllEquipmentFunction
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ChangeStateEquipmentFunction(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionAdminService.ChangeStateEquipmentFunction
        Try
            Dim equipmentFunction = _EquipmentFunctionRepository.GetEquipmentFunctionById(id)
            If equipmentFunction IsNot Nothing AndAlso equipmentFunction.Id > 0 Then
                equipmentFunction.State = state
            End If
            Dim result = Me.SaveEquipmentFunction(equipmentFunction, audit)
            If result.StateResult Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EquipmentFunction) With {.StateResult = False, .Message = {Utils.GetInnerExceptionMessageToString(ex)}.ToString}
        End Try
    End Function

    Public Function DeleteEquipmentFunction(id As Integer, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionAdminService.DeleteEquipmentFunction
        Try
            Dim equipmentFunction = _EquipmentFunctionRepository.GetEquipmentFunctionById(id)
            If equipmentFunction IsNot Nothing AndAlso equipmentFunction.Id > 0 Then
                If equipmentFunction.EquipmentRegistration.Any() Then
                    Return New ActionResult(Of EquipmentFunction) With {.StateResult = False, .Message = {"Existe un Registro de Mantenimiento relacionado"}.ToString}
                End If
            End If

            Dim unitOfWork As IUnitWork = Me._EquipmentFunctionRepository.UnitWork
            Dim sequenseUnitOfWork As IUnitWork = Me._MaintenanceSequenceDetailRepository.UnitWork
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EquipmentFunction)(equipmentFunction, audit, status)

                equipmentFunction.MarkAsDeleted()
                Me._EquipmentFunctionRepository.SaveEntity(equipmentFunction)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of EquipmentFunction) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EquipmentFunction) With {.StateResult = False, .Message = {Utils.GetInnerExceptionMessageToString(ex)}.ToString}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EquipmentFunctionRepository = Nothing
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
