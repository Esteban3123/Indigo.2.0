'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
'Author           : Angi Camila Duran Vargas
'Created          : 27-07-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ProductInTransitRepository
    Inherits GenericRepository(Of ProductInTransit)
    Implements IProductInTransitRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductInTransitMassiveConfirm(listDocuments As List(Of String)) As List(Of ProductInTransit) Implements IProductInTransitRepository.ListProductInTransitMassiveConfirm
        Return (From re In _context.ProductInTransit Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    Public Function GetProductInTransitByCode(code As String) As ProductInTransit Implements IProductInTransitRepository.GetProductInTransitByCode
        Dim res = (From re In _context.ProductInTransit.Include("Currency") Where re.Code = code Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = res.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = res.SupplierId Select s).FirstOrDefault()
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            res.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = res.WarehouseId Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            res.OriginalValue = (From re In _context.ProductInTransit.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New ProductInTransit
        End If
    End Function

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProductInTransitById(id As Integer) As ProductInTransit Implements IProductInTransitRepository.GetProductInTransitById
        Dim res = (From re In _context.ProductInTransit.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = res.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = res.SupplierId Select s).FirstOrDefault()
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            res.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = res.WarehouseId Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            Return res
        Else
            Return New ProductInTransit
        End If
    End Function

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPETDefaultSettings() As PETDefaultSettings Implements IProductInTransitRepository.GetPETDefaultSettings
        Dim res = (From re In _context.PETDefaultSettings.AsNoTracking()).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New PETDefaultSettings
        End If
    End Function

    ''' <summary>
    ''' Genera el comprobante contable para la remision de entrada
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    'Public Function SP_GenerateJournalVoucherByProductInTransit(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByProductInTransit_Result Implements IProductInTransitRepository.SP_GenerateJournalVoucherByProductInTransit
    '    DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
    '    Return _context.SP_GenerateJournalVoucherByProductInTransit(Id, CodeUser).SingleOrDefault
    'End Function

End Class
