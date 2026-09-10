'************************************************************
' Assembly         : Domain.Inventory.IPhysicalInventoryRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 11/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports System.Collections.Generic
#End Region

Public Interface IPhysicalInventoryRepository
    Inherits IRepository(Of PhysicalInventory)

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryBarCode(productCode As String, batchCode As String, userId As Integer) As Base.Entities.ActionResult(Of List(Of PhysicalInventory))

    ''' <summary>
    ''' obtiene un inventario fisico por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryById(Id As Integer) As PhysicalInventory
    ''' <summary>
    ''' Obtiene el listado de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListPhysicalInventory(productId As Integer, warehouseId As Integer, Optional isInput As Boolean = False) As List(Of PhysicalInventory)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productId"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Function GetListPhysicalInventoryCustody(patientCode As String, admissionNumber As String, productId As Integer, warehouseId As Integer) As List(Of PhysicalInventoryCustody)
    ''' <summary>
    ''' Obtiene el inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryByProductAndWarehouse(productId As Integer, warehouseId As Integer) As PhysicalInventory
    ''' <summary>
    ''' Obtiene el listado de inventario fisico de un producto 
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListPhysicalInventoryByProduct(productId As Integer) As List(Of PhysicalInventory)
    ''' <summary>
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventory(productId As Integer, warehouseId As Integer) As PhysicalInventory

    ''' <summary>
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico y el lote especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <param name="batchSerialId">Id del lote o serial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryByBatchSerial(productId As Integer, warehouseId As Integer, batchSerialId As Integer) As PhysicalInventory

    ''' <summary>
    ''' Obtiene la cantidad de un producto que hay en el inventario
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuantityByProduct(productId As Integer) As Integer

    ''' <summary>
    ''' Obtiene la cantidad de un producto que en el almacen especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuantityByProductWarehouse(productId As Integer, warehouseId As Integer) As Integer

    ''' <summary>
    ''' Obtiene la cantidad de un producto que en el almacen especifico y del lote especifico
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="warehouseId">Id del almacen</param>
    ''' <param name="batchSerialId">Id del lote</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuantityByProductWarehouseBatchSerial(productId As Integer, warehouseId As Integer, batchSerialId As Integer) As Integer

    ''' <summary>
    ''' lista los inventarios fisicos por un listado de codigo de productos
    ''' </summary>
    ''' <param name="XmlParameters"></param>
    ''' <param name="XmlATCs"></param>
    ''' <returns></returns>
    Function SP_ListPhysicalInventoryByCode(XmlParameters As String, XmlATCs As String) As List(Of PhysicalInventory)

    ''' <summary>
    ''' lista los inventarios fisicos por el id del grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPhysiclaInventoryByGroupId(groupId As Integer) As List(Of PhysicalInventory)

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de confirmar documentos
    ''' </summary>
    ''' <param name="IdDocument"></param>
    ''' <param name="DocumentType"></param>
    ''' <param name="CodeUser"></param>
    ''' <param name="ControlCost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePhysicalInventory(IdDocument As Integer, DocumentType As String, CodeUser As String, ContainerNameCrystal As String, Optional ControlCost As Boolean = False) As SP_PhysicalInventory_Result

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de guardar en el kardex y el afectar el inventario fisico
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="EntityId"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="User"></param>
    ''' <param name="ControlCost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePhysicalInventoryKardex(xml As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String, Optional ControlCost As Boolean = False) As SP_SavePhysicalInventoryKardex_Result

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de guardar en el kardex y el afectar el inventario fisico
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="EntityId"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="User"></param>
    ''' <param name="ControlCost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePhysicalInventoryKardexCustody(xml As String, admissionNumber As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String, Optional ControlCost As Boolean = False) As SP_SavePhysicalInventoryCustodyKardexCustody_Result

    ''' <summary>
    ''' Consulta productos en custodia con saldo por paciente y numero de ingreso
    ''' </summary>
    ''' <param name="patientCode">codigo de paciente</param>
    ''' <param name="admissionNumber">numero de ingreso</param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Function GetProductCustodyByPatientCodeAdmission(patientCode As String, admissionNumber As String) As List(Of SP_ProductCustody_Result)

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="userId"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Function GetPhysicalInventoryCustodyBarCode(productCode As String, batchCode As String, userId As Integer, admissionNumber As String) As Base.Entities.ActionResult(Of List(Of PhysicalInventoryCustody))

    ''' <summary>
    ''' Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Function GetPhysicalInventoryCustodyByWareHouse(admissionNumber As String, wareHouseId As Integer, userId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Function GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode As String, admissionNumber As String, wareHouseId As Integer, userId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody)

    ''' <summary>
    ''' lista los inventarios fisicos por el numero ATC del producto
    ''' </summary>
    ''' <param name="ACTNumber"></param>
    ''' <param name="type"></param>
    ''' <param name="userId"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Function ListPhysicalInventoryCustodyByATCNumber(ACTNumber As String, type As Integer, userId As Integer, admissionNumber As String) As List(Of PhysicalInventoryCustody)
End Interface
