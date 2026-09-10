'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Public Interface IDocumentInvoiceProductSalesDetailRepository
    Inherits IRepository(Of DocumentInvoiceProductSalesDetail)
    ''' <summary>
    ''' lista el detalla de la factura
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSalesId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDocumentInvoiceProductSalesDetailByIdDocumentInvoiceProductSales(DocumentInvoiceProductSalesId As Integer, Optional tracking As Boolean = True) As List(Of DocumentInvoiceProductSalesDetail)
    ''' <summary>
    ''' obtiene un detalle de la factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentInvoiceProductSalesDetailById(Id As Integer) As DocumentInvoiceProductSalesDetail
    ''' <summary>
    ''' Lista de los detalles de la factura con actividad economica
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSalesId"></param>
    ''' <returns></returns>
    Function DocumentInvoiceProductSalesEconomicActivity(DocumentInvoiceProductSalesId As Integer) As ProductGroup

End Interface
