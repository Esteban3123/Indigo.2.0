'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPHoliday

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos
    ''' </summary>
    ''' <returns>Lista de Domingos y/o Festivos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllHolidays(session As SessionValues) As List(Of Holiday)

    ''' <summary>
    ''' Elimina un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteHoliday(ByVal holiday As Holiday, session As SessionValues) As Boolean

    ''' <summary>
    ''' Almacena o Actualiza un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveHoliday(ByVal holiday As Holiday, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiDate">Fecha</param>
    ''' <returns>Domingo y/o Festivo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetHoliday(ByVal holiDate As Date, session As SessionValues) As Holiday

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos de un Año (Un año Atrás y otro Adelante de acuerdo al enviado)
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns>Lista de Domingos y/o festivos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListHolidaybyYear(ByVal year As Integer, session As SessionValues) As List(Of Holiday)

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListHolidayBetweenDate(ByVal initialDate As Date, ByVal endDate As Date, session As SessionValues) As List(Of Holiday)

End Interface
