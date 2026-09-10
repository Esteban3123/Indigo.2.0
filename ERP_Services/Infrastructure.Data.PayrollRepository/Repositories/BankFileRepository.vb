'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 26-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base

Public Class BankFileRepository
    Inherits GenericRepository(Of BankFile)
    Implements IBankFileRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetBankFileById(id As Integer, Optional AsTracking As Boolean = False) As BankFile Implements IBankFileRepository.GetBankFileById
        Dim res As BankFile = Nothing

        If AsTracking Then
            res = (From bb In _context.BankFile.Include("BankFileDetail").Include("BankFileDetail.Employee") Where bb.Id = id Select bb).FirstOrDefault()
        Else
            res = (From bb In _context.BankFile.AsNoTracking().Include("BankFileDetail").AsNoTracking().Include("BankFileDetail.Employee").AsNoTracking() Where bb.Id = id Select bb).FirstOrDefault()
        End If

        If res IsNot Nothing Then
            Dim thirdParty = (From c In _context.Company.AsNoTracking
                              Join tp In _context.ThirdParty.AsNoTracking()
                                On c.ThirdPartyId Equals tp.Id
                              Where c.Id = res.CompanyId
                              Select tp).FirstOrDefault()

            res.ThirdPartyId = thirdParty.Id
            res.ThirdPartyNit = thirdParty.Nit
            res.ThirdPartyName = thirdParty.Name

            Dim entityBankAccount = (From eba In _context.EntityBankAccounts.AsNoTracking().Include("Bank").AsNoTracking Where eba.Id = res.EntityBankAccountId Select eba).FirstOrDefault()

            res.ExpenseConceptName = (From ec In _context.ExpenseConcepts.AsNoTracking() Where ec.Id = res.ExpenseConceptId Select ec.Description).FirstOrDefault()
            res.MainAccountId = entityBankAccount.IdMainAccount
            res.EntityBankAccountNumber = entityBankAccount.Number
            res.BankName = entityBankAccount.Bank.Name
            res.BankAccountCurrencyId = entityBankAccount.CurrencyId
            res.ExpenseConceptMainAccountId = (From ec In _context.ExpenseConcepts.AsNoTracking() Where ec.Id = res.ExpenseConceptId Select ec.IdMainAccount).FirstOrDefault()

            Return res
        End If

        Return New BankFile
    End Function

    ''' <summary>
    ''' obtiene un archivo plano de bancos por id con los datos necesarios para el archivo de banco
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetBankFileByIdForGenerateFile(id As Integer) As BankFile Implements IBankFileRepository.GetBankFileByIdForGenerateFile
        Dim res = (From bb In _context.BankFile.AsNoTracking().Include("BankFileDetail").AsNoTracking() Where bb.Id = id Select bb).FirstOrDefault()

        If res IsNot Nothing Then
            Dim company = (From c In _context.Company.AsNoTracking.Include("ThirdParty").AsNoTracking() Where c.Id = res.CompanyId Select c).FirstOrDefault()

            res.CompanyNit = company.ThirdParty.Nit
            res.CompanyName = company.ThirdParty.Name

            Dim entityBankAccount = (From eba In _context.EntityBankAccounts.AsNoTracking().Include("Bank").AsNoTracking Where eba.Id = res.EntityBankAccountId Select eba).FirstOrDefault()
            Dim bank = entityBankAccount.Bank

            res.EntityBankAccountType = entityBankAccount.Type
            res.EntityBankAccountNumber = entityBankAccount.Number
            res.BankFileCode = bank.BankFileCode
            res.BankCenitCode = bank.CenitCode
            res.VoucherTransactionCode = "0"

            If res.VoucherTransactionId IsNot Nothing Then
                Dim voucher = (From vt In _context.VoucherTransaction.AsNoTracking() Where vt.Id = res.VoucherTransactionId Select vt).FirstOrDefault()
                res.VoucherTransactionCode = voucher.Code
            End If

            'Dictionaries
            Dim dictionaryBank As New Dictionary(Of Integer, Bank)()
            dictionaryBank.Add(bank.Id, bank)

            If res.BankFileDetail IsNot Nothing AndAlso res.BankFileDetail.Count > 0 Then
                For Each detail In res.BankFileDetail
                    Dim thirdParty = (From e In _context.Employee.AsNoTracking()
                                      Join tp In _context.ThirdParty.AsNoTracking()
                                        On e.ThirdPartyId Equals tp.Id
                                      Where e.Id = detail.EmployeeId
                                      Select tp).FirstOrDefault()

                    detail.ThirdPartyIdentificationType = (From p In _context.Person.AsNoTracking() Where p.Id = thirdParty.PersonId Select p.IdentificationType).FirstOrDefault()
                    detail.ThirdPartyNit = thirdParty.Nit
                    detail.ThirdPartyName = thirdParty.Name

                    If Not dictionaryBank.ContainsKey(detail.EmployeeBankId) Then
                        bank = (From b In _context.Bank.AsNoTracking Where b.Id = detail.EmployeeBankId Select b).FirstOrDefault()
                        dictionaryBank.Add(bank.Id, bank)
                    Else
                        bank = dictionaryBank(detail.EmployeeBankId)
                    End If

                    detail.EmployeeBankCenitCode = bank.CenitCode
                    detail.EmployeeBankAchCode = bank.AchCode
                Next
            End If

            Return res
        End If

        Return New BankFile
    End Function

    ''' <summary>
    ''' obtiene un archivo plano de bancos por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetBankFileByCode(code As String) As BankFile Implements IBankFileRepository.GetBankFileByCode
        Dim res = (From bb In _context.BankFile Where bb.Code = code Select bb).FirstOrDefault()
        If res IsNot Nothing Then
            res.CompanyName = (From c In _context.Company.AsNoTracking Where c.Id = res.CompanyId Select c.Nit + " - " + c.Name).FirstOrDefault()
            res.EntityBankAccountNumber = (From eba In _context.EntityBankAccounts.AsNoTracking() Where eba.Id = res.EntityBankAccountId Select eba.Number).FirstOrDefault()
            res.ExpenseConceptName = (From ec In _context.ExpenseConcepts.AsNoTracking() Where ec.Id = res.ExpenseConceptId Select ec.Code + " - " + ec.Description).FirstOrDefault()
            res.OriginalValue = (From pi In _context.BankFile.AsNoTracking() Where pi.Id = res.Id Select pi).FirstOrDefault()

            Return res
        End If
        Return New BankFile
    End Function

    ''' <summary>
    '''  Guarda un archivo plano de bancos
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="ListDeleteString"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveBankFile(EntityXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveBankFile_Result Implements IBankFileRepository.SP_SaveBankFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveBankFile(EntityXml, ListDeleteString(0), codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_ConfirmBankFile(id As Integer, codeUser As String) As SP_ConfirmBankFile_Result Implements IBankFileRepository.SP_ConfirmBankFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmBankFile(id, codeUser).SingleOrDefault
    End Function
    ''' <summary>
    ''' Traemos la lista de primas sin confirmar en Archivo Plano
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Public Function ShowIncentivePayment(Period As Integer, DateLiquidated As Date) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result) Implements IBankFileRepository.ShowIncentivePayment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim res = _context.SP_BankFileIncentivePaymentWithoutConfirm(Period, DateLiquidated).ToList()
        Return res
    End Function

#End Region

End Class
