'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 20-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports System.Threading.Tasks

Public Interface IInventoryProductRepository
    Inherits IRepository(Of InventoryProduct)

    ''' <summary>
    ''' Obtiene un producto por codigo (OBSOLETO - Usar GetInventoryProductAsync)
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <Obsolete("Esta función está marcada como obsoleta. Utilice GetInventoryProductAsync en su lugar para mejor rendimiento y funcionalidad asíncrona.", False)>
    Function GetInventoryProduct(ByVal code As String) As InventoryProduct

    ''' <summary>
    ''' NUEVA VERSIÓN OPTIMIZADA Y ASÍNCRONA - Obtiene un producto por código
    ''' </summary>
    ''' <param name="code">Código del producto</param>
    ''' <param name="includeRelatedData">Si debe incluir datos relacionados (descripciones, etc.)</param>
    ''' <param name="tracking">Si debe hacer tracking de cambios</param>
    ''' <returns>Producto encontrado o nuevo InventoryProduct si no existe</returns>
    Function GetInventoryProductAsync(ByVal code As String, Optional includeRelatedData As Boolean = True, Optional tracking As Boolean = True) As Task(Of InventoryProduct)

    ''' <summary>
    ''' Versión ligera asíncrona sin datos relacionados para mejor rendimiento
    ''' </summary>
    ''' <param name="code">Código del producto</param>
    ''' <returns>Producto básico sin descripciones adicionales</returns>
    Function GetInventoryProductLightAsync(ByVal code As String) As Task(Of InventoryProduct)

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductById(id As Integer, Optional tracking As Boolean = True) As InventoryProduct

    ''' <summary>
    ''' Consulta un producto sin includes
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetCleanInventoryProductById(id As Integer, tracking As Boolean) As InventoryProduct

    ''' <summary>
    ''' Obtiene un producto por id con su subgrupo y grupo
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductByIdSimple(id As Integer, Optional tracking As Boolean = True) As InventoryProduct

    ''' <summary>
    ''' Obtiene un producto sin agregados
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductByIdWithoutAggregates(id As Integer) As InventoryProduct

    ''' <summary>
    ''' Obtiene un producto sin agregados
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductByCodeWithoutAggregates(code As String) As InventoryProduct

    ''' <summary>
    ''' metodo para obtener un producto por codigo con un agrergado de subgrupo, para utilizarlo en control de inventario
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks>no tiene original Value</remarks>
    Function GetInventoryProductInventoryControl(code As String) As InventoryProduct


    ''' <summary>
    ''' Obtiene un producto por id con el grupo como agregado
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductByIdWithProducGroup(id As Integer, Optional tracking As Boolean = True) As InventoryProduct

    ''' <summary>
    ''' Obtiene un producto por codigo con su subgrupo y grupo
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <returns></returns>
    Function GetInventoryProductByCodeWithProducGroup(code As String, Optional tracking As Boolean = True) As InventoryProduct

    Function GetInventoryProductByCodeList(listCode As List(Of String)) As List(Of InventoryProduct)

    Function GetFirstProductByATCCode(ATCCode As String) As InventoryProduct

    Function GetFirstProductByTypeProdcutCode(ATCCode As String, TypeProduct As String) As InventoryProduct

    ''' <summary>
    ''' Guarda el producto en las tablas de crystal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveProductInCrystal(data As String) As SP_SaveProductInCrystal_Result

    ''' <summary>
    ''' Se obtiene el tipo de producto por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetClassOfProductTypeById(Id As Integer) As Byte
    Function GetInventoryProductByBarCode(barcode As String) As InventoryProduct

    ''' <summary>
    ''' Guarda el producto
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Function SP_SaveInventoryProduct(Xml As String, UserCode As String, OperatingUnitId As Integer) As SP_SaveInventoryProduct_Result

    ''' <summary>
    ''' Guarda el producto de forma asíncrona
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Function SP_SaveInventoryProductAsync(Xml As String, UserCode As String, OperatingUnitId As Integer) As Task(Of SP_SaveInventoryProduct_Result)

End Interface