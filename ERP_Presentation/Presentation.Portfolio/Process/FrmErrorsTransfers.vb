Public Class FrmErrorsTransfers 

    WriteOnly Property Errors As String
        Set(value As String)
            INDTxtError.Text = value
        End Set
    End Property
End Class