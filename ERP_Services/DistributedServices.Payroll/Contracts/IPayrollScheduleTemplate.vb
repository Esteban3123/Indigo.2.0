'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
<ServiceContract()> _
Public Interface IPayrollScheduleTemplate

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllScheduleTemplate(session As SessionValues) As List(Of ScheduleTemplate)

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo de la plantilla</param>
    ''' <returns>Plantilla</returns>
    <OperationContract()> _
    Function GetScheduleTemplate(ByVal code As String, session As SessionValues) As ScheduleTemplate

    ''' <summary>
    ''' Obtiene una plantilla a través del id
    ''' </summary>
    ''' <param name="id">id de la plantilla</param>
    ''' <returns>Plantilla</returns>
    <OperationContract()> _
    Function GetScheduleTemplateById(ByVal id As String, session As SessionValues) As ScheduleTemplate

    ''' <summary>
    ''' Graba o Actualiza una plantilla y sus agregados
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveScheduleTemplate(ByVal schedule As ScheduleTemplate, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina una plantilla
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    <OperationContract()> _
    Function DeleteScheduleTemplate(ByVal schedule As ScheduleTemplate, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un cargo
    ''' </summary>
    ''' <param name="code">Código del cargo</param>
    ''' <returns>Cargo</returns>
    <OperationContract()> _
    Function ChangeStateScheduleTemplate(Code As String, state As Boolean, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllScheduleTemplateByStatus(State As Boolean, session As SessionValues) As List(Of ScheduleTemplate)

End Interface
