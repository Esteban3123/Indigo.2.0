'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionEntranceDetailBatchSerialRepository
    Inherits IRepository(Of RemissionEntranceDetailBatchSerial)


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier As Integer, idSupplierDistributionLine As Integer) As List(Of RemissionEntranceDetailBatchSerial)
    ''' <summary>
    ''' lista los detalles del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="RemissionEntranceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId As Integer) As List(Of RemissionEntranceDetailBatchSerial)
    ''' <summary>
    ''' obtiene una detalla del detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionEntranceDetailBatchSerialById(Id As Integer) As RemissionEntranceDetailBatchSerial

    ''' <summary>
    ''' lista los detalles del detalle de la remision de entrada por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(code As String) As List(Of RemissionEntranceDetailBatchSerial)
End Interface
