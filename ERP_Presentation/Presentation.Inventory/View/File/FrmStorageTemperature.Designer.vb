Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmStorageTemperature
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDName = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeFrom = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeUntil = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSleTemperatureUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeDescription = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliFrom = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliUntil = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliTemperatureUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeUntil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTemperatureUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliUntil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliTemperatureUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1296, 588)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(4)
        Me.ToolBars.Size = New System.Drawing.Size(1296, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(6)
        Me.BarraBotones.Size = New System.Drawing.Size(1296, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 9)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 577)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDbtnCode)
        Me.INDlyRoot.Controls.Add(Me.INDName)
        Me.INDlyRoot.Controls.Add(Me.INDSeFrom)
        Me.INDlyRoot.Controls.Add(Me.INDSeUntil)
        Me.INDlyRoot.Controls.Add(Me.INDSleTemperatureUnit)
        Me.INDlyRoot.Controls.Add(Me.INDSeDescription)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 9)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1270, 475, 650, 400)
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1092, 577)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        Me.INDbtnCode.Location = New System.Drawing.Point(26, 77)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(382, 28)
        Me.INDbtnCode.StyleController = Me.INDlyRoot
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDName, False)
        Me.INDName.Location = New System.Drawing.Point(26, 137)
        Me.IndigoTextEdit1.SetMascara(Me.INDName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDName.Name = "INDName"
        Me.INDName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDName.Properties.Appearance.Options.UseBackColor = True
        Me.INDName.Properties.Appearance.Options.UseFont = True
        Me.INDName.Properties.Appearance.Options.UseForeColor = True
        Me.INDName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDName.Size = New System.Drawing.Size(382, 28)
        Me.INDName.StyleController = Me.INDlyRoot
        Me.INDName.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDName, 0)
        '
        'INDSeFrom
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeFrom, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeFrom, False)
        Me.INDSeFrom.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeFrom.Location = New System.Drawing.Point(26, 197)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeFrom, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeFrom.Name = "INDSeFrom"
        Me.INDSeFrom.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeFrom.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSeFrom.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeFrom.Properties.Appearance.Options.UseFont = True
        Me.INDSeFrom.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeFrom.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeFrom.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeFrom.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeFrom.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeFrom.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeFrom.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeFrom.Properties.IsFloatValue = False
        Me.INDSeFrom.Properties.Mask.EditMask = "d"
        Me.INDSeFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeFrom.Properties.MaxValue = New Decimal(New Integer() {999, 0, 0, 0})
        Me.INDSeFrom.Properties.MinValue = New Decimal(New Integer() {999, 0, 0, -2147483648})
        Me.INDSeFrom.Size = New System.Drawing.Size(382, 28)
        Me.INDSeFrom.StyleController = Me.INDlyRoot
        Me.INDSeFrom.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeFrom, 0)
        '
        'INDSeUntil
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeUntil, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeUntil, False)
        Me.INDSeUntil.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeUntil.Location = New System.Drawing.Point(26, 257)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeUntil, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeUntil.Name = "INDSeUntil"
        Me.INDSeUntil.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeUntil.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSeUntil.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeUntil.Properties.Appearance.Options.UseFont = True
        Me.INDSeUntil.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeUntil.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeUntil.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeUntil.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeUntil.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeUntil.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeUntil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeUntil.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeUntil.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeUntil.Properties.IsFloatValue = False
        Me.INDSeUntil.Properties.Mask.EditMask = "d"
        Me.INDSeUntil.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeUntil.Properties.MaxValue = New Decimal(New Integer() {999, 0, 0, 0})
        Me.INDSeUntil.Properties.MinValue = New Decimal(New Integer() {999, 0, 0, -2147483648})
        Me.INDSeUntil.Size = New System.Drawing.Size(382, 28)
        Me.INDSeUntil.StyleController = Me.INDlyRoot
        Me.INDSeUntil.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeUntil, 0)
        '
        'INDSleTemperatureUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleTemperatureUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleTemperatureUnit, False)
        Me.INDSleTemperatureUnit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSleTemperatureUnit.Location = New System.Drawing.Point(26, 317)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleTemperatureUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleTemperatureUnit.Name = "INDSleTemperatureUnit"
        Me.INDSleTemperatureUnit.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleTemperatureUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTemperatureUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTemperatureUnit.Properties.Appearance.Options.UseFont = True
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleTemperatureUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleTemperatureUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTemperatureUnit.Properties.DisplayMember = "Item2"
        Me.INDSleTemperatureUnit.Properties.NullText = ""
        Me.INDSleTemperatureUnit.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleTemperatureUnit.Properties.ValueMember = "Item1"
        Me.INDSleTemperatureUnit.Size = New System.Drawing.Size(382, 28)
        Me.INDSleTemperatureUnit.StyleController = Me.INDlyRoot
        Me.INDSleTemperatureUnit.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleTemperatureUnit, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit1View.DetailHeight = 239
        Me.SearchLookUpEdit1View.FixedLineWidth = 1
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Unidad"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.MinWidth = 13
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 50
        '
        'INDSeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeDescription, False)
        Me.INDSeDescription.EditValue = ""
        Me.INDSeDescription.Location = New System.Drawing.Point(26, 377)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeDescription.Name = "INDSeDescription"
        Me.INDSeDescription.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDSeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeDescription.Size = New System.Drawing.Size(382, 28)
        Me.INDSeDescription.StyleController = Me.INDlyRoot
        Me.INDSeDescription.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeDescription, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1092, 577)
        Me.Root.TextVisible = False
        '
        'INDlygPrincipalInformation
        '
        Me.INDlygPrincipalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalInformation, False)
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDliName, Me.INDliFrom, Me.INDliUntil, Me.INDliTemperatureUnit, Me.INDliDescription})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(1072, 557)
        Me.INDlygPrincipalInformation.Text = "Rangos de temperatura"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDlyItemCode.Size = New System.Drawing.Size(1048, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(45, 17)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDliName
        '
        Me.INDliName.AllowHide = False
        Me.INDliName.Control = Me.INDName
        Me.INDliName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliName.CustomizationFormText = "Nombre del rango"
        Me.INDliName.Location = New System.Drawing.Point(0, 60)
        Me.INDliName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliName.Name = "INDliName"
        Me.INDliName.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDliName.ShowInCustomizationForm = False
        Me.INDliName.Size = New System.Drawing.Size(1048, 60)
        Me.INDliName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliName.Text = "Nombre del rango"
        Me.INDliName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliName.TextSize = New System.Drawing.Size(117, 17)
        Me.INDliName.TextToControlDistance = 5
        '
        'INDliFrom
        '
        Me.INDliFrom.Control = Me.INDSeFrom
        Me.INDliFrom.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliFrom.CustomizationFormText = "Desde"
        Me.INDliFrom.Location = New System.Drawing.Point(0, 120)
        Me.INDliFrom.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliFrom.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliFrom.Name = "INDliFrom"
        Me.INDliFrom.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDliFrom.Size = New System.Drawing.Size(1048, 60)
        Me.INDliFrom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliFrom.Text = "Desde"
        Me.INDliFrom.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliFrom.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliFrom.TextSize = New System.Drawing.Size(40, 17)
        Me.INDliFrom.TextToControlDistance = 5
        '
        'INDliUntil
        '
        Me.INDliUntil.Control = Me.INDSeUntil
        Me.INDliUntil.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliUntil.CustomizationFormText = "Hasta"
        Me.INDliUntil.Location = New System.Drawing.Point(0, 180)
        Me.INDliUntil.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliUntil.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliUntil.Name = "INDliUntil"
        Me.INDliUntil.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDliUntil.Size = New System.Drawing.Size(1048, 60)
        Me.INDliUntil.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliUntil.Text = "Hasta"
        Me.INDliUntil.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliUntil.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliUntil.TextSize = New System.Drawing.Size(35, 17)
        Me.INDliUntil.TextToControlDistance = 5
        '
        'INDliTemperatureUnit
        '
        Me.INDliTemperatureUnit.Control = Me.INDSleTemperatureUnit
        Me.INDliTemperatureUnit.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliTemperatureUnit.CustomizationFormText = "Unidad de temperatura"
        Me.INDliTemperatureUnit.Location = New System.Drawing.Point(0, 240)
        Me.INDliTemperatureUnit.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliTemperatureUnit.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliTemperatureUnit.Name = "INDliTemperatureUnit"
        Me.INDliTemperatureUnit.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDliTemperatureUnit.Size = New System.Drawing.Size(1048, 60)
        Me.INDliTemperatureUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliTemperatureUnit.Text = "Unidad de temperatura"
        Me.INDliTemperatureUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliTemperatureUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliTemperatureUnit.TextSize = New System.Drawing.Size(148, 17)
        Me.INDliTemperatureUnit.TextToControlDistance = 5
        '
        'INDliDescription
        '
        Me.INDliDescription.AllowHide = False
        Me.INDliDescription.Control = Me.INDSeDescription
        Me.INDliDescription.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliDescription.CustomizationFormText = "Visualizacion"
        Me.INDliDescription.Location = New System.Drawing.Point(0, 300)
        Me.INDliDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.Name = "INDliDescription"
        Me.INDliDescription.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.INDliDescription.ShowInCustomizationForm = False
        Me.INDliDescription.Size = New System.Drawing.Size(1048, 204)
        Me.INDliDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDescription.Text = "Visualizacion"
        Me.INDliDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDescription.TextSize = New System.Drawing.Size(83, 17)
        Me.INDliDescription.TextToControlDistance = 5
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmStorageTemperature
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 725)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmStorageTemperature"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "2846"
        Me.Text = "Temperatura de almacenamiento"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeUntil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTemperatureUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliUntil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliTemperatureUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteStabilityDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemStabilityDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliFrom As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeFrom As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSeUntil As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDliUntil As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliTemperatureUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleTemperatureUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
