'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IPayrollBankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankFileById(id As Integer, session As SessionValues) As BankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankFileByCode(code As String, session As SessionValues) As BankFile

    ''' <summary>
    ''' Guarda un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <param name="listBankFileDetailDelete"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile)

    ''' <summary>
    ''' Genera el Archivo Plano de Bancos
    ''' </summary>
    ''' <param name="bankFileId">Id del Archivo</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GenerateBankFile(bankFileId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Guarda y Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <param name="listBankFileDetailDelete"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAndConfirmBankFile(BankFile As BankFile, listBankFileDetailDelete As List(Of Integer), session As SessionValues) As ActionResult(Of BankFile)
    ''' <summary>
    ''' Trae primas para la rejilla en presentacion
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ShowIncentivePayment(Period As Integer, DateLiquidated As DateTime, session As SessionValues) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result)

End Interface
