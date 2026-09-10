#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraSplashScreen
Imports Domain.DocumentalSystem.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Class FrmOpenAlert

#Region "Variables"

    Dim _presenter As PDashboardAuthorization

    Dim _viewListRequestsXpo As ViewListRequestsXpo

    Dim _traceabilityPaperworkAlert As TraceabilityPaperworkAlert

#End Region

#Region "Builder"

    Public Sub New(viewListRequestsXpo As ViewListRequestsXpo)
        InitializeComponent()

        Me._viewListRequestsXpo = viewListRequestsXpo
    End Sub

#End Region

#Region "Properties"

    Property CanSaveAlert As Boolean

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

    Private Sub SetActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.SuspendAlert)

        If ListActions.Count > 0 Then
            IndigoGridView1.SetListAcction(INDGvCurrentAlerts, ListActions)
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCurrentAlerts.Columns
                If col.Name = "colActions" Then
                    col.Width = 40
                End If
            Next
        End If
    End Sub

    Private Property CurrentAlertsXpo As XPInstantFeedbackSource
        Get
            Return INDgcCurrentAlerts.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgcCurrentAlerts.DataSource = value
        End Set
    End Property

    Private Property PastAlertsXpo As XPInstantFeedbackSource
        Get
            Return INDGcPastAlerts.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcPastAlerts.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub LoadCurrentAlerts()
        If CurrentAlertsXpo Is Nothing Then
            CurrentAlertsXpo = _presenter.InitializeAlerts(_viewListRequestsXpo.TraceabilityPaperworkId, True)
        Else
            CurrentAlertsXpo.Refresh()
        End If
    End Sub

    Private Sub LoadPastAlerts()
        If PastAlertsXpo Is Nothing Then
            PastAlertsXpo = _presenter.InitializeAlerts(_viewListRequestsXpo.TraceabilityPaperworkId, False)
        Else
            PastAlertsXpo.Refresh()
        End If
    End Sub

    Private Async Sub Guardar()
        If Not Me.CanSaveAlert Then
            Mensaje(EeventViewerImages.Advertencia) = "No tiene permisos para guardar la alerta"
            Exit Sub
        End If

        Dim ListTraceabilityPaperwork As New List(Of TraceabilityPaperwork)

        Dim TraceabilityPaperwork = New TraceabilityPaperwork
        With TraceabilityPaperwork
            If _viewListRequestsXpo.TraceabilityPaperworkId <> Nothing AndAlso _viewListRequestsXpo.TraceabilityPaperworkId > 0 Then 'Si ya hay un registro se asigna el id
                .Id = _viewListRequestsXpo.TraceabilityPaperworkId
            End If

            .AdmissionNumber = _viewListRequestsXpo.AdmissionNumber
            .Folio = _viewListRequestsXpo.Folio
            .ServiceCode = _viewListRequestsXpo.ItemCodeOriginal
            .Type = _viewListRequestsXpo.Type
            .PatientCode = _viewListRequestsXpo.PatientCode
            .CareCenterCode = _viewListRequestsXpo.CareCenterCode
            .RequestDate = _viewListRequestsXpo.RequestDate
            .RequestQuantity = _viewListRequestsXpo.Quantity
            .FunctionalUnitCode = _viewListRequestsXpo.FunctionalUnitCode
            .EntityId = _viewListRequestsXpo.EntityId
            .EntityName = _viewListRequestsXpo.EntityName
            .IsManual = _viewListRequestsXpo.IsManual
            .CareGroupId = _viewListRequestsXpo.CareGroupId
            .HealthAdministratorId = _viewListRequestsXpo.HealthAdministratorId
            .AuthorizationSourceId = Nothing
            If _viewListRequestsXpo.AuthorizationSourceId <> Nothing AndAlso _viewListRequestsXpo.AuthorizationSourceId > 0 Then
                .AuthorizationSourceId = _viewListRequestsXpo.AuthorizationSourceId
            End If
            .ProfessionalCode = _viewListRequestsXpo.ProfessionalCode
            .ServiceId = _viewListRequestsXpo.ServiceId
            .ContractDescriptionId = _viewListRequestsXpo.ContractDescriptionId
            .PreviousStatus = _viewListRequestsXpo.PreviousStatus

            'Si viene el registro con estado se asigna, sino se coloca solicitado
            If _viewListRequestsXpo.TraceabilityPaperworkStatus <> Nothing AndAlso _viewListRequestsXpo.TraceabilityPaperworkStatus > 0 Then
                .Status = _viewListRequestsXpo.TraceabilityPaperworkStatus
            Else
                .Status = 1
            End If

            .TraceabilityPaperworkAlert.Add(New TraceabilityPaperworkAlert With
            {
                .Id = _traceabilityPaperworkAlert.Id,
                .TraceabilityPaperworkId = TraceabilityPaperwork.Id,
                .Comments = _traceabilityPaperworkAlert.Comments,
                .Status = _traceabilityPaperworkAlert.Status
            })

        End With
        ListTraceabilityPaperwork.Add(TraceabilityPaperwork)

        Try
            Using model As New MDashboardAuthorization("")
                Me.AsyncLoader(True)
                Dim result = Await model.SaveTraceabilityPaperwork(ListTraceabilityPaperwork)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
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

#Region "Event"

#Region "Custom"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmOpenAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetActionsColumns()
        Me.ToolBar.Hide()

        _presenter = New PDashboardAuthorization()
    End Sub

#End Region

    Private Sub FrmOpenAlert_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LoadCurrentAlerts()
    End Sub

#Region "KeyDown"

    Private Sub FrmRequestsToAutomaticAllocation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInfo_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInfo.SelectedPageChanged
        Select Case e.Page.Name
            Case INDlcgCurrentAlerts.Name 'Alertas actuales
                LoadCurrentAlerts()
            Case INDlcgPastAlerts.Name 'Alertas pasadas
                LoadPastAlerts()
        End Select
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim alert = DirectCast(DirectCast(INDGvCurrentAlerts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, TraceabilityPaperworkAlertXpo)
        _traceabilityPaperworkAlert = New TraceabilityPaperworkAlert With
        {
            .Id = alert.Id,
            .TraceabilityPaperworkId = alert.TraceabilityPaperworkId,
            .Comments = alert.Comments,
            .Status = False
        }

        Guardar()
    End Sub

#End Region



#Region "Click"

    Private Sub INDbtnAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddAlert.Click
        If String.IsNullOrEmpty(INDMeComments.EditValue.Trim) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un comentario"
            Exit Sub
        End If

        _traceabilityPaperworkAlert = New TraceabilityPaperworkAlert With
        {
            .Comments = INDMeComments.EditValue,
            .Status = True
        }

        Guardar()
    End Sub

#End Region

#End Region

End Class