Public Class FrmChangeExpectative
    Public Property Expectative As Integer
        Get
            Return TxtVPNRate.EditValue
        End Get
        Set(value As Integer)
            TxtVPNRate.EditValue = value
        End Set
    End Property

    Public Event OnChangeExpectative(value As Integer)

    Private Sub FrmChangeRateVPN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtVPNRate.Focus()
    End Sub

    Private Sub BtnAcept_Click(sender As Object, e As EventArgs) Handles BtnAcept.Click
        RaiseEvent OnChangeExpectative(Expectative)
        Me.Close()
    End Sub

    Private Sub FrmChangeRateVPN_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class