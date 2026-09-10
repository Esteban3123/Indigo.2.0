'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-07-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IFixedPercentageRetentionRepository

    Inherits IRepository(Of FixedPercentageRetention)

    ''' <summary>
    ''' Función que obtiene el FixedPercentageRetention
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="ValidStarDate">ValidStarDate</param>
    ''' <param name="ValidEndDate">ValidEndDate</param>
    ''' <returns>FixedPercentageRetention</returns>

    Function GetFixedPercentageRetentionByEmployeeDate(EmployeeId As Integer, ValidStarDate As Date, ValidEndDate As Date) As FixedPercentageRetention

End Interface
