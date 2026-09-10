'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Payroll.Entities
Imports DistributedServices.Payroll

Partial Class PayrollService
    Implements IPayrollBankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetBankFileById(id As Integer, session As SessionValues) As BankFile Implements IPayrollBankFile.GetBankFileById
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.GetBankFileById(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un archivo plano de bancos por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetBankFileByCode(code As String, session As SessionValues) As BankFile Implements IPayrollBankFile.GetBankFileByCode
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.GetBankFileByCode(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <param name="listBankFileDetailDelete"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile) Implements IPayrollBankFile.SaveBankFile
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.SaveBankFile(BankFile, listBankFileDetailDelete, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Genera el Archivo Plano de Bancos
    ''' </summary>
    ''' <param name="bankFileId">Id del Archivo</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateBankFile(bankFileId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollBankFile.GenerateBankFile
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.GenerateBankFile(bankFileId, session)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <param name="listBankFileDetailDelete"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile) Implements IPayrollBankFile.SaveAndConfirmBankFile
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.SaveAndConfirmBankFile(BankFile, listBankFileDetailDelete, session)
        End Using
    End Function
    ''' <summary>
    ''' Trae las primas para Archivo plano 
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ShowIncentivePayment(Period As Integer, DateLiquidated As Date, session As SessionValues) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result) Implements IPayrollBankFile.ShowIncentivePayment
        Using service As IBankFileAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileAdminService)()
            Return service.ShowIncentivePayment(Period, DateLiquidated)
        End Using
    End Function
End Class
