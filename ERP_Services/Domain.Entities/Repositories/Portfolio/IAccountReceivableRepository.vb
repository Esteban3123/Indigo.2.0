'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountReceivableRepository
    Inherits IRepository(Of AccountReceivable)



    Function GetAccountReceivableByAdminssionNumberAndAccountReceivableType(invoiceNumber As String, AccountReceivableType As Integer) As AccountReceivable

    Function GetSharePortfolioNote(thirdPartyId As Integer, invoiceNumber As String, nature As Integer, shareNumber As Integer) As AccountReceivableShare

    Function GetBillPortfolioNote(thirdPartyId As Integer, invoiceNumber As String, nature As Integer, Optional IsPrivate As Boolean = False, Optional MainAccountId As Integer = 0) As AccountReceivableAccounting

    ''' <summary>
    ''' obtiene una factura por numero y por id del cliente
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idCustomer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable
    ''' <summary>
    ''' obtener la cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableById(idAccountReceivable As Integer) As AccountReceivable
    ''' <summary>
    ''' metodo para obtener las facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumber(invoiceNumber As String, idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As List(Of AccountReceivable)
    ''' <summary>
    ''' obtiene una factura por numero. Metodo usuado en la radicacion de cuentas de cobro. Glosas
    ''' </summary>
    ''' <param name="invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumberGloss(ByVal invoicenumber As String) As AccountReceivable
    ''' <summary>
    ''' Obtiene el saldo de factura de cuentas por cobrar
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadBalance(ByVal invoiceNumber As String) As Decimal
    ''' <summary>
    ''' obtiene una factura por tercero y numero
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumberAndThirdPartyId(invoiceNumber As String, ThirdPartyId As Integer) As AccountReceivable
    ''' <summary>
    ''' obtener la Cuenta por Cobrar por Código
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByCode(ByVal Code As String) As AccountReceivable
    ''' <summary>
    ''' obtener la Cuenta por Cobrar por Número De Factura
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As AccountReceivable

    ''' <summary>
    ''' Gets the account receivable by invoice identifier.
    ''' </summary>
    ''' <param name="p1">The p1.</param>
    ''' <returns></returns>
    Function GetAccountReceivableByInvoiceId(invoiceId As Integer) As List(Of AccountReceivable)

    Function GetAccountReceivableByInvoiceNumberSimple(ByVal invoiceNumber As String) As AccountReceivable

    Function GetListAccountReceivable(ByVal listInvoiceNumber As List(Of String)) As List(Of AccountReceivable)

    ''' <summary>
    ''' metodo para establecer un rango de facturas para guardar
    ''' </summary>
    ''' <param name="listAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveListAccountReceivable(listAccountReceivable As List(Of AccountReceivable)) As List(Of AccountReceivable)

    Function GetAccountReceivableByInvoiceIdAndAccountReceivableType(invoiceId As Integer, accountReceivableType As Integer) As List(Of AccountReceivable)

    ''' <summary>
    ''' Obtiene una cuenta contable por el numero
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    Function GetMainAccountByNumber(Number As String) As MainAccounts
    Function GetAccountByInvoiceNumberAndAccountReceivableType(invoiceNumber As String, accountReceivableType As Byte()) As AccountReceivable

    ''' <summary>
    ''' Obtiene el cliente por id del tercero
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Function GetCustomerIdByThirdPartyId(ThirdPartyId As Integer) As Integer?

    ''' <summary>
    ''' Obtiene el codigo cufe de una factura por el Id de la cuenta por cobrar
    ''' </summary>
    ''' <param name="AccountReceivableId"></param>
    ''' <returns></returns>
    Function GetCUFEByAccountReceivableId(AccountReceivableId As String) As String

    ''' <summary>
    ''' Obtiene la cxc por id de AccountReceivableAccounting
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetAccountReceivableByAccountReceivableAccountingId(id As Integer) As AccountReceivable

End Interface
