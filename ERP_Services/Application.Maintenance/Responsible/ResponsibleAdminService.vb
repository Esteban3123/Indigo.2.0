

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad habitacion
''' </summary>
''' <remarks></remarks>
Public Class ResponsibleAdminService
    Implements IResponsibleAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    'Repositorio de tipo de responsable
    Private _ResponsibleRepository As IResponsibleRepository

    ''' <summary>
    ''' inicia el repositorio de responsable
    ''' </summary>
    ''' <param name="ResponsibleRepository">Repositorio de responsable</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ResponsibleRepository As IResponsibleRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (ResponsibleRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de responsable")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _ResponsibleRepository = ResponsibleRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeleteResponsible(Responsible As Responsible, audit As AuditMessage) As actionresult Implements IResponsibleAdminService.DeleteResponsible
        If Responsible Is Nothing Then
            Throw New ArgumentNullException("responsable vacio")
        End If
        Dim unitWork As IUnitWork = _ResponsibleRepository.UnitWork
        Try
            Responsible.MarkAsDeleted()
            _ResponsibleRepository.DeleteEntity(Responsible)
            unitWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Responsible).Name, audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}

        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    Public Function GetResponsible(CodeResponsible As String, audit As AuditMessage) As Responsible Implements IResponsibleAdminService.GetResponsible
        If String.IsNullOrEmpty(CodeResponsible) Then
            Throw New ArgumentNullException("Codigo de responsable")
        End If
        Try
            Dim responsible As Domain.Maintenance.Entities.Responsible = _ResponsibleRepository.GetResponsible(CodeResponsible)
            If responsible IsNot Nothing AndAlso responsible.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Responsible)(responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return responsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New Responsible()
        End Try
    End Function

    Public Function ListAllResponsible() As List(Of Responsible) Implements IResponsibleAdminService.ListAllResponsible
        Try
            Return _ResponsibleRepository.ListAllResponsible
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveResponsible(Responsible As Responsible, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Responsible) Implements IResponsibleAdminService.SaveResponsible
        If Responsible Is Nothing Then
            Throw New ArgumentNullException("Habitacion vacio")
        End If
        Dim unitWork As IUnitWork = _ResponsibleRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim AuxResponsible = _ResponsibleRepository.GetResponsible(Responsible.Code, False)

            If Responsible.Code Is Nothing OrElse Responsible.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Responsible.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Maintenance.Entities.Responsible) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Maintenance.Entities.Responsible) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._ResponsibleRepository.SaveEntity(Responsible)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Responsible).Name, audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Responsible).Name, audit.Functional, Responsible.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Responsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxResponsible)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Maintenance.Entities.Responsible) With {.StateResult = True, .ObjectEmbbeded = Responsible}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Maintenance.Entities.Responsible) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.Responsible) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateResponsible(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Responsible) Implements IResponsibleAdminService.ChangeStateResponsible
        Dim responsible As Domain.Maintenance.Entities.Responsible = GetResponsible(code, audit)
        responsible.State = state
        responsible.MarkAsModified()
        Return SaveResponsible(responsible, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ResponsibleRepository = Nothing
            _secuenseDRepository = Nothing
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
