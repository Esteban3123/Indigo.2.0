'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IMovementAccountPayableRepository
    Inherits IRepository(Of MovementAccountPayables)

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesById(ByVal Id As Integer) As MovementAccountPayables

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesByIdAccountPayable(ByVal IdAccountPayable As Integer) As List(Of MovementAccountPayables)

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer) As List(Of MovementAccountPayables)

End Interface
