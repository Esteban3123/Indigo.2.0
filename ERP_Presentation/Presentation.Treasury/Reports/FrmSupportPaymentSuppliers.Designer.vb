Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSupportPaymentSuppliers
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSupportPaymentSuppliers))
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcExportExcell = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCxP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolProveedor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolNoFac = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolValFac = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolSubFac = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolReteFuente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolICA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolReteIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolValPagar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolND = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolNC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolDesFin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolFCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.indcolvouchercode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolVPgado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolRefPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCBP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlevSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSupplierUnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleInvoice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlevInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColInvoiceUnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFactura = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIndProveedor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleVoucher = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlevVoucher = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColVoucherUnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConsecutivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFechaConsecutivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProveedorConsecutivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbGenerareReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbGenerateExcell = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDSupport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDVoucher = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSbGenerareReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDCncNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDPcViewReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDDvViewReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.PopupControlContainer2 = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDPopupccEdadPaciente = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDpccFilterRecords = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDReportsPcc = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDAuditPcc = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDDocumentalSystemPcc = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDpopupcontainerRestablecer = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDpopUpBiometrico = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDpccExportar = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.DocumentViewerBarManager1 = New DevExpress.XtraPrinting.Preview.DocumentViewerBarManager(Me.components)
        Me.PreviewBar1 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewBarItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.ZoomBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomBarEditItem()
        Me.PrintPreviewRepositoryItemComboBox1 = New DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox()
        Me.PrintPreviewBarItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem18 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem19 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem20 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem21 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem22 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem23 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem24 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem25 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem26 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PreviewBar2 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewStaticItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.ProgressBarEditItem1 = New DevExpress.XtraPrinting.Preview.ProgressBarEditItem()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.PrintPreviewBarItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.PrintPreviewStaticItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.ZoomTrackBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem()
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.PrintPreviewSubItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarItem27 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.PrintPreviewSubItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarCheckItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlevSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlevInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleVoucher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlevVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSbGenerareReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcViewReport.SuspendLayout()
        Me.INDDvViewReport.SuspendLayout()
        CType(Me.PopupControlContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupccEdadPaciente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccFilterRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDReportsPcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDAuditPcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDocumentalSystemPcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopupcontainerRestablecer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopUpBiometrico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccExportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Dock = System.Windows.Forms.DockStyle.None
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolBars.Location = New System.Drawing.Point(200, 5)
        Me.ToolBars.Size = New System.Drawing.Size(808, 130)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(808, 130)
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDGcExportExcell)
        Me.INDLcBase.Controls.Add(Me.INDDateEnd)
        Me.INDLcBase.Controls.Add(Me.INDDateStart)
        Me.INDLcBase.Controls.Add(Me.INDSleSupplier)
        Me.INDLcBase.Controls.Add(Me.INDSleInvoice)
        Me.INDLcBase.Controls.Add(Me.INDSleVoucher)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerareReport)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateExcell)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(200, 5)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1056, 468, 650, 400)
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(808, 724)
        Me.INDLcBase.TabIndex = 30
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDGcExportExcell
        '
        Me.INDGcExportExcell.Location = New System.Drawing.Point(-16, 165)
        Me.INDGcExportExcell.MainView = Me.GridView2
        Me.INDGcExportExcell.Name = "INDGcExportExcell"
        Me.INDGcExportExcell.Size = New System.Drawing.Size(386, 518)
        Me.INDGcExportExcell.TabIndex = 46
        Me.INDGcExportExcell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView2.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolInvoiceDate, Me.INDcolCxP, Me.INDcolProveedor, Me.INDcolNoFac, Me.INDcolValFac, Me.INDcolSubFac, Me.INDcolIVA, Me.INDcolReteFuente, Me.INDcolICA, Me.INDcolReteIVA, Me.INDcolValPagar, Me.INDcolND, Me.INDcolNC, Me.INDcolDesFin, Me.INDcolFCE, Me.indcolvouchercode, Me.INDcolVPgado, Me.INDcolRefPago, Me.INDcolCBP})
        Me.GridView2.GridControl = Me.INDGcExportExcell
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        '
        'INDcolInvoiceDate
        '
        Me.INDcolInvoiceDate.Caption = "fecha factura"
        Me.INDcolInvoiceDate.FieldName = "BillDate"
        Me.INDcolInvoiceDate.Name = "INDcolInvoiceDate"
        Me.INDcolInvoiceDate.OptionsColumn.AllowEdit = False
        Me.INDcolInvoiceDate.OptionsColumn.AllowFocus = False
        Me.INDcolInvoiceDate.Visible = True
        Me.INDcolInvoiceDate.VisibleIndex = 0
        '
        'INDcolCxP
        '
        Me.INDcolCxP.Caption = "Cuentas por pagar"
        Me.INDcolCxP.FieldName = "Code"
        Me.INDcolCxP.Name = "INDcolCxP"
        Me.INDcolCxP.OptionsColumn.AllowEdit = False
        Me.INDcolCxP.OptionsColumn.AllowFocus = False
        Me.INDcolCxP.Visible = True
        Me.INDcolCxP.VisibleIndex = 1
        '
        'INDcolProveedor
        '
        Me.INDcolProveedor.Caption = "Proveedores "
        Me.INDcolProveedor.FieldName = "SupplierName"
        Me.INDcolProveedor.Name = "INDcolProveedor"
        Me.INDcolProveedor.OptionsColumn.AllowEdit = False
        Me.INDcolProveedor.OptionsColumn.AllowFocus = False
        Me.INDcolProveedor.Visible = True
        Me.INDcolProveedor.VisibleIndex = 2
        '
        'INDcolNoFac
        '
        Me.INDcolNoFac.Caption = "No Factura"
        Me.INDcolNoFac.FieldName = "BillNumber"
        Me.INDcolNoFac.Name = "INDcolNoFac"
        Me.INDcolNoFac.OptionsColumn.AllowEdit = False
        Me.INDcolNoFac.OptionsColumn.AllowFocus = False
        Me.INDcolNoFac.Visible = True
        Me.INDcolNoFac.VisibleIndex = 3
        '
        'INDcolValFac
        '
        Me.INDcolValFac.Caption = "Valor Factura"
        Me.INDcolValFac.FieldName = "InvoiceValue"
        Me.INDcolValFac.Name = "INDcolValFac"
        Me.INDcolValFac.OptionsColumn.AllowEdit = False
        Me.INDcolValFac.OptionsColumn.AllowFocus = False
        Me.INDcolValFac.Visible = True
        Me.INDcolValFac.VisibleIndex = 4
        '
        'INDcolSubFac
        '
        Me.INDcolSubFac.Caption = "Subtotal Factura"
        Me.INDcolSubFac.FieldName = "BaseValue"
        Me.INDcolSubFac.Name = "INDcolSubFac"
        Me.INDcolSubFac.OptionsColumn.AllowEdit = False
        Me.INDcolSubFac.OptionsColumn.AllowFocus = False
        Me.INDcolSubFac.Visible = True
        Me.INDcolSubFac.VisibleIndex = 5
        '
        'INDcolIVA
        '
        Me.INDcolIVA.Caption = "Valor IVA"
        Me.INDcolIVA.FieldName = "IvaValue"
        Me.INDcolIVA.Name = "INDcolIVA"
        Me.INDcolIVA.OptionsColumn.AllowEdit = False
        Me.INDcolIVA.OptionsColumn.AllowFocus = False
        Me.INDcolIVA.Visible = True
        Me.INDcolIVA.VisibleIndex = 6
        '
        'INDcolReteFuente
        '
        Me.INDcolReteFuente.Caption = "Retención en la fuente"
        Me.INDcolReteFuente.FieldName = "ReteFuente"
        Me.INDcolReteFuente.Name = "INDcolReteFuente"
        Me.INDcolReteFuente.OptionsColumn.AllowEdit = False
        Me.INDcolReteFuente.OptionsColumn.AllowFocus = False
        Me.INDcolReteFuente.Visible = True
        Me.INDcolReteFuente.VisibleIndex = 7
        '
        'INDcolICA
        '
        Me.INDcolICA.Caption = "Retención ICA"
        Me.INDcolICA.FieldName = "ReteICA"
        Me.INDcolICA.Name = "INDcolICA"
        Me.INDcolICA.OptionsColumn.AllowEdit = False
        Me.INDcolICA.OptionsColumn.AllowFocus = False
        Me.INDcolICA.Visible = True
        Me.INDcolICA.VisibleIndex = 8
        '
        'INDcolReteIVA
        '
        Me.INDcolReteIVA.Caption = "Retención IVA"
        Me.INDcolReteIVA.FieldName = "ReteIVA"
        Me.INDcolReteIVA.Name = "INDcolReteIVA"
        Me.INDcolReteIVA.OptionsColumn.AllowEdit = False
        Me.INDcolReteIVA.OptionsColumn.AllowFocus = False
        Me.INDcolReteIVA.Visible = True
        Me.INDcolReteIVA.VisibleIndex = 9
        '
        'INDcolValPagar
        '
        Me.INDcolValPagar.Caption = "Valor a pagar"
        Me.INDcolValPagar.FieldName = "VoucherTotalValue"
        Me.INDcolValPagar.Name = "INDcolValPagar"
        Me.INDcolValPagar.OptionsColumn.AllowEdit = False
        Me.INDcolValPagar.OptionsColumn.AllowFocus = False
        Me.INDcolValPagar.Visible = True
        Me.INDcolValPagar.VisibleIndex = 10
        '
        'INDcolND
        '
        Me.INDcolND.Caption = "Nota debito"
        Me.INDcolND.FieldName = "DebitValueNote"
        Me.INDcolND.Name = "INDcolND"
        Me.INDcolND.OptionsColumn.AllowEdit = False
        Me.INDcolND.OptionsColumn.AllowFocus = False
        Me.INDcolND.Visible = True
        Me.INDcolND.VisibleIndex = 11
        '
        'INDcolNC
        '
        Me.INDcolNC.Caption = "Nota credito"
        Me.INDcolNC.FieldName = "CreditValueNote"
        Me.INDcolNC.Name = "INDcolNC"
        Me.INDcolNC.OptionsColumn.AllowEdit = False
        Me.INDcolNC.OptionsColumn.AllowFocus = False
        Me.INDcolNC.Visible = True
        Me.INDcolNC.VisibleIndex = 12
        '
        'INDcolDesFin
        '
        Me.INDcolDesFin.Caption = "Valor descuento financiero"
        Me.INDcolDesFin.FieldName = "PromptPaymentDiscount"
        Me.INDcolDesFin.Name = "INDcolDesFin"
        Me.INDcolDesFin.OptionsColumn.AllowEdit = False
        Me.INDcolDesFin.OptionsColumn.AllowFocus = False
        Me.INDcolDesFin.Visible = True
        Me.INDcolDesFin.VisibleIndex = 13
        '
        'INDcolFCE
        '
        Me.INDcolFCE.Caption = "Fecha comprobante de egreso"
        Me.INDcolFCE.FieldName = "VoucherDate"
        Me.INDcolFCE.Name = "INDcolFCE"
        Me.INDcolFCE.OptionsColumn.AllowEdit = False
        Me.INDcolFCE.OptionsColumn.AllowFocus = False
        Me.INDcolFCE.Visible = True
        Me.INDcolFCE.VisibleIndex = 14
        '
        'indcolvouchercode
        '
        Me.indcolvouchercode.Caption = "Número comprobante de egreso"
        Me.indcolvouchercode.FieldName = "VoucherCode"
        Me.indcolvouchercode.Name = "indcolvouchercode"
        Me.indcolvouchercode.OptionsColumn.AllowEdit = False
        Me.indcolvouchercode.OptionsColumn.AllowFocus = False
        Me.indcolvouchercode.Visible = True
        Me.indcolvouchercode.VisibleIndex = 18
        '
        'INDcolVPgado
        '
        Me.INDcolVPgado.Caption = "Valor pagado"
        Me.INDcolVPgado.FieldName = "VoucherTotalValue"
        Me.INDcolVPgado.Name = "INDcolVPgado"
        Me.INDcolVPgado.OptionsColumn.AllowEdit = False
        Me.INDcolVPgado.OptionsColumn.AllowFocus = False
        Me.INDcolVPgado.Visible = True
        Me.INDcolVPgado.VisibleIndex = 15
        '
        'INDcolRefPago
        '
        Me.INDcolRefPago.Caption = "Referencia de pago"
        Me.INDcolRefPago.FieldName = "VoucherReferencePayment"
        Me.INDcolRefPago.Name = "INDcolRefPago"
        Me.INDcolRefPago.OptionsColumn.AllowEdit = False
        Me.INDcolRefPago.OptionsColumn.AllowFocus = False
        Me.INDcolRefPago.Visible = True
        Me.INDcolRefPago.VisibleIndex = 16
        '
        'INDcolCBP
        '
        Me.INDcolCBP.Caption = "Cuenta bancaria proveedor"
        Me.INDcolCBP.FieldName = "SupplierBankAccount"
        Me.INDcolCBP.Name = "INDcolCBP"
        Me.INDcolCBP.OptionsColumn.AllowEdit = False
        Me.INDcolCBP.OptionsColumn.AllowFocus = False
        Me.INDcolCBP.Visible = True
        Me.INDcolCBP.VisibleIndex = 17
        '
        'INDDateEnd
        '
        Me.INDDateEnd.EditValue = Nothing
        Me.INDDateEnd.EnterMoveNextControl = True
        Me.INDDateEnd.Location = New System.Drawing.Point(-16, 129)
        Me.INDDateEnd.Name = "INDDateEnd"
        Me.INDDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.Mask.EditMask = "G"
        Me.INDDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd.Size = New System.Drawing.Size(386, 28)
        Me.INDDateEnd.StyleController = Me.INDLcBase
        Me.INDDateEnd.TabIndex = 2
        '
        'INDDateStart
        '
        Me.INDDateStart.EditValue = Nothing
        Me.INDDateStart.EnterMoveNextControl = True
        Me.INDDateStart.Location = New System.Drawing.Point(-16, 73)
        Me.INDDateStart.Name = "INDDateStart"
        Me.INDDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.Mask.EditMask = "G"
        Me.INDDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart.Size = New System.Drawing.Size(386, 28)
        Me.INDDateStart.StyleController = Me.INDLcBase
        Me.INDDateStart.TabIndex = 1
        '
        'INDSleSupplier
        '
        Me.INDSleSupplier.Location = New System.Drawing.Point(398, 73)
        Me.INDSleSupplier.Name = "INDSleSupplier"
        Me.INDSleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleSupplier.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplier.Properties.DisplayFormat.FormatString = "d"
        Me.INDSleSupplier.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleSupplier.Properties.EditFormat.FormatString = "d"
        Me.INDSleSupplier.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleSupplier.Properties.NullText = ""
        Me.INDSleSupplier.Properties.PopupView = Me.INDSlevSupplier
        Me.INDSleSupplier.Properties.ShowClearButton = False
        Me.INDSleSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDSleSupplier.StyleController = Me.INDLcBase
        Me.INDSleSupplier.TabIndex = 1
        '
        'INDSlevSupplier
        '
        Me.INDSlevSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSupplierUnboundSelection, Me.INDColNit, Me.INDColName})
        Me.INDSlevSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSlevSupplier.Name = "INDSlevSupplier"
        Me.INDSlevSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSlevSupplier.OptionsView.ShowAutoFilterRow = True
        Me.INDSlevSupplier.OptionsView.ShowGroupPanel = False
        '
        'INDColSupplierUnboundSelection
        '
        Me.INDColSupplierUnboundSelection.Caption = "        "
        Me.INDColSupplierUnboundSelection.FieldName = "INDColSupplierUnboundSelection"
        Me.INDColSupplierUnboundSelection.Name = "INDColSupplierUnboundSelection"
        Me.INDColSupplierUnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDColSupplierUnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDColSupplierUnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSupplierUnboundSelection.Visible = True
        Me.INDColSupplierUnboundSelection.VisibleIndex = 0
        Me.INDColSupplierUnboundSelection.Width = 40
        '
        'INDColNit
        '
        Me.INDColNit.Caption = "Nit"
        Me.INDColNit.FieldName = "Code"
        Me.INDColNit.Name = "INDColNit"
        Me.INDColNit.OptionsColumn.AllowEdit = False
        Me.INDColNit.OptionsColumn.AllowFocus = False
        Me.INDColNit.Visible = True
        Me.INDColNit.VisibleIndex = 1
        Me.INDColNit.Width = 165
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "IdThirdParty.Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 2
        Me.INDColName.Width = 172
        '
        'INDSleInvoice
        '
        Me.INDSleInvoice.Location = New System.Drawing.Point(398, 129)
        Me.INDSleInvoice.Name = "INDSleInvoice"
        Me.INDSleInvoice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleInvoice.Properties.Appearance.Options.UseFont = True
        Me.INDSleInvoice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleInvoice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleInvoice.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleInvoice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleInvoice.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleInvoice.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleInvoice.Properties.DisplayFormat.FormatString = "d"
        Me.INDSleInvoice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleInvoice.Properties.EditFormat.FormatString = "d"
        Me.INDSleInvoice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleInvoice.Properties.NullText = ""
        Me.INDSleInvoice.Properties.PopupView = Me.INDSlevInvoice
        Me.INDSleInvoice.Properties.ShowClearButton = False
        Me.INDSleInvoice.Size = New System.Drawing.Size(386, 28)
        Me.INDSleInvoice.StyleController = Me.INDLcBase
        Me.INDSleInvoice.TabIndex = 2
        '
        'INDSlevInvoice
        '
        Me.INDSlevInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColInvoiceUnboundSelection, Me.INDColFactura, Me.INDColNameSupplier, Me.INDColIndProveedor})
        Me.INDSlevInvoice.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSlevInvoice.Name = "INDSlevInvoice"
        Me.INDSlevInvoice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSlevInvoice.OptionsView.ShowAutoFilterRow = True
        Me.INDSlevInvoice.OptionsView.ShowGroupPanel = False
        '
        'INDColInvoiceUnboundSelection
        '
        Me.INDColInvoiceUnboundSelection.Caption = "     "
        Me.INDColInvoiceUnboundSelection.FieldName = "INDColInvoiceUnboundSelection"
        Me.INDColInvoiceUnboundSelection.Name = "INDColInvoiceUnboundSelection"
        Me.INDColInvoiceUnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDColInvoiceUnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDColInvoiceUnboundSelection.OptionsFilter.AllowAutoFilter = False
        Me.INDColInvoiceUnboundSelection.OptionsFilter.AllowFilter = False
        Me.INDColInvoiceUnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColInvoiceUnboundSelection.Visible = True
        Me.INDColInvoiceUnboundSelection.VisibleIndex = 0
        Me.INDColInvoiceUnboundSelection.Width = 40
        '
        'INDColFactura
        '
        Me.INDColFactura.Caption = "Factura"
        Me.INDColFactura.FieldName = "BillNumber"
        Me.INDColFactura.Name = "INDColFactura"
        Me.INDColFactura.OptionsColumn.AllowEdit = False
        Me.INDColFactura.OptionsColumn.AllowFocus = False
        Me.INDColFactura.Visible = True
        Me.INDColFactura.VisibleIndex = 1
        Me.INDColFactura.Width = 111
        '
        'INDColNameSupplier
        '
        Me.INDColNameSupplier.Caption = "Nombre proveedor"
        Me.INDColNameSupplier.FieldName = "IdSupplier.Name"
        Me.INDColNameSupplier.Name = "INDColNameSupplier"
        Me.INDColNameSupplier.OptionsColumn.AllowEdit = False
        Me.INDColNameSupplier.OptionsColumn.AllowFocus = False
        Me.INDColNameSupplier.Visible = True
        Me.INDColNameSupplier.VisibleIndex = 2
        Me.INDColNameSupplier.Width = 111
        '
        'INDColIndProveedor
        '
        Me.INDColIndProveedor.Caption = "Identificacion proveedor"
        Me.INDColIndProveedor.FieldName = "IdSupplier.IdThirdParty.Nit"
        Me.INDColIndProveedor.Name = "INDColIndProveedor"
        Me.INDColIndProveedor.OptionsColumn.AllowEdit = False
        Me.INDColIndProveedor.OptionsColumn.AllowFocus = False
        Me.INDColIndProveedor.Visible = True
        Me.INDColIndProveedor.VisibleIndex = 3
        Me.INDColIndProveedor.Width = 115
        '
        'INDSleVoucher
        '
        Me.INDSleVoucher.Location = New System.Drawing.Point(398, 185)
        Me.INDSleVoucher.Name = "INDSleVoucher"
        Me.INDSleVoucher.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucher.Properties.Appearance.Options.UseFont = True
        Me.INDSleVoucher.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleVoucher.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleVoucher.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucher.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleVoucher.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleVoucher.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleVoucher.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleVoucher.Properties.DisplayFormat.FormatString = "d"
        Me.INDSleVoucher.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleVoucher.Properties.EditFormat.FormatString = "d"
        Me.INDSleVoucher.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleVoucher.Properties.NullText = ""
        Me.INDSleVoucher.Properties.PopupView = Me.INDSlevVoucher
        Me.INDSleVoucher.Properties.ShowClearButton = False
        Me.INDSleVoucher.Size = New System.Drawing.Size(386, 28)
        Me.INDSleVoucher.StyleController = Me.INDLcBase
        Me.INDSleVoucher.TabIndex = 1
        '
        'INDSlevVoucher
        '
        Me.INDSlevVoucher.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColVoucherUnboundSelection, Me.INDColConsecutivo, Me.INDColFechaConsecutivo, Me.INDColProveedorConsecutivo})
        Me.INDSlevVoucher.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSlevVoucher.Name = "INDSlevVoucher"
        Me.INDSlevVoucher.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSlevVoucher.OptionsView.ShowAutoFilterRow = True
        Me.INDSlevVoucher.OptionsView.ShowGroupPanel = False
        '
        'INDColVoucherUnboundSelection
        '
        Me.INDColVoucherUnboundSelection.Caption = "       "
        Me.INDColVoucherUnboundSelection.FieldName = "INDColVoucherUnboundSelection"
        Me.INDColVoucherUnboundSelection.Name = "INDColVoucherUnboundSelection"
        Me.INDColVoucherUnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDColVoucherUnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDColVoucherUnboundSelection.OptionsFilter.AllowAutoFilter = False
        Me.INDColVoucherUnboundSelection.OptionsFilter.AllowFilter = False
        Me.INDColVoucherUnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColVoucherUnboundSelection.Visible = True
        Me.INDColVoucherUnboundSelection.VisibleIndex = 0
        Me.INDColVoucherUnboundSelection.Width = 40
        '
        'INDColConsecutivo
        '
        Me.INDColConsecutivo.Caption = "Consecutivo"
        Me.INDColConsecutivo.FieldName = "Code"
        Me.INDColConsecutivo.Name = "INDColConsecutivo"
        Me.INDColConsecutivo.Visible = True
        Me.INDColConsecutivo.VisibleIndex = 1
        Me.INDColConsecutivo.Width = 111
        '
        'INDColFechaConsecutivo
        '
        Me.INDColFechaConsecutivo.Caption = "Fecha"
        Me.INDColFechaConsecutivo.FieldName = "DocumentDate"
        Me.INDColFechaConsecutivo.Name = "INDColFechaConsecutivo"
        Me.INDColFechaConsecutivo.Visible = True
        Me.INDColFechaConsecutivo.VisibleIndex = 2
        Me.INDColFechaConsecutivo.Width = 111
        '
        'INDColProveedorConsecutivo
        '
        Me.INDColProveedorConsecutivo.Caption = "Proveedor"
        Me.INDColProveedorConsecutivo.FieldName = "IdThirdParty.Name"
        Me.INDColProveedorConsecutivo.Name = "INDColProveedorConsecutivo"
        Me.INDColProveedorConsecutivo.Visible = True
        Me.INDColProveedorConsecutivo.VisibleIndex = 3
        Me.INDColProveedorConsecutivo.Width = 115
        '
        'INDSbGenerareReport
        '
        Me.INDSbGenerareReport.Location = New System.Drawing.Point(398, 229)
        Me.INDSbGenerareReport.Name = "INDSbGenerareReport"
        Me.INDSbGenerareReport.Size = New System.Drawing.Size(346, 28)
        Me.INDSbGenerareReport.StyleController = Me.INDLcBase
        Me.INDSbGenerareReport.TabIndex = 8
        Me.INDSbGenerareReport.Text = "Generar"
        '
        'INDSbGenerateExcell
        '
        Me.INDSbGenerateExcell.ImageOptions.Image = Global.Presentation.Treasury.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbGenerateExcell.Location = New System.Drawing.Point(744, 229)
        Me.INDSbGenerateExcell.Name = "INDSbGenerateExcell"
        Me.INDSbGenerateExcell.Size = New System.Drawing.Size(40, 28)
        Me.INDSbGenerateExcell.StyleController = Me.INDLcBase
        Me.INDSbGenerateExcell.TabIndex = 47
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgBase.CustomizationFormText = "INDLcgBase"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterRequired, Me.INDLcgFilterOptional})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(848, 707)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgFilterRequired
        '
        Me.INDLcgFilterRequired.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterRequired.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterRequired.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgFilterRequired.CustomizationFormText = "LayoutControlGroup1"
        Me.INDLcgFilterRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDateEnd, Me.LayoutControlItem1})
        Me.INDLcgFilterRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterRequired.Name = "INDLcgFilterRequired"
        Me.INDLcgFilterRequired.Size = New System.Drawing.Size(414, 687)
        Me.INDLcgFilterRequired.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateStart
        Me.INDLciDateStart.CustomizationFormText = "INDLciDateStart"
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(390, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(390, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial:"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(86, 17)
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEnd
        Me.INDLciDateEnd.CustomizationFormText = "INDLciDateEnd"
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(390, 56)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(390, 56)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final:"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(86, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcExportExcell
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 112)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 522)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLcgFilterOptional
        '
        Me.INDLcgFilterOptional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterOptional.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterOptional.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgFilterOptional.CustomizationFormText = "LayoutControlGroup2"
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDSupport, Me.INDInvoice, Me.INDVoucher, Me.INDLciSbGenerareReport, Me.LayoutControlItem2})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(414, 687)
        Me.INDLcgFilterOptional.Text = "Filtros"
        '
        'INDSupport
        '
        Me.INDSupport.Control = Me.INDSleSupplier
        Me.INDSupport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDSupport.Location = New System.Drawing.Point(0, 0)
        Me.INDSupport.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDSupport.MinSize = New System.Drawing.Size(390, 56)
        Me.INDSupport.Name = "INDSupport"
        Me.INDSupport.Size = New System.Drawing.Size(390, 56)
        Me.INDSupport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDSupport.StartNewLine = True
        Me.INDSupport.Text = "Proveedor"
        Me.INDSupport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDSupport.TextSize = New System.Drawing.Size(86, 17)
        '
        'INDInvoice
        '
        Me.INDInvoice.Control = Me.INDSleInvoice
        Me.INDInvoice.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDInvoice.CustomizationFormText = "INDLciDateEnd"
        Me.INDInvoice.Location = New System.Drawing.Point(0, 56)
        Me.INDInvoice.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDInvoice.MinSize = New System.Drawing.Size(390, 56)
        Me.INDInvoice.Name = "INDInvoice"
        Me.INDInvoice.Size = New System.Drawing.Size(390, 56)
        Me.INDInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDInvoice.Text = "Factura"
        Me.INDInvoice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDInvoice.TextSize = New System.Drawing.Size(86, 17)
        '
        'INDVoucher
        '
        Me.INDVoucher.Control = Me.INDSleVoucher
        Me.INDVoucher.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDVoucher.CustomizationFormText = "Proveedor"
        Me.INDVoucher.Location = New System.Drawing.Point(0, 112)
        Me.INDVoucher.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDVoucher.MinSize = New System.Drawing.Size(390, 56)
        Me.INDVoucher.Name = "INDVoucher"
        Me.INDVoucher.Size = New System.Drawing.Size(390, 56)
        Me.INDVoucher.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDVoucher.StartNewLine = True
        Me.INDVoucher.Text = "Comprobante"
        Me.INDVoucher.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDVoucher.TextSize = New System.Drawing.Size(86, 17)
        '
        'INDLciSbGenerareReport
        '
        Me.INDLciSbGenerareReport.Control = Me.INDSbGenerareReport
        Me.INDLciSbGenerareReport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciSbGenerareReport.CustomizationFormText = "INDLciSbGenerareReport"
        Me.INDLciSbGenerareReport.Location = New System.Drawing.Point(0, 168)
        Me.INDLciSbGenerareReport.MaxSize = New System.Drawing.Size(348, 40)
        Me.INDLciSbGenerareReport.MinSize = New System.Drawing.Size(348, 40)
        Me.INDLciSbGenerareReport.Name = "INDLciSbGenerareReport"
        Me.INDLciSbGenerareReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 10, 2)
        Me.INDLciSbGenerareReport.Size = New System.Drawing.Size(348, 466)
        Me.INDLciSbGenerareReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSbGenerareReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciSbGenerareReport.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbGenerateExcell
        Me.LayoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.ImageOptions.Image = Global.Presentation.Treasury.My.Resources.Resources.ICONO_EXCEL_02
        Me.LayoutControlItem2.Location = New System.Drawing.Point(348, 168)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 2, 10, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(42, 466)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDCncNavigation
        '
        Me.INDCncNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncNavigation.LayoutControl = Me.INDLcBase
        Me.INDCncNavigation.Location = New System.Drawing.Point(0, 5)
        Me.INDCncNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncNavigation.Name = "INDCncNavigation"
        Me.INDCncNavigation.Size = New System.Drawing.Size(200, 724)
        Me.INDCncNavigation.TabIndex = 29
        Me.INDCncNavigation.UseDisabledStatePainter = False
        '
        'INDPcViewReport
        '
        Me.INDPcViewReport.Controls.Add(Me.INDDvViewReport)
        Me.INDPcViewReport.Controls.Add(Me.INDCnBack)
        Me.INDPcViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcViewReport.Location = New System.Drawing.Point(0, 5)
        Me.INDPcViewReport.Name = "INDPcViewReport"
        Me.INDPcViewReport.Size = New System.Drawing.Size(1008, 724)
        Me.INDPcViewReport.TabIndex = 32
        '
        'INDDvViewReport
        '
        Me.INDDvViewReport.Controls.Add(Me.barDockControlLeft)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlRight)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlBottom)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlTop)
        Me.INDDvViewReport.Controls.Add(Me.BarDockControl3)
        Me.INDDvViewReport.Controls.Add(Me.BarDockControl4)
        Me.INDDvViewReport.Controls.Add(Me.BarDockControl2)
        Me.INDDvViewReport.Controls.Add(Me.BarDockControl1)
        Me.INDDvViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvViewReport, True)
        Me.INDDvViewReport.IsMetric = False
        Me.INDDvViewReport.Location = New System.Drawing.Point(72, 2)
        Me.INDDvViewReport.Name = "INDDvViewReport"
        Me.INDDvViewReport.Size = New System.Drawing.Size(934, 720)
        Me.INDDvViewReport.TabIndex = 1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 24)
        Me.barDockControlLeft.Manager = Nothing
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 674)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(934, 24)
        Me.barDockControlRight.Manager = Nothing
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 674)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 698)
        Me.barDockControlBottom.Manager = Nothing
        Me.barDockControlBottom.Size = New System.Drawing.Size(934, 0)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 24)
        Me.barDockControlTop.Manager = Nothing
        Me.barDockControlTop.Size = New System.Drawing.Size(934, 0)
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(70, 720)
        Me.INDCnBack.TabIndex = 0
        '
        'PopupControlContainer2
        '
        Me.PopupControlContainer2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupControlContainer2.CloseOnLostFocus = False
        Me.PopupControlContainer2.Location = New System.Drawing.Point(998, 393)
        Me.PopupControlContainer2.Manager = Me.DocumentViewerBarManager1
        Me.PopupControlContainer2.Name = "PopupControlContainer2"
        Me.PopupControlContainer2.ShowSizeGrip = True
        Me.PopupControlContainer2.Size = New System.Drawing.Size(784, 318)
        Me.PopupControlContainer2.TabIndex = 40
        Me.PopupControlContainer2.Visible = False
        '
        'INDPopupccEdadPaciente
        '
        Me.INDPopupccEdadPaciente.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPopupccEdadPaciente.Location = New System.Drawing.Point(-465, 259)
        Me.INDPopupccEdadPaciente.Manager = Me.DocumentViewerBarManager1
        Me.INDPopupccEdadPaciente.Name = "INDPopupccEdadPaciente"
        Me.INDPopupccEdadPaciente.Size = New System.Drawing.Size(250, 58)
        Me.INDPopupccEdadPaciente.TabIndex = 39
        Me.INDPopupccEdadPaciente.Visible = False
        '
        'INDpccFilterRecords
        '
        Me.INDpccFilterRecords.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpccFilterRecords.CloseOnLostFocus = False
        Me.INDpccFilterRecords.Location = New System.Drawing.Point(428, 173)
        Me.INDpccFilterRecords.Manager = Me.DocumentViewerBarManager1
        Me.INDpccFilterRecords.Name = "INDpccFilterRecords"
        Me.INDpccFilterRecords.Size = New System.Drawing.Size(539, 320)
        Me.INDpccFilterRecords.TabIndex = 38
        Me.INDpccFilterRecords.Visible = False
        '
        'INDReportsPcc
        '
        Me.INDReportsPcc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDReportsPcc.Location = New System.Drawing.Point(-772, 315)
        Me.INDReportsPcc.Manager = Me.DocumentViewerBarManager1
        Me.INDReportsPcc.Name = "INDReportsPcc"
        Me.INDReportsPcc.Size = New System.Drawing.Size(309, 341)
        Me.INDReportsPcc.TabIndex = 37
        Me.INDReportsPcc.Visible = False
        '
        'INDAuditPcc
        '
        Me.INDAuditPcc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDAuditPcc.CloseOnLostFocus = False
        Me.INDAuditPcc.Location = New System.Drawing.Point(-14, 164)
        Me.INDAuditPcc.Manager = Me.DocumentViewerBarManager1
        Me.INDAuditPcc.Name = "INDAuditPcc"
        Me.INDAuditPcc.Size = New System.Drawing.Size(429, 121)
        Me.INDAuditPcc.TabIndex = 36
        Me.INDAuditPcc.Visible = False
        '
        'INDDocumentalSystemPcc
        '
        Me.INDDocumentalSystemPcc.AutoSize = True
        Me.INDDocumentalSystemPcc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDDocumentalSystemPcc.Location = New System.Drawing.Point(-460, 315)
        Me.INDDocumentalSystemPcc.Manager = Me.DocumentViewerBarManager1
        Me.INDDocumentalSystemPcc.Name = "INDDocumentalSystemPcc"
        Me.INDDocumentalSystemPcc.Size = New System.Drawing.Size(272, 287)
        Me.INDDocumentalSystemPcc.TabIndex = 35
        Me.INDDocumentalSystemPcc.Visible = False
        '
        'INDpopupcontainerRestablecer
        '
        Me.INDpopupcontainerRestablecer.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpopupcontainerRestablecer.Appearance.Options.UseFont = True
        Me.INDpopupcontainerRestablecer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpopupcontainerRestablecer.Location = New System.Drawing.Point(-493, 102)
        Me.INDpopupcontainerRestablecer.Manager = Me.DocumentViewerBarManager1
        Me.INDpopupcontainerRestablecer.Name = "INDpopupcontainerRestablecer"
        Me.INDpopupcontainerRestablecer.Size = New System.Drawing.Size(169, 35)
        Me.INDpopupcontainerRestablecer.TabIndex = 33
        Me.INDpopupcontainerRestablecer.Visible = False
        '
        'INDpopUpBiometrico
        '
        Me.INDpopUpBiometrico.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpopUpBiometrico.Location = New System.Drawing.Point(-171, 188)
        Me.INDpopUpBiometrico.Manager = Me.DocumentViewerBarManager1
        Me.INDpopUpBiometrico.Name = "INDpopUpBiometrico"
        Me.INDpopUpBiometrico.Size = New System.Drawing.Size(151, 175)
        Me.INDpopUpBiometrico.TabIndex = 34
        Me.INDpopUpBiometrico.Visible = False
        '
        'INDpccExportar
        '
        Me.INDpccExportar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpccExportar.Location = New System.Drawing.Point(-770, 161)
        Me.INDpccExportar.Manager = Me.DocumentViewerBarManager1
        Me.INDpccExportar.Name = "INDpccExportar"
        Me.INDpccExportar.Size = New System.Drawing.Size(424, 92)
        Me.INDpccExportar.TabIndex = 31
        Me.INDpccExportar.Visible = False
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Me
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.BarDockControl1)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.BarDockControl2)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.BarDockControl3)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.BarDockControl4)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvViewReport
        Me.DocumentViewerBarManager1.Form = Me.INDDvViewReport
        Me.DocumentViewerBarManager1.ImageStream = CType(resources.GetObject("DocumentViewerBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem16, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem27, Me.PrintPreviewBarItem28, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 57
        Me.DocumentViewerBarManager1.PreviewBar = Me.PreviewBar1
        Me.DocumentViewerBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemZoomTrackBar1, Me.PrintPreviewRepositoryItemComboBox1})
        Me.DocumentViewerBarManager1.StatusBar = Me.PreviewBar2
        Me.DocumentViewerBarManager1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.[True]
        '
        'PreviewBar1
        '
        Me.PreviewBar1.BarName = "Toolbar"
        Me.PreviewBar1.DockCol = 0
        Me.PreviewBar1.DockRow = 0
        Me.PreviewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.PreviewBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem5, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem6, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem7), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem13, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem14), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem15, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem16), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem17, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem18), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem19), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem21, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26, True)})
        Me.PreviewBar1.Text = "Toolbar"
        '
        'PrintPreviewBarItem2
        '
        Me.PrintPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem2.Caption = "Document Map"
        Me.PrintPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap
        Me.PrintPreviewBarItem2.Enabled = False
        Me.PrintPreviewBarItem2.Hint = "Document Map"
        Me.PrintPreviewBarItem2.Id = 7
        Me.PrintPreviewBarItem2.ImageOptions.ImageIndex = 19
        Me.PrintPreviewBarItem2.Name = "PrintPreviewBarItem2"
        '
        'PrintPreviewBarItem3
        '
        Me.PrintPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem3.Caption = "Parameters"
        Me.PrintPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters
        Me.PrintPreviewBarItem3.Enabled = False
        Me.PrintPreviewBarItem3.Hint = "Parameters"
        Me.PrintPreviewBarItem3.Id = 8
        Me.PrintPreviewBarItem3.ImageOptions.ImageIndex = 22
        Me.PrintPreviewBarItem3.Name = "PrintPreviewBarItem3"
        '
        'PrintPreviewBarItem4
        '
        Me.PrintPreviewBarItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem4.Caption = "Search"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Search"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageOptions.ImageIndex = 20
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.Caption = "Customize"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Customize"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageOptions.ImageIndex = 14
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Open"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Open a document"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageOptions.ImageIndex = 23
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Save"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Save the document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageOptions.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "&Print..."
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Print"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageOptions.ImageIndex = 0
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "P&rint"
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Quick Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageOptions.ImageIndex = 1
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Page Setup"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageOptions.ImageIndex = 2
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Header And Footer"
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Header And Footer"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageOptions.ImageIndex = 15
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.ActAsDropDown = True
        Me.PrintPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem12.Caption = "Scale"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Scale"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageOptions.ImageIndex = 25
        Me.PrintPreviewBarItem12.Name = "PrintPreviewBarItem12"
        '
        'PrintPreviewBarItem13
        '
        Me.PrintPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem13.Caption = "Hand Tool"
        Me.PrintPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool
        Me.PrintPreviewBarItem13.Enabled = False
        Me.PrintPreviewBarItem13.Hint = "Hand Tool"
        Me.PrintPreviewBarItem13.Id = 18
        Me.PrintPreviewBarItem13.ImageOptions.ImageIndex = 16
        Me.PrintPreviewBarItem13.Name = "PrintPreviewBarItem13"
        '
        'PrintPreviewBarItem14
        '
        Me.PrintPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem14.Caption = "Magnifier"
        Me.PrintPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier
        Me.PrintPreviewBarItem14.Enabled = False
        Me.PrintPreviewBarItem14.Hint = "Magnifier"
        Me.PrintPreviewBarItem14.Id = 19
        Me.PrintPreviewBarItem14.ImageOptions.ImageIndex = 3
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.Caption = "Zoom Out"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Zoom Out"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageOptions.ImageIndex = 5
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.EditWidth = 70
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 21
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        '
        'PrintPreviewRepositoryItemComboBox1
        '
        Me.PrintPreviewRepositoryItemComboBox1.AutoComplete = False
        Me.PrintPreviewRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PrintPreviewRepositoryItemComboBox1.DropDownRows = 11
        Me.PrintPreviewRepositoryItemComboBox1.Name = "PrintPreviewRepositoryItemComboBox1"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom In"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom In"
        Me.PrintPreviewBarItem16.Id = 22
        Me.PrintPreviewBarItem16.ImageOptions.ImageIndex = 4
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "First Page"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "First Page"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageOptions.ImageIndex = 7
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "Previous Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "Previous Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageOptions.ImageIndex = 8
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Next Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Next Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageOptions.ImageIndex = 9
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Last Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Last Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageOptions.ImageIndex = 10
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem21.Caption = "Multiple Pages"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Multiple Pages"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageOptions.ImageIndex = 11
        Me.PrintPreviewBarItem21.Name = "PrintPreviewBarItem21"
        '
        'PrintPreviewBarItem22
        '
        Me.PrintPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem22.Caption = "&Color..."
        Me.PrintPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground
        Me.PrintPreviewBarItem22.Enabled = False
        Me.PrintPreviewBarItem22.Hint = "Background"
        Me.PrintPreviewBarItem22.Id = 28
        Me.PrintPreviewBarItem22.ImageOptions.ImageIndex = 12
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.Caption = "&Watermark..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Watermark"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageOptions.ImageIndex = 21
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem24.Caption = "Export Document..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Export Document..."
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageOptions.ImageIndex = 18
        Me.PrintPreviewBarItem24.Name = "PrintPreviewBarItem24"
        '
        'PrintPreviewBarItem25
        '
        Me.PrintPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem25.Caption = "Send via E-Mail..."
        Me.PrintPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile
        Me.PrintPreviewBarItem25.Enabled = False
        Me.PrintPreviewBarItem25.Hint = "Send via E-Mail..."
        Me.PrintPreviewBarItem25.Id = 31
        Me.PrintPreviewBarItem25.ImageOptions.ImageIndex = 17
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.Caption = "E&xit"
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Close Preview"
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageOptions.ImageIndex = 13
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PreviewBar2
        '
        Me.PreviewBar2.BarName = "Status Bar"
        Me.PreviewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.PreviewBar2.DockCol = 0
        Me.PreviewBar2.DockRow = 0
        Me.PreviewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.PreviewBar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ProgressBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem2, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomTrackBarEditItem1)})
        Me.PreviewBar2.OptionsBar.AllowQuickCustomization = False
        Me.PreviewBar2.OptionsBar.DrawDragBorder = False
        Me.PreviewBar2.OptionsBar.UseWholeRow = True
        Me.PreviewBar2.Text = "Status Bar"
        '
        'PrintPreviewStaticItem1
        '
        Me.PrintPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem1.Caption = "ninguno"
        Me.PrintPreviewStaticItem1.Id = 0
        Me.PrintPreviewStaticItem1.LeftIndent = 1
        Me.PrintPreviewStaticItem1.Name = "PrintPreviewStaticItem1"
        Me.PrintPreviewStaticItem1.RightIndent = 1
        Me.PrintPreviewStaticItem1.Type = "PageOfPages"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarStaticItem1.Id = 1
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'ProgressBarEditItem1
        '
        Me.ProgressBarEditItem1.Edit = Me.RepositoryItemProgressBar1
        Me.ProgressBarEditItem1.EditHeight = 12
        Me.ProgressBarEditItem1.EditWidth = 150
        Me.ProgressBarEditItem1.Id = 2
        Me.ProgressBarEditItem1.Name = "ProgressBarEditItem1"
        Me.ProgressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RepositoryItemProgressBar1
        '
        Me.RepositoryItemProgressBar1.Name = "RepositoryItemProgressBar1"
        '
        'PrintPreviewBarItem1
        '
        Me.PrintPreviewBarItem1.Caption = "Stop"
        Me.PrintPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding
        Me.PrintPreviewBarItem1.Enabled = False
        Me.PrintPreviewBarItem1.Hint = "Stop"
        Me.PrintPreviewBarItem1.Id = 3
        Me.PrintPreviewBarItem1.Name = "PrintPreviewBarItem1"
        Me.PrintPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left
        Me.BarButtonItem1.Enabled = False
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.Name = "BarButtonItem1"
        Me.BarButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'PrintPreviewStaticItem2
        '
        Me.PrintPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.PrintPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem2.Caption = "100%"
        Me.PrintPreviewStaticItem2.Id = 5
        Me.PrintPreviewStaticItem2.Name = "PrintPreviewStaticItem2"
        Me.PrintPreviewStaticItem2.Size = New System.Drawing.Size(40, 0)
        Me.PrintPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far
        Me.PrintPreviewStaticItem2.Type = "ZoomFactor"
        Me.PrintPreviewStaticItem2.Width = 40
        '
        'ZoomTrackBarEditItem1
        '
        Me.ZoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.ZoomTrackBarEditItem1.Edit = Me.RepositoryItemZoomTrackBar1
        Me.ZoomTrackBarEditItem1.EditValue = 90
        Me.ZoomTrackBarEditItem1.EditWidth = 140
        Me.ZoomTrackBarEditItem1.Enabled = False
        Me.ZoomTrackBarEditItem1.Id = 6
        Me.ZoomTrackBarEditItem1.Name = "ZoomTrackBarEditItem1"
        Me.ZoomTrackBarEditItem1.Range = New Integer() {10, 500}
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemZoomTrackBar1.AllowFocused = False
        Me.RepositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RepositoryItemZoomTrackBar1.Maximum = 180
        Me.RepositoryItemZoomTrackBar1.Middle = 90
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 0)
        Me.BarDockControl1.Manager = Me.DocumentViewerBarManager1
        Me.BarDockControl1.Size = New System.Drawing.Size(934, 24)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 698)
        Me.BarDockControl2.Manager = Me.DocumentViewerBarManager1
        Me.BarDockControl2.Size = New System.Drawing.Size(934, 22)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 24)
        Me.BarDockControl3.Manager = Me.DocumentViewerBarManager1
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 674)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(934, 24)
        Me.BarDockControl4.Manager = Me.DocumentViewerBarManager1
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 674)
        '
        'PrintPreviewSubItem1
        '
        Me.PrintPreviewSubItem1.Caption = "&File"
        Me.PrintPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File
        Me.PrintPreviewSubItem1.Id = 33
        Me.PrintPreviewSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26, True)})
        Me.PrintPreviewSubItem1.Name = "PrintPreviewSubItem1"
        '
        'PrintPreviewSubItem2
        '
        Me.PrintPreviewSubItem2.Caption = "&View"
        Me.PrintPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View
        Me.PrintPreviewSubItem2.Id = 34
        Me.PrintPreviewSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarToolbarsListItem1, True)})
        Me.PrintPreviewSubItem2.Name = "PrintPreviewSubItem2"
        '
        'PrintPreviewSubItem4
        '
        Me.PrintPreviewSubItem4.Caption = "&Page Layout"
        Me.PrintPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout
        Me.PrintPreviewSubItem4.Id = 36
        Me.PrintPreviewSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem28)})
        Me.PrintPreviewSubItem4.Name = "PrintPreviewSubItem4"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem27.Caption = "&Facing"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.GroupIndex = 100
        Me.PrintPreviewBarItem27.Id = 37
        Me.PrintPreviewBarItem27.Name = "PrintPreviewBarItem27"
        '
        'PrintPreviewBarItem28
        '
        Me.PrintPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem28.Caption = "&Continuous"
        Me.PrintPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous
        Me.PrintPreviewBarItem28.Enabled = False
        Me.PrintPreviewBarItem28.GroupIndex = 100
        Me.PrintPreviewBarItem28.Id = 38
        Me.PrintPreviewBarItem28.Name = "PrintPreviewBarItem28"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "Bars"
        Me.BarToolbarsListItem1.Id = 39
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'PrintPreviewSubItem3
        '
        Me.PrintPreviewSubItem3.Caption = "&Background"
        Me.PrintPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background
        Me.PrintPreviewSubItem3.Id = 35
        Me.PrintPreviewSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23)})
        Me.PrintPreviewSubItem3.Name = "PrintPreviewSubItem3"
        '
        'PrintPreviewBarCheckItem1
        '
        Me.PrintPreviewBarCheckItem1.BindableChecked = True
        Me.PrintPreviewBarCheckItem1.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem1.Checked = True
        Me.PrintPreviewBarCheckItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf
        Me.PrintPreviewBarCheckItem1.Enabled = False
        Me.PrintPreviewBarCheckItem1.GroupIndex = 2
        Me.PrintPreviewBarCheckItem1.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem1.Id = 40
        Me.PrintPreviewBarCheckItem1.Name = "PrintPreviewBarCheckItem1"
        '
        'PrintPreviewBarCheckItem2
        '
        Me.PrintPreviewBarCheckItem2.Caption = "HTML File"
        Me.PrintPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm
        Me.PrintPreviewBarCheckItem2.Enabled = False
        Me.PrintPreviewBarCheckItem2.GroupIndex = 2
        Me.PrintPreviewBarCheckItem2.Hint = "HTML File"
        Me.PrintPreviewBarCheckItem2.Id = 41
        Me.PrintPreviewBarCheckItem2.Name = "PrintPreviewBarCheckItem2"
        '
        'PrintPreviewBarCheckItem3
        '
        Me.PrintPreviewBarCheckItem3.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht
        Me.PrintPreviewBarCheckItem3.Enabled = False
        Me.PrintPreviewBarCheckItem3.GroupIndex = 2
        Me.PrintPreviewBarCheckItem3.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem3.Id = 42
        Me.PrintPreviewBarCheckItem3.Name = "PrintPreviewBarCheckItem3"
        '
        'PrintPreviewBarCheckItem4
        '
        Me.PrintPreviewBarCheckItem4.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf
        Me.PrintPreviewBarCheckItem4.Enabled = False
        Me.PrintPreviewBarCheckItem4.GroupIndex = 2
        Me.PrintPreviewBarCheckItem4.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem4.Id = 43
        Me.PrintPreviewBarCheckItem4.Name = "PrintPreviewBarCheckItem4"
        '
        'PrintPreviewBarCheckItem5
        '
        Me.PrintPreviewBarCheckItem5.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls
        Me.PrintPreviewBarCheckItem5.Enabled = False
        Me.PrintPreviewBarCheckItem5.GroupIndex = 2
        Me.PrintPreviewBarCheckItem5.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem5.Id = 44
        Me.PrintPreviewBarCheckItem5.Name = "PrintPreviewBarCheckItem5"
        '
        'PrintPreviewBarCheckItem6
        '
        Me.PrintPreviewBarCheckItem6.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx
        Me.PrintPreviewBarCheckItem6.Enabled = False
        Me.PrintPreviewBarCheckItem6.GroupIndex = 2
        Me.PrintPreviewBarCheckItem6.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Id = 45
        Me.PrintPreviewBarCheckItem6.Name = "PrintPreviewBarCheckItem6"
        '
        'PrintPreviewBarCheckItem7
        '
        Me.PrintPreviewBarCheckItem7.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv
        Me.PrintPreviewBarCheckItem7.Enabled = False
        Me.PrintPreviewBarCheckItem7.GroupIndex = 2
        Me.PrintPreviewBarCheckItem7.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem7.Id = 46
        Me.PrintPreviewBarCheckItem7.Name = "PrintPreviewBarCheckItem7"
        '
        'PrintPreviewBarCheckItem8
        '
        Me.PrintPreviewBarCheckItem8.Caption = "Text File"
        Me.PrintPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt
        Me.PrintPreviewBarCheckItem8.Enabled = False
        Me.PrintPreviewBarCheckItem8.GroupIndex = 2
        Me.PrintPreviewBarCheckItem8.Hint = "Text File"
        Me.PrintPreviewBarCheckItem8.Id = 47
        Me.PrintPreviewBarCheckItem8.Name = "PrintPreviewBarCheckItem8"
        '
        'PrintPreviewBarCheckItem9
        '
        Me.PrintPreviewBarCheckItem9.Caption = "Image File"
        Me.PrintPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic
        Me.PrintPreviewBarCheckItem9.Enabled = False
        Me.PrintPreviewBarCheckItem9.GroupIndex = 2
        Me.PrintPreviewBarCheckItem9.Hint = "Image File"
        Me.PrintPreviewBarCheckItem9.Id = 48
        Me.PrintPreviewBarCheckItem9.Name = "PrintPreviewBarCheckItem9"
        '
        'PrintPreviewBarCheckItem10
        '
        Me.PrintPreviewBarCheckItem10.BindableChecked = True
        Me.PrintPreviewBarCheckItem10.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem10.Checked = True
        Me.PrintPreviewBarCheckItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf
        Me.PrintPreviewBarCheckItem10.Enabled = False
        Me.PrintPreviewBarCheckItem10.GroupIndex = 1
        Me.PrintPreviewBarCheckItem10.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem10.Id = 49
        Me.PrintPreviewBarCheckItem10.Name = "PrintPreviewBarCheckItem10"
        '
        'PrintPreviewBarCheckItem11
        '
        Me.PrintPreviewBarCheckItem11.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht
        Me.PrintPreviewBarCheckItem11.Enabled = False
        Me.PrintPreviewBarCheckItem11.GroupIndex = 1
        Me.PrintPreviewBarCheckItem11.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem11.Id = 50
        Me.PrintPreviewBarCheckItem11.Name = "PrintPreviewBarCheckItem11"
        '
        'PrintPreviewBarCheckItem12
        '
        Me.PrintPreviewBarCheckItem12.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf
        Me.PrintPreviewBarCheckItem12.Enabled = False
        Me.PrintPreviewBarCheckItem12.GroupIndex = 1
        Me.PrintPreviewBarCheckItem12.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem12.Id = 51
        Me.PrintPreviewBarCheckItem12.Name = "PrintPreviewBarCheckItem12"
        '
        'PrintPreviewBarCheckItem13
        '
        Me.PrintPreviewBarCheckItem13.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls
        Me.PrintPreviewBarCheckItem13.Enabled = False
        Me.PrintPreviewBarCheckItem13.GroupIndex = 1
        Me.PrintPreviewBarCheckItem13.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem13.Id = 52
        Me.PrintPreviewBarCheckItem13.Name = "PrintPreviewBarCheckItem13"
        '
        'PrintPreviewBarCheckItem14
        '
        Me.PrintPreviewBarCheckItem14.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx
        Me.PrintPreviewBarCheckItem14.Enabled = False
        Me.PrintPreviewBarCheckItem14.GroupIndex = 1
        Me.PrintPreviewBarCheckItem14.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Id = 53
        Me.PrintPreviewBarCheckItem14.Name = "PrintPreviewBarCheckItem14"
        '
        'PrintPreviewBarCheckItem15
        '
        Me.PrintPreviewBarCheckItem15.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv
        Me.PrintPreviewBarCheckItem15.Enabled = False
        Me.PrintPreviewBarCheckItem15.GroupIndex = 1
        Me.PrintPreviewBarCheckItem15.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem15.Id = 54
        Me.PrintPreviewBarCheckItem15.Name = "PrintPreviewBarCheckItem15"
        '
        'PrintPreviewBarCheckItem16
        '
        Me.PrintPreviewBarCheckItem16.Caption = "Text File"
        Me.PrintPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt
        Me.PrintPreviewBarCheckItem16.Enabled = False
        Me.PrintPreviewBarCheckItem16.GroupIndex = 1
        Me.PrintPreviewBarCheckItem16.Hint = "Text File"
        Me.PrintPreviewBarCheckItem16.Id = 55
        Me.PrintPreviewBarCheckItem16.Name = "PrintPreviewBarCheckItem16"
        '
        'PrintPreviewBarCheckItem17
        '
        Me.PrintPreviewBarCheckItem17.Caption = "Image File"
        Me.PrintPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic
        Me.PrintPreviewBarCheckItem17.Enabled = False
        Me.PrintPreviewBarCheckItem17.GroupIndex = 1
        Me.PrintPreviewBarCheckItem17.Hint = "Image File"
        Me.PrintPreviewBarCheckItem17.Id = 56
        Me.PrintPreviewBarCheckItem17.Name = "PrintPreviewBarCheckItem17"
        '
        'FrmSupportPaymentSuppliers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.INDLcBase)
        Me.Controls.Add(Me.INDCncNavigation)
        Me.Controls.Add(Me.INDPcViewReport)
        Me.Controls.Add(Me.PopupControlContainer2)
        Me.Controls.Add(Me.INDPopupccEdadPaciente)
        Me.Controls.Add(Me.INDpccFilterRecords)
        Me.Controls.Add(Me.INDReportsPcc)
        Me.Controls.Add(Me.INDAuditPcc)
        Me.Controls.Add(Me.INDDocumentalSystemPcc)
        Me.Controls.Add(Me.INDpopupcontainerRestablecer)
        Me.Controls.Add(Me.INDpopUpBiometrico)
        Me.Controls.Add(Me.INDpccExportar)
        Me.IconOptions.Image = Global.Presentation.Treasury.My.Resources.Resources.Add_16
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmSupportPaymentSuppliers"
        Me.Opacity = 1.0R
        Me.Tag = "2830"
        Me.Text = "Soporte de pago proveedores"
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDpccExportar, 0)
        Me.Controls.SetChildIndex(Me.INDpopUpBiometrico, 0)
        Me.Controls.SetChildIndex(Me.INDpopupcontainerRestablecer, 0)
        Me.Controls.SetChildIndex(Me.INDDocumentalSystemPcc, 0)
        Me.Controls.SetChildIndex(Me.INDAuditPcc, 0)
        Me.Controls.SetChildIndex(Me.INDReportsPcc, 0)
        Me.Controls.SetChildIndex(Me.INDpccFilterRecords, 0)
        Me.Controls.SetChildIndex(Me.INDPopupccEdadPaciente, 0)
        Me.Controls.SetChildIndex(Me.PopupControlContainer2, 0)
        Me.Controls.SetChildIndex(Me.INDPcViewReport, 0)
        Me.Controls.SetChildIndex(Me.INDCncNavigation, 0)
        Me.Controls.SetChildIndex(Me.INDLcBase, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlevSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlevInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleVoucher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlevVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSbGenerareReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcViewReport.ResumeLayout(False)
        Me.INDDvViewReport.ResumeLayout(False)
        Me.INDDvViewReport.PerformLayout()
        CType(Me.PopupControlContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupccEdadPaciente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccFilterRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDReportsPcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDAuditPcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDocumentalSystemPcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopupcontainerRestablecer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopUpBiometrico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccExportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcExportExcell As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgFilterRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDCncNavigation As Controls.CtrNavigationControlPanel
    Friend WithEvents INDPcViewReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDDvViewReport As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDCnBack As Controls.CtrNavigation
    Friend WithEvents PopupControlContainer2 As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDPopupccEdadPaciente As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDpccFilterRecords As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDReportsPcc As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDAuditPcc As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDDocumentalSystemPcc As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDpopupcontainerRestablecer As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDpopUpBiometrico As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDpccExportar As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDSupport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlevSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleInvoice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlevInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleVoucher As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlevVoucher As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDVoucher As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbGenerareReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciSbGenerareReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbGenerateExcell As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColSupplierUnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInvoiceUnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFactura As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIndProveedor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVoucherUnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColConsecutivo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFechaConsecutivo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProveedorConsecutivo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCxP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolProveedor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolNoFac As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolValFac As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolSubFac As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolReteFuente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolICA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolReteIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolValPagar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolND As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolNC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolDesFin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolFCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolVPgado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolRefPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCBP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents indcolvouchercode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDocumentViewer1 As IndigoDocumentViewer
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents DocumentViewerBarManager1 As DevExpress.XtraPrinting.Preview.DocumentViewerBarManager
    Friend WithEvents PreviewBar1 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewBarItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents ZoomBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomBarEditItem
    Friend WithEvents PrintPreviewRepositoryItemComboBox1 As DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox
    Friend WithEvents PrintPreviewBarItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem18 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem19 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem20 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem21 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem22 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem23 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem24 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem25 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem26 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PreviewBar2 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewStaticItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents ProgressBarEditItem1 As DevExpress.XtraPrinting.Preview.ProgressBarEditItem
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents PrintPreviewBarItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PrintPreviewStaticItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents ZoomTrackBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents PrintPreviewSubItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarItem27 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem28 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents PrintPreviewSubItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarCheckItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
End Class
