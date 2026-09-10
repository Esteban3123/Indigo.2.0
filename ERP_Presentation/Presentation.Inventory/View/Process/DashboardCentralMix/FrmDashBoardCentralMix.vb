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
Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmDashBoardCentralMix
    Implements IDashBoardPharmacy

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FormRequest = New FrmPopUpDashBoardCentralMixPreproduction
        AddHandler FormRequest.DashBoardPharmacyRequestSuccessEventArgs, AddressOf DashBoardPharmacyRequestSuccess
        FormReturn = New FrmPopUpDashBoardPharmacyReturn
        AddHandler FormReturn.DashBoardPharmacyReturnSuccessEventArgs, AddressOf DashBoardPharmacyReturnSuccess
        reportDef = New Reporter.rptPharmaceuticalDispensingNeckBand
        reportDefDevolution = New Reporter.rptPharmaceuticalDispensingDevolutionReduced
        _frmReportViewer = New FrmReportViewer
        _frmReportViewer.TopLevel = False
        _frmReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDPcReportViewer.Controls.Add(_frmReportViewer)
    End Sub

#Region "GLOBALS"
    Dim FormRequest As FrmPopUpDashBoardCentralMixPreproduction
    Dim FormReturn As FrmPopUpDashBoardPharmacyReturn

    Private _frmReportViewer As FrmReportViewer
    Dim reportDef As Reporter.rptPharmaceuticalDispensingNeckBand
    Dim reportDefDevolution As Reporter.rptPharmaceuticalDispensingDevolutionReduced

    ''' <summary>
    ''' presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacy
    ''' <summary>
    ''' listado de las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private listDashBoardRequest As XPCollection(Of ViewDashBoardPharmacy)
    ''' <summary>
    ''' listado de las devoluciones
    ''' </summary>
    ''' <remarks></remarks>
    Private listDashBoardDevolution As XPCollection(Of Domain.Crystal.Entities.ViewDashBoardPharmacyDevolution)


    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory
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
#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FormRequest = Nothing
        FormReturn = Nothing
        _frmReportViewer = Nothing
        reportDef = Nothing
        reportDefDevolution = Nothing
        _presenter = Nothing
        listDashBoardRequest = Nothing
        listDashBoardDevolution = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmDashBoardPharmacy control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PDashBoardPharmacy(Me)
        Me.ToolBar.Hide()
        Using model As New MDashBoardPharmacy(Me.Tag)
            FunctionalUnitDatasource = model.LoadCareCenter()
        End Using
        Dim ListActionsRequest As New List(Of eAcciones)
        Dim ListActionsReturn As New List(Of eAcciones)

        'obtengo los permisos de la barra para el formulario de solicitudes
        BarraBotones.ActualizarPermisosBarra("322")

        If BarraBotones.PermissionsForm.ContainsKey(8) Then
            ListActionsRequest.Add(eAcciones.Annular)
        End If
        ListActionsRequest.Add(eAcciones.Process)

        BarraBotones.ActualizarPermisosBarra("1516")
        If BarraBotones.PermissionsForm.ContainsKey(8) Then
            ListActionsReturn.Add(eAcciones.Annular)
        End If
        ListActionsReturn.Add(eAcciones.Process)
        IndigoGridView1.SetListAcction(INDGvRequest, ListActionsRequest)
        IndigoGridView2.SetListAcction(INDGvReturn, ListActionsReturn)
        INDGvRequest.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvRequest.Columns.ColumnByName("colActions").Width = 150

        INDGvReturn.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvReturn.Columns.ColumnByName("colActions").Width = 150


        CleanControls()
        GetDateServer()

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

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleFunctionalUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareCenter.EditValueChanged
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
        INDPceDetail.ClosePopup()
        If FunctionalUnitId IsNot Nothing Then
            Me.Cursor = ChangeCursorIndigo()
            'AsyncLoader(True)
            If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
                GetRequest()
            Else
                GetReturn()
            End If
            'AsyncLoader(False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        'Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        'Dim purcharse = DirectCast(INDGvRequest.GetFocusedRow(), ViewDashBoardPharmacy)
        'purcharse.SelectOption = checkControl.EditValue

        'Dim listActivated = listDashBoardRequest.ToList().FindAll(Function(x) x.SelectOption = True)
        'If listActivated.Count = listDashBoardRequest.Count Then
        '    Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        'Else
        '    Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        'End If

        'Me.INDGcRequest.RefreshDataSource()
        'Me.INDGcRequest.Invalidate()
    End Sub

    Private Sub INDRptCheActivateDevolution_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivateDevolution.EditValueChanged
        'Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        'Dim purcharse = DirectCast(INDGvReturn.GetFocusedRow(), ViewDashBoardPharmacyDevolution)
        'purcharse.SelectOption = checkControl.EditValue

        'Dim listActivated = listDashBoardDevolution.ToList().FindAll(Function(x) x.SelectOption = True)
        'If listActivated.Count = listDashBoardDevolution.Count Then
        '    Me.INDGclStateDevolution.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        'Else
        '    Me.INDGclStateDevolution.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        'End If

        'Me.INDGcReturn.RefreshDataSource()
        'Me.INDGcReturn.Invalidate()
    End Sub
