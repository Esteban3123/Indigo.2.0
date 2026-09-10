'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Rafael Eduardo Patiño cabrera
' Created          : 09-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountReceivableAccountingRepository
    Inherits IRepository(Of AccountReceivableAccounting)

    ''' <summary>
    ''' Metodo que retorna Objeto del historico de Estructura de cuentas por cobrar.
    ''' </summary>
    ''' <param name="id">Id del movimiento de la cuenta por cobrar</param>
    ''' <returns>objeto historico cuenta por cobrar</returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableAccountingById(ByVal id As Integer, Optional asTracking As Boolean = True) As AccountReceivableAccounting

    ''' <summary>
    ''' Metodo que retorna Objeto del historico de Estructura de cuentas por cobrar.
    ''' </summary>
    ''' <param name="AccountReceivableId">Id de la cuenta por cobrar</param>
    ''' <param name="MainAccountId">Id de la cuenta contable de acuerdo al proceso</param>
    ''' <returns>objeto historico cuenta por cobrar</returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableAccounting(ByVal AccountReceivableId As Integer, ByVal MainAccountId As Integer) As AccountReceivableAccounting

    ''' <summary>
    ''' Obtener movimiento contable por numero de factura y cuenta contable
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    Function GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(invoiceNumber As String, mainAccountId As Integer, thirdPartyId As Integer) As AccountReceivableAccounting

    ''' <summary>
    ''' Lista de Historico de cuentas por pagar
    ''' </summary>
    ''' <param name="AccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAccountReceivableAccounting(ByVal AccountReceivableId As Integer) As List(Of AccountReceivableAccounting)

    ''' <summary>
    ''' Valida la cabecera de la factura
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateAccountReceivable(InvoiceNumber As String, ThirdPartyId As Integer, Position As Integer) As Tuple(Of Boolean, String)

End Interface
