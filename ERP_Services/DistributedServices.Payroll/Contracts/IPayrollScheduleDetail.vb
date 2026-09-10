'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
<ServiceContract()> _
Public Interface IPayrollScheduleDetail

    ''' <summary>
    ''' obtiene un detalle del calendario
    ''' </summary>
    ''' <param name="id">id del detalle del calendario</param>
    ''' <returns>ScheduleDetail</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetScheduleDetail(ByVal id As String, session As SessionValues) As ScheduleDetail

    ''' <summary>
    ''' Guarda un detalle Horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveScheduleDetail(scheduleDetail As ScheduleDetail, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un detalle horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteScheduleDetail(scheduleDetail As ScheduleDetail, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene la lista de detalles que tiene un contrato especifico
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Lista de ScheduleDetail</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetScheduleDetailByContractId(contractId As Integer, session As SessionValues) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene el listado de los detalles que estan dentro de un rango de fechas, de un determinado grupo, y que estan marcados como eventos
    ''' </summary>
    ''' <param name="functionalunitId">id de la unidad funcional</param>
    ''' <param name="dateInitial">fecha inicio</param>
    ''' <param name="dateEnd">fecha fin</param>
    ''' <returns>los schedule details</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetScheduleDetailWithEvents(functionalunitId As Integer, dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Guarda un listado de schedule details con los Eventos aprobados
    ''' </summary>
    ''' <param name="list_schedule_details">Listado de detalles con eventos aprobados</param>
    ''' <param name="session">Variables de sesion</param>
    ''' <returns>si realizo o no la accion</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveScheduleDetailWithEventMasive(list_schedule_details As List(Of ScheduleDetail), session As SessionValues) As Boolean

End Interface
