'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-12-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IRetroactiveCRepository
    Inherits IRepository(Of RetroactiveC)

    ''' <summary>
    ''' Obtener el Listado de Retroactividad
    ''' </summary>
    ''' <param name="RetroactiveInitialDate">fecha Inicio Retroactivo</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Function GetListRetroactive(ByVal RetroactiveInitialDate As Date, GroupId As Integer, Optional employeeId As Integer = 0) As List(Of RetroactiveC)

    Function GetListRetroactiveByYear(VarYear As Integer, GroupId As Integer) As List(Of RetroactiveC)

    Function GetListRetroactiveByRangeOfDates(StartDate As Date, EndDate As Date, GroupId As Integer) As List(Of RetroactiveC)

    Function GetListRetroactiveByEmployeeId(VarYear As Integer, EmployeeId As Integer) As RetroactiveC

    Function GetListRetroactiveByContractId(ContractId As Integer) As List(Of RetroactiveC)

    Function GetListRetroactiveByPayrollDate(PayrollDate As Date, GroupId As Integer) As List(Of RetroactiveC)
End Interface
