'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IConsignmentInventoryRemissionDetailControlRepository
    Inherits IRepository(Of ConsignmentInventoryRemissionDetailControl)

    ''' <summary>
    ''' obtiene el control del detalle de un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionDetailControlById(Id As Integer) As ConsignmentInventoryRemissionDetailControl

    ''' <summary>
    ''' lista el control del detalle de un detalle de la remision por el id del batch serial
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailBatchSerialId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionDetailControlByConsignmentInventoryRemissionDetailBatchSerialId(ConsignmentInventoryRemissionDetailBatchSerialId As Integer) As IEnumerable(Of ConsignmentInventoryRemissionDetailControl)

    ''' <summary>
    ''' lista el control del detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetail(ConsignmentInventoryRemissionDetailId As Integer) As List(Of ConsignmentInventoryRemissionDetailControl)

    ''' <summary>
    ''' lista el control del detalle de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionDetailId"></param>
    ''' <param name="BatchSerialId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailControlByIdConsignmentInventoryRemissionDetailAndBatchSerialId(ConsignmentInventoryRemissionDetailId As Integer, BatchSerialId As Integer?) As IEnumerable(Of ConsignmentInventoryRemissionDetailControl)

    ''' <summary>
    ''' funcion para consultar la tabla de gastos y los lotes respectivamente
    ''' </summary>
    ''' <param name="listBatchConsigment"></param>
    ''' <returns></returns>
    Function GetConsignmentControlByListBatch(listBatchConsigment As List(Of Integer)) As ConsignmentRelationControl

End Interface