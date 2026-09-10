'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAccountReceivableAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableById(idAccountReceivable As Integer) As AccountReceivable
    ''' <summary>
    ''' obtiene una factura por numero y por id del cliente
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idCustomer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable
    ''' <summary>
    ''' Guarda una cuenta por cobrar
    ''' </summary>
    Function SaveAccountReceivable(accountReceivable As AccountReceivable, audit As AuditMessage, Optional idSequence As Long = 0, Optional assignInvoiceNumber As Boolean = False) As ActionResult(Of AccountReceivable)

    ''' <summary>
    ''' obtiene la Cuenta por Cobrar por Código
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByCode(ByVal Code As String) As AccountReceivable
    ''' <summary>
    ''' obtiene cuenta por cobrar teniendo en cuenta el numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idCustomer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As AccountReceivable

End Interface
