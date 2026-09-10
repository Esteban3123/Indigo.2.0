#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base

#End Region

Public Class PopUpCancellationReasons

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim _presenter As PDashboardAuthorization

#End Region

#Region "Event"

    Public Event ReturnCancellationReasonArgs(sender As Object, e As AddCancellationReasonEventArgs)

#End Region

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda una cancelación
    ''' </summary>
    Private Sub Guardar()
        Dim errors As New StringBuilder

        If INDsleCancellationReasons.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un motivo")
        End If

        If String.IsNullOrEmpty(INDmemoCancellationReasonsObservations.EditValue) OrElse String.IsNullOrWhiteSpace(INDmemoCancellationReasonsObservations.EditValue) Then
            errors.AppendLine("Ingrese una descripción")
        End If

        If errors.ToString.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If

        Dim args As New AddCancellationReasonEventArgs With
        {
            .CancellationReasonsId = INDsleCancellationReasons.EditValue,
            .CancellationReasonsObservations = INDmemoCancellationReasonsObservations.EditValue
        }
        RaiseEvent ReturnCancellationReasonArgs(Me, args)
        Me.Close()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpCancellationReasons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PDashboardAuthorization()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpCancellationReasons_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCancellationReasons.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de motivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCancellationReasons_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCancellationReasons.QueryPopUp
        If INDsleCancellationReasons.Properties.DataSource Is Nothing Then
            INDsleCancellationReasons.Properties.DataSource = _presenter.InitializeCancellationReason()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de motivos de cancelación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCancellationReasons_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCancellationReasons.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2131, Nothing, True)
            INDsleCancellationReasons.Properties.DataSource = _presenter.InitializeCancellationReason()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpCancellationReasons_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDmemoCancellationReasonsObservations_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoCancellationReasonsObservations.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddCancellationReason.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddCancellationReason_Click(sender As Object, e As EventArgs) Handles INDbtnAddCancellationReason.Click
        Guardar()
    End Sub

#End Region

#End Region

End Class

Public Class AddCancellationReasonEventArgs
    Inherits EventArgs

    Property CancellationReasonsId As Integer

    Property CancellationReasonsObservations As String

End Class