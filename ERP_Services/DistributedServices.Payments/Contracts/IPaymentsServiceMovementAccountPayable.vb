'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsServiceMovementAccountPayable

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetMovementAccountPayablesById(ByVal Id As Integer) As ActionResult(Of MovementAccountPayables)

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetMovementAccountPayablesByIdAccountPayable(ByVal IdAccountPayable As Integer) As ActionResult(Of List(Of MovementAccountPayables))

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetMovementAccountPayablesByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer) As ActionResult(Of List(Of MovementAccountPayables))

    ''' <summary>
    ''' Guarda un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveMovementAccountPayable(ByVal movementAccountPayable As MovementAccountPayables) As ActionResult(Of MovementAccountPayables)

    ''' <summary>
    ''' Elimina un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMovementAccountPayable(movementAccountPayable As MovementAccountPayables, audit As AuditMessage) As ActionResult

End Interface
