'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 02-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ITransferOrderDetailBatchSerialRepository
    Inherits IRepository(Of TransferOrderDetailBatchSerial)

    ''' <summary>
    ''' lista los detalles del detalle de la orden de traslado por id de la orden de traslado
    ''' </summary>
    ''' <param name="transferOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId As Integer, flagQuantiyZero As Boolean) As List(Of TransferOrderDetailBatchSerial)

    ''' <summary>
    ''' obtiene un detalle del detalle de orden de traslado por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderDetailBatchSerialById(id As Integer) As TransferOrderDetailBatchSerial

End Interface
