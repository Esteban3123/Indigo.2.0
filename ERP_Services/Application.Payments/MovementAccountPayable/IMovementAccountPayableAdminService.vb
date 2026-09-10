'***********************************************************************
' Assembly         : Application.Payments
' Author           : Diego Andrés Roldán lozano
' Created          : 16-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMovementAccountPayableAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesById(ByVal Id As Integer) As ActionResult(Of MovementAccountPayables)

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesByIdAccountPayable(ByVal IdAccountPayable As Integer) As ActionResult(Of List(Of MovementAccountPayables))

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Function GetMovementAccountPayablesByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer) As ActionResult(Of List(Of MovementAccountPayables))

    ''' <summary>
    ''' Guarda un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    Function SaveMovementAccountPayable(ByVal movementAccountPayable As MovementAccountPayables, ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult(Of MovementAccountPayables)

    ''' <summary>
    ''' Elimina un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    Function DeleteMovementAccountPayable(ByVal movementAccountPayable As MovementAccountPayables, ByVal audit As AuditMessage) As ActionResult

End Interface
