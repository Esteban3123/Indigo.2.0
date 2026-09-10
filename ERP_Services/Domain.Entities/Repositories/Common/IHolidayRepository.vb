'************************************************************
' Assembly         : Domain.Common
' Author           : Juan Diego Diaz
' Created          : 15-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Common.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio Días Festivos.
''' </summary>
''' <remarks></remarks>
Public Interface IHolidayRepository
    Inherits IRepository(Of Holiday)

    ''' <summary>
    ''' Función que obtiene una lista completa de días festivos.
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllHolidays() As List(Of Holiday)

    ''' <summary>
    ''' Consulta una día festivo segun fecha.
    ''' </summary>
    ''' <param name="holiDate">la fecha del día festivo</param>
    ''' <returns>Objeto Holiday</returns>
    Function GetHoliday(ByVal holiDate As DateTime) As Holiday

    ''' <summary>
    ''' Obtiene una Lista de Fechas por Año (Tener en cuenta que se buscan un año antes y uno después de acuerdo al año enviado)
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns>Lista de Fechas por Mes y Año</returns>
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
