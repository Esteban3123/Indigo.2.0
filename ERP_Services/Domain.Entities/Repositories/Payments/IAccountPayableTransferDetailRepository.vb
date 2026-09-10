'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountPayableTransferDetailRepository
    Inherits IRepository(Of AccountPayableTransferDetail)


    ''' <summary>
    ''' Obtiene una lista de detalle de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListAccountPayableTransferDetailByAccountPayableId(listIDAccountPayableTransferDetail As List(Of Integer)) As List(Of AccountPayableTransferDetail)
    ''' <summary>
    ''' Valida si es la ultima revision de traslado que se le realiza al oficio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateConfirmEvaluation(AccountPayableTransferId As Integer) As Boolean

End Interface
