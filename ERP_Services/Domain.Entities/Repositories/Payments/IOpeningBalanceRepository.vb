'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IOpeningBalanceRepository
    Inherits IRepository(Of InitialBalance)

    ''' <summary>
    ''' Obtiene un saldo inicial por el consecutivo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOpeningBalance(ByVal code As String, Optional tracking As Boolean = True) As InitialBalance

End Interface
