Imports System.Runtime.Serialization

Public Class TransferOrderDetailBatchSerial

    <DataMember>
    Property ProductId As Integer

    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property CodeNameWarehouse As String

    <DataMember>
    Property CodeBatchSerial As String

    <DataMember>
    Property QuantityDeliver As Integer

End Class
