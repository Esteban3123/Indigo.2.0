'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 09-09-2015
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
Imports System.Text

#End Region

Public Class PaymentOrderAdminService
    Implements IPaymentOrderAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentOrderRepository As IPaymentOrderRepository

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

    Private _budgetService As IBudgetService

    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    Private _categoryRepository As IBudgetItemRepository

    Private _revenueTypeRepository As IExpenseTypeRepository

    Private _obligationDetailRepository As IObligationDetailRepository

    Private _obligationRepository As IObligationRepository

    Private _annualizedCashFlowRepository As IAnnualizedCashFlowRepository

#End Region

#Region "Builder"

    Public Sub New(paymentOrderRepository As IPaymentOrderRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository,
                   categoryRepository As IBudgetItemRepository, revenueTypeRepository As IExpenseTypeRepository, obligationRepository As IObligationRepository,
                   obligationDetailRepository As IObligationDetailRepository, annualizedCashFlowRepository As IAnnualizedCashFlowRepository)
        If paymentOrderRepository Is Nothing Then
            Throw New ArgumentNullException("paymentOrderRepository")
        End If
        _paymentOrderRepository = paymentOrderRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
        _categoryRepository = categoryRepository
        _revenueTypeRepository = revenueTypeRepository
        _obligationDetailRepository = obligationDetailRepository
        _obligationRepository = obligationRepository
        _annualizedCashFlowRepository = annualizedCashFlowRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una orden de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As PaymentOrder Implements IPaymentOrderAdminService.GetPaymentOrderByCode
        Try
            Dim paymentOrder = _paymentOrderRepository.GetPaymentOrderByCode(code, BudgetaryValidityId)
            If paymentOrder IsNot Nothing AndAlso paymentOrder.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentOrder)(paymentOrder, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return paymentOrder
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PaymentOrder
        End Try
    End Function

    ''' <summary>
    ''' obtiene una orden de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentOrderById(id As Integer) As PaymentOrder Implements IPaymentOrderAdminService.GetPaymentOrderById
        Try
            Return _paymentOrderRepository.GetPaymentOrderById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PaymentOrder
        End Try
    End Function

    ''' <summary>
    ''' Guarda una orden de pago
    ''' </summary>
    ''' <param name="paymentOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentOrder(paymentOrder As PaymentOrder, listPaymentOrderDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PaymentOrder) Implements IPaymentOrderAdminService.SavePaymentOrder
        If paymentOrder Is Nothing Then
            Throw New ArgumentNullException("paymentOrder")
        End If

        Dim unitOfWork As IUnitWork = Me._paymentOrderRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlPaymentOrder(paymentOrder)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listPaymentOrderDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(paymentOrder.Id, paymentOrder.Status)

                Dim resultStore = Me._paymentOrderRepository.SP_SavePaymentOrder(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of PaymentOrder) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                paymentOrder.Id = resultStore.Id
                paymentOrder.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of PaymentOrder)(paymentOrder, audit, auditStatus, paymentOrder.OriginalValue)
                auditProcess.Execute()

                paymentOrder.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of PaymentOrder) With {.StateResult = True, .ObjectEmbbeded = paymentOrder, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of PaymentOrder) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentOrder) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlPaymentOrder(PaymentOrder As PaymentOrder) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<PaymentOrder>")

        builder.Append("<Id>" & PaymentOrder.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & PaymentOrder.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & PaymentOrder.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & PaymentOrder.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & PaymentOrder.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<ThirdPartyId>" & PaymentOrder.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<Document>" & PaymentOrder.Document & "</Document>")
        builder.Append("<PaymentOrderType>" & PaymentOrder.PaymentOrderType & "</PaymentOrderType>")
        builder.Append("<Observations>" & PaymentOrder.Observations & "</Observations>")
        builder.Append("<Status>" & PaymentOrder.Status & "</Status>")
        If PaymentOrder.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & PaymentOrder.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & PaymentOrder.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & PaymentOrder.EntityName & "</EntityName>")
        End If

        For Each detail In PaymentOrder.PaymentOrderDetail
            builder.Append("<PaymentOrderDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<PaymentOrderId>" & detail.PaymentOrderId & "</PaymentOrderId>")
            builder.Append("<ObligationDetailId>" & detail.ObligationDetailId & "</ObligationDetailId>")
            builder.Append("<ExpiredDate>" & detail.ExpiredDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ExpiredDate>")
            builder.Append("<InitialValue>" & detail.InitialValue.ToString().Replace(",", ".") & "</InitialValue>")

            builder.Append("</PaymentOrderDetail>")
        Next

        builder.Append("</PaymentOrder>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listPaymentOrderDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listPaymentOrderDetailDelete IsNot Nothing AndAlso listPaymentOrderDetailDelete.Count > 0 Then
            For Each detail In listPaymentOrderDetailDelete
                builder.Append("<PaymentOrderDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</PaymentOrderDetail>")
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
            End If
            _paymentOrderRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _categoryRepository = Nothing
            _revenueTypeRepository = Nothing
            _obligationDetailRepository = Nothing
            _obligationRepository = Nothing
            _annualizedCashFlowRepository = Nothing
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
