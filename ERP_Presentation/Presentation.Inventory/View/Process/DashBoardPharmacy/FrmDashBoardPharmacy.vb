'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 24-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Dynamic
Imports System.IO
Imports System.Timers
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class FrmDashBoardPharmacy
    Implements IDashBoardPharmacy

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "BUILDER"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        _frmReportViewer = New FrmReportViewer
        _frmReportViewer.TopLevel = False
        _frmReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDPcReportViewer.Controls.Add(_frmReportViewer)

        _frmReportViewerPharmacyNotes = New FrmReportViewer
        _frmReportViewerPharmacyNotes.TopLevel = False
        _frmReportViewerPharmacyNotes.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewerPharmacyNotes.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDPcReportViewerPharmacyNotes.Controls.Add(_frmReportViewerPharmacyNotes)
    End Sub

#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' Id de la Unidad Operativa Seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Private _frmReportViewer As FrmReportViewer
    Private _frmReportViewerPharmacyNotes As FrmReportViewer
    Dim reportDef As DevExpress.XtraReports.UI.XtraReport
    Dim reportDefDevolution As DevExpress.XtraReports.UI.XtraReport

    ''' <summary>
    ''' presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacy
    ''' <summary>
    ''' parámetros de inventario
    ''' </summary>
    Private _settingInventory As SettingInventory
    ''' <summary>
    ''' listado de las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private listDashBoardRequest As XPCollection(Of ViewDashBoardPharmacy)
    ''' <summary>
    ''' listado de las devoluciones
    ''' </summary>
    ''' <remarks></remarks>
    Private listDashBoardDevolution As XPCollection(Of ViewDashBoardPharmacyDevolution)

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

    ''' <summary>
    ''' Variable que se utiliza para obtener los tipos de filtro
    ''' </summary>
    Private typeFilters As String

    ''' <summary>
    ''' Obtiene un objeto de la clase despues de procesar
    ''' </summary>
    Private evenPostProcess As PharmaceuticalDispensing

    ''' <summary>
    ''' obtiene la lista de los items de la solicitud
    ''' </summary>
    Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution)

    ''' <summary>
    ''' propiedad privada que establece timer que refrescará el dashboard
    ''' </summary>
    Private _timer As System.Timers.Timer

    Private _flagTimer As Boolean = False

    ''' <summary>
    ''' Variable para saber si tiene filtro de medicamentos
    ''' </summary>
    Private medicamentos As Nullable(Of Integer)

    ''' <summary>
    ''' Variable para saber si tiene filtro de  medicamentos tipo insumo
    ''' </summary>
    Private medicamentos_insumos As Nullable(Of Integer)

    ''' <summary>
    ''' Variable para saber si tiene filtro de insumo
    ''' </summary>
    Private insumos As Nullable(Of Integer)

    ''' <summary>
    ''' Variable que almacena el datasource del detalle de la pestaña Central de mezclas
    ''' </summary>
    Private datasourceMixingSationDetail As List(Of ViewDashBoardPharmacyMixingSation)

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Datasource de unidades funcionales
    ''' </summary>
    Public Property FunctionalUnitDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IDashBoardPharmacy.CareCenterXPO
        Get
            Return CType(INDSleCareCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad funcional
    ''' </summary>
    Public Property FunctionalUnitId As String Implements IDashBoardPharmacy.CareCenterCode
        Get
            Return INDSleCareCenter.EditValue
        End Get
        Set(value As String)
            INDSleCareCenter.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' propiedad que obtiene o asigna el valor que espicifica si se activa 
    ''' o no el refrescado automatico sobre la rejilla
    ''' </summary>
    ''' <returns></returns>
    Public Property AutomaticReload As Boolean
        Get
            Return INDTbAutomaticReload.EditValue
        End Get
        Set(value As Boolean)
            INDTbAutomaticReload.EditValue = value
        End Set
    End Property
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _frmReportViewer = Nothing
        _frmReportViewerPharmacyNotes = Nothing
        reportDef = Nothing
        reportDefDevolution = Nothing
        _presenter = Nothing
        listDashBoardRequest = Nothing
        listDashBoardDevolution = Nothing
        evenPostProcess = Nothing
        record = Nothing

        If Not My.Computer.FileSystem.DirectoryExists(String.Format("{0}\Pharmacy\{1}\{2}", Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDTbAutomaticReload), "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format("{0}\Pharmacy\{1}\{2}", Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDTbAutomaticReload), ""))
        End If
        Dim automaticReloadPath As String = String.Format("{0}\Pharmacy\{1}\{2}", Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDTbAutomaticReload), $"{NameOf(INDTbAutomaticReload)}.IndigoUser_{SessionValues.Instance.UserIndigo}.fav")
        If File.Exists(automaticReloadPath) Then
            File.Delete(automaticReloadPath)
        End If

        Using ms As New MemoryStream()
            Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(AutomaticReload))
            Dim dictionaryCustom = New AutomaticReload
            dictionaryCustom.Name = NameOf(AutomaticReload)
            dictionaryCustom.Value = Me.AutomaticReload
            xs.Serialize(ms, dictionaryCustom)
            File.WriteAllText(automaticReloadPath, System.Text.Encoding.UTF8.GetString(ms.ToArray()).Trim())
        End Using

    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmDashBoardPharmacy control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExecuteTimer()

        Dim automaticReloadPath As String = String.Format("{0}\Pharmacy\{1}\{2}", Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDTbAutomaticReload), $"{NameOf(INDTbAutomaticReload)}.IndigoUser_{SessionValues.Instance.UserIndigo}.fav")

        If File.Exists(automaticReloadPath) AndAlso Not String.IsNullOrEmpty(File.ReadAllText(automaticReloadPath)) Then
            Dim body As String = File.ReadAllText(automaticReloadPath).Trim()

            Using ms As MemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(body))
                Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(AutomaticReload))
                Dim dictionaryAutomaticReload = CType(xs.Deserialize(ms), AutomaticReload)
                Me.AutomaticReload = dictionaryAutomaticReload.Value
            End Using
        End If

        _idOperativeUnit = BarraBotones.OperatingUnitValue
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra("344")
        INDviewSearchCareCenter.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)

        _presenter = New PDashBoardPharmacy(Me)
        Me.ToolBar.Hide()
        Using model As New MDashBoardPharmacy(Me.Tag)
            FunctionalUnitDatasource = model.LoadCareCenter()
        End Using
        Dim ListActionsRequest As New List(Of eAcciones)
        Dim ListActionsReturn As New List(Of eAcciones)

        'obtengo los permisos de la barra para el formulario de solicitudes
        BarraBotones.ActualizarPermisosBarra("322")

        ListActionsRequest.Add(eAcciones.Process)
        IndigoGridView7.SetListAcction(INDviewMixingStationDetail, ListActionsRequest, True, False)

        If BarraBotones.PermissionsForm.ContainsKey(8) Then
            ListActionsRequest.Add(eAcciones.Annular)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(143) Then
            Me.INDBbMakeRequest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            ListActionsRequest.Add(eAcciones.MakeRequest)
        End If

        ListActionsReturn.Add(eAcciones.Process)
        BarraBotones.ActualizarPermisosBarra("1516")
        If BarraBotones.PermissionsForm.ContainsKey(8) Then
            ListActionsReturn.Add(eAcciones.Annular)
        End If

        IndigoGridView1.SetListAcction(INDGvRequest, ListActionsRequest, True, False)
        IndigoGridView2.SetListAcction(INDGvReturn, ListActionsReturn, True, False)
        IndigoGridView3.SetListAcction(INDgvPaquetes, ListActionsRequest, True, False)
        IndigoGridView4.SetListAcction(INDGvExtramural, ListActionsRequest, True, False)
        IndigoGridView5.SetListAcction(INDviewChemoterapy, ListActionsRequest, True, False)

        INDGvRequest.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvRequest.Columns.ColumnByName("colActions").Width = 150

        INDGvReturn.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvReturn.Columns.ColumnByName("colActions").Width = 150

        INDgvPaquetes.Columns.ColumnByName("colActions").Caption = "Acción"
        INDgvPaquetes.Columns.ColumnByName("colActions").Width = 90

        INDGvExtramural.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvExtramural.Columns.ColumnByName("colActions").Width = 90

        INDviewChemoterapy.Columns.ColumnByName("colActions").Caption = "Acción"
        INDviewChemoterapy.Columns.ColumnByName("colActions").Width = 90

        INDviewMixingStationDetail.Columns.ColumnByName("colActions").Caption = "Acción"
        INDviewMixingStationDetail.Columns.ColumnByName("colActions").Width = 100
        INDviewMixingStationDetail.Columns.ColumnByName("colActions").ShowColumn(INDviewMixingStationDetail.Columns.Count - 1)

        CleanControls()
        GetDateServer()

        'Se obtiene el reporte de acuerdo a los parámetros de inventarios
        Await LoadParameters()
        If _settingInventory IsNot Nothing AndAlso _settingInventory.PharmacyDashboardReport = 2 Then
            reportDef = New Reporter.rptPharmaceuticalDispensing
            reportDefDevolution = New Reporter.rptPharmaceuticalDispensingDevolutionReport

            Dim width = INDLcMainData.Width * 0.5
            Me.INDLciReportViewer.MinSize = New System.Drawing.Size(width, 25)
            Me.INDLciReportViewer.MaxSize = New System.Drawing.Size(width, 0)
        Else
            reportDef = New Reporter.rptPharmaceuticalDispensingNeckBandProcess
            reportDefDevolution = New Reporter.rptPharmaceuticalDispensingDevolutionReducedProcces
        End If

    End Sub

    ''' <summary>
    ''' Handles the Shown event of the FrmDashBoardPharmacy control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDashBoardPharmacy_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDSleCareCenter.Properties.PopupFormMinSize = New System.Drawing.Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Properties.PopupFormSize = New System.Drawing.Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Focus()
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Try
            If INDGvRequest.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If

            AsyncLoader(True)

            Dim dashboardPharmacy = DirectCast(DirectCast(INDGvRequest.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacy)
            Me.closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            If tagGrid = "Annular" Then

                'se anula el registro sin necesidad de abrir el formulario
                Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                If ResultAnnulmentReason?.StateResult Then
                    Using model As New MDashBoardPharmacy(Me.Tag)
                        listDashboardPharmacyDetail = model.ListDashboardPharmacyDetail(dashboardPharmacy.ConsecutivoFarmacia, dashboardPharmacy.CodigoPaciente.Trim(), dashboardPharmacy.Ingreso.Trim())

                        If Not listDashboardPharmacyDetail?.Any() Then
                            listDashboardPharmacyDetail.Add(New Domain.Crystal.Entities.ViewDashboardPharmacyDetail With
                                {
                                    .Ingreso = dashboardPharmacy.Ingreso.Trim(),
                                    .CodigoPaciente = dashboardPharmacy.CodigoPaciente.Trim(),
                                    .CareCenterCode = dashboardPharmacy.CodigoCentroAtencion.Trim(),
                                    .FunctionUnitCode = dashboardPharmacy.UFUCODIGO.Trim(),
                                    .ConsecutivePescription = If(dashboardPharmacy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacy.ConsecutivoPrescripcion)),
                                    .ConsecutiveInputs = dashboardPharmacy.ConsecutivoInsumos,
                                    .ConsecutivePharmacy = dashboardPharmacy.ConsecutivoFarmacia,
                                    .Consecutivo = dashboardPharmacy.ConsecutivoFarmacia,
                                    .HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB,
                                    .Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                                })
                        Else
                            For Each item In listDashboardPharmacyDetail
                                item.PattientSupplyOrder = 1
                                item.AdmissionNumeber = dashboardPharmacy.Ingreso.Trim()
                                item.PattientCode = dashboardPharmacy.CodigoPaciente.Trim()
                                item.CareCenterCode = dashboardPharmacy.CodigoCentroAtencion.Trim()
                                item.FunctionUnitCode = dashboardPharmacy.UFUCODIGO.Trim()
                                item.ConsecutivePescription = If(dashboardPharmacy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacy.ConsecutivoPrescripcion))
                                item.ConsecutiveInputs = dashboardPharmacy.ConsecutivoInsumos
                                item.ConsecutivePharmacy = dashboardPharmacy.ConsecutivoFarmacia
                                item.Action = 2
                                item.CantidadEntregada = 0
                                item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                            Next
                        End If

                        Dim result = Await model.SaveDashBoardPharmacy(Nothing, listDashboardPharmacyDetail, 0)
                        If result.StateResult Then
                            GetRequest()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End Using

                    AsyncLoader(False)
                End If
            ElseIf tagGrid = "Process" Then
                Me.Cursor = ChangeCursorIndigo()

                If Not Await LockRecord(dashboardPharmacy.ConsecutivoFarmacia) Then
                    Exit Sub
                End If

                Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = Nothing
                Using model As New MDashBoardPharmacy(Me.Tag)
                    Dim resultFunctionalUnit = model.ValidateFunctionalUnitDashBoard(dashboardPharmacy.Ingreso.Trim(), dashboardPharmacy.CodigoPaciente.Trim(), dashboardPharmacy.UFUCODIGO.Trim(), dashboardPharmacy.Row)
                    If resultFunctionalUnit.StateResult Then
						If resultFunctionalUnit.ObjectEmbbeded.Trim() <> dashboardPharmacy.UFUCODIGO.Trim() Then
                            If MessageIndigo.Show("El paciente se encuentra en una unidad funcional diferente. ¿Desea actualizar la unidad funcional?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                                Using modelFunctionalUnit As New MFunctionalUnit(Me.Tag)
                                    functionalUnit = modelFunctionalUnit.GetFuncUnit(resultFunctionalUnit.ObjectEmbbeded)
                                    If functionalUnit IsNot Nothing AndAlso functionalUnit.Id = 0 Then
                                        Mensaje(EeventViewerImages.Advertencia) = "La unidad funcional " + resultFunctionalUnit.ObjectEmbbeded + " no esta homologada en VIE"
                                        FinishRecord()
                                        Exit Sub
                                    End If
                                End Using

                                Dim resultUpdateFunctionalUnit = model.UpdateFunctionalUnitCrystal(dashboardPharmacy.ConsecutivoFarmacia, resultFunctionalUnit.ObjectEmbbeded)

                                If resultUpdateFunctionalUnit.StateResult = False Then
                                    Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error actulizando la unidad funcional en Crystal"
                                    FinishRecord()
                                    Exit Sub
                                End If
                            Else
                                FinishRecord()
								Exit Sub
							End If
						End If
					Else
                        Mensaje(EeventViewerImages.Advertencia) = resultFunctionalUnit.Message
                        FinishRecord()
                        Exit Sub
                    End If
                End Using

                OpenPopUpDashBoardPharmacyRequest(dashboardPharmacy, functionalUnit, typeFilters)

            ElseIf tagGrid = "MakeRequest" Then
                Me.RequestCrystalForm(dashboardPharmacy.CodigoPaciente.Trim(), dashboardPharmacy.Ingreso.Trim(), dashboardPharmacy.CodigoCentroAtencion.Trim(), dashboardPharmacy.UFUCODIGO.Trim())
                AsyncLoader(False)
            End If
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        Try
            If INDGvExtramural.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If

            AsyncLoader(True)

            Dim dashboardPharmacyExtramural = DirectCast(DirectCast(INDGvExtramural.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacy)
            closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            If tagGrid = "Annular" Then
                'se anula el registro sin necesidad de abrir el formulario
                Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                If ResultAnnulmentReason?.StateResult Then
                    Using model As New MDashBoardPharmacy(Me.Tag)
                        listDashboardPharmacyDetail = model.ListDashboardPharmacyDetail(dashboardPharmacyExtramural.ConsecutivoFarmacia, dashboardPharmacyExtramural.CodigoPaciente.Trim(), dashboardPharmacyExtramural.Ingreso.Trim())
                        If listDashboardPharmacyDetail.Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para anular"
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        For Each item In listDashboardPharmacyDetail
                            item.PattientSupplyOrder = 1
                            item.AdmissionNumeber = dashboardPharmacyExtramural.Ingreso.Trim()
                            item.PattientCode = dashboardPharmacyExtramural.CodigoPaciente.Trim()
                            item.CareCenterCode = dashboardPharmacyExtramural.CodigoCentroAtencion.Trim()
                            item.FunctionUnitCode = dashboardPharmacyExtramural.UFUCODIGO.Trim()
                            item.ConsecutivePescription = If(dashboardPharmacyExtramural.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacyExtramural.ConsecutivoPrescripcion))
                            item.ConsecutiveInputs = dashboardPharmacyExtramural.ConsecutivoInsumos
                            item.ConsecutivePharmacy = dashboardPharmacyExtramural.ConsecutivoFarmacia
                            item.Action = 2
                            item.CantidadEntregada = 0
                            item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                            item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                        Next
                        Dim result = Await model.SaveDashBoardPharmacy(Nothing, listDashboardPharmacyDetail, 0)
                        If result.StateResult Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                            GetRequest()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End Using

                    AsyncLoader(False)
                End If
            Else
                Me.Cursor = ChangeCursorIndigo()

                If Not Await LockRecord(dashboardPharmacyExtramural.ConsecutivoFarmacia) Then
                    Exit Sub
                End If

                OpenPopUpDashBoardPharmacyRequest(dashboardPharmacyExtramural, Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Try
            If INDGvReturn.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If

            AsyncLoader(True)

            Dim dashboardPharmacyDevolution = DirectCast(DirectCast(INDGvReturn.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacyDevolution)
            Me.closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            If tagGrid = "Annular" Then
                listDashboardPharmacyDetail = New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution)

                'metodo de anulacion, muestra razon de anulacion y mensaje
                Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                If ResultAnnulmentReason?.StateResult Then
                    Using model As New MDashBoardPharmacy(Me.Tag)
                        '''obtiene los datos de la consulta
                        Dim collect As XPCollection(Of ViewDashboardPharmacyDetailDevolution)

                        ''se consultan los items segun el filtro
                        If String.IsNullOrEmpty(typeFilters) Then
                            collect = model.ListDashBoardPharmacyDetailDevolutionCollection(dashboardPharmacyDevolution.ConsecutivoDevoluciones, dashboardPharmacyDevolution.CodigoPaciente.Trim(), dashboardPharmacyDevolution.Ingreso.Trim())
                        Else
                            collect = model.ListDashBoardPharmacyDetailDevolutionByTypeCollection(dashboardPharmacyDevolution.ConsecutivoDevoluciones, dashboardPharmacyDevolution.CodigoPaciente.Trim(), dashboardPharmacyDevolution.Ingreso.Trim(), typeFilters)
                        End If

                        ''por cada item en la solicitud se agregan a la lista
                        For Each item In collect
                            Dim detail As New Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution
                            With detail
                                .Consecutivo = item.Consecutivo
                                .CodigoPacienteDevolucion = item.CodigoPacienteDevolucion
                                .Ingreso = item.Ingreso
                                .CODCENATE = item.CODCENATE
                                .UFUCODIGO = item.UFUCODIGO
                                .CODPROSAL = item.CODPROSAL
                                .CODPRODUC = item.CODPRODUC
                                .CantidadDevuelta = item.CantidadDevuelta
                                .PROESTADO = item.PROESTADO
                                .FECRESGIS = item.FECRESGIS
                                .CODUSUARI = item.CODUSUARI
                                .NOPOS = item.NOPOS
                                .Entidad = item.Entidad
                                .Producto = item.Producto
                                .ContratoPlan = item.ContratoPlan
                                .Tipo = IIf(item.Custody, "4", item.Tipo)
                                .CantidadPendiente = item.CantidadPendiente
                                .Medico = item.Medico
                                .Especialidad = item.Especialidad
                                .EntityId = item.EntityId
                                .EntityName = item.EntityName
                                .CantidadFisico = item.CantidadFisico
                                .FinalProductId = item.FinalProductId
                                .FinalProductCode = item.FinalProductCode
                                .FinalProductName = item.FinalProductName
                                .HCDEVMEDDId = item.HCDEVMEDDId
                                .BatchCode = item.BatchCode
                                .Custody = item.Custody
                                .Action = 2
                                .QuantityReceived = 0
                                .HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                .Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                            End With
                            listDashboardPharmacyDetail.Add(detail)
                        Next

                        Dim result = Await model.SaveDashBoardPharmacyDevolution(Nothing, listDashboardPharmacyDetail, 0)

                        If result.StateResult Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                            GetReturn()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End Using
                    AsyncLoader(False)
                End If
            Else
                Me.Cursor = ChangeCursorIndigo()

                If Not Await LockRecord(dashboardPharmacyDevolution.ConsecutivoDevoluciones) Then
                    Exit Sub
                End If

                OpenPopUpDashBoardPharmacyReturn(dashboardPharmacyDevolution, typeFilters)
            End If
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        Try
            If INDgvPaquetes.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If

            AsyncLoader(True)

            Dim SurgicalPackage = DirectCast(DirectCast(INDgvPaquetes.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacy_SurgicalPackage)
            Me.closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            Select Case tagGrid
                Case "Annular"
                    'se anula el registro sin necesidad de abrir el formulario
                    Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils)
                    'metodo de anulacion, muestra razon de anulacion y mensaje
                    Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                    If ResultAnnulmentReason?.StateResult Then
                        Using model As New MDashBoardPharmacy(Me.Tag)
                            listDashboardPharmacyDetail = model.ListDashboardPharmacyDetailSurgicalPackage(SurgicalPackage.ConsecutivoFarmacia, SurgicalPackage.CodigoPaciente.Trim())
                            If listDashboardPharmacyDetail.Count = 0 Then
                                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para anular"
                                AsyncLoader(False)
                                Exit Sub
                            End If

                            For Each item In listDashboardPharmacyDetail
                                item.PattientSupplyOrder = 1
                                item.AdmissionNumeber = SurgicalPackage.Ingreso.Trim()
                                item.PattientCode = SurgicalPackage.CodigoPaciente.Trim()
                                item.CareCenterCode = SurgicalPackage.CodigoCentroAtencion.Trim()
                                item.FunctionUnitCode = SurgicalPackage.UFUCODIGO.Trim()
                                item.ConsecutivePescription = If(SurgicalPackage.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(SurgicalPackage.ConsecutivoPrescripcion))
                                item.ConsecutiveInputs = SurgicalPackage.ConsecutivoInsumos
                                item.ConsecutivePharmacy = SurgicalPackage.ConsecutivoFarmacia
                                item.Action = 2
                                item.CantidadEntregada = 0
                                item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                            Next
                            Dim result = Await model.SaveDashboardPharmacySurgicalPackage(Nothing, listDashboardPharmacyDetail, 0)
                            If result.StateResult Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                                getSurgicalPackage()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                            End If
                        End Using

                        AsyncLoader(False)
                    End If

                Case "Process"
                    Me.Cursor = ChangeCursorIndigo()

                    If Not Await LockRecord(SurgicalPackage.ConsecutivoFarmacia) Then
                        Exit Sub
                    End If

                    If String.IsNullOrEmpty(SurgicalPackage.CentroCostos.Trim()) Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("No se encuentra parametrizado un centro de costo en el concepto de facturación del CUPS {0} para la unidad funcional {1}. Si ya actualizaste por favor refresca la rejilla", SurgicalPackage.Procedimiento.Trim(), SurgicalPackage.UFUCODIGO.Trim())
                        FinishRecord()
                        Exit Sub
                    End If

                    OpenPopUpDashBoardPharmacySurgycalPackage(SurgicalPackage, typeFilters)
                    AsyncLoader(False)
                Case "MakeRequest"
                    Me.RequestCrystalForm(SurgicalPackage.CodigoPaciente.Trim(), SurgicalPackage.Ingreso.Trim(), SurgicalPackage.CodigoCentroAtencion.Trim(), SurgicalPackage.UFUCODIGO.Trim())
                    AsyncLoader(False)
            End Select
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Menu de la rejilla de quimioterapias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridView5_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView5.Click_ButtonAction, IndigoGridView5.ContexMenuActions
        Try
            If INDviewChemoterapy.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If

            AsyncLoader(True)

            Dim dashboardPharmacyChemotherapy = DirectCast(DirectCast(INDviewChemoterapy.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacyChemotherapy)
            Me.closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            Select Case tagGrid
                Case "Annular"
                    'se anula el registro sin necesidad de abrir el formulario
                    Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
                    'metodo de anulacion, muestra razon de anulacion y mensaje
                    Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                    If ResultAnnulmentReason?.StateResult Then
                        Using model As New MDashBoardPharmacy(Me.Tag)
                            listDashboardPharmacyDetail = model.ListDashboardPharmacyDetail(dashboardPharmacyChemotherapy.ConsecutivoFarmacia, dashboardPharmacyChemotherapy.CodigoPaciente.Trim(), dashboardPharmacyChemotherapy.Ingreso.Trim())
                            If listDashboardPharmacyDetail.Count = 0 Then
                                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para anular"
                                AsyncLoader(False)
                                Exit Sub
                            End If

                            For Each item In listDashboardPharmacyDetail
                                item.PattientSupplyOrder = 1
                                item.AdmissionNumeber = dashboardPharmacyChemotherapy.Ingreso.Trim()
                                item.PattientCode = dashboardPharmacyChemotherapy.CodigoPaciente.Trim()
                                item.CareCenterCode = dashboardPharmacyChemotherapy.CodigoCentroAtencion.Trim()
                                item.FunctionUnitCode = dashboardPharmacyChemotherapy.UFUCODIGO.Trim()
                                item.ConsecutivePescription = If(dashboardPharmacyChemotherapy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacyChemotherapy.ConsecutivoPrescripcion))
                                item.ConsecutiveInputs = dashboardPharmacyChemotherapy.ConsecutivoInsumos
                                item.ConsecutivePharmacy = dashboardPharmacyChemotherapy.ConsecutivoFarmacia
                                item.Action = 2
                                item.CantidadEntregada = 0
                                item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                            Next
                            Dim result = Await model.SaveDashBoardPharmacy(Nothing, listDashboardPharmacyDetail, 0)
                            If result.StateResult Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                                GetChemotherapy()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                            End If
                        End Using

                        AsyncLoader(False)
                    End If
                Case "Process"
                    Me.Cursor = ChangeCursorIndigo()

                    If Not Await LockRecord(dashboardPharmacyChemotherapy.ConsecutivoFarmacia) Then
                        Exit Sub
                    End If

                    Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = Nothing
                    Using model As New MDashBoardPharmacy(Me.Tag)
                        Dim resultFunctionalUnit = model.ValidateFunctionalUnitDashBoard(dashboardPharmacyChemotherapy.Ingreso.Trim(), dashboardPharmacyChemotherapy.CodigoPaciente.Trim(), dashboardPharmacyChemotherapy.UFUCODIGO.Trim(), dashboardPharmacyChemotherapy.Row)
                        If resultFunctionalUnit.StateResult Then
                            If resultFunctionalUnit.ObjectEmbbeded.Trim() <> dashboardPharmacyChemotherapy.UFUCODIGO.Trim() Then
                                If MessageIndigo.Show("El paciente se encuentra en una unidad funcional diferente. ¿Desea actualizar la unidad funcional?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                                    Using modelFunctionalUnit As New MFunctionalUnit(Me.Tag)
                                        functionalUnit = modelFunctionalUnit.GetFuncUnit(resultFunctionalUnit.ObjectEmbbeded)
                                        If functionalUnit IsNot Nothing AndAlso functionalUnit.Id = 0 Then
                                            Mensaje(EeventViewerImages.Advertencia) = "La unidad funcional " + resultFunctionalUnit.ObjectEmbbeded + " no esta homologada en VIE"
                                            FinishRecord()
                                            Exit Sub
                                        End If
                                    End Using

                                    Dim resultUpdateFunctionalUnit = model.UpdateFunctionalUnitCrystal(dashboardPharmacyChemotherapy.ConsecutivoFarmacia, resultFunctionalUnit.ObjectEmbbeded)

                                    If resultUpdateFunctionalUnit.StateResult = False Then
                                        Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un Error actualizando la unidad funcional en Crystal"
                                        FinishRecord()
                                        Exit Sub
                                    End If
                                Else
                                    FinishRecord()
                                    Exit Sub
                                End If
                            End If
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = resultFunctionalUnit.Message
                            FinishRecord()
                            Exit Sub
                        End If

                        If dashboardPharmacyChemotherapy IsNot Nothing AndAlso dashboardPharmacyChemotherapy.IDCITA > 0 Then
                            Dim _CitaCancelaReprograma = model.CitaCanceladaReprogramaXPInstant(dashboardPharmacyChemotherapy.IDCITA)
                            If _CitaCancelaReprograma IsNot Nothing Then
                                Dim _mensaje As String = String.Empty
                                If _CitaCancelaReprograma.TIPO = 1 Then
                                    _mensaje = String.Format("La cita fue cancelada, el {0} por {1}, motivo {2}, ¿desea continuar con la dispensación?", _CitaCancelaReprograma.FECHA, _CitaCancelaReprograma.USUARIO, _CitaCancelaReprograma.MOTIVO)
                                Else
                                    _mensaje = String.Format("La cita fue reprogramada para el día {0} por {1}, motivo {2}, ¿desea continuar con la dispensación?", _CitaCancelaReprograma.FECHA, _CitaCancelaReprograma.USUARIO, _CitaCancelaReprograma.MOTIVO)
                                End If
                                If MessageIndigo.Show(_mensaje, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                                Else
                                    FinishRecord()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End Using

                    OpenPopUpDashBoardPharmacyChemotherapy(dashboardPharmacyChemotherapy, functionalUnit, typeFilters)

                Case "MakeRequest"
                    Me.RequestCrystalForm(dashboardPharmacyChemotherapy.CodigoPaciente.Trim(), dashboardPharmacyChemotherapy.Ingreso.Trim(), dashboardPharmacyChemotherapy.CodigoCentroAtencion.Trim(), dashboardPharmacyChemotherapy.UFUCODIGO.Trim())
                    AsyncLoader(False)
            End Select
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub IndigoGridView7_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView7.Click_ButtonAction, IndigoGridView7.ContexMenuActions
        Try
            If INDviewMixingStationDetail.GetSelectedRows.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                AsyncLoader(False)
                Exit Sub
            End If

            AsyncLoader(True)

            Dim dashboardPharmacyMixingStation
            Dim focusedRowHandle As Integer = INDviewMixingStation.FocusedRowHandle

            If focusedRowHandle >= 0 Then
                Dim mixingStationDetail As Object = INDviewMixingStation.GetRow(focusedRowHandle)

                If mixingStationDetail IsNot Nothing Then
                    Dim gridViewDetail As GridView = TryCast(INDviewMixingStation.GetDetailView(focusedRowHandle, 0), GridView)

                    If gridViewDetail IsNot Nothing Then
                        Dim dashboardPharmacyMixingStatio As Object = gridViewDetail.GetFocusedRow()

                        If dashboardPharmacyMixingStatio IsNot Nothing Then
                            dashboardPharmacyMixingStation = dashboardPharmacyMixingStatio
                        End If
                    End If
                End If
            End If

            Me.closeReportViewer()

            'Se obtiene si la forma en como se accede a este evento es con buttonAction o contextMenuActions
            Dim tagGrid As String = ""
            If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
                tagGrid = sender.Tag.ToString()
            Else
                tagGrid = sender.Text
            End If

            If tagGrid <> "Annular" Then
                Me.Cursor = ChangeCursorIndigo()

                If Not Await LockRecord(dashboardPharmacyMixingStation.ConsecutivoFarmacia) Then
                    Exit Sub
                End If

                Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = Nothing
                Using model As New MDashBoardPharmacy(Me.Tag)
                    Dim resultFunctionalUnit = model.ValidateFunctionalUnitDashBoard(dashboardPharmacyMixingStation.Ingreso.Trim(), dashboardPharmacyMixingStation.CodigoPaciente.Trim(), dashboardPharmacyMixingStation.UFUCODIGO.Trim(), dashboardPharmacyMixingStation.Row)
                    If resultFunctionalUnit.StateResult Then
                        If resultFunctionalUnit.ObjectEmbbeded.Trim() <> dashboardPharmacyMixingStation.UFUCODIGO.Trim() Then
                            If MessageIndigo.Show("El paciente se encuentra en una unidad funcional diferente. ¿Desea actualizar la unidad funcional?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                                Using modelFunctionalUnit As New MFunctionalUnit(Me.Tag)
                                    functionalUnit = modelFunctionalUnit.GetFuncUnit(resultFunctionalUnit.ObjectEmbbeded)
                                    If functionalUnit IsNot Nothing AndAlso functionalUnit.Id = 0 Then
                                        Mensaje(EeventViewerImages.Advertencia) = "La unidad funcional " + resultFunctionalUnit.ObjectEmbbeded + " no esta homologada en VIE"
                                        FinishRecord()
                                        Exit Sub
                                    End If
                                End Using

                                Dim resultUpdateFunctionalUnit = model.UpdateFunctionalUnitCrystal(dashboardPharmacyMixingStation.ConsecutivoFarmacia, resultFunctionalUnit.ObjectEmbbeded)

                                If resultUpdateFunctionalUnit.StateResult = False Then
                                    Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un Error actulizando la unidad funcional en Crystal"
                                    FinishRecord()
                                    Exit Sub
                                End If
                            Else
                                FinishRecord()
                                Exit Sub
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultFunctionalUnit.Message
                        FinishRecord()
                        Exit Sub
                    End If
                End Using

                OpenPopUpDashBoardPharmacyMixingStation(dashboardPharmacyMixingStation)
            End If
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "SelectedPageChanged"
    ''' <summary>
    ''' Metodo que se ejecuta al cambiar de una pestana a otra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTcgDashBoard_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgDashBoard.SelectedPageChanged
        If String.IsNullOrEmpty(CareCenterFilters) Then
            Exit Sub
        End If

        Me.closeReportViewer()
        DeleteBlockedRecord()
        Me.Cursor = ChangeCursorIndigo()

        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            INDBbiPrint.Enabled = True
            GetRequest()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 1 Then
            INDBbiPrint.Enabled = True
            GetExtramural()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 2 Then
            INDBbiPrint.Enabled = False
            GetReturn()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 3 Then
            INDBbiPrint.Enabled = False
            getSurgicalPackage()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then
            INDBbiPrint.Enabled = False
            GetChemotherapy()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 5 Then
            INDBbiPrint.Enabled = False
            If INDgcMixingStation.DataSource Is Nothing Then
                GetRequestMixingStation()
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#Region "ItemClick"
    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        Me.RefreshDatasource()
    End Sub

    ''' <summary>
    ''' Funcionalidad boton solicitar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbMakeRequest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbMakeRequest.ItemClick
        Using formulario As New FrmPopupSearchAdmission()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.27)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Text = "Ingreso"
            formulario.FormBorderStyle = FormBorderStyle.Sizable
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            formulario.Dispose()
        End Using
    End Sub
#End Region

#Region "CloseUp"

    Private Sub INDSleCareCenter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCareCenter.CloseUp
        'Se obtienen los códigos de centro de atención seleccionados
        CareCenterFilters = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select "'" & DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE & "'"))

        If String.IsNullOrEmpty(CareCenterFilters) Then
            INDSleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        ElseIf INDviewSearchCareCenter.GetSelectedRows().Count() = 1 Then
            INDSleCareCenter.Properties.NullText = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.NOMCENATE.ToString().Trim()))
        Else
            INDSleCareCenter.Properties.NullText = INDviewSearchCareCenter.GetSelectedRows().Count().ToString() + " Item Seleccionados"
        End If

        Me.closeReportViewer(True)
        Me.Cursor = ChangeCursorIndigo()
        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            GetRequest()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 1 Then
            GetExtramural()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 2 Then
            GetReturn()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 3 Then
            getSurgicalPackage()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then
            GetChemotherapy()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 5 Then
            GetRequestMixingStation()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        allowTypeFilter()
    End Sub

    ''' <summary>
    ''' Evento al cerrar popup de tipos de filtro para solicitudes intrahospitalarias
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleTypeFilter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleTypeFilter.CloseUp
        'Limpiar variables de tipo de filtro
        medicamentos = Nothing
        insumos = Nothing
        medicamentos_insumos = Nothing

        If INDgvSearchType?.GetSelectedRows()?.Any() AndAlso Not INDgvSearchType?.GetSelectedRows()?.Contains(0) Then
            typeFilters = String.Join(",", INDgvSearchType?.GetSelectedRows())
            INDSleTypeFilter.Properties.NullText = INDgvSearchType?.GetSelectedRows?.Count().ToString() + " Item Seleccionados"
        Else
            CleanFilter()
        End If

        ''se hace la consulta segun los filtros seleccionados
        ''en caso de que este la opcion todos seleccionada, vuelve a tomar el datasource regular
        If INDgvSearchType?.GetSelectedRows()?.ToList()?.Contains(0) Then
            INDBbiRefresh_ItemClick(Nothing, Nothing)
            Exit Sub
        End If

        Dim selectedOptions = INDgvSearchType?.GetSelectedRows()?.ToList()

        If selectedOptions.Contains(1) Then
            medicamentos = 1
        End If
        If selectedOptions.Contains(2) Then
            insumos = 1
        End If
        If selectedOptions.Contains(3) Then
            medicamentos_insumos = 1
        End If
        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            GetRequest()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 1 Then
            GetExtramural()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 2 Then
            GetReturn()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 3 Then
            getSurgicalPackage()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then
            GetChemotherapy()
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Evento al cerrar al abrir el popup para que ponga por defecto el item de "Todos"
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleTypeFilter_Popup(sender As Object, e As EventArgs) Handles INDSleTypeFilter.QueryPopUp
        If INDSleTypeFilter.Properties.DataSource Is Nothing Then
            allowTypeFilter()
        End If
    End Sub

    ''' <summary>
    ''' Evento al limpiar el combo de filtros para que ponga por defecto el item de "Todos"
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleTypeFilter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTypeFilter.EditValueChanged
        If String.IsNullOrEmpty(INDSleTypeFilter.EditValue) Then
            CleanFilter()
        End If
        INDSleTypeFilter_CloseUp(Nothing, Nothing)
    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Metodo para ejecutar consulta cuando es con filtro
    ''' </summary>
    Private Sub dataWithFilters()
        If INDSleTypeFilter.Properties.DataSource Is Nothing Then
            allowTypeFilter()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MSettingInventory(Me.Tag)
            'parámetros de inventario
            Dim result = Await Model.GetInventorySettingsRegister(_idOperativeUnit)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If
            _settingInventory = result.ObjectEmbbeded
            If _settingInventory Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If
        End Using
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDTcgDashBoard.SelectedTabPageIndex = 0
        allowTypeFilter()
        CleanFilter()
    End Sub

    ''' <summary>
    ''' limpia el control del filtro
    ''' </summary>
    Private Sub CleanFilter()
        typeFilters = String.Empty
        INDgvSearchType.SelectRow(0)
        INDgvSearchType?.
            GetSelectedRows()?.
            ToList()?.
            FindAll(Function(x) x <> 0).
            ForEach(Sub(y)
                        INDgvSearchType.UnselectRow(y)
                    End Sub)
        INDgvSearchType.FocusedRowHandle = 0
        INDSleTypeFilter.Properties.NullText = "Todos"
    End Sub

    Private Sub closeReportViewer(Optional close As Boolean = False)
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLycReportPhamacyNotes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If close Then
            If _frmReportViewer?.DocViewer?.DocumentSource IsNot Nothing Then
                _frmReportViewer.DocViewer.DocumentSource = Nothing
            End If

            If _frmReportViewerPharmacyNotes?.DocViewer?.DocumentSource IsNot Nothing Then
                _frmReportViewerPharmacyNotes.DocViewer.DocumentSource = Nothing
            End If
        Else
            _frmReportViewer.DocViewer.Document = Nothing
            _frmReportViewerPharmacyNotes.DocViewer.Document = Nothing
        End If


    End Sub

    ''' <summary>
    ''' metodo para obtener las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRequest()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details As XPInstantFeedbackSource
            ''validacion para que si tiene filtros actualice con estos
            If medicamentos IsNot Nothing OrElse insumos IsNot Nothing OrElse medicamentos_insumos IsNot Nothing Then
                details = model.ListDashBoardPharmacyByTypeXPInstant(CareCenterFilters, medicamentos, insumos, medicamentos_insumos)
            Else
                details = model.ListDashBoardPharmacyXPInstant(CareCenterFilters)
            End If
            INDGcRequest.DataSource = details
        End Using
    End Sub


    ''' <summary>
    ''' metodo para obtener las solicitudes que tienen proceso en central de mezclas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRequestMixingStation()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details = model.ListDashBoardPharmacyMixingStationPatientXPInstant(CareCenterFilters)
            INDgcMixingStation.DataSource = details
            datasourceMixingSationDetail = model.ListDashBoardPharmacyMixingStationXPCollection(CareCenterFilters).ToList()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener las quimioterapias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChemotherapy()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details As XPInstantFeedbackSource
            ''validacion para que si tiene filtros actualice con estos
            If medicamentos IsNot Nothing OrElse insumos IsNot Nothing OrElse medicamentos_insumos IsNot Nothing Then
                details = model.ListDashBoardPharmacyChemotherapyByTypeXPInstant(CareCenterFilters, medicamentos, insumos, medicamentos_insumos)
            Else
                details = model.ListDashBoardPharmacyChemotherapyXPInstant(CareCenterFilters)
            End If
            INDgcChemotherapy.DataSource = details
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener las solicitudes extramurales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetExtramural()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details As XPInstantFeedbackSource
            ''validacion para que si tiene filtros actualice con estos
            If medicamentos IsNot Nothing OrElse insumos IsNot Nothing OrElse medicamentos_insumos IsNot Nothing Then
                details = model.ListDashBoardPharmacyByTypeXPInstantExtramural(CareCenterFilters, medicamentos, insumos, medicamentos_insumos)
            Else
                details = model.ListDashBoardPharmacyXPInstantExtramural(CareCenterFilters)
            End If
            INDGcExtramural.DataSource = details
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener las devoluciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetReturn()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details As XPInstantFeedbackSource
            ''validacion para que si tiene filtros actualice con estos
            If medicamentos IsNot Nothing OrElse insumos IsNot Nothing OrElse medicamentos_insumos IsNot Nothing Then
                details = model.ListDashBoardPharmacyDevolutionByTypeXPInstant(CareCenterFilters, medicamentos, insumos, medicamentos_insumos)
            Else
                details = model.ListDashBoardPharmacyDevolutionXPInstant(CareCenterFilters)
            End If
            INDGcReturn.DataSource = details
        End Using
    End Sub
    ''' <summary>
    ''' metodo que carga los paciente para cirugias  - paquetes QX
    ''' </summary>
    Private Sub getSurgicalPackage()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details As XPInstantFeedbackSource
            ''validacion para que si tiene filtros actualice con estos
            If medicamentos IsNot Nothing OrElse insumos IsNot Nothing OrElse medicamentos_insumos IsNot Nothing Then
                details = model.ListDashBoardPharmacy_SurgicalPackageByTypeXPInstant(CareCenterFilters, medicamentos, insumos, medicamentos_insumos)
            Else
                details = model.ListDashBoardPharmacy_SurgicalPackageXPInstant(CareCenterFilters)
            End If
            INDgcPaquetes.DataSource = details
            INDgcPaquetes.RefreshDataSource()
            INDgvPaquetes.ExpandAllGroups()
        End Using
    End Sub

    ''' <summary>
    ''' metodo que habilita el combo de tipo de agrupacion en soliitudes intrah.
    ''' </summary>
    Private Sub allowTypeFilter()
        If INDTcgDashBoard.SelectedTabPageIndex <> 5 Then
            INDLycTypeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ''datasource para el combo de filtro en solicitudes intrahospitalarias
            Dim filterOptions = New List(Of Tuple(Of Int16, String))
            filterOptions.Add(New Tuple(Of Int16, String)(0, "Todos"))
            filterOptions.Add(New Tuple(Of Int16, String)(1, "Medicamentos"))
            filterOptions.Add(New Tuple(Of Int16, String)(2, "Insumos"))
            filterOptions.Add(New Tuple(Of Int16, String)(3, "Medicamentos tipo insumos"))
            INDSleTypeFilter.Properties.DataSource = filterOptions
        Else
            INDLycTypeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

    End Sub

    ''' <summary>
    ''' funcion que ejecuta el form del EHR  de solicitudes
    ''' </summary>
    ''' <param name="PatientCode"></param>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="CareCenter"></param>
    ''' <param name="FunctionalUnitCode"></param>
    Private Sub RequestCrystalForm(PatientCode As String, AdmissionNumber As String, CareCenter As String, FunctionalUnitCode As String)
        Dim RequestCrystalForm As New IndigoeHistorias.frmHCSolicitudMedicamentosInsumos(PatientCode, AdmissionNumber, CareCenter, FunctionalUnitCode, Nothing, IndigoeHistorias.frmHCSolicitudMedicamentosInsumos.eORIGEN.DashboardPacientesEnfermeria)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        RequestCrystalForm.ShowDialog(Me)
        RequestCrystalForm.Dispose()
    End Sub

    ''' <summary>
    ''' creacion del timer, programado a 2 minutos
    ''' </summary>
    Private Sub ExecuteTimer()
        If _timer Is Nothing OrElse Not _timer.Enabled Then
            _timer = New System.Timers.Timer(120000)
            AddHandler _timer.Elapsed, AddressOf OnTimedEvent
            _timer.SynchronizingObject = Me
            _timer.AutoReset = False
            _timer.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' evento timer que llama a refrescar la rejilla
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        Me._flagTimer = True
        Me.RefreshDatasource()
        Me._flagTimer = False
    End Sub

    ''' <summary>
    ''' metodo que refresca el datasource ya sea cuando se da click o cuando el timer se active
    ''' </summary>
    Private Sub RefreshDatasource()
        Me.SafeInvoke(Sub()
                          If String.IsNullOrEmpty(CareCenterFilters) Then
                              Exit Sub
                          End If

                          If Not _flagTimer Then
                              Me.closeReportViewer(True)
                          End If

                          Me.Cursor = ChangeCursorIndigo()
                          DeleteBlockedRecord()

                          If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
                              GetRequest()
                          ElseIf INDTcgDashBoard.SelectedTabPageIndex = 1 Then
                              GetExtramural()
                          ElseIf INDTcgDashBoard.SelectedTabPageIndex = 2 Then
                              GetReturn()
                          ElseIf INDTcgDashBoard.SelectedTabPageIndex = 3 Then
                              getSurgicalPackage()
                          ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then
                              GetChemotherapy()
                          ElseIf INDTcgDashBoard.SelectedTabPageIndex = 5 Then
                              GetRequestMixingStation()
                          End If
                          Me.Cursor = System.Windows.Forms.Cursors.Default
                      End Sub)

    End Sub

#End Region

#Region "OpenPopUp"

    ''' <summary>
    ''' Apertura del FrmPopUpDashBoardPharmacyRequest para solicitudes Intrahospitalarias y ambulatorias
    ''' </summary>
    Private Sub OpenPopUpDashBoardPharmacyRequest(dashboardPharmacy As ViewDashBoardPharmacy, FunctionalUnit As Domain.Payroll.Entities.FunctionalUnit, typeFilters As String)
        Using FormRequest As New FrmPopUpDashBoardPharmacyRequest()
            AddHandler FormRequest.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess
            FormRequest.ViewModeEditHold = True
            FormRequest.PatientCode = dashboardPharmacy.CodigoPaciente.Trim()
            FormRequest.PatientName = dashboardPharmacy.NombrePaciente.Trim()
            FormRequest.BirthDay = dashboardPharmacy?.BirthDay
            FormRequest.Consecutive = dashboardPharmacy.ConsecutivoFarmacia
            FormRequest.AdmissionNumber = dashboardPharmacy.Ingreso.Trim()
            FormRequest.typeFilter = typeFilters

            If FunctionalUnit IsNot Nothing Then
                FormRequest.FunctionalUnitCode = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            Else
                FormRequest.FunctionalUnitCode = dashboardPharmacy.UFUCODIGO.Trim() + " - " + dashboardPharmacy.UnidadFuncional.Trim()
            End If

            FormRequest.Store = dashboardPharmacy.Bodega.Trim()
            FormRequest.CareCenter = dashboardPharmacy.CodigoCentroAtencion.Trim()
            FormRequest.ConsecutivePescription = If(dashboardPharmacy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacy.ConsecutivoPrescripcion))
            FormRequest.ConsecutiveInputs = dashboardPharmacy.ConsecutivoInsumos
            FormRequest.ConsecutivePharmacy = dashboardPharmacy.ConsecutivoFarmacia
            FormRequest.HistoryType = dashboardPharmacy.IDETIPHIS
            FormRequest.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9
            FormRequest.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.9
            FormRequest.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(FormRequest, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            If FormRequest.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                FinishRecord()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Apertura del FrmPopUpDashBoardPharmacyRequest para Quimioterapias
    ''' </summary>
    Private Sub OpenPopUpDashBoardPharmacyChemotherapy(dashboardPharmacyChemotherapy As ViewDashBoardPharmacyChemotherapy, FunctionalUnit As Domain.Payroll.Entities.FunctionalUnit, typeFilters As String)
        'dashboardPharmacyChemotherapy
        Using FormRequest As New FrmPopUpDashBoardPharmacyRequest()
            AddHandler FormRequest.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess

            FormRequest.ViewModeEditHold = True
            FormRequest.PatientCode = dashboardPharmacyChemotherapy.CodigoPaciente.Trim()
            FormRequest.PatientName = dashboardPharmacyChemotherapy.NombrePaciente.Trim()
            FormRequest.Consecutive = dashboardPharmacyChemotherapy.ConsecutivoFarmacia
            FormRequest.AdmissionNumber = dashboardPharmacyChemotherapy.Ingreso.Trim()

            If FunctionalUnit IsNot Nothing Then
                FormRequest.FunctionalUnitCode = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            Else
                FormRequest.FunctionalUnitCode = dashboardPharmacyChemotherapy.UFUCODIGO.Trim() + " - " + dashboardPharmacyChemotherapy.UnidadFuncional.Trim()
            End If

            FormRequest.Store = dashboardPharmacyChemotherapy.Bodega.Trim()
            FormRequest.CareCenter = dashboardPharmacyChemotherapy.CodigoCentroAtencion.Trim()
            FormRequest.ConsecutivePescription = If(dashboardPharmacyChemotherapy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacyChemotherapy.ConsecutivoPrescripcion))
            FormRequest.ConsecutiveInputs = dashboardPharmacyChemotherapy.ConsecutivoInsumos
            FormRequest.ConsecutivePharmacy = dashboardPharmacyChemotherapy.ConsecutivoFarmacia
            FormRequest.HistoryType = dashboardPharmacyChemotherapy.IDETIPHIS
            FormRequest.IDHCORDPRON = dashboardPharmacyChemotherapy.IDHCORDPRON
            FormRequest.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9
            FormRequest.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.9
            FormRequest.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(FormRequest, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            If FormRequest.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                FinishRecord()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Apertura del FrmPopUpDashBoardPharmacyReturn para Devolutivos
    ''' </summary>
    Private Sub OpenPopUpDashBoardPharmacyReturn(dashboardPharmacyDevolution As ViewDashBoardPharmacyDevolution, typeFilters As String)
        Using FormReturn = New FrmPopUpDashBoardPharmacyReturn()
            AddHandler FormReturn.DashBoardPharmacyReturnSuccessEventArgs, AddressOf DashBoardPharmacyReturnSuccess

            FormReturn.ViewModeEditHold = True
            FormReturn.ORIDEVMED = Convert.ToInt32(dashboardPharmacyDevolution.ORIDEVMED.ToString)
            FormReturn.PatientCode = dashboardPharmacyDevolution.CodigoPaciente.Trim()
            FormReturn.PatientName = dashboardPharmacyDevolution.NombrePaciente.Trim()
            FormReturn.BirthDay = dashboardPharmacyDevolution?.BirthDay
            FormReturn.Consecutive = dashboardPharmacyDevolution.ConsecutivoDevoluciones
            FormReturn.Admission = dashboardPharmacyDevolution.Ingreso.Trim()
            FormReturn.FunctionalUnit = dashboardPharmacyDevolution.UFUCODIGO.Trim() + " - " + dashboardPharmacyDevolution.UnidadFuncional.Trim()
            FormReturn.Store = dashboardPharmacyDevolution.Bodega.Trim()
            FormReturn.CareCenter = dashboardPharmacyDevolution.CodigoCentroAtencion.Trim()
            FormReturn.DevolutionOrigin = dashboardPharmacyDevolution.ORIDEVMED
            FormReturn.typeFilter = typeFilters

            FormReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(FormReturn, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            If FormReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                FinishRecord()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Apertura del FrmPopUpDashBoardPharmacySurgycalPackage para Paquetes quirúrgicos
    ''' </summary>
    Private Sub OpenPopUpDashBoardPharmacySurgycalPackage(SurgicalPackage As ViewDashBoardPharmacy_SurgicalPackage, typeFilters As String)
        Using FormSurgicalPackage = New FrmPopUpDashBoardPharmacySurgycalPackage
            AddHandler FormSurgicalPackage.DashBoardPharmacySurgicalPackageSuccessEventArgs, AddressOf DashBoardPharmacySurgicalPackageSuccess
            FormSurgicalPackage.ViewModeEditHold = True
            FormSurgicalPackage.PatientCode = SurgicalPackage.CodigoPaciente.Trim()
            FormSurgicalPackage.PatientName = SurgicalPackage.NombrePaciente.Trim()
            FormSurgicalPackage.BirthDay = SurgicalPackage?.BirthDay
            FormSurgicalPackage.Consecutive = SurgicalPackage.ConsecutivoFarmacia
            FormSurgicalPackage.AdmissionNumber = SurgicalPackage.Ingreso.Trim()
            FormSurgicalPackage.Procedimiento = SurgicalPackage.Procedimiento.Trim()
            FormSurgicalPackage.CostCenterId = SurgicalPackage.CostCenterId
            FormSurgicalPackage.FunctionalUnitCode = SurgicalPackage.UFUCODIGO.Trim() + " - " + SurgicalPackage.UnidadFuncional.Trim()
            FormSurgicalPackage.Store = String.Empty
            FormSurgicalPackage.CareCenter = SurgicalPackage.CodigoCentroAtencion.Trim()
            FormSurgicalPackage.ConsecutivePescription = If(SurgicalPackage.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(SurgicalPackage.ConsecutivoPrescripcion))
            FormSurgicalPackage.ConsecutiveInputs = SurgicalPackage.ConsecutivoInsumos
            FormSurgicalPackage.ConsecutivePharmacy = SurgicalPackage.ConsecutivoFarmacia
            FormSurgicalPackage.HistoryType = SurgicalPackage.IDETIPHIS
            FormSurgicalPackage.typeFilter = typeFilters

            FormSurgicalPackage.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(FormSurgicalPackage, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            If FormSurgicalPackage.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                FinishRecord()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Apertura del FrmPopUpDashBoardPharmacyMixingStation para Central de mezclas
    ''' </summary>
    Private Sub OpenPopUpDashBoardPharmacyMixingStation(dashboardPharmacyMixingStation As ViewDashBoardPharmacyMixingSation)
        Using FormMixingStation = New FrmPopUpDashBoardPharmacyMixingStation
            AddHandler FormMixingStation.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess
            FormMixingStation.ViewModeEditHold = True
            FormMixingStation.PatientCode = dashboardPharmacyMixingStation.CodigoPaciente.Trim()
            FormMixingStation.PatientName = dashboardPharmacyMixingStation.NombrePaciente.Trim()
            FormMixingStation.Consecutive = dashboardPharmacyMixingStation.ConsecutivoFarmacia
            FormMixingStation.AdmissionNumber = dashboardPharmacyMixingStation.Ingreso.Trim()
            FormMixingStation.FunctionalUnitCode = dashboardPharmacyMixingStation.UFUCODIGO.Trim() + " - " + dashboardPharmacyMixingStation.UnidadFuncional.Trim()
            FormMixingStation.Store = dashboardPharmacyMixingStation.Bodega.Trim()
            FormMixingStation.CareCenter = dashboardPharmacyMixingStation.CodigoCentroAtencion.Trim()
            FormMixingStation.ConsecutivePescription = If(dashboardPharmacyMixingStation.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacyMixingStation.ConsecutivoPrescripcion))
            FormMixingStation.ConsecutiveInputs = dashboardPharmacyMixingStation.ConsecutivoInsumos
            FormMixingStation.ConsecutivePharmacy = dashboardPharmacyMixingStation.ConsecutivoFarmacia
            FormMixingStation.HistoryType = dashboardPharmacyMixingStation.IDETIPHIS
            FormMixingStation.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9
            FormMixingStation.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.9

            FormMixingStation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(FormMixingStation, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)

            If FormMixingStation.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                FinishRecord()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Se finaliza el registro
    ''' </summary>
    Private Sub FinishRecord()
        Me.Cursor = ChageCursorDefault()
        DeleteBlockedRecord()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Se validan registros bloqueados
    ''' </summary>
    Private Async Function LockRecord(PharmacySequence As Decimal) As Task(Of Boolean)
        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
            Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PharmacySequence))
            If result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = CInt(PharmacySequence)}
                Dim operation = Await ModelRecord.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                record = result
                If record.CodUser <> Me.indigo.UserIndigo Then
                    Mensaje(EeventViewerImages.Advertencia) = "El registro se encuentra bloqueado por el usuario " + result.CodUser + " - " + result.NameUser + ", desde " + result.BlockDate
                    Me.Cursor = ChageCursorDefault()
                    AsyncLoader(False)

                    Return False
                End If
            End If

            Return True
        End Using
    End Function

#End Region

    Private Sub INDBbiPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrint.ItemClick
        Dim listId As Object = (From l In listDashBoardRequest Where l.SelectOption = True Select l.CodigoPaciente, l.ConsecutivoFarmacia, l.Ingreso).ToList()

        Using Form As New FrmPrintDashBoardPharmacy
            'Form.ViewModeEditHold = True
            Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Form.Size = New Size(345, 435)
            Form.listFilter = listId
            'Form.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Dim transparent As New FrmTransparent(Form, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using

    End Sub

    Private Async Sub FrmDashBoardPharmacy_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    Private Sub DashBoardPharmacyRequestSuccess(sender As Object, e As DashBoardPharmacyEventArgs)
        INDLcMainData.BeginUpdate()
        DeleteBlockedRecord()
        AsyncLoader(False)

        If e?.PharmaceuticalDispensing IsNot Nothing And Not String.IsNullOrEmpty(e?.PharmaceuticalDispensing?.Code) Then
            _frmReportViewer.DocViewer.DocumentSource = Nothing
            Me.evenPostProcess = Nothing
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDef, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensing, 1)
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.evenPostProcess = e.PharmaceuticalDispensing
        End If

        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            GetRequest()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 1 Then
            GetExtramural()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then
            GetChemotherapy()
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 5 Then
            GetRequestMixingStation()
        End If

        INDLcMainData.EndUpdate()
    End Sub

    Private Sub DashBoardPharmacyReturnSuccess(sender As Object, e As DashBoardPharmacyEventArgs)
        'AsyncLoader(True)
        INDLcMainData.BeginUpdate()
        DeleteBlockedRecord()
        If e.PharmaceuticalDispensingDevolution IsNot Nothing Then
            _frmReportViewer.DocViewer.DocumentSource = Nothing
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDefDevolution, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensingDevolution, 1)
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        GetReturn()
        INDLcMainData.EndUpdate()
        'AsyncLoader(False)
    End Sub


    Private Sub DashBoardPharmacySurgicalPackageSuccess(sender As Object, e As DashBoardPharmacyEventArgs)
        'AsyncLoader(True)
        INDLcMainData.BeginUpdate()
        DeleteBlockedRecord()
        If e.PharmaceuticalDispensing IsNot Nothing And Not String.IsNullOrEmpty(e.PharmaceuticalDispensing.Code) Then
            _frmReportViewer.DocViewer.DocumentSource = Nothing
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDef, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensing, 1)
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        getSurgicalPackage()
        INDLcMainData.EndUpdate()
        'AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Clic sobre el cerrar repote de la dispensación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnCloseReportViewer_Click(sender As Object, e As EventArgs) Handles INDBtnCloseReportViewer.Click
        If {0, 1}.Contains(INDTcgDashBoard.SelectedTabPageIndex) Then
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _frmReportViewerPharmacyNotes.DocViewer.DocumentSource = Nothing
            INDLycReportPhamacyNotes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Dim reportNeckBand As New Reporter.rptPharmaceuticalDispensingNeckBandPatient
            ReportHelper.ExecuteReport(_frmReportViewerPharmacyNotes, False, reportNeckBand, Me, Me.BarraBotones.PermissionsForm, Me.evenPostProcess)
        Else
            _frmReportViewerPharmacyNotes.DocViewer.DocumentSource = Nothing
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Clic sobre el cerrar repote de notas de farmacia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnCloseReportViewerPharmacyNote_Click(sender As Object, e As EventArgs) Handles INDBtnCloseReportViewerPharmacyNote.Click
        _frmReportViewerPharmacyNotes.DocViewer.DocumentSource = Nothing
        INDLycReportPhamacyNotes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre imprimir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then 'Solicutdes IntraHospitalarias
            Dim listSelected = DirectCast(INDGcRequest.FocusedView, GridView).GetSelectedRows()
            If listSelected.Count = 0 Then
                Return
            End If
            Dim listObjSelected As New List(Of ViewDashBoardPharmacy)()
            For Each row As Integer In listSelected
                Dim objSelected = DirectCast(DirectCast(INDGcRequest.FocusedView, GridView).GetRow(row), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                listObjSelected.Add(objSelected.OriginalRow)
            Next

            'Dim zzy = INDGcRequest.GetSelectedRowHandles()
            Dim listId As Object = (From l In listObjSelected Select l.CodigoPaciente, l.ConsecutivoFarmacia, l.Ingreso).ToList()

            Using Form As New FrmPrintDashBoardPharmacy
                'Form.ViewModeEditHold = True
                Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Form.Size = New Size(345, 435)
                Form.listFilter = listId
                'Form.WindowState = System.Windows.Forms.FormWindowState.Maximized
                Dim transparent As New FrmTransparent(Form, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 2 Then 'Devoluciones
            Dim listSelected = DirectCast(INDGcReturn.FocusedView, GridView).GetSelectedRows()
            If listSelected.Count = 0 Then
                Return
            End If
            Dim listObjSelected As New List(Of ViewDashBoardPharmacyDevolution)()
            For Each row As Integer In listSelected
                Dim objSelected = DirectCast(DirectCast(INDGcReturn.FocusedView, GridView).GetRow(row), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                listObjSelected.Add(objSelected.OriginalRow)
            Next

            'Dim zzy = INDGcRequest.GetSelectedRowHandles()
            Dim listId As Object = (From l In listObjSelected Select l.CodigoPaciente, l.Row, l.Ingreso).ToList()

            Using Form As New FrmPrintDashBoardPharmacy
                'Form.ViewModeEditHold = True
                Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Form.Size = New Size(345, 435)
                Form.listFilter = listId
                Form.isDevolution = True
                'Form.WindowState = System.Windows.Forms.FormWindowState.Maximized
                Dim transparent As New FrmTransparent(Form, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 3 Then 'paquetes QX
            Dim listSelected = DirectCast(INDgcPaquetes.FocusedView, GridView).GetSelectedRows()
            If listSelected.Count = 0 Then
                Return
            End If
            Dim listObjSelected As New List(Of ViewDashBoardPharmacy_SurgicalPackage)()
            For Each row As Integer In listSelected
                Dim objSelected = DirectCast(DirectCast(INDgcPaquetes.FocusedView, GridView).GetRow(row), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                listObjSelected.Add(objSelected.OriginalRow)
            Next

            'Dim zzy = INDGcRequest.GetSelectedRowHandles()
            Dim listId As Object = (From l In listObjSelected Select l.CodigoPaciente, l.ConsecutivoFarmacia, l.Ingreso).ToList()

            Using Form As New FrmPrintDashBoardPharmacy
                'Form.ViewModeEditHold = True
                Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Form.Size = New Size(345, 435)
                Form.listFilter = listId
                'Form.WindowState = System.Windows.Forms.FormWindowState.Maximized
                Dim transparent As New FrmTransparent(Form, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        ElseIf INDTcgDashBoard.SelectedTabPageIndex = 4 Then 'Quimioterapias
            Dim listSelected = DirectCast(INDgcChemotherapy.FocusedView, GridView).GetSelectedRows()
            If listSelected.Count = 0 Then
                Return
            End If
            Dim listObjSelected As New List(Of ViewDashBoardPharmacyChemotherapy)()
            For Each row As Integer In listSelected
                Dim objSelected = DirectCast(DirectCast(INDgcChemotherapy.FocusedView, GridView).GetRow(row), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                listObjSelected.Add(objSelected.OriginalRow)
            Next

            Dim listId As Object = (From l In listObjSelected Select l.CodigoPaciente, l.ConsecutivoFarmacia, l.Ingreso).ToList()

            Using Form As New FrmPrintDashBoardPharmacy
                'Form.ViewModeEditHold = True
                Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Form.Size = New Size(345, 435)
                Form.listFilter = listId
                'Form.WindowState = System.Windows.Forms.FormWindowState.Maximized
                Dim transparent As New FrmTransparent(Form, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' popup para determinar el motivo de la anulacion
    ''' </summary>
    Private Function ShowAnnulmentReasonsPopup() As ActionResult(Of Object)
        Using formulario As New FrmPopupObservations()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.41, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.5)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Text = ResourceManager.GetString("AnnularMessage")
            formulario.FilterType = 28
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                Dim AnnulmentReasons As Object = New ExpandoObject()
                AnnulmentReasons.IdHCMOANULB = formulario.IdHCMOANULB
                AnnulmentReasons.Description = formulario.Description

                'variable donde se almacena el mensaje que muestra la alerta
                Dim concatMessages As String

                'segun el tipo de filtro elejido asi se muestra el mensaje
                Select Case typeFilters
                    Case 1 'medicamentos
                        concatMessages = "Se va a realizar la anulación de los medicamentos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 2 'insumos
                        concatMessages = "Se va a realizar la anulación de los insumos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 3 'medicamentos tipos insumo
                        concatMessages = "Se va a realizar la anulación de los Medicamentos tipo insumo filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case Else 'en caso de elegir todos o no seleccionar ninguno no se muestra la alerta

                End Select
                Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = AnnulmentReasons}
            Else
                Return New ActionResult(Of Object) With {.StateResult = False}
            End If
        End Using
    End Function

    Private Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
        CleanFilter()
    End Sub

#Region "Toggle"
    ''' <summary>
    ''' evento del boton toggle cuando cambia de estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTbAutomaticReload_Toggled(sender As Object, e As EventArgs) Handles INDTbAutomaticReload.Toggled
        If Not Me.AutomaticReload Then
            _timer.Stop()
            _timer.Enabled = False
        Else
            _timer.AutoReset = True
            _timer.Enabled = True
        End If
    End Sub
#End Region

#Region "MasterRowGet"
    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewMixingStation_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDviewMixingStation.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim MixingStationDetails = INDviewMixingStation.GetFocusedObject(Of ViewDashBoardPharmacyMixingSationPatient)

            If datasourceMixingSationDetail IsNot Nothing Then
                INDviewMixingStationDetail.ShowLoadingPanel()
                e.ChildList = datasourceMixingSationDetail.ToList().FindAll(Function(d) d.CodigoPaciente = MixingStationDetails.CodigoPaciente).ToList()
                INDviewMixingStationDetail.HideLoadingPanel()
            End If
        End If
    End Sub

    Private Sub INDviewMixingStation_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDviewMixingStation.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    Private Sub INDviewMixingStation_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDviewMixingStation.MasterRowGetRelationName
        e.RelationName = "Solicitudes pendientes"
    End Sub
#End Region
End Class

''' <summary>
''' clase para serializar 
''' </summary>
Public Class AutomaticReload
    Property Name As String
    Property Value As Boolean
End Class