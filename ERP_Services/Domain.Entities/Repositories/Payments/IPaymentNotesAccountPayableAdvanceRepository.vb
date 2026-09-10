'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPaymentNotesAccountPayableAdvanceRepository
    Inherits IRepository(Of PaymentNotesAccountPayableAdvance)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNotesAccountPayableAdvanceById(id As String, Optional tracking As Boolean = True) As PaymentNotesAccountPayableAdvance

    ''' <summary>
    ''' Gets the payment notes account payable advance by account payable share identifier.
    ''' </summary>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetPaymentNotesAccountPayableAdvanceByAccountPayableListId(ByVal accountPayableListId As List(Of Integer), Optional tracking As Boolean = True) As List(Of String)

    ''' <summary>
    ''' Obtiene un listado de cxp por id de la tabla PaymentNotesAccountPayableAdvance
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer, Optional tracking As Boolean = True) As List(Of PaymentNotesAccountPayableAdvance)

End Interface
