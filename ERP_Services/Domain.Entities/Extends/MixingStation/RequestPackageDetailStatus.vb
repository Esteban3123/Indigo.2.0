Imports System.Runtime.Serialization

Partial Public Class RequestPackageDetailStatus

    ''' <summary>
    ''' Id del almacen cuando se haga el ajuste del inventario
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InventoryAdjustmentWarehouseId As Integer

    ''' <summary>
    ''' Codigo de la central relacionada a la solicitud del paquete
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CodeMixingStation As String

    ''' <summary>
    ''' Nombre de la central relacionada a la solicitud del paquete
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property NameMixingStation As String

    ''' <summary>
    ''' Nombre del medicamento principal la solicitud del paquete
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property NameMainMedicinePackage As String

    ''' <summary>
    ''' Fecha de vencimiento del lote proveedor
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property SupplierBatchExpirationDate As String


End Class
