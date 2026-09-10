Public Class FrmValidationWarning 

    Public Property Datasource As List(Of String)
        Get
            Return INDgcValidations.DataSource
        End Get
        Set(value As List(Of String))
            INDgcValidations.DataSource = value.Select(Function(x) New With {.Validation = x}).ToList()
        End Set
    End Property

    Private Sub FrmValidationWarning_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub INDsbContinue_Click(sender As Object, e As EventArgs) Handles INDsbContinue.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub INDsbCancel_Click(sender As Object, e As EventArgs) Handles INDsbCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
    End Sub
End Class