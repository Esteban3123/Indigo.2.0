'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 25-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ITransferOrderDetailRepository
    Inherits IRepository(Of TransferOrderDetail)

    ''' <summary>
    ''' Obtiene una lista de de detalles de orden de traslado por el almacen o unidad funcional de destino
    ''' </summary>
    ''' <param name="idFilter"></param>
    ''' <param name="orderType"></param>
    ''' <param name="dispatchTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListTransferOrderDetailByTarget(idFilter As Integer, orderType As Integer, dispatchTo As Integer) As List(Of TransferOrderDetail)

    ''' <summary>
    ''' obtiene un detalle de orden de traslado por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderDetailById(id As Integer) As TransferOrderDetail

End Interface
