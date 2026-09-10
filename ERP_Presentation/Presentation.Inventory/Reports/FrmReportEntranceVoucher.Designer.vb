Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportEntranceVoucher
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportEntranceVoucher))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDCbeSupplierStatusEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeEntranceVoucherStatusEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCncNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcTypeReport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleStatus = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleGroupBy = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcGroupBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSupplier_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier_ThirdPartyDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier_ThirdPartyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier_City = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleDocument = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvDocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDocument_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument_ThirdPartyNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument_ThirdPartyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument_WarehouseId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn761 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn771 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbGenerateExcell = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGroupBy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDDvDocumentViewer = New DevExpress.XtraPrinting.Preview.DocumentViewer()
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
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.INDPcDocumentViewer = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGcExportExcell = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeSupplierStatusEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeEntranceVoucherStatusEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleGroupBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGroupBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvDocumentViewer.SuspendLayout()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDocumentViewer.SuspendLayout()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncNavigation)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcDocumentViewer)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 7)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1543, 890)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1693, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDCbeSupplierStatusEnd
        '
        Me.INDCbeSupplierStatusEnd.AutoHeight = False
        Me.INDCbeSupplierStatusEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeSupplierStatusEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", False, -1)})
        Me.INDCbeSupplierStatusEnd.Name = "INDCbeSupplierStatusEnd"
        '
        'INDCbeEntranceVoucherStatusEnd
        '
        Me.INDCbeEntranceVoucherStatusEnd.AutoHeight = False
        Me.INDCbeEntranceVoucherStatusEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeEntranceVoucherStatusEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Registrado", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Confirmado", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anulado", CType(3, Byte), -1)})
        Me.INDCbeEntranceVoucherStatusEnd.Name = "INDCbeEntranceVoucherStatusEnd"
        '
        'INDCncNavigation
        '
        Me.INDCncNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncNavigation.LayoutControl = Me.INDLcBase
        Me.INDCncNavigation.Location = New System.Drawing.Point(2, 9)
        Me.INDCncNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncNavigation.Name = "INDCncNavigation"
        Me.INDCncNavigation.Size = New System.Drawing.Size(200, 879)
        Me.INDCncNavigation.TabIndex = 0
        Me.INDCncNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDGleTypeReport)
        Me.INDLcBase.Controls.Add(Me.INDGleStatus)
        Me.INDLcBase.Controls.Add(Me.INDGleGroupBy)
        Me.INDLcBase.Controls.Add(Me.INDDeDateEnd)
        Me.INDLcBase.Controls.Add(Me.INDDeDateStart)
        Me.INDLcBase.Controls.Add(Me.INDSleSupplier)
        Me.INDLcBase.Controls.Add(Me.INDSleDocument)
        Me.INDLcBase.Controls.Add(Me.INDsleCurrency)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateExcell)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 9)
        Me.INDLcBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(1339, 879)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(579, 202)
        Me.INDSbGenerateReport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(435, 39)
        Me.INDSbGenerateReport.MinimumSize = New System.Drawing.Size(435, 39)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(435, 39)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 10
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDGleTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.Location = New System.Drawing.Point(24, 381)
        Me.INDGleTypeReport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleTypeReport.Name = "INDGleTypeReport"
        Me.INDGleTypeReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTypeReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeReport.Properties.DisplayMember = "Item2"
        Me.INDGleTypeReport.Properties.ImmediatePopup = True
        Me.INDGleTypeReport.Properties.NullText = ""
        Me.INDGleTypeReport.Properties.PopupView = Me.GridLookUpEdit3View
        Me.INDGleTypeReport.Properties.ValueMember = "Item1"
        Me.INDGleTypeReport.Size = New System.Drawing.Size(416, 34)
        Me.INDGleTypeReport.StyleController = Me.INDLcBase
        Me.INDGleTypeReport.TabIndex = 5
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeReport, Nothing)
        '
        'GridLookUpEdit3View
        '
        Me.GridLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcTypeReport})
        Me.GridLookUpEdit3View.DetailHeight = 431
        Me.GridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit3View.Name = "GridLookUpEdit3View"
        Me.GridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit3View, False)
        '
        'INDGrcTypeReport
        '
        Me.INDGrcTypeReport.Caption = "Tipo Reporte"
        Me.INDGrcTypeReport.FieldName = "Item2"
        Me.INDGrcTypeReport.MinWidth = 23
        Me.INDGrcTypeReport.Name = "INDGrcTypeReport"
        Me.INDGrcTypeReport.Visible = True
        Me.INDGrcTypeReport.VisibleIndex = 0
        Me.INDGrcTypeReport.Width = 87
        '
        'INDGleStatus
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleStatus, False)
        Me.INDGleStatus.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleStatus, False)
        Me.INDGleStatus.Location = New System.Drawing.Point(24, 307)
        Me.INDGleStatus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleStatus.Name = "INDGleStatus"
        Me.INDGleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDGleStatus.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleStatus.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleStatus.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleStatus.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleStatus.Properties.DisplayMember = "Item2"
        Me.INDGleStatus.Properties.ImmediatePopup = True
        Me.INDGleStatus.Properties.NullText = ""
        Me.INDGleStatus.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleStatus.Properties.ValueMember = "Item1"
        Me.INDGleStatus.Size = New System.Drawing.Size(416, 34)
        Me.INDGleStatus.StyleController = Me.INDLcBase
        Me.INDGleStatus.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleStatus, Nothing)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcStatus})
        Me.GridLookUpEdit2View.DetailHeight = 431
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'INDGrcStatus
        '
        Me.INDGrcStatus.Caption = "Estado"
        Me.INDGrcStatus.FieldName = "Item2"
        Me.INDGrcStatus.MinWidth = 23
        Me.INDGrcStatus.Name = "INDGrcStatus"
        Me.INDGrcStatus.Visible = True
        Me.INDGrcStatus.VisibleIndex = 0
        Me.INDGrcStatus.Width = 87
        '
        'INDGleGroupBy
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleGroupBy, False)
        Me.INDGleGroupBy.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleGroupBy, False)
        Me.INDGleGroupBy.Location = New System.Drawing.Point(24, 233)
        Me.INDGleGroupBy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleGroupBy.Name = "INDGleGroupBy"
        Me.INDGleGroupBy.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleGroupBy.Properties.Appearance.Options.UseFont = True
        Me.INDGleGroupBy.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleGroupBy.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleGroupBy.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleGroupBy.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleGroupBy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleGroupBy.Properties.DisplayMember = "Item2"
        Me.INDGleGroupBy.Properties.ImmediatePopup = True
        Me.INDGleGroupBy.Properties.NullText = ""
        Me.INDGleGroupBy.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleGroupBy.Properties.ValueMember = "Item1"
        Me.INDGleGroupBy.Size = New System.Drawing.Size(416, 34)
        Me.INDGleGroupBy.StyleController = Me.INDLcBase
        Me.INDGleGroupBy.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleGroupBy, Nothing)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcGroupBy})
        Me.GridLookUpEdit1View.DetailHeight = 431
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDGrcGroupBy
        '
        Me.INDGrcGroupBy.Caption = "Agrupamiento"
        Me.INDGrcGroupBy.FieldName = "Item2"
        Me.INDGrcGroupBy.MinWidth = 23
        Me.INDGrcGroupBy.Name = "INDGrcGroupBy"
        Me.INDGrcGroupBy.Visible = True
        Me.INDGrcGroupBy.VisibleIndex = 0
        Me.INDGrcGroupBy.Width = 87
        '
        'INDDeDateEnd
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDateEnd, False)
        Me.INDDeDateEnd.EditValue = Nothing
        Me.INDDeDateEnd.EnterMoveNextControl = True
        Me.INDDeDateEnd.Location = New System.Drawing.Point(24, 159)
        Me.INDDeDateEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDateEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDateEnd.Name = "INDDeDateEnd"
        Me.INDDeDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDeDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateEnd.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDateEnd.Size = New System.Drawing.Size(416, 34)
        Me.INDDeDateEnd.StyleController = Me.INDLcBase
        Me.INDDeDateEnd.TabIndex = 2
        '
        'INDDeDateStart
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDateStart, False)
        Me.INDDeDateStart.EditValue = Nothing
        Me.INDDeDateStart.EnterMoveNextControl = True
        Me.INDDeDateStart.Location = New System.Drawing.Point(24, 85)
        Me.INDDeDateStart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDateStart, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDateStart.Name = "INDDeDateStart"
        Me.INDDeDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDeDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateStart.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDateStart.Size = New System.Drawing.Size(416, 34)
        Me.INDDeDateStart.StyleController = Me.INDLcBase
        Me.INDDeDateStart.TabIndex = 1
        '
        'INDSleSupplier
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupplier, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupplier, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Location = New System.Drawing.Point(579, 85)
        Me.INDSleSupplier.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleSupplier.Name = "INDSleSupplier"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplier.Properties.NullText = ""
        Me.INDSleSupplier.Properties.PopupView = Me.INDGvSupplier
        Me.INDSleSupplier.Properties.ShowClearButton = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupplier, True)
        Me.INDSleSupplier.Size = New System.Drawing.Size(435, 34)
        Me.INDSleSupplier.StyleController = Me.INDLcBase
        Me.INDSleSupplier.TabIndex = 11
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupplier, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupplier, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSupplier, False)
        '
        'INDGvSupplier
        '
        Me.INDGvSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSupplier.Appearance.Row.Options.UseFont = True
        Me.INDGvSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSupplier_UnboundSelection, Me.INDColSupplier_ThirdPartyDocument, Me.INDColSupplier_ThirdPartyName, Me.INDColSupplier_Name, Me.INDColSupplier_City, Me.INDColSupplier_Status})
        Me.INDGvSupplier.DetailHeight = 431
        Me.INDGvSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSupplier.Name = "INDGvSupplier"
        Me.INDGvSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSupplier.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvSupplier.OptionsSelection.MultiSelect = True
        Me.INDGvSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSupplier.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSupplier.OptionsView.ShowDetailButtons = False
        Me.INDGvSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSupplier, False)
        '
        'INDColSupplier_UnboundSelection
        '
        Me.INDColSupplier_UnboundSelection.Caption = " "
        Me.INDColSupplier_UnboundSelection.FieldName = "INDColSupplier_UnboundSelection"
        Me.INDColSupplier_UnboundSelection.MinWidth = 23
        Me.INDColSupplier_UnboundSelection.Name = "INDColSupplier_UnboundSelection"
        Me.INDColSupplier_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSupplier_UnboundSelection.Visible = True
        Me.INDColSupplier_UnboundSelection.VisibleIndex = 0
        Me.INDColSupplier_UnboundSelection.Width = 23
        '
        'INDColSupplier_ThirdPartyDocument
        '
        Me.INDColSupplier_ThirdPartyDocument.Caption = "Tercero Documento"
        Me.INDColSupplier_ThirdPartyDocument.FieldName = "IdThirdParty.Nit"
        Me.INDColSupplier_ThirdPartyDocument.MinWidth = 23
        Me.INDColSupplier_ThirdPartyDocument.Name = "INDColSupplier_ThirdPartyDocument"
        Me.INDColSupplier_ThirdPartyDocument.OptionsColumn.AllowEdit = False
        Me.INDColSupplier_ThirdPartyDocument.OptionsColumn.AllowFocus = False
        Me.INDColSupplier_ThirdPartyDocument.Visible = True
        Me.INDColSupplier_ThirdPartyDocument.VisibleIndex = 1
        Me.INDColSupplier_ThirdPartyDocument.Width = 175
        '
        'INDColSupplier_ThirdPartyName
        '
        Me.INDColSupplier_ThirdPartyName.Caption = "Tercero Nombre"
        Me.INDColSupplier_ThirdPartyName.FieldName = "IdThirdParty.Name"
        Me.INDColSupplier_ThirdPartyName.MinWidth = 23
        Me.INDColSupplier_ThirdPartyName.Name = "INDColSupplier_ThirdPartyName"
        Me.INDColSupplier_ThirdPartyName.OptionsColumn.AllowEdit = False
        Me.INDColSupplier_ThirdPartyName.OptionsColumn.AllowFocus = False
        Me.INDColSupplier_ThirdPartyName.Visible = True
        Me.INDColSupplier_ThirdPartyName.VisibleIndex = 2
        Me.INDColSupplier_ThirdPartyName.Width = 233
        '
        'INDColSupplier_Name
        '
        Me.INDColSupplier_Name.Caption = "Nombre"
        Me.INDColSupplier_Name.FieldName = "Name"
        Me.INDColSupplier_Name.MinWidth = 23
        Me.INDColSupplier_Name.Name = "INDColSupplier_Name"
        Me.INDColSupplier_Name.OptionsColumn.AllowEdit = False
        Me.INDColSupplier_Name.OptionsColumn.AllowFocus = False
        Me.INDColSupplier_Name.Visible = True
        Me.INDColSupplier_Name.VisibleIndex = 3
        Me.INDColSupplier_Name.Width = 233
        '
        'INDColSupplier_City
        '
        Me.INDColSupplier_City.Caption = "Ciudad"
        Me.INDColSupplier_City.FieldName = "IdCity.Name"
        Me.INDColSupplier_City.MinWidth = 23
        Me.INDColSupplier_City.Name = "INDColSupplier_City"
        Me.INDColSupplier_City.OptionsColumn.AllowEdit = False
        Me.INDColSupplier_City.OptionsColumn.AllowFocus = False
        Me.INDColSupplier_City.Visible = True
        Me.INDColSupplier_City.VisibleIndex = 4
        Me.INDColSupplier_City.Width = 175
        '
        'INDColSupplier_Status
        '
        Me.INDColSupplier_Status.Caption = "Estado"
        Me.INDColSupplier_Status.FieldName = "StatusName"
        Me.INDColSupplier_Status.MinWidth = 23
        Me.INDColSupplier_Status.Name = "INDColSupplier_Status"
        Me.INDColSupplier_Status.OptionsColumn.AllowEdit = False
        Me.INDColSupplier_Status.OptionsColumn.AllowFocus = False
        Me.INDColSupplier_Status.Visible = True
        Me.INDColSupplier_Status.VisibleIndex = 5
        Me.INDColSupplier_Status.Width = 175
        '
        'INDSleDocument
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleDocument, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleDocument, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.Location = New System.Drawing.Point(579, 154)
        Me.INDSleDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleDocument.Name = "INDSleDocument"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleDocument, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleDocument.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleDocument.Properties.Appearance.Options.UseFont = True
        Me.INDSleDocument.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDocument.Properties.DisplayMember = "code"
        Me.INDSleDocument.Properties.NullText = ""
        Me.INDSleDocument.Properties.PopupView = Me.INDGvDocument
        Me.INDSleDocument.Properties.ShowClearButton = False
        Me.INDSleDocument.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleDocument, True)
        Me.INDSleDocument.Size = New System.Drawing.Size(435, 34)
        Me.INDSleDocument.StyleController = Me.INDLcBase
        Me.INDSleDocument.TabIndex = 12
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleDocument, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleDocument, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleDocument, False)
        '
        'INDGvDocument
        '
        Me.INDGvDocument.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDocument.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDocument.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDocument.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDocument.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocument.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDocument.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocument.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDocument.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDocument.Appearance.Row.Options.UseFont = True
        Me.INDGvDocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDocument_UnboundSelection, Me.INDColDocumentDate, Me.INDColDocument_Code, Me.INDColDocument_ThirdPartyNit, Me.INDColDocument_ThirdPartyName, Me.INDColDocument_WarehouseId, Me.INDColDocument_Status})
        Me.INDGvDocument.DetailHeight = 431
        Me.INDGvDocument.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvDocument.Name = "INDGvDocument"
        Me.INDGvDocument.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvDocument.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvDocument.OptionsSelection.MultiSelect = True
        Me.INDGvDocument.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDocument.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDocument.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDocument.OptionsView.ShowDetailButtons = False
        Me.INDGvDocument.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDocument, False)
        '
        'INDColDocument_UnboundSelection
        '
        Me.INDColDocument_UnboundSelection.Caption = " "
        Me.INDColDocument_UnboundSelection.FieldName = "INDColDocument_UnboundSelection"
        Me.INDColDocument_UnboundSelection.MinWidth = 23
        Me.INDColDocument_UnboundSelection.Name = "INDColDocument_UnboundSelection"
        Me.INDColDocument_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColDocument_UnboundSelection.Visible = True
        Me.INDColDocument_UnboundSelection.VisibleIndex = 0
        Me.INDColDocument_UnboundSelection.Width = 23
        '
        'INDColDocumentDate
        '
        Me.INDColDocumentDate.Caption = "Fecha Documento"
        Me.INDColDocumentDate.FieldName = "DocumentDate"
        Me.INDColDocumentDate.MinWidth = 23
        Me.INDColDocumentDate.Name = "INDColDocumentDate"
        Me.INDColDocumentDate.OptionsColumn.AllowEdit = False
        Me.INDColDocumentDate.OptionsColumn.AllowFocus = False
        Me.INDColDocumentDate.Visible = True
        Me.INDColDocumentDate.VisibleIndex = 1
        Me.INDColDocumentDate.Width = 212
        '
        'INDColDocument_Code
        '
        Me.INDColDocument_Code.Caption = "Codigo"
        Me.INDColDocument_Code.FieldName = "Code"
        Me.INDColDocument_Code.MinWidth = 23
        Me.INDColDocument_Code.Name = "INDColDocument_Code"
        Me.INDColDocument_Code.OptionsColumn.AllowEdit = False
        Me.INDColDocument_Code.OptionsColumn.AllowFocus = False
        Me.INDColDocument_Code.Visible = True
        Me.INDColDocument_Code.VisibleIndex = 3
        Me.INDColDocument_Code.Width = 87
        '
        'INDColDocument_ThirdPartyNit
        '
        Me.INDColDocument_ThirdPartyNit.Caption = "Nit Proveedor"
        Me.INDColDocument_ThirdPartyNit.FieldName = "SupplierId.IdThirdParty.Nit"
        Me.INDColDocument_ThirdPartyNit.MinWidth = 23
        Me.INDColDocument_ThirdPartyNit.Name = "INDColDocument_ThirdPartyNit"
        Me.INDColDocument_ThirdPartyNit.OptionsColumn.AllowEdit = False
        Me.INDColDocument_ThirdPartyNit.OptionsColumn.AllowFocus = False
        Me.INDColDocument_ThirdPartyNit.Visible = True
        Me.INDColDocument_ThirdPartyNit.VisibleIndex = 2
        Me.INDColDocument_ThirdPartyNit.Width = 87
        '
        'INDColDocument_ThirdPartyName
        '
        Me.INDColDocument_ThirdPartyName.Caption = "Nombre Proveedor"
        Me.INDColDocument_ThirdPartyName.FieldName = "SupplierId.IdThirdParty.Name"
        Me.INDColDocument_ThirdPartyName.MinWidth = 23
        Me.INDColDocument_ThirdPartyName.Name = "INDColDocument_ThirdPartyName"
        Me.INDColDocument_ThirdPartyName.OptionsColumn.AllowEdit = False
        Me.INDColDocument_ThirdPartyName.OptionsColumn.AllowFocus = False
        Me.INDColDocument_ThirdPartyName.Visible = True
        Me.INDColDocument_ThirdPartyName.VisibleIndex = 6
        Me.INDColDocument_ThirdPartyName.Width = 87
        '
        'INDColDocument_WarehouseId
        '
        Me.INDColDocument_WarehouseId.Caption = "Almacen"
        Me.INDColDocument_WarehouseId.FieldName = "WarehouseId.Code"
        Me.INDColDocument_WarehouseId.MinWidth = 23
        Me.INDColDocument_WarehouseId.Name = "INDColDocument_WarehouseId"
        Me.INDColDocument_WarehouseId.OptionsColumn.AllowEdit = False
        Me.INDColDocument_WarehouseId.OptionsColumn.AllowFocus = False
        Me.INDColDocument_WarehouseId.Visible = True
        Me.INDColDocument_WarehouseId.VisibleIndex = 4
        Me.INDColDocument_WarehouseId.Width = 87
        '
        'INDColDocument_Status
        '
        Me.INDColDocument_Status.Caption = "Estado"
        Me.INDColDocument_Status.FieldName = "StatusName"
        Me.INDColDocument_Status.MinWidth = 23
        Me.INDColDocument_Status.Name = "INDColDocument_Status"
        Me.INDColDocument_Status.OptionsColumn.AllowEdit = False
        Me.INDColDocument_Status.OptionsColumn.AllowFocus = False
        Me.INDColDocument_Status.Visible = True
        Me.INDColDocument_Status.VisibleIndex = 5
        Me.INDColDocument_Status.Width = 87
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Location = New System.Drawing.Point(24, 461)
        Me.INDsleCurrency.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDsleCurrency.MaximumSize = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.MinimumSize = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.Name = "INDsleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.INDsleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleCurrency.Properties.DisplayMember = "CurrencyName"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.SearchLookUpEdit1View1
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, True)
        Me.INDsleCurrency.Size = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.StyleController = Me.INDLcBase
        Me.INDsleCurrency.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
        '
        'SearchLookUpEdit1View1
        '
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn178, Me.GridColumn761, Me.GridColumn771})
        Me.SearchLookUpEdit1View1.DetailHeight = 431
        Me.SearchLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View1.Name = "SearchLookUpEdit1View1"
        Me.SearchLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        '
        'GridColumn178
        '
        Me.GridColumn178.Caption = "Código"
        Me.GridColumn178.FieldName = "Codigo"
        Me.GridColumn178.MinWidth = 23
        Me.GridColumn178.Name = "GridColumn178"
        Me.GridColumn178.Visible = True
        Me.GridColumn178.VisibleIndex = 0
        Me.GridColumn178.Width = 87
        '
        'GridColumn761
        '
        Me.GridColumn761.Caption = "Nombre"
        Me.GridColumn761.FieldName = "CurrencyName"
        Me.GridColumn761.MinWidth = 23
        Me.GridColumn761.Name = "GridColumn761"
        Me.GridColumn761.Visible = True
        Me.GridColumn761.VisibleIndex = 1
        Me.GridColumn761.Width = 87
        '
        'GridColumn771
        '
        Me.GridColumn771.Caption = "Abreviación"
        Me.GridColumn771.FieldName = "Abbreviation"
        Me.GridColumn771.MinWidth = 23
        Me.GridColumn771.Name = "GridColumn771"
        Me.GridColumn771.Visible = True
        Me.GridColumn771.VisibleIndex = 2
        Me.GridColumn771.Width = 87
        '
        'INDSbGenerateExcell
        '
        Me.INDSbGenerateExcell.BackgroundImage = Global.Presentation.Inventory.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbGenerateExcell.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbGenerateExcell.Location = New System.Drawing.Point(1016, 202)
        Me.INDSbGenerateExcell.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSbGenerateExcell.MaximumSize = New System.Drawing.Size(47, 39)
        Me.INDSbGenerateExcell.MinimumSize = New System.Drawing.Size(47, 39)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateExcell, False)
        Me.INDSbGenerateExcell.Name = "INDSbGenerateExcell"
        Me.INDSbGenerateExcell.Size = New System.Drawing.Size(47, 39)
        Me.INDSbGenerateExcell.StyleController = Me.INDLcBase
        Me.INDSbGenerateExcell.TabIndex = 38
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "INDLcgBase"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterRequired, Me.INDLcgFilterOptional})
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(1339, 879)
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilterRequired, False)
        Me.INDLcgFilterRequired.CustomizationFormText = "Filtros"
        Me.INDLcgFilterRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateEnd, Me.INDLciDateStart, Me.INDLciGroupBy, Me.INDLciStatus, Me.INDLciTypeReport, Me.INDlyCurrency})
        Me.INDLcgFilterRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterRequired.Name = "INDLcgFilterRequired"
        Me.INDLcgFilterRequired.Size = New System.Drawing.Size(555, 859)
        Me.INDLcgFilterRequired.Text = "Filtros"
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDeDateEnd
        Me.INDLciDateEnd.CustomizationFormText = "Fecha Final :"
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 69)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciDateEnd.Size = New System.Drawing.Size(531, 74)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final :"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDeDateStart
        Me.INDLciDateStart.CustomizationFormText = "Fecha Inicial :"
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(420, 69)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(420, 69)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(531, 69)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial :"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciGroupBy
        '
        Me.INDLciGroupBy.Control = Me.INDGleGroupBy
        Me.INDLciGroupBy.CustomizationFormText = "Agrupamiento :"
        Me.INDLciGroupBy.Location = New System.Drawing.Point(0, 143)
        Me.INDLciGroupBy.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciGroupBy.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciGroupBy.Name = "INDLciGroupBy"
        Me.INDLciGroupBy.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciGroupBy.Size = New System.Drawing.Size(531, 74)
        Me.INDLciGroupBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGroupBy.Text = "Agrupamiento :"
        Me.INDLciGroupBy.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciGroupBy.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciStatus
        '
        Me.INDLciStatus.Control = Me.INDGleStatus
        Me.INDLciStatus.CustomizationFormText = "Estado :"
        Me.INDLciStatus.Location = New System.Drawing.Point(0, 217)
        Me.INDLciStatus.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciStatus.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciStatus.Name = "INDLciStatus"
        Me.INDLciStatus.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciStatus.Size = New System.Drawing.Size(531, 74)
        Me.INDLciStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStatus.Text = "Estado :"
        Me.INDLciStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStatus.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGleTypeReport
        Me.INDLciTypeReport.CustomizationFormText = "Tipo Reporte :"
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 291)
        Me.INDLciTypeReport.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciTypeReport.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciTypeReport.Size = New System.Drawing.Size(531, 74)
        Me.INDLciTypeReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeReport.Text = "Tipo Reporte :"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDlyCurrency
        '
        Me.INDlyCurrency.Control = Me.INDsleCurrency
        Me.INDlyCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyCurrency.CustomizationFormText = "Moneda de reporte:"
        Me.INDlyCurrency.Location = New System.Drawing.Point(0, 365)
        Me.INDlyCurrency.MaxSize = New System.Drawing.Size(531, 97)
        Me.INDlyCurrency.MinSize = New System.Drawing.Size(531, 91)
        Me.INDlyCurrency.Name = "INDlyCurrency"
        Me.INDlyCurrency.Size = New System.Drawing.Size(531, 435)
        Me.INDlyCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyCurrency.Text = "Moneda de reporte:"
        Me.INDlyCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyCurrency.TextSize = New System.Drawing.Size(183, 32)
        Me.INDlyCurrency.TextToControlDistance = 5
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilterOptional, False)
        Me.INDLcgFilterOptional.CustomizationFormText = "Rangos"
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcSupplier, Me.INDLciDocument, Me.INDLciGenerateReport, Me.LayoutControlItem2})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(555, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(764, 859)
        Me.INDLcgFilterOptional.Text = "Rangos"
        '
        'INDLcSupplier
        '
        Me.INDLcSupplier.Control = Me.INDSleSupplier
        Me.INDLcSupplier.CustomizationFormText = "Proveedor"
        Me.INDLcSupplier.Location = New System.Drawing.Point(0, 0)
        Me.INDLcSupplier.MaxSize = New System.Drawing.Size(439, 69)
        Me.INDLcSupplier.MinSize = New System.Drawing.Size(439, 69)
        Me.INDLcSupplier.Name = "INDLcSupplier"
        Me.INDLcSupplier.Size = New System.Drawing.Size(740, 69)
        Me.INDLcSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcSupplier.Text = "Proveedor"
        Me.INDLcSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcSupplier.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciDocument
        '
        Me.INDLciDocument.Control = Me.INDSleDocument
        Me.INDLciDocument.CustomizationFormText = "Documento"
        Me.INDLciDocument.Location = New System.Drawing.Point(0, 69)
        Me.INDLciDocument.MaxSize = New System.Drawing.Size(439, 69)
        Me.INDLciDocument.MinSize = New System.Drawing.Size(439, 69)
        Me.INDLciDocument.Name = "INDLciDocument"
        Me.INDLciDocument.Size = New System.Drawing.Size(740, 69)
        Me.INDLciDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocument.Text = "Documento"
        Me.INDLciDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocument.TextSize = New System.Drawing.Size(127, 23)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.CustomizationFormText = "LayoutControlItem10"
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 138)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(439, 662)
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbGenerateExcell
        Me.LayoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(439, 138)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 2, 7, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(301, 662)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvDocumentViewer
        Me.DocumentViewerBarManager1.Form = Me.INDDvDocumentViewer
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
        Me.PrintPreviewStaticItem1.Caption = "Nothing"
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
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1453, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 848)
        Me.barDockControlBottom.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1453, 27)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 818)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1453, 30)
        Me.barDockControlRight.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 818)
        '
        'INDDvDocumentViewer
        '
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlLeft)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlRight)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlBottom)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlTop)
        Me.INDDvDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvDocumentViewer, True)
        Me.INDDvDocumentViewer.IsMetric = True
        Me.INDDvDocumentViewer.Location = New System.Drawing.Point(84, 2)
        Me.INDDvDocumentViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDDvDocumentViewer.Name = "INDDvDocumentViewer"
        Me.INDDvDocumentViewer.Size = New System.Drawing.Size(1453, 875)
        Me.INDDvDocumentViewer.TabIndex = 1
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
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDPcDocumentViewer
        '
        Me.INDPcDocumentViewer.Controls.Add(Me.INDDvDocumentViewer)
        Me.INDPcDocumentViewer.Controls.Add(Me.INDCnBack)
        Me.INDPcDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcDocumentViewer.Location = New System.Drawing.Point(2, 9)
        Me.INDPcDocumentViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPcDocumentViewer.Name = "INDPcDocumentViewer"
        Me.INDPcDocumentViewer.Size = New System.Drawing.Size(1539, 879)
        Me.INDPcDocumentViewer.TabIndex = 2
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(82, 875)
        Me.INDCnBack.TabIndex = 0
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'INDGcExportExcell
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExportExcell, Nothing)
        Me.INDGcExportExcell.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExportExcell, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExportExcell, False)
        Me.INDGcExportExcell.Location = New System.Drawing.Point(463, 297)
        Me.INDGcExportExcell.MainView = Me.GridView1
        Me.INDGcExportExcell.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcExportExcell.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGcExportExcell.Name = "INDGcExportExcell"
        Me.INDGcExportExcell.Size = New System.Drawing.Size(616, 302)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExportExcell, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExportExcell.TabIndex = 41
        Me.INDGcExportExcell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        Me.INDGcExportExcell.Visible = False
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn34, Me.GridColumn35, Me.GridColumn36, Me.GridColumn38, Me.GridColumn39, Me.GridColumn40, Me.GridColumn41, Me.GridColumn83, Me.GridColumn84, Me.GridColumn85, Me.GridColumn86})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.INDGcExportExcell
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "GridColumn34"
        Me.GridColumn34.MinWidth = 150
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Visible = True
        Me.GridColumn34.VisibleIndex = 0
        Me.GridColumn34.Width = 150
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "GridColumn35"
        Me.GridColumn35.MinWidth = 150
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 1
        Me.GridColumn35.Width = 150
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "GridColumn36"
        Me.GridColumn36.MinWidth = 150
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 2
        Me.GridColumn36.Width = 150
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "GridColumn38"
        Me.GridColumn38.MinWidth = 150
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 3
        Me.GridColumn38.Width = 150
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "GridColumn39"
        Me.GridColumn39.MinWidth = 150
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 4
        Me.GridColumn39.Width = 150
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "GridColumn40"
        Me.GridColumn40.MinWidth = 150
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 5
        Me.GridColumn40.Width = 150
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "GridColumn41"
        Me.GridColumn41.MinWidth = 150
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 6
        Me.GridColumn41.Width = 150
        '
        'GridColumn83
        '
        Me.GridColumn83.Caption = "GridColumn83"
        Me.GridColumn83.MinWidth = 150
        Me.GridColumn83.Name = "GridColumn83"
        Me.GridColumn83.Visible = True
        Me.GridColumn83.VisibleIndex = 7
        Me.GridColumn83.Width = 150
        '
        'GridColumn84
        '
        Me.GridColumn84.Caption = "GridColumn84"
        Me.GridColumn84.MinWidth = 150
        Me.GridColumn84.Name = "GridColumn84"
        Me.GridColumn84.Visible = True
        Me.GridColumn84.VisibleIndex = 8
        Me.GridColumn84.Width = 150
        '
        'GridColumn85
        '
        Me.GridColumn85.Caption = "GridColumn85"
        Me.GridColumn85.MinWidth = 150
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.Visible = True
        Me.GridColumn85.VisibleIndex = 9
        Me.GridColumn85.Width = 150
        '
        'GridColumn86
        '
        Me.GridColumn86.Caption = "GridColumn86"
        Me.GridColumn86.MinWidth = 100
        Me.GridColumn86.Name = "GridColumn86"
        Me.GridColumn86.Visible = True
        Me.GridColumn86.VisibleIndex = 10
        Me.GridColumn86.Width = 100
        '
        'FrmReportEntranceVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1543, 897)
        Me.Controls.Add(Me.INDGcExportExcell)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmReportEntranceVoucher"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "1408"
        Me.Text = "Reporte De Comprobantes De Entrada"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDGcExportExcell, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeSupplierStatusEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeEntranceVoucherStatusEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleGroupBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGroupBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvDocumentViewer.ResumeLayout(False)
        Me.INDDvDocumentViewer.PerformLayout()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDocumentViewer.ResumeLayout(False)
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGleGroupBy As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDDeDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDDeDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLcgFilterRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciGroupBy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCncNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDGleTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleStatus As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGrcTypeReport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcGroupBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDPcDocumentViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDDvDocumentViewer As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDCnBack As Presentation.Controls.CtrNavigation
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
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDCbeEntranceVoucherStatusEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeSupplierStatusEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents INDSleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColSupplier_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_ThirdPartyDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_ThirdPartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_City As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleDocument As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvDocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColDocument_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocument_ThirdPartyNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocument_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocument_WarehouseId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocument_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColDocument_ThirdPartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn761 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn771 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbGenerateExcell As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcExportExcell As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
End Class
