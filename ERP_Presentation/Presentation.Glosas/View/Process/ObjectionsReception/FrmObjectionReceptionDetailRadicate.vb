#Region "Imports"

Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Presentation.Base
Imports Presentation.Glosas.MVP

#End Region

Public Class FrmObjectionReceptionDetailRadicate

#Region "Variables"

    ''' <summary>
    ''' Entidad que se envia desde los diferentes procesos
    ''' </summary>
    Public ListObjectionReceptionDetail As List(Of GlosaObjectionsReceptionD)

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
    ''' Guarda una reasignación de usuarios
    ''' </summary>
    Private Async Sub Guardar()
        If INDDeRadicatedDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la fecha de radicacion"
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDTeRadicatedReceiver.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar quien recibe la respuesta"
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDMeRadicatedObservation.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar una observacion"
            Exit Sub
        End If

        Dim ListGlosaObjectionsReceptionD As New List(Of GlosaObjectionsReceptionD)
        For Each ObjectionReceptionDetail In ListObjectionReceptionDetail
            Dim GlosaObjectionsReceptionD = New GlosaObjectionsReceptionD
            With GlosaObjectionsReceptionD
                .Id = ObjectionReceptionDetail.Id
                .RadicatedDate = INDDeRadicatedDate.EditValue
                .RadicatedReceiver = INDTeRadicatedReceiver.EditValue
                .RadicatedObservation = INDMeRadicatedObservation.EditValue
            End With
            ListGlosaObjectionsReceptionD.Add(GlosaObjectionsReceptionD)
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MObjectionsReception("")
                Dim result = Await model.GlosaObjetionReceptionDetailRadicate(ListGlosaObjectionsReceptionD)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
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

#Region "Handlers"

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmObjectionReceptionDetailRadicate_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDDeRadicatedDate.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Guardar()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmObjectionReceptionDetailRadicate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateObjectionReceptionDetailRadicates_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            INDbtnAccept.Focus()
        End If
    End Sub

#End Region

#End Region

End Class