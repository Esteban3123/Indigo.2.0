Imports System.Runtime.Serialization

Partial Public Class CampaignDetailItems

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida de la concentración
    ''' </summary>
    <DataMember()>
    Public Property DeliveredQuantity As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property GroupName As String


    <DataMember>
    Public Property AtcCodeName As String
    <DataMember>
    Public Property SupplyCodeName As String
    <DataMember>
    Public Property ProductCodeName As String

    Public Function ItemByType() As String
        If Me.AtcId IsNot Nothing Then
            Return $"ATC-{Me.AtcId}"
        ElseIf Me.SupplyId IsNot Nothing Then
            Return $"Supplie-{Me.SupplyId}"
        ElseIf Me.InventoryProduct?.SupplieId IsNot Nothing Then
            Return $"Supplie-{Me.InventoryProduct?.SupplieId}"
        End If

        Return $"Product-{Me.ProductId}"
    End Function
End Class
