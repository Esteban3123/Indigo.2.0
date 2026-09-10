<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupPhysicalInventory
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PopupPhysicalInventory))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcPhysicalInventory = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPhysicalInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPresentation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBatchSerial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.ColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColExpirationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcPhysicalInventory)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'INDGcPhysicalInventory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPhysicalInventory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPhysicalInventory, Nothing)
        Me.INDGcPhysicalInventory.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPhysicalInventory, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPhysicalInventory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPhysicalInventory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPhysicalInventory, False)
        resources.ApplyResources(Me.INDGcPhysicalInventory, "INDGcPhysicalInventory")
        Me.INDGcPhysicalInventory.MainView = Me.INDGvPhysicalInventory
        Me.INDGcPhysicalInventory.Name = "INDGcPhysicalInventory"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPhysicalInventory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcPhysicalInventory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPhysicalInventory})
        '
        'INDGvPhysicalInventory
        '
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvPhysicalInventory.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPhysicalInventory.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvPhysicalInventory.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvPhysicalInventory.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPhysicalInventory.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvPhysicalInventory.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvPhysicalInventory.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPhysicalInventory.Appearance.Row.Font = CType(resources.GetObject("INDGvPhysicalInventory.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvPhysicalInventory.Appearance.Row.Options.UseFont = True
        Me.INDGvPhysicalInventory.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvPhysicalInventory.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvPhysicalInventory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPhysicalInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCode, Me.ColProduct, Me.ColPresentation, Me.ColBatchSerial, Me.ColExpirationDate, Me.ColQuantity})
        Me.INDGvPhysicalInventory.GridControl = Me.INDGcPhysicalInventory
        Me.INDGvPhysicalInventory.Name = "INDGvPhysicalInventory"
        Me.INDGvPhysicalInventory.OptionsCustomization.AllowGroup = False
        Me.INDGvPhysicalInventory.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvPhysicalInventory.OptionsDetail.ShowDetailTabs = False
        Me.INDGvPhysicalInventory.OptionsFind.AlwaysVisible = True
        Me.INDGvPhysicalInventory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPhysicalInventory.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPhysicalInventory.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPhysicalInventory.OptionsView.ShowDetailButtons = False
        Me.INDGvPhysicalInventory.OptionsView.ShowFooter = True
        Me.INDGvPhysicalInventory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPhysicalInventory, False)
        '
        'ColProduct
        '
        resources.ApplyResources(Me.ColProduct, "ColProduct")
        Me.ColProduct.FieldName = "ProductId.Name"
        Me.ColProduct.Name = "ColProduct"
        Me.ColProduct.OptionsColumn.AllowEdit = False
        Me.ColProduct.OptionsColumn.AllowFocus = False
        Me.ColProduct.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColProduct.OptionsColumn.AllowMove = False
        Me.ColProduct.OptionsColumn.AllowSize = False
        Me.ColProduct.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
        '
        'ColPresentation
        '
        resources.ApplyResources(Me.ColPresentation, "ColPresentation")
        Me.ColPresentation.FieldName = "ProductId.Presentation"
        Me.ColPresentation.Name = "ColPresentation"
        Me.ColPresentation.OptionsColumn.AllowEdit = False
        Me.ColPresentation.OptionsColumn.AllowFocus = False
        Me.ColPresentation.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColPresentation.OptionsColumn.AllowMove = False
        Me.ColPresentation.OptionsColumn.AllowSize = False
        '
        'ColBatchSerial
        '
        resources.ApplyResources(Me.ColBatchSerial, "ColBatchSerial")
        Me.ColBatchSerial.FieldName = "BatchSerialId.BatchCode"
        Me.ColBatchSerial.Name = "ColBatchSerial"
        Me.ColBatchSerial.OptionsColumn.AllowEdit = False
        Me.ColBatchSerial.OptionsColumn.AllowFocus = False
        Me.ColBatchSerial.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColBatchSerial.OptionsColumn.AllowMove = False
        Me.ColBatchSerial.OptionsColumn.AllowSize = False
        '
        'ColQuantity
        '
        resources.ApplyResources(Me.ColQuantity, "ColQuantity")
        Me.ColQuantity.FieldName = "Quantity"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.OptionsColumn.AllowEdit = False
        Me.ColQuantity.OptionsColumn.AllowFocus = False
        Me.ColQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColQuantity.OptionsColumn.AllowMove = False
        Me.ColQuantity.OptionsColumn.AllowSize = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1133, 544)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1113, 524)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcPhysicalInventory
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1089, 465)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(180, 120)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'ColCode
        '
        resources.ApplyResources(Me.ColCode, "ColCode")
        Me.ColCode.FieldName = "ProductId.Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.OptionsColumn.AllowEdit = False
        Me.ColCode.OptionsColumn.AllowFocus = False
        Me.ColCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColCode.OptionsColumn.AllowMove = False
        Me.ColCode.OptionsColumn.AllowSize = False
        '
        'ColExpirationDate
        '
        resources.ApplyResources(Me.ColExpirationDate, "ColExpirationDate")
        Me.ColExpirationDate.FieldName = "BatchSerialId.ExpirationDate"
        Me.ColExpirationDate.Name = "ColExpirationDate"
        Me.ColExpirationDate.OptionsColumn.AllowEdit = False
        Me.ColExpirationDate.OptionsColumn.AllowFocus = False
        Me.ColExpirationDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColExpirationDate.OptionsColumn.AllowMove = False
        Me.ColExpirationDate.OptionsColumn.AllowSize = False
        '
        'PopupPhysicalInventory
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "PopupPhysicalInventory"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDGcPhysicalInventory,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvPhysicalInventory,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcPhysicalInventory As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDGvPhysicalInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents ColProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBatchSerial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPresentation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColExpirationDate As DevExpress.XtraGrid.Columns.GridColumn
End Class
