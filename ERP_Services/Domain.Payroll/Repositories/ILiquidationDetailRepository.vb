'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface ILiquidationDetailRepository
    Inherits IRepository(Of LiquidationDetail)

    ''' <summary>
    ''' Funcion para calcular las liquidaciones de un emplado en un rango de fecha
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="initialDate"></param>
    ''' <param name="endDate"></param>
    ''' <returns></returns>
    Function LiquidationDetailBaseVacationByEmployeeDate(EmployeeId As Integer, initialDate As Date, endDate As Date) As List(Of LiquidationDetail)

    Function LiquidationDetailBaseVacation(ContractId As Integer, ListConceptClass As List(Of String), initialDate As Date, endDate As Date) As List(Of LiquidationDetail)

End Interface
