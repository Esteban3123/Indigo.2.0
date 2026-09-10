#Region "imports"
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

Public Class SalesExecutiveAdminService
    Implements ISalesExecutiveAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private Const FORM_NAME As String = "FrmSalesExecutive"
    Private _SalesExecutiveRepository As ISalesExecutiveRepository
    Private _sequenseRepository As IBillingSequenceDetailRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal SalesExecutiveRepository As ISalesExecutiveRepository, ByVal sequenseRepository As IBillingSequenceDetailRepository)
        _SalesExecutiveRepository = SalesExecutiveRepository
        _sequenseRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por Codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalesExecutiveByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutiveAdminService.GetSalesExecutiveByCode
        Try
            Dim SalesExecutive As SalesExecutive = _SalesExecutiveRepository.GetSalesExecutiveByCode(code)
            Return New ActionResult(Of SalesExecutive) With {.StateResult = True, .ObjectEmbbeded = SalesExecutive}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetSalesExecutiveById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutiveAdminService.GetSalesExecutiveById
        Try
            Dim SalesExecutive As SalesExecutive = _SalesExecutiveRepository.GetSalesExecutiveById(id)
            Return New ActionResult(Of SalesExecutive) With {.StateResult = True, .ObjectEmbbeded = Nothing}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y Actualiza un registro
    ''' </summary>
    ''' <param name="SalesExecutive"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SalesExecutive) Implements ISalesExecutiveAdminService.SaveSalesExecutive
        If SalesExecutive Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If

        Dim unitOfWork As IUnitWork = Me._SalesExecutiveRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(SalesExecutive.Code) Then
                    Dim seq As BillingSequenceDetail = Me._sequenseRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            SalesExecutive.Code = res
                            seq.Next += 1
                            Me._sequenseRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of SalesExecutive) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), SalesExecutive.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of SalesExecutive) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As SalesExecutive = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of SalesExecutive)
                Dim status As Integer

                If SalesExecutive.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    SalesExecutive.CreationUser = audit.CodeUser
                    SalesExecutive.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = SalesExecutive.OriginalValue
                    SalesExecutive.ModificationUser = audit.CodeUser
                    SalesExecutive.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._SalesExecutiveRepository.SaveEntity(SalesExecutive)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of SalesExecutive)(SalesExecutive, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                SalesExecutive.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of SalesExecutive) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = SalesExecutive, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="Id">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateSalesExecutive(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutiveAdminService.ChangeStateSalesExecutive
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim SalesExecutive As SalesExecutive = Me._SalesExecutiveRepository.GetSalesExecutiveById(Id)
            If SalesExecutive IsNot Nothing AndAlso SalesExecutive.Id > 0 Then
                SalesExecutive.Status = state
            End If
            Dim result = Me.SaveSalesExecutive(SalesExecutive, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' eliminar
    ''' </summary>
    ''' <param name="SalesExecutive">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeleteSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutiveAdminService.DeleteSalesExecutive
        If SalesExecutive Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._SalesExecutiveRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                SalesExecutive.ModificationUser = audit.CodeUser
                SalesExecutive.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SalesExecutive)(SalesExecutive, audit, status)
                SalesExecutive.MarkAsDeleted()
                Me._SalesExecutiveRepository.SaveEntity(SalesExecutive)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of SalesExecutive) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SalesExecutive) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _SalesExecutiveRepository = Nothing
            _sequenseRepository = Nothing
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
