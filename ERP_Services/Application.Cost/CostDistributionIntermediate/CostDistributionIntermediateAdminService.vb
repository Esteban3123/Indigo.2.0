'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-01-2015
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
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Transactions

Public Class CostDistributionIntermediateAdminService
    Implements ICostDistributionIntermediateAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribución intermedia
    ''' </summary>
    Private _distributionIntermediateRepository As ICostDistributionIntermediateRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository
    Private _costService As ICostServices

#End Region

#Region "Methods"

    Public Sub New(ByVal distributionIntermediateRepository As ICostDistributionIntermediateRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository,
                   interopCostService As ICostServices)
        If distributionIntermediateRepository Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediateRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionIntermediateRepository = distributionIntermediateRepository
        _sequenceDRepository = sequenceDRepository
        _costService = interopCostService
    End Sub
#End Region

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Public Function DeleteDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage) As ActionResult Implements ICostDistributionIntermediateAdminService.DeleteDistributionIntermediate
        If distributionIntermediate Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediate")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionIntermediateRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionIntermediate.CostDistributionIntermediateDetail.Count > 0
                    distributionIntermediate.CostDistributionIntermediateDetail.Item(distributionIntermediate.CostDistributionIntermediateDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionIntermediate.MarkAsDeleted()
                distributionIntermediate.ModificationUser = audit.CodeUser
                distributionIntermediate.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status)
                Me._distributionIntermediateRepository.SaveEntity(distributionIntermediate)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.GetDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionIntermediate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate Implements ICostDistributionIntermediateAdminService.GetDistributionIntermediateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionIntermediateRepository.GetDistributionIntermediateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.ListDistributionIntermediateByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim _listDistributionIntermediate As List(Of CostDistributionIntermediate) = Me._distributionIntermediateRepository.ListDistributionIntermediateByYearMonth(year, month)
            If _listDistributionIntermediate IsNot Nothing AndAlso _listDistributionIntermediate.Count > 0 Then
                'For Each item As DistributionIntermediate In _listDistributionIntermediate
                '    'item.FullNameFixedAsset = String.Concat(mainAccount.AACCODACT.Trim(), " - ", mainAccount.AFNPRODUC.APRNOMBRE.Trim())
                'Next
            End If
            Return _listDistributionIntermediate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionIntermediateAdminService.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionIntermediateRepository.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Public Function SaveDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.SaveDistributionIntermediate
        If distributionIntermediate Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediate")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionIntermediateRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            Dim resultValidate As ActionResult = _costService.ValidateDistributionIntermediateSave(distributionIntermediate)
            If Not resultValidate.StateResult Then
                unitOfWork.RollbackChangesUnitOfWork()
                Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionIntermediate.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionIntermediate.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionIntermediate.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxdistributionIntermediate As CostDistributionIntermediate = Nothing
                Dim status As Integer
                If distributionIntermediate.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionIntermediate.Code)
                    End If
                    distributionIntermediate.CreationDate = Date.Now
                    distributionIntermediate.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionIntermediate.ModificationDate = Date.Now
                    distributionIntermediate.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxdistributionIntermediate = distributionIntermediate.OriginalValue
                End If
                Me._distributionIntermediateRepository.SaveEntity(distributionIntermediate)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status, auxdistributionIntermediate)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionIntermediate, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state distribution intermediate.
    ''' </summary>
    Public Function UpdateStateDistributionIntermediate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.UpdateStateDistributionIntermediate
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
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                distributionIntermediate.Status = state
            End If
            Return Me.SaveDistributionIntermediate(distributionIntermediate, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _costService.Dispose()
            End If
            _distributionIntermediateRepository = Nothing
            _costService = Nothing
            _sequenceDRepository = Nothing
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