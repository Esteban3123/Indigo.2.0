'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RemissionEntranceDetailRepository
    Inherits GenericRepository(Of RemissionEntranceDetail)
    Implements IRemissionEntranceDetailRepository



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
    ''' lista el detalla de la remision
    ''' </summary>
    ''' <param name="idRemissionEntrance"></param>
    ''' <returns></returns>
    Public Function ListRemissionEntranceDetailByIdRemissionEntrance(idRemissionEntrance As Integer) As List(Of RemissionEntranceDetail) Implements IRemissionEntranceDetailRepository.ListRemissionEntranceDetailByIdRemissionEntrance
        Dim res = (From red In _context.RemissionEntranceDetail.Include("RemissionEntranceDetailBatchSerial") Where red.RemissionEntranceId = idRemissionEntrance Select red).ToList()
        For Each item In res
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            If product.ManufacturerId IsNot Nothing Then
                item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
            End If
            item.HealthRegistration = product.HealthRegistration
            item.Presentation = product.Presentation
            If item.PurchaseOrderDetailId IsNot Nothing Then
                item.QuantityImport = (From pod In _context.PurchaseOrderDetail.AsNoTracking() Where pod.Id = item.PurchaseOrderDetailId Select pod.OutstandingQuantity).FirstOrDefault()
            End If
            If item.ContractDetailId IsNot Nothing Then
                item.QuantityImport = (From cd In _context.InventoryContractDetail.AsNoTracking() Where cd.Id = item.ContractDetailId Select cd.OutstandingQuantity).FirstOrDefault()
            End If
            For Each itemBatch In item.RemissionEntranceDetailBatchSerial
                If itemBatch.BatchSerialId IsNot Nothing Then
                    Dim batch = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = itemBatch.BatchSerialId Select bs).FirstOrDefault()
                    itemBatch.CodeBatchSerial = batch.BatchCode
                End If
            Next
        Next
        Return res
    End Function

    ''' <summary>
    ''' Lista la remisison de entrada por 
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(SupplierId As Integer, SupplierDistributionLineId As Integer) As List(Of RemissionEntranceDetail) Implements IRemissionEntranceDetailRepository.ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine
        Dim status As Integer = 2
        Dim res = (From red In _context.RemissionEntranceDetail
                Join re In _context.RemissionEntrance On red.RemissionEntranceId Equals re.Id
                Where re.SupplierId = SupplierId And re.SupplierDistributionLineId = SupplierDistributionLineId And re.Status = status Select red).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim remissionEntrance = (From re In _context.RemissionEntrance.AsNoTracking() Where re.Id = item.RemissionEntranceId Select re).FirstOrDefault()
                item.Code = remissionEntrance.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.CodeNameProduct = product.Code + " - " + product.Name
                If product.ManufacturerId IsNot Nothing Then
                    item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                End If
                item.HealthRegistration = product.HealthRegistration
                item.Presentation = product.Presentation
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRemissionEntranceDetailById(Id As Integer) As RemissionEntranceDetail Implements IRemissionEntranceDetailRepository.GetRemissionEntranceDetailById
        Dim remissionEntranceDetail = (From red In _context.RemissionEntranceDetail Where red.Id = Id Select red).FirstOrDefault()

        If remissionEntranceDetail IsNot Nothing Then
            Dim kardex = (From k In _context.Kardex Where k.EntityId = remissionEntranceDetail.RemissionEntranceId And k.EntityName = GetType(RemissionEntrance).Name And k.ProductId = remissionEntranceDetail.ProductId Select k).ToList()
            If kardex Is Nothing OrElse kardex.Count = 0 Then
                remissionEntranceDetail.ValueInKardex = remissionEntranceDetail.UnitValue
            Else
                remissionEntranceDetail.ValueInKardex = kardex.Sum(Function(k) k.Quantity * k.Value) / kardex.Sum(Function(k) k.Quantity)
            End If
        End If

        Return remissionEntranceDetail
    End Function
End Class
