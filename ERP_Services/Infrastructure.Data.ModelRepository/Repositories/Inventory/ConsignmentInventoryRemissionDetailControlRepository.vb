'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ConsignmentInventoryRemissionDetailControlRepository
    Inherits GenericRepository(Of ConsignmentInventoryRemissionDetailControl)
    Implements IConsignmentInventoryRemissionDetailControlRepository

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
    ''' obtiene el control del detalle de un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryRemissionDetailControlById(Id As Integer) As ConsignmentInventoryRemissionDetailControl Implements IConsignmentInventoryRemissionDetailControlRepository.GetConsignmentInventoryRemissionDetailControlById
        Dim consignmentInventoryRemissionDetailControl = (From red In _context.ConsignmentInventoryRemissionDetailControl Where red.Id = Id Select red).FirstOrDefault()
        Return consignmentInventoryRemissionDetailControl
    End Function

    ''' <summary>
    ''' lista el control del detalle de un detalle de la remision por el id del batch serial
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailBatchSerialId"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryRemissionDetailControlByConsignmentInventoryRemissionDetailBatchSerialId(ConsignmentInventoryRemissionDetailBatchSerialId As Integer) As IEnumerable(Of ConsignmentInventoryRemissionDetailControl) Implements IConsignmentInventoryRemissionDetailControlRepository.GetConsignmentInventoryRemissionDetailControlByConsignmentInventoryRemissionDetailBatchSerialId
        Dim consignmentInventoryRemissionDetailBatchSerial = (From cirdbs In _context.ConsignmentInventoryRemissionDetailBatchSerial.AsNoTracking() Where cirdbs.Id = ConsignmentInventoryRemissionDetailBatchSerialId Select cirdbs).FirstOrDefault()
        Return _context.ConsignmentInventoryRemissionDetailControl.Where(Function(cirdc) cirdc.ConsignmentInventoryRemissionDetailId = consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailId And (cirdc.BatchSerialId Is Nothing OrElse (cirdc.BatchSerialId = consignmentInventoryRemissionDetailBatchSerial.BatchSerialId)))
    End Function

    ''' <summary>
    ''' lista el control del detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailId"></param>
    ''' <returns></returns>
    Public Function ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetail(ConsignmentInventoryRemissionDetailId As Integer) As List(Of ConsignmentInventoryRemissionDetailControl) Implements IConsignmentInventoryRemissionDetailControlRepository.ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetail
        Dim res = (From red In _context.ConsignmentInventoryRemissionDetailControl.Include("ConsignmentInventoryRemissionDetail").Include("ConsignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial") Where red.ConsignmentInventoryRemissionDetailId = ConsignmentInventoryRemissionDetailId Select red).ToList()
        For Each item In res
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ConsignmentInventoryRemissionDetail.ProductId Select p).FirstOrDefault()
            item.ConsignmentInventoryRemissionDetail.CodeNameProduct = product.Code + " - " + product.Name
            If item.ConsignmentInventoryRemissionDetail.PurchaseOrderDetailId IsNot Nothing Then
                item.ConsignmentInventoryRemissionDetail.QuantityImport = (From pod In _context.PurchaseOrderDetail.AsNoTracking() Where pod.Id = item.ConsignmentInventoryRemissionDetail.PurchaseOrderDetailId Select pod.OutstandingQuantity).FirstOrDefault()
            End If
            If item.ConsignmentInventoryRemissionDetail.ContractDetailId IsNot Nothing Then
                item.ConsignmentInventoryRemissionDetail.QuantityImport = (From cd In _context.InventoryContractDetail.AsNoTracking() Where cd.Id = item.ConsignmentInventoryRemissionDetail.ContractDetailId Select cd.OutstandingQuantity).FirstOrDefault()
            End If
            For Each itemBatch In item.ConsignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial
                If itemBatch.BatchSerialId IsNot Nothing Then
                    Dim batch = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = itemBatch.BatchSerialId Select bs).FirstOrDefault()
                    itemBatch.CodeBatchSerial = batch.BatchCode
                End If
            Next
        Next
        Return res
    End Function

    ''' <summary>
    ''' lista el control del detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailId"></param>
    ''' <param name="BatchSerialId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetailAndBatchSerialId(ConsignmentInventoryRemissionDetailId As Integer, BatchSerialId As Integer?) As IEnumerable(Of ConsignmentInventoryRemissionDetailControl) Implements IConsignmentInventoryRemissionDetailControlRepository.ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetailAndBatchSerialId
        Return _context.ConsignmentInventoryRemissionDetailControl.Where(Function(cirdc) cirdc.ConsignmentInventoryRemissionDetailId = ConsignmentInventoryRemissionDetailId And (BatchSerialId Is Nothing OrElse (cirdc.BatchSerialId = BatchSerialId)))
    End Function

    ''' <summary>
    ''' Obtiene los gastos masivamente por Id de la tabla  detalle Lote de remision en consignacion
    ''' </summary>
    ''' <param name="listBatchConsigment"></param>
    ''' <returns></returns>
    Public Function GetConsignmentControlByListBatch(listBatchConsigment As List(Of Integer)) As ConsignmentRelationControl Implements IConsignmentInventoryRemissionDetailControlRepository.GetConsignmentControlByListBatch


        Dim queryBatchSerial = (From cirdb In _context.ConsignmentInventoryRemissionDetailBatchSerial.AsNoTracking()
                                Where listBatchConsigment.Contains(cirdb.Id))

        Dim query = (From cirdc In _context.ConsignmentInventoryRemissionDetailControl.AsNoTracking().Where(Function(x) x.MovementType = 2 AndAlso x.QuantityPendingLegalization > 0)
                     Join cird In _context.ConsignmentInventoryRemissionDetail.AsNoTracking() On cirdc.ConsignmentInventoryRemissionDetailId Equals cird.Id
                     Join cirdb In queryBatchSerial On cird.Id Equals cirdb.ConsignmentInventoryRemissionDetailId
                     Where listBatchConsigment.Contains(cirdb.Id) AndAlso (cirdc.BatchSerialId Is Nothing OrElse cirdc.BatchSerialId = cirdb.BatchSerialId)
                     Select New With {
                            .ConsignmentInventoryRemissionDetailControl = cirdc,
                            .ConsignmentInventoryRemissionDetailBatchSerialId = cirdb.Id}).ToList()

        Parallel.ForEach(query, Sub(item)
                                    item.ConsignmentInventoryRemissionDetailControl.ConsignmentInventoryRemissionDetailBatchSerialId = item.ConsignmentInventoryRemissionDetailBatchSerialId
                                End Sub)

        Dim remissionControl = query.Select(Function(x) x.ConsignmentInventoryRemissionDetailControl).ToList()

        Dim remissionBatchSerial As List(Of ConsignmentInventoryRemissionDetailBatchSerial) = queryBatchSerial.ToList()

        Dim consignmentRelationControl = New ConsignmentRelationControl With {.ConsignmentInventoryRemissionDetailBatchSerial = remissionBatchSerial,
                                                                               .ConsignmentInventoryRemissionDetailControl = remissionControl}

        Return consignmentRelationControl
    End Function
End Class