#End Region

#Region "Actions"
    Dim dashboardPharmacy As ViewDashBoardPharmacy

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If INDGvRequest.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
            Exit Sub
        End If

        Dim dashboardPharmacyTmp = DirectCast(DirectCast(INDGvRequest.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewDashBoardPharmacy)
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.Document = Nothing

        If sender.Tag = "Annular" Then
            'se anula el registro sin necesidad de abrir el formulario
            Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                AsyncLoader(True)
                dashboardPharmacy = dashboardPharmacyTmp

                Using model As New MDashBoardPharmacy(Me.Tag)
                    listDashboardPharmacyDetail = model.ListDashboardPharmacyDetail(dashboardPharmacy.ConsecutivoFarmacia, dashboardPharmacy.CodigoPaciente.Trim(), dashboardPharmacy.Ingreso.Trim())
                    If listDashboardPharmacyDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para anular"
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    For Each item In listDashboardPharmacyDetail
                        item.PattientSupplyOrder = 1
                        item.AdmissionNumeber = dashboardPharmacy.Ingreso.Trim()
                        item.PattientCode = dashboardPharmacy.CodigoPaciente.Trim()
                        item.CareCenterCode = INDSleCareCenter.EditValue.ToString().Trim()
                        item.FunctionUnitCode = dashboardPharmacy.UFUCODIGO.Trim()
                        item.ConsecutivePescription = If(dashboardPharmacy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacy.ConsecutivoPrescripcion))
                        item.ConsecutiveInputs = dashboardPharmacy.ConsecutivoInsumos
                        item.ConsecutivePharmacy = dashboardPharmacy.ConsecutivoFarmacia
                        item.Action = 2
                        item.CantidadEntregada = 0
                    Next
                    Dim result = Await model.SaveDashBoardPharmacy(Nothing, listDashboardPharmacyDetail, 0)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                        dashboardPharmacy = Nothing
                        GetRequest()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using

                AsyncLoader(False)
            End If
        Else
            'muestra el popoup

            AsyncLoader(True)

            If dashboardPharmacy Is Nothing Then
                dashboardPharmacy = dashboardPharmacyTmp
            ElseIf dashboardPharmacyTmp.Equals(dashboardPharmacy) Then
                If PopupContainerControl1.Controls.Count > 0 Then
                    INDPceDetail.ShowPopup()
                    DirectCast(PopupContainerControl1.Controls(0), FrmPopUpDashBoardCentralMixPreproduction).SetFocusCUM = True
                End If
                AsyncLoader(False)
                Exit Sub
            Else
                dashboardPharmacy = dashboardPharmacyTmp
                DeleteBlockedRecord()
            End If
            Me.Cursor = ChangeCursorIndigo()
            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(dashboardPharmacy.ConsecutivoFarmacia))
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = CInt(dashboardPharmacy.ConsecutivoFarmacia)}
                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    If record.CodUser <> Me.indigo.UserIndigo Then
                        Mensaje(EeventViewerImages.Advertencia) = "El registro se encuentra bloqueado por el usuario " + result.CodUser + " - " + result.NameUser + ", desde " + result.BlockDate
                        dashboardPharmacy = Nothing
                        Me.Cursor = ChageCursorDefault()
                        AsyncLoader(False)
                        Exit Sub
                    End If
                End If
            End Using
            AsyncLoader(False)
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
                                    Me.Cursor = ChageCursorDefault()
                                    DeleteBlockedRecord()

                                    Exit Sub
                                End If
                            End Using

                            Dim resultUpdateFunctionalUnit = model.UpdateFunctionalUnitCrystal(dashboardPharmacy.ConsecutivoFarmacia, resultFunctionalUnit.ObjectEmbbeded)

                            If resultUpdateFunctionalUnit.StateResult = False Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error actulizando la unidad funcional en Crystal"
                                Me.Cursor = ChageCursorDefault()
                                DeleteBlockedRecord()

                                Exit Sub
                            End If
                        Else

                            DeleteBlockedRecord()
                            Me.Cursor = ChageCursorDefault()
                            dashboardPharmacy = Nothing
                            Exit Sub
                        End If
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = resultFunctionalUnit.Message
                    dashboardPharmacy = Nothing
                    DeleteBlockedRecord()
                    Me.Cursor = ChageCursorDefault()

                    Exit Sub
                End If
            End Using


            FormRequest.ViewModeEditHold = True
            FormRequest.PatientCode = dashboardPharmacy.CodigoPaciente.Trim()
            FormRequest.PatientName = dashboardPharmacy.NombrePaciente.Trim()
            FormRequest.Consecutive = dashboardPharmacy.ConsecutivoFarmacia
            FormRequest.AdmissionNumber = dashboardPharmacy.Ingreso.Trim()
            FormRequest.CareCenterCode = dashboardPharmacy.CodigoCentroAtencion
            'FormRequest.FunctionalUnit = dashboardPharmacy.UFUCODIGO
            FormRequest.Validado = dashboardPharmacy.Validado

            If functionalUnit IsNot Nothing Then
                FormRequest.FunctionalUnitCode = functionalUnit.Code + " - " + functionalUnit.Name
            Else
                FormRequest.FunctionalUnitCode = dashboardPharmacy.UFUCODIGO.Trim() + " - " + dashboardPharmacy.UnidadFuncional.Trim()
            End If

            FormRequest.Store = dashboardPharmacy.Bodega.Trim()
            FormRequest.CareCenter = INDSleCareCenter.EditValue.ToString().Trim()
            FormRequest.ConsecutivePescription = If(dashboardPharmacy.ConsecutivoPrescripcion = String.Empty, 0, Convert.ToInt32(dashboardPharmacy.ConsecutivoPrescripcion))
            FormRequest.ConsecutiveInputs = dashboardPharmacy.ConsecutivoInsumos
            FormRequest.ConsecutivePharmacy = dashboardPharmacy.ConsecutivoFarmacia
            FormRequest.HistoryType = dashboardPharmacy.IDETIPHIS
            FormRequest.TopLevel = False
            FormRequest.Dock = System.Windows.Forms.DockStyle.Fill
            FormRequest.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            FormRequest.Execute()
            PopupContainerControl1.Controls.Clear()
            PopupContainerControl1.Controls.Add(FormRequest)

            FormRequest.Show()
            Me.Cursor = ChageCursorDefault()
            PopupContainerControl1.Size = New Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 200)

            PopupContainerControl1.Location = New Point(0, 0)
            INDPceDetail.ShowPopup()


        End If
    End Sub

    Dim dashboardPharmacyDevolution As Domain.Crystal.Entities.ViewDashBoardPharmacyDevolution
    Private Async Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions

        If INDGvReturn.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
            Exit Sub
        End If
        Dim dashboardPharmacyDevolutionTmp = DirectCast(DirectCast(INDGvReturn.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Domain.Crystal.Entities.ViewDashBoardPharmacyDevolution)
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.Document = Nothing

        If sender.Tag = "Annular" Then
            Dim listDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution)
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                AsyncLoader(True)
                dashboardPharmacyDevolution = dashboardPharmacyDevolutionTmp
                Using model As New MDashBoardPharmacy(Me.Tag)
                    listDashboardPharmacyDetail = model.ListDashboardPharmacyDetailDevolution(dashboardPharmacyDevolution.ConsecutivoDevoluciones, dashboardPharmacyDevolution.CodigoPaciente.Trim(), dashboardPharmacyDevolution.Ingreso.Trim())
                    If listDashboardPharmacyDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para anular"
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    For Each item In listDashboardPharmacyDetail
                        item.Action = 2
                        item.QuantityReceived = 0
                    Next

                    Dim result = Await model.SaveDashBoardPharmacyDevolution(Nothing, listDashboardPharmacyDetail, 0)

                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        dashboardPharmacyDevolution = Nothing
                        GetReturn()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
                AsyncLoader(False)
            End If

        Else
            If dashboardPharmacyDevolution Is Nothing Then
                dashboardPharmacyDevolution = dashboardPharmacyDevolutionTmp
            ElseIf dashboardPharmacyDevolutionTmp.Equals(dashboardPharmacyDevolution) Then
                If PopupContainerControl1.Controls.Count > 0 Then
                    INDPceDetail.ShowPopup()
                    DirectCast(PopupContainerControl1.Controls(0), FrmPopUpDashBoardPharmacyReturn).SetFocusCUM = True
                End If
                Exit Sub
            Else
                dashboardPharmacyDevolution = dashboardPharmacyDevolutionTmp
                DeleteBlockedRecord()
            End If

            Me.Cursor = ChangeCursorIndigo()

            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(dashboardPharmacyDevolution.ConsecutivoDevoluciones))
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = CInt(dashboardPharmacyDevolution.ConsecutivoDevoluciones)}
                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    If record.CodUser <> Me.indigo.UserIndigo Then
                        Mensaje(EeventViewerImages.Advertencia) = "El registro se encuentra bloqueado por el usuario " + result.CodUser + " - " + result.NameUser + ", desde " + result.BlockDate
                        dashboardPharmacyDevolution = Nothing
                        Me.Cursor = ChageCursorDefault()
                        AsyncLoader(False)
                        Exit Sub
                    End If
                End If
            End Using

            FormReturn.ViewModeEditHold = True
            FormReturn.PatientCode = dashboardPharmacyDevolution.CodigoPaciente.Trim()
            FormReturn.PatientName = dashboardPharmacyDevolution.NombrePaciente.Trim()
            FormReturn.Consecutive = dashboardPharmacyDevolution.ConsecutivoDevoluciones
            FormReturn.Admission = dashboardPharmacyDevolution.Ingreso.Trim()
            FormReturn.FunctionalUnit = dashboardPharmacyDevolution.UFUCODIGO.Trim() + " - " + dashboardPharmacyDevolution.UnidadFuncional.Trim()
            FormReturn.Store = dashboardPharmacyDevolution.Bodega.Trim()
            FormReturn.CareCenter = INDSleCareCenter.EditValue.ToString().Trim()
            FormReturn.DevolutionOrigin = dashboardPharmacyDevolution.ORIDEVMED
            FormReturn.TopLevel = False
            FormReturn.Dock = System.Windows.Forms.DockStyle.Fill
            FormReturn.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            FormReturn.Execute()
            PopupContainerControl1.Controls.Clear()
            PopupContainerControl1.Controls.Add(FormReturn)

            FormReturn.Show()
            Me.Cursor = ChageCursorDefault()
            PopupContainerControl1.Size = New Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 200)

            PopupContainerControl1.Location = New Point(0, 0)
            INDPceDetail.ShowPopup()

        End If

    End Sub
