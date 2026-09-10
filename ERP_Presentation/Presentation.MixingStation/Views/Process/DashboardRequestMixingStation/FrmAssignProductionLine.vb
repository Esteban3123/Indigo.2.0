'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmAssignProductionLine

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardRequestMixingStation

    ''' <summary>
    ''' Id de la solicitud
    ''' </summary>
    Public RequestMixingStationDetailId As Integer

    Public Property RequestMixingStationId As Integer

    ''' <summary>
    ''' Código y nombre de la linea de producción
    ''' </summary>
    Public ProductionLineCodeName As String

    ''' <summary>
    ''' Id de la linea de producción
    ''' </summary>
    Public ProductionLineId As Integer

    ''' <summary>
    ''' Id de  la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Property MixingStationId As Integer

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

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

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignProductionLine_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardRequestMixingStation()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignProductionLine_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If ProductionLineId <> Nothing AndAlso ProductionLineId > 0 AndAlso Not String.IsNullOrEmpty(ProductionLineCodeName) Then
            INDsleProductionLine.EditValue = ProductionLineId
            INDsleProductionLine.Properties.NullText = ProductionLineCodeName
        End If
        INDsleProductionLine.Focus()
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Async loader
    ''' </summary>
    ''' <param name="State"></param>
    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        INDbtnAccept.Enabled = Not State
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        If INDsleProductionLine.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una línea de producción"
            Exit Sub
        End If

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardRequestMixingStation("")
                Dim data As New Tuple(Of Integer, Integer)(RequestMixingStationDetailId, INDsleProductionLine.EditValue)
                Dim result = Await model.SaveRequestMixingStation(data)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se ha modificado la línea de producción correctamente"
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignProductionLine_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductionLine_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleProductionLine.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAccept.Focus()
        End If
    End Sub


    Private Sub INDsleProductionLine_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleProductionLine.QueryPopUp
        If INDsleProductionLine.Properties.DataSource Is Nothing Then
            INDsleProductionLine.Properties.DataSource = Presenter.InitializeProductionLine(MixingStationId, RequestMixingStationDetailId)
        End If
    End Sub

#End Region

#End Region

End Class