'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IContractLiquidationRepository
    Inherits IRepository(Of ContractLiquidation)

    Function GetPaymentsByContractId(contractId As Integer) As List(Of Liquidation)

    Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Funcion que obtiene los contratos liquidados de x empleado
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractLiquidationByEmployee(employeeId As Integer) As List(Of ContractLiquidation)

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer) As List(Of ContractLiquidation)

    Function LiquidationEmployeeByDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation)

    Function GetContractLiquidationByContractId(ByVal IdContract As Integer) As ContractLiquidation

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión</param>
    ''' <returns>DataTable con el reporte</returns>
    Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional session As Infrastructure.CrossCutting.Base.SessionValues = Nothing) As System.Data.DataTable

End Interface
