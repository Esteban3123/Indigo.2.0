Imports System.Runtime.Serialization

Partial Public Class BatchSerial
    Property Quantity As Integer
    Property ExpirationDateNew As Date?
    Property CommentUpdateExpirationDate As String

    ''' <summary>
    ''' Cantidad disponible
    ''' </summary>
    <DataMember()>
    Property OutstandingQuantity As Integer
End Class
