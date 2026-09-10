'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface INoveltyScheduleDetailRepository
    Inherits IRepository(Of NoveltyScheduleDetail)

    ''' <summary>
    ''' obtiene un listado de los detalles de las novedades que deben tener deducciones
    ''' </summary>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de las novedades</returns>
    ''' <remarks></remarks>
    Function GetNoveltyScheduleDetailBetweenDate(dateInitial As Date, dateEnd As Date) As List(Of NoveltyScheduleDetail)

    ''' <summary>
    ''' Obtiene un listado de los detalles de una novedad
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="noveltyId">Id novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNoveltyScheduleDetailByEmployeeNoveltyId(employeeId As Integer, noveltyId As Integer) As List(Of NoveltyScheduleDetail)
    ''' <summary>
    ''' Función utilizada en la Liquidación de la Nómina para 
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="dateInitial"></param>
    ''' <param name="dateEnd"></param>
    ''' <returns></returns>
    Function GetNoveltyScheduleDetailByEmployeeUnitBetweenDateWithoutNovelties(employeeId As Integer, PayrollDate As Date, Status As Integer, IdGroup As Integer) As List(Of NoveltyScheduleDetail)

End Interface
