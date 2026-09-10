

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class PartAdminService
    Implements IPartAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Maintenance.IMaintenanceSequenceDetailRepository
    'Repositorio de tipo de parte
    Private _PartRepository As IPartRepository

    ''' <summary>
    ''' inicia el repositorio de parte
    ''' </summary>
    ''' <param name="PartRepository">Repositorio de parte</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PartRepository As IPartRepository, ByVal secuenseDRepository As Domain.Maintenance.IMaintenanceSequenceDetailRepository)
        If (PartRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de parte")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PartRepository = PartRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeletePart(Part As Domain.Maintenance.Entities.Part, audit As AuditMessage) As ActionResult Implements IPartAdminService.DeletePart
        If Part Is Nothing Then
            Throw New ArgumentNullException("parte vacio")
        End If
        Dim unitWork As IUnitWork = _PartRepository.UnitWork
        Try
            Part.MarkAsDeleted()
            _PartRepository.DeleteEntity(Part)
            unitWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Part).Name, audit.Functional, Part.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Part)(Part, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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

    Public Function GetPart(codePart As String, audit As AuditMessage) As Domain.Maintenance.Entities.Part Implements IPartAdminService.GetPart
        If String.IsNullOrEmpty(codePart) Then
            Throw New ArgumentNullException("Codigo de parte vacio")
        End If
        Try
            Dim part As Domain.Maintenance.Entities.Part = _PartRepository.GetPart(codePart)
            If part IsNot Nothing AndAlso part.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Part)(part, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return part
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Maintenance.Entities.Part()
        End Try
    End Function

    Public Function ListAllPart() As List(Of Domain.Maintenance.Entities.Part) Implements IPartAdminService.ListAllPart
        Try
            Return _PartRepository.ListAllPart
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SavePart(Part As Domain.Maintenance.Entities.Part, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Maintenance.Entities.Part) Implements IPartAdminService.SavePart
        If Part Is Nothing Then
            Throw New ArgumentNullException("parte vacio")
        End If
        Dim unitWork As IUnitWork = _PartRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As MaintenanceSequenceDetail = Nothing
            Dim AuxPart = _PartRepository.GetPart(Part.Code, False)

            If Part.Code Is Nothing OrElse Part.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Part.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._PartRepository.SaveEntity(Part)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Part).Name, audit.Functional, Part.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Part)(Part, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Part).Name, audit.Functional, Part.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Part)(Part, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxPart)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = True, .ObjectEmbbeded = Part}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePart(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Part) Implements IPartAdminService.ChangeStatePart
        'Dim part As Domain.Maintenance.Entities.Part = GetPart(code, audit)
        'part.State = state
        'part.MarkAsModified()
        'Return SavePart(part, audit)


        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Part As Domain.Maintenance.Entities.Part = GetPart(code, audit)
            If Part IsNot Nothing AndAlso Part.Id > 0 Then
                Part.State = state
            End If
            Dim result = Me.SavePart(Part, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.Part) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PartRepository = Nothing
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
