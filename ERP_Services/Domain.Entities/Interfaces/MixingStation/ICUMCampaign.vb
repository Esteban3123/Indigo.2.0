Public Interface ICUMCampaign
    Property Id() As Integer
    Property ProductId() As Integer
    Property ProductFullName() As String
    Property WareHouseId() As Integer
    Property WareHouseFullName() As String
    Property ExpirationDate() As Date?
    Property BatchSerialId() As Integer?
    Property BatchSerialCode() As String
    Property AvailableQuantity() As Integer
    Property DeliveredQuantity() As Integer
    Property DeliveredQuantityTmp() As Integer?
    Property TypeProcess() As Integer?
    Property MeasurementUnitId() As String
    Property Covered() As Boolean
    Property TransferOrderQuantityTmp() As Integer?
End Interface
