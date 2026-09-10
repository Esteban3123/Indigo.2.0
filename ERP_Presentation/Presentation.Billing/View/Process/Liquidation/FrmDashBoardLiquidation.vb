'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 2018-04-11
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
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Billing.MVP
Imports System.Drawing.Imaging
Imports DevExpress.XtraBars
Imports System.Text
Imports System.Linq
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraSplashScreen
Imports System.ComponentModel
Imports Presentation.Reporter
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.BillingRepository
#End Region

Public Class FrmDashBoardLiquidation
    Implements ILiquidation

    Public Sub New()
        InitializeComponent()
        _frmReportViewer = New FrmReportViewer
        _frmReportViewer.TopLevel = False
        _frmReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        _frmReportViewer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        INDPcReportViewer.Controls.Add(_frmReportViewer)
    End Sub

#Region "GLOBALS"
    Private _frmReportViewer As FrmReportViewer

    ''' <summary>
    ''' Listado de items seleccionados para cuando se filtre se reestablezca los checks de los items
    ''' </summary>
    Private selectedRows As New List(Of Integer)()

    ''' <summary>
    ''' Parametros de Facturación
    ''' </summary>
    Private _parameterBilling As SettingsBilling
#End Region

#Region "PROPERTIES"

    Public ReadOnly Property RequiresConditionsSale As Boolean
        Get
            Dim _requiresConditionsSale As Boolean

            If _parameterBilling IsNot Nothing Then
                _requiresConditionsSale = _parameterBilling.requiresConditionsSale
            End If

            Return _requiresConditionsSale
        End Get
    End Property

    ''' <summary>
    ''' Datasource de unidades funcionales
    ''' </summary>
    Public Property FunctionalUnitDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDSleCareCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad funcional
    ''' </summary>
    Public Property CareCenterCode As String
        Get
            Return INDSleCareCenter.EditValue
        End Get
        Set(value As String)
            INDSleCareCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de ventas
    ''' </summary>
    Public Property ConditionSalesId As Integer?
        Get
            Return INDSleConditionSales.EditValue
        End Get
        Set(value As Integer?)
            INDSleConditionSales.EditValue = value
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
    ''' Obtiene el Id de la autorización seleccionada
    ''' </summary>
    ''' <returns>El Id de la autorización seleccionada</returns>
    Public Property IdBillingAuthorizationSelected As Integer
        Get
            Return Me.SleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            Me.SleBillingAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de autorizaciones de facturación
    ''' asignadas al usuario
    ''' </summary>
    ''' <value>Lista de autorizaciones de facturación</value>
    ''' <returns>Lamlista de autorizaciones de facturación</returns>
    Public Property ListBillingAuthorization As List(Of BillingAuthorization)
        Get
            Return Me.SleBillingAuthorization.Properties.DataSource
        End Get
        Set(value As List(Of BillingAuthorization))
            Me.SleBillingAuthorization.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                Me.SleBillingAuthorization.EditValue = value(0).Id
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de unidades operativas
    ''' </summary>
    ''' <value>Lista de unidades operativa</value>
    ''' <returns>La lista de unidades operativas</returns>
    Public Property ListOperatingUnit As List(Of OperatingUnit)
        Get
            Return Me.SleOperatingUnit.Properties.DataSource
        End Get
        Set(value As List(Of OperatingUnit))
            If value IsNot Nothing Then
                Dim listFilter = (From e In value Where e IsNot Nothing Select e).ToList()
                Me.SleOperatingUnit.Properties.DataSource = listFilter
                If listFilter.Count > 0 Then
                    Me.SleOperatingUnit.EditValue = SessionValues.Instance.IndigoOperatingUnitId
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Id de la unidad operativa seleccionad</returns>
    Public Property IdOperatingUnitSelected As Integer
        Get
            Return Me.SleOperatingUnit.EditValue
        End Get
        Set(value As Integer)
            Me.SleOperatingUnit.EditValue = value
        End Set
    End Property

    Public ReadOnly Property ListFoliosIdOrder As List(Of Object) Implements ILiquidation.ListFoliosIdOrder
        Get

        End Get
    End Property

    Private Property ILiquidation_ListOperatingUnit As List(Of OperatingUnit) Implements ILiquidation.ListOperatingUnit
        Get

        End Get
        Set(value As List(Of OperatingUnit))

        End Set
    End Property

    Private ReadOnly Property ILiquidation_OperatingUnitSelected As OperatingUnit Implements ILiquidation.OperatingUnitSelected
        Get

        End Get
    End Property

    Private Property ILiquidation_IdOperatingUnitSelected As Integer Implements ILiquidation.IdOperatingUnitSelected
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Private Property ILiquidation_ListBillingAuthorization As List(Of BillingAuthorization) Implements ILiquidation.ListBillingAuthorization
        Get

        End Get
        Set(value As List(Of BillingAuthorization))

        End Set
    End Property

    Private ReadOnly Property ILiquidation_BillingAuthorizationSelected As BillingAuthorization Implements ILiquidation.BillingAuthorizationSelected
        Get

        End Get
    End Property

    Private Property ILiquidation_IdBillingAuthorizationSelected As Integer Implements ILiquidation.IdBillingAuthorizationSelected
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Public ReadOnly Property MyTag As String Implements ILiquidation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property IsBusy As Boolean Implements ILiquidation.IsBusy
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Property PermissionsForm1 As Dictionary(Of Integer, String) Implements ILiquidation.PermissionsForm
        Get
            Return Me.BarraBotones.PermissionsForm
        End Get
        Set(value As Dictionary(Of Integer, String))

        End Set
    End Property

    Public ReadOnly Property TxtPatientCode As Object Implements ILiquidation.TxtPatientCode
        Get

        End Get
    End Property

    Private ReadOnly Property ILiquidation_Name As String Implements ILiquidation.Name
        Get
            Return Me.Name
        End Get
    End Property

    Public ReadOnly Property TxtPatientName As Object Implements ILiquidation.TxtPatientName
        Get

        End Get
    End Property

    ''' <summary>
    ''' propiedad para saber si el sistema es IVA incluido o No.
    ''' </summary>
    ''' <returns></returns>
    Public Property FlagTaxInclude As Boolean Implements ILiquidation.FlagTaxInclude

#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _frmReportViewer = Nothing
        selectedRows = Nothing
    End Sub


    ''' <summary>
    ''' Handles the Load event of the FrmDashBoardPharmacy control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        IndigoGridView1.MoreInfoColunmns(INDGvAdmissions)

        Await Me.LoadParameters()
        Await Me.LoadDatasource()
        LoadActionsGrid()

        CleanControls()
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de obtener la parametrizacion del modulo de facturacion
    ''' </summary>
    Private Async Function LoadParameters() As Task
        Try
            Using model As New MBillingSetting(MyTag)
                Me._parameterBilling = Await model.GetSettingsBillingByIdUnitOperative(SessionValues.Instance.IndigoOperatingUnitId, False)
                If Me._parameterBilling Is Nothing OrElse Me._parameterBilling.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Facturación para la unidad operativa seleccionada"
                    Exit Function
                End If

            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de setear el Datasource de los diferentes campos del formulario
    ''' </summary>
    Private Async Function LoadDatasource() As Task
        Try
            Using model As New MDashBoardPharmacy(Me.Tag)
                FunctionalUnitDatasource = model.LoadCareCenter()
            End Using

            Using model As New MLiquidation()
                ListBillingAuthorization = Await model.ListBillingAuthorizationByUserCode(SessionValues.Instance.UserIndigo)
                rptDiagnosticos.DataSource = model.ListAllINDIAGNOS()
            End Using

            Using model = New Security.MVP.MUsuario
                Dim _Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = SessionValues.Instance.IndigoContainerId).FirstOrDefault
                ListOperatingUnit = Await model.GetOperatingUnitByContainerPermission(SessionValues.Instance.IndigoContainerId, SessionValues.Instance.UserType, SessionValues.Instance.UserIndigo, _Company.Administrator)
            End Using


            If RequiresConditionsSale Then

                INDlciConditionSales.HideControl(Not RequiresConditionsSale)
                INDlciConditionSales.ShowInCustomizationForm = Not RequiresConditionsSale

                Using model As New MLiquidation()
                    INDSleConditionSales.Properties.DataSource = Await model.ListConditionSales

                    Dim ConditionSales = TryCast(INDSleConditionSales.Properties.DataSource, List(Of ConditionSalesXpo))
                    If ConditionSales.Count = 1 Then
                        ConditionSalesId = ConditionSales.FirstOrDefault.Id
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Metodo que se encarga de definir las acciones con las que va a contar la rejilla
    ''' </summary>
    Private Sub LoadActionsGrid()
        IndigoGridView1.SetListAcction(INDGvAdmissions, {eAcciones.View, eAcciones.Liquidate}.ToList)
        INDGvAdmissions.Columns.ColumnByName("colActions").Caption = "Acción"
        INDGvAdmissions.Columns.ColumnByName("colActions").Width = 150
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

    ''' <summary>
    ''' Handles the Shown event of the FrmDashBoardPharmacy control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDashBoardPharmacy_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BbiFacturacionMasiva.Visibility = IIf(BarraBotones.PermissionsForm.ContainsKey(2), BarItemVisibility.Always, BarItemVisibility.Never)
        INDBbiPrint.Visibility = IIf(BarraBotones.PermissionsForm.ContainsKey(23), BarItemVisibility.Always, BarItemVisibility.Never)
        INDBbiRefresh.Visibility = IIf(BarraBotones.PermissionsForm.ContainsKey(40), BarItemVisibility.Always, BarItemVisibility.Never)
        If Me.BarraBotones.PermissionsForm.ContainsKey(80) Then
            colEgresDate.OptionsColumn.AllowEdit = True
            colEgresDate.OptionsColumn.AllowFocus = True

            colDiagnostico.OptionsColumn.AllowEdit = True
            colDiagnostico.OptionsColumn.AllowFocus = True
        Else
            colEgresDate.OptionsColumn.AllowEdit = False
            colEgresDate.OptionsColumn.AllowFocus = False

            colDiagnostico.OptionsColumn.AllowEdit = False
            colDiagnostico.OptionsColumn.AllowFocus = False
        End If
        INDSleCareCenter.Properties.PopupFormMinSize = New Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Properties.PopupFormSize = New Size(INDSleCareCenter.Size.Width - 11, 0)
        INDSleCareCenter.Focus()
    End Sub


