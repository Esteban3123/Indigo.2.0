'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IConsignmentInventoryRemissionDetailRepository
    Inherits IRepository(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionDetailById(Id As Integer, Optional withBatchs As Boolean = False) As ConsignmentInventoryRemissionDetail

    ''' <summary>
    ''' lista el detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailByIdConsignmentInventoryRemission(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' Lista la remisison de entrada por proveedor y linea de destribución
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(SupplierId As Integer, SupplierDistributionLineId As Integer) As List(Of ConsignmentInventoryRemissionDetail)

End Interface