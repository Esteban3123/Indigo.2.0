'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 09-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IPaymentOrderAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una orden de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As PaymentOrder
    ''' <summary>
    ''' obtiene una orden de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentOrderById(id As Integer) As PaymentOrder
    ''' <summary>
    ''' Guarda una orden de pago
    ''' </summary>
    ''' <param name="paymentOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePaymentOrder(paymentOrder As PaymentOrder, listPaymentOrderDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PaymentOrder)

End Interface
