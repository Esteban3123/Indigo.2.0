Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class InventoryControlDetail

    ''' <summary>
    ''' Descripcion del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Descripcion del producto
    ''' </summary>
    <DataMember()>
    Public Property QuantityPhysical As Integer

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    <DataMember()>
    Public Property WarehouseId As Integer


    ''' <summary>
    ''' id del centro de costo que maneja el almacen
    ''' </summary>
    <DataMember()>
    Public Property WarehouseCostCenterId As Integer?

End Class
