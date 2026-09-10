Imports System.Runtime.Serialization

Partial Public Class ElectronicsRIPS
    <DataMember>
    Property DocumentNumber As String

    <DataMember>
    Property EntityName As String

    <DataMember>
    Property DocumentType As Byte

    <DataMember>
    Property EntityId As Integer

    <DataMember>
    Property BillingNoteInvoiceNumber As String

    <DataMember>
    Property CUV As String

    <DataMember>
    Property StatusRIPS As Byte
End Class
