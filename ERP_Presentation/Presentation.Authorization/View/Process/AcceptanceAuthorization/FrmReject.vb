'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Presentation.Authorization.MVP
Imports DevExpress.Xpo
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraEditors.BaseCheckedListBoxControl
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Authorization
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReject

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PAcceptanceAuthorization

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As RejectEventArgs)

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

#Region "Handlers"

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReject_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Presenter = New PAcceptanceAuthorization()
        INDsleRejection.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If INDsleRejection.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un rechazo"
            Exit Sub
        End If

        If String.IsNullOrEmpty(INDmemoAnnotations.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una observación de rechazo"
            Exit Sub
        End If

        Dim args As New RejectEventArgs With {.ObservationsReject = INDmemoAnnotations.EditValue, .AuthorizationRejectionId = INDsleRejection.EditValue}
        RaiseEvent ReturnModalArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReject_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de rechazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRejection_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleRejection.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(2185, Nothing, True)
            INDsleRejection.Properties.DataSource = Presenter.InitializeAuthorizationRejection()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rechazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRejection_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRejection.QueryPopUp
        If INDsleRejection.Properties.DataSource Is Nothing Then
            INDsleRejection.Properties.DataSource = Presenter.InitializeAuthorizationRejection()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class RejectEventArgs
    Inherits EventArgs

    Property ObservationsReject As String

    Property AuthorizationRejectionId As Integer

End Class