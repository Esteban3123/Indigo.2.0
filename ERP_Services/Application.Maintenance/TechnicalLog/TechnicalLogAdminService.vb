

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre el registro tecnico
''' </summary>
''' <remarks></remarks>
Public Class TechnicalLogAdminService
    Implements ITechnicalLogAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    'Repositorio de tipo de registro tecnico
    Private _TechnicalLogRepository As ITechnicalLogRepository

    ''' <summary>
    ''' inicia el repositorio de habitacion
    ''' </summary>
    ''' <param name="TechnicalLogRepository">Repositorio de habitacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TechnicalLogRepository As ITechnicalLogRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (TechnicalLogRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de registro tecnico")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _TechnicalLogRepository = TechnicalLogRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeleteTechnicalLog(TechnicalLog As TechnicalLog, audit As AuditMessage) As ActionResult Implements ITechnicalLogAdminService.DeleteTechnicalLog
        If TechnicalLog Is Nothing Then
            Throw New ArgumentNullException("registro tecnico vacio")
        End If
        Dim unitWork As IUnitWork = _TechnicalLogRepository.UnitWork
        Try

            While TechnicalLog.TechnicalLogMeasurementUnitDetail.Count > 0
                TechnicalLog.TechnicalLogMeasurementUnitDetail(0).MarkAsDeleted()
            End While
            TechnicalLog.MarkAsDeleted()
            _TechnicalLogRepository.SaveEntity(TechnicalLog)
            unitWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(Domain.Entities.TechnicalLog).Name, audit.Functional, TechnicalLog.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TechnicalLog)(TechnicalLog, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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

    Public Function GetTechnicalLog(CodeTechnicalLog As String, audit As AuditMessage) As TechnicalLog Implements ITechnicalLogAdminService.GetTechnicalLog
        If String.IsNullOrEmpty(CodeTechnicalLog) Then
            Throw New ArgumentNullException("Codigo de habitacion")
        End If
        Try
            Dim TechnicalLog = _TechnicalLogRepository.GetTechnicalLog(CodeTechnicalLog)
            If TechnicalLog IsNot Nothing AndAlso TechnicalLog.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TechnicalLog)(TechnicalLog, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return TechnicalLog
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New TechnicalLog()
        End Try
    End Function

    Public Function ListAllTechnicalLog() As List(Of TechnicalLog) Implements ITechnicalLogAdminService.ListAllTechnicalLog
        Try
            Return _TechnicalLogRepository.ListAllTechnicalLog
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveTechnicalLog(TechnicalLog As TechnicalLog, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.TechnicalLog) Implements ITechnicalLogAdminService.SaveTechnicalLog
        If TechnicalLog Is Nothing Then
            Throw New ArgumentNullException("Habitacion vacio")
        End If
        Dim unitWork As IUnitWork = _TechnicalLogRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxTechnicalLog = _TechnicalLogRepository.GetTechnicalLog(TechnicalLog.Code, False)

            If TechnicalLog.Code Is Nothing OrElse TechnicalLog.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        TechnicalLog.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If


            If TechnicalLog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse TechnicalLog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._TechnicalLogRepository.SaveEntity(TechnicalLog)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If TechnicalLog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.TechnicalLog).Name, audit.Functional, TechnicalLog.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TechnicalLog)(TechnicalLog, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf TechnicalLog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.TechnicalLog).Name, audit.Functional, TechnicalLog.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.TechnicalLog)(TechnicalLog, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxTechnicalLog)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = True, .ObjectEmbbeded = TechnicalLog}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = False}
        End Try
    End Function

    Public Function ChangeStateTechnicalLog(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TechnicalLog) Implements ITechnicalLogAdminService.ChangeStateTechnicalLog
        'Dim TechnicalLog As Domain.Entities.TechnicalLog = GetTechnicalLog(code, audit)
        'TechnicalLog.State = state
        'TechnicalLog.MarkAsModified()
        'Return SaveTechnicalLog(TechnicalLog, audit)

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
            Dim TechnicalLog As Domain.Entities.TechnicalLog = GetTechnicalLog(code, audit)
            If TechnicalLog IsNot Nothing AndAlso TechnicalLog.Id > 0 Then
                TechnicalLog.State = state
            End If
            Dim result = Me.SaveTechnicalLog(TechnicalLog, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.TechnicalLog) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _TechnicalLogRepository = Nothing
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
