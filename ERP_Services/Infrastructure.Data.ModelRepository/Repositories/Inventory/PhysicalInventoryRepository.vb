'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 11/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class PhysicalInventoryRepository
    Inherits GenericRepository(Of PhysicalInventory)
    Implements IPhysicalInventoryRepository

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
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventory(productId As Integer, warehouseId As Integer) As PhysicalInventory Implements IPhysicalInventoryRepository.GetPhysicalInventory
        Dim query = (From e In _context.PhysicalInventory
                     Where e.ProductId = productId And e.WarehouseId = warehouseId And e.Quantity > 0
                     Select e)
        If query.Count() > 0 Then
            Return query.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListPhysicalInventoryByProduct(productId As Integer) As List(Of PhysicalInventory) Implements IPhysicalInventoryRepository.GetListPhysicalInventoryByProduct
        Dim res = (From e In _context.PhysicalInventory
                   Where e.ProductId = productId And e.Quantity > 0
                   Select e).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim warehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select wh).FirstOrDefault()
                item.CodeNameWarehouse = warehouse.Code + " - " + warehouse.Name

                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerial.BatchCode
                    item.BatchSerialExpiredDate = batchSerial.ExpirationDate
                End If
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico y el lote especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <param name="batchSerialId">Id del lote o serial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventoryByBatchSerial(productId As Integer, warehouseId As Integer, batchSerialId As Integer) As PhysicalInventory Implements IPhysicalInventoryRepository.GetPhysicalInventoryByBatchSerial
        Dim query = (From e In _context.PhysicalInventory
                     Where e.ProductId = productId And e.WarehouseId = warehouseId And e.BatchSerialId = batchSerialId
                     Select e)
        If query.Count() > 0 Then
            Return query.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    Public Function GetListPhysicalInventory(productId As Integer, warehouseId As Integer, Optional isInput As Boolean = False) As List(Of PhysicalInventory) Implements IPhysicalInventoryRepository.GetListPhysicalInventory
        Dim warehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = warehouseId Select wh).FirstOrDefault()
        Dim res = (From pi In _context.PhysicalInventory Where pi.ProductId = productId AndAlso pi.WarehouseId = warehouseId AndAlso (isInput OrElse pi.Quantity > 0) Select pi).ToList()
        If res.Count > 0 Then
            For Each item In res
                item.CodeNameWarehouse = warehouse.Code + " - " + warehouse.Name

                If String.IsNullOrEmpty(item.CodeNameProduct) Then
                    Dim nameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                    item.CodeNameProduct = nameProduct.Code + " - " + nameProduct.Name
                End If

                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerial.BatchCode
                    item.BatchSerialExpiredDate = batchSerial.ExpirationDate
                End If
            Next
        End If
        Return res
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productId"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Public Function GetListPhysicalInventoryCustody(patientCode As String, admissionNumber As String, productId As Integer, warehouseId As Integer) As List(Of PhysicalInventoryCustody) Implements IPhysicalInventoryRepository.GetListPhysicalInventoryCustody
        Dim warehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = warehouseId Select wh).FirstOrDefault()
        Dim res = (From pi In _context.PhysicalInventoryCustody Where pi.AdmissionNumber IsNot Nothing AndAlso pi.AdmissionNumber = admissionNumber _
                                                                                              AndAlso pi.ProductId = productId AndAlso pi.WarehouseId = warehouseId AndAlso pi.Quantity > 0 Select pi).ToList()
        If res.Count > 0 Then
            For Each item In res
                item.CodeNameWarehouse = warehouse.Code + " - " + warehouse.Name
                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerial.BatchCode
                    item.BatchSerialExpiredDate = batchSerial.ExpirationDate
                End If
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de un producto que hay en el inventario
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQuantityByProduct(productId As Integer) As Integer Implements IPhysicalInventoryRepository.GetQuantityByProduct
        Dim quantity = (From e In _context.PhysicalInventory Where e.ProductId = productId Select New With {.Quantity = e.Quantity}).ToList().Sum(Function(x) x.Quantity)
        Return quantity
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de un producto que en el almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQuantityByProductWarehouse(productId As Integer, warehouseId As Integer) As Integer Implements IPhysicalInventoryRepository.GetQuantityByProductWarehouse
        Dim quantity = (From e In _context.PhysicalInventory Where e.ProductId = productId And e.WarehouseId = warehouseId Select New With {.Quantity = e.Quantity}).ToList().Sum(Function(x) x.Quantity)
        Return quantity
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de un producto que en el almacen especifico y del lote especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <param name="batchSerialId">Id del lote</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQuantityByProductWarehouseBatchSerial(productId As Integer, warehouseId As Integer, batchSerialId As Integer) As Integer Implements IPhysicalInventoryRepository.GetQuantityByProductWarehouseBatchSerial
        Dim quantity = (From e In _context.PhysicalInventory Where e.ProductId = productId And e.WarehouseId = warehouseId And e.BatchSerialId = batchSerialId Select New With {.Quantity = e.Quantity}).ToList().Sum(Function(x) x.Quantity)
        Return quantity
    End Function

    ''' <summary>
    ''' obtiene un inventario fisico por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPhysicalInventoryById(Id As Integer) As PhysicalInventory Implements IPhysicalInventoryRepository.GetPhysicalInventoryById
        Return (From pi In _context.PhysicalInventory Where pi.Id = Id Select pi).FirstOrDefault()
    End Function

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventoryBarCode(productCode As String, batchCode As String, userId As Integer) As ActionResult(Of List(Of PhysicalInventory)) Implements IPhysicalInventoryRepository.GetPhysicalInventoryBarCode
        Dim quantity = 0
        Dim DateNow As Date = Now()
        Dim product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Code = productCode Select p).FirstOrDefault()
        If product Is Nothing Then
            'consulto la tabla productBarcode para saber si existe el producto
            Dim productBarCode = (From pbc In _context.ProductBarcode Where pbc.Barcode = productCode Select pbc).FirstOrDefault()
            If productBarCode Is Nothing Then
                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "El producto " + productCode + " no existe"}
            End If
            product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Id = productBarCode.ProductId Select p).FirstOrDefault()

        End If

        If product.ProductSubGroupId = 0 Then
            Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " no tiene un subgrupo asociado"}
        End If
        If product.ProductSubGroup.HandlesBatch Then
            'valido si el producto maneja lote
            If batchCode Is String.Empty Then
                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " maneja lote y no se asigno"}
            End If
            Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.BatchCode = batchCode And bs.ProductId = product.Id And bs.ExpirationDate >= DateNow Select bs).FirstOrDefault()
            If batchSerial Is Nothing Then
                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "El lote " + batchCode + " no existe"}
            End If
            'consulto el inventario fisico por producto y lote, escojo el primero que tiene aurtorizado el proveedor
            Dim ListphysicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking()
                                         Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                         Join bt In _context.BatchSerial.AsNoTracking() On pi.BatchSerialId Equals bt.Id
                                         Where pi.ProductId = product.Id And bt.BatchCode = batchCode And pi.Quantity > quantity And whu.UserId = userId And bt.ExpirationDate > DateNow Select pi).ToList()
            If ListphysicalInventory.Count > 0 Then

                For Each item In ListphysicalInventory
                    item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                    Dim productPhysical = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                    item.InventoryProduct = productPhysical
                    item.CodeNameProduct = $"{productPhysical.Code} - {productPhysical.Name}"

                    If item.BatchSerialId IsNot Nothing Then
                        Dim batchSerialTmp = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                        item.CodeNameBatchSerial = batchSerialTmp.BatchCode
                        item.BatchSerialExpiredDate = batchSerialTmp.ExpirationDate
                    End If
                Next


                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = True, .ObjectEmbbeded = ListphysicalInventory}
            Else
                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "No se encontro inventario fisico para el producto " + product.Code + " - " + product.Name + " en el lote " + batchCode}
            End If
        Else
            'consulto el inventario fisico por producto y escojo el primero que tiene aurtorizado el proveedor
            Dim ListphysicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking()
                                         Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                         Where pi.ProductId = product.Id And pi.Quantity > quantity And whu.UserId = userId Select pi).ToList()
            If ListphysicalInventory.Count > 0 Then

                For Each item In ListphysicalInventory
                    item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                    item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                    item.BatchSerialExpiredDate = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs.ExpirationDate).FirstOrDefault()
                Next

                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = True, .ObjectEmbbeded = ListphysicalInventory}
            Else
                Return New ActionResult(Of List(Of PhysicalInventory)) With {.StateResult = False, .Message = "No se encontro inventario fisico para el producto " + product.Code + " - " + product.Name}
            End If
        End If

    End Function

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function GetPhysicalInventoryCustodyBarCode(productCode As String, batchCode As String, userId As Integer, admissionNumber As String) As ActionResult(Of List(Of PhysicalInventoryCustody)) Implements IPhysicalInventoryRepository.GetPhysicalInventoryCustodyBarCode
        Dim quantity = 0
        Dim product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Code = productCode Select p).FirstOrDefault()
        If product Is Nothing Then
            'consulto la tabla productBarcode para saber si existe el producto
            Dim productBarCode = (From pbc In _context.ProductBarcode Where pbc.Barcode = productCode Select pbc).FirstOrDefault()
            If productBarCode Is Nothing Then
                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "El producto " + productCode + " no existe"}
            End If
            product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Id = productBarCode.ProductId Select p).FirstOrDefault()

        End If

        If product.ProductSubGroupId = 0 Then
            Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " no tiene un subgrupo asociado"}
        End If
        If product.ProductSubGroup.HandlesBatch Then
            'valido si el producto maneja lote
            If batchCode Is String.Empty Then
                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " maneja lote y no se asigno"}
            End If
            Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.BatchCode = batchCode And bs.ProductId = product.Id Select bs).FirstOrDefault()
            If batchSerial Is Nothing Then
                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "El lote " + batchCode + " no existe"}
            End If
            'consulto el inventario fisico por producto y lote, escojo el primero que tiene aurtorizado el proveedor
            Dim ListphysicalInventory = (From pi In _context.PhysicalInventoryCustody.AsNoTracking()
                                         Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                         Where pi.AdmissionNumber = admissionNumber And pi.ProductId = product.Id And pi.BatchSerialId = batchSerial.Id And pi.Quantity > quantity And whu.UserId = userId Select pi).ToList()
            If ListphysicalInventory.Count > 0 Then

                For Each item In ListphysicalInventory
                    item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                    item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                    If item.BatchSerialId IsNot Nothing Then
                        Dim batchSerialTmp = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                        item.CodeNameBatchSerial = batchSerialTmp.BatchCode
                        item.BatchSerialExpiredDate = batchSerialTmp.ExpirationDate
                    End If
                Next


                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = True, .ObjectEmbbeded = ListphysicalInventory}
            Else
                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "No se encontro inventario fisico para el producto " + product.Code + " - " + product.Name + " en el lote " + batchCode}
            End If
        Else
            'consulto el inventario fisico por producto y escojo el primero que tiene aurtorizado el proveedor
            Dim ListphysicalInventory = (From pi In _context.PhysicalInventoryCustody.AsNoTracking()
                                         Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                         Where pi.AdmissionNumber = admissionNumber And pi.ProductId = product.Id And pi.Quantity > quantity And whu.UserId = userId Select pi).ToList()
            If ListphysicalInventory.Count > 0 Then

                For Each item In ListphysicalInventory
                    item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                    item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                Next

                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = True, .ObjectEmbbeded = ListphysicalInventory}
            Else
                Return New ActionResult(Of List(Of PhysicalInventoryCustody)) With {.StateResult = False, .Message = "No se encontro inventario fisico para el producto " + product.Code + " - " + product.Name}
            End If
        End If

    End Function

    ''' <summary>
    ''' Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function GetPhysicalInventoryCustodyByWareHouse(admissionNumber As String, wareHouseId As Integer, userId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody) Implements IPhysicalInventoryRepository.GetPhysicalInventoryCustodyByWareHouse
        'consulto el inventario fisico por producto y lote, escojo el primero que tiene aurtorizado el proveedor
        Dim ListphysicalInventory = (From pi In _context.PhysicalInventoryCustody.AsNoTracking()
                                     Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                     Where pi.AdmissionNumber = admissionNumber And pi.WarehouseId = wareHouseId And pi.Quantity > 0 And whu.UserId = userId Select pi).ToList()
        If ListphysicalInventory.Count > 0 Then

            For Each item In ListphysicalInventory
                item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerialTmp = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerialTmp.BatchCode
                    item.BatchSerialExpiredDate = batchSerialTmp.ExpirationDate
                End If
            Next
            Return ListphysicalInventory

        Else
            Return New List(Of PhysicalInventoryCustody)
        End If



    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode As String, admissionNumber As String, wareHouseId As Integer, userId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody) Implements IPhysicalInventoryRepository.GetPhysicalInventoryCustodyByAdmissionWareHouse
        'consulto el inventario fisico por producto y lote, escojo el primero que tiene aurtorizado el proveedor
        Dim ListphysicalInventory = (From pi In _context.PhysicalInventoryCustody.AsNoTracking()
                                     Join whu In _context.WarehouseUser.AsNoTracking() On pi.WarehouseId Equals whu.WarehouseId
                                     Where pi.AdmissionNumber IsNot Nothing AndAlso pi.AdmissionNumber = admissionNumber AndAlso pi.WarehouseId = wareHouseId AndAlso pi.Quantity > 0 AndAlso whu.UserId = userId Select pi).ToList()
        If ListphysicalInventory.Count > 0 Then

            For Each item In ListphysicalInventory
                item.CodeNameWarehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select String.Concat(wh.Code, " - ", wh.Name)).FirstOrDefault()
                item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerialTmp = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerialTmp.BatchCode
                    item.BatchSerialExpiredDate = batchSerialTmp.ExpirationDate
                End If
            Next
            Return ListphysicalInventory

        Else
            Return New List(Of PhysicalInventoryCustody)
        End If
    End Function

    ''' <summary>
    ''' lista los inventarios fisicos por un listado de codigo de productos
    ''' </summary>
    ''' <param name="XmlParameters"></param>
    ''' <param name="XmlATCs"></param>
    ''' <returns></returns>
    Public Function SP_ListPhysicalInventoryByCode(XmlParameters As String, XmlATCs As String) As List(Of PhysicalInventory) Implements IPhysicalInventoryRepository.SP_ListPhysicalInventoryByCode
        Dim listPhysicalInventory As New List(Of PhysicalInventory)

        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim result = _context.SP_ListPhysicalInventoryByCode(XmlParameters, XmlATCs).ToList()
        If result IsNot Nothing AndAlso result.Any() Then
            For Each item In result
                listPhysicalInventory.Add(New PhysicalInventory With {
                                            .Id = item.Id,
                                            .Code = item.Code,
                                            .WarehouseId = item.WarehouseId,
                                            .CodeNameWarehouse = item.WarehouseCodeName,
                                            .ProductId = item.ProductId,
                                            .CodeNameProduct = item.ProductCodeName,
                                            .BatchSerialId = item.BatchSerialId,
                                            .CodeNameBatchSerial = item.BatchSerialCode,
                                            .BatchSerialExpiredDate = item.BatchSerialExpirationDate,
                                            .Quantity = item.Quantity,
                                            .ClassType = item.ClassType,
                                            .ClassName = item.ClassName,
                                            .Dose = item.Dose,
                                            .DoseMeasurement = item.DoseMeasurement,
                                            .Covered = item.Covered
                                          })
            Next
        End If

        Return listPhysicalInventory
    End Function

    ''' <summary>
    ''' lista los inventarios fisicos por el numero ATC del producto
    ''' </summary>
    ''' <param name="ACTNumber"></param>
    ''' <param name="type"></param>
    ''' <param name="userId"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function ListPhysicalInventoryCustodyByATCNumber(ACTNumber As String, type As Integer, userId As Integer, admissionNumber As String) As List(Of PhysicalInventoryCustody) Implements IPhysicalInventoryRepository.ListPhysicalInventoryCustodyByATCNumber
        Dim result As New List(Of PhysicalInventoryCustody)

        Dim products = (From p In _context.InventoryProduct.AsNoTracking
                        Group Join atc In _context.ATC.AsNoTracking On atc.Id Equals p.ATCId Into Group
                        From atc In Group.DefaultIfEmpty()
                        Where (type = 1 AndAlso atc.Code = ACTNumber) OrElse (type <> 1 AndAlso p.Code = ACTNumber)
                        Select p).ToList()

        If products.Count > 0 Then
            For Each product In products
                result.AddRange((From pi In _context.PhysicalInventoryCustody.AsNoTracking
                                 Join wu In _context.WarehouseUser.AsNoTracking On pi.WarehouseId Equals wu.WarehouseId
                                 Where pi.ProductId = product.Id AndAlso pi.Quantity > 0 AndAlso wu.UserId = userId AndAlso pi.AdmissionNumber = admissionNumber
                                 Select pi).ToList())
            Next

            If result.Count > 0 Then
                Dim warehouses = (From w In _context.Warehouse.AsNoTracking
                                  Join wu In _context.WarehouseUser.AsNoTracking On w.Id Equals wu.WarehouseId
                                  Where wu.UserId = userId Select w).ToList()

                If warehouses.Count > 0 Then
                    For Each product In products
                        For Each warehouse In warehouses
                            If result.Any(Function(d) d.ProductId = product.Id AndAlso d.WarehouseId = warehouse.Id) Then
                                For Each physicalInventory In result.Where(Function(d) d.ProductId = product.Id AndAlso d.WarehouseId = warehouse.Id)
                                    physicalInventory.CodeNameWarehouse = String.Format("{0} - {1}", warehouse.Code, warehouse.Name)
                                    physicalInventory.CodeNameProduct = String.Format("{0} - {1}", product.Code, product.Name)
                                    physicalInventory.Quantity = If(warehouse.VirtualStore, Integer.MaxValue, physicalInventory.Quantity)
                                Next
                            End If
                        Next
                    Next
                End If
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' lista los inventarios fisicos por el id del grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    Public Function ListPhysiclaInventoryByGroupId(groupId As Integer) As List(Of PhysicalInventory) Implements IPhysicalInventoryRepository.ListPhysiclaInventoryByGroupId
        Dim res = (From pi In _context.PhysicalInventory
                   Join p In _context.InventoryProduct On p.Id Equals pi.ProductId
                   Join sg In _context.ProductSubGroup On sg.Id Equals p.ProductSubGroupId
                   Where sg.Id = groupId Select pi).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of PhysicalInventory)
        End If
    End Function

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que guarda en el inventario fisico
    ''' </summary>
    ''' <param name="IdDocument">id del documento</param>
    ''' <param name="DocumentType">tipo de documento</param>
    ''' <param name="CodeUser">codigo del usuario</param>
    ''' <param name="ControlCost">controlar el costo promedio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePhysicalInventory(IdDocument As Integer, DocumentType As String, CodeUser As String, ContainerNameCrystal As String, Optional ControlCost As Boolean = False) As SP_PhysicalInventory_Result Implements IPhysicalInventoryRepository.SavePhysicalInventory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_PhysicalInventory(IdDocument, DocumentType, CodeUser, ContainerNameCrystal, ControlCost).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de guardar en el kardex y el afectar el inventario fisico
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="EntityId"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="User"></param>
    ''' <param name="ControlCost">controlar el costo promedio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePhysicalInventoryKardex(xml As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String, Optional ControlCost As Boolean = False) As SP_SavePhysicalInventoryKardex_Result Implements IPhysicalInventoryRepository.SavePhysicalInventoryKardex
        Return _context.SP_SavePhysicalInventoryKardex(xml, EntityId, EntityCode, EntityName, User, ControlCost).FirstOrDefault()
    End Function

    Public Function GetPhysicalInventoryByProductAndWarehouse(productId As Integer, warehouseId As Integer) As PhysicalInventory Implements IPhysicalInventoryRepository.GetPhysicalInventoryByProductAndWarehouse
        Dim res = (From pi In _context.PhysicalInventory Where pi.ProductId = productId And pi.WarehouseId = warehouseId Select pi).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim warehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = item.WarehouseId Select wh).FirstOrDefault()
                item.CodeNameWarehouse = warehouse.Code + " - " + warehouse.Name
                If item.BatchSerialId IsNot Nothing Then
                    Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerial.BatchCode
                    item.BatchSerialExpiredDate = batchSerial.ExpirationDate
                End If
            Next
            Return res(0)
        End If
        Return New PhysicalInventory()
    End Function

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de guardar en el kardex y el afectar el inventario fisico
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="EntityId"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="User"></param>
    ''' <param name="ControlCost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePhysicalInventoryKardexCustody(xml As String, admissionNumber As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String, Optional ControlCost As Boolean = False) As SP_SavePhysicalInventoryCustodyKardexCustody_Result Implements IPhysicalInventoryRepository.SavePhysicalInventoryKardexCustody
        Return _context.SP_SavePhysicalInventoryCustodyKardexCustody(xml, admissionNumber, EntityId, EntityCode, EntityName, User, ControlCost).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta productos en custodia con saldo por paciente y numero de ingreso
    ''' </summary>
    ''' <param name="patientCode">codigo de paciente</param>
    ''' <param name="admissionNumber">numero de ingreso</param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Function GetProductCustodyByPatientCodeAdmission(patientCode As String, admissionNumber As String) As List(Of SP_ProductCustody_Result) Implements IPhysicalInventoryRepository.GetProductCustodyByPatientCodeAdmission
        Return _context.SP_ProductCustody(patientCode, admissionNumber).ToList()

    End Function
End Class
