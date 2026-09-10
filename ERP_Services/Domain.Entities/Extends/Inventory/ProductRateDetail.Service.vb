Imports System.Runtime.Serialization

Public Class ProductRateDetail

#Region "Properties and Variables"
    ''' <summary>
    ''' Código del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCode As String

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductName As String

    ''' <summary>
    ''' Tipo del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductType As String

    ''' <summary>
    ''' Clase del Producto
    ''' </summary>
    <DataMember()>
    Public Property ProductTypeClass As String

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Codigo y descripcion cups
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CupsCodeName As String

    ''' <summary>
    ''' Codigo  cups
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CupsCode As String

    ''' <summary>
    ''' codigo y descripcion, descripcion relacionada cups
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ContractDesCodeName As String

    ''' <summary>
    ''' Codigo de la descripcion relacionada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ContractDesCode As String

    ''' <summary>
    ''' Producto de control 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ProductControl As Boolean?

    ''' <summary>
    ''' control de precio  
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ProductWithPriceControl As Boolean?

    ''' <summary>
    ''' codigo del paquete 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property PackageCode As String

    ''' <summary>
    ''' nombre del paquete
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property PackageName As String

    <DataMember()>
    Public Property ATCPackageId As Integer?

    ''' <summary>
    ''' Indica la descripcion del tipo de clase del detalle
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RateClassName As String
#End Region

#Region "Functions"
    Public Function GetSalePriceByRateType() As Decimal
        Dim SalePriceRateType As Decimal = 0
        Select Case Me.RateType
            Case 1
                SalePriceRateType = Me.SalesValue
            Case 2
                'porcentaje basado en = 1.Costo promedio ponderado; 2 - Ultimo costo
                If Me.PercentageBasedOn = 1 Then
                    SalePriceRateType = (Me.InventoryProduct.ProductCost * (Me.Percentage / 100)) + (Me.InventoryProduct.ProductCost)
                ElseIf Me.PercentageBasedOn = 2 Then
                    SalePriceRateType = (Me.InventoryProduct.FinalProductCost * (Me.Percentage / 100)) + (Me.InventoryProduct.FinalProductCost)
                End If
        End Select
        Return SalePriceRateType
    End Function
#End Region

End Class
