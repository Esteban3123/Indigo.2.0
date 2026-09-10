
#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad poliza
''' </summary>
''' <remarks></remarks>

Public Class PolizaAdminService
    Implements IPolizaAdminService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    'Repositorio de tipo de fabricante
    Private _PolizaRepository As IPolizaRepository

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="PolizaRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PolizaRepository As IPolizaRepository, ByVal secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (PolizaRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de poliza")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PolizaRepository = PolizaRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeletePoliza(Poliza As Poliza, audit As AuditMessage) As ActionResult Implements IPolizaAdminService.DeletePoliza
        'If Poliza Is Nothing Then
        '    Throw New ArgumentNullException("Poliza vacio")
        'End If
        'Dim unitWork As IUnitWork = _PolizaRepository.UnitWork
        'Try
        '    Poliza.MarkAsDeleted()
        '    _PolizaRepository.DeleteEntity(Poliza)
        '    unitWork.Commit()
        '    'Auditoria básica
        '    IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Poliza).Name, audit.Functional, Poliza.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    'Ausitoria avanzada
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(Poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
        '    unitWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try

        If Poliza Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PolizaRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                'Poliza.ModificationUser = audit.CodeUser
                ' Poliza.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                'Dim auditProcess As New IndigoAuditSimpleEntity(Of Poliza)(Poliza, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Poliza.MarkAsDeleted()
                Me._PolizaRepository.SaveEntity(Poliza)
                unitOfWork.Commit()
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Poliza).Name, audit.Functional, Poliza.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(Poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
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

    Public Function GetPoliza(codePoliza As String, audit As AuditMessage) As Poliza Implements IPolizaAdminService.GetPoliza
        If String.IsNullOrEmpty(codePoliza) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try
            Dim poliza As Domain.Maintenance.Entities.Poliza = _PolizaRepository.GetPoliza(codePoliza)
            If poliza IsNot Nothing AndAlso poliza.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return poliza
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Poliza()
        End Try
    End Function

    Public Function ListAllPoliza() As List(Of Poliza) Implements IPolizaAdminService.ListAllPoliza
        Try
            Return _PolizaRepository.ListAllPoliza
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveMPoliza(Poliza As Poliza, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Poliza) Implements IPolizaAdminService.SavePoliza
        If Poliza Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitWork As IUnitWork = _PolizaRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Nothing
            Dim auxPoliza = _PolizaRepository.GetPoliza(Poliza.Code, False)

            If Poliza.Code Is Nothing OrElse Poliza.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Poliza.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._PolizaRepository.SaveEntity(Poliza)
            End If
            unitWork.Commit()
            sequenseUnitOfWork.Commit()

            If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Poliza).Name, audit.Functional, Poliza.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(Poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Maintenance.Entities.Poliza).Name, audit.Functional, Poliza.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(Poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPoliza)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = True, .ObjectEmbbeded = Poliza}
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False}
        End Try


        'If Poliza Is Nothing Then
        '    Throw New ArgumentNullException("ObjEntity")
        'End If
        'Dim unitOfWork As IUnitWork = Me._PolizaRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
        '        Dim MessageResult As String = String.Empty

        '        If String.IsNullOrEmpty(Poliza.Code) Then
        '            Dim seq As Domain.Entities.MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
        '            If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
        '                Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '                If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                    Poliza.Code = res
        '                    seq.Next += 1
        '                    Me._secuenseDRepository.SaveEntity(seq)
        '                Else
        '                    scope.Dispose()
        '                    Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
        '                End If
        '                MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Poliza.Code), ResourceManager.GetString("SaveMessage"))
        '            Else
        '                scope.Dispose()
        '                Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
        '            End If
        '        Else
        '            MessageResult = ResourceManager.GetString("SaveMessage")
        '        End If

        '        Dim auxObjEntity As Domain.Maintenance.Entities.Poliza = Nothing
        '        Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)
        '        Dim status As Integer

        '        If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Poliza.CreationUser = audit.CodeUser
        '            Poliza.CreationDate = DateTime.Now
        '            status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '        Else
        '            MessageResult = ResourceManager.GetString("UpdateMessage")
        '            auxObjEntity = Poliza.OriginalValue
        '            Poliza.ModificationUser = audit.CodeUser
        '            Poliza.ModificationDate = DateTime.Now
        '            status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        End If

        '        Me._PolizaRepository.SaveEntity(Poliza)
        '        unitOfWork.Commit()
        '        sequenseUnitOfWork.Commit()
        '        auditProcess = New IndigoAuditSimpleEntity(Of Domain.Maintenance.Entities.Poliza)(Poliza, audit, status, auxObjEntity)
        '        auditProcess.Execute()

        '        'Se marca la entidad como sin cambios
        '        Poliza.MarkAsUnchanged()
        '        scope.Complete()
        '        Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ObjEntity, .Message = MessageResult}
        '    End Using
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of Domain.Maintenance.Entities.Poliza) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        'End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePoliza(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Poliza) Implements IPolizaAdminService.ChangeStatePoliza
        'Dim poliza As Domain.Maintenance.Entities.Poliza = GetPoliza(code, audit)
        'poliza.State = state
        'poliza.MarkAsModified()
        'Return SaveMPoliza(poliza, audit)



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
            Dim poliza As Poliza = Me.GetPoliza(code.Trim(), audit)
            If poliza IsNot Nothing AndAlso poliza.Id > 0 Then
                poliza.State = state
            End If
            Dim result = Me.SaveMPoliza(poliza, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Poliza) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PolizaRepository = Nothing
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
