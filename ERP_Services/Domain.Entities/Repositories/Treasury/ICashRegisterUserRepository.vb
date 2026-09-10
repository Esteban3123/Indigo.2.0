'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Security.Entities

Public Interface ICashRegisterUserRepository
    Inherits IRepository(Of CashRegisterUser)

    ''' <summary>
    ''' Obtiene un usuario de registro de caja
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashRegisterUserById(ByVal Id As Integer) As CashRegisterUser

    ''' <summary>
    ''' Lista los usuarios relacionados a un registro de caja
    ''' </summary>
    ''' <returns></returns>
    Function ListCashRegisterUserByIdCashRegister(ByVal IdCashRegister As Integer) As List(Of CashRegisterUser)

End Interface
