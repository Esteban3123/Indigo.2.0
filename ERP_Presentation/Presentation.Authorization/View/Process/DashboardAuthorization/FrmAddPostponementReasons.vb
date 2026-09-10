'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/12/2020
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

Public Class FrmAddPostponementReasons

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Entidad que viene desde los diferentes procesos
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
    ''' Guarda un motivo de cancelación
    ''' </summary>
    Private Async Sub Guardar()
        Dim errors As New StringBuilder

        If INDslePostponementReasons.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un motivo")
        End If

        If INDdtePostponementDate.EditValue Is Nothing Then
            errors.AppendLine("Ingrese una fecha")
        End If

        If String.IsNullOrEmpty(INDmemoPostponementObservations.EditValue) OrElse String.IsNullOrWhiteSpace(INDmemoPostponementObservations.EditValue) Then
            errors.AppendLine("Ingrese una descripción")
        End If

        If errors.ToString.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
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
                .AssignUserCode = If(ViewListRequestsXpo.AssignUser IsNot Nothing, ViewListRequestsXpo.AssignUser.Split(" - ")(0).ToString(), Nothing)
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
                .PreviousStatus = If(ViewListRequestsXpo.TraceabilityPaperworkStatus = 0, 1, ViewListRequestsXpo.TraceabilityPaperworkStatus)
                .Status = 16

                Dim TraceabilityPaperworkPostponementReasons As New TraceabilityPaperworkPostponementReasons
                TraceabilityPaperworkPostponementReasons.PostponementReasonsId = INDslePostponementReasons.EditValue
                TraceabilityPaperworkPostponementReasons.PostponementDate = INDdtePostponementDate.EditValue
                TraceabilityPaperworkPostponementReasons.PostponementObservations = INDmemoPostponementObservations.EditValue
                TraceabilityPaperworkPostponementReasons.StatusPrevious = ViewListRequestsXpo.TraceabilityPaperworkStatus
                TraceabilityPaperworkPostponementReasons.Status = True
                TraceabilityPaperworkPostponementReasons.CreationUser = indigo.UserIndigo
                .TraceabilityPaperworkPostponementReasons.Add(TraceabilityPaperworkPostponementReasons)
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
    Private Sub FrmAddCancellationReasons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardAuthorization()
        INDdtePostponementDate.Properties.MinValue = GetDateServer()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de motivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePostponementReasons_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDslePostponementReasons.QueryPopUp
        If INDslePostponementReasons.Properties.DataSource Is Nothing Then
            INDslePostponementReasons.Properties.DataSource = Presenter.InitializePostponementReasons()
        End If
    End Sub

#End Region

#Region "ButtonClick"

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddCancellationReasons_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDslePostponementReasons.Focus()
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

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddPostponementReasons_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDmemoPostponementObservations_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoPostponementObservations.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddCancellationReason.Focus()
        End If
    End Sub

#End Region

#End Region

End Class