#End Region

#Region "MouseDoubleClick"
    Private Sub INDGcRequest_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcRequest.MouseDoubleClick
        'Dim hitPoint = Me.INDGvRequest.CalcHitInfo(e.Location)
        'If hitPoint.Column IsNot Nothing Then
        '    If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
        '        If DirectCast(Me.INDGvRequest.DataSource, XPCollection(Of ViewDashBoardPharmacy)).Where(Function(s) s.SelectOption).ToList().Count = listDashBoardRequest.Count Then
        '            listDashBoardRequest.ToList().ForEach(Sub(x) x.SelectOption = False)
        '            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        '        Else
        '            listDashBoardRequest.ToList().ForEach(Sub(x) x.SelectOption = True)
        '            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        '        End If
        '        Me.INDGcRequest.RefreshDataSource()
        '        Me.INDGcRequest.Invalidate()
        '    End If
        'End If
    End Sub

    Private Sub INDGcReturn_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcReturn.MouseDoubleClick
        'Dim hitPoint = Me.INDGvReturn.CalcHitInfo(e.Location)
        'If hitPoint.Column IsNot Nothing Then
        '    If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclStateDevolution") Then
        '        If DirectCast(Me.INDGvReturn.DataSource, XPCollection(Of ViewDashBoardPharmacyDevolution)).Where(Function(s) s.SelectOption).ToList().Count = listDashBoardDevolution.Count Then
        '            listDashBoardDevolution.ToList().ForEach(Sub(x) x.SelectOption = False)
        '            Me.INDGclStateDevolution.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        '        Else
        '            listDashBoardDevolution.ToList().ForEach(Sub(x) x.SelectOption = True)
        '            Me.INDGclStateDevolution.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        '        End If
        '        Me.INDGcReturn.RefreshDataSource()
        '        Me.INDGcReturn.Invalidate()
        '    End If
        'End If
    End Sub
