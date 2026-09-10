Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRecognition
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRecognition))
        Me.INDGcRecognition = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRecognition = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rptItemImageComboBoxState = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImgStates = New DevExpress.Utils.ImageCollection(Me.components)
        Me.ColCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNumFolios = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcRecognition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRecognition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rptItemImageComboBoxState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgStates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1116, 347)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1116, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1116, 98)
        '
        'INDGcRecognition
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRecognition, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRecognition, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRecognition, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRecognition, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRecognition, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRecognition, False)
        Me.INDGcRecognition.Location = New System.Drawing.Point(5, 5)
        Me.INDGcRecognition.MainView = Me.INDGvRecognition
        Me.INDGcRecognition.Name = "INDGcRecognition"
        Me.INDGcRecognition.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rptItemImageComboBoxState})
        Me.INDGcRecognition.Size = New System.Drawing.Size(1102, 328)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRecognition, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRecognition.TabIndex = 0
        Me.INDGcRecognition.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRecognition})
        '
        'INDGvRecognition
        '
        Me.INDGvRecognition.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRecognition.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRecognition.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRecognition.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRecognition.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRecognition.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRecognition.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRecognition.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRecognition.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRecognition.Appearance.Row.Options.UseFont = True
        Me.INDGvRecognition.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRecognition.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRecognition.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColState, Me.ColCareGroup, Me.ColValue, Me.ColNumFolios})
        Me.INDGvRecognition.GridControl = Me.INDGcRecognition
        Me.INDGvRecognition.Name = "INDGvRecognition"
        Me.INDGvRecognition.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRecognition.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRecognition.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRecognition.OptionsView.ShowFooter = True
        Me.INDGvRecognition.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRecognition, False)
        '
        'ColState
        '
        Me.ColState.Caption = " "
        Me.ColState.ColumnEdit = Me.rptItemImageComboBoxState
        Me.ColState.FieldName = "StateOperation"
        Me.ColState.Name = "ColState"
        Me.ColState.OptionsColumn.AllowEdit = False
        Me.ColState.OptionsColumn.AllowFocus = False
        Me.ColState.OptionsColumn.AllowMove = False
        Me.ColState.OptionsColumn.AllowSize = False
        Me.ColState.OptionsColumn.FixedWidth = True
        Me.ColState.Visible = True
        Me.ColState.VisibleIndex = 0
        Me.ColState.Width = 26
        '
        'rptItemImageComboBoxState
        '
        Me.rptItemImageComboBoxState.Appearance.Options.UseTextOptions = True
        Me.rptItemImageComboBoxState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rptItemImageComboBoxState.AutoHeight = False
        Me.rptItemImageComboBoxState.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(1, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(2, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(3, Byte), 2)})
        Me.rptItemImageComboBoxState.LargeImages = Me.ImgStates
        Me.rptItemImageComboBoxState.Name = "rptItemImageComboBoxState"
        Me.rptItemImageComboBoxState.SmallImages = Me.ImgStates
        '
        'ImgStates
        '
        Me.ImgStates.ImageStream = CType(resources.GetObject("ImgStates.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImgStates.Images.SetKeyName(0, "Acceptance.png")
        Me.ImgStates.Images.SetKeyName(1, "carga.gif")
        Me.ImgStates.Images.SetKeyName(2, "cancelar16x16.png")
        '
        'ColCareGroup
        '
        Me.ColCareGroup.Caption = "Grupo de Atención"
        Me.ColCareGroup.FieldName = "CareGroupCodeName"
        Me.ColCareGroup.Name = "ColCareGroup"
        Me.ColCareGroup.OptionsColumn.AllowEdit = False
        Me.ColCareGroup.OptionsColumn.AllowFocus = False
        Me.ColCareGroup.Visible = True
        Me.ColCareGroup.VisibleIndex = 1
        Me.ColCareGroup.Width = 370
        '
        'ColValue
        '
        Me.ColValue.Caption = "Valor"
        Me.ColValue.DisplayFormat.FormatString = "C0"
        Me.ColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColValue.FieldName = "TotalCareGroup"
        Me.ColValue.Name = "ColValue"
        Me.ColValue.OptionsColumn.AllowEdit = False
        Me.ColValue.OptionsColumn.AllowFocus = False
        Me.ColValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCareGroup", "Total {0:C0}")})
        Me.ColValue.Visible = True
        Me.ColValue.VisibleIndex = 2
        Me.ColValue.Width = 211
        '
        'ColNumFolios
        '
        Me.ColNumFolios.Caption = "# Folios"
        Me.ColNumFolios.FieldName = "FolioQuantity"
        Me.ColNumFolios.Name = "ColNumFolios"
        Me.ColNumFolios.OptionsColumn.AllowEdit = False
        Me.ColNumFolios.OptionsColumn.AllowFocus = False
        Me.ColNumFolios.Visible = True
        Me.ColNumFolios.VisibleIndex = 3
        Me.ColNumFolios.Width = 89
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDGcRecognition)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(1112, 338)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1112, 338)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcRecognition
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1112, 338)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmRecognition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 469)
        Me.Name = "FrmRecognition"
        Me.Opacity = 1.0R
        Me.Tag = "750"
        Me.Text = "Reconocimiento de Ingresos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcRecognition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRecognition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rptItemImageComboBoxState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgStates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDGcRecognition As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRecognition As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents ColCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNumFolios As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rptItemImageComboBoxState As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImgStates As DevExpress.Utils.ImageCollection
End Class
