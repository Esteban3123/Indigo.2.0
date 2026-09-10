'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 26-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IBankFileRepository
    Inherits IRepository(Of BankFile)

    ''' <summary>
    ''' obtiene un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankFileById(id As Integer, Optional AsTracking As Boolean = False) As BankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por id con los datos necesarios para el archivo de banco
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankFileByIdForGenerateFile(id As Integer) As BankFile

    ''' <summary>
    ''' obtiene un archivo plano de bancos por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankFileByCode(code As String) As BankFile

    '' <summary>
    '' Guarda un archivo plano de bancos
    '' </summary>
    '' <param name="EntityXml"></param>
    '' <param name="ListDeleteString"></param>
    '' <param name="codeUser"></param>
    '' <returns></returns>
    Function SP_SaveBankFile(EntityXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveBankFile_Result

    ''' <summary>
    ''' Confirma un archivo plano de bancos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_ConfirmBankFile(id As Integer, codeUser As String) As SP_ConfirmBankFile_Result
    ''' <summary>
    ''' Mostrara las primas liquidadas y no confirmadas en el archivo plano
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Function ShowIncentivePayment(Period As Integer, DateLiquidated As DateTime) As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result)



End Interface
