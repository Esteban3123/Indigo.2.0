'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IDocumentInvoiceProductSalesRepository
    Inherits IRepository(Of DocumentInvoiceProductSales)
    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentInvoiceProductSalesByCode(code As String) As DocumentInvoiceProductSales
    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentInvoiceProductSalesById(id As Integer) As DocumentInvoiceProductSales
End Interface
