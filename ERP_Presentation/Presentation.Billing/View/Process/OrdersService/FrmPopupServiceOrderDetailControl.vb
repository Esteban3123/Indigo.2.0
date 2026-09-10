#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

Public Class FrmPopupServiceOrderDetailControl

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

    Public Property Justify As String
        Get
            Return INDMeServiceOrderDetailControlJustify.Text
        End Get
        Set(value As String)
            INDMeServiceOrderDetailControlJustify.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    Private Function ValidateFields() As Boolean
        Dim errorList As New StringBuilder()
        If String.IsNullOrEmpty(INDMeServiceOrderDetailControlJustify.EditValue) OrElse String.IsNullOrWhiteSpace(INDMeServiceOrderDetailControlJustify.EditValue) Then
            errorList.AppendLine(INDLciServiceOrderDetailControlJustify.Text)
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Existen campos sin diligenciar: " & vbCrLf & "{0}", errorList.ToString())
            Return False
        End If
        Return True
    End Function

#End Region

#Region "Handlers"

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddCancellationReasons_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDMeServiceOrderDetailControlJustify.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddServiceOrderDetailControl_Click(sender As Object, e As EventArgs) Handles INDbtnAddServiceOrderDetailControl.Click
        If ValidateFields() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupServiceOrderDetailControl_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDMeServiceOrderDetailControlJustify_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeServiceOrderDetailControlJustify.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddServiceOrderDetailControl.Focus()
        End If
    End Sub

#End Region

#End Region

End Class