#End Region

#Region "SelectedPageChanged"
    Private Sub INDTcgDashBoard_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgDashBoard.SelectedPageChanged
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.Document = Nothing
        DeleteBlockedRecord()
        INDPceDetail.ClosePopup()
        PopupContainerControl1.Controls.Clear()
        dashboardPharmacy = Nothing
        dashboardPharmacyDevolution = Nothing
        Me.Cursor = ChangeCursorIndigo()
        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            INDBbiPrint.Enabled = True
            If INDSleCareCenter.EditValue IsNot Nothing AndAlso INDGcRequest.DataSource Is Nothing Then
                GetRequest()
            End If
        Else
            INDBbiPrint.Enabled = False
            If INDSleCareCenter.EditValue IsNot Nothing AndAlso INDGcReturn.DataSource Is Nothing Then
                GetReturn()
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#Region "ItemClick"
    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
        INDPceDetail.ClosePopup()
        Me.Cursor = ChangeCursorIndigo()
        DeleteBlockedRecord()

        If INDTcgDashBoard.SelectedTabPageIndex = 0 Then
            If INDSleCareCenter.EditValue IsNot Nothing Then
                GetRequest()
            End If
        Else
            If INDSleCareCenter.EditValue IsNot Nothing Then
                GetReturn()
            End If
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
#End Region
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDTcgDashBoard.SelectedTabPageIndex = 0
    End Sub

    ''' <summary>
    ''' metodo para obtener las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRequest()
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details = model.ListDashBoardCentralMixXPInstant(INDSleCareCenter.EditValue)
            INDGcRequest.DataSource = details
        End Using
    End Sub
    ''' <summary>
    ''' metodo para obtener las devoluciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetReturn()
        Using model As New MDashBoardPharmacy(Me.Tag)

            Dim details = model.ListDashBoardPharmacyDevolutionXPInstant(INDSleCareCenter.EditValue)
            INDGcReturn.DataSource = details
        End Using
    End Sub

