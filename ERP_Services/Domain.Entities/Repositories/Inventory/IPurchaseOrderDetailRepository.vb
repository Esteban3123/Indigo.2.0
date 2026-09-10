'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPurchaseOrderDetailRepository
    Inherits IRepository(Of PurchaseOrderDetail)
    ''' <summary>
    ''' obtiene los detalles de la orden de compra por el id del proveedor  y la linea de distribucion
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer) As List(Of PurchaseOrderDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderDetailById(id As Integer) As PurchaseOrderDetail
End Interface
