'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ConsignmentInventoryRemissionRepository
    Inherits GenericRepository(Of ConsignmentInventoryRemission)
    Implements IConsignmentInventoryRemissionRepository

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
    ''' Obtiene las cantidades en el almacén de consignación
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryQuantities(warehouseId As Integer, productId As Integer) As ConsignmentMovementInventory Implements IConsignmentInventoryRemissionRepository.GetConsignmentInventoryQuantities
        Dim kardexQuantity = _context.Kardex.Where(Function(m) m.WarehouseId = warehouseId AndAlso m.ProductId = productId) _
            .GroupBy(Function(m) New With {Key m.WarehouseId, Key m.ProductId}) _
            .Select(Function(m) m.Where(Function(o) o.MovementType = 1).Sum(Function(o) o.Quantity) - m.Where(Function(o) o.MovementType = 2).Sum(Function(o) o.Quantity)) _
            .FirstOrDefault()

        Dim data = (From c In _context.ConsignmentInventoryRemission
                    Join cd In _context.ConsignmentInventoryRemissionDetail On cd.ConsignmentInventoryRemissionId Equals c.Id
                    Where c.WarehouseId = warehouseId AndAlso cd.ProductId = productId
                    Group cd By ProductWhGrouped = New With {Key cd.ProductId, Key c.WarehouseId} Into Group
                    Select New ConsignmentMovementInventory With {
                        .ProductId = ProductWhGrouped.ProductId,
                        .WarehouseId = ProductWhGrouped.WarehouseId,
                        .MaxLimitQuantity = Group.Sum(Function(m) m.Quantity),
                        .IncrementQuantity = 0,
                        .KardexQuantity = kardexQuantity
                    }).AsNoTracking() _
                    .FirstOrDefault()
        Return data
    End Function


    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryRemissionById(id As Integer) As ConsignmentInventoryRemission Implements IConsignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionById
        Dim consignmentInventoryRemission = (From re In _context.ConsignmentInventoryRemission.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        If consignmentInventoryRemission IsNot Nothing Then
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = consignmentInventoryRemission.SupplierId Select s).FirstOrDefault()
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = consignmentInventoryRemission.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = consignmentInventoryRemission.WarehouseId Select wh).FirstOrDefault()

            consignmentInventoryRemission.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            consignmentInventoryRemission.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            consignmentInventoryRemission.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            consignmentInventoryRemission.Prefix = wareHouse.Prefix

            Return consignmentInventoryRemission
        Else
            Return New ConsignmentInventoryRemission
        End If
    End Function

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsignmentInventoryRemissionByCode(code As String) As ConsignmentInventoryRemission Implements IConsignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionByCode
        Dim consignmentInventoryRemission = (From re In _context.ConsignmentInventoryRemission.Include("Currency").AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
        If consignmentInventoryRemission IsNot Nothing Then
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = consignmentInventoryRemission.SupplierId Select s).FirstOrDefault()
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = consignmentInventoryRemission.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = consignmentInventoryRemission.WarehouseId Select wh).FirstOrDefault()

            consignmentInventoryRemission.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            consignmentInventoryRemission.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            consignmentInventoryRemission.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            consignmentInventoryRemission.Prefix = wareHouse.Prefix
            consignmentInventoryRemission.OriginalValue = (From re In _context.ConsignmentInventoryRemission.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()

            Return consignmentInventoryRemission
        Else
            Return New ConsignmentInventoryRemission
        End If
    End Function

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConsignmentInventoryRemissionMassiveConfirm(listDocuments As List(Of String)) As List(Of ConsignmentInventoryRemission) Implements IConsignmentInventoryRemissionRepository.ListConsignmentInventoryRemissionMassiveConfirm
        Return (From re In _context.ConsignmentInventoryRemission Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    ''' <summary>
    ''' Genera el comprobante contable para remision de inventario en consignación
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByConsignmentInventoryRemission(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByConsignmentInventoryRemission_Result Implements IConsignmentInventoryRemissionRepository.SP_GenerateJournalVoucherByConsignmentInventoryRemission
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByConsignmentInventoryRemission(Id, CodeUser).SingleOrDefault
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "
        DELETE FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial
        WHERE EXISTS (
            SELECT 1
            FROM Inventory.ConsignmentInventoryRemissionDetail cird
            JOIN Inventory.ConsignmentInventoryRemission cir ON cir.Id = cird.ConsignmentInventoryRemissionId
            WHERE cir.Code = @Code AND cir.Status = 1
              AND cird.Id = ConsignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailId
        );

        DELETE FROM Inventory.ConsignmentInventoryRemissionDetail
        WHERE EXISTS (
            SELECT 1
            FROM Inventory.ConsignmentInventoryRemission cir
            WHERE cir.Code = @Code AND cir.Status = 1
              AND cir.Id = ConsignmentInventoryRemissionDetail.ConsignmentInventoryRemissionId
        );

        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

        DELETE FROM Inventory.ConsignmentInventoryRemission
        WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class