'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.InterfaceERPPayroll

Partial Class PayrollService

    Implements IPayrollCostDistribution

    ''' <summary>
    ''' Elimino la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostDistribution(ListCostDistributions As List(Of Domain.Payroll.Entities.CostDistributions), session As SessionValues) As Boolean Implements IPayrollCostDistribution.DeleteCostDistribution
        Using CostDistributionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostDistributionAdminService)()
            Return CostDistributionAdminService.DeleteCostDistribution(ListCostDistributions, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Contrato
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="ContractId">ID del Contrato</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateContractId(PayrollDate As Date, ContractId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.CostDistributions) Implements IPayrollCostDistribution.ListCostDistributionsByPayrollDateContractId
        Using CostDistributionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostDistributionAdminService)()
            Return CostDistributionAdminService.ListCostDistributionsByPayrollDateContractId(PayrollDate, ContractId)
        End Using
    End Function

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Grupo
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateGroupId(PayrollDate As Date, GroupId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.CostDistributions) Implements IPayrollCostDistribution.ListCostDistributionsByPayrollDateGroupId
        Using CostDistributionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostDistributionAdminService)()
            Return CostDistributionAdminService.ListCostDistributionsByPayrollDateGroupId(PayrollDate, GroupId)
        End Using
    End Function

    ''' <summary>
    ''' Almaceno la Distribución del Gasto
    ''' </summary>
    ''' <param name="ListCostDistributions">ListCostDistributions</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveCostDistribution(ListCostDistributions As List(Of Domain.Payroll.Entities.CostDistributions), session As SessionValues) As Boolean Implements IPayrollCostDistribution.SaveCostDistribution
        Using CostDistributionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostDistributionAdminService)()
            Return CostDistributionAdminService.SaveCostDistribution(ListCostDistributions, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función que permite generar la Distribución de Gasto
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateCostDistribution(GroupId As Integer, PayrollDate As Date, session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of String)) Implements IPayrollCostDistribution.GenerateCostDistribution
        Using CostDistributionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICostDistributionAdminService)()
            Return CostDistributionAdminService.GenerateCostDistribution(GroupId, PayrollDate, session)
        End Using
    End Function
End Class
