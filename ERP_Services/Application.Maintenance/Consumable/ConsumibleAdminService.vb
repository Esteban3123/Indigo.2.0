#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class ConsumibleAdminService
    Implements IConsumableAdminService

    'Repositorio de el tipo de equipo
    Private _ConsumableRepository As IConsumableRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <param name="ConsumableAdminService">Repositorio de tipo de equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ConsumableAdminService As IConsumableRepository, sequenceRepository As IMaintenanceSequenceDetailRepository)
        If (ConsumableAdminService Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del consumible")
        End If
        _sequenceRepository = sequenceRepository
        _ConsumableRepository = ConsumableAdminService
    End Sub

    Public Function DeleteConsumable(Consumable As Consumable, audit As AuditMessage) As Boolean Implements IConsumableAdminService.DeleteConsumable
        If Consumable Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _ConsumableRepository.UnitWork
        Try
            Consumable.MarkAsDeleted()
            _ConsumableRepository.DeleteEntity(Consumable)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of Consumable)(Consumable, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetConsumable(codeConsumablee As String) As Consumable Implements IConsumableAdminService.GetConsumable
        If String.IsNullOrEmpty(codeConsumablee) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try

            Return _ConsumableRepository.GetConsumable(codeConsumablee)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Consumable()
        End Try
    End Function

    Public Function ListAllConsumable() As List(Of Consumable) Implements IConsumableAdminService.ListAllConsumable
        Try
            Return _ConsumableRepository.ListAllConsumable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveConsumable(Consumable As Consumable, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.Consumable) Implements IConsumableAdminService.SaveConsumable
        If Consumable Is Nothing Then
            Throw New ArgumentNullException("tipo de equipo vacio")
        End If
        Dim unitWork As IUnitWork = _ConsumableRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxConsumable As Domain.Entities.Consumable
            If Consumable.ChangeTracker.State = ObjectState.Modified Then
                auxConsumable = _ConsumableRepository.GetConsumable(Consumable.Code, True)
            End If

            If Consumable.Code Is Nothing OrElse Consumable.Code.Trim().Equals(String.Empty) Then
                seq = _sequenceRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Consumable.Code = res
                        seq.Next += 1
                        Me._sequenceRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Consumable) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Consumable) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._ConsumableRepository.SaveEntity(Consumable)
            End If

            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Consumable).Name, audit.Functional, Consumable.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Consumable)(Consumable, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Consumable).Name, audit.Functional, Consumable.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Consumable)(Consumable, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxConsumable)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Consumable) With {.StateResult = True, .ObjectEmbbeded = Consumable}

        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Consumable) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Consumable) Implements IConsumableAdminService.ChangeState
        'Dim Consumable As Consumable = _ConsumableRepository.GetConsumable(code)
        'Consumable.State = state
        'Consumable.MarkAsModified()
        'Return SaveConsumable(Consumable, audit)

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
            Dim Consumable As Consumable = Me._ConsumableRepository.GetConsumable(code.Trim())
            If Consumable IsNot Nothing AndAlso Consumable.Id > 0 Then
                Consumable.State = state
            End If
            Dim result = Me.SaveConsumable(Consumable, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Consumable) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _ConsumableRepository = Nothing
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
