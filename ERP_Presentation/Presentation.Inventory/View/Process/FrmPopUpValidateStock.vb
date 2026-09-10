Public Class FrmPopUpValidateStock

#Region "Properties"

    Public Property Datasource As List(Of String)
        Get
            Return INDGcMessageValidateStock.DataSource
        End Get
        Set(value As List(Of String))
            INDGcMessageValidateStock.DataSource = value.Distinct().Select(Function(x) New With {.Validation = x}).ToList()
        End Set
    End Property

#End Region

#Region "Handles"

    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Me.Close()
    End Sub

    Private Sub FrmPopUpValidateStock_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

End Class