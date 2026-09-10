'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 19-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface ISchedulePaymentDetailRepository
    Inherits IRepository(Of SchedulePaymentDetail)

    ''' <summary>
    ''' obtiene el detalle de la programacion de pagos por id
    ''' </summary>
    ''' <returns></returns>
    Function GetSchedulePaymentDetailById(ByVal Id As Integer) As SchedulePaymentDetail

    ''' <summary>
    ''' Lista los detalles de la programacion de pagos por id de la programacion de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentId">The schedule payment identifier.</param>
    ''' <returns></returns>
    Function ListSchedulePaymentDetailBySchedulePaymentId(ByVal SchedulePaymentId As Integer) As List(Of SchedulePaymentDetail)

    ''' <summary>
    ''' Determines whether [is account payable share in schedule payment detail active] [the specified account payable share identifier].
    ''' </summary>
    ''' <param name="AccountPayableShareId">The account payable share identifier.</param>
    ''' <returns></returns>
    Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As ActionResult

End Interface