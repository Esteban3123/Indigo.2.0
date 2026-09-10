'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 14-09-2015
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Data.Entity.Validation
Imports System.Text

#End Region

Public Class ReimbursementResourceAdminService
    Implements IReimbursementResourceAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de reintegro
    ''' </summary>
    ''' <remarks></remarks>
    Private _reimbursementResourceRepository As IReimbursementResourceRepository

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    ''' <summary>
    ''' repositorio de sequencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

    Private _sequenceRepository As IBudgetSequenceRepository

    Private _budgetService As IBudgetService
    ''' <summary>
    ''' servicios de modificación obligación
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationModificationAdminService As IObligationModificationAdminService
    ''' <summary>
    ''' Servicios de modificación compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentModificationAdminService As ICommitmentModificationAdminService
    ''' <summary>
    ''' Servicios de modificación disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityModificationAdminService As IAvailabilityModificationAdminService
    ''' <summary>
    ''' Servicios de modificación presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetModificationAdminService As IBudgetModificationAdminService
    ''' <summary>
    ''' repositorio de los detalles de la obligación
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationDetailRepository As IObligationDetailRepository
    ''' <summary>
    ''' repositorio de la modificación de obligación
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationModificationRepository As IObligationModificationRepository
    ''' <summary>
    ''' repositorio de la modificación de compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentModificationRepository As ICommitmentModificationRepository
    ''' <summary>
    ''' repositorio de la modificación de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityModificationRepository As IAvailabilityModificationRepository
    ''' <summary>
    ''' repositorio de la modificación de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetModificationRepository As IBudgetModificationRepository
    ''' <summary>
    ''' repositorio de orden de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentOrderRepository As IPaymentOrderRepository
    ''' <summary>
    ''' servicios de orden de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentOrderAdminService As IPaymentOrderAdminService
    ''' <summary>
    ''' repositorio del compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentDetailRepository As ICommitmentDetailRepository
    ''' <summary>
    ''' repositorio de los detalles de la disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availablityDetailRepository As IAvailabilityDetailRepository
    ''' <summary>
    ''' repositorio de vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _validityRepository As IValidityRepository
    ''' <summary>
    ''' repositorio de obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationRepository As IObligationRepository
    ''' <summary>
    ''' repositorio de los detalles de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetDetailRepository As IBudgetRepository
    ''' <summary>
    ''' repositorio del pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _annualizedCashFlowRepository As IAnnualizedCashFlowRepository
    ''' <summary>
    ''' repositorio de rubro
    ''' </summary>
    ''' <remarks></remarks>
    Private _categoryRepository As IBudgetItemRepository
    ''' <summary>
    ''' repositorio de tipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _revenueTypeRepository As IExpenseTypeRepository
    ''' <summary>
    ''' repositorio del detalle de una orden de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentOrderDetailRepository As IPaymentOrderDetailRepository

#End Region

