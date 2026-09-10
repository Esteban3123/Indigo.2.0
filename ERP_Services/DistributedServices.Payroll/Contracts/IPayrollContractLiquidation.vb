'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollContractLiquidation

    ''' <summary>
    ''' Lista todos los Grupos de contratos
    ''' </summary>
    ''' <returns>Lista los grupos de contratos</returns>
    <OperationContract()> _
    Function GetPaymentsByContractId(contractId As Integer, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Metodo que liquida los empleados recibidos en el parametro
    ''' </summary>
    ''' <param name="employeesToLiquidate">Diccionario que contiene el id del empleado como llave del diccionario, 
    ''' y una tupla con los valores de si confirma, la fecha de retiro, y el id del motivo de retiro </param>
    ''' <returns>El listado de mensaje de resultados asociados con la operacion</returns>
    <OperationContract()>
    Function LiquidateContracts(employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer)), session As SessionValues) As List(Of ActionMessageResult(Of ContractLiquidation))

    ''' <summary>
    ''' Lista todos las liquidaciones por contrato base
    ''' </summary>
    ''' <param name="baseContractId"></param>
    ''' <returns>Lista los liquidaciones</returns>
    <OperationContract()> _
    Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Metodo para confirmar una liquidacion de contrato
    ''' </summary>
    ''' <param name="contractLiquidation">liquidacion contrato</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ConfirmLiquidationContract(contractLiquidation As ContractLiquidation, session As SessionValues) As ActionMessageResult

    ''' <summary>
    ''' Metodo para confirmar una lista de liquidacion de contratos
    ''' </summary>
    ''' <param name="ListContractLiquidation">Lista de liquidacion contratos</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ConfirmListLiquidationContract(ListContractLiquidation As List(Of ContractLiquidation), session As SessionValues) As ActionMessageResult

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer, session As SessionValues) As List(Of ContractLiquidation)

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación de contratos</returns>
    <OperationContract()>
    Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, employeeId As Integer?, session As SessionValues) As System.Data.DataTable

End Interface
