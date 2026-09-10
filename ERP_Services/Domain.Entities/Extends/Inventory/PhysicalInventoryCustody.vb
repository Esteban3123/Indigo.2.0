Imports System.Runtime.Serialization

Partial Public Class PhysicalInventoryCustody
    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property CodeNameWarehouse As String

    <DataMember>
    Property CodeNameBatchSerial As String

    <DataMember>
    Property BatchSerialExpiredDate As Date?

    Property QuantityDeliver As Integer
End Class
