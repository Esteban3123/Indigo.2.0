'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Jose Luis Rojas
' Created          : 28-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IContractLiquidationDomain
    Inherits IDisposable

    ''' <summary>
    ''' Metodo que liquida el empleado a la fecha de retiro enviada
    ''' </summary>
    ''' <param name="employee">Empleado a liquidar</param>
    ''' <param name="retirementDate">Fecha de retiro</param>
    ''' <returns>Liquidacion del contrato</returns>
    Function LiquidateContract(employee As Employee, retirementDate As Date, retirementReasonId As Integer, ByVal session As SessionValues) As ActionMessageResult(Of ContractLiquidation)

    Function ExecuteContractLiquidation(employee As Employee, retirementDate As Date, retirementReasonId As Integer, ByVal session As SessionValues) As ActionMessageResult(Of ContractLiquidation)

    ''' <summary>
    ''' Función para crear el Comprobante de Egreso de Liquidaciones de Contrato
    ''' </summary>
    ''' <param name="ContractLiquidation">ContractLiquidation</param>
    ''' <param name="indigo">Indigo</param>
    ''' <returns>ActionResult(Of Domain.Entities.VoucherTransaction)</returns>
    Function CreateVoucherTransaction(ContractLiquidation As ContractLiquidation, indigo As SessionValues) As ActionResult(Of Domain.Entities.VoucherTransaction)

    ''' <summary>
    ''' Función para crear el Comprobante Contable de las Liquidaciones de Contrato
    ''' </summary>
    ''' <param name="ListContractLiquidationDetail">ListContractLiquidationDetail</param>
    ''' <param name="Employee">Employee</param>
    ''' <param name="Contract">Contract</param>
    ''' <param name="group">group</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult(Of Domain.Entities.JournalVouchers)</returns>
    Function CreateJournalVoucher(ListContractLiquidationDetail As List(Of ContractLiquidationDetail), Employee As Domain.Payroll.Entities.Employee, Contract As Domain.Payroll.Entities.Contract, group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of List(Of Domain.Entities.JournalVouchers))

    Function LiquidateContractByFormulate(employee As Domain.Payroll.Entities.Employee, retirementDate As Date, retirementReasonId As Integer, session As SessionValues) As ActionMessageResult(Of Entities.ContractLiquidation)

End Interface
