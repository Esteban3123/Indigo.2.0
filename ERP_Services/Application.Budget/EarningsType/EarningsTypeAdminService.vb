'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class EarningsTypeAdminService
    Implements IEarningsTypeAdminService


#Region "Fields"
    Private Const FORM_NAME As String = "FrmEarningsType"
    ''' <summary>
    ''' Repositorio de tipos de ingreso
    ''' </summary>
    Private _earningsTypeRepository As IEarningsTypeRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="earningsTypeRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal earningsTypeRepository As IEarningsTypeRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If earningsTypeRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _earningsTypeRepository = earningsTypeRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    Public Function GetEarningsType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType Implements IEarningsTypeAdminService.GetEarningsType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim earningsType As RevenueType = Me._earningsTypeRepository.GetEarningsType(code.Trim(), validityId, type)
            If earningsType IsNot Nothing AndAlso earningsType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return earningsType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    Public Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType) Implements IEarningsTypeAdminService.ListEarningsTypeByValidity
        Try
            Return _earningsTypeRepository.ListEarningsTypeByValidity(ValidityId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetEarningsTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType Implements IEarningsTypeAdminService.GetEarningsTypeByValidity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim earningsType As RevenueType = Me._earningsTypeRepository.GetEarningsTypeByValidity(code.Trim(), ValidityId)
            If earningsType IsNot Nothing AndAlso earningsType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return earningsType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEarningsType(earningsType As RevenueType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RevenueType) Implements IEarningsTypeAdminService.SaveEarningsType
        'If earningsType Is Nothing Then
        '    Throw New ArgumentNullException("earningsType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._earningsTypeRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        'Try
        '    Dim seq As BudgetSequenceDetail = Nothing
        '    If earningsType.Code Is Nothing OrElse earningsType.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                earningsType.Code = res
        '                seq.Next += 1
        '                Me._sequenseBudgetDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxRevenueType As RevenueType = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
        '    Dim status As Integer

        '    If earningsType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        earningsType.CreationUser = audit.CodeUser
        '        earningsType.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxRevenueType = earningsType.OriginalValue
        '        earningsType.ModificationUser = audit.CodeUser
        '        earningsType.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._earningsTypeRepository.SaveEntity(earningsType)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, status, auxRevenueType)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    earningsType.MarkAsUnchanged()

        '    Return New ActionResult(Of RevenueType) With {.StateResult = True, .ObjectEmbbeded = earningsType}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try


        If earningsType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._earningsTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(earningsType.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            earningsType.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RevenueType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), earningsType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RevenueType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RevenueType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
                Dim status As Integer

                If earningsType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    earningsType.CreationUser = audit.CodeUser
                    earningsType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = earningsType.OriginalValue
                    earningsType.ModificationUser = audit.CodeUser
                    earningsType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._earningsTypeRepository.SaveEntity(earningsType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                earningsType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RevenueType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = earningsType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina el tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEarningsType(earningsType As RevenueType, audit As AuditMessage) As ActionResult Implements IEarningsTypeAdminService.DeleteEarningsType
        'If earningsType Is Nothing Then
        '    Throw New ArgumentNullException("earningsType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._earningsTypeRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
        '    auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._earningsTypeRepository.DeleteEntity(earningsType)
        '    unitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try



        If earningsType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._earningsTypeRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                earningsType.ModificationUser = audit.CodeUser
                earningsType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RevenueType)(earningsType, audit, status)

                'While earningsType.InvoiceCategoriesUser.Count > 0
                '    earningsType.InvoiceCategoriesUser(earningsType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                earningsType.MarkAsDeleted()
                Me._earningsTypeRepository.SaveEntity(earningsType)
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

    ''' <summary>
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateEarningsType(code As String, validityId As Integer, type As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of RevenueType) Implements IEarningsTypeAdminService.ChangeStateEarningsType
        'Dim earningsType As RevenueType = GetEarningsType(code, validityId, type, audit)
        'earningsType.Status = state
        'earningsType.MarkAsModified()
        'Return SaveEarningsType(earningsType, audit)
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
            Dim earningsType As RevenueType = GetEarningsType(code, validityId, type, audit)
            If earningsType IsNot Nothing AndAlso earningsType.Id > 0 Then
                earningsType.Status = state
            End If
            Dim result = Me.SaveEarningsType(earningsType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _earningsTypeRepository = Nothing
            _sequenseBudgetDRepository = Nothing
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
