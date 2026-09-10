'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports Domain.Entities.Service
Imports System.Transactions

Public Class DistributionFixedAssetAdminService
    Implements IDistributionFixedAssetAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribución de activos fijos
    ''' </summary>
    Private _distributionFixedAssetRepository As IDistributionFixedAssetRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

    ''' <summary>
    ''' repositorio de activos fijos
    ''' </summary>
    Private _fixedAssetRepository As IFixedAssetRepository
    Private _iAFNDEPRECIRepository As IAFNDEPRECIRepository
    Private _interopCostService As IInteropCostServices
#End Region

#Region "Methods"

    Public Sub New(ByVal distributionFixedAssetRepository As IDistributionFixedAssetRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository,
                   ByVal fixedAssetRepository As IFixedAssetRepository, iAFNDEPRECIRepository As IAFNDEPRECIRepository, interopCostService As IInteropCostServices)
        If distributionFixedAssetRepository Is Nothing Then
            Throw New ArgumentNullException("distributionFixedAssetRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionFixedAssetRepository = distributionFixedAssetRepository
        _sequenceDRepository = sequenceDRepository
        _fixedAssetRepository = fixedAssetRepository
        _iAFNDEPRECIRepository = iAFNDEPRECIRepository
        _interopCostService = interopCostService
    End Sub

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Public Function DeleteDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, audit As AuditMessage) As ActionResult Implements IDistributionFixedAssetAdminService.DeleteDistributionFixedAsset
        If distributionFixedAsset Is Nothing Then
            Throw New ArgumentNullException("distributionFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionFixedAssetRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionFixedAsset.DistributionFixedAssetDetail.Count > 0
                    distributionFixedAsset.DistributionFixedAssetDetail.Item(distributionFixedAsset.DistributionFixedAssetDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionFixedAsset.MarkAsDeleted()
                distributionFixedAsset.ModificationUser = audit.CodeUser
                distributionFixedAsset.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionFixedAsset)(distributionFixedAsset, audit, status)
                Me._distributionFixedAssetRepository.SaveEntity(distributionFixedAsset)
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
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Public Function GetDistributionFixedAsset(code As String, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset) Implements IDistributionFixedAssetAdminService.GetDistributionFixedAsset
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionFixedAsset As DistributionFixedAsset = Me._distributionFixedAssetRepository.GetDistributionFixedAsset(code.Trim())
            If distributionFixedAsset IsNot Nothing AndAlso distributionFixedAsset.Id > 0 Then
                Dim mainAccount As AFNACTIVO = _fixedAssetRepository.GetFixedAssetById(distributionFixedAsset.FixedAssetId)
                distributionFixedAsset.FullNameFixedAsset = String.Concat(mainAccount.AACCODACT.Trim(), " - ", mainAccount.AFNPRODUC1.APRNOMBRE.Trim())

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionFixedAsset)(distributionFixedAsset, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionFixedAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Public Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset Implements IDistributionFixedAssetAdminService.GetDistributionFixedAssetById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionFixedAssetRepository.GetDistributionFixedAssetById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of DistributionFixedAsset) Implements IDistributionFixedAssetAdminService.ListDistributionFixedAssetByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim _listDistributionFixedAsset As List(Of DistributionFixedAsset) = Me._distributionFixedAssetRepository.ListDistributionFixedAssetByYearMonth(year, month)
            If _listDistributionFixedAsset IsNot Nothing AndAlso _listDistributionFixedAsset.Count > 0 Then
                For Each item As DistributionFixedAsset In _listDistributionFixedAsset
                    Dim mainAccount As AFNACTIVO = _fixedAssetRepository.GetFixedAssetById(item.FixedAssetId)
                    item.FullNameFixedAsset = String.Concat(mainAccount.AACCODACT.Trim(), " - ", mainAccount.AFNPRODUC1.APRNOMBRE.Trim())
                Next
            End If
            Return _listDistributionFixedAsset
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    Public Function SaveDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DistributionFixedAsset) Implements IDistributionFixedAssetAdminService.SaveDistributionFixedAsset
        If distributionFixedAsset Is Nothing Then
            Throw New ArgumentNullException("distributionFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionFixedAssetRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            Dim resultValidate As ActionResult = _interopCostService.ValidateDistributionFixedAssetSave(distributionFixedAsset)
            If Not resultValidate.StateResult Then
                unitOfWork.RollbackChangesUnitOfWork()
                Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionFixedAsset.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionFixedAsset.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionFixedAsset.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxdistributionFixedAsset As DistributionFixedAsset = Nothing
                Dim status As Integer
                If distributionFixedAsset.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionFixedAsset.Code)
                    End If
                    distributionFixedAsset.CreationDate = Date.Now
                    distributionFixedAsset.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionFixedAsset.ModificationDate = Date.Now
                    distributionFixedAsset.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxdistributionFixedAsset = distributionFixedAsset.OriginalValue
                End If
                Me._distributionFixedAssetRepository.SaveEntity(distributionFixedAsset)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionFixedAsset)(distributionFixedAsset, audit, status, auxdistributionFixedAsset)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionFixedAsset, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state distribution fixed asset.
    ''' </summary>
    Public Function UpdateStateDistributionFixedAsset(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset) Implements IDistributionFixedAssetAdminService.UpdateStateDistributionFixedAsset
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
            Dim distributionDirectCost As DistributionFixedAsset = Me._distributionFixedAssetRepository.GetDistributionFixedAsset(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                distributionDirectCost.Status = state
            End If
            Return Me.SaveDistributionFixedAsset(distributionDirectCost, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionFixedAsset) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IDistributionFixedAssetAdminService.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionFixedAssetRepository.ListPeriodWithDataByMaximumPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionFixedAsset) Implements IDistributionFixedAssetAdminService.GetDistributionFixedAssetByActivoAndYearMonth
        If activoOid = 0 Then
            Throw New ArgumentNullException("activoOid")
        End If
        Try
            Dim distributionFixedAsset As DistributionFixedAsset = Me._distributionFixedAssetRepository.GetDistributionFixedAssetByActivoAndYearMonth(activoOid, year, month)

            If distributionFixedAsset IsNot Nothing AndAlso distributionFixedAsset.Id > 0 Then
                Dim mainAccount As AFNACTIVO = _fixedAssetRepository.GetFixedAssetById(distributionFixedAsset.FixedAssetId)
                distributionFixedAsset.FullNameFixedAsset = String.Concat(mainAccount.AACCODACT.Trim(), " - ", mainAccount.AFNPRODUC1.APRNOMBRE.Trim())
            End If

            Return New ActionResult(Of DistributionFixedAsset) With {.StateResult = True, .ObjectEmbbeded = distributionFixedAsset}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionFixedAsset) With {.StateResult = False}
        End Try
    End Function

    Public Function GetAFNDEPRECIByOidYearMonth(oid As Integer, year As Integer, month As Integer) As AFNDEPRECI Implements IDistributionFixedAssetAdminService.GetAFNDEPRECIByOidYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._iAFNDEPRECIRepository.GetAFNDEPRECIByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetDeprecationValue(oidAfnActivo As Integer, year As Integer, month As Integer) As Decimal Implements IDistributionFixedAssetAdminService.GetDeprecationValue
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim afn As AFNDEPRECI = Me._iAFNDEPRECIRepository.GetAFNDEPRECIByYearMonth(year, month)
            If afn IsNot Nothing AndAlso afn.OID > 0 AndAlso afn.AFNCALDEP.Any() Then
                Dim afncaldep = afn.AFNCALDEP.Where(Function(x) x.AFNACTIVO IsNot Nothing AndAlso x.AFNACTIVO.Value = oidAfnActivo).FirstOrDefault()
                If afncaldep IsNot Nothing AndAlso afncaldep.OID > 0 Then
                    Return afncaldep.ACADEPMEN
                Else
                    Return 0D
                End If
            Else
                Return 0D
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, audit As AuditMessage) As ActionResult Implements IDistributionFixedAssetAdminService.SP_ConfirmMasiveDistributionFixedAsset
        If Container = Nothing OrElse Container = String.Empty Then
            Throw New ArgumentNullException("ListIds")
        End If
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                'Se convierte el listado de ids a xml para enviarlo al sp
                Dim resultStore = _distributionFixedAssetRepository.SP_ConfirmMasiveDistributionFixedAsset(Container, Year, Month, audit.CodeUser)
                If resultStore.CodeMessage <> 1 Then
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

    Public Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)) Implements IDistributionFixedAssetAdminService.SP_ExportExcelDistributionFixedAsset
        If Container = Nothing OrElse Container = String.Empty Then
            Throw New ArgumentNullException("Container")
        End If
        If Year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If Month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            'Se convierte el listado de ids a xml para enviarlo al sp
            Dim resultStore = _distributionFixedAssetRepository.SP_ExportExcelDistributionFixedAsset(Container, Year, Month)
            Return New ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = resultStore}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _interopCostService.Dispose()
            End If
            _distributionFixedAssetRepository = Nothing
            _sequenceDRepository = Nothing
            _fixedAssetRepository = Nothing
            _iAFNDEPRECIRepository = Nothing
            _interopCostService = Nothing
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
