Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class ShowDialogJustification

    Public Property Justification As String

    Private Sub INDsbAccept_Click(sender As Object, e As EventArgs) Handles INDsbAccept.Click
        If INDmeJustification.Text.Trim Is String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar justificación de anulación!"
            INDmeJustification.Focus()
            Exit Sub
        End If

        If INDmeJustification.Text.Trim.Length < 10 Then
            Mensaje(EeventViewerImages.Advertencia) = "La justificación debe tener mínimo 10 caracteres!"
            INDmeJustification.Focus()
            Exit Sub
        End If
        Justification = INDmeJustification.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

End Class