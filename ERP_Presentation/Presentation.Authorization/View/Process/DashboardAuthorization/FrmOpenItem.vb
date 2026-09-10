'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2020
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
Imports Domain.DocumentalSystem.Entities

#End Region

Public Class FrmOpenItem

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Item de la rejilla del formulario principal
    ''' </summary>
    Public ViewListRequestsXpo As ViewListRequestsXpo

    ''' <summary>
    ''' Indica los estados para listar las solicitudes
    ''' </summary>
    Public StringStatus As String

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenEventsAsync As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAnnexesAsync As CancellationTokenSource

    ''' <summary>
    ''' Splash que se muestra cuando se guarda un anexo
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True)

    ''' <summary>
    ''' bandera para saber si el popup de abre desde trazabilidad
    ''' </summary>
    Public FromTraceability As Boolean
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

    ''' <summary>
    ''' Bloquea los controles mientras se esta guardando
    ''' </summary>
    Private WriteOnly Property BlockControls() As Boolean
        Set(value As Boolean)
            INDsleType.Properties.ReadOnly = value
            INDsleCUPS.Properties.ReadOnly = value
            INDsleProduct.Properties.ReadOnly = value
            INDsleSource.Properties.ReadOnly = value
            INDseQuantity.Properties.ReadOnly = value
        End Set
    End Property

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento que se ejecuta para realizar las acciones del formulario principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnOpenItemModalArgs(sender As Object, e As OpenItemEventArgs)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna las acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra("2176")

        Dim ListActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(97) Then 'Si tiene permiso de solicitar cotización
            ListActions.Add(eAcciones.RequestQuotation)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(98) Then 'Si tiene permiso de agregar evento
            ListActions.Add(eAcciones.AddEvent)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(99) Then 'Si tiene permiso de cancelar
            ListActions.Add(eAcciones.CancelRequest)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(100) Then 'Si tiene permiso de reasignar
            ListActions.Add(eAcciones.Reasign)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(101) Then 'Si tiene permiso de generar anexo
            ListActions.Add(eAcciones.GenerateAnnex)
        End If

        If ListActions.Count > 0 Then 'Si hay menu se asigna al gridView
            IndigoGridView1.SetListAcction(INDviewRequests, ListActions)
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRequests.Columns
                If col.Name = "colActions" Then
                    col.Width = 50
                End If
            Next
        End If

        ListActions = New List(Of eAcciones)
        ListActions.Add(eAcciones.View)
        If BarraBotones.PermissionsForm.ContainsKey(13) Then 'Si tiene permiso de documentos
            ListActions.Add(eAcciones.ViewDocuments)
        End If
        IndigoGridView3.SetListAcction(INDviewEvents, ListActions)

        ListActions = New List(Of eAcciones)
        ListActions.Add(eAcciones.View)
        If BarraBotones.PermissionsForm.ContainsKey(98) Then 'Si tiene permiso de agregar evento
            ListActions.Add(eAcciones.AddEvent)
        End If
        IndigoGridView2.SetListAcction(INDviewAnnexes, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAnnexes.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewEvents.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next
    End Sub

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos de las solicitudes
    ''' </summary>
    Private Async Function LoadRequests() As Task
        If INDgcRequests.DataSource IsNot Nothing Then
            Exit Function
        End If

        INDviewRequests.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Await Task.Factory.StartNew(Sub()
                                        Dim result As Object
                                        If FromTraceability Then
                                            result = Presenter.ListViewListRequestsTraceabilityXpoByAdmissionNumberAndPatientCodeAndStatus(ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.PatientCode, StringStatus)
                                        Else
                                            result = Presenter.ListViewListRequestsByAdmissionNumberAndPatientCodeAndStatus(ViewListRequestsXpo.AdmissionNumber, ViewListRequestsXpo.PatientCode, StringStatus)
                                        End If
                                        If Not tokenAsync.IsCancellationRequested Then
                                            INDgcRequests.SafeInvoke(Sub()
                                                                         INDviewRequests.HideLoadingPanel()
                                                                         INDgcRequests.DataSource = result
                                                                     End Sub)
                                        End If
                                    End Sub, tokenAsync.Token)
    End Function

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos de los anexos
    ''' </summary>
    Private Sub LoadAnnexes()
        If INDgcAnnexes.DataSource IsNot Nothing Then
            Exit Sub
        End If

        Dim listIds As New List(Of Integer)
        Dim Requests As List(Of ViewListRequestsXpo) = New List(Of ViewListRequestsXpo)

        If FromTraceability Then
            Requests = TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsTraceabilityXpo)).ConvertToViewListRequests
        Else
            Requests = TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsXpo))
        End If

        If Requests IsNot Nothing AndAlso Requests.Count > 0 Then
            listIds = (From x In Requests Where x.TraceabilityPaperworkId <> Nothing AndAlso x.TraceabilityPaperworkId > 0 Select x.TraceabilityPaperworkId).ToList()
        End If

        INDviewAnnexes.ShowLoadingPanel()
        tokenAnnexesAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewListAnnexesByTraceabilityId(listIds)
                                  If Not tokenAnnexesAsync.IsCancellationRequested Then
                                      INDgcAnnexes.SafeInvoke(Sub()
                                                                  INDviewAnnexes.HideLoadingPanel()
                                                                  INDgcAnnexes.DataSource = result
                                                              End Sub)
                                  End If
                              End Sub, tokenAnnexesAsync.Token)
    End Sub

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos de los eventos
    ''' </summary>
    Private Sub LoadEvents()
        If INDgcEvents.DataSource IsNot Nothing Then
            Exit Sub
        End If

        Dim listIds As New List(Of Integer)
        Dim Requests As List(Of ViewListRequestsXpo) = New List(Of ViewListRequestsXpo)
        If FromTraceability Then
            Requests = TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsTraceabilityXpo)).ConvertToViewListRequests
        Else
            Requests = TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsXpo))
        End If
        If Requests IsNot Nothing AndAlso Requests.Count > 0 Then
            listIds = (From x In Requests Where x.TraceabilityPaperworkId <> Nothing AndAlso x.TraceabilityPaperworkId > 0 Select x.TraceabilityPaperworkId).ToList()
        End If

        INDviewEvents.ShowLoadingPanel()
        tokenEventsAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewListEventsByTraceabilityId(listIds)
                                  If Not tokenEventsAsync.IsCancellationRequested Then
                                      INDgcEvents.SafeInvoke(Sub()
                                                                 INDviewEvents.HideLoadingPanel()
                                                                 INDgcEvents.DataSource = result
                                                             End Sub)
                                  End If
                              End Sub, tokenEventsAsync.Token)
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
    ''' Método que me abre el reporte del anexo
    ''' </summary>
    Private Sub OpenReportAnnex()
        'Se obtiene el registro que tiene el foco
        Dim annexXpo = CType(INDviewAnnexes.GetFocusedRow, ViewListAnnexesXpo)

        'Busco la cabecera del anexo en la rejilla de solicitudes
        Dim entityXpo As Object

        If FromTraceability Then
            entityXpo = (From x In TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsTraceabilityXpo)) Where x.TraceabilityPaperworkId = annexXpo.TraceabilityPaperworkId Select x).FirstOrDefault()
        Else
            entityXpo = (From x In TryCast(INDgcRequests.DataSource, List(Of ViewListRequestsXpo)) Where x.TraceabilityPaperworkId = annexXpo.TraceabilityPaperworkId Select x).FirstOrDefault()
        End If

        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim reportDef As New Reporter.rptADSolicituddeServicios()
        reportDef.PatientCode = entityXpo.PatientCode
        reportDef.CareCenterCode = entityXpo.CareCenterCode
        reportDef.ProfessionalCode = entityXpo.ProfessionalCode
        reportDef.HealthAdministratorCode = entityXpo.HealthAdministratorCode
        reportDef.NumberFolio = annexXpo.Folio
        reportDef.DiagnosticCode = annexXpo.DiagnosticCode
        reportDef.TypeRequestServices = annexXpo.TypeRequestServices
        reportDef.PriorityAttention = annexXpo.PriorityAttention
        reportDef.Justification = annexXpo.Justification
        reportDef.FunctionalUnitName = entityXpo.FunctionalUnitName
        reportDef.FunctionalUnitCode = entityXpo.FunctionalUnitCode
        reportDef.AnnexId = annexXpo.TraceabilityPaperworkAnnexesId
        reportDef.AnnexesConsecutives = annexXpo.Consecutive.ToString()
        reportDef.AdmissionNumber = entityXpo.AdmissionNumber
        AddHandler reportDef.AfterPrint, AddressOf HideLoaderForm
        ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm)
    End Sub

    ''' <summary>
    ''' Método que me abre el reporte del evento
    ''' </summary>
    Private Sub OpenReportEvent()
        'Se obtiene el registro que tiene el foco
        Dim eventXpo = CType(INDviewEvents.GetFocusedRow, ViewListEventsXpo)

        'Busco la cabecera del evento en la rejilla de solicitudes
        Dim entityXpo As Object

        If FromTraceability Then
            entityXpo = (From x In CType(INDgcRequests.DataSource, List(Of ViewListRequestsTraceabilityXpo)) Where x.TraceabilityPaperworkId = eventXpo.TraceabilityPaperworkId Select x).FirstOrDefault()
        Else
            entityXpo = (From x In CType(INDgcRequests.DataSource, List(Of ViewListRequestsXpo)) Where x.TraceabilityPaperworkId = eventXpo.TraceabilityPaperworkId Select x).FirstOrDefault()
        End If

        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim reportDef As New Reporter.rptADRegistroEventos()
        reportDef.PatientCode = entityXpo.PatientCode
        reportDef.AdmissionNumber = entityXpo.AdmissionNumber
        reportDef.EventId = eventXpo.TraceabilityPaperworkEventsId
        AddHandler reportDef.AfterPrint, AddressOf HideLoaderForm
        ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm)
    End Sub

    ''' <summary>
    ''' Método que abre el form de agregar eventos, se dispara desde el form principal
    ''' </summary>
    Private Async Sub OpenAddEvent()
        'Se obtiene el registro que tiene el foco del anexo
        Dim annexXpo = CType(INDviewAnnexes.GetFocusedRow, ViewListAnnexesXpo)

        'Busco la cabecera del anexo en la rejilla de solicitudes
        Dim entityXpo As ViewListRequestsXpo
        If FromTraceability Then
            entityXpo = (From x In CType(INDgcRequests.DataSource, List(Of ViewListRequestsTraceabilityXpo)) Where x.TraceabilityPaperworkId = annexXpo.TraceabilityPaperworkId Select x).ToList.ConvertToViewListRequests.FirstOrDefault()
        Else
            entityXpo = (From x In CType(INDgcRequests.DataSource, List(Of ViewListRequestsXpo)) Where x.TraceabilityPaperworkId = annexXpo.TraceabilityPaperworkId Select x).FirstOrDefault()
        End If

        Dim args As New OpenItemEventArgs
        args.ViewListRequestsXpo = entityXpo
        args.Action = 2
        args.TraceabilityPaperworkAnnexesId = annexXpo.TraceabilityPaperworkAnnexesId
        RaiseEvent ReturnOpenItemModalArgs(Nothing, args)

        INDgcRequests.DataSource = Nothing
        Await LoadRequests()

        INDgcAnnexes.DataSource = Nothing
        LoadAnnexes()

        INDgcEvents.DataSource = Nothing
        LoadEvents()
    End Sub

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listType As New List(Of Tuple(Of Integer, String))
        listType.Add(New Tuple(Of Integer, String)(1, "Servicio"))
        listType.Add(New Tuple(Of Integer, String)(2, "Producto"))
        INDsleType.Properties.DataSource = listType.ToList()
    End Sub

    ''' <summary>
    ''' Agrega un servicio a la solicitud
    ''' </summary>
    Private Async Sub SaveAddService()
        Dim errors As New StringBuilder

        If INDsleType.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un tipo")
        End If

        If INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleCUPS.EditValue Is Nothing Then
                errors.AppendLine("Seleccione un CUPS")
            End If
        End If

        If INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleProduct.EditValue Is Nothing Then
                errors.AppendLine("Seleccione un producto")
            End If
        End If

        If INDsleSource.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un origen")
        End If

        If INDseQuantity.EditValue Is Nothing OrElse INDseQuantity.EditValue = 0 Then
            errors.AppendLine("Ingrese una cantidad")
        End If

        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Dim TraceabilityPaperwork = New TraceabilityPaperwork
        With TraceabilityPaperwork
            .AdmissionNumber = ViewListRequestsXpo.AdmissionNumber
            .Folio = Nothing
            .ServiceCode = If(INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always, INDsleCUPS.EditValue, INDsleProduct.EditValue)
            .Type = CInt(INDsleType.EditValue)
            .PatientCode = ViewListRequestsXpo.PatientCode
            .CareCenterCode = ViewListRequestsXpo.CareCenterCode
            .RequestDate = GetDateServer()
            .RequestQuantity = CInt(INDseQuantity.EditValue)
            .FunctionalUnitCode = ViewListRequestsXpo.FunctionalUnitCode
            .AuthorizationSourceId = INDsleSource.EditValue
            .IsManual = 1
            .CareGroupId = ViewListRequestsXpo.CareGroupId
            .HealthAdministratorId = ViewListRequestsXpo.HealthAdministratorId
            .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode
            .ServiceId = ViewListRequestsXpo.ServiceId
            .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId
            .Status = 1
        End With

        Me.AsyncLoader(True)
        BlockControls = True
        Try
            Using model As New MDashboardAuthorization("")
                Dim result = Await model.SaveTraceabilityPaperwork({TraceabilityPaperwork}.ToList())
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")

                    Dim args As New OpenItemEventArgs
                    args.Action = 0
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)

                    CleanControlsPopup()
                    INDgcRequests.DataSource = Nothing
                    INDgcAnnexes.DataSource = Nothing
                    INDgcEvents.DataSource = Nothing
                    Await LoadRequests()
                    Me.AsyncLoader(False)
                Else
                    Me.AsyncLoader(False)
                    BlockControls = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            BlockControls = False
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    Private Sub CleanControlsPopup()
        INDsleType.EditValue = Nothing
        INDsleCUPS.EditValue = Nothing
        INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleProduct.EditValue = Nothing
        INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDseQuantity.EditValue = Nothing
        BlockControls = False
        INDpceAddItem.ClosePopup()
    End Sub

    ''' <summary>
    ''' Método que abre el formulario para visualizar los documentos
    ''' </summary>
    Private Sub OpenViewDocuments()
        'Se obtiene el registro que tiene el foco
        Dim eventXpo = CType(INDviewEvents.GetFocusedRow, ViewListEventsXpo)

        'Se obtienen los documentos adjuntados al evento
        Dim listAttachments = Presenter.ListDocumentsByEventId(eventXpo.TraceabilityPaperworkEventsId)

        Dim listDocuments As New List(Of DocumentsStore)
        If listAttachments IsNot Nothing AndAlso listAttachments.Count > 0 Then 'Si hay documentos para mostrar
            For Each attachment In listAttachments
                Dim entity As New DocumentsStore
                With entity
                    .IdEntity = attachment.Id
                    .Name = attachment.Name
                    .Type = attachment.Extension
                    .MetaData = attachment.Description
                    .AttachDate = attachment.CreationDate
                    .Content = attachment.FileAttached
                End With
                listDocuments.Add(entity)
            Next
        End If

        Using formulario As New FrmAttach(2176, 0, Nothing, listDocuments)
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 600
            formulario.Height = 400
            formulario.TopForm = Me
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmOpenItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetActionsColumns()
        Me.ToolBar.Hide()
        Me.Text = "Paciente: " + ViewListRequestsXpo.PatientCode.Trim + " - " + ViewListRequestsXpo.PatientName.Trim
        If ViewListRequestsXpo.AdmissionNumber IsNot Nothing Then
            Me.Text = Me.Text + "    No. Ingreso: " + ViewListRequestsXpo.AdmissionNumber.Trim
        End If
        Presenter = New PDashboardAuthorization()
        InitializeTuples()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmOpenItem_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await LoadRequests()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmOpenItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Try
            AsyncLoader(True)

            Dim args As New OpenItemEventArgs
            Dim list As New List(Of ViewListRequestsXpo)

            If FromTraceability Then
                args.ViewListRequestsXpo = {TryCast(INDviewRequests.GetFocusedRow(), ViewListRequestsTraceabilityXpo)}.ToList().ConvertToViewListRequests.FirstOrDefault
                list = (From x In INDviewRequests.GetSelectedRows() Where INDviewRequests.IsGroupRow(x) = False Select CType(INDviewRequests.GetRow(x), ViewListRequestsTraceabilityXpo)).ToList().ConvertToViewListRequests
                If Not list.Any Then
                    list = {CType(INDviewRequests.GetFocusedRow(), ViewListRequestsTraceabilityXpo)}.ToList().ConvertToViewListRequests
                End If
            Else
                args.ViewListRequestsXpo = TryCast(INDviewRequests.GetFocusedRow(), ViewListRequestsXpo)
                list = (From x In INDviewRequests.GetSelectedRows() Where INDviewRequests.IsGroupRow(x) = False Select CType(INDviewRequests.GetRow(x), ViewListRequestsXpo)).ToList()
                If Not list.Any Then
                    list = {CType(INDviewRequests.GetFocusedRow(), ViewListRequestsXpo)}.ToList()

                End If
            End If

            Select Case sender.Tag.ToString
                Case "RequestQuotation"

                Case "AddEvent"
                    args.Action = 2
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)
                Case "CancelRequest"
                    args.Action = 3
                    args.ListViewListRequestsXpo = list
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)
                Case "Reasign"
                    args.Action = 4
                    args.ListViewListRequestsXpo = list
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)
                Case "GenerateAnnex"
                    args.Action = 5
                    args.ListViewListRequestsXpo = list
                    RaiseEvent ReturnOpenItemModalArgs(Nothing, args)
            End Select

            INDgcRequests.DataSource = Nothing
            INDgcAnnexes.DataSource = Nothing
            INDgcEvents.DataSource = Nothing
            Await LoadRequests()
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de anexos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "View"
                OpenReportAnnex()
            Case "AddEvent"
                OpenAddEvent()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de anexos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "View"
                OpenReportAnnex()
            Case "AddEvent"
                OpenAddEvent()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As Object = Nothing
        Dim text As String = ""
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
            text = btn.Tag.ToString
        Else
            btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
            text = btn.Text
        End If
        Select Case text
            Case "View", "Ver"
                OpenReportEvent()
            Case "ViewDocuments", "Ver Documentos"
                OpenViewDocuments()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "View"
                OpenReportEvent()
            Case "ViewDocuments"
                OpenViewDocuments()
        End Select
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmOpenItem_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
        If tokenEventsAsync IsNot Nothing Then
            tokenEventsAsync.Cancel()
        End If
        If tokenAnnexesAsync IsNot Nothing Then
            tokenAnnexesAsync.Cancel()
        End If
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDtcgInfo_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInfo.SelectedPageChanged
        Select Case e.Page.Name
            Case INDlcgRequests.Name 'Solicitudes
                Await LoadRequests()
            Case INDlcgEventsAndAnnex.Name 'Eventos y Anexos
                LoadAnnexes()
                LoadEvents()
        End Select
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCUPS.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(970, Nothing, True)
            INDsleCUPS.Properties.DataSource = Presenter.InitializeCUPS(ViewListRequestsXpo.CareCenterCode)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(1034, Nothing, True)
            INDsleProduct.Properties.DataSource = Presenter.InitializeProducts(ViewListRequestsXpo.CareCenterCode)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSource_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleSource.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(2132, Nothing, True)
            INDsleSource.Properties.DataSource = Presenter.InitializeSource()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCUPS.QueryPopUp
        If INDsleCUPS.Properties.DataSource Is Nothing Then
            INDsleCUPS.Properties.DataSource = Presenter.InitializeCUPS(ViewListRequestsXpo.CareCenterCode)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing Then
            INDsleProduct.Properties.DataSource = Presenter.InitializeProducts(ViewListRequestsXpo.CareCenterCode)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSource_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSource.QueryPopUp
        If INDsleSource.Properties.DataSource Is Nothing Then
            INDsleSource.Properties.DataSource = Presenter.InitializeSource()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 1 Then 'Servicios
                INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else 'Producto
                INDlyItemCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddItem.Click
        SaveAddService()
    End Sub

#End Region

#End Region

End Class

Public Class OpenItemEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Representa el tipo de acción que va a ejecutar 1.RequestQuotation, 2.Radicate, 3.CancelRequest, 4.Reasign, 5.OpenFormGenerateAnnex
    ''' </summary>
    ''' <returns></returns>
    Property Action As Integer

    ''' <summary>
    ''' Item que representa a la entidad que tiene el foco y le va a realizar las acciones del form principal
    ''' </summary>
    ''' <returns></returns>
    Property ViewListRequestsXpo As ViewListRequestsXpo

    ''' <summary>
    ''' Item que representa a las entidades para realizar un solo anexo con las solicitudes seleccionadas
    ''' </summary>
    ''' <returns></returns>
    Property ListViewListRequestsXpo As List(Of ViewListRequestsXpo)

    ''' <summary>
    ''' Permite saber si el evento que se va a crear esta asociado a un anexo
    ''' </summary>
    ''' <returns></returns>
    Property TraceabilityPaperworkAnnexesId As Integer?

End Class