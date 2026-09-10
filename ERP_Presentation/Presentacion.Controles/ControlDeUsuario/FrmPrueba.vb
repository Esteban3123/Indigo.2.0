Public Class FrmPrueba 
    Private Sub obtener()
        Dim hola = CtrBiometricoGuardar1.ValorTemplate
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        obtener()

    End Sub
End Class