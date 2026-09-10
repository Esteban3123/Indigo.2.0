'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IPortfolioServiceAccountReceivable
    ''' <summary>
    ''' obtine una cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableById(idAccountReceivable) As AccountReceivable
    ''' <summary>
    ''' obtiene una factura por numero y por id del cliente
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idCustomer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable
    ''' <summary>
    ''' obtine una Cuenta por Cobrar teniendo en cuenta el Código de la misma
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableByCode(ByVal Code As String) As AccountReceivable
    ''' <summary>
    ''' obtine una Cuenta por Cobrar teniendo en cuenta el Número de la Factura
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As AccountReceivable

End Interface
