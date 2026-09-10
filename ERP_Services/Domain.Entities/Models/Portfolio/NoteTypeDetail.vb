
Imports System.Runtime.Serialization

<DataContract()>
Public Class NoteTypeDetail
    <DataMember()>
    Property Id As Integer
    <DataMember()>
    Property InvoiceDetailId As Integer
    <DataMember()>
    Property InvoiceId As Integer
    <DataMember()>
    Property BillingNoteDetailId As Integer
    <DataMember()>
    Property Code As String
    <DataMember()>
    Property Name As String
    <DataMember()>
    Property CodeAlternative As String
    <DataMember()>
    Property CodeAlternativeTwo As String
    <DataMember()>
    Property BillingGroup As String
    <DataMember()>
    Property InvoicedQuantity As Integer
    <DataMember()>
    Property UnitValue As Decimal
    <DataMember()>
    Property BaseValue As Decimal
    <DataMember()>
    Property TotalAdjustmentValue As Decimal
    <DataMember()>
    Property TaxValue As Decimal
    <DataMember()>
    Property TaxPercentage As Decimal
    <DataMember()>
    Property TaxCode As String
    <DataMember()>
    Property NameAlternative As String
    <DataMember()>
    Property NameAlternativeTwo As String
    <DataMember()>
    Property TaxClassificationType As Byte?
End Class
