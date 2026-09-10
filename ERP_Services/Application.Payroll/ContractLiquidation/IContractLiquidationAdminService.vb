Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Interface IContractLiquidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Metodo que devuelve las nominas pagadas por contrato
    ''' </summary>
    ''' <param name="contractId">id del contrato</param>
    ''' <returns>Lista de nominas</returns>
    Function GetPaymentsByContractId(contractId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Metodo que liquida los empleados recibidos en el parametro
    ''' </summary>
    ''' <param name="employeesToLiquidate">Diccionario que contiene el id del empleado como llave del diccionario, 
    ''' y una tupla con los valores de si confirma, la fecha de retiro, y el id del motivo de retiro</param>
    ''' <returns>El listado de mensaje de resultados asociados con la operacion</returns>
    Function LiquidateContracts(employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer)), ByVal session As SessionValues) As List(Of ActionMessageResult(Of ContractLiquidation))

    ''' <summary>
    ''' Metodo que devuelve las nominas pagadas por contrato base
    ''' </summary>
    ''' <param name="baseContractId">id del contrato base</param>
    ''' <returns>Lista de nominas</returns>
    Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Metodo para confirmar una liquidacion de contrato
    ''' </summary>
    ''' <param name="contractLiquidation">liquidacion contrato</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmLiquidationContract(contractLiquidation As ContractLiquidation, ByVal session As SessionValues) As ActionMessageResult

    ''' <summary>
    ''' Metodo para confirmar una lista de liquidacion de contratos
    ''' </summary>
    ''' <param name="ListContractLiquidation">Lista de liquidacion contratos</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmListLiquidationContract(ListContractLiquidation As List(Of ContractLiquidation), audit As AuditMessage) As ActionMessageResult

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer, audit As AuditMessage) As List(Of ContractLiquidation)

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión</param>
    ''' <returns>DataTable con el reporte</returns>
    Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional session As SessionValues = Nothing) As System.Data.DataTable

End Interface
