'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.InterfaceERPPayroll

Public Interface ICostDistributionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Contrato
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="ContractId">ID del Contrato</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Function ListCostDistributionsByPayrollDateContractId(PayrollDate As Date, ContractId As Integer) As List(Of CostDistributions)

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Grupo
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Function ListCostDistributionsByPayrollDateGroupId(PayrollDate As Date, GroupId As Integer) As List(Of CostDistributions)

    ''' <summary>
    ''' Elimino la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteCostDistribution(ListCostDistributions As List(Of CostDistributions), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean

    ''' <summary>
    ''' Almaceno la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveCostDistribution(ListCostDistributions As List(Of CostDistributions), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean

    ''' <summary>
    ''' Función para Crear la Distribución del Gasto
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Liquidación</param>
    ''' <returns>ActionResult(Of List(Of CostDistributions))</returns>
    ''' <remarks></remarks>
    Function GenerateCostDistribution(GroupId As Integer, PayrollDate As Date, Indigo As SessionValues) As ActionResult(Of List(Of String))

End Interface
