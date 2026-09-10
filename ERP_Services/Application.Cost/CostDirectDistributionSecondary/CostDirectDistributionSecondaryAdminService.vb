'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
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
Imports System.Transactions
Imports System.Text

Public Class CostDirectDistributionSecondaryAdminService
    Implements ICostDirectDistributionSecondaryAdminService

#Region "Fields"

    Private _sequenceDRepository As ICostSequenceDetailRepository
    Private _distributionSecondaryRepository As ICostDistributionSecondaryRepository
    Private _directDistributionSecondaryRepository As ICostDirectDistributionSecondaryRepository

#End Region

#Region "Methods"

    Public Sub New(sequenceDRepository As ICostSequenceDetailRepository, distributionSecondaryRepository As ICostDistributionSecondaryRepository, directDistributionSecondaryRepository As ICostDirectDistributionSecondaryRepository)
        _sequenceDRepository = sequenceDRepository
        _distributionSecondaryRepository = distributionSecondaryRepository
        _directDistributionSecondaryRepository = directDistributionSecondaryRepository
    End Sub

    Public Function CalculateDistributionSecondary(CostDistributionSecondaryId As Integer, ByVal Value As Decimal, ByVal Year As Integer, ByVal Month As Integer) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail)) Implements ICostDirectDistributionSecondaryAdminService.CalculateDistributionSecondary
        Try
            Dim listCostDirectDistributionSecondaryDetail As New List(Of CostDirectDistributionSecondaryDetail)()
            Dim listPCenter = Me._directDistributionSecondaryRepository.CalculateDistributionSecondary(CostDistributionSecondaryId, Year, Month)

            If listPCenter IsNot Nothing AndAlso listPCenter.Count > 0 Then
                For Each pCenter In listPCenter
                    Dim costDirectDistributionSecondaryDetail As New CostDirectDistributionSecondaryDetail()
                    costDirectDistributionSecondaryDetail.ProductionCenterId = pCenter.ProductionCenterId
                    costDirectDistributionSecondaryDetail.CodeNameProductionCenter = pCenter.ProductionCenterCodeName
                    costDirectDistributionSecondaryDetail.Percentage = pCenter.Percentage
                    costDirectDistributionSecondaryDetail.Value = pCenter.Value
                    listCostDirectDistributionSecondaryDetail.Add(costDirectDistributionSecondaryDetail)
                Next
            End If

            Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail)) With {.StateResult = True, .ObjectEmbbeded = listCostDirectDistributionSecondaryDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail)) With {.StateResult = False, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    Public Function GetCostDirectDistributionSecondary(code As String, audit As AuditMessage) As CostDirectDistributionSecondary Implements ICostDirectDistributionSecondaryAdminService.GetCostDirectDistributionSecondary
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DirectDistributionSecondary As CostDirectDistributionSecondary = Me._directDistributionSecondaryRepository.GetCostDirectDistributionSecondary(code.Trim())
            If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDirectDistributionSecondary)(DirectDistributionSecondary, audit, status)
                auditProcess.Execute()
            End If
            Return DirectDistributionSecondary
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CostDirectDistributionSecondary
        End Try
    End Function

    Public Function GetCostDirectDistributionSecondaryById(id As Integer) As CostDirectDistributionSecondary Implements ICostDirectDistributionSecondaryAdminService.GetCostDirectDistributionSecondaryById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._directDistributionSecondaryRepository.GetCostDirectDistributionSecondaryById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCostDirectDistributionSecondary(distributionSecondary As CostDirectDistributionSecondary, listDistributionSecondaryDetailForDelete As List(Of Integer), ListCostLogisticsProductionCenterDetail As List(Of Integer), audit As AuditMessage) As ActionResult(Of CostDirectDistributionSecondary) Implements ICostDirectDistributionSecondaryAdminService.SaveCostDirectDistributionSecondary
        If distributionSecondary Is Nothing Then
            Throw New ArgumentNullException("DirectDistributionSecondary")
        End If

        Dim unitOfWork As IUnitWork = Me._directDistributionSecondaryRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertDistributionSecondaryToXml(distributionSecondary)
                Dim ListDeleteXml As String = ConvertListDistributionSecondaryForDeleteToXml(listDistributionSecondaryDetailForDelete)
                Dim ListLogisticsProductionCenterDetailXml As String = ConvertListCostLogisticsProductionCenterDetailToXml(ListCostLogisticsProductionCenterDetail)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(distributionSecondary.Id, distributionSecondary.Status)

                Dim resultStore = Me._directDistributionSecondaryRepository.SP_SaveDistributionSecondary(EntityXml, ListDeleteXml, ListLogisticsProductionCenterDetailXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CostDirectDistributionSecondary) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                distributionSecondary.Id = resultStore.Id
                distributionSecondary.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of CostDirectDistributionSecondary)(distributionSecondary, audit, auditStatus, distributionSecondary.OriginalValue)
                auditProcess.Execute()

                distributionSecondary.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CostDirectDistributionSecondary) With {.StateResult = True, .ObjectEmbbeded = distributionSecondary, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CostDirectDistributionSecondary) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostDirectDistributionSecondary) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult Implements ICostDirectDistributionSecondaryAdminService.GenerateDistributionSecondary
        Dim unitOfWork As IUnitWork = Me._directDistributionSecondaryRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = Me._directDistributionSecondaryRepository.SP_GenerateDistributionSecondary(Year, Month, OperatingUnitId, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = resultStore.MessageResult}
                End If
                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Methods Private"

    Private Function ConvertDistributionSecondaryToXml(distributionSecondary As CostDirectDistributionSecondary) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CostDirectDistributionSecondary>")

        builder.Append("<Id>" & distributionSecondary.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & distributionSecondary.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & distributionSecondary.Code & "</Code>")
        builder.Append("<DistributionSecondaryId>" & distributionSecondary.DistributionSecondaryId & "</DistributionSecondaryId>")
        builder.Append("<Year>" & distributionSecondary.Year & "</Year>")
        builder.Append("<Month>" & distributionSecondary.Month & "</Month>")
        builder.Append("<CostEstimationId>" & distributionSecondary.CostEstimationId & "</CostEstimationId>")
        builder.Append("<Value>" & distributionSecondary.Value.ToString().Replace(",", ".") & "</Value>")
        builder.Append("<Description>" & distributionSecondary.Description & "</Description>")
        builder.Append("<Status>" & distributionSecondary.Status & "</Status>")

        For Each detail In distributionSecondary.CostDirectDistributionSecondaryDetail
            builder.Append("<CostDirectDistributionSecondaryDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<DirectDistributionSecondaryId>" & detail.DirectDistributionSecondaryId & "</DirectDistributionSecondaryId>")
            builder.Append("<ProductionCenterId>" & detail.ProductionCenterId & "</ProductionCenterId>")
            If detail.MeasurementUnitId > 0 Then
                builder.Append("<MeasurementUnitId>" & detail.MeasurementUnitId & "</MeasurementUnitId>")
            End If
            builder.Append("<Percentage>" & detail.Percentage.ToString().Replace(",", ".") & "</Percentage>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</CostDirectDistributionSecondaryDetail>")
        Next

        builder.Append("</CostDirectDistributionSecondary>")

        Return builder.ToString()
    End Function

    Private Function ConvertListDistributionSecondaryForDeleteToXml(listDistributionSecondaryDetailForDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listDistributionSecondaryDetailForDelete IsNot Nothing AndAlso listDistributionSecondaryDetailForDelete.Count > 0 Then
            For Each detail In listDistributionSecondaryDetailForDelete
                builder.Append("<DistributionSecondaryDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</DistributionSecondaryDetail>")
            Next
        End If

        Return builder.ToString()
    End Function

    Private Function ConvertListCostLogisticsProductionCenterDetailToXml(ListIds As List(Of Integer))
        Dim builder As StringBuilder = New StringBuilder()

        If ListIds IsNot Nothing AndAlso ListIds.Count > 0 Then
            For Each item In ListIds
                builder.Append("<Data>")
                builder.Append("<Id>" & item & "</Id>")
                builder.Append("</Data>")
            Next
        End If

        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _directDistributionSecondaryRepository = Nothing
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
