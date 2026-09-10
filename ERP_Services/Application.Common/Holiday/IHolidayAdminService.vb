'***********************************************************************
' Assembly         : Application.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Interface IHolidayAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Domingos y Festivos
    ''' </summary>
    ''' <returns>Lista Domingos y Festivos</returns>
    ''' <remarks></remarks>
    Function ListAllHolidays() As List(Of Holiday)

    ''' <summary>
    ''' Obtiene un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiDate">Fecha</param>
    ''' <returns>Domingo y/o Festivo</returns>
    ''' <remarks></remarks>
    Function GetHoliday(ByVal holiDate As DateTime) As Holiday

    ''' <summary>
    ''' Almacena un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveHoliday(ByVal holiday As Holiday, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteHoliday(ByVal holiday As Holiday, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos de un Año (Un año Atrás y otro Adelante de acuerdo al enviado)
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns>Lista de Domingos y/o festivos</returns>
    ''' <remarks></remarks>
    Function ListHolidaybyYear(ByVal year As Integer) As List(Of Holiday)

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListHolidayBetweenDate(ByVal initialDate As Date, ByVal endDate As Date) As List(Of Holiday)

End Interface
