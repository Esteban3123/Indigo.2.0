Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class FrmPopUpOpcionCups

#Region "Properties"
    Public Property Options As Byte
        Get
            Return INDRgCups.EditValue
        End Get
        Set(value As Byte)
            INDRgCups.EditValue = value
        End Set
    End Property
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
#End Region

#Region "Methods"

#End Region

#Region "Handlers"
    Private Sub INDBtnAceptar_Click(sender As Object, e As EventArgs) Handles INDBtnAceptar.Click
        If INDRgCups.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una opción"
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub FrmPopUpOpcionCups_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult <> System.Windows.Forms.DialogResult.OK Then
            If MessageIndigo.Show("¿Está seguro que desea cancelar la operación?", MessageType.Warning, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

End Class