'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class RetirementReasonAdminService
    Implements IRetirementReasonAdminService

    Private Const FORM_NAME As String = "FrmRetirementReason"
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    ' Repositorio de Razones de Retiro
    Private _RetirementReasonRepository As IRetirementReasonRepository

    ''' <summary>
    ''' Contructor el cual inicia la instancia del repositorio de Razones de Retiro
    ''' </summary>
    ''' <param name="repository">Repositorio de Razones de Retiro</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IRetirementReasonRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("RetirementReasonRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _RetirementReasonRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function ChangeStateRetirementReason(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RetirementReason) Implements IRetirementReasonAdminService.ChangeStateRetirementReason
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
            Dim RetirementReason As RetirementReason = Me._RetirementReasonRepository.GetRetirementReason(code.Trim())
            If RetirementReason IsNot Nothing AndAlso RetirementReason.Id > 0 Then
                RetirementReason.State = state
            End If
            Dim result = Me.SaveRetirementReason(RetirementReason, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RetirementReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina la Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IRetirementReasonAdminService.DeleteRetirementReason
        'Dim result As New ActionMessageResult(Of RetirementReason)
        'result.StateResult = True
        'If (RetirementReason Is Nothing = True) Then
        '    Throw New ArgumentNullException("RetirementReason vacío")
        'End If
        'Dim UnitOfWork As IUnitWork = _RetirementReasonRepository.UnitWork
        'Try
        '    _RetirementReasonRepository.DeleteEntity(RetirementReason)
        '    UnitOfWork.Commit()
        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute(RetirementReason.GetType.Name, audit.Functional, RetirementReason.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of RetirementReason)(RetirementReason, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return result
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", RetirementReason.Code))
        '    Return result
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    result.StateResult = False
        '    Return result
        'End Try


        If RetirementReason Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._RetirementReasonRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                RetirementReason.ModificationUser = audit.CodeUser
                RetirementReason.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RetirementReason)(RetirementReason, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(RetirementReason.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                RetirementReason.MarkAsDeleted()
                Me._RetirementReasonRepository.SaveEntity(RetirementReason)
                unitOfWork.Commit()
                IndigoAuditBasic.Execute(RetirementReason.GetType.Name, audit.Functional, RetirementReason.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
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

    ''' <summary>
    ''' Obtiene una Razón de Retiro específica
    ''' </summary>
    ''' <param name="code">Código de Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    ''' <remarks></remarks>
    Public Function GetRetirementReason(code As String) As Domain.Payroll.Entities.RetirementReason Implements IRetirementReasonAdminService.GetRetirementReason
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("code vacío")
        End If
        Try
            Return _RetirementReasonRepository.GetRetirementReason(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las Razones de Retiro
    ''' </summary>
    ''' <returns>Lista de Razones de Retiro</returns>
    ''' <remarks></remarks>
    Public Function ListAllRetirementReason() As List(Of Domain.Payroll.Entities.RetirementReason) Implements IRetirementReasonAdminService.ListAllRetirementReason
        Try
            Return _RetirementReasonRepository.ListAllRetirementReason()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza la Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function SaveRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of Domain.Payroll.Entities.RetirementReason) Implements IRetirementReasonAdminService.SaveRetirementReason
        'If (RetirementReason Is Nothing = True) Then
        '    Throw New ArgumentNullException("RetirementReason vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _RetirementReasonRepository.UnitWork
        'Try

        '    Dim auditProcess As IndigoAuditSimpleEntity(Of RetirementReason)
        '    Dim auxRetirementReason As RetirementReason = Nothing
        '    Dim status As Integer

        '    If RetirementReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        RetirementReason.ModificationUser = audit.CodeUser
        '        RetirementReason.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxRetirementReason = _RetirementReasonRepository.GetRetirementReason(RetirementReason.Code, False)
        '    Else
        '        RetirementReason.CreationUser = audit.CodeUser
        '        RetirementReason.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If

        '    'Valido si se va a guardar o a eliminar
        '    _RetirementReasonRepository.SaveEntity(RetirementReason)
        '    UnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of RetirementReason)(RetirementReason, audit, status, auxRetirementReason)
        '    auditProcess.Execute()
        '    Return True

        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If RetirementReason Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._RetirementReasonRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(RetirementReason.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            RetirementReason.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RetirementReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), RetirementReason.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RetirementReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RetirementReason = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RetirementReason)
                Dim status As Integer

                If RetirementReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    RetirementReason.CreationUser = audit.CodeUser
                    RetirementReason.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _RetirementReasonRepository.GetRetirementReason(RetirementReason.Code, False)
                    RetirementReason.ModificationUser = audit.CodeUser
                    RetirementReason.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._RetirementReasonRepository.SaveEntity(RetirementReason)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RetirementReason)(RetirementReason, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                RetirementReason.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RetirementReason) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = RetirementReason, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RetirementReason) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RetirementReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _RetirementReasonRepository = Nothing
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