#Region "Builder"

    Public Sub New(reimbursementResourceRepository As IReimbursementResourceRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, obligationModificationAdminService As IObligationModificationAdminService,
                   commitmentModificationAdminService As ICommitmentModificationAdminService, availabilityModificationAdminService As IAvailabilityModificationAdminService,
                   budgetModificationAdminService As IBudgetModificationAdminService, obligationDetailRepository As IObligationDetailRepository, obligationModificationRepository As IObligationModificationRepository,
                   commitmentModificationRepository As ICommitmentModificationRepository, availabilityModificationRepository As IAvailabilityModificationRepository,
                   budgetModificationRepository As IBudgetModificationRepository, paymentOrderRepository As IPaymentOrderRepository, paymentOrderAdminService As IPaymentOrderAdminService,
                   commitmentDetailRepository As ICommitmentDetailRepository, validityRepository As IValidityRepository, sequenceRepository As IBudgetSequenceRepository,
                   availablityDetailRepository As IAvailabilityDetailRepository, obligationRepository As IObligationRepository, budgetDetailRepository As IBudgetRepository,
                   annualizedCashFlowRepository As IAnnualizedCashFlowRepository, categoryRepository As IBudgetItemRepository, revenueTypeRepository As IExpenseTypeRepository,
                   paymentOrderDetailRepository As IPaymentOrderDetailRepository)
        If reimbursementResourceRepository Is Nothing Then
            Throw New ArgumentNullException("reimbursementResourceRepository")
        End If
        _reimbursementResourceRepository = reimbursementResourceRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _obligationModificationAdminService = obligationModificationAdminService
        _commitmentModificationAdminService = commitmentModificationAdminService
        _availabilityModificationAdminService = availabilityModificationAdminService
        _budgetModificationAdminService = budgetModificationAdminService
        _obligationDetailRepository = obligationDetailRepository
        _obligationModificationRepository = obligationModificationRepository
        _commitmentModificationRepository = commitmentModificationRepository
        _availabilityModificationRepository = availabilityModificationRepository
        _budgetModificationRepository = budgetModificationRepository
        _paymentOrderRepository = paymentOrderRepository
        _paymentOrderAdminService = paymentOrderAdminService
        _commitmentDetailRepository = commitmentDetailRepository
        _validityRepository = validityRepository
        _sequenceRepository = sequenceRepository
        _availablityDetailRepository = availablityDetailRepository
        _obligationRepository = obligationRepository
        _budgetDetailRepository = budgetDetailRepository
        _annualizedCashFlowRepository = annualizedCashFlowRepository
        _categoryRepository = categoryRepository
        _revenueTypeRepository = revenueTypeRepository
        _paymentOrderDetailRepository = paymentOrderDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene un reintegro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementResourceByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As ReimbursementResource Implements IReimbursementResourceAdminService.GetReimbursementResourceByCode
        Try
            Dim reimbursementResource = _reimbursementResourceRepository.GetReimbursementResourceByCode(code, BudgetaryValidityId)
            If reimbursementResource IsNot Nothing AndAlso reimbursementResource.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ReimbursementResource)(reimbursementResource, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return reimbursementResource
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ReimbursementResource
        End Try
    End Function

    ''' <summary>
    ''' obtiene un reintegro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementResourceById(id As Integer) As ReimbursementResource Implements IReimbursementResourceAdminService.GetReimbursementResourceById
        Try
            Return _reimbursementResourceRepository.GetReimbursementResourceById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ReimbursementResource
        End Try
    End Function

    ''' <summary>
    ''' Metodo para consultar hasta que punto se puede hacer el reintegro
    ''' </summary>
    ''' <param name="paymentOrderId"></param>
    ''' <returns>1 = Obligación, 2 = Compromiso / Reserva, 3 = Presupuesto</returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementUntilByPaymentOrderId(paymentOrderId As Integer) As Integer Implements IReimbursementResourceAdminService.GetReimbursementUntilByPaymentOrderId
        Try
            Return _reimbursementResourceRepository.GetReimbursementUntilByPaymentOrderId(paymentOrderId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Guarda un reintegro
    ''' </summary>
    ''' <param name="reimbursementResource"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveReimbursementResource(reimbursementResource As ReimbursementResource, listReimbursementResourceDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of ReimbursementResource) Implements IReimbursementResourceAdminService.SaveReimbursementResource
        If reimbursementResource Is Nothing Then
            Throw New ArgumentNullException("ReimbursementResource")
        End If

        Dim unitOfWork As IUnitWork = Me._reimbursementResourceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlReimbursementResource(reimbursementResource)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listReimbursementResourceDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(reimbursementResource.Id, reimbursementResource.Status)

                Dim resultStore = Me._reimbursementResourceRepository.SP_SaveReimbursementResource(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of ReimbursementResource) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                reimbursementResource.Id = resultStore.Id
                reimbursementResource.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of ReimbursementResource)(reimbursementResource, audit, auditStatus, reimbursementResource.OriginalValue)
                auditProcess.Execute()

                reimbursementResource.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of ReimbursementResource) With {.StateResult = True, .ObjectEmbbeded = reimbursementResource, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of ReimbursementResource) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ReimbursementResource) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlReimbursementResource(reimbursementResource As ReimbursementResource) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ReimbursementResource>")

        builder.Append("<Id>" & reimbursementResource.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & reimbursementResource.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & reimbursementResource.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & reimbursementResource.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & reimbursementResource.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<PaymentOrderId>" & reimbursementResource.PaymentOrderId & "</PaymentOrderId>")
        builder.Append("<UpTo>" & reimbursementResource.UpTo & "</UpTo>")
        builder.Append("<Document>" & reimbursementResource.Document & "</Document>")
        builder.Append("<Observations>" & reimbursementResource.Observations & "</Observations>")
        builder.Append("<Status>" & reimbursementResource.Status & "</Status>")

        For Each detail In reimbursementResource.ReimbursementResourceDetaill
            builder.Append("<ReimbursementResourceDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<PaymentOrderDetailId>" & detail.PaymentOrderDetailId & "</PaymentOrderDetailId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</ReimbursementResourceDetail>")
        Next

        builder.Append("</ReimbursementResource>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listObligationModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listObligationModificationDetailDelete IsNot Nothing AndAlso listObligationModificationDetailDelete.Count > 0 Then
            For Each detail In listObligationModificationDetailDelete
                builder.Append("<ObligationModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</ObligationModificationDetail>")
            Next
        End If

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _budgetService.Dispose()
                _obligationModificationAdminService.Dispose()
                _commitmentModificationAdminService.Dispose()
                _availabilityModificationAdminService.Dispose()
                _budgetModificationAdminService.Dispose()
                _paymentOrderAdminService.Dispose()
            End If
            _reimbursementResourceRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _obligationModificationAdminService = Nothing
            _commitmentModificationAdminService = Nothing
            _availabilityModificationAdminService = Nothing
            _budgetModificationAdminService = Nothing
            _obligationDetailRepository = Nothing
            _obligationModificationRepository = Nothing
            _commitmentModificationRepository = Nothing
            _availabilityModificationRepository = Nothing
            _budgetModificationRepository = Nothing
            _paymentOrderRepository = Nothing
            _paymentOrderAdminService = Nothing
            _commitmentDetailRepository = Nothing
            _validityRepository = Nothing
            _sequenceRepository = Nothing
            _availablityDetailRepository = Nothing
            _obligationRepository = Nothing
            _budgetDetailRepository = Nothing
            _annualizedCashFlowRepository = Nothing
            _categoryRepository = Nothing
            _revenueTypeRepository = Nothing
            _paymentOrderDetailRepository = Nothing
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
