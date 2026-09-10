''' <summary>
''' Resultado de reservar un código consecutivo desde BillingSequence (uso atómico vía SP).
''' </summary>
Public Class BillingSequenceCodeReservation
    Public Property Success As Boolean
    Public Property Code As String
    Public Property Message As String
End Class
