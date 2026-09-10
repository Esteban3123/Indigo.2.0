Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

Public Class TransferOrderRepository
    Inherits GenericRepository(Of TransferOrder)
    Implements ITransferOrderRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

#Region "Methods"

    Public Function GetTransferOrderById(id As Integer) As TransferOrder Implements ITransferOrderRepository.GetTransferOrderById
        Dim res = (From re In _context.TransferOrder.AsNoTracking().Include("TransferOrderDetail").AsNoTracking().Include("TransferOrderDetail.TransferOrderDetailBatchSerial").AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.SourceWarehouseId Select w).FirstOrDefault()
            res.DescriptionSourceWarehouse = warehouse.Code + " - " + warehouse.Name
            res.Prefix = warehouse.Prefix

            If res.TransitWarehouseId IsNot Nothing Then
                warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.TransitWarehouseId Select w).FirstOrDefault()
                res.DescriptionTransitWarehouse = warehouse.Code + " - " + warehouse.Name
            End If

            If res.TargetWarehouseId IsNot Nothing Then
                warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.TargetWarehouseId Select w).FirstOrDefault()
                res.DescriptionTargetWarehouse = warehouse.Code + " - " + warehouse.Name
            End If

            If res.TargetFunctionalUnitId IsNot Nothing Then
                Dim FunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = res.TargetFunctionalUnitId Select fu).FirstOrDefault()
                res.DescriptionTargetFuntionalUnit = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            End If

            If res.AdjustmentConceptId IsNot Nothing Then
                Dim conceptMovement = (From cm In _context.AdjustmentConcept.AsNoTracking() Where cm.Id = res.AdjustmentConceptId Select cm).FirstOrDefault()
                res.DescriptionAdjustmentConcept = conceptMovement.Code + " - " + conceptMovement.Name
            End If

            If res.ThirdPartyId IsNot Nothing Then
                Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.ThirdPartyId Select tp).FirstOrDefault()
                res.DescirptionThirdParty = thirdParty.Nit + " - " + thirdParty.Name
            End If

            Return res
        Else
            Return New TransferOrder
        End If
    End Function

    Public Function GetTransferOrderByCode(code As String) As TransferOrder Implements ITransferOrderRepository.GetTransferOrderByCode
        Dim res = (From re In _context.TransferOrder.
                       Include("TransferOrderDetail").
                       Include("TransferOrderDetail.TransferOrderDetailBatchSerial").
                       Include("TransferOrderDetail.TransferOrderDetailBatchSerial.PhysicalInventory").AsNoTracking()
                   Where re.Code = code Select re).FirstOrDefault()

        If res IsNot Nothing Then
            Dim warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.SourceWarehouseId Select w).FirstOrDefault()
            res.DescriptionSourceWarehouse = warehouse.Code + " - " + warehouse.Name
            res.Prefix = warehouse.Prefix

            If res.TransitWarehouseId IsNot Nothing Then
                warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.TransitWarehouseId Select w).FirstOrDefault()
                res.DescriptionTransitWarehouse = warehouse.Code + " - " + warehouse.Name
            End If

            If res.TargetWarehouseId IsNot Nothing Then
                warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.TargetWarehouseId Select w).FirstOrDefault()
                res.DescriptionTargetWarehouse = warehouse.Code + " - " + warehouse.Name
            End If

            If res.TargetFunctionalUnitId IsNot Nothing Then
                Dim FunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = res.TargetFunctionalUnitId Select fu).FirstOrDefault()
                res.DescriptionTargetFuntionalUnit = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            End If

            If res.AdjustmentConceptId IsNot Nothing Then
                Dim conceptMovement = (From cm In _context.AdjustmentConcept.AsNoTracking() Where cm.Id = res.AdjustmentConceptId Select cm).FirstOrDefault()
                res.DescriptionAdjustmentConcept = conceptMovement.Code + " - " + conceptMovement.Name
            End If

            If res.ThirdPartyId IsNot Nothing Then
                Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.ThirdPartyId Select tp).FirstOrDefault()
                res.DescirptionThirdParty = thirdParty.Nit + " - " + thirdParty.Name
            End If

            If res.TransferOrderDetail?.Any() Then

                Dim dictionaryProduct As New Dictionary(Of Integer, String)
                Dim dictionaryBatchSerial As New Dictionary(Of Integer, String)

                For Each item As TransferOrderDetail In res.TransferOrderDetail

                    If dictionaryProduct.ContainsKey(item.ProductId) Then
                        item.DescriptionProduct = dictionaryProduct(item.ProductId)
                    Else
                        Dim product = (From p In Me._context.InventoryProduct.AsNoTracking()
                                       Where p.Id = item.ProductId
                                       Select p).FirstOrDefault

                        item.DescriptionProduct = product.Code + " - " + product.Name
                        Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking()
                                           Where pu.Id = product.PackagingUnitId
                                           Select pu).FirstOrDefault()

                        item.ConsumptionUnit = packingUnit.Code + " - " + packingUnit.Name
                        item.CostProduct = product.ProductCost
                        dictionaryProduct.Add(item.ProductId, item.DescriptionProduct)
                    End If

                    If item.TransferOrderDetailBatchSerial?.Any() Then
                        For Each itemDetail As TransferOrderDetailBatchSerial In item.TransferOrderDetailBatchSerial
                            If itemDetail.PhysicalInventory.BatchSerialId IsNot Nothing Then
                                If dictionaryBatchSerial.ContainsKey(itemDetail.PhysicalInventory.BatchSerialId) Then
                                    itemDetail.CodeBatchSerial = dictionaryBatchSerial(itemDetail.PhysicalInventory.BatchSerialId)
                                Else
                                    Dim batchSerial = (From bs In Me._context.BatchSerial.AsNoTracking()
                                                       Where bs.Id = itemDetail.PhysicalInventory.BatchSerialId
                                                       Select bs).FirstOrDefault

                                    itemDetail.CodeBatchSerial = batchSerial.BatchCode
                                    dictionaryBatchSerial.Add(itemDetail.PhysicalInventory.BatchSerialId, batchSerial.BatchCode)
                                End If
                            End If

                            If dictionaryProduct.ContainsKey(item.ProductId) Then
                                itemDetail.CodeNameProduct = item.DescriptionProduct
                            End If
                        Next
                    End If
                Next
            End If

            res.OriginalValue = (From re In _context.TransferOrder.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New TransferOrder
        End If
    End Function


    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback

        Dim sql = "

        DELETE FROM Inventory.TransferOrderDetailBatchSerial
        WHERE TransferOrderDetailId IN (
            SELECT tod.Id
            FROM Inventory.TransferOrderDetail tod
            JOIN Inventory.TransferOrder tro ON tod.TransferOrderId = tro.Id
            WHERE tro.Code = @Code AND tro.Status = 1
        );

        DELETE FROM Inventory.TransferOrderDetail
        WHERE TransferOrderId IN (
            SELECT Id
            FROM Inventory.TransferOrder
            WHERE Code = @Code AND Status = 1
        );

        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

        DELETE FROM Inventory.TransferOrder
        WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected

    End Function

    Public Function SP_SaveTransferOrder(transferOrderXml As String, userCode As String) As SP_SaveTransferOrder_Result Implements ITransferOrderRepository.SP_SaveTransferOrder
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveTransferOrder(transferOrderXml, userCode).SingleOrDefault()
    End Function

#End Region

End Class
