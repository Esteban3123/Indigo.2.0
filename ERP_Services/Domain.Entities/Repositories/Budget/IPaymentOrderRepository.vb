'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 10-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IPaymentOrderRepository
    Inherits IRepository(Of PaymentOrder)

    ''' <summary>
    ''' obtiene una orden de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer) As PaymentOrder

    ''' <summary>
    ''' obtiene una orden de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentOrderById(id As Integer) As PaymentOrder

    ''' <summary>
    ''' Guarda la obligacion
    ''' </summary>
    ''' <param name="PaymentOrderXml"></param>
    ''' <param name="PaymentOrderDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SavePaymentOrder(PaymentOrderXml As String, PaymentOrderDetailForDeleteXml As String, CodeUser As String) As SP_SavePaymentOrder_Result

End Interface
