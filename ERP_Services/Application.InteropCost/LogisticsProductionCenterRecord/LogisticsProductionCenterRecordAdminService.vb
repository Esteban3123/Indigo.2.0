'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Juan F. Tamayo Puertas
' Created          : 2016-11-05
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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports System.Transactions

#End Region

Public Class LogisticsProductionCenterRecordAdminService
    Implements ILogisticsProductionCenterRecordAdminService

#Region "Fields"

    Private _logisticsProductionCenterRecordRepository As ILogisticsProductionCenterRecordReporsitory

    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

#End Region

#Region "Builders"

    Public Sub New(ByVal logisticsProductionCenterRecordRepository As ILogisticsProductionCenterRecordReporsitory, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository)
        _logisticsProductionCenterRecordRepository = logisticsProductionCenterRecordRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetLogisticsProductionCenterRecordByCode(code As String, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements ILogisticsProductionCenterRecordAdminService.GetLogisticsProductionCenterRecordByCode
        Try
            Dim result = _logisticsProductionCenterRecordRepository.GetLogisticsProductionCenterRecordByCode(code)
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
            Dim auditProcess As New IndigoAuditSimpleEntity(Of LogisticsProductionCenterRecord)(result, audit, status)
            auditProcess.Execute()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    Public Function GetLogisticsProductionCenterRecordById(id As Integer, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements ILogisticsProductionCenterRecordAdminService.GetLogisticsProductionCenterRecordById
        Try
            Dim result = _logisticsProductionCenterRecordRepository.GetLogisticsProductionCenterRecordById(id)
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
            Dim auditProcess As New IndigoAuditSimpleEntity(Of LogisticsProductionCenterRecord)(result, audit, status)
            auditProcess.Execute()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    Public Function DeleteLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage) As ActionResult(Of LogisticsProductionCenterRecord) Implements ILogisticsProductionCenterRecordAdminService.DeleteLogisticsProductionCenterRecord
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        Dim unitOfWork As IUnitWork = Me._logisticsProductionCenterRecordRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While entity.LogisticsProductionCenterRecordDetail.Count > 0
                    Dim _detail As LogisticsProductionCenterRecordDetail = entity.LogisticsProductionCenterRecordDetail.Item(entity.LogisticsProductionCenterRecordDetail.Count() - 1)
                    _detail.MarkAsDeleted()
                End While

                entity.MarkAsDeleted()
                entity.ModificationUser = audit.CodeUser
                entity.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of LogisticsProductionCenterRecord)(entity, audit, status)

                Me._logisticsProductionCenterRecordRepository.SaveEntity(entity)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveLogisticsProductionCenterRecord(entity As LogisticsProductionCenterRecord, audit As AuditMessage, idSequense As Int64) As ActionResult(Of LogisticsProductionCenterRecord) Implements ILogisticsProductionCenterRecordAdminService.SaveLogisticsProductionCenterRecord
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        Dim unitOfWork As IUnitWork = Me._logisticsProductionCenterRecordRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(entity.Code) Or entity.Code = "Nuevo" Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            entity.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), entity.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As LogisticsProductionCenterRecord = Nothing
                Dim status As Integer
                If entity.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), entity.Code)
                    End If
                    entity.CreationDate = Date.Now
                    entity.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    entity.ModificationDate = Date.Now
                    entity.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = entity.OriginalValue
                End If

                Me._logisticsProductionCenterRecordRepository.SaveEntity(entity)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of LogisticsProductionCenterRecord)(entity, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = entity, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result) Implements ILogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogistic
        Try
            Return _logisticsProductionCenterRecordRepository.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportConsolidatedCCLogisticA_Result)()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result) Implements ILogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogisticB
        Try
            Return _logisticsProductionCenterRecordRepository.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportConsolidatedCCLogisticB_Result)()
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
            _logisticsProductionCenterRecordRepository = Nothing
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
