Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class GroupAdminService

    Implements IGroupAdminService

    'Repositorio de unidad de negocio
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' inicia el repositorio de los Grupos
    ''' </summary>
    ''' <param name="groupRepository">Repositorio de Grupos </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal groupRepository As IGroupRepository)
        If (groupRepository Is Nothing) Then
            Throw New ArgumentNullException("GroupRepository vacio")
        End If
        _groupRepository = groupRepository
    End Sub

    ''' <summary>
    ''' Elimina un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    Public Function DeleteGroup(Group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Group) Implements IGroupAdminService.DeleteGroup
        Dim result As New ActionMessageResult(Of Group)
        result.StateResult = True
        If Group Is Nothing Then
            Throw New ArgumentNullException("Group Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _groupRepository.UnitWork
        Try
            'While FunctionalUnit.FunctionalUnitResponsible.Count > 0
            '    FunctionalUnit.FunctionalUnitResponsible.Item(FunctionalUnit.FunctionalUnitResponsible.Count() - 1).MarkAsDeleted()
            'End While
            'FunctionalUnit.MarkAsDeleted()
            '_FunctionalRepository.SaveEntity(FunctionalUnit)


            While Group.GroupEventConcept.Count > 0
                Group.GroupEventConcept.Item(Group.GroupEventConcept.Count() - 1).MarkAsDeleted()
            End While

            Group.MarkAsDeleted()
            _groupRepository.SaveEntity(Group)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(Group.GetType.Name, audit.Functional, Group.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Group)(Group, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", Group.Code))
            Return result
            UnitOfWork.RollbackChanges()
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Grupo
    ''' </summary>
    ''' <param name="code">Código del Grupo</param>
    ''' <returns></returns>
    Public Function GetGroup(code As String) As Group Implements IGroupAdminService.GetGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("codeGroup Vacio")
        End If
        Try
            Return _groupRepository.GetGroup(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllGroups() As List(Of Group) Implements IGroupAdminService.ListAllGroups
        Try
            Return _groupRepository.ListAllGroups()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    Public Function SaveGroup(Group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Object Implements IGroupAdminService.SaveGroup
        If Group Is Nothing Then
            Throw New ArgumentNullException("Group Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _groupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Group)
            Dim auxGroup As Group = Nothing
            Dim status As Integer
            If Group.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Group.ModificationUser = audit.CodeUser
                Group.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxGroup = _groupRepository.GetGroup(Group.Code, False)
            Else
                Group.CreationUser = audit.CodeUser
                Group.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            'Valido si se va a guardar o a eliminar
            _groupRepository.SaveEntity(Group)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Group)(Group, audit, status, auxGroup)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Grupo por Id
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupById(groupId As String) As Group Implements IGroupAdminService.GetGroupById
        If String.IsNullOrEmpty(groupId) Then
            Throw New ArgumentNullException("groupId Vacio")
        End If
        Try
            Return _groupRepository.GetGroupById(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene unos grupos filtrado por empresa
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupsByCompanyId(ByVal companyId As String) As List(Of Group) Implements IGroupAdminService.GetGroupsByCompanyId
        If String.IsNullOrEmpty(companyId) Then
            Throw New ArgumentNullException("companyId Vacio")
        End If
        Try
            Return _groupRepository.GetGroupsByCompanyId(companyId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Grupo y sus correspondientes liquidaciones
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Public Function GetGroupLiquidationById(ByVal GroupId As String) As List(Of Group) Implements IGroupAdminService.GetGroupLiquidationById
        If String.IsNullOrEmpty(GroupId) Then
            Throw New ArgumentNullException("GroupId Vacio")
        End If
        Try
            Return _groupRepository.GetGroupLiquidationById(GroupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function GroupChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IGroupAdminService.GroupChangeState
        Dim ObjGroup As Group = _groupRepository.GetGroup(code, True)
        ObjGroup.State = state
        'ScheduleTemplate.MarkAsModified()
        Return SaveGroup(ObjGroup, audit)
    End Function

    ''' <summary>
    ''' Lista todos los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGroupsByState(Status As Boolean) As List(Of Group) Implements IGroupAdminService.ListGroupsByStatus
        Try
            Return _groupRepository.ListGroupsByStatus(Status)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _groupRepository = Nothing
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
