Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportNotesListP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportNotesListP))
        Me.RepositoryItemImageComboBox12 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox13 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox9 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox10 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox5 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox6 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox7 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox8 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox3 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox4 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCncNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleNotesEnd = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEnds = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStatusEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNatureEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleNotesStart = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateStarts = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStatusStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNatureStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleThirdPartyEnd = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDNitEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPersonTypeEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDContributionTypeEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRetentionTypeEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStateEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleThirdPartyStart = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IDNNitStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPersonTypeStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDContributionTypeStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRetentionTypeStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStateStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLblNotes = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblThirdParty = New DevExpress.XtraEditors.LabelControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleNatureReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleStatusReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStatusReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNatureReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciSbGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdPartyStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdPartyEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNotesStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNotesEnd = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.INDDvViewReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
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
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.INDPcViewReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDSleNotesEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleNotesStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdPartyEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdPartyStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleNatureReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleStatusReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStatusReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNatureReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSbGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdPartyStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdPartyEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNotesStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNotesEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvViewReport.SuspendLayout()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcViewReport.SuspendLayout()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncNavigation)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcViewReport)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 24)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 705)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'RepositoryItemImageComboBox12
        '
        Me.RepositoryItemImageComboBox12.AccessibleName = "INDRceStatusEnd"
        Me.RepositoryItemImageComboBox12.AutoHeight = False
        Me.RepositoryItemImageComboBox12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox12.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Registrado", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Confirmado", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anulado", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox12.Name = "RepositoryItemImageComboBox12"
        '
        'RepositoryItemImageComboBox13
        '
        Me.RepositoryItemImageComboBox13.AccessibleName = "INDRceNatureEnd"
        Me.RepositoryItemImageComboBox13.AutoHeight = False
        Me.RepositoryItemImageComboBox13.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox13.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Débito", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Crédito", CType(2, Byte), -1)})
        Me.RepositoryItemImageComboBox13.Name = "RepositoryItemImageComboBox13"
        '
        'RepositoryItemImageComboBox9
        '
        Me.RepositoryItemImageComboBox9.AccessibleName = "INDRceStatusStart"
        Me.RepositoryItemImageComboBox9.AutoHeight = False
        Me.RepositoryItemImageComboBox9.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox9.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Registrado", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Confirmado", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Anulado", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox9.Name = "RepositoryItemImageComboBox9"
        '
        'RepositoryItemImageComboBox10
        '
        Me.RepositoryItemImageComboBox10.AccessibleName = "INDRceNatureStart"
        Me.RepositoryItemImageComboBox10.AutoHeight = False
        Me.RepositoryItemImageComboBox10.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox10.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Débito", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Crédito", CType(2, Byte), -1)})
        Me.RepositoryItemImageComboBox10.Name = "RepositoryItemImageComboBox10"
        '
        'RepositoryItemImageComboBox5
        '
        Me.RepositoryItemImageComboBox5.AccessibleName = "INDRcePersonTypeEnd"
        Me.RepositoryItemImageComboBox5.AutoHeight = False
        Me.RepositoryItemImageComboBox5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox5.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Natural", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jurídico", CType(2, Byte), -1)})
        Me.RepositoryItemImageComboBox5.Name = "RepositoryItemImageComboBox5"
        '
        'RepositoryItemImageComboBox6
        '
        Me.RepositoryItemImageComboBox6.AccessibleName = "INDRceContributionTypeEnd"
        Me.RepositoryItemImageComboBox6.AutoHeight = False
        Me.RepositoryItemImageComboBox6.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox6.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Simplificado", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Común", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Empresa Estatal", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gran Contribuyente", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox6.Name = "RepositoryItemImageComboBox6"
        '
        'RepositoryItemImageComboBox7
        '
        Me.RepositoryItemImageComboBox7.AccessibleName = "IDNRceRetentionTypeEnd"
        Me.RepositoryItemImageComboBox7.AutoHeight = False
        Me.RepositoryItemImageComboBox7.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox7.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Exento De Retención", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hace Retención", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autoretendedor", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox7.Name = "RepositoryItemImageComboBox7"
        '
        'RepositoryItemImageComboBox8
        '
        Me.RepositoryItemImageComboBox8.AccessibleName = "INDRceStateEnd"
        Me.RepositoryItemImageComboBox8.AutoHeight = False
        Me.RepositoryItemImageComboBox8.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox8.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", CType(1, Byte), -1)})
        Me.RepositoryItemImageComboBox8.Name = "RepositoryItemImageComboBox8"
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AccessibleName = "INDRcePersonTypeStart"
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Natural", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jurídico", CType(2, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'RepositoryItemImageComboBox2
        '
        Me.RepositoryItemImageComboBox2.AccessibleName = "INDRceContributionTypeStart"
        Me.RepositoryItemImageComboBox2.AutoHeight = False
        Me.RepositoryItemImageComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox2.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Simplificado", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Común", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Empresa Estatal", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gran Contribuyente", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox2.Name = "RepositoryItemImageComboBox2"
        '
        'RepositoryItemImageComboBox3
        '
        Me.RepositoryItemImageComboBox3.AccessibleName = "INDRceRetentionTypeStart"
        Me.RepositoryItemImageComboBox3.AutoHeight = False
        Me.RepositoryItemImageComboBox3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox3.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Exento De Retención", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hace Retención", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autoretenedor", CType(3, Byte), -1)})
        Me.RepositoryItemImageComboBox3.Name = "RepositoryItemImageComboBox3"
        '
        'RepositoryItemImageComboBox4
        '
        Me.RepositoryItemImageComboBox4.AccessibleName = "INDRceStatusStart"
        Me.RepositoryItemImageComboBox4.AutoHeight = False
        Me.RepositoryItemImageComboBox4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox4.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", CType(1, Byte), -1)})
        Me.RepositoryItemImageComboBox4.Name = "RepositoryItemImageComboBox4"
        '
        'INDCncNavigation
        '
        Me.INDCncNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncNavigation.LayoutControl = Me.INDLcBase
        Me.INDCncNavigation.Location = New System.Drawing.Point(2, 7)
        Me.INDCncNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncNavigation.Name = "INDCncNavigation"
        Me.INDCncNavigation.Size = New System.Drawing.Size(200, 696)
        Me.INDCncNavigation.TabIndex = 0
        Me.INDCncNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSleNotesEnd)
        Me.INDLcBase.Controls.Add(Me.INDSleNotesStart)
        Me.INDLcBase.Controls.Add(Me.INDSleThirdPartyEnd)
        Me.INDLcBase.Controls.Add(Me.INDSleThirdPartyStart)
        Me.INDLcBase.Controls.Add(Me.INDLblNotes)
        Me.INDLcBase.Controls.Add(Me.INDLblThirdParty)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDGleTypeReport)
        Me.INDLcBase.Controls.Add(Me.INDGleNatureReport)
        Me.INDLcBase.Controls.Add(Me.INDGleStatusReport)
        Me.INDLcBase.Controls.Add(Me.INDDateEnd)
        Me.INDLcBase.Controls.Add(Me.INDDateStart)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 696)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSleNotesEnd
        '
        Me.INDSleNotesEnd.AllowQueryOne = True
        Me.INDSleNotesEnd.Datasource = Nothing
        Me.INDSleNotesEnd.DisplayMember = "{Code}"
        Me.INDSleNotesEnd.DisplayNullText = ""
        Me.INDSleNotesEnd.EditValue = Nothing
        Me.INDSleNotesEnd.EnterMoveNextControl = True
        Me.INDSleNotesEnd.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleNotesEnd.IdOpenForm = 0
        Me.INDSleNotesEnd.Location = New System.Drawing.Point(480, 205)
        Me.INDSleNotesEnd.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleNotesEnd.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleNotesEnd.Name = "INDSleNotesEnd"
        Me.INDSleNotesEnd.PopUpFormSize = New System.Drawing.Size(800, 300)
        Me.INDSleNotesEnd.Size = New System.Drawing.Size(314, 28)
        Me.INDSleNotesEnd.TabIndex = 9
        Me.INDSleNotesEnd.ValueMember = "Code"
        Me.INDSleNotesEnd.View = Me.SearchLookUpEditExView11
        '
        'SearchLookUpEditExView11
        '
        Me.SearchLookUpEditExView11.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView11.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView11.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView11.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView11.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView11.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView11.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView11.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView11.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView11.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView11.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeEnd, Me.INDDateEnds, Me.INDStatusEnd, Me.INDNatureEnd})
        Me.SearchLookUpEditExView11.Name = "SearchLookUpEditExView11"
        Me.SearchLookUpEditExView11.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView11.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView11.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView11.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEditExView11.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView11.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView11.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView11.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView11.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView11, False)
        '
        'INDCodeEnd
        '
        Me.INDCodeEnd.Caption = "Código"
        Me.INDCodeEnd.FieldName = "Code"
        Me.INDCodeEnd.Name = "INDCodeEnd"
        Me.INDCodeEnd.OptionsColumn.AllowEdit = False
        Me.INDCodeEnd.Visible = True
        Me.INDCodeEnd.VisibleIndex = 0
        '
        'INDDateEnds
        '
        Me.INDDateEnds.Caption = "Fecha"
        Me.INDDateEnds.FieldName = "NoteDate"
        Me.INDDateEnds.Name = "INDDateEnds"
        Me.INDDateEnds.OptionsColumn.AllowEdit = False
        Me.INDDateEnds.Visible = True
        Me.INDDateEnds.VisibleIndex = 1
        '
        'INDStatusEnd
        '
        Me.INDStatusEnd.Caption = "Estado"
        Me.INDStatusEnd.ColumnEdit = Me.RepositoryItemImageComboBox12
        Me.INDStatusEnd.FieldName = "Status"
        Me.INDStatusEnd.Name = "INDStatusEnd"
        Me.INDStatusEnd.OptionsColumn.AllowEdit = False
        Me.INDStatusEnd.Visible = True
        Me.INDStatusEnd.VisibleIndex = 2
        '
        'INDNatureEnd
        '
        Me.INDNatureEnd.Caption = "Naturaleza"
        Me.INDNatureEnd.ColumnEdit = Me.RepositoryItemImageComboBox13
        Me.INDNatureEnd.FieldName = "Nature"
        Me.INDNatureEnd.Name = "INDNatureEnd"
        Me.INDNatureEnd.OptionsColumn.AllowEdit = False
        Me.INDNatureEnd.Visible = True
        Me.INDNatureEnd.VisibleIndex = 3
        '
        'INDSleNotesStart
        '
        Me.INDSleNotesStart.AllowQueryOne = True
        Me.INDSleNotesStart.Datasource = Nothing
        Me.INDSleNotesStart.DisplayMember = "{Code}"
        Me.INDSleNotesStart.DisplayNullText = ""
        Me.INDSleNotesStart.EditValue = Nothing
        Me.INDSleNotesStart.EnterMoveNextControl = True
        Me.INDSleNotesStart.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleNotesStart.IdOpenForm = 0
        Me.INDSleNotesStart.Location = New System.Drawing.Point(480, 173)
        Me.INDSleNotesStart.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleNotesStart.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleNotesStart.Name = "INDSleNotesStart"
        Me.INDSleNotesStart.PopUpFormSize = New System.Drawing.Size(800, 300)
        Me.INDSleNotesStart.Size = New System.Drawing.Size(314, 28)
        Me.INDSleNotesStart.TabIndex = 8
        Me.INDSleNotesStart.ValueMember = "Code"
        Me.INDSleNotesStart.View = Me.SearchLookUpEditExView10
        '
        'SearchLookUpEditExView10
        '
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView10.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView10.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView10.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView10.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeStart, Me.INDDateStarts, Me.INDStatusStart, Me.INDNatureStart})
        Me.SearchLookUpEditExView10.Name = "SearchLookUpEditExView10"
        Me.SearchLookUpEditExView10.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView10.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView10.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView10.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEditExView10.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView10.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView10.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView10.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView10.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView10, False)
        '
        'INDCodeStart
        '
        Me.INDCodeStart.Caption = "Código"
        Me.INDCodeStart.FieldName = "Code"
        Me.INDCodeStart.Name = "INDCodeStart"
        Me.INDCodeStart.OptionsColumn.AllowEdit = False
        Me.INDCodeStart.Visible = True
        Me.INDCodeStart.VisibleIndex = 0
        '
        'INDDateStarts
        '
        Me.INDDateStarts.Caption = "Fecha"
        Me.INDDateStarts.FieldName = "NoteDate"
        Me.INDDateStarts.Name = "INDDateStarts"
        Me.INDDateStarts.OptionsColumn.AllowEdit = False
        Me.INDDateStarts.Visible = True
        Me.INDDateStarts.VisibleIndex = 1
        '
        'INDStatusStart
        '
        Me.INDStatusStart.Caption = "Estado"
        Me.INDStatusStart.ColumnEdit = Me.RepositoryItemImageComboBox9
        Me.INDStatusStart.FieldName = "Status"
        Me.INDStatusStart.Name = "INDStatusStart"
        Me.INDStatusStart.OptionsColumn.AllowEdit = False
        Me.INDStatusStart.Visible = True
        Me.INDStatusStart.VisibleIndex = 2
        '
        'INDNatureStart
        '
        Me.INDNatureStart.Caption = "Naturaleza"
        Me.INDNatureStart.ColumnEdit = Me.RepositoryItemImageComboBox10
        Me.INDNatureStart.FieldName = "Nature"
        Me.INDNatureStart.Name = "INDNatureStart"
        Me.INDNatureStart.OptionsColumn.AllowEdit = False
        Me.INDNatureStart.Visible = True
        Me.INDNatureStart.VisibleIndex = 3
        '
        'INDSleThirdPartyEnd
        '
        Me.INDSleThirdPartyEnd.AllowQueryOne = True
        Me.INDSleThirdPartyEnd.Datasource = Nothing
        Me.INDSleThirdPartyEnd.DisplayMember = "{Nit}"
        Me.INDSleThirdPartyEnd.DisplayNullText = ""
        Me.INDSleThirdPartyEnd.EditValue = Nothing
        Me.INDSleThirdPartyEnd.EnterMoveNextControl = True
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleThirdPartyEnd.IdOpenForm = 0
        Me.INDSleThirdPartyEnd.Location = New System.Drawing.Point(480, 116)
        Me.INDSleThirdPartyEnd.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleThirdPartyEnd.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleThirdPartyEnd.Name = "INDSleThirdPartyEnd"
        Me.INDSleThirdPartyEnd.PopUpFormSize = New System.Drawing.Size(800, 300)
        Me.INDSleThirdPartyEnd.Size = New System.Drawing.Size(314, 28)
        Me.INDSleThirdPartyEnd.TabIndex = 7
        Me.INDSleThirdPartyEnd.ValueMember = "Nit"
        Me.INDSleThirdPartyEnd.View = Me.SearchLookUpEditExView9
        '
        'SearchLookUpEditExView9
        '
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView9.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView9.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView9.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView9.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDNitEnd, Me.INDNameEnd, Me.INDPersonTypeEnd, Me.INDContributionTypeEnd, Me.INDRetentionTypeEnd, Me.INDStateEnd})
        Me.SearchLookUpEditExView9.Name = "SearchLookUpEditExView9"
        Me.SearchLookUpEditExView9.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView9.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView9.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView9.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEditExView9.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView9.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView9.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView9.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView9.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView9, False)
        '
        'INDNitEnd
        '
        Me.INDNitEnd.Caption = "Nit"
        Me.INDNitEnd.FieldName = "Nit"
        Me.INDNitEnd.Name = "INDNitEnd"
        Me.INDNitEnd.OptionsColumn.AllowEdit = False
        Me.INDNitEnd.Visible = True
        Me.INDNitEnd.VisibleIndex = 0
        '
        'INDNameEnd
        '
        Me.INDNameEnd.Caption = "Nombre"
        Me.INDNameEnd.FieldName = "Name"
        Me.INDNameEnd.Name = "INDNameEnd"
        Me.INDNameEnd.OptionsColumn.AllowEdit = False
        Me.INDNameEnd.Visible = True
        Me.INDNameEnd.VisibleIndex = 1
        '
        'INDPersonTypeEnd
        '
        Me.INDPersonTypeEnd.Caption = "Tipo Persona"
        Me.INDPersonTypeEnd.ColumnEdit = Me.RepositoryItemImageComboBox5
        Me.INDPersonTypeEnd.FieldName = "PersonType"
        Me.INDPersonTypeEnd.Name = "INDPersonTypeEnd"
        Me.INDPersonTypeEnd.OptionsColumn.AllowEdit = False
        Me.INDPersonTypeEnd.Visible = True
        Me.INDPersonTypeEnd.VisibleIndex = 2
        '
        'INDContributionTypeEnd
        '
        Me.INDContributionTypeEnd.Caption = "Tipo Contribución"
        Me.INDContributionTypeEnd.ColumnEdit = Me.RepositoryItemImageComboBox6
        Me.INDContributionTypeEnd.FieldName = "ContributionType"
        Me.INDContributionTypeEnd.Name = "INDContributionTypeEnd"
        Me.INDContributionTypeEnd.OptionsColumn.AllowEdit = False
        Me.INDContributionTypeEnd.Visible = True
        Me.INDContributionTypeEnd.VisibleIndex = 3
        '
        'INDRetentionTypeEnd
        '
        Me.INDRetentionTypeEnd.Caption = "Tipo Retención"
        Me.INDRetentionTypeEnd.ColumnEdit = Me.RepositoryItemImageComboBox7
        Me.INDRetentionTypeEnd.FieldName = "RetentionType"
        Me.INDRetentionTypeEnd.Name = "INDRetentionTypeEnd"
        Me.INDRetentionTypeEnd.OptionsColumn.AllowEdit = False
        Me.INDRetentionTypeEnd.Visible = True
        Me.INDRetentionTypeEnd.VisibleIndex = 4
        '
        'INDStateEnd
        '
        Me.INDStateEnd.Caption = "Estado"
        Me.INDStateEnd.ColumnEdit = Me.RepositoryItemImageComboBox8
        Me.INDStateEnd.FieldName = "State"
        Me.INDStateEnd.Name = "INDStateEnd"
        Me.INDStateEnd.OptionsColumn.AllowEdit = False
        Me.INDStateEnd.Visible = True
        Me.INDStateEnd.VisibleIndex = 5
        '
        'INDSleThirdPartyStart
        '
        Me.INDSleThirdPartyStart.AllowQueryOne = True
        Me.INDSleThirdPartyStart.Datasource = Nothing
        Me.INDSleThirdPartyStart.DisplayMember = "{Nit}"
        Me.INDSleThirdPartyStart.DisplayNullText = ""
        Me.INDSleThirdPartyStart.EditValue = Nothing
        Me.INDSleThirdPartyStart.EnterMoveNextControl = True
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleThirdPartyStart.IdOpenForm = 0
        Me.INDSleThirdPartyStart.Location = New System.Drawing.Point(480, 84)
        Me.INDSleThirdPartyStart.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleThirdPartyStart.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleThirdPartyStart.Name = "INDSleThirdPartyStart"
        Me.INDSleThirdPartyStart.PopUpFormSize = New System.Drawing.Size(800, 300)
        Me.INDSleThirdPartyStart.Size = New System.Drawing.Size(314, 28)
        Me.INDSleThirdPartyStart.TabIndex = 6
        Me.INDSleThirdPartyStart.ValueMember = "Nit"
        Me.INDSleThirdPartyStart.View = Me.SearchLookUpEditExView8
        '
        'SearchLookUpEditExView8
        '
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView8.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.IDNNitStart, Me.INDNameStart, Me.INDPersonTypeStart, Me.INDContributionTypeStart, Me.INDRetentionTypeStart, Me.INDStateStart})
        Me.SearchLookUpEditExView8.Name = "SearchLookUpEditExView8"
        Me.SearchLookUpEditExView8.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView8.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView8.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView8.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView8.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView8, False)
        '
        'IDNNitStart
        '
        Me.IDNNitStart.Caption = "Nit"
        Me.IDNNitStart.FieldName = "Nit"
        Me.IDNNitStart.Name = "IDNNitStart"
        Me.IDNNitStart.OptionsColumn.AllowEdit = False
        Me.IDNNitStart.Visible = True
        Me.IDNNitStart.VisibleIndex = 0
        '
        'INDNameStart
        '
        Me.INDNameStart.Caption = "Nombre"
        Me.INDNameStart.FieldName = "Name"
        Me.INDNameStart.Name = "INDNameStart"
        Me.INDNameStart.OptionsColumn.AllowEdit = False
        Me.INDNameStart.Visible = True
        Me.INDNameStart.VisibleIndex = 1
        '
        'INDPersonTypeStart
        '
        Me.INDPersonTypeStart.Caption = "Tipo Persona"
        Me.INDPersonTypeStart.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.INDPersonTypeStart.FieldName = "PersonType"
        Me.INDPersonTypeStart.Name = "INDPersonTypeStart"
        Me.INDPersonTypeStart.OptionsColumn.AllowEdit = False
        Me.INDPersonTypeStart.Visible = True
        Me.INDPersonTypeStart.VisibleIndex = 2
        '
        'INDContributionTypeStart
        '
        Me.INDContributionTypeStart.Caption = "Tipo Contribución"
        Me.INDContributionTypeStart.ColumnEdit = Me.RepositoryItemImageComboBox2
        Me.INDContributionTypeStart.FieldName = "ContributionType"
        Me.INDContributionTypeStart.Name = "INDContributionTypeStart"
        Me.INDContributionTypeStart.OptionsColumn.AllowEdit = False
        Me.INDContributionTypeStart.Visible = True
        Me.INDContributionTypeStart.VisibleIndex = 3
        '
        'INDRetentionTypeStart
        '
        Me.INDRetentionTypeStart.Caption = "Tipo Retención"
        Me.INDRetentionTypeStart.ColumnEdit = Me.RepositoryItemImageComboBox3
        Me.INDRetentionTypeStart.FieldName = "RetentionType"
        Me.INDRetentionTypeStart.Name = "INDRetentionTypeStart"
        Me.INDRetentionTypeStart.OptionsColumn.AllowEdit = False
        Me.INDRetentionTypeStart.Visible = True
        Me.INDRetentionTypeStart.VisibleIndex = 4
        '
        'INDStateStart
        '
        Me.INDStateStart.Caption = "Estado"
        Me.INDStateStart.ColumnEdit = Me.RepositoryItemImageComboBox4
        Me.INDStateStart.FieldName = "State"
        Me.INDStateStart.Name = "INDStateStart"
        Me.INDStateStart.OptionsColumn.AllowEdit = False
        Me.INDStateStart.Visible = True
        Me.INDStateStart.VisibleIndex = 5
        '
        'INDLblNotes
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLblNotes, True)
        Me.INDLblNotes.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLblNotes, False)
        Me.INDLblNotes.Location = New System.Drawing.Point(408, 148)
        Me.INDLblNotes.Name = "INDLblNotes"
        Me.INDLblNotes.Size = New System.Drawing.Size(33, 21)
        Me.INDLblNotes.StyleController = Me.INDLcBase
        Me.INDLblNotes.TabIndex = 15
        Me.INDLblNotes.Text = "Nota"
        '
        'INDLblThirdParty
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLblThirdParty, True)
        Me.INDLblThirdParty.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLblThirdParty, False)
        Me.INDLblThirdParty.Location = New System.Drawing.Point(408, 59)
        Me.INDLblThirdParty.Name = "INDLblThirdParty"
        Me.INDLblThirdParty.Size = New System.Drawing.Size(50, 21)
        Me.INDLblThirdParty.StyleController = Me.INDLcBase
        Me.INDLblThirdParty.TabIndex = 12
        Me.INDLblThirdParty.Text = "Tercero"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(408, 245)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(386, 28)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 10
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDGleTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.Location = New System.Drawing.Point(24, 307)
        Me.INDGleTypeReport.Name = "INDGleTypeReport"
        Me.INDGleTypeReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeReport.Properties.DisplayMember = "Item2"
        Me.INDGleTypeReport.Properties.ImmediatePopup = True
        Me.INDGleTypeReport.Properties.NullText = ""
        Me.INDGleTypeReport.Properties.ValueMember = "Item1"
        Me.INDGleTypeReport.Properties.View = Me.GridView2
        Me.INDGleTypeReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleTypeReport.StyleController = Me.INDLcBase
        Me.INDGleTypeReport.TabIndex = 5
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeReport, Nothing)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tipo de Reporte"
        Me.GridColumn8.FieldName = "Item2"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'INDGleNatureReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleNatureReport, False)
        Me.INDGleNatureReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleNatureReport, False)
        Me.INDGleNatureReport.Location = New System.Drawing.Point(24, 251)
        Me.INDGleNatureReport.Name = "INDGleNatureReport"
        Me.INDGleNatureReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleNatureReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleNatureReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleNatureReport.Properties.DisplayMember = "Item2"
        Me.INDGleNatureReport.Properties.ImmediatePopup = True
        Me.INDGleNatureReport.Properties.NullText = ""
        Me.INDGleNatureReport.Properties.ValueMember = "Item1"
        Me.INDGleNatureReport.Properties.View = Me.GridView1
        Me.INDGleNatureReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleNatureReport.StyleController = Me.INDLcBase
        Me.INDGleNatureReport.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleNatureReport, Nothing)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Naturaleza"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'INDGleStatusReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleStatusReport, False)
        Me.INDGleStatusReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleStatusReport, False)
        Me.INDGleStatusReport.Location = New System.Drawing.Point(24, 195)
        Me.INDGleStatusReport.Name = "INDGleStatusReport"
        Me.INDGleStatusReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleStatusReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleStatusReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleStatusReport.Properties.DisplayMember = "Item2"
        Me.INDGleStatusReport.Properties.ImmediatePopup = True
        Me.INDGleStatusReport.Properties.NullText = ""
        Me.INDGleStatusReport.Properties.ValueMember = "Item1"
        Me.INDGleStatusReport.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleStatusReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleStatusReport.StyleController = Me.INDLcBase
        Me.INDGleStatusReport.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleStatusReport, Nothing)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Estado"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDDateEnd
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEnd, False)
        Me.INDDateEnd.EditValue = Nothing
        Me.INDDateEnd.EnterMoveNextControl = True
        Me.INDDateEnd.Location = New System.Drawing.Point(24, 139)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
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
        Me.INDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEnd.StyleController = Me.INDLcBase
        Me.INDDateEnd.TabIndex = 2
        '
        'INDDateStart
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateStart, False)
        Me.INDDateStart.EditValue = Nothing
        Me.INDDateStart.EnterMoveNextControl = True
        Me.INDDateStart.Location = New System.Drawing.Point(24, 83)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateStart, Presentation.Controls.IndigoDate.EMask.Fecha)
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
        Me.INDDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart.Size = New System.Drawing.Size(356, 28)
        Me.INDDateStart.StyleController = Me.INDLcBase
        Me.INDDateStart.TabIndex = 1
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
        Me.INDLcgBase.CustomizationFormText = "INDLcgBase"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterRequired, Me.INDLcgFilterOptional})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(818, 679)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgFilterRequired
        '
        Me.INDLcgFilterRequired.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterRequired.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDLcgFilterRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDateEnd, Me.INDLciStatusReport, Me.INDLciNatureReport, Me.INDLciTypeReport})
        Me.INDLcgFilterRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterRequired.Name = "INDLcgFilterRequired"
        Me.INDLcgFilterRequired.Size = New System.Drawing.Size(384, 659)
        Me.INDLcgFilterRequired.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateStart
        Me.INDLciDateStart.CustomizationFormText = "Fecha Inicial:"
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial:"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(107, 21)
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEnd
        Me.INDLciDateEnd.CustomizationFormText = "Fecha Final:"
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final:"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(107, 21)
        '
        'INDLciStatusReport
        '
        Me.INDLciStatusReport.Control = Me.INDGleStatusReport
        Me.INDLciStatusReport.CustomizationFormText = "Estado"
        Me.INDLciStatusReport.Location = New System.Drawing.Point(0, 112)
        Me.INDLciStatusReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatusReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatusReport.Name = "INDLciStatusReport"
        Me.INDLciStatusReport.Size = New System.Drawing.Size(360, 56)
        Me.INDLciStatusReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStatusReport.Text = "Estado"
        Me.INDLciStatusReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStatusReport.TextSize = New System.Drawing.Size(107, 21)
        '
        'INDLciNatureReport
        '
        Me.INDLciNatureReport.Control = Me.INDGleNatureReport
        Me.INDLciNatureReport.CustomizationFormText = "Naturaleza"
        Me.INDLciNatureReport.Location = New System.Drawing.Point(0, 168)
        Me.INDLciNatureReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciNatureReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciNatureReport.Name = "INDLciNatureReport"
        Me.INDLciNatureReport.Size = New System.Drawing.Size(360, 56)
        Me.INDLciNatureReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNatureReport.Text = "Naturaleza"
        Me.INDLciNatureReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNatureReport.TextSize = New System.Drawing.Size(107, 21)
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGleTypeReport
        Me.INDLciTypeReport.CustomizationFormText = "Tipo de Reporte"
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 224)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Size = New System.Drawing.Size(360, 376)
        Me.INDLciTypeReport.Text = "Tipo de Reporte"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(107, 21)
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
        Me.INDLcgFilterOptional.CustomizationFormText = "Rangos"
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciSbGenerateReport, Me.INDLciThirdParty, Me.INDLciNotes, Me.INDLciThirdPartyStart, Me.INDLciThirdPartyEnd, Me.INDLciNotesStart, Me.INDLciNotesEnd})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(384, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(414, 659)
        Me.INDLcgFilterOptional.Text = "Filtros"
        '
        'INDLciSbGenerateReport
        '
        Me.INDLciSbGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciSbGenerateReport.CustomizationFormText = "INDLciSbGenerateReport"
        Me.INDLciSbGenerateReport.Location = New System.Drawing.Point(0, 178)
        Me.INDLciSbGenerateReport.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDLciSbGenerateReport.MinSize = New System.Drawing.Size(390, 40)
        Me.INDLciSbGenerateReport.Name = "INDLciSbGenerateReport"
        Me.INDLciSbGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INDLciSbGenerateReport.Size = New System.Drawing.Size(390, 422)
        Me.INDLciSbGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSbGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciSbGenerateReport.TextVisible = False
        '
        'INDLciThirdParty
        '
        Me.INDLciThirdParty.Control = Me.INDLblThirdParty
        Me.INDLciThirdParty.CustomizationFormText = "INDLciThirdParty"
        Me.INDLciThirdParty.Location = New System.Drawing.Point(0, 0)
        Me.INDLciThirdParty.Name = "INDLciThirdParty"
        Me.INDLciThirdParty.Size = New System.Drawing.Size(390, 25)
        Me.INDLciThirdParty.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciThirdParty.TextVisible = False
        '
        'INDLciNotes
        '
        Me.INDLciNotes.Control = Me.INDLblNotes
        Me.INDLciNotes.CustomizationFormText = "INDLciNotes"
        Me.INDLciNotes.Location = New System.Drawing.Point(0, 89)
        Me.INDLciNotes.Name = "INDLciNotes"
        Me.INDLciNotes.Size = New System.Drawing.Size(390, 25)
        Me.INDLciNotes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciNotes.TextVisible = False
        '
        'INDLciThirdPartyStart
        '
        Me.INDLciThirdPartyStart.Control = Me.INDSleThirdPartyStart
        Me.INDLciThirdPartyStart.CustomizationFormText = "Inicial:"
        Me.INDLciThirdPartyStart.Location = New System.Drawing.Point(0, 25)
        Me.INDLciThirdPartyStart.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyStart.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyStart.Name = "INDLciThirdPartyStart"
        Me.INDLciThirdPartyStart.Size = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdPartyStart.Text = "Inicial:"
        Me.INDLciThirdPartyStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdPartyStart.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciThirdPartyStart.TextToControlDistance = 12
        '
        'INDLciThirdPartyEnd
        '
        Me.INDLciThirdPartyEnd.Control = Me.INDSleThirdPartyEnd
        Me.INDLciThirdPartyEnd.CustomizationFormText = "Final:"
        Me.INDLciThirdPartyEnd.Location = New System.Drawing.Point(0, 57)
        Me.INDLciThirdPartyEnd.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyEnd.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyEnd.Name = "INDLciThirdPartyEnd"
        Me.INDLciThirdPartyEnd.Size = New System.Drawing.Size(390, 32)
        Me.INDLciThirdPartyEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdPartyEnd.Text = "Final:"
        Me.INDLciThirdPartyEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdPartyEnd.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciThirdPartyEnd.TextToControlDistance = 12
        '
        'INDLciNotesStart
        '
        Me.INDLciNotesStart.Control = Me.INDSleNotesStart
        Me.INDLciNotesStart.CustomizationFormText = "Inicial:"
        Me.INDLciNotesStart.Location = New System.Drawing.Point(0, 114)
        Me.INDLciNotesStart.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciNotesStart.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciNotesStart.Name = "INDLciNotesStart"
        Me.INDLciNotesStart.Size = New System.Drawing.Size(390, 32)
        Me.INDLciNotesStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNotesStart.Text = "Inicial:"
        Me.INDLciNotesStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNotesStart.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciNotesStart.TextToControlDistance = 12
        '
        'INDLciNotesEnd
        '
        Me.INDLciNotesEnd.Control = Me.INDSleNotesEnd
        Me.INDLciNotesEnd.CustomizationFormText = "Final:"
        Me.INDLciNotesEnd.Location = New System.Drawing.Point(0, 146)
        Me.INDLciNotesEnd.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciNotesEnd.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciNotesEnd.Name = "INDLciNotesEnd"
        Me.INDLciNotesEnd.Size = New System.Drawing.Size(390, 32)
        Me.INDLciNotesEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNotesEnd.Text = "Final:"
        Me.INDLciNotesEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNotesEnd.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciNotesEnd.TextToControlDistance = 12
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvViewReport
        Me.DocumentViewerBarManager1.Form = Me.INDDvViewReport
        Me.DocumentViewerBarManager1.ImageStream = CType(resources.GetObject("DocumentViewerBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem16, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem27, Me.PrintPreviewBarItem28, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 57
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
        Me.PrintPreviewBarItem4.Caption = "Search"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Search"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageIndex = 20
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.Caption = "Customize"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Customize"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageIndex = 14
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Open"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Open a document"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageIndex = 23
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Save"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Save the document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "&Print..."
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Print"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageIndex = 0
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "P&rint"
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Quick Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageIndex = 1
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Page Setup"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageIndex = 2
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Header And Footer"
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Header And Footer"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageIndex = 15
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
        Me.PrintPreviewBarItem12.ImageIndex = 25
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
        Me.PrintPreviewBarItem13.ImageIndex = 16
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
        Me.PrintPreviewBarItem14.ImageIndex = 3
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.Caption = "Zoom Out"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Zoom Out"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageIndex = 5
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 21
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
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom In"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom In"
        Me.PrintPreviewBarItem16.Id = 22
        Me.PrintPreviewBarItem16.ImageIndex = 4
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "First Page"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "First Page"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageIndex = 7
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "Previous Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "Previous Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageIndex = 8
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Next Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Next Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageIndex = 9
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Last Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Last Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageIndex = 10
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
        Me.PrintPreviewBarItem21.ImageIndex = 11
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
        Me.PrintPreviewBarItem22.ImageIndex = 12
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.Caption = "&Watermark..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Watermark"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageIndex = 21
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
        Me.PrintPreviewBarItem24.ImageIndex = 18
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
        Me.PrintPreviewBarItem25.ImageIndex = 17
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.Caption = "E&xit"
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Close Preview"
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageIndex = 13
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
        Me.PrintPreviewStaticItem2.Width = 40
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
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(930, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 666)
        Me.barDockControlBottom.Size = New System.Drawing.Size(930, 26)
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
        'INDDvViewReport
        '
        Me.INDDvViewReport.Controls.Add(Me.barDockControlLeft)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlRight)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlBottom)
        Me.INDDvViewReport.Controls.Add(Me.barDockControlTop)
        Me.INDDvViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvViewReport, True)
        Me.INDDvViewReport.IsMetric = True
        Me.INDDvViewReport.Location = New System.Drawing.Point(72, 2)
        Me.INDDvViewReport.Name = "INDDvViewReport"
        Me.INDDvViewReport.Size = New System.Drawing.Size(930, 692)
        Me.INDDvViewReport.TabIndex = 1
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
        'INDPcViewReport
        '
        Me.INDPcViewReport.Controls.Add(Me.INDDvViewReport)
        Me.INDPcViewReport.Controls.Add(Me.INDCnBack)
        Me.INDPcViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcViewReport.Location = New System.Drawing.Point(2, 7)
        Me.INDPcViewReport.Name = "INDPcViewReport"
        Me.INDPcViewReport.Size = New System.Drawing.Size(1004, 696)
        Me.INDPcViewReport.TabIndex = 2
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(70, 692)
        Me.INDCnBack.TabIndex = 0
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'FrmReportNotesListP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmReportNotesListP"
        Me.Opacity = 1.0R
        Me.Tag = "742"
        Me.Text = "Listado De Notas"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDSleNotesEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleNotesStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdPartyEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdPartyStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleNatureReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleStatusReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStatusReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNatureReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSbGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdPartyStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdPartyEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNotesStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNotesEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvViewReport.ResumeLayout(False)
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcViewReport.ResumeLayout(False)
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCncNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgFilterRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleStatusReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciStatusReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleNatureReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciNatureReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciSbGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcViewReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDCnBack As Presentation.Controls.CtrNavigation
    Friend WithEvents INDDvViewReport As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
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
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDLblThirdParty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDLblNotes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleThirdPartyStart As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SearchLookUpEditExView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SearchLookUpEditExView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SearchLookUpEditExView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciThirdPartyStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IDNNitStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPersonTypeStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDContributionTypeStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRetentionTypeStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStateStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleThirdPartyEnd As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents INDLciThirdPartyEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDNitEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPersonTypeEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDContributionTypeEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRetentionTypeEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStateEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox3 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox4 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDSleNotesStart As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents INDLciNotesStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemImageComboBox5 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox6 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox7 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox8 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCodeStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDateStarts As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStatusStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNatureStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleNotesEnd As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents INDLciNotesEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemImageComboBox9 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox10 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCodeEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDateEnds As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStatusEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNatureEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox12 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemImageComboBox13 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
End Class
