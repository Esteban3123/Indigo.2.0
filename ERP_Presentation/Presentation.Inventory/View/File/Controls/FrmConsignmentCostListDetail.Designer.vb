<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmConsignmentCostListDetail
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConsignmentCostListDetail))
        Me.INDControlPanelBase = New DevExpress.XtraEditors.PanelControl()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.CtrBarraBotones2 = New Presentation.Controls.CtrBarraBotones()
        Me.INDSbSaveProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeCostNew = New DevExpress.XtraEditors.TextEdit()
        Me.INDSluProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNewCost = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.INDControlPanelBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDControlPanelBase.SuspendLayout()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDTeCostNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSluProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNewCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDControlPanelBase
        '
        Me.INDControlPanelBase.Controls.Add(Me.INDlcRoot)
        Me.INDControlPanelBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDControlPanelBase.Location = New System.Drawing.Point(0, 0)
        Me.INDControlPanelBase.Name = "INDControlPanelBase"
        Me.INDControlPanelBase.Size = New System.Drawing.Size(438, 619)
        Me.INDControlPanelBase.TabIndex = 1
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.CtrBarraBotones2)
        Me.INDlcRoot.Controls.Add(Me.INDSbSaveProduct)
        Me.INDlcRoot.Controls.Add(Me.INDTeCostNew)
        Me.INDlcRoot.Controls.Add(Me.INDSluProduct)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(2, 2)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(802, 422, 650, 400)
        Me.INDlcRoot.Root = Me.Root
        Me.INDlcRoot.Size = New System.Drawing.Size(434, 615)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'CtrBarraBotones2
        '
        Me.CtrBarraBotones2.ChangeMessageProgressBar = Nothing
        Me.CtrBarraBotones2.ClicBotonActualizar = False
        Me.CtrBarraBotones2.ColumnInfo = Nothing
        Me.CtrBarraBotones2.FilterDataSource = Nothing
        Me.CtrBarraBotones2.HomologationsCount = 0
        Me.CtrBarraBotones2.Huella = Nothing
        Me.CtrBarraBotones2.LegalBookId = 0
        Me.CtrBarraBotones2.ListOperatingUnit = Nothing
        Me.CtrBarraBotones2.Location = New System.Drawing.Point(12, 12)
        Me.CtrBarraBotones2.LyHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.CtrBarraBotones2.LySaveHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.CtrBarraBotones2.Name = "CtrBarraBotones2"
        Me.CtrBarraBotones2.OperatingUnitValue = 0
        Me.CtrBarraBotones2.OperatingUnitVisible = True
        Me.CtrBarraBotones2.PermissionsForm = Nothing
        Me.CtrBarraBotones2.PermiteConsultar = False
        Me.CtrBarraBotones2.PermiteGuardarResponsablePagoTercero = False
        Me.CtrBarraBotones2.ProgressBar = False
        Me.CtrBarraBotones2.Size = New System.Drawing.Size(410, 137)
        Me.CtrBarraBotones2.States = CType(resources.GetObject("CtrBarraBotones2.States"), System.Collections.Generic.List(Of Presentation.Controls.StatusRecord))
        Me.CtrBarraBotones2.StatesWhitActions = Nothing
        Me.CtrBarraBotones2.StatusRecord = Nothing
        Me.CtrBarraBotones2.StatusRecordEnabled = True
        Me.CtrBarraBotones2.StatusRecordVisible = False
        Me.CtrBarraBotones2.TabIndex = 7
        Me.CtrBarraBotones2.XtraLabelImage = Nothing
        Me.CtrBarraBotones2.XtraLabelText = "---"
        Me.CtrBarraBotones2.XtraLabelVisibility = False
        '
        'INDSbSaveProduct
        '
        Me.INDSbSaveProduct.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbSaveProduct.Appearance.Options.UseFont = True
        Me.INDSbSaveProduct.Location = New System.Drawing.Point(24, 555)
        Me.INDSbSaveProduct.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDSbSaveProduct.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDSbSaveProduct.Name = "INDSbSaveProduct"
        Me.INDSbSaveProduct.Size = New System.Drawing.Size(386, 36)
        Me.INDSbSaveProduct.StyleController = Me.INDlcRoot
        Me.INDSbSaveProduct.TabIndex = 6
        Me.INDSbSaveProduct.Text = "Agregar"
        '
        'INDTeCostNew
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeCostNew, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeCostNew, False)
        Me.INDTeCostNew.Location = New System.Drawing.Point(24, 266)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeCostNew, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeCostNew.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDTeCostNew.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDTeCostNew.Name = "INDTeCostNew"
        Me.INDTeCostNew.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTeCostNew.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCostNew.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTeCostNew.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeCostNew.Properties.Appearance.Options.UseFont = True
        Me.INDTeCostNew.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeCostNew.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeCostNew.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeCostNew.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeCostNew.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCostNew.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTeCostNew.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeCostNew.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeCostNew.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTeCostNew.Properties.EditFormat.FormatString = "c2"
        Me.INDTeCostNew.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDTeCostNew.Properties.Mask.EditMask = "n2"
        Me.INDTeCostNew.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeCostNew.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeCostNew.Properties.MaxLength = 12
        Me.INDTeCostNew.Size = New System.Drawing.Size(386, 28)
        Me.INDTeCostNew.StyleController = Me.INDlcRoot
        Me.INDTeCostNew.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeCostNew, 0)
        '
        'INDSluProduct
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSluProduct, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSluProduct, False)
        Me.INDSluProduct.Location = New System.Drawing.Point(24, 214)
        Me.IndigoTextEdit1.SetMascara(Me.INDSluProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSluProduct.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDSluProduct.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDSluProduct.Name = "INDSluProduct"
        Me.INDSluProduct.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSluProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSluProduct.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSluProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSluProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSluProduct.Properties.Appearance.Options.UseForeColor = True
        Me.INDSluProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSluProduct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSluProduct.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSluProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSluProduct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSluProduct.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSluProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSluProduct.Properties.DisplayMember = "CodeName"
        Me.INDSluProduct.Properties.NullText = ""
        Me.INDSluProduct.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSluProduct.Properties.ValueMember = "Id"
        Me.INDSluProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDSluProduct.StyleController = Me.INDlcRoot
        Me.INDSluProduct.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSluProduct, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMain, Me.LayoutControlItem2})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(434, 615)
        Me.Root.TextVisible = False
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProduct, Me.INDLciNewCost, Me.LayoutControlItem1, Me.EmptySpaceItem1})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 141)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.Size = New System.Drawing.Size(414, 454)
        Me.INDLcgMain.Text = "Datos Principales"
        '
        'INDLciProduct
        '
        Me.INDLciProduct.Control = Me.INDSluProduct
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(380, 52)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(380, 52)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.Size = New System.Drawing.Size(390, 52)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto"
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(80, 17)
        '
        'INDLciNewCost
        '
        Me.INDLciNewCost.Control = Me.INDTeCostNew
        Me.INDLciNewCost.Location = New System.Drawing.Point(0, 52)
        Me.INDLciNewCost.MaxSize = New System.Drawing.Size(0, 52)
        Me.INDLciNewCost.MinSize = New System.Drawing.Size(84, 52)
        Me.INDLciNewCost.Name = "INDLciNewCost"
        Me.INDLciNewCost.Size = New System.Drawing.Size(390, 52)
        Me.INDLciNewCost.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNewCost.Text = "Costo nuevo"
        Me.INDLciNewCost.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNewCost.TextSize = New System.Drawing.Size(80, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSbSaveProduct
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 361)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 40)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 104)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(390, 257)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.CtrBarraBotones2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(414, 141)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'FrmConsignmentCostListDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 619)
        Me.Controls.Add(Me.INDControlPanelBase)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmConsignmentCostListDetail"
        Me.Text = "Productos"
        CType(Me.INDControlPanelBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDControlPanelBase.ResumeLayout(False)
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDTeCostNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSluProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNewCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrBarraBotones1 As Controls.CtrBarraBotones
    Friend WithEvents INDControlPanelBase As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTeCostNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSluProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNewCost As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents INDSbSaveProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrBarraBotones2 As Controls.CtrBarraBotones
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
End Class
