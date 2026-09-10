'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/09/2016
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
Imports System.Text

Public Class CostDistributionFixedAssetAdminService
    Implements ICostDistributionFixedAssetAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribución de activos fijos
    ''' </summary>
    Private _costDistributionFixedAssetRepository As ICostDistributionFixedAssetRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository

    Private _costService As ICostServices

#End Region

#Region "Builder"

    Public Sub New(costDistributionFixedAssetRepository As ICostDistributionFixedAssetRepository, sequenceDRepository As ICostSequenceDetailRepository,
                   costService As ICostServices)
        If costDistributionFixedAssetRepository Is Nothing Then
            Throw New ArgumentNullException("costDistributionFixedAssetRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _costDistributionFixedAssetRepository = costDistributionFixedAssetRepository
        _sequenceDRepository = sequenceDRepository
        _costService = costService
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostDistributionFixedAssetById(id As Integer) As ActionResult(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetAdminService.GetCostDistributionFixedAssetById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim CostDistributionFixedAsset As CostDistributionFixedAsset = Me._costDistributionFixedAssetRepository.GetCostDistributionFixedAssetById(id)
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = CostDistributionFixedAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCostDistributionFixedAsset(code As String) As ActionResult(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetAdminService.GetCostDistributionFixedAsset
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim CostDistributionFixedAsset As CostDistributionFixedAsset = Me._costDistributionFixedAssetRepository.GetCostDistributionFixedAsset(code.Trim())
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = CostDistributionFixedAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(year As Integer, month As Integer, Optional physicalId As Integer = Nothing) As ActionResult(Of List(Of CostDistributionFixedAsset)) Implements ICostDistributionFixedAssetAdminService.GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth
        Try
            Dim listDistributionManpower As New List(Of CostDistributionFixedAsset)

            Dim listDistributions As List(Of SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId_Result) = Me._costDistributionFixedAssetRepository.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId(year, month, physicalId)
            For Each distribution In listDistributions
                Dim costDistributionFixedAsset As CostDistributionFixedAsset = listDistributionManpower.FirstOrDefault(Function(d) d.FixedAssetPhysicalAssetId = distribution.Id)
                If costDistributionFixedAsset Is Nothing Then
                    costDistributionFixedAsset = New CostDistributionFixedAsset With {
                        .Year = year,
                        .Month = month,
                        .FixedAssetPhysicalAssetId = distribution.Id,
                        .Description = String.Format("{0} - {1}", distribution.Plate, distribution.FixedAssetItemCodeDescription),
                        .Status = 1
                    }
                    listDistributionManpower.Add(costDistributionFixedAsset)
                End If

                costDistributionFixedAsset.HoursWorked += distribution.HoursQuantity
                costDistributionFixedAsset.DepreciationValue += distribution.DepreciationValue

                costDistributionFixedAsset.CostDistributionFixedAssetDetail.Add(New CostDistributionFixedAssetDetail With {
                        .ProductionCenterId = If(distribution.ProductionCenterId Is Nothing, 0, distribution.ProductionCenterId),
                        .ProductionCenterCodeName = distribution.ProductionCenterCodeName,
                        .HoursQuantity = distribution.HoursQuantity,
                        .DepreciationValue = distribution.DepreciationValue
                    }
                )
            Next

            Return New ActionResult(Of List(Of CostDistributionFixedAsset)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listDistributionManpower}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CostDistributionFixedAsset)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As ActionResult(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetAdminService.GetCostDistributionFixedAssetByPhysicalIdAndYearMonth
        If physicalId = 0 Then
            Throw New ArgumentNullException("physicalId")
        End If
        Try
            Dim CostDistributionFixedAsset As CostDistributionFixedAsset = Me._costDistributionFixedAssetRepository.GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId, year, month)
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = CostDistributionFixedAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDistributionFixedDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionFixedAssetAdminService.ListPeriodWithDistributionFixedDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._costDistributionFixedAssetRepository.ListPeriodWithDistributionFixedDataByMaximumPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetAdminService.ListDistributionFixedAssetByYearMonth
        Try
            Return Me._costDistributionFixedAssetRepository.ListDistributionFixedAssetByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result)) Implements ICostDistributionFixedAssetAdminService.SP_ExportExcelCostDistributionFixedAsset
        Try
            'Se convierte el listado de ids a xml para enviarlo al sp
            Dim resultStore = _costDistributionFixedAssetRepository.SP_ExportExcelCostDistributionFixedAsset(Year, Month)
            Return New ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = resultStore}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements ICostDistributionFixedAssetAdminService.ImportCostDistributionFixedAsset
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim ImportXml As String = ConvertToXmlImportIds(ImportIds)
                Dim resultStore = _costDistributionFixedAssetRepository.SP_ImportCostDistributionFixedAsset(Year, Month, OperatingUnitId, ImportXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function SaveCostDistributionFixedAsset(costDistributionFixedAsset As CostDistributionFixedAsset, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionFixedAsset) Implements ICostDistributionFixedAssetAdminService.SaveCostDistributionFixedAsset
        If costDistributionFixedAsset Is Nothing Then
            Throw New ArgumentNullException("CostDistributionFixedAsset")
        End If

        Dim unitOfWork As IUnitWork = Me._costDistributionFixedAssetRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlCostDistributionFixedAsset(costDistributionFixedAsset)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(costDistributionFixedAsset.Id, costDistributionFixedAsset.Status)

                Dim resultStore = Me._costDistributionFixedAssetRepository.SP_SaveCostDistributionFixedAsset(EntityXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CostDistributionFixedAsset) With {.StatusCode = eStatusResult.WARNING, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                costDistributionFixedAsset.Id = resultStore.Id
                costDistributionFixedAsset.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of CostDistributionFixedAsset)(costDistributionFixedAsset, audit, auditStatus, costDistributionFixedAsset.OriginalValue)
                auditProcess.Execute()

                costDistributionFixedAsset.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = costDistributionFixedAsset, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostDistributionFixedAsset) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult Implements ICostDistributionFixedAssetAdminService.SP_ConfirmMasiveCostDistributionFixedAsset
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim resultStore = _costDistributionFixedAssetRepository.SP_ConfirmMasiveCostDistributionFixedAsset(Year, Month, OperatingUnitId, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                End If

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function DeleteCostDistributionFixedAsset(CostDistributionFixedAsset As CostDistributionFixedAsset, audit As AuditMessage) As ActionResult Implements ICostDistributionFixedAssetAdminService.DeleteCostDistributionFixedAsset
        If CostDistributionFixedAsset Is Nothing Then
            Throw New ArgumentNullException("CostDistributionFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._costDistributionFixedAssetRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While CostDistributionFixedAsset.CostDistributionFixedAssetDetail.Count > 0
                    CostDistributionFixedAsset.CostDistributionFixedAssetDetail.Item(CostDistributionFixedAsset.CostDistributionFixedAssetDetail.Count() - 1).MarkAsDeleted()
                End While
                CostDistributionFixedAsset.MarkAsDeleted()
                CostDistributionFixedAsset.ModificationUser = audit.CodeUser
                CostDistributionFixedAsset.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionFixedAsset)(CostDistributionFixedAsset, audit, status)
                Me._costDistributionFixedAssetRepository.SaveEntity(CostDistributionFixedAsset)
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

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlImportIds(ImportIds As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        For Each detail In ImportIds
            builder.Append("<Data>")
            builder.Append("<Id>" & detail & "</Id>")
            builder.Append("</Data>")
        Next

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlCostDistributionFixedAsset(costDistributionFixedAsset As CostDistributionFixedAsset) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CostDistributionFixedAsset>")

        builder.Append("<Id>" & costDistributionFixedAsset.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & costDistributionFixedAsset.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & costDistributionFixedAsset.Code & "</Code>")
        builder.Append("<Year>" & costDistributionFixedAsset.Year & "</Year>")
        builder.Append("<Month>" & costDistributionFixedAsset.Month & "</Month>")
        builder.Append("<FixedAssetPhysicalAssetId>" & costDistributionFixedAsset.FixedAssetPhysicalAssetId & "</FixedAssetPhysicalAssetId>")
        builder.Append("<FixedAssetCode>" & costDistributionFixedAsset.FixedAssetCode & "</FixedAssetCode>")
        builder.Append("<Description>" & costDistributionFixedAsset.Description & "</Description>")
        builder.Append("<HoursWorked>" & costDistributionFixedAsset.HoursWorked & "</HoursWorked>")
        builder.Append("<DepreciationValue>" & costDistributionFixedAsset.DepreciationValue.ToString().Replace(",", ".") & "</DepreciationValue>")
        builder.Append("<Status>" & costDistributionFixedAsset.Status & "</Status>")

        For Each detail In costDistributionFixedAsset.CostDistributionFixedAssetDetail
            builder.Append("<CostDistributionFixedAssetDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<DistributionFixedAssetId>" & detail.DistributionFixedAssetId & "</DistributionFixedAssetId>")
            builder.Append("<ProductionCenterId>" & detail.ProductionCenterId & "</ProductionCenterId>")
            builder.Append("<HoursQuantity>" & detail.HoursQuantity & "</HoursQuantity>")
            builder.Append("<Proportion>" & detail.Proportion.ToString().Replace(",", ".") & "</Proportion>")
            builder.Append("<DepreciationValue>" & detail.DepreciationValue.ToString().Replace(",", ".") & "</DepreciationValue>")

            builder.Append("</CostDistributionFixedAssetDetail>")
        Next

        builder.Append("</CostDistributionFixedAsset>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _costService.Dispose()
            End If
            _costDistributionFixedAssetRepository = Nothing
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
