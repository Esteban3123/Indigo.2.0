'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DocumentInvoiceProductSalesRepository
    Inherits GenericRepository(Of DocumentInvoiceProductSales)
    Implements IDocumentInvoiceProductSalesRepository

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetDocumentInvoiceProductSalesByCode(code As String) As DocumentInvoiceProductSales Implements IDocumentInvoiceProductSalesRepository.GetDocumentInvoiceProductSalesByCode
        Dim res = (From pi In _context.DocumentInvoiceProductSales.Include("ContractExternalClients")
                   Where pi.Code = code
                   Select pi).FirstOrDefault()

        If res IsNot Nothing Then
            res.NameBillingAuthorization = (From ba In _context.BillingAuthorization.AsNoTracking()
                                            Where ba.Id = res.BillingAuthorizationId
                                            Select ba.Name).FirstOrDefault()

            res.CodeNameFunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking()
                                          Where fu.Id = res.FunctionalUnitId
                                          Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()

            res.NitNameThirdParty = (From tp In _context.ThirdParty.AsNoTracking()
                                     Where tp.Id = res.ThirdPartyId
                                     Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()

            res.CodeNameWareHouse = (From w In _context.Warehouse.AsNoTracking()
                                     Where w.Id = res.WarehouseId
                                     Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()

            If res.BranchOfficeId IsNot Nothing Then
                res.CodeNameBranchOffice = (From x In _context.BranchOffice.AsNoTracking()
                                            Where x.Id = res.BranchOfficeId
                                            Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ConditionSalesId IsNot Nothing Then
                res.CodeNameConditionSales = (From cs In _context.ConditionSales.AsNoTracking()
                                              Where cs.Id = res.ConditionSalesId
                                              Select String.Concat(cs.Code, " - ", cs.Name)).FirstOrDefault()
            End If

            If res.EconomicActivityId IsNot Nothing Then
                res.CodeNameEconomicActivity = (From ea In _context.EconomicActivity.AsNoTracking
                                                Where ea.Id = res.EconomicActivityId
                                                Select String.Concat(ea.Code, " - ", ea.Name)).FirstOrDefault
            End If

            Return res
        End If

        Return New DocumentInvoiceProductSales
    End Function

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetDocumentInvoiceProductSalesById(id As Integer) As DocumentInvoiceProductSales Implements IDocumentInvoiceProductSalesRepository.GetDocumentInvoiceProductSalesById
        Dim res = (From pi In _context.DocumentInvoiceProductSales Where pi.Id = id Select pi).FirstOrDefault()
        If res IsNot Nothing Then
            res.NameBillingAuthorization = (From ba In _context.BillingAuthorization.AsNoTracking() Where ba.Id = res.BillingAuthorizationId Select ba.Name).FirstOrDefault()
            res.CodeNameFunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = res.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
            res.NitNameThirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.ThirdPartyId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
            Dim wareHouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select w).FirstOrDefault()
            res.CodeNameWareHouse = String.Concat(wareHouse.Code, " - ", wareHouse.Name)
            res.OriginalValue = (From pi In _context.DocumentInvoiceProductSales.AsNoTracking() Where pi.Id = id Select pi).FirstOrDefault()
            Return res
        End If
        Return New DocumentInvoiceProductSales
    End Function
End Class
