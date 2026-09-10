'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Public Interface IBankFileAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Function GetBankFileById(id As Integer) As BankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    Function GetBankFileByCode(code As String, audit As AuditMessage) As BankFile

    ''' <summary>
    ''' Guarda un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <param name="listBankFileDetailDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BankFile)

    ''' <summary>
    ''' Genera el Archivo Plano de Bancos
    ''' </summary>
    ''' <param name="bankFileId">Id del Archivo</param>
    ''' <param name="session">Id Company</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateBankFile(bankFileId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <returns></returns>
    Function ConfirmBankFile(BankFile As BankFile, session As SessionValues) As ActionResult(Of BankFile)

    ''' <summary>
    ''' Guarda y Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <returns></returns>
    Function SaveAndConfirmBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile)
    ''' <summary>
    ''' Funcion que genera la lista de primas sin confirmada pero no dsipensada en archivo plano
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Function ShowIncentivePayment(Period As Integer, DateLiquidated As DateTime) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result)

End Interface
