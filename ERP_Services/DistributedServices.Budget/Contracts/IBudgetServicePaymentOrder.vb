'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 09-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServicePaymentOrder

    ''' <summary>
    ''' obtiene una orden de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.PaymentOrder
    ''' <summary>
    ''' obtiene una orden de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPaymentOrderById(id As Integer) As PaymentOrder

    ''' <summary>
    ''' Guarda una orden de pago
    ''' </summary>
    ''' <param name="paymentOrder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePaymentOrder(paymentOrder As PaymentOrder, listPaymentOrderDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PaymentOrder)

End Interface
