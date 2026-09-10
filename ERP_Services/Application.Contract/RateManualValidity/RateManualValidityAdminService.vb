#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class RateManualValidityAdminService
    Implements IRateManualValidityAdminService

#Region "Builder"

    Private _rateManualValidityRepository As IRateManualValidityRepository
    Private _secuenseContractDetailRepository As ISequenseContractDRepository

    Public Sub New(ByVal rateManualValidityRepository As IRateManualValidityRepository,
                   ByVal secuenseContractDetailRepository As ISequenseContractDRepository)
        _rateManualValidityRepository = rateManualValidityRepository
        _secuenseContractDetailRepository = secuenseContractDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetRateManualValidityById(id As Integer, audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IRateManualValidityAdminService.GetRateManualValidityById
        Try
            Dim rateManualValidity As RateManualValidity = Me._rateManualValidityRepository.GetRateManualValidityById(id)
            If rateManualValidity IsNot Nothing AndAlso rateManualValidity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RateManualValidity)(rateManualValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RateManualValidity) With {.StateResult = True, .ObjectEmbbeded = rateManualValidity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetRateManualValidity(code As String, audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IRateManualValidityAdminService.GetRateManualValidity
        Try
            Dim rateManualValidity As RateManualValidity = Me._rateManualValidityRepository.GetRateManualValidity(code.Trim())
            If rateManualValidity IsNot Nothing AndAlso rateManualValidity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RateManualValidity)(rateManualValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RateManualValidity) With {.StateResult = True, .ObjectEmbbeded = rateManualValidity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveRateManualValidity(rateManualValidity As RateManualValidity, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RateManualValidity) Implements IRateManualValidityAdminService.SaveRateManualValidity
        Dim unitOfWork As IUnitWork = Me._rateManualValidityRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseContractDetailRepository.UnitWork
        Try
            If rateManualValidity.Code Is Nothing OrElse rateManualValidity.Code.Trim().Equals(String.Empty) Then
                Dim seq = Me._secuenseContractDetailRepository.GetSequenseDById(idSequense)
                If seq Is Nothing OrElse seq.Id = 0 Then
                    Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {"No tiene secuencia numérica parametrizada."}.ToList()}
                End If

                If seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        rateManualValidity.Code = res
                        seq.Next += 1
                        Me._secuenseContractDetailRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {"La secuencia numérica ya excedió el limite permitido."}.ToList()}
                    End If
                End If
            End If

            Dim auxRateManual As RateManualValidity = Nothing
            Dim status As Integer

            If rateManualValidity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                rateManualValidity.CreationUser = audit.CodeUser
                rateManualValidity.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxRateManual = rateManualValidity.OriginalValue
                rateManualValidity.ModificationUser = audit.CodeUser
                rateManualValidity.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._rateManualValidityRepository.SaveEntity(rateManualValidity)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()

            Dim auditProcess = New IndigoAuditSimpleEntity(Of RateManualValidity)(rateManualValidity, audit, status, auxRateManual)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            rateManualValidity.MarkAsUnchanged()

            Return New ActionResult(Of RateManualValidity) With {.StateResult = True, .ObjectEmbbeded = rateManualValidity}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {"-111"}.ToList(), .Message = "No se puede insertar porque existe código duplicado: " + rateManualValidity.Code}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManualValidity) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    Public Function ChangeStateRateManualValidity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IRateManualValidityAdminService.ChangeStateRateManualValidity
        Dim rateManualValidity As RateManualValidity = _rateManualValidityRepository.GetRateManualValidity(code)
        rateManualValidity.Status = state
        Return SaveRateManualValidity(rateManualValidity, audit)
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _rateManualValidityRepository = Nothing
            _secuenseContractDetailRepository = Nothing
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
