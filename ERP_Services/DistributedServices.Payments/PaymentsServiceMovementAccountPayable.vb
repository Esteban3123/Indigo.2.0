'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService
    Implements IPaymentsServiceMovementAccountPayable

    ''' <summary>
    ''' Elimina un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    Public Function DeleteMovementAccountPayable(movementAccountPayable As MovementAccountPayables, audit As AuditMessage) As ActionResult Implements IPaymentsServiceMovementAccountPayable.DeleteMovementAccountPayable
        Using service As IMovementAccountPayableAdminService = Container.Current.Resolve(Of IMovementAccountPayableAdminService)()
            Return service.DeleteMovementAccountPayable(movementAccountPayable, audit)
        End Using
        'Return Me._movementAdminService.DeleteMovementAccountPayable(movementAccountPayable, audit)
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetMovementAccountPayablesById(Id As Integer) As ActionResult(Of MovementAccountPayables) Implements IPaymentsServiceMovementAccountPayable.GetMovementAccountPayablesById
        Using service As IMovementAccountPayableAdminService = Container.Current.Resolve(Of IMovementAccountPayableAdminService)()
            Return service.GetMovementAccountPayablesById(Id)
        End Using
        'Return Me._movementAdminService.GetMovementAccountPayablesById(Id)
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    Public Function GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable As Integer) As ActionResult(Of List(Of MovementAccountPayables)) Implements IPaymentsServiceMovementAccountPayable.GetMovementAccountPayablesByIdAccountPayable
        Using service As IMovementAccountPayableAdminService = Container.Current.Resolve(Of IMovementAccountPayableAdminService)()
            Return service.GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable)
        End Using
        'Return Me._movementAdminService.GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable)
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Public Function GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare As Integer) As ActionResult(Of List(Of MovementAccountPayables)) Implements IPaymentsServiceMovementAccountPayable.GetMovementAccountPayablesByIdAccountPayableShare
        Using service As IMovementAccountPayableAdminService = Container.Current.Resolve(Of IMovementAccountPayableAdminService)()
            Return service.GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare)
        End Using
        'Return Me._movementAdminService.GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare)
    End Function

    ''' <summary>
    ''' Guarda un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <returns></returns>
    Public Function SaveMovementAccountPayable(movementAccountPayable As MovementAccountPayables) As ActionResult(Of MovementAccountPayables) Implements IPaymentsServiceMovementAccountPayable.SaveMovementAccountPayable
        Using service As IMovementAccountPayableAdminService = Container.Current.Resolve(Of IMovementAccountPayableAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveMovementAccountPayable(movementAccountPayable, audit)
        End Using
        'Return Me._movementAdminService.SaveMovementAccountPayable(movementAccountPayable, audit)
    End Function
End Class
