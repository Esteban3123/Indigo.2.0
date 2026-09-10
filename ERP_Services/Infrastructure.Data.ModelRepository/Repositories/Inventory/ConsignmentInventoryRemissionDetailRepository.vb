'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ConsignmentInventoryRemissionDetailRepository
    Inherits GenericRepository(Of ConsignmentInventoryRemissionDetail)
    Implements IConsignmentInventoryRemissionDetailRepository

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

    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryRemissionDetailById(Id As Integer, Optional withBatchs As Boolean = False) As ConsignmentInventoryRemissionDetail Implements IConsignmentInventoryRemissionDetailRepository.GetConsignmentInventoryRemissionDetailById
        Dim ConsignmentInventoryRemissionDetail As ConsignmentInventoryRemissionDetail
        If withBatchs = False Then
            consignmentInventoryRemissionDetail = (From red In _context.ConsignmentInventoryRemissionDetail Where red.Id = Id Select red).FirstOrDefault()
        Else
            consignmentInventoryRemissionDetail = (From red In _context.ConsignmentInventoryRemissionDetail.Include("ConsignmentInventoryRemissionDetailBatchSerial") Where red.Id = Id Select red).FirstOrDefault()
        End If
        If consignmentInventoryRemissionDetail IsNot Nothing Then
            Dim kardex = (From k In _context.Kardex Where k.EntityId = consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionId And k.EntityName = GetType(ConsignmentInventoryRemission).Name And k.ProductId = consignmentInventoryRemissionDetail.ProductId Select k).ToList()
            If kardex Is Nothing  OrElse kardex.Count = 0 Then
                consignmentInventoryRemissionDetail.ValueInKardex = consignmentInventoryRemissionDetail.UnitValue
            ElseIf kardex.Sum(Function(k) k.Quantity) > 0 'HRR BUG4076
                consignmentInventoryRemissionDetail.ValueInKardex = kardex.Sum(Function(k) k.Quantity * k.Value) / kardex.Sum(Function(k) k.Quantity)
            Else
                consignmentInventoryRemissionDetail.ValueInKardex = 0
            End If
        End If

        Return consignmentInventoryRemissionDetail
    End Function

    ''' <summary>
    ''' lista el detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    Public Function ListConsignmentInventoryRemissionDetailByIdConsignmentInventoryRemission(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetail) Implements IConsignmentInventoryRemissionDetailRepository.ListConsignmentInventoryRemissionDetailByIdConsignmentInventoryRemission
        Dim res = (From red In _context.ConsignmentInventoryRemissionDetail.Include("ConsignmentInventoryRemissionDetailBatchSerial") Where red.ConsignmentInventoryRemissionId = ConsignmentInventoryRemissionId Select red).ToList()
        For Each item In res
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            If product.ManufacturerId IsNot Nothing Then
                item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where p.Id = product.ManufacturerId Select p.Name).FirstOrDefault()
            End If
            item.HealthRegistration = product.HealthRegistration
            item.Presentation = product.Presentation
            If item.PurchaseOrderDetailId IsNot Nothing Then
                item.QuantityImport = (From pod In _context.PurchaseOrderDetail.AsNoTracking() Where pod.Id = item.PurchaseOrderDetailId Select pod.OutstandingQuantity).FirstOrDefault()
            End If
            If item.ContractDetailId IsNot Nothing Then
                item.QuantityImport = (From cd In _context.InventoryContractDetail.AsNoTracking() Where cd.Id = item.ContractDetailId Select cd.OutstandingQuantity).FirstOrDefault()
            End If
            If item.ConsignmentInventoryRemissionDetailId IsNot Nothing Then
                item.ConsignmentInventoryRemissionDetailBatchSerialId = item.ConsignmentInventoryRemissionDetailBatchSerial.First().ConsignmentInventoryRemissionDetailBatchSerialId
            End If
            For Each itemBatch In item.ConsignmentInventoryRemissionDetailBatchSerial
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
    Public Function ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(SupplierId As Integer, SupplierDistributionLineId As Integer) As List(Of ConsignmentInventoryRemissionDetail) Implements IConsignmentInventoryRemissionDetailRepository.ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine
        Dim status As Integer = 2
        Dim res = (From red In _context.ConsignmentInventoryRemissionDetail
                   Join re In _context.ConsignmentInventoryRemission On red.ConsignmentInventoryRemissionId Equals re.Id
                   Where re.SupplierId = SupplierId And re.SupplierDistributionLineId = SupplierDistributionLineId And re.Status = status Select red).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim remissionEntrance = (From re In _context.ConsignmentInventoryRemission.AsNoTracking() Where re.Id = item.ConsignmentInventoryRemissionId Select re).FirstOrDefault()
                item.Code = remissionEntrance.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.CodeNameProduct = product.Code + " - " + product.Name
                If product.ManufacturerId IsNot Nothing Then
                    item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where p.Id = product.ManufacturerId Select p.Name).FirstOrDefault()
                End If
                item.HealthRegistration = product.HealthRegistration
                item.Presentation = product.Presentation
            Next
        End If
        Return res
    End Function

End Class