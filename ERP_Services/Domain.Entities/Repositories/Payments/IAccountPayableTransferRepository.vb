'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountPayableTransferRepository
    Inherits IRepository(Of AccountPayableTransfer)

    Function GetAccountPayableFilingUnitSourceIdRefundId(refundId As Integer, FilingUnitSourceId As Integer) As Refunds

    Function GetAccountPayableFilingUnitSourceIdAccountPayableId(accountPayableId As Integer, FilingUnitSourceId As Integer) As AccountPayable

    

    Function GetAccountPayableTransferDetailByRefundId(refundId As Integer) As AccountPayableTransferDetail

    ''' <summary>
    ''' Obtiene un traslado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableTransfer(code As String, Optional tracking As Boolean = True) As AccountPayableTransfer

    ''' <summary>
    ''' Obtiene un traslado
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableTransferById(id As String, Optional tracking As Boolean = True) As AccountPayableTransfer

    ''' <summary>
    ''' Obtiene un traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableTransferDetailByAccountPayableId(accountPayableId As Integer) As AccountPayableTransferDetail

End Interface
