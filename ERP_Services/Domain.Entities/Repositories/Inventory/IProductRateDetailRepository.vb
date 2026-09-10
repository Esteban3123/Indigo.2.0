'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IProductRateDetailRepository
    Inherits IRepository(Of ProductRateDetail)

    Function GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId As Integer, ProductId As List(Of Integer), ServiceDate As Date) As List(Of ProductRateDetail)

    ''' <summary>
    ''' Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
    ''' </summary>
    Function GetProductRateDetailByCareGroupIdProductIdServiceDate(ByVal CareGroupId As Integer, ByVal ProductId As Integer, ByVal ServiceDate As Date) As ProductRateDetail

    Function GetListProductRateDetailByContractExternalClientIdProductIdServiceDate(ContractExternalClientId As Integer, ProductId As Integer, ServiceDate As Date) As ProductRateDetail

    ''' <summary>
    ''' Trae la definicion de la tarifa en base a paquetes de central de mezclas
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="PackageIds"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Function GetListProductRateDetailByCareGroupIdPackageServiceDate(CareGroupId As Integer, PackageIds As List(Of Integer), ServiceDate As Date) As List(Of ProductRateDetail)

    ''' <summary>
    ''' Obtiene el PackageId asociado a un producto terminado (ítem producción)
    ''' consultando la tabla RequestPackageDetailStatus por ProductId
    ''' </summary>
    ''' <param name="productId">Id del producto terminado</param>
    ''' <returns>PackageId si existe, Nothing si no</returns>
    Function GetPackageIdByProductId(productId As Integer) As Integer?

End Interface