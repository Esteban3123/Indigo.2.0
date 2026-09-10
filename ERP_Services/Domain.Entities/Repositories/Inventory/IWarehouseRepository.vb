'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports System.Threading.Tasks
#End Region


Public Interface IWarehouseRepository
    Inherits IRepository(Of Warehouse)

    ''' <summary>
    ''' Obtiene un almacen por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetWarehouse(code As String, Optional tracking As Boolean = True) As Warehouse

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete>
    Function GetWarehouseById(id As Integer) As Warehouse

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetWarehouseByIdAsync(id As Integer) As Task(Of Warehouse)

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteConditionsByWarehouseId(id As Integer)

    Function ListPrefixs() As List(Of String)

    ''' <summary>
    ''' Obtiene la bodega de un proveedor por tipo
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Function GetWarehouseSupplierByType(supplierId As Integer, type As Integer, Optional costCenterId As Integer? = Nothing) As Warehouse


End Interface
