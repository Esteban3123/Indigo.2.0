'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICostDistributionDomain
    Inherits IDisposable

    ''' <summary>
    ''' Se crea el objeto de Distribución de Costo
    ''' </summary>
    ''' <param name="PayrollLiquidation">Liquidation</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Function GenerateCostDistribution(PayrollLiquidation As Liquidation, ScheduleEmployee As List(Of CostDistributionCostCenter), IndigoPayrollIntegration As Integer, SessionValues As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of List(Of Entities.CostDistributions))

    Function CreateAccountingByVie(ListCostDistribution As List(Of CostDistributions), Group As Group, indigo As SessionValues, LiquidationConfirm As List(Of Liquidation)) As ActionResult(Of List(Of Domain.Entities.JournalVouchers))

    Function CreateAccountReceivableDocument(ListCostDistributionInabilities As List(Of CostDistributions), Group As Group, indigo As SessionValues) As ActionResult(Of List(Of Domain.Entities.AccountReceivableDocument))

    Function CreateVoucherTransaction(ListLiquidationDetail As List(Of LiquidationDetail), payrollSettings As PayrollSettings, group As Group, indigo As SessionValues) As ActionResult(Of List(Of Domain.Entities.VoucherTransaction))
End Interface
