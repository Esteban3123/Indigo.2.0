'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 09-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.PaymentOrder Implements IBudgetServicePaymentOrder.GetPaymentOrderByCode
        Using service As IPaymentOrderAdminService = Container.Current.Resolve(Of IPaymentOrderAdminService)()
            Return service.GetPaymentOrderByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPaymentOrderById(id As Integer) As Domain.Entities.PaymentOrder Implements IBudgetServicePaymentOrder.GetPaymentOrderById
        Using service As IPaymentOrderAdminService = Container.Current.Resolve(Of IPaymentOrderAdminService)()
            Return service.GetPaymentOrderById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="PaymentOrder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentOrder(paymentOrder As PaymentOrder, listPaymentOrderDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PaymentOrder) Implements IBudgetServicePaymentOrder.SavePaymentOrder
        Using service As IPaymentOrderAdminService = Container.Current.Resolve(Of IPaymentOrderAdminService)()
            Return service.SavePaymentOrder(paymentOrder, listPaymentOrderDetailDelete, audit)
        End Using
    End Function

End Class