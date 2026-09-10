'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2020
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

Public Class FrmAssignUser

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Entidad que se envia desde los diferentes procesos
    ''' </summary>
    Public ListViewListRequestsXpo As List(Of ViewListRequestsXpo)

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

#Region "Methods"

    ''' <summary>
    ''' Guarda una reasignación de usuarios
    ''' </summary>
    Private Async Sub Guardar()
        If INDsleINDsleScheduleTemplateUsers.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un usuario"
            Exit Sub
        End If

        Dim ListTraceabilityPaperwork As New List(Of TraceabilityPaperwork)
        For Each ViewListRequestsXpo In ListViewListRequestsXpo
            Dim TraceabilityPaperwork = New TraceabilityPaperwork
            With TraceabilityPaperwork
                If ViewListRequestsXpo.TraceabilityPaperworkId <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkId > 0 Then 'Si ya hay un registro se asigna el id
                    .Id = ViewListRequestsXpo.TraceabilityPaperworkId
                End If

                .AdmissionNumber = ViewListRequestsXpo.AdmissionNumber
                .Folio = ViewListRequestsXpo.Folio
                .ServiceCode = ViewListRequestsXpo.ItemCodeOriginal
                .Type = ViewListRequestsXpo.Type
                .PatientCode = ViewListRequestsXpo.PatientCode
                .CareCenterCode = ViewListRequestsXpo.CareCenterCode
                .RequestDate = ViewListRequestsXpo.RequestDate
                .RequestQuantity = ViewListRequestsXpo.Quantity
                .FunctionalUnitCode = ViewListRequestsXpo.FunctionalUnitCode
                .EntityId = ViewListRequestsXpo.EntityId
                .EntityName = ViewListRequestsXpo.EntityName
                .IsManual = ViewListRequestsXpo.IsManual
                .CareGroupId = ViewListRequestsXpo.CareGroupId
                .HealthAdministratorId = ViewListRequestsXpo.HealthAdministratorId
                .AuthorizationSourceId = Nothing
                If ViewListRequestsXpo.AuthorizationSourceId <> Nothing AndAlso ViewListRequestsXpo.AuthorizationSourceId > 0 Then
                    .AuthorizationSourceId = ViewListRequestsXpo.AuthorizationSourceId
                End If
                .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode
                .ServiceId = ViewListRequestsXpo.ServiceId
                .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId
                .PreviousStatus = ViewListRequestsXpo.PreviousStatus

                'Si viene el registro con estado se asigna, sino se coloca solicitado
                If ViewListRequestsXpo.TraceabilityPaperworkStatus <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkStatus > 0 Then
                    .Status = ViewListRequestsXpo.TraceabilityPaperworkStatus
                Else
                    .Status = 1
                End If
                .AssignUserCode = INDsleINDsleScheduleTemplateUsers.Text.Split(" - ")(0)
            End With

            ListTraceabilityPaperwork.Add(TraceabilityPaperwork)
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardAuthorization("")
                Dim result = Await model.SaveTraceabilityPaperwork(ListTraceabilityPaperwork)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
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

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardAuthorization()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleINDsleScheduleTemplateUsers.QueryPopUp
        If INDsleINDsleScheduleTemplateUsers.Properties.DataSource Is Nothing Then
            INDsleINDsleScheduleTemplateUsers.Properties.DataSource = Presenter.InitializeListScheduleTemplateUsers()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de plantilla de turnos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleINDsleScheduleTemplateUsers.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(2135, Nothing, True)
            INDsleINDsleScheduleTemplateUsers.Properties.DataSource = Presenter.InitializeListScheduleTemplateUsers()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignUser_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleINDsleScheduleTemplateUsers.Focus()
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
    Private Sub FrmAssignUser_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleINDsleScheduleTemplateUsers_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleINDsleScheduleTemplateUsers.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAccept.Focus()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddReasignUserEventArgs
    Inherits EventArgs

    Property UserId As Integer

    Property UserCode As String

    Property ViewListRequestsXpo As ViewListRequestsXpo

End Class