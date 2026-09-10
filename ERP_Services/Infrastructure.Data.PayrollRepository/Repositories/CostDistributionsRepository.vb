'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-02-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure

Public Class CostDistributionsRepository
    Inherits GenericRepository(Of CostDistributions)

    Implements ICostDistributionsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Contrato
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="ContractId">ID del Contrato</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateContractId(PayrollDate As Date, ContractId As Integer) As List(Of CostDistributions) Implements ICostDistributionsRepository.ListCostDistributionsByPayrollDateContractId
        Dim CostDistributions = From e In _context.CostDistributions
                                Where e.PayrollDateLiquidated = PayrollDate And e.ContractId = ContractId
                                Select e
        Return CostDistributions.ToList()
    End Function

    ''' <summary>
    ''' Lista la Distribución de la Nómina por Fecha y Id del Grupo
    ''' </summary>
    ''' <param name="PayrollDate">Fecha de Liquidación de Nómina</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function ListCostDistributionsByPayrollDateGroupId(PayrollDate As Date, GroupId As Integer) As List(Of CostDistributions) Implements ICostDistributionsRepository.ListCostDistributionsByPayrollDateGroupId
        Dim CostDistributions = From e In _context.CostDistributions
                                Where e.PayrollDateLiquidated = PayrollDate And e.GroupId = GroupId
                                Select e
        Return CostDistributions.ToList()
    End Function

    ''' <summary>
    ''' Lista la Cuenta Contable por Número
    ''' </summary>
    ''' <param name="NumberAccount">Número de Cuenta</param>
    ''' <returns>MainAccounts</returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountByNumber(NumberAccount As String) As MainAccounts Implements ICostDistributionsRepository.GetMainAccountByNumber
        Dim MainAccounts = From e In _context.MainAccounts.Include("Legalbook")
                           Where e.Number = NumberAccount And e.LegalBook.OfficialBook = True
                           Select e
        Return MainAccounts.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista la Cuenta Contable por Número
    ''' </summary>
    ''' <param name="IdAccount">Id Cuenta Contable
    ''' <returns>MainAccounts</returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountById(IdAccount As String) As MainAccounts Implements ICostDistributionsRepository.GetMainAccountById
        Dim MainAccounts = From e In _context.MainAccounts
                           Where e.Id = IdAccount
                           Select e
        Return MainAccounts.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista el Tipo de Documento por Código
    ''' </summary>
    ''' <param name="Code">Code</param>
    ''' <returns>JournalVoucherTypes</returns>
    ''' <remarks></remarks>
    Public Function GetJournalVoucherTypes(Code As String) As JournalVoucherTypes Implements ICostDistributionsRepository.GetJournalVoucherTypes
        Dim JournalVoucherTypes = From e In _context.JournalVoucherTypes
                                  Where e.Code = Code
                                  Select e
        Return JournalVoucherTypes.FirstOrDefault()
    End Function


    Public Function GetContractAccountingData(IdContract As Integer) As Contract Implements ICostDistributionsRepository.GetContractAccountingData
        Dim Contract = From e In _context.Contract.Include("FunctionalUnit").Include("FunctionalUnit.AccountingStructure").Include("FunctionalUnit.AccountingStructure.ConceptAccountingStructure").Include("FundContract")
                       Where e.Id = IdContract
                       Select e

        Return Contract.FirstOrDefault()
    End Function

    Function SP_GenerateJournalVouchers(groupId As Integer, payrollEndDate As Date, payrollIntegration As Integer, codeUser As String) As List(Of SP_GenerateJournalVouchers_Result) Implements ICostDistributionsRepository.SP_GenerateJournalVouchers
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVouchers(groupId, payrollEndDate, payrollIntegration, codeUser).ToList()
    End Function

End Class
