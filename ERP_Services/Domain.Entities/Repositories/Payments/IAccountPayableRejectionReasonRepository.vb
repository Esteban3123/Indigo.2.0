'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountPayableRejectionReasonRepository
    Inherits IRepository(Of AccountPayableRejectionReason)

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableRejectionReason(code As String, Optional tracking As Boolean = True) As AccountPayableRejectionReason

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableRejectionReasonById(id As String, Optional tracking As Boolean = True) As AccountPayableRejectionReason
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRejectionReason() As List(Of AccountPayableRejectionReason)

End Interface
