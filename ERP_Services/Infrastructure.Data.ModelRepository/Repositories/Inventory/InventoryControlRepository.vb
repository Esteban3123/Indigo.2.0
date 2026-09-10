'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 14-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Class InventoryControlRepository
    Inherits GenericRepository(Of InventoryControl)
    Implements IInventoryControlRepository


    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    ''' <summary>
    ''' funcion que consulta el inventoryControl por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControl(code As String) As InventoryControl Implements IInventoryControlRepository.GetInventoryControl
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d In Me._context.InventoryControl.Include("InventoryControlDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then
            If res.InventoryControlDetail IsNot Nothing AndAlso res.InventoryControlDetail.Count > 0 Then
                For Each item As InventoryControlDetail In res.InventoryControlDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.ProductCodeName = product.Code + " - " + product.Name

                    Dim batch = (From b In _context.InventoryControlDetailBatchSerial Where item.Id = b.InventoryControlDetailId Select b).ToList()

                    For Each bt In batch
                        If bt.BatchSerialId IsNot Nothing Then
                            bt.BatchSerialCode = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = bs.Id Select bs.BatchCode).FirstOrDefault()
                        End If
                        item.InventoryControlDetailBatchSerial.Add(bt)
                    Next


                    If res.DocumentType = 2 Then
                        Dim physical = (From ph In _context.PhysicalInventory.AsNoTracking() Where ph.ProductId = item.ProductId Select ph).ToList()
                        item.QuantityPhysical = physical.Sum(Function(x) x.Quantity)
                    End If
                Next
            End If
            Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
            res.DescriptionWarehouse = wh.Code + " - " + wh.Name
            res.OriginalValue = res
            Return res
        Else
            Return New InventoryControl
        End If
    End Function

    ''' <summary>
    ''' funcion que consulta el inventoryControl por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlById(id As Integer) As InventoryControl Implements IInventoryControlRepository.GetInventoryControlById
        Dim res = (From d In Me._context.InventoryControl.Include("InventoryControlDetail") Where d.Id.Equals(id) Select d).FirstOrDefault

        If res IsNot Nothing Then
            If res.InventoryControlDetail IsNot Nothing AndAlso res.InventoryControlDetail.Count > 0 Then
                For Each item As InventoryControlDetail In res.InventoryControlDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.ProductCodeName = product.Code + " - " + product.Name

                    Dim batch = (From b In _context.InventoryControlDetailBatchSerial Where item.Id = b.InventoryControlDetailId Select b).ToList()
                    If batch IsNot Nothing Then
                        For Each bt In batch
                            bt.ProductCodeName = item.ProductCodeName
                            If bt.BatchSerialId IsNot Nothing Then
                                Dim bts = (From b In _context.BatchSerial.AsNoTracking() Where b.Id = bt.BatchSerialId Select b).FirstOrDefault()
                                bt.BatchSerialCode = bts.BatchCode
                            End If
                            Select Case bt.Status
                                Case 0
                                    bt.StatusName = "No Aplica"
                                Case 1
                                    bt.StatusName = "No Ajustado"
                                Case 2
                                    bt.StatusName = "Ajustado"
                            End Select
                            item.InventoryControlDetailBatchSerial.Add(bt)
                        Next
                    End If

                    If res.DocumentType = 2 Then
                        Dim physical = (From ph In _context.PhysicalInventory.AsNoTracking() Where ph.ProductId = item.ProductId Select ph).ToList()
                        item.QuantityPhysical = physical.Sum(Function(x) x.Quantity)
                    End If
                Next
            End If
            Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
            res.DescriptionWarehouse = wh.Code + " - " + wh.Name
            'res.OriginalValue = (From d In Me._context.InventoryControl.AsNoTracking() Where d.Id.Equals(id) Select d).FirstOrDefault
            Return res
        Else
            Return New InventoryControl
        End If
    End Function

    ''' <summary>
    ''' funcion que consulta el inventoryControl por id con consultas en tareas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlByIdTask(id As Integer, Optional status As Byte? = Nothing) As InventoryControl Implements IInventoryControlRepository.GetInventoryControlByIdTask
        Dim res = (From d In Me._context.InventoryControl.AsNoTracking()
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault

        Dim invDetail = (From detail In Me._context.InventoryControlDetail.AsNoTracking()
                         Join batch In Me._context.InventoryControlDetailBatchSerial.AsNoTracking() On detail.Id Equals (batch.InventoryControlDetailId)
                         Where detail.InventoryControlId = id AndAlso (status Is Nothing OrElse status = batch.Status)
                         Select detail)?.ToList()

        If res Is Nothing Then
            Return New InventoryControl
        End If

        Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
        res.DescriptionWarehouse = wh.Code + " - " + wh.Name

        If invDetail.Any() Then
            Dim productsIds As List(Of Integer) = invDetail.GroupBy(Function(x) x.ProductId).Select(Function(d) d.Key).ToList()
            Dim batchList As List(Of InventoryControlDetailBatchSerial) = New List(Of InventoryControlDetailBatchSerial)
            Dim productList As List(Of InventoryProduct) = New List(Of InventoryProduct)
            Dim physicalList As List(Of PhysicalInventory) = New List(Of PhysicalInventory)
            Dim batchSerialList As List(Of BatchSerial) = New List(Of BatchSerial)
            Dim detailListIds = invDetail.Select(Function(i) i.Id).ToList()
            Dim currentContainer = ServerSessionValues.Current.CurrentContainer


            Dim batchTask As Task = Task.Run(Sub()
                                                 Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                     batchList = (From b In context1.InventoryControlDetailBatchSerial.AsNoTracking()
                                                                  Where detailListIds.Contains(b.InventoryControlDetailId) AndAlso (status Is Nothing OrElse b.Status = status)
                                                                  Select b)?.ToList()
                                                     If batchList?.Exists(Function(x) x.BatchSerialId IsNot Nothing) Then
                                                         Dim idsBatchs = batchList.FindAll(Function(x) x.BatchSerialId IsNot Nothing).Select(Function(d) d.BatchSerialId.Value).ToList()
                                                         batchSerialList = (From b In context1.BatchSerial.AsNoTracking()
                                                                            Where idsBatchs.Contains(b.Id)
                                                                            Select b).ToList()
                                                     End If
                                                 End Using
                                             End Sub)

            Dim productTask As Task = Task.Run(Sub()
                                                   Using context2 As New GlobalModelUnitOfWork(currentContainer)
                                                       productList = (From p In context2.InventoryProduct.AsNoTracking() Where productsIds.Contains(p.Id) Select p)?.ToList()
                                                       If res.DocumentType = 2 Then
                                                           physicalList = (From ph In context2.PhysicalInventory.AsNoTracking() Where productsIds.Contains(ph.ProductId) Select ph)?.ToList()
                                                       End If
                                                   End Using
                                               End Sub)

            Task.WaitAll(batchTask, productTask)

            If Not batchList.Any() Then
                Return res
            End If

            For Each item As InventoryControlDetail In invDetail
                Dim product = (From p In productList Where item.ProductId = p.Id Select p).FirstOrDefault()
                item.ProductCodeName = product.Code + " - " + product.Name

                Dim batch = (From b In batchList Where item.Id = b.InventoryControlDetailId Select b).ToList()
                If batch IsNot Nothing Then
                    For Each bt In batch
                        bt.ProductCodeName = item.ProductCodeName
                        If bt.BatchSerialId IsNot Nothing Then
                            Dim bts = (From b In batchSerialList Where b.Id = bt.BatchSerialId Select b).FirstOrDefault()
                            bt.BatchSerialCode = bts.BatchCode
                        End If
                        Select Case bt.Status
                            Case 0
                                bt.StatusName = "No Aplica"
                            Case 1
                                bt.StatusName = "No Ajustado"
                            Case 2
                                bt.StatusName = "Ajustado"
                        End Select
                        item.InventoryControlDetailBatchSerial.Add(bt)
                    Next
                End If

                If res.DocumentType = 2 Then
                    Dim physical = (From ph In physicalList Where ph.ProductId = item.ProductId Select ph).ToList()
                    item.QuantityPhysical = physical.Sum(Function(x) x.Quantity)
                End If

                res.InventoryControlDetail.Add(item)
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene Los InventoryControlDetailBatchSerial por InventoryAdjustemtId
    ''' </summary>
    ''' <param name="inventoryAdjustmentId"></param>
    ''' <returns></returns>
    Public Function GetInventoryControlByInventoryAdjustmentId(inventoryAdjustmentId As Integer) As List(Of InventoryControlDetailBatchSerial) Implements IInventoryControlRepository.GetInventoryControlByInventoryAdjustmentId
        If inventoryAdjustmentId = 0 Then
            Throw New ArgumentNullException("inventoryAdjustmentId")
        End If

        Dim objAdjustment = (From x In Me._context.InventoryAdjustment.AsNoTracking() Where x.Id = inventoryAdjustmentId)?.FirstOrDefault()

        If objAdjustment Is Nothing Then
            Throw New ArgumentNullException("InventoryAdjustment")
        End If


        Dim listInventoryControlDetailBatchSerialId = (From iac In Me._context.InventoryAdjustmentControl.AsNoTracking()
                                                       Where iac.InventoryAdjustmentId.Equals(inventoryAdjustmentId)
                                                       Select iac.InventoryControlDetailBatchSerialId)?.ToList()

        Dim res As List(Of InventoryControlDetailBatchSerial) = (From icdb In Me._context.InventoryControlDetailBatchSerial.Include("InventoryControlDetail").AsNoTracking()
                                                                 Where listInventoryControlDetailBatchSerialId.Contains(icdb.Id)
                                                                 Select icdb)?.ToList()

        If res Is Nothing OrElse Not res.Any() Then
            Return New List(Of InventoryControlDetailBatchSerial)
        End If

        Dim inventoryControlId = res?.FirstOrDefault?.InventoryControlDetail?.InventoryControlId
        Dim execptIds = res.Select(Function(d) d.Id)?.ToList()

        Dim res2 As List(Of InventoryControlDetailBatchSerial) = New List(Of InventoryControlDetailBatchSerial)

        If objAdjustment.Status = 1 Then
            res2 = (From icdb In Me._context.InventoryControlDetailBatchSerial.Include("InventoryControlDetail").AsNoTracking()
                    Where icdb.InventoryControlDetail.InventoryControlId = inventoryControlId _
                    AndAlso icdb.Status = 1 AndAlso Not execptIds.Contains(icdb.Id)
                    Select icdb)?.ToList()
        End If
        If res2 Is Nothing Then
            res2 = New List(Of InventoryControlDetailBatchSerial)
        End If

        Dim union = res.Union(res2).ToList()
        Dim productsIds As List(Of Integer) = union.Select(Function(x) x.InventoryControlDetail.ProductId).ToList()
        Dim productList As List(Of InventoryProduct) = New List(Of InventoryProduct)
        Dim physicalList As List(Of PhysicalInventory) = New List(Of PhysicalInventory)
        Dim batchSerialList As List(Of BatchSerial) = New List(Of BatchSerial)
        Dim currentContainer = ServerSessionValues.Current.CurrentContainer

        Dim batchTask As Task = Task.Run(Sub()
                                             Using context1 As New GlobalModelUnitOfWork(currentContainer)
                                                 If union?.Exists(Function(x) x.BatchSerialId IsNot Nothing) Then
                                                     Dim idsBatchs = union.FindAll(Function(x) x.BatchSerialId IsNot Nothing).Select(Function(d) d.BatchSerialId.Value).ToList()
                                                     batchSerialList = (From b In context1.BatchSerial.AsNoTracking()
                                                                        Where idsBatchs.Contains(b.Id)
                                                                        Select b).ToList()
                                                 End If
                                             End Using
                                         End Sub)

        Dim productTask As Task = Task.Run(Sub()
                                               Using context2 As New GlobalModelUnitOfWork(currentContainer)
                                                   productList = (From p In context2.InventoryProduct.AsNoTracking() Where productsIds.Contains(p.Id) Select p)?.ToList()
                                               End Using
                                           End Sub)

        Task.WaitAll(batchTask, productTask)

        For Each item As InventoryControlDetailBatchSerial In union

            item.Selected = execptIds.Contains(item.Id)

            Dim product = productList.Find(Function(x) x.Id = item.InventoryControlDetail.ProductId)
            item.ProductCodeName = product.Code + " - " + product.Name

            If item.BatchSerialId IsNot Nothing Then
                item.BatchSerialCode = batchSerialList.Find(Function(x) x.Id = item.BatchSerialId).BatchCode
            End If
            Select Case item.Status
                Case 0
                    item.StatusName = "No Aplica"
                Case 1
                    item.StatusName = "No Ajustado"
                Case 2
                    item.StatusName = "Ajustado"
            End Select
        Next
        Return union
    End Function

    ''' <summary>
    ''' funcion que consulta el inventoryControlDetail por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlDetailByInventoryControlDetailBatchSerialId(InventoryControlDetailBatchSerialId As Integer) As InventoryControlDetail Implements IInventoryControlRepository.GetInventoryControlDetailByInventoryControlDetailBatchSerialId
        Dim res = (From d In Me._context.InventoryControlDetail
                   Join dbs In Me._context.InventoryControlDetailBatchSerial On d.Id Equals dbs.InventoryControlDetailId
                   Where dbs.Id.Equals(InventoryControlDetailBatchSerialId) Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim ic = (From icontrol In Me._context.InventoryControl.AsNoTracking Where icontrol.Id.Equals(res.InventoryControlId) Select icontrol).FirstOrDefault()
            res.WarehouseId = ic.WarehouseId
            Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where ic.WarehouseId.Equals(w.Id) Select w).FirstOrDefault()
            res.WarehouseCostCenterId = wh.CostCenterId
            Dim batch = (From bt In Me._context.InventoryControlDetailBatchSerial Where bt.InventoryControlDetailId = res.Id Select bt).ToList()
            For Each i In batch
                res.InventoryControlDetailBatchSerial.Add(i)
            Next

            'res.OriginalValue = res
            Return res
        Else
            Return New InventoryControlDetail
        End If
    End Function

    ''' <summary>
    ''' obtyiene el control de inventarios por id sin agregado solo con el original value
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetInventoryControlByIdNoAdded(id As Integer) As InventoryControl Implements IInventoryControlRepository.GetInventoryControlByIdNoAdded
        Dim res = (From ic In _context.InventoryControl Where ic.Id = id Select ic).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From ic In _context.InventoryControl.AsNoTracking() Where ic.Id = id Select ic).FirstOrDefault()
            Return res
        Else
            Return New InventoryControl
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener el control de inventario sin agregados, solo con el original value
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlByCodeNoAdded(code As String) As InventoryControl Implements IInventoryControlRepository.GetInventoryControlByCodeNoAdded
        Dim res = (From ic In _context.InventoryControl Where ic.Code = code Select ic).FirstOrDefault()
        If res IsNot Nothing Then
            Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
            res.DescriptionWarehouse = wh.Code + " - " + wh.Name
            res.OriginalValue = (From ic In _context.InventoryControl.AsNoTracking() Where ic.Code = code Select ic).FirstOrDefault()
            Return res
        Else
            Return New InventoryControl
        End If
    End Function
    ''' <summary>
    ''' lista los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlDetailByInventoryControlId(inventoryControlId As Integer) As List(Of InventoryControlDetail) Implements IInventoryControlRepository.GetInventoryControlDetailByInventoryControlId
        Dim res = (From icd In _context.InventoryControlDetail.Include("InventoryControlDetailBatchSerial") Where icd.InventoryControlId = inventoryControlId Select icd).ToList()
        For Each itemDetail In res
            itemDetail.ProductCodeName = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = itemDetail.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
            For Each itemBatch In itemDetail.InventoryControlDetailBatchSerial
                If itemBatch.BatchSerialId IsNot Nothing Then
                    itemBatch.BatchSerialCode = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = itemBatch.BatchSerialId Select bs.BatchCode).FirstOrDefault()
                End If
            Next
            itemDetail.QuantityPhysical = itemDetail.InventoryControlDetailBatchSerial.Sum(Function(x) x.InventoryQuantity)
        Next
        Return res
    End Function

End Class
