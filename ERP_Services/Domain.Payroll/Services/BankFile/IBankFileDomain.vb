'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IBankFileDomain
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el codigo del archivo plano segun banco y la liquidacion, para generar el archivo plano
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SelectBank(bankFile As BankFile) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Genera el archivo plano de Bancolombia
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBancolombia(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el archivo plano de AV Villas
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveAvVillas(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el archivo plano de Banco Popular
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBancoPopular(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el archivo plano de Banco BBVA
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBancoBBVA(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el archivo plano de Davivienda
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveDavivienda(bankFile As BankFile) As StringBuilder
    ''' <summary>
    ''' Genera el archivo plano de DaviviendaCr
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveDaviviendaCR(bankFile As BankFile) As StringBuilder
    ''' <summary>
    '''  Genera el archivo plano de Banco Bogota
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBancoBogota(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el archivo plano de Bancolombia SAP
    ''' </summary>
    ''' <param name="bankFile">archivo plano</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBancolombiaSAP(bankFile As BankFile) As StringBuilder

    ''' <summary>
    ''' Genera el detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="bankFile"></param>
    ''' <returns></returns>
    Function CreateVoucherTransaction(bankFile As BankFile) As ActionResult(Of Domain.Entities.VoucherTransaction)

End Interface
