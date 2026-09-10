'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
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
Imports System.Transactions
Imports Domain.Entities.Service

Public Class ProductionCenterAdminService
    Implements IProductionCenterAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de centros de produccion
    ''' </summary>
    Private _productionCenterRepository As IProductionCenterRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

    ''' <summary>
    ''' repositorio de areas de servicio
    ''' </summary>
    Private _serviceAreaRepository As IServiceAreaRepository

    ''' <summary>
    ''' Repositorios de centros de costo de dinamica
    ''' </summary>
    Private _costCenterRepository As ICostCenterRepository

    Private _mainAccountDRepository As IMainAccountRepository
    Private _interopCostService As IInteropCostServices

#End Region

#Region "Methods"

    Public Sub New(ByVal productionCenterRepository As IProductionCenterRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository, ByVal serviceAreaRepository As IServiceAreaRepository,
                   ByVal costCenterRepository As ICostCenterRepository, mainAccountDRepository As IMainAccountRepository, interopCostService As IInteropCostServices)
        If productionCenterRepository Is Nothing Then
            Throw New ArgumentNullException("productionCenterRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        If serviceAreaRepository Is Nothing Then
            Throw New ArgumentNullException("serviceAreaRepository")
        End If
        If costCenterRepository Is Nothing Then
            Throw New ArgumentNullException("costCenterRepository")
        End If
        _serviceAreaRepository = serviceAreaRepository
        _productionCenterRepository = productionCenterRepository
        _sequenceDRepository = sequenceDRepository
        _costCenterRepository = costCenterRepository
        _mainAccountDRepository = mainAccountDRepository
        _interopCostService = interopCostService
    End Sub

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Public Function DeleteProductionCenter(productionCenter As ProductionCenter, audit As AuditMessage) As ActionResult Implements IProductionCenterAdminService.DeleteProductionCenter
        If productionCenter Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._productionCenterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While productionCenter.ProductionCenterHomologation.Count > 0
                    productionCenter.ProductionCenterHomologation.Item(productionCenter.ProductionCenterHomologation.Count() - 1).MarkAsDeleted()
                End While
                While productionCenter.ProductionCenterCostCenter.Count > 0
                    productionCenter.ProductionCenterCostCenter.Item(productionCenter.ProductionCenterCostCenter.Count() - 1).MarkAsDeleted()
                End While

                productionCenter.MarkAsDeleted()
                productionCenter.ModificationUser = audit.CodeUser
                productionCenter.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProductionCenter)(productionCenter, audit, status)

                Me._productionCenterRepository.SaveEntity(productionCenter)
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
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    Public Function GetProductionCenter(code As String, audit As AuditMessage) As ActionResult(Of ProductionCenter) Implements IProductionCenterAdminService.GetProductionCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim productionCenter As ProductionCenter = Me._productionCenterRepository.GetProductionCenter(code.Trim())
            If productionCenter IsNot Nothing AndAlso productionCenter.Id > 0 Then

                If productionCenter.CancellationCostMainAccountId IsNot Nothing Then
                    Dim cancellationCostMainAccount As CTNCUENTA = _mainAccountDRepository.GetMainAccountById(productionCenter.CancellationCostMainAccountId)
                    productionCenter.NumberNameMainAccountCancellationCost = String.Concat(cancellationCostMainAccount.CUECODIGO, " - ", cancellationCostMainAccount.CUENOMBRE)
                End If

                If productionCenter.ProductionCenterCostCenter IsNot Nothing AndAlso productionCenter.ProductionCenterCostCenter.Any() Then
                    For Each cc In productionCenter.ProductionCenterCostCenter
                        Dim ccenter = _costCenterRepository.GetCostCenterById(cc.CostCenterId)
                        cc.CostCenterName = ccenter.CCNOMBRE
                    Next
                End If

                'If productionCenter.ProductionCenterSales IsNot Nothing AndAlso productionCenter.ProductionCenterSales.Count > 0 Then
                '    For Each item As ProductionCenterSales In productionCenter.ProductionCenterSales
                '        Dim servArea As GENARESER = _serviceAreaRepository.GetServiceAreaById(item.ServiceAreaId)
                '        If servArea IsNot Nothing AndAlso servArea.OID > 0 Then
                '            item.ServiceAreaName = servArea.GASNOMBRE
                '            item.CTNCuentaServiceArea = servArea.CTNCUENTA2
                '        End If
                '    Next
                'End If
                If productionCenter.ProductionCenterHomologation IsNot Nothing AndAlso productionCenter.ProductionCenterHomologation.Count > 0 Then
                    For Each item As ProductionCenterHomologation In productionCenter.ProductionCenterHomologation
                        Dim mainAccountOrigin As CTNCUENTA = _mainAccountDRepository.GetMainAccountById(item.AccountOriginId)
                        If mainAccountOrigin IsNot Nothing AndAlso mainAccountOrigin.OID > 0 Then
                            item.FullNameAccountOrigin = String.Concat(mainAccountOrigin.CUECODIGO, " - ", mainAccountOrigin.CUENOMBRE)
                        End If
                        If item.HomologationType <> 6 And productionCenter.CenterType = 1 Then
                            Dim mainAccountDestination As CTNCUENTA = _mainAccountDRepository.GetMainAccountById(item.AccountTargetId)
                            If mainAccountDestination IsNot Nothing AndAlso mainAccountDestination.OID > 0 Then
                                item.FullNameAccountDestination = String.Concat(mainAccountDestination.CUECODIGO, " - ", mainAccountDestination.CUENOMBRE)
                            End If
                        End If
                    Next
                End If

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProductionCenter)(productionCenter, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    Public Function GetProductionCenterById(id As Integer) As ProductionCenter Implements IProductionCenterAdminService.GetProductionCenterById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim productionCenter As ProductionCenter = Me._productionCenterRepository.GetProductionCenterById(id)
            Return productionCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetProductionCenterByCostCenterOid(costCenterOid As Integer) As ProductionCenter Implements IProductionCenterAdminService.GetProductionCenterByCostCenterOid
        If costCenterOid = 0 Then
            Throw New ArgumentNullException("costCenterOid")
        End If
        Try
            Dim productionCenter As ProductionCenter = Me._productionCenterRepository.GetProductionCenterByCostCenterOid(costCenterOid)
            Return productionCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Function SaveProductionCenter(ByVal productionCenter As ProductionCenter, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ProductionCenter) Implements IProductionCenterAdminService.SaveProductionCenter
        If productionCenter Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._productionCenterRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try


            'validar centros de costo

            'Dim resV As ActionResult = _interopCostService.ValidateProductionCenterSave(productionCenter)
            'If Not resV.StateResult Then
            '    Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = resV.Message}
            'End If


            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(productionCenter.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            productionCenter.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), productionCenter.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As ProductionCenter = Nothing
                Dim status As Integer
                If productionCenter.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), productionCenter.Code)
                    End If
                    productionCenter.CreationDate = Date.Now
                    productionCenter.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    productionCenter.ModificationDate = Date.Now
                    productionCenter.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = productionCenter.OriginalValue
                End If

                Me._productionCenterRepository.SaveEntity(productionCenter)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ProductionCenter)(productionCenter, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = productionCenter, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionCenter) With {.StatusCode = eStatusResult.WARNING, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Function UpdateStateProductionCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionCenter) Implements IProductionCenterAdminService.UpdateStateProductionCenter
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
            Dim productionCenter As ProductionCenter = Me._productionCenterRepository.GetProductionCenter(code.Trim())
            If productionCenter IsNot Nothing AndAlso productionCenter.Id > 0 Then
                productionCenter.Status = state
            End If
            Return Me.SaveProductionCenter(productionCenter, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionCenter) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenter() As List(Of ProductionCenter) Implements IProductionCenterAdminService.ListProductionCenter
        Try
            Return _productionCenterRepository.ListProductionCenter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTES
    ''' </summary>
    ''' <returns></returns>
    Public Function ListrptResultProductionCostsExpenses(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String) As List(Of SP_ReportResultProductionCostsExpenses_Result) Implements IProductionCenterAdminService.ListrptResultProductionCostsExpenses
        Try
            Return _productionCenterRepository.ListrptResultProductionCostsExpenses(InitialMonth, EndMonth, Year, InitialCodeCenter, EndCodeCenter, Container)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportResultProductionCostsExpenses_Result)()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTES
    ''' </summary>
    ''' <returns></returns>
    Public Function ListrptResultProductionCostsExpensesDetail(DateStart As Date, ByVal DateEnd As Date, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String, ByVal DetailType As Integer) As List(Of SP_ReportResultProductionCostsExpensesDetail_Result) Implements IProductionCenterAdminService.ListrptResultProductionCostsExpensesDetail
        Try
            Return _productionCenterRepository.ListrptResultProductionCostsExpensesDetail(DateStart, DateEnd, InitialCodeCenter, EndCodeCenter, Container, DetailType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportResultProductionCostsExpensesDetail_Result)()
        End Try
    End Function
#End Region

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResult_Result) Implements IProductionCenterAdminService.ListReportOperatingResult
        Try
            Return _productionCenterRepository.ListReportOperatingResult(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportOperatingResult_Result)()
        End Try
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result) Implements IProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStructure
        Try
            Return _productionCenterRepository.ListReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result)()
        End Try
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements IProductionCenterAdminService.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Try
            Return _productionCenterRepository.ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result)()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _interopCostService.Dispose()
            End If
            _serviceAreaRepository = Nothing
            _productionCenterRepository = Nothing
            _sequenceDRepository = Nothing
            _costCenterRepository = Nothing
            _mainAccountDRepository = Nothing
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