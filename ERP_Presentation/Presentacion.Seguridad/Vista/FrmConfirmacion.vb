Public Class FrmConfirmacion
    Property valor As Boolean

    Private Sub btnyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnyes.Click
        valor = True
        Me.Close()
    End Sub

    Private Sub btnno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnno.Click
        valor = False
        Me.Close()
    End Sub

    Private Sub FrmConfirmacion_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If valor = Nothing Then
            valor = False
        End If
    End Sub

    Private Sub FrmConfirmacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pnlappbar.Width = Me.Width
        pnlappbar.Left = Me.Left
        pnlappbar.Height = 100
        pnlappbar.Top = Me.Height - pnlappbar.Height
        btnno.Left = (pnlappbar.Width - btnno.Width) - 10
        btnyes.Left = (btnno.Left - btnno.Width) - 10
        lblinfo.Left = Me.Left + 200
        lblinfo2.Left = Me.Left + 200
        Me.TopMost = True
    End Sub

    Private Sub btnyes_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnyes.MouseEnter
        btnyes.BackColor = Color.Gray
    End Sub

    Private Sub btnno_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnno.MouseEnter
        btnno.BackColor = Color.Gray
    End Sub

    Private Sub btnyes_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnyes.MouseLeave
        btnyes.BackColor = Color.Black
    End Sub

    Private Sub btnno_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnno.MouseLeave
        btnno.BackColor = Color.Black
    End Sub
End Class