Partial Public Class InventoryMeasurementUnit

    Public ReadOnly Property CodeName As String
        Get
            Return (Me.Code & " - " & Me.Name)
        End Get
    End Property

End Class
