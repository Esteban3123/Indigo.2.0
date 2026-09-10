Imports System.Runtime.Serialization
Imports Domain.Entities

Partial Public Class CampaignDetailPicking
    Implements ICUMCampaign

    <DataMember()>
    Public Property ProductFullName As String Implements ICUMCampaign.ProductFullName

    <DataMember()>
    Public Property WareHouseFullName As String Implements ICUMCampaign.WareHouseFullName

    Private _expirationDate As Date?
    <DataMember()>
    Public Property ExpirationDate As Date? Implements ICUMCampaign.ExpirationDate
        Get
            If _expirationDate Is Nothing Then
                _expirationDate = _batchSerial?.ExpirationDate
            End If

            Return _expirationDate
        End Get
        Set(value As Date?)
            _expirationDate = value
        End Set
    End Property

    <DataMember()>
    Public Property BatchSerialCode As String Implements ICUMCampaign.BatchSerialCode

    <DataMember()>
    Public Property AvailableQuantity As Integer Implements ICUMCampaign.AvailableQuantity

    <DataMember()>
    Public Property DeliveredQuantity As Integer Implements ICUMCampaign.DeliveredQuantity
        Get
            Return Me._quantity
        End Get
        Set(value As Integer)
            Me._quantity = value
        End Set
    End Property

    <DataMember()>
    Public Property Covered As Boolean Implements ICUMCampaign.Covered

    <DataMember()>
    Private Property ICUMCampaign_Id As Integer Implements ICUMCampaign.Id
        Get
            Return Me._id
        End Get
        Set(value As Integer)
            Me._id = value
        End Set
    End Property

    <DataMember()>
    Private Property ICUMCampaign_ProductId As Integer Implements ICUMCampaign.ProductId
        Get
            Return Me._productId
        End Get
        Set(value As Integer)
            Me._productId = value
        End Set
    End Property

    <DataMember()>
    Private Property ICUMCampaign_WareHouseId As Integer Implements ICUMCampaign.WareHouseId
        Get
            Return Me._warehouseId
        End Get
        Set(value As Integer)
            Me._warehouseId = value
        End Set
    End Property

    <DataMember()>
    Private Property ICUMCampaign_BatchSerialId As Integer? Implements ICUMCampaign.BatchSerialId
        Get
            Return Me._batchSerialId
        End Get
        Set(value As Integer?)
            Me._batchSerialId = value
        End Set
    End Property

    <DataMember()>
    Public Property DeliveredQuantityTmp As Integer? Implements ICUMCampaign.DeliveredQuantityTmp

    <DataMember()>
    Public Property TypeProcess As Integer? Implements ICUMCampaign.TypeProcess

    <DataMember()>
    Public Property MeasurementUnitId As String Implements ICUMCampaign.MeasurementUnitId

    <DataMember()>
    Public Property TransferOrderQuantityTmp As Integer? Implements ICUMCampaign.TransferOrderQuantityTmp

End Class
