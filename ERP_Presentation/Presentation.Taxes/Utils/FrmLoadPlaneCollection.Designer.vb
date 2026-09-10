Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLoadPlaneCollection
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLoadPlaneCollection))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtProgress = New DevExpress.XtraEditors.TextEdit()
        Me.INDPbcProcess = New DevExpress.XtraEditors.ProgressBarControl()
        Me.INDGcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProgress = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProgressBar = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTxtProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPbcProcess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProgressBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtProgress)
        Me.LayoutControl1.Controls.Add(Me.INDPbcProcess)
        Me.LayoutControl1.Controls.Add(Me.INDGcDetail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1461, 574)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDTxtProgress
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProgress, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProgress, False)
        Me.INDTxtProgress.EditValue = "Iniciando el Proceso de Validación..."
        Me.INDTxtProgress.Enabled = False
        Me.INDTxtProgress.Location = New System.Drawing.Point(24, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProgress, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProgress.Name = "INDTxtProgress"
        Me.INDTxtProgress.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtProgress.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProgress.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProgress.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProgress.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtProgress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDTxtProgress.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProgress.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProgress.Size = New System.Drawing.Size(1413, 28)
        Me.INDTxtProgress.StyleController = Me.LayoutControl1
        Me.INDTxtProgress.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProgress, 0)
        '
        'INDPbcProcess
        '
        Me.INDPbcProcess.Location = New System.Drawing.Point(24, 24)
        Me.INDPbcProcess.Name = "INDPbcProcess"
        Me.INDPbcProcess.Properties.ShowTitle = True
        Me.INDPbcProcess.Size = New System.Drawing.Size(1413, 21)
        Me.INDPbcProcess.StyleController = Me.LayoutControl1
        Me.INDPbcProcess.TabIndex = 5
        '
        'INDGcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetail, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetail, False)
        Me.INDGcDetail.Location = New System.Drawing.Point(12, 97)
        Me.INDGcDetail.MainView = Me.INDGvDetail
        Me.INDGcDetail.Name = "INDGcDetail"
        Me.INDGcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDGcDetail.Size = New System.Drawing.Size(1437, 465)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetail, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcDetail, New System.Drawing.Size(1441, 0))
        Me.INDGcDetail.TabIndex = 4
        Me.INDGcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDetail})
        '
        'INDGvDetail
        '
        Me.INDGvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn2})
        Me.INDGvDetail.GridControl = Me.INDGcDetail
        Me.INDGvDetail.GroupCount = 1
        Me.INDGvDetail.GroupFormat = "[#image]{1} {2}"
        Me.INDGvDetail.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Item2", Nothing, "")})
        Me.INDGvDetail.Name = "INDGvDetail"
        Me.INDGvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetail.OptionsView.ShowDetailButtons = False
        Me.INDGvDetail.OptionsView.ShowFooter = True
        Me.INDGvDetail.OptionsView.ShowGroupPanel = False
        Me.INDGvDetail.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetail, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Estado"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn1.FieldName = "Item1"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 78
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 2)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        Me.RepositoryItemImageComboBox1.SmallImages = Me.ImageCollection1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "Iconos correcto e incorrecto 24x24px-01.png")
        Me.ImageCollection1.Images.SetKeyName(1, "Iconos correcto e incorrecto 24x24px-02.png")
        Me.ImageCollection1.Images.SetKeyName(2, "Iconos correcto e incorrecto 24x24px-03.png")
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Mensaje"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 638
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Registro"
        Me.GridColumn2.FieldName = "Item3"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 676
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLcgProgress})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1461, 574)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDetail
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 85)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1441, 469)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLcgProgress
        '
        Me.INDLcgProgress.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDLciProgressBar})
        Me.INDLcgProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProgress.Name = "INDLcgProgress"
        Me.INDLcgProgress.Size = New System.Drawing.Size(1441, 85)
        Me.INDLcgProgress.TextVisible = False
        Me.INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDTxtProgress
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 25)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(54, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1417, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLciProgressBar
        '
        Me.INDLciProgressBar.Control = Me.INDPbcProcess
        Me.INDLciProgressBar.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProgressBar.MaxSize = New System.Drawing.Size(0, 25)
        Me.INDLciProgressBar.MinSize = New System.Drawing.Size(54, 25)
        Me.INDLciProgressBar.Name = "INDLciProgressBar"
        Me.INDLciProgressBar.Size = New System.Drawing.Size(1417, 25)
        Me.INDLciProgressBar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProgressBar.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciProgressBar.TextVisible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'FrmLoadPlaneCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmLoadPlaneCollection"
        Me.Opacity = 1.0R
        Me.Tag = "1823"
        Me.Text = "FrmLoadPlaneCollection"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPbcProcess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProgressBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDPbcProcess As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents INDLciProgressBar As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents INDTxtProgress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDLcgProgress As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
