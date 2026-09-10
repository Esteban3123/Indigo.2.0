<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseOrderImport
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcProduct = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGclState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptCheActivate = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLciAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPncAccept = New DevExpress.XtraEditors.PanelControl()
        Me.INDSmbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PopupContainerEdit1 = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDSpeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGcProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRptCheActivate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciAccept,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDPncAccept,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPncAccept.SuspendLayout
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupContainerEdit1.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupContainerControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.PopupContainerControl1.SuspendLayout
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(660, 538)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProduct, Me.INDLciAccept})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(640, 494)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLciProduct
        '
        Me.INDLciProduct.AllowHide = False
        Me.INDLciProduct.Control = Me.INDGcProduct
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.ShowInCustomizationForm = False
        Me.INDLciProduct.Size = New System.Drawing.Size(616, 434)
        Me.INDLciProduct.Text = "Solicitudes de compra"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'INDGcProduct
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProduct, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProduct, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProduct, False)
        Me.INDGcProduct.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProduct, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProduct, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProduct, False)
        Me.INDGcProduct.Location = New System.Drawing.Point(24, 49)
        Me.INDGcProduct.MainView = Me.INDGvProduct
        Me.INDGcProduct.Name = "INDGcProduct"
        Me.INDGcProduct.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptCheActivate})
        Me.INDGcProduct.Size = New System.Drawing.Size(612, 405)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProduct, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcProduct.TabIndex = 4
        Me.INDGcProduct.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProduct})
        '
        'INDGvProduct
        '
        Me.INDGvProduct.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProduct.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGclState, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.INDGvProduct.GridControl = Me.INDGcProduct
        Me.INDGvProduct.Name = "INDGvProduct"
        Me.INDGvProduct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct, False)
        '
        'INDGclState
        '
        Me.INDGclState.Caption = "Sel. "
        Me.INDGclState.ColumnEdit = Me.INDRptCheActivate
        Me.INDGclState.FieldName = "Activated"
        Me.INDGclState.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDGclState.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        Me.INDGclState.Name = "INDGclState"
        Me.INDGclState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMove = False
        Me.INDGclState.OptionsColumn.AllowShowHide = False
        Me.INDGclState.OptionsColumn.AllowSize = False
        Me.INDGclState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.FixedWidth = True
        Me.INDGclState.OptionsFilter.AllowAutoFilter = False
        Me.INDGclState.OptionsFilter.AllowFilter = False
        Me.INDGclState.Visible = True
        Me.INDGclState.VisibleIndex = 0
        Me.INDGclState.Width = 25
        '
        'INDRptCheActivate
        '
        Me.INDRptCheActivate.AutoHeight = False
        Me.INDRptCheActivate.Name = "INDRptCheActivate"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "N° Documento"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Producto"
        Me.GridColumn2.FieldName = "DescriptionProduct"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 308
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha"
        Me.GridColumn3.FieldName = "DocumentDate"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 86
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cantidad"
        Me.GridColumn4.FieldName = "OutstandingQuantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        '
        'INDLciAccept
        '
        Me.INDLciAccept.AllowHide = false
        Me.INDLciAccept.Control = Me.INDPncAccept
        Me.INDLciAccept.Location = New System.Drawing.Point(0, 434)
        Me.INDLciAccept.Name = "INDLciAccept"
        Me.INDLciAccept.ShowInCustomizationForm = false
        Me.INDLciAccept.Size = New System.Drawing.Size(616, 36)
        Me.INDLciAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAccept.TextVisible = false
        '
        'INDPncAccept
        '
        Me.INDPncAccept.Controls.Add(Me.INDSmbAccept)
        Me.INDPncAccept.Location = New System.Drawing.Point(24, 458)
        Me.INDPncAccept.MinimumSize = New System.Drawing.Size(0, 30)
        Me.INDPncAccept.Name = "INDPncAccept"
        Me.INDPncAccept.Size = New System.Drawing.Size(612, 32)
        Me.INDPncAccept.TabIndex = 5
        '
        'INDSmbAccept
        '
        Me.INDSmbAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSmbAccept.Appearance.Options.UseFont = true
        Me.INDSmbAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDSmbAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAccept, true)
        Me.INDSmbAccept.Name = "INDSmbAccept"
        Me.INDSmbAccept.Size = New System.Drawing.Size(608, 28)
        Me.INDSmbAccept.TabIndex = 0
        Me.INDSmbAccept.Text = "Aceptar"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PopupContainerEdit1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 494)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(640, 24)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = false
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'PopupContainerEdit1
        '
        Me.PopupContainerEdit1.Location = New System.Drawing.Point(12, 506)
        Me.PopupContainerEdit1.Name = "PopupContainerEdit1"
        Me.PopupContainerEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PopupContainerEdit1.Properties.PopupControl = Me.PopupContainerControl1
        Me.PopupContainerEdit1.Size = New System.Drawing.Size(636, 20)
        Me.PopupContainerEdit1.StyleController = Me.LayoutControl1
        Me.PopupContainerEdit1.TabIndex = 8
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.INDSpeQuantity)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(276, 189)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(277, 25)
        Me.PopupContainerControl1.TabIndex = 7
        '
        'INDSpeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpeQuantity, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpeQuantity, true)
        Me.INDSpeQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSpeQuantity.Location = New System.Drawing.Point(-110, -3)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpeQuantity, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDSpeQuantity.Name = "INDSpeQuantity"
        Me.INDSpeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSpeQuantity.Properties.Appearance.Options.UseBackColor = true
        Me.INDSpeQuantity.Properties.Appearance.Options.UseFont = true
        Me.INDSpeQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSpeQuantity.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDSpeQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpeQuantity.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDSpeQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpeQuantity.Properties.MaxLength = 9
        Me.INDSpeQuantity.Properties.MaxValue = New Decimal(New Integer() {2147483646, 0, 0, 0})
        Me.INDSpeQuantity.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSpeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpeQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpeQuantity, 0)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PopupContainerEdit1)
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.INDPncAccept)
        Me.LayoutControl1.Controls.Add(Me.INDGcProduct)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(660, 538)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'FrmPurchaseOrderImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 538)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmPurchaseOrderImport"
        Me.Text = "Importar Información"
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGcProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRptCheActivate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciAccept,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDPncAccept,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPncAccept.ResumeLayout(false)
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupContainerEdit1.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupContainerControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PopupContainerControl1.ResumeLayout(false)
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcProduct As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPncAccept As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSmbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents INDLciAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGclState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents INDRptCheActivate As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSpeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupContainerEdit1 As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
End Class