#End Region

    Private Sub INDBbiPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrint.ItemClick
        INDPceDetail.ClosePopup()
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

    Private Sub FrmDashBoardPharmacy_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
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
        'AsyncLoader(True)
        INDLcMainData.BeginUpdate()
        INDPceDetail.ClosePopup()
        DeleteBlockedRecord()
        dashboardPharmacy = Nothing
        If e.PharmaceuticalDispensing IsNot Nothing Then
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDef, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensing, 1)
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        GetRequest()
        INDLcMainData.EndUpdate()
        'AsyncLoader(False)
    End Sub

    Private Sub DashBoardPharmacyReturnSuccess(sender As Object, e As DashBoardPharmacyEventArgs)
        'AsyncLoader(True)
        INDLcMainData.BeginUpdate()
        INDPceDetail.ClosePopup()
        DeleteBlockedRecord()
        dashboardPharmacyDevolution = Nothing
        If e.PharmaceuticalDispensingDevolution IsNot Nothing Then
            ReportHelper.ExecuteReport(_frmReportViewer, False, reportDefDevolution, Me, Me.BarraBotones.PermissionsForm, e.PharmaceuticalDispensingDevolution, 1)
            INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        GetReturn()
        INDLcMainData.EndUpdate()
        'AsyncLoader(False)
    End Sub


    Private Sub INDBtnCloseReportViewer_Click(sender As Object, e As EventArgs) Handles INDBtnCloseReportViewer.Click
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre imprimir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        INDPceDetail.ClosePopup()
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
    End Sub
End Class