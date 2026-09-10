'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Application.Accounting

Public Class RetentionConceptAdminService
    Implements IRetentionConceptAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _retentionRepository As IRetentionConceptRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

    Private Const FORM_NAME As String = "Conceptos de Retención"

#End Region

    Public Sub New(ByVal retentionRepository As IRetentionConceptRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        If retentionRepository Is Nothing Then
            Throw New ArgumentNullException("retentionRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        Me._retentionRepository = retentionRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de retencion
    ''' </summary>
    ''' <param name="retentionConcept"></param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>
    ''' Resultado de la acción
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function DeleteRetentionConcept(retentionConcept As RetentionConcepts, audit As AuditMessage) As ActionResult Implements IRetentionConceptAdminService.DeleteRetentionConcept
        If retentionConcept Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._retentionRepository.UnitWork
        Try
            retentionConcept.ModificationDate = DateTime.Now
            retentionConcept.ModificationUser = audit.CodeUser
            Dim auditProcess As New IndigoAuditSimpleEntity(Of RetentionConcepts)(retentionConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While retentionConcept.RetentionConceptRanges.Count > 0
                retentionConcept.RetentionConceptRanges.ElementAt(0).MarkAsDeleted()
            End While
            retentionConcept.MarkAsDeleted()
            Me._retentionRepository.SaveEntity(retentionConcept)
            unitOfWork.Commit()
            auditProcess.Execute()

            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de retencion
    ''' </summary>
    ''' <param name="code">Código de la clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>
    ''' Clase contable
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetRetentionConcept(code As String, audit As AuditMessage) As RetentionConcepts Implements IRetentionConceptAdminService.GetRetentionConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim retentionConcept As RetentionConcepts = Me._retentionRepository.GetRetentionConcept(code.Trim())
            If retentionConcept IsNot Nothing AndAlso retentionConcept.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RetentionConcepts)(retentionConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return retentionConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un concepto de retencion
    ''' </summary>
    ''' <param name="retentionConcept">The retention concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SaveRetentionConcept(retentionConcept As RetentionConcepts, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RetentionConcepts) Implements IRetentionConceptAdminService.SaveRetentionConcept
        If retentionConcept Is Nothing Then
            Throw New ArgumentNullException("retentionConcept")
        End If
        Dim retentionUnitOfWork As IUnitWork = Me._retentionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If retentionConcept.Code Is Nothing OrElse retentionConcept.Code.Trim().Equals(String.Empty) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            retentionConcept.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RetentionConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), retentionConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RetentionConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RetentionConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RetentionConcepts)
                Dim status As Integer

                If retentionConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    retentionConcept.CreationDate = DateTime.Now
                    retentionConcept.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    retentionConcept.ModificationDate = DateTime.Now
                    retentionConcept.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxObjEntity = retentionConcept.OriginalValue
                End If

                Me._retentionRepository.SaveEntity(retentionConcept)
                retentionUnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RetentionConcepts)(retentionConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of RetentionConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = retentionConcept, .Message = MessageResult}

            End Using
        Catch ex As OptimisticConcurrencyException
            retentionUnitOfWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            Return New ActionResult(Of RetentionConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            retentionUnitOfWork.RollbackChanges()
            sequenseUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RetentionConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionById(id As Integer, ByVal audit As AuditMessage) As RetentionConcepts Implements IRetentionConceptAdminService.GetRetentionById
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Return Me._retentionRepository.GetRetentionById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity Implements IRetentionConceptAdminService.GetRetentionConceptByCity
        Try
            Return Me._retentionRepository.GetRetentionConceptByCity(id, addressId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RetentionConceptByCity
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateRetentionConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RetentionConcepts) Implements IRetentionConceptAdminService.ChangeStateRetentionConcept
        Dim retention = GetRetentionConcept(code, audit)
        retention.Status = state
        retention.MarkAsModified()
        Return SaveRetentionConcept(retention, audit)
    End Function

    ''' <summary>
    ''' Obtiene un listado de rangos de retencion para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As ActionResult(Of List(Of RetentionConceptRanges)) Implements IRetentionConceptAdminService.GetListRetentionRangeByRetentionConceptId
        If RetentionConceptId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Try
            Dim ListRanges As List(Of RetentionConceptRanges) = Me._retentionRepository.GetListRetentionRangeByRetentionConceptId(RetentionConceptId)
            Return New ActionResult(Of List(Of RetentionConceptRanges)) With {.StateResult = True, .ObjectEmbbeded = ListRanges}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of RetentionConceptRanges)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts Implements IRetentionConceptAdminService.GetRetentionConceptByIdBrachOfficeId
        If brachOfficeId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Try
            Return _retentionRepository.GetRetentionConceptByIdBrachOfficeId(brachOfficeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts Implements IRetentionConceptAdminService.GetIVARetentionConceptByThirdPartyId
        If thirdPartyId = 0 Then
            Throw New ArgumentNullException("RetentionConceptId")
        End If
        Try
            Return _retentionRepository.GetIVARetentionConceptByThirdPartyId(thirdPartyId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._retentionRepository = Nothing
            Me._secuenseDRepository = Nothing
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
