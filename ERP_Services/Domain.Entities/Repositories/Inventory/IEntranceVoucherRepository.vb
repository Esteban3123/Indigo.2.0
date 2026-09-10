'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 14-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IEntranceVoucherRepository
    Inherits IRepository(Of EntranceVoucher)
    Inherits IRepositoryRollbackStrategy

    Function ListEntranceVoucherMassiveConfirm(listDocuments As List(Of String)) As List(Of EntranceVoucher)

    ''' <summary>
    ''' Obtiene un EntranceVoucherDetailBatchSerial por IdEntrancevoucher
    ''' </summary>
    ''' <param name="EntranceVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId As Integer) As List(Of EntranceVoucherDetailBatchSerial)

    ''' <summary>
    ''' Obtiene un InventoryContrac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntranceVoucher(code As String) As EntranceVoucher

    ''' <summary>
    ''' Obtiene un InventoryContrac por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntranceVoucherById(id As Integer) As EntranceVoucher

    ''' <summary>
    ''' Genera el comprobante contable para reclasificación remision de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GenerateJournalVoucherByReclassificationRemissionEntrance(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByReclassificationRemissionEntrance_Result

    ''' <summary>
    ''' Valida que la cantidad de los productos que tengan presupuesto asociado al grupo sea igual que la sumatoria de los compromisos
    ''' </summary>
    ''' <param name="listEntranceVoucherDetail"></param>
    ''' <param name="listEntranceVoucherCommitment"></param>
    ''' <returns></returns>
    Function ValidateProductsAndCommitments(freightValue As Decimal, freightIVAValue As Decimal, listEntranceVoucherDetail As List(Of EntranceVoucherDetail), listEntranceVoucherCommitment As List(Of EntranceVoucherCommitment)) As Boolean

    ''' <summary>
    ''' Obtiene el valor con el que se afecto el kardex al momento de confirmar el comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntrancerVoucerValueInKardex(Id As Integer, productId As Integer) As Decimal
End Interface
