'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
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
Imports System.Data.SqlClient

#End Region

Public Class CostLogisticsProductionCenterRecordAdminService
    Implements ICostLogisticsProductionCenterRecordAdminService

#Region "Fields"

    Private _logisticsProductionCenterRecordRepository As ICostLogisticsProductionCenterRecordReporsitory

    Private _sequenceDRepository As ICostSequenceDetailRepository

#End Region

#Region "Builders"

    Public Sub New(ByVal logisticsProductionCenterRecordRepository As ICostLogisticsProductionCenterRecordReporsitory, ByVal sequenceDRepository As ICostSequenceDetailRepository)
        _logisticsProductionCenterRecordRepository = logisticsProductionCenterRecordRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

#End Region

#Region "Methods"

    Public Function DeleteCostLogisticsProductionCenterRecord(entity As CostLogisticsProductionCenterRecord, audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord) Implements ICostLogisticsProductionCenterRecordAdminService.DeleteCostLogisticsProductionCenterRecord
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        Dim unitOfWork As IUnitWork = Me._logisticsProductionCenterRecordRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While entity.CostLogisticsProductionCenterRecordDetail.Count > 0
                    Dim _detail As CostLogisticsProductionCenterRecordDetail = entity.CostLogisticsProductionCenterRecordDetail.Item(entity.CostLogisticsProductionCenterRecordDetail.Count() - 1)
                    _detail.MarkAsDeleted()
                End While

                entity.MarkAsDeleted()
                entity.ModificationUser = audit.CodeUser
                entity.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostLogisticsProductionCenterRecord)(entity, audit, status)

                Me._logisticsProductionCenterRecordRepository.SaveEntity(entity)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCostLogisticsProductionCenterRecordByCode(code As String, audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord) Implements ICostLogisticsProductionCenterRecordAdminService.GetCostLogisticsProductionCenterRecordByCode
        Try
            Dim result = _logisticsProductionCenterRecordRepository.GetCostLogisticsProductionCenterRecordByCode(code)
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostLogisticsProductionCenterRecord)(result, audit, status)
            auditProcess.Execute()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    Public Function GetCostLogisticsProductionCenterRecordById(id As Integer, audit As AuditMessage) As ActionResult(Of CostLogisticsProductionCenterRecord) Implements ICostLogisticsProductionCenterRecordAdminService.GetCostLogisticsProductionCenterRecordById
        Try
            Dim result = _logisticsProductionCenterRecordRepository.GetCostLogisticsProductionCenterRecordById(id)
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostLogisticsProductionCenterRecord)(result, audit, status)
            auditProcess.Execute()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    Public Function SaveCostLogisticsProductionCenterRecord(entity As CostLogisticsProductionCenterRecord, audit As AuditMessage, idSequense As Long) As ActionResult(Of CostLogisticsProductionCenterRecord) Implements ICostLogisticsProductionCenterRecordAdminService.SaveCostLogisticsProductionCenterRecord
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        Dim unitOfWork As IUnitWork = Me._logisticsProductionCenterRecordRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(entity.Code) Or entity.Code = "Nuevo" Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            entity.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), entity.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As CostLogisticsProductionCenterRecord = Nothing
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
                If entity.Status = 2 Then
                    entity.ConfirmDate = Date.Now
                    entity.ConfirmUser = audit.CodeUser
                ElseIf entity.Status = 3 Then
                    entity.AnnulmentDate = Date.Now
                    entity.AnnulmentUser = audit.CodeUser
                End If

                Me._logisticsProductionCenterRecordRepository.SaveEntity(entity)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostLogisticsProductionCenterRecord)(entity, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = entity, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostLogisticsProductionCenterRecord) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_CostReportConsolidatedCCLogisticA_Result) Implements ICostLogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogistic
        Try
            Return _logisticsProductionCenterRecordRepository.ListReportConsolidatedCCLogistic(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportConsolidatedCCLogisticA_Result)()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_CostReportConsolidatedCCLogisticB_Result) Implements ICostLogisticsProductionCenterRecordAdminService.ListReportConsolidatedCCLogisticB
        Try
            Return _logisticsProductionCenterRecordRepository.ListReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportConsolidatedCCLogisticB_Result)()
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure spReportCostMeasurementUnit
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues) As DataSet Implements ICostLogisticsProductionCenterRecordAdminService.GetListReportCostMeasurementUnit    
        If initialMonth = Nothing OrElse initialMonth = 0 Then
            Throw New ArgumentNullException("initialMonth")
        End If
        If initialYear = Nothing OrElse initialYear = 0 Then
            Throw New ArgumentNullException("initialYear")
        End If
        If endMonth = Nothing OrElse endMonth = 0 Then
            Throw New ArgumentNullException("endMonth")
        End If
        If endYear = Nothing OrElse endYear = 0 Then
            Throw New ArgumentNullException("endYear")
        End If
        If ProductionCenterId = Nothing OrElse ProductionCenterId = 0 Then
            Throw New ArgumentNullException("ProductionCenterId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            'si vienen los filtros de centro de costo vacios
            If initialMeasurementUnitCode = String.Empty And endMeasurementUnitCode = String.Empty Then
                query1 = "exec [InteropCost].[spReportCostMeasurementUnit] " & initialMonth & "," & initialYear & "," & endMonth & "," & endYear & ",NULL,NULL," & ProductionCenterId
                'si vienen los filtros de tercero y centro de costo vacios
            Else
                query1 = "exec [InteropCost].[spReportCostMeasurementUnit] " & initialMonth & "," & initialYear & "," & endMonth & "," & endYear & ",'" & initialMeasurementUnitCode & "','" & endMeasurementUnitCode & "'," & ProductionCenterId
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCostMeasurementUnit")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()

            End Try
        End Using
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
