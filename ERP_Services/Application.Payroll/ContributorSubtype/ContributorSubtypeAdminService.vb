'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Imports Application.Billing
Imports Application.Security
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.PayrollRepository
Imports Microsoft.VisualBasic.Devices


#End Region

Public Class ContributorSubtypeAdminService
    Implements IContributorSubtypeAdminService

    Private _ContributorSubtypeRepository As IContributorSubtypeRepository
    Private _secuenseDRepository As IPayrollSequenceDetailRepository
    Public Const FORM_NAME As String = "FrmContributorSubtype"

    Public Sub New(ByVal contributorSubtypeRepository As IContributorSubtypeRepository, secuenseDRepository As IPayrollSequenceDetailRepository)
        If contributorSubtypeRepository Is Nothing Then
            Throw New ArgumentNullException("contributorSubtypeRepository Vacio")
        End If

        _ContributorSubtypeRepository = contributorSubtypeRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function ListAllContributorSubtype() As List(Of ContributorSubtype) Implements IContributorSubtypeAdminService.ListAllContributorSubtype
        Try
            Return _ContributorSubtypeRepository.ListAllContributorSubtype()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage) As ActionResult Implements IContributorSubtypeAdminService.DeleteContributorSubtype
        If contributorSubtype Is Nothing Then
            Throw New ArgumentNullException("contributorSubtype")
        End If
        Dim unitOfWork As IUnitWork = _ContributorSubtypeRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ContributorSubtype)(contributorSubtype, audit, status)

                contributorSubtype.MarkAsDeleted()
                Me._ContributorSubtypeRepository.SaveEntity(contributorSubtype)
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

    Public Function SaveContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContributorSubtype) Implements IContributorSubtypeAdminService.SaveContributorSubtype
        If contributorSubtype Is Nothing Then
            Throw New ArgumentNullException("contributorSubtype Control Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _ContributorSubtypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(contributorSubtype.Code) Then
                    Dim seq As PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            contributorSubtype.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ContributorSubtype) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), contributorSubtype.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ContributorSubtype) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If


                Dim auxCommon As ContributorSubtype = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ContributorSubtype)
                Dim status As Integer

                If contributorSubtype.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    contributorSubtype.CreationUser = audit.CodeUser
                    contributorSubtype.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else

                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = contributorSubtype.OriginalValue
                    contributorSubtype.ModificationUser = audit.CodeUser
                    contributorSubtype.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ContributorSubtypeRepository.SaveEntity(contributorSubtype)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ContributorSubtype)(contributorSubtype, audit, status, auxCommon)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                contributorSubtype.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ContributorSubtype) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = contributorSubtype, .Message = MessageResult}

            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of ContributorSubtype) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContributorSubtype) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetContributorSubtypeByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContributorSubtype) Implements IContributorSubtypeAdminService.GetContributorSubtypeByCode
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code JustificationControl vacio")
        End If
        Try
            Dim contributorSubtype = _ContributorSubtypeRepository.GetContributorSubtypeByCode(code)

            Return New ActionResult(Of ContributorSubtype) With {.StateResult = True, .ObjectEmbbeded = contributorSubtype}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetContributorSubtypeById(ByVal id As Integer) As ActionResult(Of ContributorSubtype) Implements IContributorSubtypeAdminService.GetContributorSubtypeById
        If Not (id > 0) Then
            Throw New ArgumentNullException("GetContributorSubtypeById Vacio")
        End If
        Try
            Dim contributorSubtype As ContributorSubtype = _ContributorSubtypeRepository.GetContributorSubtypeById(id)
            Return New ActionResult(Of ContributorSubtype) With {.StateResult = True, .ObjectEmbbeded = contributorSubtype}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContributorSubtype) With {.StateResult = False, .Message = {ex.Message}.ToString}
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


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub


#End Region

End Class