#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the rptDiagnosticos control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub RptDiagnosticos_QueryPopUp(sender As Object, e As CancelEventArgs) Handles rptDiagnosticos.QueryPopUp
        If rptDiagnosticos.DataSource Is Nothing Then
            Using model As New MLiquidation()
                rptDiagnosticos.DataSource = model.ListAllINDIAGNOS()
            End Using
        End If
    End Sub

#End Region

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

        If frmDeparture IsNot Nothing Then
            frmDeparture.CareCenterCode = CareCenterCode
        End If

        If Not String.IsNullOrEmpty(CareCenterCode) Then
            Me.Cursor = ChangeCursorIndigo()
            GetRequest()
            Me.Cursor = Windows.Forms.Cursors.Default
        End If
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If INDGvAdmissions.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
            Exit Sub
        End If

        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.Document = Nothing

        Select Case (sender.Tag)
            Case "View", ResourceManager.GetString("View")
                OpenLiquidateForm()

            Case "Liquidate", ResourceManager.GetString("Liquidate")
                LiquidateFolioMultiple({INDGvAdmissions.FocusedRowHandle})
        End Select
    End Sub
#End Region

#Region "MouseDoubleClick"

    Private waitForm As New SplashScreenManager(Me, GetType(wfMain), False, True)
    Private Sub INDGcRequest_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcAdmissions.MouseDoubleClick
        Dim hitPoint = Me.INDGvAdmissions.CalcHitInfo(e.Location)
        If hitPoint.InRow Then
            OpenLiquidateForm()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de desplegar el formulario de liquidacion
    ''' </summary>
    Private Sub OpenLiquidateForm()
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim rowData As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetFocusedRow()
        Using FrmLiquidation As New FrmLiquidation()
            FrmLiquidation.Size = New Size(Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            FrmLiquidation.MinimizeBox = False
            FrmLiquidation.MaximizeBox = False
            Using m As New MControlOutpatientServices(Me.Tag)
                Dim admissionCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                    .CrystalService.Liquidation_GetAdmission(rowData.AdmissionCodeWithOutTrim)
                FrmLiquidation.AuxAdmissionToReload = admissionCollection
            End Using
            FrmLiquidation.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            AddHandler FrmLiquidation.Shown, Sub()
                                                 waitForm.CloseWaitForm()
                                             End Sub
            Dim transparent As New FrmTransparent(FrmLiquidation, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "ItemClick"
    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
        selectedRows = New List(Of Integer)
        INDPceDetail.ClosePopup()
        Me.Cursor = ChangeCursorIndigo()

        If Not String.IsNullOrEmpty(CareCenterCode) Then
            GetRequest()
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvAdmissions_MouseDown(sender As Object, e As Windows.Forms.MouseEventArgs) Handles INDGvAdmissions.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing AndAlso hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
            If Not hi.InRow Then
                Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount
                If Not allSelected Then
                    For i As Integer = 0 To view.RowCount - 1
                        Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                        If Not selectedRows.Contains(sourceHandle) Then
                            selectedRows.Add(sourceHandle)
                        End If
                    Next i
                Else
                    selectedRows.Clear()
                End If
            Else
                Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)
                If Not selectedRows.Contains(sourceHandle) Then
                    selectedRows.Add(sourceHandle)
                Else
                    selectedRows.Remove(sourceHandle)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ColumnFilterChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvAdmissions_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDGvAdmissions.ColumnFilterChanged
        RestoreSelection(TryCast(sender, GridView))
    End Sub

#End Region

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Metodo que reestablece el check de los items de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub RestoreSelection(ByVal view As GridView)
        BeginInvoke(New Action(Sub()
                                   Dim i As Integer = 0
                                   Do While i < selectedRows.Count
                                       view.SelectRow(view.GetRowHandle(selectedRows(i)))
                                       i += 1
                                   Loop
                               End Sub))
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.Document = Nothing
        INDPceDetail.ClosePopup()
        Me.Cursor = ChangeCursorIndigo()
        INDBbiPrint.Enabled = False

        If Not String.IsNullOrEmpty(CareCenterCode) Then
            GetRequest()
        End If
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    ''' <summary>
    ''' Metodo para obtener las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRequest()
        If Not BarraBotones.PermissionsForm.ContainsKey(40) Then
            Mensaje(EeventViewerImages.Advertencia) = "No Posee permisos para consultar"
            Exit Sub
        End If

        Using model As New MLiquidation
            INDGcAdmissions.DataSource = model.ListAdmissionsToLiquidationDashboard(CareCenterCode)
        End Using
    End Sub
#End Region

    Private Sub INDBbiPrint_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiPrint.ItemClick
        INDPceDetail.ClosePopup()
    End Sub

    Private Sub FrmDashBoardPharmacy_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub INDBtnCloseReportViewer_Click(sender As Object, e As EventArgs) Handles INDBtnCloseReportViewer.Click
        INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _frmReportViewer.DocViewer.DocumentSource = Nothing
    End Sub

    Private bgw_printReport As BackgroundWorker
    Private reportDefSaleInvoice As rptSubSaleInvoiceAll
    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre imprimir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BbiImprimir.ItemClick
        INDPceDetail.ClosePopup()
        If INDGcAdmissions.DataSource Is Nothing OrElse Not CType(INDGcAdmissions.DataSource, XPCollection(Of ViewAdmissionsToLiquidationDashboard)).Any(Function(o) o.StateOperation IsNot Nothing AndAlso o.StateOperation = 2) Then
            Return
        End If
        Dim listAdmissionCodes As New List(Of String)()
        Dim listInvoices As New List(Of String)()
        For Each item In CType(INDGcAdmissions.DataSource, XPCollection(Of ViewAdmissionsToLiquidationDashboard)).Where(Function(o) o.StateOperation IsNot Nothing AndAlso o.StateOperation = 2)
            listAdmissionCodes.Add(item.AdmissionCodeWithOutTrim)
            If item.InvoiceList IsNot Nothing Then
                listInvoices.AddRange(CType(item.InvoiceList, List(Of InvoiceResult)) _
                .Select(Function(o) o.InvoiceNumber).ToList())
            End If
        Next
        If bgw_printReport Is Nothing Then
            bgw_printReport = New BackgroundWorker()
            AddHandler bgw_printReport.DoWork, AddressOf bgw_printReport_DoWork
            AddHandler bgw_printReport.RunWorkerCompleted, AddressOf bgw_printReport_RunWorkerCompleted
        End If
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim admissionsStr As String = ""
        If listAdmissionCodes.Count = 1 Then
            admissionsStr = String.Format("'{0}'", listAdmissionCodes(0))
        Else
            admissionsStr = $"'{String.Join("','", listAdmissionCodes)}'"
        End If
        Dim invoicesStr As String = String.Empty
        If listInvoices.Any() Then
            invoicesStr = $"'{String.Join("','", listInvoices)}'"
        End If

        reportDefSaleInvoice = New rptSubSaleInvoiceAll()
        reportDefSaleInvoice.ParametrosReporte = New Object() {Nothing,                 'INDDateStart.EditValue,
                                                               Nothing,                 'INDDateEnd.EditValue,
                                                               0,                       'INDGleTypeInvoice.EditValue,
                                                               1,                       'INDGleStatus.EditValue,
                                                               invoicesStr,             'INDSleInitialInvoice.EditValue,
                                                               Nothing,                 'INDSleFinalInvoice.EditValue,
                                                               Nothing,                 'INDSleHealthAdministrator.EditValue,
                                                               Nothing,                 'INDSlePatient.EditValue,
                                                               Nothing,                 'INDSleGroup.EditValue,
                                                               Nothing,                 'INDSleCategories.EditValue,
                                                               Nothing,                 'INDSleThirdParty.EditValue,
                                                               Nothing,                 'INDSleBranchOffice.EditValue,
                                                               admissionsStr,           'INDsleAdmissionNumber.EditValue,
                                                               Nothing,                 'INDSleRadicated.EditValue,
                                                               Nothing,                 'INDSleUser.EditValue,
                                                               Nothing}                 'INDGleOrder.EditValue

        AddHandler reportDefSaleInvoice.AfterPrint, Sub()
                                                        waitForm.CloseWaitForm()
                                                    End Sub
        If Not bgw_printReport.IsBusy Then
            bgw_printReport.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgw_printReport_DoWork(sender As Object, e As DoWorkEventArgs)
        reportDefSaleInvoice.CargarDataSource()
        Dim resutl = reportDefSaleInvoice.DataSource
        e.Result = resutl
    End Sub

    Private Sub bgw_printReport_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        reportDefSaleInvoice.DataSource = e.Result
        ReportHelper.ExecuteReport(reportDefSaleInvoice, Me, Me.BarraBotones.PermissionsForm)
    End Sub

    Private ht As New Hashtable()
    Private Sub INDGvAdmissions_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvAdmissions.CustomUnboundColumnData
        If e.IsGetData Then
            Dim row_Renamed As ViewAdmissionsToLiquidationDashboard = CType(e.Row, ViewAdmissionsToLiquidationDashboard)
            Dim key As Byte? = row_Renamed.StateOperation
            If key IsNot Nothing Then
                If Not ht.ContainsKey(key) Then
                    ht.Add(key, GetImage(key))
                End If
                e.Value = ht(key)
            End If
        End If
    End Sub

    Private Function GetImage(ByVal key As Integer) As Byte()
        Dim img As Image = Nothing
        Select Case key
            Case 1
                img = My.Resources.cargando
            Case 2
                img = My.Resources.ok
            Case 3
                img = My.Resources.warning
            Case 4
                img = My.Resources.calificar16x16
        End Select
        Return ByteImageConverter.ToByteArray(img, ImageFormat.Gif)
    End Function

    Private Sub SleOperatingUnit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles SleOperatingUnit.EditValueChanging
        If e.NewValue IsNot Nothing Then
            INDSleCategory.Properties.View.ShowLoadingPanel()
            Task.Run(Sub()
                         Using model As New MLiquidation()
                             Dim setting = model.GetSettingsBillingByOperatingUnitId(CInt(e.NewValue))
                             If setting Is Nothing Then
                                 Me.SafeInvoke(Sub()
                                                   Me.ShowMessage(EeventViewerImages.Advertencia) = "Actualmente no existe parametros de facturacion"
                                                   INDSleCategory.Properties.View.HideLoadingPanel()
                                               End Sub)
                                 Return
                             End If
                             INDSleCategory.SafeInvoke(Sub()
                                                           INDSleCategory.Properties.DataSource = model.ListInvoiceCategoryByPermissionCategories(1, Nothing)
                                                           INDSleCategory.Properties.View.HideLoadingPanel()
                                                       End Sub)
                         End Using
                     End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Facturacion Masiva
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub BarButtonItem2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BbiFacturacionMasiva.ItemClick
        LiquidateFolioMultiple(INDGvAdmissions.GetSelectedRows())
    End Sub

    Private Sub INDGvAdmissions_RowClick(sender As Object, e As RowClickEventArgs) Handles INDGvAdmissions.RowClick
        Dim hitInfo As GridHitInfo = INDGvAdmissions.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
        If hitInfo.InRow AndAlso hitInfo.InRowCell AndAlso hitInfo.Column.Name.Equals("ColImgAction") Then
            Dim frmNotificationItem As New FrmNotificationItemDetail()
            frmNotificationItem.TxtMessage.Text = CType(INDGvAdmissions.GetFocusedRow(), ViewAdmissionsToLiquidationDashboard).MessageInfo
            If String.IsNullOrEmpty(frmNotificationItem.TxtMessage.Text) Then
                Exit Sub
            End If
            Using transparent As New FrmTransparent(frmNotificationItem, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    Private Async Sub LiquidateFolioMultiple(admissionsToLiquidate As Integer())
        Try
            If Not BarraBotones.PermissionsForm.ContainsKey(2) Then
                Mensaje(EeventViewerImages.Advertencia) = "No posee permisos para realizar esta acción"
                Exit Sub
            End If

            If INDGcAdmissions.DataSource Is Nothing Then
                Exit Sub
            End If

            If admissionsToLiquidate Is Nothing OrElse Not admissionsToLiquidate.Any() Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar por lo menos un ítem para realizar esta acción"
                Exit Sub
            End If

            If MessageIndigo.Show("¿Esta seguro que desea facturar los ingresos seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.AsyncLoaderOnlyBar(True)
                Dim errorList As New StringBuilder()

                If IdBillingAuthorizationSelected = 0 Then
                    errorList.AppendLine("Seleccione una Autorización")
                End If

                If IdOperatingUnitSelected = 0 Then
                    errorList.AppendLine("Seleccione una Unidad Operativa")
                End If

                If INDSleCategory.EditValue Is Nothing Then
                    errorList.AppendLine("Seleccione una Categoría de Facturas")
                End If

                If INDlciConditionSales.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If ConditionSalesId Is Nothing Then
                        errorList.AppendLine("Seleccione una condición de venta")
                    End If
                End If

                If errorList.Length > 0 Then
                    Me.ShowMessage(eStatusResult.WARNING) = errorList.ToString()
                    Me.AsyncLoaderOnlyBar(False)
                    Exit Sub
                End If

                Dim msgError As New StringBuilder()
                Dim msgErrorRow As New StringBuilder()
                Dim msgConsecutivos As New List(Of String)()
                Dim msgRecognition As New List(Of Integer)()
                Dim countProcess As Integer = 0
                Dim totalQuantity As Integer = admissionsToLiquidate.Count()

                For Each index As Integer In admissionsToLiquidate
                    Dim admissionObject As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetRow(index)
                    INDGcAdmissions.RefreshDataSource()
                    admissionObject.StateOperation = 1
                    admissionObject.MessageInfo = String.Empty
                    Using model As New MLiquidation()

                        'validamos que el ingreso tenga folios sin liquidar
                        Dim query As New StringBuilder()
                        query.AppendLine($" select * from billing.revenuecontroldetail with(nolock) where revenuecontrolid = {admissionObject.RevenueControlId} And status = 1 ")

                        Dim dtResult As DataTable = Await model.ExecuteCommandDt(query.ToString(), SessionValues.Instance.TransactionalContainer)
                        If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then

                            'Validaciones
                            If admissionObject.PatientBalance > 0 AndAlso _parameterBilling.RequiresPermissionForCxCPatient AndAlso Not Me.BarraBotones.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.PermiteCuentaXCobrarPaciente)) Then
                                msgErrorRow.AppendLine($"El Ingreso ({admissionObject.AdmissionCodeWithOutTrim}) genera pagaré pero el usuario actual no tiene permiso para ejecutar esta acción.")
                            End If
                            If admissionObject.FECALTPAC Is Nothing Then
                                msgErrorRow.AppendLine($"El Ingreso ({admissionObject.AdmissionCodeWithOutTrim}) no tiene fecha de egreso.")
                            End If
                            If admissionObject.Diagnostico Is Nothing Then
                                msgErrorRow.AppendLine($"El Ingreso ({admissionObject.AdmissionCodeWithOutTrim}) no tiene diagnostico.")
                            End If

                            Dim resultCategory As Boolean = True

                            query.Clear()
                            query.AppendLine(" select distinct rc.AdmissionNumber from Billing.RevenueControl rc with(nolock)			")
                            query.AppendLine(" inner join Billing.RevenueControlDetail rcd with(nolock) on rcd.RevenueControlId = rc.Id	")
                            query.AppendLine($" where rcd.InvoiceCategoryId is null And rc.AdmissionNumber = '{admissionObject.AdmissionCodeWithOutTrim}'")

                            dtResult = Await model.ExecuteCommandDt(query.ToString(), SessionValues.Instance.TransactionalContainer)
                            If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then
                                query.Clear()
                                query.AppendLine($" UPDATE Billing.RevenueControlDetail SET InvoiceCategoryId = {CInt(INDSleCategory.EditValue)} ")
                                query.AppendLine($" WHERE RevenueControlId = {admissionObject.RevenueControlId} And InvoiceCategoryId Is Null ")

                                resultCategory = Await model.ExecuteQuery(query.ToString(), SessionValues.Instance.TransactionalContainer)
                                If Not resultCategory Then
                                    msgErrorRow.AppendLine($"El Ingreso ({admissionObject.AdmissionCodeWithOutTrim}) no pudo ser actualizado con la categoría ({INDSleCategory.Text}).")
                                End If
                            End If

                            If msgErrorRow.Length = 0 Then
                                If resultCategory Then
                                    admissionObject.RevenueControlDetailCrossingList = Nothing
                                    Dim rcdCrossing As New RevenueControlDetailCrossing()
                                    With rcdCrossing
                                        .FolioOrder = admissionObject.FolioOrder
                                        .FolioType = admissionObject.FolioType
                                        .CareGroupId = admissionObject.CareGroupFolioId
                                        .RevenueControlDetailId = admissionObject.RevenueControlDetailId
                                        .ListPortfolioAdvance = New List(Of PortfolioAdvance)()
                                        .TotalPatientDiscount = admissionObject.PatientDiscount
                                        .RevenueControlId = admissionObject.RevenueControlId
                                        .CutType = 1
                                        .InitialDate = admissionObject.AdmissionDate
                                        .OutputDate = admissionObject.FECALTPAC
                                        .IsCutAccount = False
                                        .OutputDiagnosis = admissionObject.Diagnostico
                                        .CurrencyId = indigo.OfficialCurrencyId
                                        .TRMValue = 1
                                        .ConditionSalesId = ConditionSalesId
                                    End With
                                    If admissionObject.PortfolioAdvanceId IsNot Nothing Then
                                        Dim portfolioData As String() = admissionObject.PortfolioAdvanceConcat.Split(";")
                                        Dim portfolioAdvance As New PortfolioAdvance()
                                        With portfolioAdvance
                                            .Id = admissionObject.PortfolioAdvanceId
                                            .Code = admissionObject.PortfolioAdvanceCode
                                            .AdmissionNumber = admissionObject.AdmissionCode
                                            .ThirdPartyId = CInt(portfolioData(3).Trim())
                                            .MainAccountId = CInt(portfolioData(4).Trim())
                                            .CostCenterId = If(CInt(portfolioData(5).Trim()) = 0, CType(Nothing, Integer?), CInt(portfolioData(5).Trim()))
                                            .Balance = CDec(portfolioData(2).Replace(".", ",").Trim())
                                            .CrossingValue = admissionObject.TotalPatientWithDiscount - admissionObject.PatientBalance
                                            .DocumentDate = CDate(portfolioData(6).Trim())
                                        End With
                                        rcdCrossing.ListPortfolioAdvance.Add(portfolioAdvance)
                                    End If
                                    admissionObject.RevenueControlDetailCrossingList = {rcdCrossing}.ToList()
                                End If
                            End If
                        Else
                            'No hay folios para liquidar
                            msgErrorRow.AppendLine($"El Ingreso ({admissionObject.AdmissionCodeWithOutTrim}) no contiene folios disponibles para liquidar.")
                        End If

                        If msgErrorRow.Length > 0 Then
                            admissionObject.MessageInfo = msgErrorRow.ToString()
                            admissionObject.StateOperation = 3
                            msgError.AppendLine(msgErrorRow.ToString())
                        Else

                            Dim thirdPartyToValidate As Integer? = admissionObject.ThirdPartyIdForAgeValidation
                            Dim canLiquidate As Boolean = True

                            ' Validar mayoría de edad
                            If thirdPartyToValidate.HasValue Then
                                Dim ageValidationResult = Await model.ValidateAgeOfMajorityForLiquidationAsync(thirdPartyToValidate.Value, IdOperatingUnitSelected, admissionObject.AdmissionCodeWithOutTrim)

                                If ageValidationResult IsNot Nothing AndAlso ageValidationResult.StateResult AndAlso
                                   ageValidationResult.ObjectEmbbeded IsNot Nothing AndAlso
                                   ageValidationResult.ObjectEmbbeded.RequiresResponsible Then
                                    ' El tercero es menor de edad y requiere responsable
                                    Dim thirdPartyDescription = GetThirdPartyValidationDescription(admissionObject)
                                    admissionObject.MessageInfo = $"{ageValidationResult.ObjectEmbbeded.ValidationMessage} ({thirdPartyDescription}). Debe asignar un responsable mayor de edad desde el detalle del folio."
                                    admissionObject.StateOperation = 3
                                    msgError.AppendLine($"Ingreso ({admissionObject.AdmissionCodeWithOutTrim}): {ageValidationResult.ObjectEmbbeded.ValidationMessage} - {thirdPartyDescription}")
                                    canLiquidate = False
                                End If
                            End If

                            If canLiquidate Then
                                Dim thirdPartyForLiquidation As Integer = If(thirdPartyToValidate.HasValue, thirdPartyToValidate.Value, admissionObject.ThirdPartyPatientId)
                                Dim result As ActionResult(Of List(Of InvoiceResult)) = Await model.LiquidateFolioAsync(admissionObject.RevenueControlDetailCrossingList, admissionObject.PatientCode, admissionObject.AdmissionCodeWithOutTrim, IdBillingAuthorizationSelected, IdOperatingUnitSelected, thirdPartyForLiquidation, False)
                                If result.StateResult Then
                                    Dim resultMessage As String = result.MessageResult.ElementAt(0)
                                    admissionObject.MessageInfo = resultMessage
                                    admissionObject.StateOperation = 2
                                    msgConsecutivos.Add(vbCrLf)
                                    msgConsecutivos.Add($"Ingreso {admissionObject.AdmissionCodeWithOutTrim} :")
                                    msgConsecutivos.Add(admissionObject.MessageInfo)
                                    admissionObject.InvoiceList = result.ObjectEmbbeded
                                Else
                                    admissionObject.MessageInfo = result.Message
                                    msgError.AppendLine(admissionObject.MessageInfo)
                                    admissionObject.StateOperation = 3
                                End If
                            End If
                        End If

                        INDGcAdmissions.RefreshDataSource()
                        countProcess = countProcess + 1
                        If countProcess = totalQuantity Then
                            Me.AsyncLoaderOnlyBar(False)
                            If msgConsecutivos.Count > 0 Then
                                Dim consecutivos As String = String.Join(vbCrLf, msgConsecutivos)
                                Me.Mensaje(Base.EeventViewerImages.Informacion) =
                                        String.Format("Se han liquidado los siguientes Ingresos : {0}{1}", vbCrLf, consecutivos)
                            End If
                            If msgError.Length > 0 Then
                                Me.Mensaje(Base.EeventViewerImages.Advertencia) =
                                            String.Format("Han ocurrido los siguientes errores : {0}{1}", vbCrLf, msgError.ToString())
                            End If
                        End If
                    End Using
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub INDGvAdmissions_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDGvAdmissions.CustomColumnDisplayText
        Dim view As ColumnView = CType(sender, ColumnView)
        If e.ListSourceRowIndex <> DevExpress.XtraGrid.GridControl.InvalidRowHandle AndAlso e.Value IsNot Nothing Then

            If e.Column.Name.Equals(colDiagnostico.Name) Then
                e.DisplayText = CStr(view.GetListSourceRowCellValue(e.ListSourceRowIndex, "DiagnosticoCodeName"))
            ElseIf e.Column.Name.Equals(colRecibo.Name) Then
                If CStr(view.GetListSourceRowCellValue(e.ListSourceRowIndex, "PortfolioAdvanceConcat")) IsNot Nothing Then
                    e.DisplayText = CStr(view.GetListSourceRowCellValue(e.ListSourceRowIndex, "PortfolioAdvanceConcat")).Split(";")(1).Trim()
                Else
                    e.DisplayText = ""
                End If
            End If

        End If
    End Sub

    Private Sub swed(sender As Object, e As CancelEventArgs) Handles INDGvAdmissions.ShowingEditor
        If DirectCast(sender, ColumnView).FocusedColumn.Name.Equals(colRecibo.Name) Then
            Dim admissionObject = CType(INDGvAdmissions.GetFocusedRow(), ViewAdmissionsToLiquidationDashboard)
            If admissionObject.TotalPatientWithDiscount = 0 Then
                e.Cancel = True
            End If
            rptPortfolioAdvance.ReadOnly = admissionObject.TotalPatientWithDiscount = 0
        End If
    End Sub

    Private Sub rptDiagnosticos_EditValueChanged(sender As Object, e As EventArgs) Handles rptDiagnosticos.EditValueChanged
        Dim control = CType(sender, SearchLookUpEdit)
        Dim obj As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetFocusedRow()
        If control.EditValue IsNot Nothing Then
            obj.DiagnosticoCodeName = control.Text
        Else
            obj.DiagnosticoCodeName = ""
        End If
    End Sub

    Private Sub ModifiedData(sender As Object, e As EventArgs) Handles rptDiagnosticos.EditValueChanged, rptEgressDate.EditValueChanged, rptPortfolioAdvance.EditValueChanged
        Dim control As BaseEdit = CType(sender, BaseEdit)
        Dim obj As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetFocusedRow()
        If control.EditValue IsNot Nothing Then
            obj.StateOperation = 4
        Else
            obj.StateOperation = Nothing
        End If
    End Sub

    Private Sub INDGvAdmissions_ColumnPositionChanged(sender As Object, e As EventArgs) Handles INDGvAdmissions.ColumnPositionChanged
        If Not CType(sender, DevExpress.XtraGrid.Columns.GridColumn).Visible Then
            IndigoGridView1.MoreInfoColunmns(INDGvAdmissions)
        End If
    End Sub

    Private Sub rptPortfolioAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles rptPortfolioAdvance.QueryPopUp
        Using model As New MLiquidation()
            Dim obj As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetFocusedRow()

            If obj IsNot Nothing AndAlso obj.ThirdPartyPatientId IsNot Nothing AndAlso rptPortfolioAdvance.DataSource Is Nothing Then
                rptPortfolioAdvance.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
                    .PortfolioService.GetPortfolioAdvanceByThirdPartyIdAndAdmission(obj.ThirdPartyPatientId, obj.AdmissionCode)
            End If
        End Using
    End Sub

    Private Sub rptPortfolioAdvance_Closed(sender As Object, e As ClosedEventArgs) Handles rptPortfolioAdvance.Closed
        rptPortfolioAdvance.DataSource = Nothing
    End Sub

    Private objPortfolioAdvanceXpo As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance
    Private Sub rptPortfolioAdvance_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles rptPortfolioAdvance.EditValueChanging
        If e.NewValue IsNot Nothing Then
            objPortfolioAdvanceXpo = DirectCast(DirectCast(CType(sender, SearchLookUpEdit).Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance)
        Else
            objPortfolioAdvanceXpo = Nothing
        End If
    End Sub

    Private Sub rptCashReceivableNumber_EditValueChanged(sender As Object, e As EventArgs) Handles rptPortfolioAdvance.EditValueChanged
        Dim control = CType(sender, SearchLookUpEdit)
        Dim obj As ViewAdmissionsToLiquidationDashboard = INDGvAdmissions.GetFocusedRow()
        If control.EditValue IsNot Nothing AndAlso objPortfolioAdvanceXpo IsNot Nothing Then
            obj.PortfolioAdvanceConcat = $"{objPortfolioAdvanceXpo.Id};{objPortfolioAdvanceXpo.Code};{objPortfolioAdvanceXpo.Balance};{objPortfolioAdvanceXpo.ThirdPartyId.Id};{objPortfolioAdvanceXpo.MainAccountId.Id};{If(objPortfolioAdvanceXpo.CostCenterId Is Nothing, 0, objPortfolioAdvanceXpo.CostCenterId.Id)};{objPortfolioAdvanceXpo.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss")}"
            obj.PortfolioAdvanceCode = objPortfolioAdvanceXpo.Code
            obj.PortfolioAdvanceId = objPortfolioAdvanceXpo.Id
        Else
            obj.PortfolioAdvanceConcat = Nothing
            obj.PortfolioAdvanceId = Nothing
            obj.PortfolioAdvanceCode = Nothing
        End If
    End Sub

    Private frmDeparture As Common.FrmPatientDeparture
    Private Sub TabbedControlGroup1_SelectedPageChanging(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangingEventArgs) Handles TabbedControlGroup1.SelectedPageChanging

        If String.IsNullOrEmpty(CareCenterCode) Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione un centro de atención"
            e.Cancel = True

        ElseIf e.Page.Name.Equals("INDtpIntraHospitalario") AndAlso frmDeparture Is Nothing Then
            frmDeparture = New Common.FrmPatientDeparture()
            frmDeparture.ToolBar.Hide()
            frmDeparture.InContainer = True
            frmDeparture.TopLevel = False
            frmDeparture.CareCenterCode = CareCenterCode
            frmDeparture.Parent = INDPcContainerForm
            frmDeparture.ViewModeEditHold = False
            frmDeparture.Dock = System.Windows.Forms.DockStyle.Fill
            frmDeparture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler frmDeparture.Shown, Sub()
                                           End Sub
            frmDeparture.Show()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene una descripción del tercero que se está validando para mayoría de edad
    ''' </summary>
    ''' <param name="admissionObject">Objeto de admisión</param>
    ''' <returns>Descripción del tipo de tercero validado</returns>
    Private Function GetThirdPartyValidationDescription(admissionObject As ViewAdmissionsToLiquidationDashboard) As String
        If admissionObject.PatientQuotaResponsibleThirdPartyId.HasValue AndAlso admissionObject.PatientQuotaResponsibleThirdPartyId.Value > 0 Then
            Return "Responsable Cuota Paciente"
        ElseIf admissionObject.FolioType = 3 AndAlso admissionObject.ThirdPartyId.HasValue AndAlso admissionObject.ThirdPartyId.Value > 0 Then
            Return "Tercero del Folio Particular"
        Else
            Return "Paciente"
        End If
    End Function

End Class

Public Enum eModeLiquidation
    MULTIPLE_PATIENT
    SIMPLE
End Enum