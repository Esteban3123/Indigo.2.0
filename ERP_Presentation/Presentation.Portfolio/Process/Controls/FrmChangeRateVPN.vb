Public Class FrmChangeRateVPN
    Public Property VPNRate As Decimal
        Get
            Return TxtVPNRate.EditValue
        End Get
        Set(value As Decimal)
            TxtVPNRate.EditValue = value
        End Set
    End Property

    Public Event OnChangeVPNRate(value As Decimal)

    Private Sub FrmChangeRateVPN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtVPNRate.Focus()
    End Sub

    Private Sub BtnAcept_Click(sender As Object, e As EventArgs) Handles BtnAcept.Click
        RaiseEvent OnChangeVPNRate(VPNRate)
        Me.Close()
    End Sub

    Private Sub FrmChangeRateVPN_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class