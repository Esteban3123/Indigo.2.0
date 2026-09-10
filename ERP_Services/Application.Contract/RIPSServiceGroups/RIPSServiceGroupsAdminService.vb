'************************************************************
' Assembly         : Application.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
#End Region

Public Class RIPSServiceGroupsAdminService
    Implements IRIPSServiceGroupsAdminService

    Private _RIPSServiceGroupsRepository As IRIPSServiceGroupsRepository
    Private _SequenceDRepository As ISequenseContractDRepository

    Public Const FORM_NAME As String = "FrmRIPSServiceGroups"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="serviceGroupRepository"></param>
    ''' <param name="sequenceDRepository"></param>
    Public Sub New(ByVal serviceGroupRepository As IRIPSServiceGroupsRepository, sequenceDRepository As ISequenseContractDRepository)
        If serviceGroupRepository Is Nothing Then
            Throw New ArgumentNullException("ServiceGroup vacío")
        End If
        _RIPSServiceGroupsRepository = serviceGroupRepository
        _SequenceDRepository = sequenceDRepository
    End Sub

    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista de grupos de servicios</returns>
    Public Function ListAllRIPSServiceGroups() As List(Of RIPSServiceGroups) Implements IRIPSServiceGroupsAdminService.ListAllRIPSServiceGroups
        Try
            Return _RIPSServiceGroupsRepository.ListAllRIPSServiceGroups()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Busca un grupo de servicios RIPS por el ID
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Public Function GetRIPSServiceGroupById(id As Integer) As ActionResult(Of RIPSServiceGroups) Implements IRIPSServiceGroupsAdminService.GetRIPSServiceGroupById
        If Not (id > 0) Then
            Throw New ArgumentNullException("id del service group vacío")
        End If
        Try
            Dim serviceGroup As RIPSServiceGroups = _RIPSServiceGroupsRepository.GetRIPSServiceGroupById(id)
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = True, .ObjectEmbbeded = serviceGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Public Function GetRIPSServiceGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups) Implements IRIPSServiceGroupsAdminService.GetRIPSServiceGroupByCode
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code is null or empty")
        End If

        Try
            Dim serviceGroup As RIPSServiceGroups = _RIPSServiceGroupsRepository.GetRIPSServiceGroupByCode(code)
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = True, .ObjectEmbbeded = serviceGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un grupo de servicios RIPS.
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequense"></param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Function SaveRIPSServiceGroup(serviceGroup As RIPSServiceGroups, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RIPSServiceGroups) Implements IRIPSServiceGroupsAdminService.SaveRIPSServiceGroup
        If serviceGroup Is Nothing Then
            Throw New ArgumentNullException("service group vacío")
        End If

        Dim UnitOfWork As IUnitWork = _RIPSServiceGroupsRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._SequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(serviceGroup.Code) Then
                    Dim seq As ContractSequenceDetail = Me._SequenceDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            serviceGroup.Code = res
                            seq.Next += 1
                            Me._SequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RIPSServiceGroups) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.ContractSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), serviceGroup.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RIPSServiceGroups) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCommon As RIPSServiceGroups = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RIPSServiceGroups)
                Dim status As Integer

                If serviceGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    serviceGroup.CreationUser = audit.CodeUser
                    serviceGroup.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = serviceGroup.OriginalValue
                    serviceGroup.ModificationUser = audit.CodeUser
                    serviceGroup.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._RIPSServiceGroupsRepository.SaveEntity(serviceGroup)
                UnitOfWork.Commit()
                sequenceUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RIPSServiceGroups)(serviceGroup, audit, status, auxCommon)
                auditProcess.Execute()

                serviceGroup.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RIPSServiceGroups) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = serviceGroup, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSServiceGroups) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado del grupo de servicio RIPS entre -activo- o -inactivo-
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio RIPS</param>
    ''' <param name="state">Nuevo estado</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Grupo de servicio RIPS guardado</returns>
    Public Function ChangeStateRIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RIPSServiceGroups) Implements IRIPSServiceGroupsAdminService.ChangeStateRIPSServiceGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code vacío")
        End If
        Dim ServiceGroup As RIPSServiceGroups = _RIPSServiceGroupsRepository.GetRIPSServiceGroupByCode(code)
        ServiceGroup.Status = state
        Return SaveRIPSServiceGroup(ServiceGroup, audit)
    End Function

    ''' <summary>
    ''' Elimina el registro de un grupo de servicios RIPS
    ''' </summary>
    ''' <param name="ServiceGroup">Instancia de un grupo de servicios RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteRIPSServiceGroup(serviceGroup As RIPSServiceGroups, audit As AuditMessage) As ActionResult Implements IRIPSServiceGroupsAdminService.DeleteRIPSServiceGroup
        If serviceGroup Is Nothing Then
            Throw New ArgumentNullException("BillingJustificationControl")
        End If
        Dim unitOfWork As IUnitWork = _RIPSServiceGroupsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RIPSServiceGroups)(serviceGroup, audit, status)
                serviceGroup.MarkAsDeleted()
                Me._RIPSServiceGroupsRepository.SaveEntity(serviceGroup)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()

                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub





#End Region
End Class
