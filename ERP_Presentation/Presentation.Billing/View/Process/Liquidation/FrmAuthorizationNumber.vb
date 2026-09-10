Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class FrmAuthorizationNumber

#Region "Events"

    Public Event ReturnValue(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    Public Property AuthorizationNumber As String
        Get
            Return INDtxtAuthorizationNumber.EditValue
        End Get
        Set(value As String)
            INDtxtAuthorizationNumber.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Hanlders"

#Region "Click"

    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If Not String.IsNullOrEmpty(RTrim(LTrim(AuthorizationNumber))) Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            ShowMessage(EeventViewerImages.Advertencia) = "Ingrese un número de autorización"
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmAuthorizationNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        End If
    End Sub

#End Region

#End Region

End Class