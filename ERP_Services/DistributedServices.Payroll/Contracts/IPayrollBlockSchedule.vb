'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-08-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollBlockSchedule

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    <OperationContract()>
    Function ListAllBlockSchedule(session As SessionValues) As Tuple(Of BlockScheduleC, List(Of BlockSchedule))

    ''' <summary>
    ''' Elimina un banco
    ''' </summary>
    ''' <param name="bank">Banco</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBlockSchedule(ByVal BlockScheduleC As BlockScheduleC, ByVal ListBlockSchedule As List(Of BlockSchedule), session As SessionValues) As ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule)))

End Interface
