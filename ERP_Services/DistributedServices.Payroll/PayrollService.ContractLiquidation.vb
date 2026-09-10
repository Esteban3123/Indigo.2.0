Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Partial Class PayrollService

    ''' <summary>
    ''' Metodo que lista las nominas totalizadas por id de contrato
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="session"></param>
    ''' <returns>Lista de nominas totalizadas</returns>
    Public Function GetPaymentsByContractId(contractId As Integer, session As SessionValues) As List(Of Liquidation) Implements IPayrollContractLiquidation.GetPaymentsByContractId
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.GetPaymentsByContractId(contractId)
        End Using
    End Function


    ''' <summary>
    ''' Metodo que lista las nominas por id de contrato base
    ''' </summary>
    ''' <param name="baseContractId">Id del contrato base</param>
    ''' <param name="session"></param>
    ''' <returns>Lista de nominas totalizadas</returns>
    Public Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer, session As SessionValues) As List(Of Liquidation) Implements IPayrollContractLiquidation.GetLiquidationsPaidByBaseContractId
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.GetLiquidationsPaidByBaseContractId(baseContractId)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que liquida los empleados recibidos en el parametro
    ''' </summary>
    ''' <param name="employeesToLiquidate">Diccionario que contiene el id del empleado como llave del diccionario, 
    ''' y una tupla con los valores de si confirma, la fecha de retiro, y el id del motivo de retiro</param>
    ''' <returns>El listado de mensaje de resultados asociados con la operacion</returns>
    Public Function LiquidateContracts(employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer)), session As SessionValues) As List(Of ActionMessageResult(Of ContractLiquidation)) Implements IPayrollContractLiquidation.LiquidateContracts
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.LiquidateContracts(employeesToLiquidate, session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para confirmar una liquidacion de contrato
    ''' </summary>
    ''' <param name="contractLiquidation">liquidacion contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmLiquidationContract(contractLiquidation As ContractLiquidation, session As SessionValues) As ActionMessageResult Implements IPayrollContractLiquidation.ConfirmLiquidationContract
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.ConfirmLiquidationContract(contractLiquidation, session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para confirmar una lista de liquidacion de contratos
    ''' </summary>
    ''' <param name="ListContractLiquidation">Lista de liquidacion contratos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmListLiquidationContract(ListContractLiquidation As List(Of ContractLiquidation), session As SessionValues) As ActionMessageResult Implements IPayrollContractLiquidation.ConfirmListLiquidationContract
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.ConfirmListLiquidationContract(ListContractLiquidation, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer, session As SessionValues) As List(Of ContractLiquidation) Implements IPayrollContractLiquidation.GetContractLiquidationByMonthAndYear
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.GetContractLiquidationByMonthAndYear(Month, Year, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación de contratos</returns>
    Public Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, employeeId As Integer?, session As SessionValues) As System.Data.DataTable Implements IPayrollContractLiquidation.GetContractLiquidationDetailReport
        Using ContractLiquidationAdmin As IContractLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContractLiquidationAdminService)()
            Return ContractLiquidationAdmin.GetContractLiquidationDetailReport(initialDate, endDate, employeeId, session)
        End Using
    End Function
End Class
