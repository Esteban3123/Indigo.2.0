Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class FrmBalancedScorecardName

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Public Property BalancedScoreCardName As String
        Get
            Return INDTxtName.Text.Trim()
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Private Sub FrmBalancedScorecardName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmBalancedScorecardName_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If String.IsNullOrEmpty(INDTxtName.Text.Trim()) Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor llene el campo nombre"
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub
End Class