Imports System.Runtime.Serialization

Partial Public Class PharmaceuticalDispensingDetailBatchSerial
    <DataMember>
    Property CodeNameBatchSerial As String

    <DataMember>
    Property DateBatchSerial As Date?

    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property ProductId As Integer

    Property DevolutionQuantity As Integer

    <DataMember>
    Property CodePharmaceuticalDispensing As String

    <DataMember>
    Property DatePharmaceuticalDispensing As Date

    <DataMember>
    Property CodeNameWarehouse As String

    <DataMember>
    Property IdWarehouse As Integer

    <DataMember>
    Property CodeNameFunctionalUnit As String

    <DataMember()>
    Public Property QuotationPharmaceuticalDispensingDetailId As Integer

    <DataMember>
    Property BatchSerialId As Integer?

    <DataMember>
    Property UserIndigo As String

End Class
