Imports System.Runtime.Serialization

Partial Public Class PhysicalInventory
    <DataMember>
    Property Code As String

    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property CodeNameWarehouse As String

    <DataMember>
    Property CodeNameBatchSerial As String

    <DataMember>
    Property BatchSerialExpiredDate As Date?

    <DataMember>
    Property QuantityDeliver As Integer

    <DataMember>
    Property ClassName As String

    <DataMember>
    Property ClassType As Byte

    <DataMember>
    Property Dose As Decimal?

    <DataMember>
    Property DoseMeasurement As String

    <DataMember>
    Property Covered As Boolean

End Class
