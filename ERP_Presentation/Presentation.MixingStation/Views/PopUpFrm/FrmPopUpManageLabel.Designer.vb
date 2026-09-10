Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopUpManageLabel
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopUpManageLabel))
        Me.INDGcItems = New DevExpress.XtraGrid.GridControl()
        Me.INDGvItems = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRequestType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDoseType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRequestQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMoreInfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPceMoreInfo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPccMoreInfo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrMoreInfoElaborationParameter1 = New Presentation.MixingStation.CtrMoreInfoElaborationParameter()
        Me.INDColLabelType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptLabelTypes = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiManageLabel = New DevExpress.XtraBars.BarButtonItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccMoreInfo.SuspendLayout()
        CType(Me.INDRptLabelTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDPccMoreInfo)
        Me.INDPanelControlBase.Controls.Add(Me.INDGcItems)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1252, 412)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1252, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1252, 130)
        '
        'INDGcItems
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcItems, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcItems, Nothing)
        Me.INDGcItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoGridControl1.SetExportButton(Me.INDGcItems, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcItems, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcItems, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcItems, False)
        Me.INDGcItems.Location = New System.Drawing.Point(2, 7)
        Me.INDGcItems.MainView = Me.INDGvItems
        Me.INDGcItems.MenuManager = Me.BarManager1
        Me.INDGcItems.Name = "INDGcItems"
        Me.INDGcItems.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptLabelTypes, Me.INDPceMoreInfo})
        Me.IndigoGridControl1.SetShowNewRecordButton(Me.INDGcItems, False)
        Me.INDGcItems.Size = New System.Drawing.Size(1248, 403)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcItems, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcItems.TabIndex = 0
        Me.INDGcItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvItems})
        '
        'INDGvItems
        '
        Me.INDGvItems.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvItems.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvItems.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvItems.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvItems.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvItems.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvItems.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvItems.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvItems.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvItems.Appearance.Row.Options.UseFont = True
        Me.INDGvItems.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvItems.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvItems.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColItem, Me.INDColRequestType, Me.INDColDoseType, Me.INDColRequestQuantity, Me.INDColMoreInfo, Me.INDColLabelType})
        Me.INDGvItems.GridControl = Me.INDGcItems
        Me.INDGvItems.Name = "INDGvItems"
        Me.INDGvItems.OptionsSelection.MultiSelect = True
        Me.INDGvItems.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvItems.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvItems.OptionsView.ShowAutoFilterRow = True
        Me.INDGvItems.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvItems, True)
        '
        'INDColItem
        '
        Me.INDColItem.Caption = "Item"
        Me.INDColItem.FieldName = "ItemCodeName"
        Me.INDColItem.Name = "INDColItem"
        Me.INDColItem.OptionsColumn.AllowEdit = False
        Me.INDColItem.OptionsColumn.AllowFocus = False
        Me.INDColItem.Visible = True
        Me.INDColItem.VisibleIndex = 0
        Me.INDColItem.Width = 485
        '
        'INDColRequestType
        '
        Me.INDColRequestType.Caption = "Tipo Solicitud"
        Me.INDColRequestType.FieldName = "RequestTypeName"
        Me.INDColRequestType.Name = "INDColRequestType"
        Me.INDColRequestType.OptionsColumn.AllowEdit = False
        Me.INDColRequestType.OptionsColumn.AllowFocus = False
        Me.INDColRequestType.Visible = True
        Me.INDColRequestType.VisibleIndex = 1
        Me.INDColRequestType.Width = 193
        '
        'INDColDoseType
        '
        Me.INDColDoseType.Caption = "Tipo de Dosis"
        Me.INDColDoseType.FieldName = "UnitDoseTypeCodeName"
        Me.INDColDoseType.Name = "INDColDoseType"
        Me.INDColDoseType.OptionsColumn.AllowEdit = False
        Me.INDColDoseType.OptionsColumn.AllowFocus = False
        Me.INDColDoseType.Visible = True
        Me.INDColDoseType.VisibleIndex = 2
        Me.INDColDoseType.Width = 165
        '
        'INDColRequestQuantity
        '
        Me.INDColRequestQuantity.Caption = "Cant. a Gestionar"
        Me.INDColRequestQuantity.FieldName = "ManageQuantity"
        Me.INDColRequestQuantity.Name = "INDColRequestQuantity"
        Me.INDColRequestQuantity.OptionsColumn.AllowEdit = False
        Me.INDColRequestQuantity.OptionsColumn.AllowFocus = False
        Me.INDColRequestQuantity.Visible = True
        Me.INDColRequestQuantity.VisibleIndex = 3
        Me.INDColRequestQuantity.Width = 108
        '
        'INDColMoreInfo
        '
        Me.INDColMoreInfo.Caption = "+ Info"
        Me.INDColMoreInfo.ColumnEdit = Me.INDPceMoreInfo
        Me.INDColMoreInfo.Name = "INDColMoreInfo"
        Me.INDColMoreInfo.OptionsColumn.AllowSize = False
        Me.INDColMoreInfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColMoreInfo.OptionsColumn.FixedWidth = True
        Me.INDColMoreInfo.Visible = True
        Me.INDColMoreInfo.VisibleIndex = 5
        Me.INDColMoreInfo.Width = 59
        '
        'INDPceMoreInfo
        '
        Me.INDPceMoreInfo.AutoHeight = False
        Me.INDPceMoreInfo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceMoreInfo.Name = "INDPceMoreInfo"
        Me.INDPceMoreInfo.PopupControl = Me.INDPccMoreInfo
        Me.INDPceMoreInfo.PopupSizeable = False
        Me.INDPceMoreInfo.ShowPopupCloseButton = False
        Me.INDPceMoreInfo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPccMoreInfo
        '
        Me.INDPccMoreInfo.Controls.Add(Me.CtrMoreInfoElaborationParameter1)
        Me.INDPccMoreInfo.Location = New System.Drawing.Point(162, 97)
        Me.INDPccMoreInfo.Name = "INDPccMoreInfo"
        Me.INDPccMoreInfo.Size = New System.Drawing.Size(782, 313)
        Me.INDPccMoreInfo.TabIndex = 11
        '
        'CtrMoreInfoElaborationParameter1
        '
        Me.CtrMoreInfoElaborationParameter1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrMoreInfoElaborationParameter1.ElaborationParameters = Nothing
        Me.CtrMoreInfoElaborationParameter1.Location = New System.Drawing.Point(0, 0)
        Me.CtrMoreInfoElaborationParameter1.Name = "CtrMoreInfoElaborationParameter1"
        Me.CtrMoreInfoElaborationParameter1.PhysicoChemicalParameters = Nothing
        Me.CtrMoreInfoElaborationParameter1.Size = New System.Drawing.Size(782, 313)
        Me.CtrMoreInfoElaborationParameter1.TabIndex = 0
        '
        'INDColLabelType
        '
        Me.INDColLabelType.Caption = "Tipo de Etiqueta"
        Me.INDColLabelType.ColumnEdit = Me.INDRptLabelTypes
        Me.INDColLabelType.FieldName = "LabelType"
        Me.INDColLabelType.Name = "INDColLabelType"
        Me.INDColLabelType.Visible = True
        Me.INDColLabelType.VisibleIndex = 4
        Me.INDColLabelType.Width = 213
        '
        'INDRptLabelTypes
        '
        Me.INDRptLabelTypes.AutoHeight = False
        Me.INDRptLabelTypes.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptLabelTypes.DisplayMember = "Item2"
        Me.INDRptLabelTypes.Name = "INDRptLabelTypes"
        Me.INDRptLabelTypes.NullText = ""
        Me.INDRptLabelTypes.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.INDRptLabelTypes.ValueMember = "Item1"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemGridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Etiqueta"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiManageLabel})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1252, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 547)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1252, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 542)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1252, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 542)
        '
        'INDBbiManageLabel
        '
        Me.INDBbiManageLabel.Caption = "Asignar Etiqueta"
        Me.INDBbiManageLabel.Id = 0
        Me.INDBbiManageLabel.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_24x24_blue
        Me.INDBbiManageLabel.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBbiManageLabel.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiManageLabel.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBbiManageLabel.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiManageLabel.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBbiManageLabel.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiManageLabel.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBbiManageLabel.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiManageLabel.Name = "INDBbiManageLabel"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiManageLabel)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'Bar1
        '
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.Text = "Custom 2"
        '
        'FrmPopUpManageLabel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1252, 547)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("FrmPopUpManageLabel.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpManageLabel"
        Me.Opacity = 1.0R
        Me.Text = "Gestionar Etiquetas"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccMoreInfo.ResumeLayout(False)
        CType(Me.INDRptLabelTypes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDGcItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDColItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRequestType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDoseType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRequestQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMoreInfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLabelType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptLabelTypes As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents INDPceMoreInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPccMoreInfo As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrMoreInfoElaborationParameter1 As CtrMoreInfoElaborationParameter
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiManageLabel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
End Class
