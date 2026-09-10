'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/06/2020
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
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.XtraSplashScreen

#End Region

Public Class FrmAddAnnexes

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Entidad que representa a los tramites
    ''' </summary>
    Dim ListTraceabilityPaperwork As List(Of TraceabilityPaperwork)

    ''' <summary>
    ''' Listado de items de la rejilla principal, solo hay mas de un item en este listado cuando 
    ''' en el form modal FrmOpenItem seleccionan varias solicitudes y es para agrupar en un mismo anexo varios servicios,
    ''' de lo contrario siempre viene un item
    ''' </summary>
    Public ListViewListRequestsXpo As List(Of ViewListRequestsXpo)

    ''' <summary>
    ''' No. del folio que se asigna al momento de seleccionar el item en el popup
    ''' </summary>
    Dim NumberFolio As String

    ''' <summary>
    ''' Código del diagnostico que se asigna al momento de seleccionar el item en el popup
    ''' </summary>
    Dim DiagnosticCode As String

    ''' <summary>
    ''' Splash que se muestra cuando se guarda un anexo
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True)

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

#Region "Event"

    ''' <summary>
    ''' Evento que se ejecuta cuando se guarda un evento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm() As String
        Dim errors As New StringBuilder

        If INDsleHealthAdministrator.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una entidad")
        End If

        If INDsleTypeRequestServices.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un tipo servicios solicitados")
        End If

        If INDslePriorityAttention.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una prioridad de la atención")
        End If

        If INDlcgClinicHistory.Enabled = True Then
            If INDviewClinicHistory.SelectedRowsCount = 0 Then
                errors.AppendLine("Seleccione un folio")
            End If
        End If

        If INDlcgDiagnoses.Enabled = True Then
            If INDviewDiagnoses.SelectedRowsCount = 0 Then
                errors.AppendLine("Seleccione un diagnóstico")
            End If
        End If

        If String.IsNullOrEmpty(INDmemoJustification.EditValue) Then
            errors.AppendLine("Ingrese una justificación")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Guarda un evento
    ''' </summary>
    Private Async Sub Guardar()
        Dim errors = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        AssigningValues()

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardAuthorization(Me.Tag.ToString())
                Dim result = Await model.SaveTraceabilityPaperwork(ListTraceabilityPaperwork)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If MessageIndigo.Show("Desea visualizar el reporte?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        If Not waitForm.IsSplashFormVisible Then
                            waitForm.ShowWaitForm()
                        End If
                        Dim reportDef As New Reporter.rptADSolicituddeServicios()
                        reportDef.PatientCode = ListViewListRequestsXpo(0).PatientCode
                        reportDef.CareCenterCode = ListViewListRequestsXpo(0).CareCenterCode
                        reportDef.ProfessionalCode = ListViewListRequestsXpo(0).ProfessionalCode
                        reportDef.HealthAdministratorCode = INDsleHealthAdministrator.Text.Split(" - ")(0)
                        reportDef.NumberFolio = NumberFolio
                        reportDef.DiagnosticCode = DiagnosticCode
                        reportDef.TypeRequestServices = CInt(INDsleTypeRequestServices.EditValue)
                        reportDef.PriorityAttention = CInt(INDslePriorityAttention.EditValue)
                        reportDef.Justification = INDmemoJustification.EditValue
                        reportDef.FunctionalUnitName = ListViewListRequestsXpo(0).FunctionalUnitName
                        reportDef.FunctionalUnitCode = ListViewListRequestsXpo(0).FunctionalUnitCode
                        reportDef.AnnexId = result.ObjectEmbbeded(0).AnnexId
                        reportDef.AnnexesConsecutives = result.ObjectEmbbeded(0).AnnexesConsecutives
                        reportDef.AdmissionNumber = ListViewListRequestsXpo(0).AdmissionNumber
                        AddHandler reportDef.AfterPrint, AddressOf HideLoaderForm
                        ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm)
                    End If

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

    ''' <summary>
    ''' Oculta el splash screen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HideLoaderForm(sender As Object, e As EventArgs)
        waitForm.CloseWaitForm()
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        ListTraceabilityPaperwork = New List(Of TraceabilityPaperwork)
        For Each ViewListRequestsXpo In ListViewListRequestsXpo
            Dim TraceabilityPaperwork As New TraceabilityPaperwork

            With TraceabilityPaperwork
                If ViewListRequestsXpo.TraceabilityPaperworkId <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkId > 0 Then 'Si ya existe un registro se asigna el id
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
                .PreviousStatus = ViewListRequestsXpo.PreviousStatus

                'Si viene el registro con estado se asigna, sino se coloca solicitado
                If ViewListRequestsXpo.TraceabilityPaperworkStatus <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkStatus > 0 Then
                    .Status = ViewListRequestsXpo.TraceabilityPaperworkStatus
                Else
                    .Status = 1
                End If

                Dim TraceabilityPaperworkAnnexes As New TraceabilityPaperworkAnnexes
                With TraceabilityPaperworkAnnexes
                    .HealthAdministratorId = INDsleHealthAdministrator.EditValue
                    .TypeRequestServices = CInt(INDsleTypeRequestServices.EditValue)
                    .PriorityAttention = CInt(INDslePriorityAttention.EditValue)
                    .Justification = INDmemoJustification.EditValue
                    .GenerateConsecutiveWithMultipleService = If(ListViewListRequestsXpo.Count > 1, True, False)

                    .Folio = Nothing
                    If String.IsNullOrEmpty(NumberFolio) = False Then
                        .Folio = NumberFolio
                    End If
                    .DiagnosticCode = Nothing
                    If String.IsNullOrEmpty(DiagnosticCode) = False Then
                        .DiagnosticCode = DiagnosticCode
                    End If
                End With

                .TraceabilityPaperworkAnnexes.Add(TraceabilityPaperworkAnnexes)
            End With

            ListTraceabilityPaperwork.Add(TraceabilityPaperwork)
        Next
    End Sub

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listTypeRequestServices As New List(Of Tuple(Of Integer, String))
        listTypeRequestServices.Add(New Tuple(Of Integer, String)(1, "Posterior a la Atención Inicial"))
        listTypeRequestServices.Add(New Tuple(Of Integer, String)(2, "Servicios Electivos"))
        INDsleTypeRequestServices.Properties.DataSource = listTypeRequestServices.ToList()

        Dim listPriorityAttention As New List(Of Tuple(Of Integer, String))
        listPriorityAttention.Add(New Tuple(Of Integer, String)(1, "Prioritaria"))
        listPriorityAttention.Add(New Tuple(Of Integer, String)(2, "No Prioritaria"))
        INDslePriorityAttention.Properties.DataSource = listPriorityAttention.ToList()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddAnnexes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuples()
        Presenter = New PDashboardAuthorization()
        INDsleHealthAdministrator.EditValue = ListViewListRequestsXpo(0).HealthAdministratorId
        INDsleHealthAdministrator.Properties.NullText = ListViewListRequestsXpo(0).HealthAdministratorCodeName
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddAnnexes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleHealthAdministrator.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(972, Nothing, True)
            INDsleHealthAdministrator.Properties.DataSource = Presenter.InitializeHealthAdministrator()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            INDsleHealthAdministrator.Properties.DataSource = Presenter.InitializeHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de folio para obtener la justificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceFolio_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceFolio.QueryPopUp
        'Se valida si viene algun ingreso, si no viene es porque se esta generando anexo con servicios agregados manualmente que no tienen ingreso
        If (From x In ListViewListRequestsXpo Where String.IsNullOrEmpty(x.AdmissionNumber) = False Select x).Count = 0 Then
            'Se bloquea la pestaña de historia clinica y se activa la pestaña de diagnosticos
            INDlcgClinicHistory.Enabled = False
            INDlcgDiagnoses.Enabled = True
            If INDgcDiagnoses.DataSource Is Nothing Then
                INDgcDiagnoses.DataSource = Presenter.ListDiagnosXpo()
            End If
        Else
            'Se bloquea la pestaña de diagnosticos y se activa la pestaña de historia clinica
            INDlcgClinicHistory.Enabled = True
            INDlcgDiagnoses.Enabled = False
            If INDgcClinicHistory.DataSource Is Nothing Then
                Dim patientCodes = String.Join(",", (From x In ListViewListRequestsXpo Select "'" + x.PatientCode + "'").ToArray())
                Dim admissionNumbers = String.Join(",", (From x In ListViewListRequestsXpo Select "'" + x.AdmissionNumber + "'").ToArray())
                INDgcClinicHistory.DataSource = Presenter.ListHCHISPACAXpo(patientCodes, admissionNumbers)
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddAnnexes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar f4 o enter en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceFolio_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceFolio.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceFolio.ShowPopup()
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla de historia clinica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewClinicHistory_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewClinicHistory.SelectionChanged
        Dim rowHandleInfoSelected = INDviewClinicHistory.FocusedRowHandle()
        Dim listRowHandles = INDviewClinicHistory.GetSelectedRows()

        NumberFolio = String.Empty
        DiagnosticCode = String.Empty
        INDmemoJustification.EditValue = String.Empty

        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                If rowHandleInfoSelected <> listRowHandles(i) Then
                    INDviewClinicHistory.UnselectRow(listRowHandles(i))
                End If
            Next

            Dim infoRowSelected As HCHISPACAXpo = INDviewClinicHistory.GetFocusedRow()
            NumberFolio = infoRowSelected.NUMEFOLIO.Trim()
            INDmemoJustification.EditValue = infoRowSelected.DATOBJETI
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla de diagnosticos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewDiagnoses_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewDiagnoses.SelectionChanged
        Dim rowHandleInfoSelected = INDviewDiagnoses.FocusedRowHandle()
        Dim listRowHandles = INDviewDiagnoses.GetSelectedRows()

        NumberFolio = String.Empty
        DiagnosticCode = String.Empty
        INDmemoJustification.EditValue = String.Empty

        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                If rowHandleInfoSelected <> listRowHandles(i) Then
                    INDviewDiagnoses.UnselectRow(listRowHandles(i))
                End If
            Next

            Dim infoRowSelected As INDIAGNOS = INDviewDiagnoses.GetFocusedRow()
            DiagnosticCode = infoRowSelected.CODDIAGNO
            INDmemoJustification.EditValue = infoRowSelected.CODDIAGNO.Trim() + " - " + infoRowSelected.NOMDIAGNO.Trim()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class