'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

#End Region

Public Class AccountReceivableConceptAdminService
    Implements IAccountReceivableConceptAdminService

    Private Const FORM_NAME As String = "FrmCxCConcepts"
    Private _accountReceivableConceptRepository As IAccountReceivableConceptRepository

    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository

#Region "Builder"
    Public Sub New(ByVal accountReceivableConceptRepository As IAccountReceivableConceptRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        If (accountReceivableConceptRepository Is Nothing) Then
            Throw New ArgumentNullException("accountReceivableConceptRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _accountReceivableConceptRepository = accountReceivableConceptRepository
        _sequensePortfolioDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para eliminar un concepto de cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivableConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">PortfolioConcept Vacio</exception>
    Public Function DeleteaccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, audit As AuditMessage) As ActionResult Implements IAccountReceivableConceptAdminService.DeleteAccountReceivableConcept
        'If accountReceivableConcept Is Nothing Then
        '    Throw New ArgumentNullException("accountReceivableConcept Vacio")
        'End If
        'Dim UnitOfWork As IUnitWork = _accountReceivableConceptRepository.UnitWork
        'Try
        '    accountReceivableConcept.ModificationDate = DateTime.Now
        '    accountReceivableConcept.ModificationUser = audit.CodeUser
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountReceivableConcept)(accountReceivableConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    _accountReceivableConceptRepository.DeleteEntity(accountReceivableConcept)
        '    UnitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}

        'Catch ex As OptimisticConcurrencyException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    UnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try





        If accountReceivableConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._accountReceivableConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                accountReceivableConcept.ModificationUser = audit.CodeUser
                accountReceivableConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountReceivableConcept)(accountReceivableConcept, audit, status)

                'While accountReceivableConcept.InvoiceCategoriesUser.Count > 0
                '    accountReceivableConcept.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                accountReceivableConcept.MarkAsDeleted()
                Me._accountReceivableConceptRepository.SaveEntity(accountReceivableConcept)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener todos los conceptos de cuentas por pagar
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableConcept(audit As AuditMessage) As Object Implements IAccountReceivableConceptAdminService.GetAllAccountReceivableConcept
        Try
            Dim accountReceivableConcept = _accountReceivableConceptRepository.GetAllAccountReceivableConcept()
            For Each item As AccountReceivableConcept In accountReceivableConcept
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioConcept)(accountReceivableConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            Next
            Return accountReceivableConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code Vacio</exception>
    Public Function GetAccountReceivableConceptByCode(code As String, audit As AuditMessage) As Object Implements IAccountReceivableConceptAdminService.GetAccountReceivableConceptByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code Vacio")
        End If
        Try
            Dim accountReceivableConcept As AccountReceivableConcept = _accountReceivableConceptRepository.GetAccountReceivableConceptByCode(code)
            If accountReceivableConcept IsNot Nothing AndAlso accountReceivableConcept.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountReceivableConcept)(accountReceivableConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return accountReceivableConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar un concepto de cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivableConcept"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">portfolioConcept Vacio</exception>
    Public Function SaveAccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountReceivableConcept) Implements IAccountReceivableConceptAdminService.SaveAccountReceivableConcept
        'If accountReceivableConcept Is Nothing Then
        '    Throw New ArgumentNullException("accountReceivableConcept Vacio")
        'End If
        'Dim accountReceivableConceptUnitOfWork As IUnitWork = _accountReceivableConceptRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        'Try
        '    Dim seq As PortfolioSequenceDetail = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of AccountReceivableConcept)
        '    Dim status As Integer
        '    Dim auxAccountReceivableConcept As AccountReceivableConcept = Nothing

        '    If accountReceivableConcept.Code Is Nothing OrElse accountReceivableConcept.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequensePortfolioDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                accountReceivableConcept.Code = res
        '                seq.Next += 1
        '                Me._sequensePortfolioDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If


        '    If accountReceivableConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        accountReceivableConcept.CreationUser = audit.CodeUser
        '        accountReceivableConcept.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        accountReceivableConcept.ModificationDate = DateTime.Now
        '        accountReceivableConcept.ModificationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxAccountReceivableConcept = _accountReceivableConceptRepository.GetAccountReceivableConceptByCode(accountReceivableConcept.Code, False)
        '    End If

        '    _accountReceivableConceptRepository.SaveEntity(accountReceivableConcept)
        '    accountReceivableConceptUnitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of AccountReceivableConcept)(accountReceivableConcept, audit, status, auxAccountReceivableConcept)
        '    auditProcess.Execute()
        '    Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = True, .ObjectEmbbeded = accountReceivableConcept}

        'Catch ex As OptimisticConcurrencyException
        '    accountReceivableConceptUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    accountReceivableConceptUnitOfWork.RollbackChanges()
        '    sequenseUnitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False}
        'End Try



        If accountReceivableConcept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._accountReceivableConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(accountReceivableConcept.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequensePortfolioDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            accountReceivableConcept.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AccountReceivableConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), accountReceivableConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AccountReceivableConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AccountReceivableConcept = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountReceivableConcept)
                Dim status As Integer

                If accountReceivableConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    accountReceivableConcept.CreationUser = audit.CodeUser
                    accountReceivableConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = accountReceivableConcept.OriginalValue
                    accountReceivableConcept.ModificationUser = audit.CodeUser
                    accountReceivableConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._accountReceivableConceptRepository.SaveEntity(accountReceivableConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountReceivableConcept)(accountReceivableConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                accountReceivableConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = accountReceivableConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>    
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountReceivableConcept) Implements IAccountReceivableConceptAdminService.ChangeState
        'Dim accountReceivableConcept As AccountReceivableConcept = GetAccountReceivableConceptByCode(code, audit)
        'accountReceivableConcept.Status = state
        'accountReceivableConcept.MarkAsModified()
        'Return SaveAccountReceivableConcept(accountReceivableConcept, audit)



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
            Dim accountReceivableConcept As AccountReceivableConcept = Me._accountReceivableConceptRepository.GetAccountReceivableConceptByCode(code.Trim())
            If accountReceivableConcept IsNot Nothing AndAlso accountReceivableConcept.Id > 0 Then
                accountReceivableConcept.Status = state
            End If
            Dim result = Me.SaveAccountReceivableConcept(accountReceivableConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountReceivableConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableConceptById(id As Integer) As AccountReceivableConcept Implements IAccountReceivableConceptAdminService.GetAccountReceivableConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim accountReceivableConcept = Me._accountReceivableConceptRepository.GetAccountReceivableConceptById(id)
            Return accountReceivableConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
            _accountReceivableConceptRepository = Nothing
            _sequensePortfolioDRepository = Nothing
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
