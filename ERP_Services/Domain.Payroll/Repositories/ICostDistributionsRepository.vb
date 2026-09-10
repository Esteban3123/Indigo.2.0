'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 18-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface ICostDistributionsRepository
    Inherits IRepository(Of CostDistributions)

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
    ''' Lista la Cuenta Contable por Número
    ''' </summary>
    ''' <param name="NumberAccount">Número de Cuenta</param>
    ''' <returns>MainAccounts</returns>
    ''' <remarks></remarks>
    Function GetMainAccountByNumber(NumberAccount As String) As MainAccounts

    ''' <summary>
    ''' Lista el Tipo de Documento por Código
    ''' </summary>
    ''' <param name="Code">Code</param>
    ''' <returns>JournalVoucherTypes</returns>
    ''' <remarks></remarks>
    Function GetJournalVoucherTypes(Code As String) As JournalVoucherTypes

    Function GetContractAccountingData(IdContract As Integer) As Contract

    Function GetMainAccountById(IdAccount As String) As MainAccounts

    ''' <summary>
    ''' Generar los comprobantes contables de nomina
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <param name="payrollEndDate"></param>
    ''' <param name="payrollIntegration"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_GenerateJournalVouchers(groupId As Integer, payrollEndDate As Date, payrollIntegration As Integer, codeUser As String) As List(Of SP_GenerateJournalVouchers_Result)

End Interface
