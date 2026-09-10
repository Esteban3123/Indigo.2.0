'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService
    ''' <summary>
    ''' obtine una la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableById(idAccountReceivable As Object) As Domain.Entities.AccountReceivable Implements IPortfolioServiceAccountReceivable.GetAccountReceivableById
        Using service As IAccountReceivableAdminService = Container.Current.Resolve(Of IAccountReceivableAdminService)()
            Return service.GetAccountReceivableById(idAccountReceivable)
        End Using
        'Return _accountReceivableAdminService.GetAccountReceivableById(idAccountReceivable)
    End Function

    Public Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As Domain.Entities.AccountReceivable Implements IPortfolioServiceAccountReceivable.GetAccountReceivableByInvoiceNumberAndCustomer
        Using service As IAccountReceivableAdminService = Container.Current.Resolve(Of IAccountReceivableAdminService)()
            Return service.GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber, idCustomer)
        End Using
        'Return _accountReceivableAdminService.GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber, idCustomer)
    End Function


    ''' <summary>
    ''' obtine una Cuenta por Cobrar teniendo en cuenta el Código de la misma
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAccountReceivableByCode(ByVal Code As String) As Domain.Entities.AccountReceivable Implements IPortfolioServiceAccountReceivable.GetAccountReceivableByCode
        Using service As IAccountReceivableAdminService = Container.Current.Resolve(Of IAccountReceivableAdminService)()
            Return service.GetAccountReceivableByCode(Code)
        End Using
        'Return _accountReceivableAdminService.GetAccountReceivableByCode(Code)
    End Function

    ''' <summary>
    ''' obtine una Cuenta por Cobrar teniendo en cuenta el Número de Factura
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As Domain.Entities.AccountReceivable Implements IPortfolioServiceAccountReceivable.GetAccountReceivableByInvoiceNumber
        Using service As IAccountReceivableAdminService = Container.Current.Resolve(Of IAccountReceivableAdminService)()
            Return service.GetAccountReceivableByInvoiceNumber(invoiceNumber)
        End Using
        'Return Me._accountReceivableAdminService.GetAccountReceivableByInvoiceNumber(invoiceNumber)
    End Function

End Class
