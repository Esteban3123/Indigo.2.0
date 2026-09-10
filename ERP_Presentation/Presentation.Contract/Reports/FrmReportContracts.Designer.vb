Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportContracts
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportContracts))
        Me.INDCnc = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleStatus = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxeNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleContractEntity = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleHealAdministrator = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleContracts = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDContractCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDContractName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDContractObject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDContractStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciContracts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHealtAdministrator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciContractEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.INDPcDocumentViewer = New DevExpress.XtraEditors.PanelControl()
        Me.INDDvDocumentViewer = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.DocumentViewerBarManager1 = New DevExpress.XtraPrinting.Preview.DocumentViewerBarManager()
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
        Me.PrintPreviewBarItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.ZoomBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomBarEditItem()
        Me.PrintPreviewRepositoryItemComboBox1 = New DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox()
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
        Me.PrintPreviewBarItem27 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
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
        Me.PrintPreviewSubItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem29 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
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
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxeNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleContractEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleHealAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleContracts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciContracts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHealtAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciContractEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDocumentViewer.SuspendLayout()
        Me.INDDvDocumentViewer.SuspendLayout()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnc)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcDocumentViewer)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 24)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 705)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.Dock = System.Windows.Forms.DockStyle.None
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'INDCnc
        '
        Me.INDCnc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnc.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnc.LayoutControl = Me.INDLcBase
        Me.INDCnc.Location = New System.Drawing.Point(2, 7)
        Me.INDCnc.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnc.Name = "INDCnc"
        Me.INDCnc.Size = New System.Drawing.Size(200, 696)
        Me.INDCnc.TabIndex = 0
        Me.INDCnc.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDGleStatus)
        Me.INDLcBase.Controls.Add(Me.INDTxeNumber)
        Me.INDLcBase.Controls.Add(Me.INDSleContractEntity)
        Me.INDLcBase.Controls.Add(Me.INDSleHealAdministrator)
        Me.INDLcBase.Controls.Add(Me.INDSleContracts)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 696)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(24, 347)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(356, 28)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 9
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDGleStatus
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleStatus, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleStatus, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleStatus, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleStatus, False)
        Me.INDGleStatus.Location = New System.Drawing.Point(24, 307)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleStatus.Name = "INDGleStatus"
        Me.INDGleStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDGleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleStatus.Properties.DisplayMember = "Item2"
        Me.INDGleStatus.Properties.ImmediatePopup = True
        Me.INDGleStatus.Properties.NullText = ""
        Me.INDGleStatus.Properties.ValueMember = "Item1"
        Me.INDGleStatus.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleStatus.Size = New System.Drawing.Size(356, 28)
        Me.INDGleStatus.StyleController = Me.INDLcBase
        Me.INDGleStatus.TabIndex = 8
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleStatus, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleStatus, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDStatus})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDStatus
        '
        Me.INDStatus.Caption = "Estado"
        Me.INDStatus.FieldName = "Item2"
        Me.INDStatus.Name = "INDStatus"
        Me.INDStatus.Visible = True
        Me.INDStatus.VisibleIndex = 0
        '
        'INDTxeNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeNumber, False)
        Me.INDTxeNumber.Location = New System.Drawing.Point(24, 251)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeNumber.Name = "INDTxeNumber"
        Me.INDTxeNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxeNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxeNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxeNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxeNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxeNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeNumber.Size = New System.Drawing.Size(356, 28)
        Me.INDTxeNumber.StyleController = Me.INDLcBase
        Me.INDTxeNumber.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeNumber, 0)
        '
        'INDSleContractEntity
        '
        Me.INDSleContractEntity.AllowQueryOne = False
        Me.INDSleContractEntity.Datasource = Nothing
        Me.INDSleContractEntity.DisplayMember = "{Code} - {Name}"
        Me.INDSleContractEntity.DisplayNullText = ""
        Me.INDSleContractEntity.EditValue = Nothing
        Me.INDSleContractEntity.EnterMoveNextControl = False
        Me.INDSleContractEntity.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleContractEntity.IdOpenForm = 0
        Me.INDSleContractEntity.Location = New System.Drawing.Point(24, 195)
        Me.INDSleContractEntity.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleContractEntity.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleContractEntity.Name = "INDSleContractEntity"
        Me.INDSleContractEntity.PopUpFormSize = New System.Drawing.Size(600, 300)
        Me.INDSleContractEntity.Size = New System.Drawing.Size(356, 28)
        Me.INDSleContractEntity.TabIndex = 6
        Me.INDSleContractEntity.ValueMember = "Code"
        Me.INDSleContractEntity.View = Me.SearchLookUpEditExView3
        '
        'SearchLookUpEditExView3
        '
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView3.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn17})
        Me.SearchLookUpEditExView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEditExView3.Name = "SearchLookUpEditExView3"
        Me.SearchLookUpEditExView3.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView3.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEditExView3.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView3, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "Code"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        Me.GridColumn16.Width = 366
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Nombre"
        Me.GridColumn17.FieldName = "Name"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 1266
        '
        'INDSleHealAdministrator
        '
        Me.INDSleHealAdministrator.AllowQueryOne = False
        Me.INDSleHealAdministrator.Datasource = Nothing
        Me.INDSleHealAdministrator.DisplayMember = "{Code} - {Name}"
        Me.INDSleHealAdministrator.DisplayNullText = ""
        Me.INDSleHealAdministrator.EditValue = Nothing
        Me.INDSleHealAdministrator.EnterMoveNextControl = False
        Me.INDSleHealAdministrator.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleHealAdministrator.IdOpenForm = 0
        Me.INDSleHealAdministrator.Location = New System.Drawing.Point(24, 139)
        Me.INDSleHealAdministrator.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleHealAdministrator.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleHealAdministrator.Name = "INDSleHealAdministrator"
        Me.INDSleHealAdministrator.PopUpFormSize = New System.Drawing.Size(900, 300)
        Me.INDSleHealAdministrator.Size = New System.Drawing.Size(356, 28)
        Me.INDSleHealAdministrator.TabIndex = 5
        Me.INDSleHealAdministrator.ValueMember = "Code"
        Me.INDSleHealAdministrator.View = Me.SearchLookUpEditExView2
        '
        'SearchLookUpEditExView2
        '
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView2.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8})
        Me.SearchLookUpEditExView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEditExView2.Name = "SearchLookUpEditExView2"
        Me.SearchLookUpEditExView2.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView2.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView2, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 247
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 489
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo Entidad"
        Me.GridColumn7.FieldName = "EntityType"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 287
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tercero"
        Me.GridColumn8.FieldName = "ThirdPartyId.NitName"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 609
        '
        'INDSleContracts
        '
        Me.INDSleContracts.AllowQueryOne = False
        Me.INDSleContracts.Datasource = Nothing
        Me.INDSleContracts.DisplayMember = "{Code} - {ContractName}"
        Me.INDSleContracts.DisplayNullText = ""
        Me.INDSleContracts.EditValue = Nothing
        Me.INDSleContracts.EnterMoveNextControl = False
        Me.INDSleContracts.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleContracts.IdOpenForm = 0
        Me.INDSleContracts.Location = New System.Drawing.Point(24, 83)
        Me.INDSleContracts.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleContracts.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleContracts.Name = "INDSleContracts"
        Me.INDSleContracts.PopUpFormSize = New System.Drawing.Size(900, 300)
        Me.INDSleContracts.Size = New System.Drawing.Size(356, 28)
        Me.INDSleContracts.TabIndex = 4
        Me.INDSleContracts.ValueMember = "Code"
        Me.INDSleContracts.View = Me.SearchLookUpEditExView1
        '
        'SearchLookUpEditExView1
        '
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDContractCode, Me.INDEntity, Me.INDContractName, Me.INDContractObject, Me.INDContractStatus})
        Me.SearchLookUpEditExView1.Name = "SearchLookUpEditExView1"
        Me.SearchLookUpEditExView1.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView1.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView1, False)
        '
        'INDContractCode
        '
        Me.INDContractCode.Caption = "Código"
        Me.INDContractCode.FieldName = "Code"
        Me.INDContractCode.Name = "INDContractCode"
        Me.INDContractCode.OptionsColumn.AllowEdit = False
        Me.INDContractCode.Visible = True
        Me.INDContractCode.VisibleIndex = 0
        '
        'INDEntity
        '
        Me.INDEntity.Caption = "Entidad"
        Me.INDEntity.FieldName = "HealthAdministratorId.CodeName"
        Me.INDEntity.Name = "INDEntity"
        Me.INDEntity.OptionsColumn.AllowEdit = False
        Me.INDEntity.Visible = True
        Me.INDEntity.VisibleIndex = 1
        '
        'INDContractName
        '
        Me.INDContractName.Caption = "Nombre"
        Me.INDContractName.FieldName = "ContractName"
        Me.INDContractName.Name = "INDContractName"
        Me.INDContractName.OptionsColumn.AllowEdit = False
        Me.INDContractName.Visible = True
        Me.INDContractName.VisibleIndex = 2
        '
        'INDContractObject
        '
        Me.INDContractObject.Caption = "Objeto"
        Me.INDContractObject.FieldName = "ContractObject"
        Me.INDContractObject.Name = "INDContractObject"
        Me.INDContractObject.OptionsColumn.AllowEdit = False
        Me.INDContractObject.Visible = True
        Me.INDContractObject.VisibleIndex = 3
        '
        'INDContractStatus
        '
        Me.INDContractStatus.Caption = "Estado"
        Me.INDContractStatus.FieldName = "StatusName"
        Me.INDContractStatus.Name = "INDContractStatus"
        Me.INDContractStatus.OptionsColumn.AllowEdit = False
        Me.INDContractStatus.Visible = True
        Me.INDContractStatus.VisibleIndex = 4
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterOptional})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 696)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgFilterOptional
        '
        Me.INDLcgFilterOptional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterOptional.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciContracts, Me.INDLciHealtAdministrator, Me.INDLciContractEntity, Me.INDLciNumber, Me.INDLciStatus, Me.INDLciGenerateReport})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(784, 676)
        Me.INDLcgFilterOptional.Text = "Filtros"
        '
        'INDLciContracts
        '
        Me.INDLciContracts.Control = Me.INDSleContracts
        Me.INDLciContracts.Location = New System.Drawing.Point(0, 0)
        Me.INDLciContracts.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciContracts.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciContracts.Name = "INDLciContracts"
        Me.INDLciContracts.Size = New System.Drawing.Size(760, 56)
        Me.INDLciContracts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciContracts.Text = "Código"
        Me.INDLciContracts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciContracts.TextSize = New System.Drawing.Size(164, 21)
        '
        'INDLciHealtAdministrator
        '
        Me.INDLciHealtAdministrator.Control = Me.INDSleHealAdministrator
        Me.INDLciHealtAdministrator.Location = New System.Drawing.Point(0, 56)
        Me.INDLciHealtAdministrator.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciHealtAdministrator.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciHealtAdministrator.Name = "INDLciHealtAdministrator"
        Me.INDLciHealtAdministrator.Size = New System.Drawing.Size(760, 56)
        Me.INDLciHealtAdministrator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHealtAdministrator.Text = "Administradora de Salud"
        Me.INDLciHealtAdministrator.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciHealtAdministrator.TextSize = New System.Drawing.Size(164, 21)
        '
        'INDLciContractEntity
        '
        Me.INDLciContractEntity.Control = Me.INDSleContractEntity
        Me.INDLciContractEntity.Location = New System.Drawing.Point(0, 112)
        Me.INDLciContractEntity.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciContractEntity.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciContractEntity.Name = "INDLciContractEntity"
        Me.INDLciContractEntity.Size = New System.Drawing.Size(760, 56)
        Me.INDLciContractEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciContractEntity.Text = "Entidad Contrato"
        Me.INDLciContractEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciContractEntity.TextSize = New System.Drawing.Size(164, 21)
        '
        'INDLciNumber
        '
        Me.INDLciNumber.Control = Me.INDTxeNumber
        Me.INDLciNumber.Location = New System.Drawing.Point(0, 168)
        Me.INDLciNumber.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciNumber.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciNumber.Name = "INDLciNumber"
        Me.INDLciNumber.Size = New System.Drawing.Size(760, 56)
        Me.INDLciNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNumber.Text = "Número"
        Me.INDLciNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNumber.TextSize = New System.Drawing.Size(164, 21)
        '
        'INDLciStatus
        '
        Me.INDLciStatus.Control = Me.INDGleStatus
        Me.INDLciStatus.Location = New System.Drawing.Point(0, 224)
        Me.INDLciStatus.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatus.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatus.Name = "INDLciStatus"
        Me.INDLciStatus.Size = New System.Drawing.Size(760, 56)
        Me.INDLciStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStatus.Text = "Estado"
        Me.INDLciStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStatus.TextSize = New System.Drawing.Size(164, 21)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 280)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(360, 40)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(360, 40)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(760, 337)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'INDPcDocumentViewer
        '
        Me.INDPcDocumentViewer.Controls.Add(Me.INDDvDocumentViewer)
        Me.INDPcDocumentViewer.Controls.Add(Me.INDCnBack)
        Me.INDPcDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcDocumentViewer.Location = New System.Drawing.Point(2, 7)
        Me.INDPcDocumentViewer.Name = "INDPcDocumentViewer"
        Me.INDPcDocumentViewer.Size = New System.Drawing.Size(1004, 696)
        Me.INDPcDocumentViewer.TabIndex = 2
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
        Me.INDDvDocumentViewer.Location = New System.Drawing.Point(72, 2)
        Me.INDDvDocumentViewer.Name = "INDDvDocumentViewer"
        Me.INDDvDocumentViewer.Size = New System.Drawing.Size(930, 692)
        Me.INDDvDocumentViewer.TabIndex = 1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 635)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(930, 31)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 635)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 666)
        Me.barDockControlBottom.Size = New System.Drawing.Size(930, 26)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(930, 31)
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(70, 692)
        Me.INDCnBack.TabIndex = 0
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
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.PrintPreviewBarItem16, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewBarItem27, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem28, Me.PrintPreviewBarItem29, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 58
        Me.DocumentViewerBarManager1.PreviewBar = Me.PreviewBar1
        Me.DocumentViewerBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemZoomTrackBar1, Me.PrintPreviewRepositoryItemComboBox1})
        Me.DocumentViewerBarManager1.StatusBar = Me.PreviewBar2
        Me.DocumentViewerBarManager1.TransparentEditors = True
        '
        'PreviewBar1
        '
        Me.PreviewBar1.BarName = "Toolbar"
        Me.PreviewBar1.DockCol = 0
        Me.PreviewBar1.DockRow = 0
        Me.PreviewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.PreviewBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem6, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem7, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem13), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem14, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem15), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem16, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem17), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem18, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem19), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem21), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
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
        Me.PrintPreviewBarItem2.ImageIndex = 19
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
        Me.PrintPreviewBarItem3.ImageIndex = 22
        Me.PrintPreviewBarItem3.Name = "PrintPreviewBarItem3"
        '
        'PrintPreviewBarItem4
        '
        Me.PrintPreviewBarItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem4.Caption = "Thumbnails"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Thumbnails
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Thumbnails"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageIndex = 23
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem5.Caption = "Search"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Search"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageIndex = 20
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Customize"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Customize"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageIndex = 14
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Open"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Open a document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "Save"
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Save the document"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageIndex = 25
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "&Print..."
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageIndex = 0
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "P&rint"
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Quick Print"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageIndex = 1
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Page Setup"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageIndex = 2
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.Caption = "Header And Footer"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Header And Footer"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageIndex = 15
        Me.PrintPreviewBarItem12.Name = "PrintPreviewBarItem12"
        '
        'PrintPreviewBarItem13
        '
        Me.PrintPreviewBarItem13.ActAsDropDown = True
        Me.PrintPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem13.Caption = "Scale"
        Me.PrintPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale
        Me.PrintPreviewBarItem13.Enabled = False
        Me.PrintPreviewBarItem13.Hint = "Scale"
        Me.PrintPreviewBarItem13.Id = 18
        Me.PrintPreviewBarItem13.ImageIndex = 26
        Me.PrintPreviewBarItem13.Name = "PrintPreviewBarItem13"
        '
        'PrintPreviewBarItem14
        '
        Me.PrintPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem14.Caption = "Hand Tool"
        Me.PrintPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool
        Me.PrintPreviewBarItem14.Enabled = False
        Me.PrintPreviewBarItem14.Hint = "Hand Tool"
        Me.PrintPreviewBarItem14.Id = 19
        Me.PrintPreviewBarItem14.ImageIndex = 16
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem15.Caption = "Magnifier"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Magnifier"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageIndex = 3
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom Out"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom Out"
        Me.PrintPreviewBarItem16.Id = 21
        Me.PrintPreviewBarItem16.ImageIndex = 5
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 22
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        Me.ZoomBarEditItem1.Width = 70
        '
        'PrintPreviewRepositoryItemComboBox1
        '
        Me.PrintPreviewRepositoryItemComboBox1.AutoComplete = False
        Me.PrintPreviewRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PrintPreviewRepositoryItemComboBox1.DropDownRows = 11
        Me.PrintPreviewRepositoryItemComboBox1.Name = "PrintPreviewRepositoryItemComboBox1"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "Zoom In"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "Zoom In"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageIndex = 4
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "First Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "First Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageIndex = 7
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Previous Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Previous Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageIndex = 8
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Next Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Next Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageIndex = 9
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.Caption = "Last Page"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Last Page"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageIndex = 10
        Me.PrintPreviewBarItem21.Name = "PrintPreviewBarItem21"
        '
        'PrintPreviewBarItem22
        '
        Me.PrintPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem22.Caption = "Multiple Pages"
        Me.PrintPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages
        Me.PrintPreviewBarItem22.Enabled = False
        Me.PrintPreviewBarItem22.Hint = "Multiple Pages"
        Me.PrintPreviewBarItem22.Id = 28
        Me.PrintPreviewBarItem22.ImageIndex = 11
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem23.Caption = "&Color..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Background"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageIndex = 12
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.Caption = "&Watermark..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Watermark"
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageIndex = 21
        Me.PrintPreviewBarItem24.Name = "PrintPreviewBarItem24"
        '
        'PrintPreviewBarItem25
        '
        Me.PrintPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem25.Caption = "Export Document..."
        Me.PrintPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile
        Me.PrintPreviewBarItem25.Enabled = False
        Me.PrintPreviewBarItem25.Hint = "Export Document..."
        Me.PrintPreviewBarItem25.Id = 31
        Me.PrintPreviewBarItem25.ImageIndex = 18
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem26.Caption = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageIndex = 17
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.Caption = "E&xit"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.Hint = "Close Preview"
        Me.PrintPreviewBarItem27.Id = 33
        Me.PrintPreviewBarItem27.ImageIndex = 13
        Me.PrintPreviewBarItem27.Name = "PrintPreviewBarItem27"
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
        Me.PrintPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        Me.PrintPreviewStaticItem1.Type = "PageOfPages"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarStaticItem1.Id = 1
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        Me.BarStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'ProgressBarEditItem1
        '
        Me.ProgressBarEditItem1.Edit = Me.RepositoryItemProgressBar1
        Me.ProgressBarEditItem1.EditHeight = 12
        Me.ProgressBarEditItem1.Id = 2
        Me.ProgressBarEditItem1.Name = "ProgressBarEditItem1"
        Me.ProgressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.ProgressBarEditItem1.Width = 150
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
        Me.PrintPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far
        Me.PrintPreviewStaticItem2.Type = "ZoomFactor"
        '
        'ZoomTrackBarEditItem1
        '
        Me.ZoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.ZoomTrackBarEditItem1.Edit = Me.RepositoryItemZoomTrackBar1
        Me.ZoomTrackBarEditItem1.EditValue = 90
        Me.ZoomTrackBarEditItem1.Enabled = False
        Me.ZoomTrackBarEditItem1.Id = 6
        Me.ZoomTrackBarEditItem1.Name = "ZoomTrackBarEditItem1"
        Me.ZoomTrackBarEditItem1.Range = New Integer() {10, 500}
        Me.ZoomTrackBarEditItem1.Width = 140
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemZoomTrackBar1.AllowFocused = False
        Me.RepositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RepositoryItemZoomTrackBar1.Maximum = 180
        Me.RepositoryItemZoomTrackBar1.Middle = 5
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        Me.RepositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight
        '
        'PrintPreviewSubItem1
        '
        Me.PrintPreviewSubItem1.Caption = "&File"
        Me.PrintPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File
        Me.PrintPreviewSubItem1.Id = 34
        Me.PrintPreviewSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
        Me.PrintPreviewSubItem1.Name = "PrintPreviewSubItem1"
        '
        'PrintPreviewSubItem2
        '
        Me.PrintPreviewSubItem2.Caption = "&View"
        Me.PrintPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View
        Me.PrintPreviewSubItem2.Id = 35
        Me.PrintPreviewSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarToolbarsListItem1, True)})
        Me.PrintPreviewSubItem2.Name = "PrintPreviewSubItem2"
        '
        'PrintPreviewSubItem4
        '
        Me.PrintPreviewSubItem4.Caption = "&Page Layout"
        Me.PrintPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout
        Me.PrintPreviewSubItem4.Id = 37
        Me.PrintPreviewSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem28), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem29)})
        Me.PrintPreviewSubItem4.Name = "PrintPreviewSubItem4"
        '
        'PrintPreviewBarItem28
        '
        Me.PrintPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem28.Caption = "&Facing"
        Me.PrintPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing
        Me.PrintPreviewBarItem28.Enabled = False
        Me.PrintPreviewBarItem28.GroupIndex = 100
        Me.PrintPreviewBarItem28.Id = 38
        Me.PrintPreviewBarItem28.Name = "PrintPreviewBarItem28"
        '
        'PrintPreviewBarItem29
        '
        Me.PrintPreviewBarItem29.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem29.Caption = "&Continuous"
        Me.PrintPreviewBarItem29.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous
        Me.PrintPreviewBarItem29.Enabled = False
        Me.PrintPreviewBarItem29.GroupIndex = 100
        Me.PrintPreviewBarItem29.Id = 39
        Me.PrintPreviewBarItem29.Name = "PrintPreviewBarItem29"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "Bars"
        Me.BarToolbarsListItem1.Id = 40
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'PrintPreviewSubItem3
        '
        Me.PrintPreviewSubItem3.Caption = "&Background"
        Me.PrintPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background
        Me.PrintPreviewSubItem3.Id = 36
        Me.PrintPreviewSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24)})
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
        Me.PrintPreviewBarCheckItem1.Id = 41
        Me.PrintPreviewBarCheckItem1.Name = "PrintPreviewBarCheckItem1"
        '
        'PrintPreviewBarCheckItem2
        '
        Me.PrintPreviewBarCheckItem2.Caption = "HTML File"
        Me.PrintPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm
        Me.PrintPreviewBarCheckItem2.Enabled = False
        Me.PrintPreviewBarCheckItem2.GroupIndex = 2
        Me.PrintPreviewBarCheckItem2.Hint = "HTML File"
        Me.PrintPreviewBarCheckItem2.Id = 42
        Me.PrintPreviewBarCheckItem2.Name = "PrintPreviewBarCheckItem2"
        '
        'PrintPreviewBarCheckItem3
        '
        Me.PrintPreviewBarCheckItem3.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht
        Me.PrintPreviewBarCheckItem3.Enabled = False
        Me.PrintPreviewBarCheckItem3.GroupIndex = 2
        Me.PrintPreviewBarCheckItem3.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem3.Id = 43
        Me.PrintPreviewBarCheckItem3.Name = "PrintPreviewBarCheckItem3"
        '
        'PrintPreviewBarCheckItem4
        '
        Me.PrintPreviewBarCheckItem4.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf
        Me.PrintPreviewBarCheckItem4.Enabled = False
        Me.PrintPreviewBarCheckItem4.GroupIndex = 2
        Me.PrintPreviewBarCheckItem4.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem4.Id = 44
        Me.PrintPreviewBarCheckItem4.Name = "PrintPreviewBarCheckItem4"
        '
        'PrintPreviewBarCheckItem5
        '
        Me.PrintPreviewBarCheckItem5.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls
        Me.PrintPreviewBarCheckItem5.Enabled = False
        Me.PrintPreviewBarCheckItem5.GroupIndex = 2
        Me.PrintPreviewBarCheckItem5.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem5.Id = 45
        Me.PrintPreviewBarCheckItem5.Name = "PrintPreviewBarCheckItem5"
        '
        'PrintPreviewBarCheckItem6
        '
        Me.PrintPreviewBarCheckItem6.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx
        Me.PrintPreviewBarCheckItem6.Enabled = False
        Me.PrintPreviewBarCheckItem6.GroupIndex = 2
        Me.PrintPreviewBarCheckItem6.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Id = 46
        Me.PrintPreviewBarCheckItem6.Name = "PrintPreviewBarCheckItem6"
        '
        'PrintPreviewBarCheckItem7
        '
        Me.PrintPreviewBarCheckItem7.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv
        Me.PrintPreviewBarCheckItem7.Enabled = False
        Me.PrintPreviewBarCheckItem7.GroupIndex = 2
        Me.PrintPreviewBarCheckItem7.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem7.Id = 47
        Me.PrintPreviewBarCheckItem7.Name = "PrintPreviewBarCheckItem7"
        '
        'PrintPreviewBarCheckItem8
        '
        Me.PrintPreviewBarCheckItem8.Caption = "Text File"
        Me.PrintPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt
        Me.PrintPreviewBarCheckItem8.Enabled = False
        Me.PrintPreviewBarCheckItem8.GroupIndex = 2
        Me.PrintPreviewBarCheckItem8.Hint = "Text File"
        Me.PrintPreviewBarCheckItem8.Id = 48
        Me.PrintPreviewBarCheckItem8.Name = "PrintPreviewBarCheckItem8"
        '
        'PrintPreviewBarCheckItem9
        '
        Me.PrintPreviewBarCheckItem9.Caption = "Image File"
        Me.PrintPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic
        Me.PrintPreviewBarCheckItem9.Enabled = False
        Me.PrintPreviewBarCheckItem9.GroupIndex = 2
        Me.PrintPreviewBarCheckItem9.Hint = "Image File"
        Me.PrintPreviewBarCheckItem9.Id = 49
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
        Me.PrintPreviewBarCheckItem10.Id = 50
        Me.PrintPreviewBarCheckItem10.Name = "PrintPreviewBarCheckItem10"
        '
        'PrintPreviewBarCheckItem11
        '
        Me.PrintPreviewBarCheckItem11.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht
        Me.PrintPreviewBarCheckItem11.Enabled = False
        Me.PrintPreviewBarCheckItem11.GroupIndex = 1
        Me.PrintPreviewBarCheckItem11.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem11.Id = 51
        Me.PrintPreviewBarCheckItem11.Name = "PrintPreviewBarCheckItem11"
        '
        'PrintPreviewBarCheckItem12
        '
        Me.PrintPreviewBarCheckItem12.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf
        Me.PrintPreviewBarCheckItem12.Enabled = False
        Me.PrintPreviewBarCheckItem12.GroupIndex = 1
        Me.PrintPreviewBarCheckItem12.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem12.Id = 52
        Me.PrintPreviewBarCheckItem12.Name = "PrintPreviewBarCheckItem12"
        '
        'PrintPreviewBarCheckItem13
        '
        Me.PrintPreviewBarCheckItem13.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls
        Me.PrintPreviewBarCheckItem13.Enabled = False
        Me.PrintPreviewBarCheckItem13.GroupIndex = 1
        Me.PrintPreviewBarCheckItem13.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem13.Id = 53
        Me.PrintPreviewBarCheckItem13.Name = "PrintPreviewBarCheckItem13"
        '
        'PrintPreviewBarCheckItem14
        '
        Me.PrintPreviewBarCheckItem14.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx
        Me.PrintPreviewBarCheckItem14.Enabled = False
        Me.PrintPreviewBarCheckItem14.GroupIndex = 1
        Me.PrintPreviewBarCheckItem14.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Id = 54
        Me.PrintPreviewBarCheckItem14.Name = "PrintPreviewBarCheckItem14"
        '
        'PrintPreviewBarCheckItem15
        '
        Me.PrintPreviewBarCheckItem15.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv
        Me.PrintPreviewBarCheckItem15.Enabled = False
        Me.PrintPreviewBarCheckItem15.GroupIndex = 1
        Me.PrintPreviewBarCheckItem15.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem15.Id = 55
        Me.PrintPreviewBarCheckItem15.Name = "PrintPreviewBarCheckItem15"
        '
        'PrintPreviewBarCheckItem16
        '
        Me.PrintPreviewBarCheckItem16.Caption = "Text File"
        Me.PrintPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt
        Me.PrintPreviewBarCheckItem16.Enabled = False
        Me.PrintPreviewBarCheckItem16.GroupIndex = 1
        Me.PrintPreviewBarCheckItem16.Hint = "Text File"
        Me.PrintPreviewBarCheckItem16.Id = 56
        Me.PrintPreviewBarCheckItem16.Name = "PrintPreviewBarCheckItem16"
        '
        'PrintPreviewBarCheckItem17
        '
        Me.PrintPreviewBarCheckItem17.Caption = "Image File"
        Me.PrintPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic
        Me.PrintPreviewBarCheckItem17.Enabled = False
        Me.PrintPreviewBarCheckItem17.GroupIndex = 1
        Me.PrintPreviewBarCheckItem17.Hint = "Image File"
        Me.PrintPreviewBarCheckItem17.Id = 57
        Me.PrintPreviewBarCheckItem17.Name = "PrintPreviewBarCheckItem17"
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'FrmReportContracts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmReportContracts"
        Me.Opacity = 1.0R
        Me.Tag = "1676"
        Me.Text = "Informe de Contratos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxeNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleContractEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleHealAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleContracts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciContracts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHealtAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciContractEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDocumentViewer.ResumeLayout(False)
        Me.INDDvDocumentViewer.ResumeLayout(False)
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnc As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSleContracts As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciContracts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxeNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDSleContractEntity As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleHealAdministrator As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciHealtAdministrator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciContractEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleStatus As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcDocumentViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDCnBack As Presentation.Controls.CtrNavigation
    Friend WithEvents INDDvDocumentViewer As DevExpress.XtraPrinting.Preview.DocumentViewer
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
    Friend WithEvents PrintPreviewBarItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents ZoomBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomBarEditItem
    Friend WithEvents PrintPreviewRepositoryItemComboBox1 As DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox
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
    Friend WithEvents PrintPreviewBarItem27 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
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
    Friend WithEvents PrintPreviewBarItem28 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem29 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents PrintPreviewSubItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
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
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDContractCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDContractName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDContractObject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDContractStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
End Class
