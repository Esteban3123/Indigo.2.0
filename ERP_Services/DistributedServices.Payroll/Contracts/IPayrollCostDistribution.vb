'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.InterfaceERPPayroll

<ServiceContract()> _
Public Interface IPayrollCostDistribution

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Contrato
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="ContractId">ID del Contrato</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListCostDistributionsByPayrollDateContractId(PayrollDate As Date, ContractId As Integer, session As SessionValues) As List(Of CostDistributions)

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Grupo
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListCostDistributionsByPayrollDateGroupId(PayrollDate As Date, GroupId As Integer, session As SessionValues) As List(Of CostDistributions)

    ''' <summary>
    ''' Elimino la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteCostDistribution(ListCostDistributions As List(Of CostDistributions), session As SessionValues) As Boolean

    ''' <summary>
    ''' Almaceno la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveCostDistribution(ListCostDistributions As List(Of CostDistributions), session As SessionValues) As Boolean

    ''' <summary>
    ''' Función que permite generar la Distribución de Gasto
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GenerateCostDistribution(GroupId As Integer, PayrollDate As Date, session As SessionValues) As ActionResult(Of List(Of String))


End Interface
