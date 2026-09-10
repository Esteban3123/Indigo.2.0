'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 09-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IEntranceVoucherDevolutionRepository
    Inherits IRepository(Of EntranceVoucherDevolution)
    Inherits IRepositoryRollbackStrategy

    ''' <summary>
    ''' Obtiene un Entrance Voucher Devolution por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntranceVoucherDevolution(code As String) As EntranceVoucherDevolution

    ''' <summary>
    ''' Obtiene un Entrance Voucher Devolution por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntranceVoucherDevolutionById(id As Integer) As EntranceVoucherDevolution

    ''' <summary>
    ''' Obtiene un inventory product a partir de EntranceVoucherDetailBatchSerialId
    ''' </summary>
    ''' <param name="EntranceVoucherDetailBatchSerialId"></param>
    ''' <returns>"InventoryProduct"</returns>
    ''' <remarks></remarks>
    Function GetInventoryProductByEntranceVoucherDetailBatchSerialId(EntranceVoucherDetailBatchSerialId As Integer) As InventoryProduct

    ''' <summary>
    ''' Obtiene todas las devoluciones confirmadas de un comprobante de entrada
    ''' </summary>
    ''' <param name="EntranceVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntranceVoucherDevolutionsByEntranceVoucherId(EntranceVoucherId As Integer) As List(Of EntranceVoucherDevolution)
End Interface
