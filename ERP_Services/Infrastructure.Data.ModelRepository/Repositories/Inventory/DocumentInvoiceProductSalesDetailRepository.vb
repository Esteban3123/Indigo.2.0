'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DocumentInvoiceProductSalesDetailRepository
    Inherits GenericRepository(Of DocumentInvoiceProductSalesDetail)
    Implements IDocumentInvoiceProductSalesDetailRepository


    ''' <summary>
    ''' Contexto 
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto 
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetDocumentInvoiceProductSalesDetailById(Id As Integer) As DocumentInvoiceProductSalesDetail Implements IDocumentInvoiceProductSalesDetailRepository.GetDocumentInvoiceProductSalesDetailById
        Return (From pid In _context.DocumentInvoiceProductSalesDetail Where pid.Id = Id Select pid).FirstOrDefault()
    End Function

    Public Function ListDocumentInvoiceProductSalesDetailByIdDocumentInvoiceProductSales(DocumentInvoiceProductSalesId As Integer, Optional tracking As Boolean = True) As List(Of DocumentInvoiceProductSalesDetail) Implements IDocumentInvoiceProductSalesDetailRepository.ListDocumentInvoiceProductSalesDetailByIdDocumentInvoiceProductSales
        If tracking Then
            Dim res = (From pid In _context.DocumentInvoiceProductSalesDetail.Include("DocumentInvoiceProductSalesDetailBatchSerial") Where pid.DocumentInvoiceProductSalesId = DocumentInvoiceProductSalesId Select pid).ToList()
            For Each item In res
                item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                For Each itembatch In item.DocumentInvoiceProductSalesDetailBatchSerial
                    Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = itembatch.PhysicalInventoryId Select pi).FirstOrDefault()
                    If physicalInventory.BatchSerialId IsNot Nothing Then
                        itembatch.BatchCode = (From b In _context.BatchSerial.AsNoTracking() Where b.Id = physicalInventory.BatchSerialId Select b.BatchCode).FirstOrDefault()
                    End If
                Next
            Next
            Return res
        Else
            Return (From pid In _context.DocumentInvoiceProductSalesDetail.AsNoTracking().Include("DocumentInvoiceProductSalesDetailBatchSerial").AsNoTracking() Where pid.DocumentInvoiceProductSalesId = DocumentInvoiceProductSalesId Select pid).ToList()
        End If
    End Function

    ''' <summary>
    ''' Fucnion para deveolver el productGroup dependiendo del id del porducto.
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function DocumentInvoiceProductSalesEconomicActivity(productId As Integer) As ProductGroup Implements IDocumentInvoiceProductSalesDetailRepository.DocumentInvoiceProductSalesEconomicActivity

        Dim res = (From ip In _context.InventoryProduct
                   Join pg In _context.ProductGroup.AsNoTracking() On pg.Id Equals ip.ProductGroupId
                   Where ip.Id = productId Select pg).FirstOrDefault

        Return res
    End Function
End Class
