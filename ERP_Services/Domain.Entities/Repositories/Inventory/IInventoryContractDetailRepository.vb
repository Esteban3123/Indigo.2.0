'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities


Public Interface IInventoryContractDetailRepository
    Inherits IRepository(Of InventoryContractDetail)

    
    ''' <summary>
    ''' obtiene los detalles del contrato por el id del proveedor  y la linea de distribucion
    ''' </summary>    
    ''' <param name="contractType">tipo de contrato 1 - Fijo, 2 - Variable</param>
    ''' <returns></returns>
    Function GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer, contractType As Integer) As List(Of InventoryContractDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractDetailById(id As Integer) As InventoryContractDetail
End Interface
