'************************************************************
' Assembly         : Infraestructure.Data.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 09-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Días Festivos.
''' </summary>
Public Class HolidayRepository
    Inherits GenericRepository(Of Holiday)
    Implements IHolidayRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Consulta una día festivo segun fecha.
    ''' </summary>
    ''' <param name="holiDate">la fecha del día festivo</param>
    ''' <returns>Objeto Holiday</returns>
    Public Function GetHoliday(holiDate As DateTime) As Holiday Implements IHolidayRepository.GetHoliday
        Dim holiDay = (From e In _context.Holiday
                       Where e.Holiday1 = holiDate
                       Select e)

        If holiDay.Count > 0 Then
            Return holiDay.FirstOrDefault()
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Función que obtiene una lista completa de días festivos.
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Public Function ListAllHolidays() As List(Of Holiday) Implements IHolidayRepository.ListAllHolidays
        Dim Busqueda = From e In _context.Holiday
          Where e.State = True
          Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos de un Año (Un año Atrás y otro Adelante de acuerdo al enviado)
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns>Lista de Domingos y/o festivos</returns>
    ''' <remarks></remarks>
    Public Function ListHolidaybyYear(year As Integer) As List(Of Holiday) Implements IHolidayRepository.ListHolidaybyYear
        Dim Busqueda = From e In _context.Holiday
                       Where e.Holiday1.Year >= (year - 1) And e.Holiday1.Year <= (year + 1)
          Select e
        Return Busqueda.ToList()

    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHolidayBetweenDate(initialDate As Date, endDate As Date) As List(Of Holiday) Implements IHolidayRepository.ListHolidayBetweenDate
        Dim query = From e In _context.Holiday.AsNoTracking
                    Where e.Holiday1 >= initialDate And e.Holiday1 <= endDate
                    Select e
        Return query.ToList()
    End Function
End Class