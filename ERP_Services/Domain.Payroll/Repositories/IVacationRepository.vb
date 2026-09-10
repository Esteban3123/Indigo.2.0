'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 09-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IVacationRepository
    Inherits IRepository(Of Vacation)

    ''' <summary>
    ''' Funcion la cual obtiene las vacaciones de una fecha de liquidacion especifica
    ''' </summary>
    ''' <param name="dateLiquidation">Fecha de liquidacion de nomina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationLiquidationDate(dateLiquidation As Date, state As Byte) As List(Of Vacation)

    ''' <summary>
    ''' Funcion para listar todas las vacaciones de tengan cierto estado de incorporacion
    ''' </summary>
    ''' <param name="stateIncorporation">Estado de incorporacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationStateIncorporation(stateIncorporation As Byte) As List(Of Vacation)

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Vacation)
    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas y contratos no liquidados
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationBetweenDateAndActiveContract(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Vacation)

    ''' <summary>
    ''' Obtiene los detalles de conceptos de vacaciones por clase
    ''' que ocurrieron dentro del rango de fechas especificado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="conceptClass">Clase del Concepto (Ej: 049 para Prima de Vacaciones)</param>
    ''' <param name="dateInitial">Fecha Inicial del período</param>
    ''' <param name="dateEnd">Fecha Final del período</param>
    ''' <returns>Lista de VacationDetail con la clase de concepto especificada</returns>
    ''' <remarks></remarks>
    Function GetVacationIncentiveByDateRange(employeeId As Integer, conceptClass As String, dateInitial As Date, dateEnd As Date) As List(Of VacationDetail)

End Interface
