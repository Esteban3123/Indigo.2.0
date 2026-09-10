'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class PharmaceuticalDispensingDetailBatchSerialRepository
    Inherits GenericRepository(Of PharmaceuticalDispensingDetailBatchSerial)
    Implements IPharmaceuticalDispensingDetailBatchSerialRepository, Inject

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene los productos para la devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="productType"></param>
    ''' <returns></returns>
    Public Function ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber As String, functionalUnitCode As String, productCode As String, productType As Integer, userId As Integer, Optional batchSerialCode As String = "") As List(Of PharmaceuticalDispensingDetailBatchSerial) Implements IPharmaceuticalDispensingDetailBatchSerialRepository.ListPharmaceuticalDispensingDetailBatchSerialDevolution
        Dim res As List(Of PharmaceuticalDispensingDetailBatchSerial)

        Dim quantity = 0
        If productType = 2 Then 'insumo
            res = (From p In _context.InventoryProduct
                   Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                   Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                   Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                   Join fu In _context.FunctionalUnit On pdd.FunctionalUnitId Equals fu.Id
                   Join ins In _context.InventorySupplie On ins.Id Equals p.SupplieId
                   Where p.Code = productCode And pd.AdmissionNumber = admissionNumber And fu.Code = functionalUnitCode And pddbs.OutstandingQuantity > quantity And pd.Status = 2 _
                       AndAlso (String.IsNullOrEmpty(batchSerialCode) OrElse pddbs.PhysicalInventory.BatchSerial.BatchCode = batchSerialCode)
                   Select pddbs).ToList()

            'si no se obtienen registro de la unidad funcional se traen los de todas
            If res.Count = 0 Then
                res = (From p In _context.InventoryProduct
                       Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                       Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                       Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                       Join ins In _context.InventorySupplie On ins.Id Equals p.SupplieId
                       Where (String.IsNullOrEmpty(batchSerialCode) OrElse pddbs.PhysicalInventory.BatchSerial.BatchCode = batchSerialCode) AndAlso ins.Code = productCode And pd.AdmissionNumber = admissionNumber And pddbs.OutstandingQuantity > quantity And pd.Status = 2 Select pddbs).ToList()
            End If
        Else
            ' medicamentos o medicamento disponible como insumo
            res = (From atc In _context.ATC
                   Join p In _context.InventoryProduct On atc.Id Equals p.ATCId
                   Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                   Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                   Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                   Join fu In _context.FunctionalUnit On pdd.FunctionalUnitId Equals fu.Id
                   Where (String.IsNullOrEmpty(batchSerialCode) OrElse pddbs.PhysicalInventory.BatchSerial.BatchCode = batchSerialCode) AndAlso atc.Code = productCode And pd.AdmissionNumber = admissionNumber And fu.Code = functionalUnitCode And pddbs.OutstandingQuantity > quantity And pd.Status = 2 Select pddbs).ToList()

            'si no se obtienen registro de la unidad funcional se traen los de todas
            If res.Count = 0 Then
                res = (From atc In _context.ATC
                       Join p In _context.InventoryProduct On atc.Id Equals p.ATCId
                       Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                       Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                       Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                       Where (String.IsNullOrEmpty(batchSerialCode) OrElse pddbs.PhysicalInventory.BatchSerial.BatchCode = batchSerialCode) AndAlso atc.Code = productCode And pd.AdmissionNumber = admissionNumber And pddbs.OutstandingQuantity > quantity And pd.Status = 2 Select pddbs).ToList()
            End If

        End If
        For Each item In res
            Dim PharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking().Include("InventoryProduct").AsNoTracking() Where pdd.Id = item.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
            Dim wareHouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = PharmaceuticalDispensingDetail.WarehouseId Select w).FirstOrDefault()
            item.CodeNameWarehouse = String.Concat(wareHouse.Code, " - ", wareHouse.Name)
            item.IdWarehouse = wareHouse.Id
            item.CodeNameProduct = String.Concat(PharmaceuticalDispensingDetail.InventoryProduct.Code, " - ", PharmaceuticalDispensingDetail.InventoryProduct.Name)
            item.ProductId = PharmaceuticalDispensingDetail.InventoryProduct.Id
            item.CodeNameFunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = PharmaceuticalDispensingDetail.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
            Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = If(item.PhysicalInventoryId, item.PhysicalInventoryCustodyId) Select pi).FirstOrDefault()
            If physicalInventory?.BatchSerialId IsNot Nothing Then
                Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs).FirstOrDefault()
                item.CodeNameBatchSerial = batchSerial.BatchCode
                item.DateBatchSerial = batchSerial.ExpirationDate
            End If
            Dim PharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id Select pd).FirstOrDefault()
            item.CodePharmaceuticalDispensing = PharmaceuticalDispensing.Code
            item.DatePharmaceuticalDispensing = PharmaceuticalDispensing.DocumentDate
        Next
        Return res
    End Function

    ''' <summary>
    ''' obtiene los detalle para hacer la devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber As String, productCode As String, batchCode As String, userId As Integer) As ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) Implements IPharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode
        Dim quantity = 0
        Dim product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Code = productCode Select p).FirstOrDefault()
        If product Is Nothing Then
            'consulto la tabla productBarcode para saber si existe el producto
            Dim productBarCode = (From pbc In _context.ProductBarcode Where pbc.Barcode = productCode Select pbc).FirstOrDefault()
            If productBarCode Is Nothing Then
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "El producto " + productCode + " no existe"}
            End If
            product = (From p In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking() Where p.Id = productBarCode.ProductId Select p).FirstOrDefault()

        End If

        If product.ProductSubGroupId = 0 Then
            Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " no tiene un subgrupo asociado"}
        End If

        If product.ProductSubGroup.HandlesBatch Then
            'valido si el producto maneja lote
            If batchCode Is String.Empty Then
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " maneja lote y no se asigno"}
            End If
            Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.ProductId = product.Id AndAlso bs.BatchCode = batchCode Select bs).FirstOrDefault()
            If batchSerial Is Nothing Then
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "El lote " + batchCode + " no existe"}
            End If


            Dim res = (From p In _context.InventoryProduct
                       Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                       Join wu In _context.WarehouseUser On wu.WarehouseId Equals pdd.WarehouseId
                       Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                       Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                       Join pi In _context.PhysicalInventory.AsNoTracking() On pi.Id Equals pddbs.PhysicalInventoryId
                       Where p.Id = product.Id And pd.AdmissionNumber = admissionNumber And pddbs.OutstandingQuantity > quantity And wu.UserId = userId And pd.Status = 2 And pi.BatchSerialId = batchSerial.Id Select pddbs).ToList()

            For Each item In res
                Dim PharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking().Include("InventoryProduct").AsNoTracking() Where pdd.Id = item.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
                item.ProductId = PharmaceuticalDispensingDetail.InventoryProduct.Id
                Dim wareHouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = PharmaceuticalDispensingDetail.WarehouseId Select w).FirstOrDefault()
                item.IdWarehouse = wareHouse.Id
                item.CodeNameProduct = String.Concat(PharmaceuticalDispensingDetail.InventoryProduct.Code, " - ", PharmaceuticalDispensingDetail.InventoryProduct.Name)
                item.ProductId = PharmaceuticalDispensingDetail.InventoryProduct.Id
                item.CodeNameFunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = PharmaceuticalDispensingDetail.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
                Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = item.PhysicalInventoryId Select pi).FirstOrDefault()
                If physicalInventory.BatchSerialId IsNot Nothing Then
                    'Dim batchSerialTmp = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs).FirstOrDefault()
                    item.CodeNameBatchSerial = batchSerial.BatchCode
                    item.DateBatchSerial = batchSerial.ExpirationDate
                End If
                Dim PharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id Select pd).FirstOrDefault()
                item.CodePharmaceuticalDispensing = PharmaceuticalDispensing.Code
                item.DatePharmaceuticalDispensing = PharmaceuticalDispensing.DocumentDate

            Next

            If res.Count > 0 Then
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = True, .ObjectEmbbeded = res}
            Else
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "No se encontraron productos para devolver en el lote " + batchCode}
            End If
        Else
            Dim res = (From p In _context.InventoryProduct
                       Join pdd In _context.PharmaceuticalDispensingDetail On p.Id Equals pdd.ProductId
                       Join wu In _context.WarehouseUser On wu.WarehouseId Equals pdd.WarehouseId
                       Join pd In _context.PharmaceuticalDispensing On pd.Id Equals pdd.PharmaceuticalDispensingId
                       Join pddbs In _context.PharmaceuticalDispensingDetailBatchSerial On pdd.Id Equals pddbs.PharmaceuticalDispensingDetailId
                       Where p.Id = product.Id And pd.AdmissionNumber = admissionNumber And pddbs.OutstandingQuantity > quantity And wu.UserId = userId And pd.Status = 2 Select pddbs).ToList()

            For Each item In res
                Dim PharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking().Include("InventoryProduct").AsNoTracking() Where pdd.Id = item.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
                item.ProductId = PharmaceuticalDispensingDetail.InventoryProduct.Id
                Dim wareHouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = PharmaceuticalDispensingDetail.WarehouseId Select w).FirstOrDefault()
                item.IdWarehouse = wareHouse.Id
                item.CodeNameProduct = String.Concat(PharmaceuticalDispensingDetail.InventoryProduct.Code, " - ", PharmaceuticalDispensingDetail.InventoryProduct.Name)
                item.ProductId = PharmaceuticalDispensingDetail.InventoryProduct.Id
                item.CodeNameFunctionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = PharmaceuticalDispensingDetail.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
                Dim PharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id Select pd).FirstOrDefault()
                item.CodePharmaceuticalDispensing = PharmaceuticalDispensing.Code
                item.DatePharmaceuticalDispensing = PharmaceuticalDispensing.DocumentDate
            Next

            If res.Count > 0 Then
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = True, .ObjectEmbbeded = res}
            Else
                Return New ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial)) With {.StateResult = False, .Message = "No se encontraron productos para devolver"}
            End If
        End If
    End Function

    ''' <summary>
    ''' lista los detalle de las dispensacion para hacer devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber As String) As List(Of PharmaceuticalDispensingDetailBatchSerial) Implements IPharmaceuticalDispensingDetailBatchSerialRepository.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber
        Dim quantity = 0
        Dim res = (From pddbs In _context.PharmaceuticalDispensingDetailBatchSerial
                   Join pdd In _context.PharmaceuticalDispensingDetail On pddbs.PharmaceuticalDispensingDetailId Equals pdd.Id
                   Join pd In _context.PharmaceuticalDispensing On pdd.PharmaceuticalDispensingId Equals pd.Id
                   Where pd.AdmissionNumber = admissionNumber And pddbs.OutstandingQuantity > quantity Select pddbs).ToList()
        For Each item In res

            Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = item.PhysicalInventoryId Select pi).FirstOrDefault()
            If physicalInventory.BatchSerialId IsNot Nothing Then
                item.CodeNameBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
            Dim PharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking() Where pdd.Id = item.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = PharmaceuticalDispensingDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = String.Concat(product.Code, " - ", product.Name)
            item.ProductId = product.Id

            Dim PharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id Select pd).FirstOrDefault()
            item.CodePharmaceuticalDispensing = PharmaceuticalDispensing.Code
            item.DatePharmaceuticalDispensing = PharmaceuticalDispensing.DocumentDate
        Next
        Return res
    End Function


    ''' <summary>
    ''' lista un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalDispensingDetailBatchSerialById(id As Integer) As PharmaceuticalDispensingDetailBatchSerial Implements IPharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialById
        Dim res = (From pddbs In _context.PharmaceuticalDispensingDetailBatchSerial Where pddbs.Id = id Select pddbs).FirstOrDefault()
        If res IsNot Nothing Then
            If res.PhysicalInventoryId IsNot Nothing Then
                Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = res.PhysicalInventoryId Select pi).FirstOrDefault()
                If physicalInventory.BatchSerialId IsNot Nothing Then
                    res.CodeNameBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs.BatchCode).FirstOrDefault()
                End If
            End If

            If res.PhysicalInventoryCustodyId IsNot Nothing Then
                Dim physicalInventoryCustody = (From pi In _context.PhysicalInventoryCustody.AsNoTracking() Where pi.Id = res.PhysicalInventoryCustodyId Select pi).FirstOrDefault()
                If physicalInventoryCustody.BatchSerialId IsNot Nothing Then
                    res.CodeNameBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventoryCustody.BatchSerialId Select bs.BatchCode).FirstOrDefault()
                End If
            End If

            Dim PharmaceuticalDispensingDetail = (From pdd In _context.PharmaceuticalDispensingDetail.AsNoTracking() Where pdd.Id = res.PharmaceuticalDispensingDetailId Select pdd).FirstOrDefault()
            res.IdWarehouse = PharmaceuticalDispensingDetail.WarehouseId
            res.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = PharmaceuticalDispensingDetail.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()

            Dim PharmaceuticalDispensing = (From pd In _context.PharmaceuticalDispensing.AsNoTracking() Where PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = pd.Id Select pd).FirstOrDefault()
            res.CodePharmaceuticalDispensing = PharmaceuticalDispensing.Code
            res.DatePharmaceuticalDispensing = PharmaceuticalDispensing.DocumentDate

            Return res
        Else
            Return New PharmaceuticalDispensingDetailBatchSerial
        End If
    End Function

    ''' <summary>
    ''' Lista por el Id de la dispensación
    ''' </summary>
    ''' <param name="pharmaceuticalDispensingId">Id de la dispensación</param>
    ''' <returns>Lista resultado</returns>
    Public Function GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(pharmaceuticalDispensingId As Integer) As List(Of PharmaceuticalDispensingDetailBatchSerial) Implements IPharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId
        Dim result = (From pdd In _context.PharmaceuticalDispensingDetail Where pdd.PharmaceuticalDispensingId = pharmaceuticalDispensingId Select pdd.Id)

        If result IsNot Nothing Then
            Dim listIds As List(Of Int32) = result.ToList()
            Dim resul1 = (From pddb As PharmaceuticalDispensingDetailBatchSerial In _context.PharmaceuticalDispensingDetailBatchSerial.Include("PhysicalInventory").Include("PhysicalInventory.BatchSerial") Where listIds.Contains(pddb.PharmaceuticalDispensingDetailId) AndAlso pddb.PhysicalInventoryId IsNot Nothing AndAlso pddb.PhysicalInventoryId > 0 Select pddb)
            Dim resul2 = (From pddb As PharmaceuticalDispensingDetailBatchSerial In _context.PharmaceuticalDispensingDetailBatchSerial.Include("PhysicalInventoryCustody").Include("PhysicalInventoryCustody.BatchSerial") Where listIds.Contains(pddb.PharmaceuticalDispensingDetailId) AndAlso pddb.PhysicalInventoryCustodyId IsNot Nothing AndAlso pddb.PhysicalInventoryCustodyId > 0 Select pddb)

            If resul1 IsNot Nothing AndAlso resul1.Count > 0 AndAlso (resul2 Is Nothing OrElse resul2.Count = 0) Then
                Return resul1.ToList()
            ElseIf resul2 IsNot Nothing AndAlso resul2.Count > 0 AndAlso (resul1 Is Nothing OrElse resul1.Count = 0) Then
                Return resul2.ToList()
            ElseIf resul1 IsNot Nothing AndAlso resul1.Count > 0 AndAlso resul2 IsNot Nothing AndAlso resul2.Count > 0 Then
                Return resul1.ToList().Union(resul2).ToList()
            Else
                Return New List(Of PharmaceuticalDispensingDetailBatchSerial)()
            End If
        Else
            Return New List(Of PharmaceuticalDispensingDetailBatchSerial)()
        End If
    End Function
End Class
