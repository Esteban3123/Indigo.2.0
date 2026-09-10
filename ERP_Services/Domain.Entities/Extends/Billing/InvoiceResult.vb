Imports System.Runtime.Serialization

<Serializable()>
Public Class InvoiceResult
    <DataMember()>
    Property InvoiceId As Integer
    <DataMember()>
    Property InvoiceNumber As String
End Class
