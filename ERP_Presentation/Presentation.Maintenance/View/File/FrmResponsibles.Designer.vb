Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmResponsibles
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyResponsible = New DevExpress.XtraLayout.LayoutControl()
        Me.INDglCostCenter = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.INDColCodeCostCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameCostCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglBranch = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter2View = New Presentation.Controls.CustomGridView()
        Me.INDColCodeBranch = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameBranch = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDgleTypeEntailment = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleTypeResponsable = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrResponsible = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTypeEntailment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBranch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemResponsibleType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyResponsible.SuspendLayout()
        CType(Me.INDglCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglBranch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleTypeEntailment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleTypeResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTypeEntailment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemResponsibleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyResponsible)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(811, 361)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(811, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(811, 98)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(186, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbteCode.Properties.MaxLength = 15
        Me.INDbteCode.Size = New System.Drawing.Size(244, 28)
        Me.INDbteCode.StyleController = Me.INDlyResponsible
        Me.INDbteCode.TabIndex = 4
        Me.INDbteCode.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlyResponsible
        '
        Me.INDlyResponsible.AllowCustomization = False
        Me.INDlyResponsible.Controls.Add(Me.INDglCostCenter)
        Me.INDlyResponsible.Controls.Add(Me.INDglBranch)
        Me.INDlyResponsible.Controls.Add(Me.INDtxtName)
        Me.INDlyResponsible.Controls.Add(Me.INDbteCode)
        Me.INDlyResponsible.Controls.Add(Me.INDgleTypeEntailment)
        Me.INDlyResponsible.Controls.Add(Me.INDgleTypeResponsable)
        Me.INDlyResponsible.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyResponsible, False)
        Me.INDlyResponsible.Location = New System.Drawing.Point(202, 7)
        Me.INDlyResponsible.Name = "INDlyResponsible"
        Me.INDlyResponsible.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 250, 350)
        Me.INDlyResponsible.Root = Me.LayoutControlGroup1
        Me.INDlyResponsible.Size = New System.Drawing.Size(607, 352)
        Me.INDlyResponsible.TabIndex = 0
        Me.INDlyResponsible.Text = "LayoutControl1"
        '
        'INDglCostCenter
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDglCostCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglCostCenter, True)
        Me.INDglCostCenter.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDglCostCenter, False)
        Me.INDglCostCenter.Location = New System.Drawing.Point(186, 229)
        Me.IndigoTextEdit1.SetMascara(Me.INDglCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglCostCenter.Name = "INDglCostCenter"
        Me.INDglCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDglCostCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDglCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDglCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDglCostCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDglCostCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDglCostCenter.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDglCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglCostCenter.Properties.AutoComplete = False
        Me.INDglCostCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDglCostCenter.Properties.DisplayMember = "Name"
        Me.INDglCostCenter.Properties.ImmediatePopup = True
        Me.INDglCostCenter.Properties.NullText = ""
        Me.INDglCostCenter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglCostCenter.Properties.ValueMember = "Id"
        Me.INDglCostCenter.Properties.View = Me.CustomGridView1
        Me.INDglCostCenter.Size = New System.Drawing.Size(244, 28)
        Me.INDglCostCenter.StyleController = Me.INDlyResponsible
        Me.INDglCostCenter.TabIndex = 17
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDglCostCenter, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglCostCenter, 0)
        Me.INDglCostCenter.ToolTip = "Este Campo es Necesario"
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CustomGridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomGridView1.Appearance.GroupRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeCostCenter, Me.INDColNameCostCenter})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowAutoFilterRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CustomGridView1, False)
        '
        'INDColCodeCostCenter
        '
        Me.INDColCodeCostCenter.FieldName = "Code"
        Me.INDColCodeCostCenter.Name = "INDColCodeCostCenter"
        Me.INDColCodeCostCenter.Visible = True
        Me.INDColCodeCostCenter.VisibleIndex = 0
        '
        'INDColNameCostCenter
        '
        Me.INDColNameCostCenter.FieldName = "Name"
        Me.INDColNameCostCenter.Name = "INDColNameCostCenter"
        Me.INDColNameCostCenter.Visible = True
        Me.INDColNameCostCenter.VisibleIndex = 1
        '
        'INDglBranch
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDglBranch, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglBranch, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglBranch, True)
        Me.INDglBranch.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDglBranch, False)
        Me.INDglBranch.Location = New System.Drawing.Point(186, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDglBranch, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglBranch.Name = "INDglBranch"
        Me.INDglBranch.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDglBranch.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDglBranch.Properties.Appearance.Options.UseBackColor = True
        Me.INDglBranch.Properties.Appearance.Options.UseFont = True
        Me.INDglBranch.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDglBranch.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDglBranch.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDglBranch.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglBranch.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglBranch.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglBranch.Properties.AutoComplete = False
        Me.INDglBranch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDglBranch.Properties.DisplayMember = "Name"
        Me.INDglBranch.Properties.ImmediatePopup = True
        Me.INDglBranch.Properties.NullText = ""
        Me.INDglBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglBranch.Properties.ValueMember = "Id"
        Me.INDglBranch.Properties.View = Me.GridLookUpMultiFilter2View
        Me.INDglBranch.Size = New System.Drawing.Size(244, 28)
        Me.INDglBranch.StyleController = Me.INDlyResponsible
        Me.INDglBranch.TabIndex = 16
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDglBranch, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglBranch, 0)
        Me.INDglBranch.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpMultiFilter2View
        '
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeBranch, Me.INDColNameBranch})
        Me.GridLookUpMultiFilter2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter2View.Name = "GridLookUpMultiFilter2View"
        Me.GridLookUpMultiFilter2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpMultiFilter2View, False)
        '
        'INDColCodeBranch
        '
        Me.INDColCodeBranch.FieldName = "Code"
        Me.INDColCodeBranch.Name = "INDColCodeBranch"
        Me.INDColCodeBranch.Visible = True
        Me.INDColCodeBranch.VisibleIndex = 0
        '
        'INDColNameBranch
        '
        Me.INDColNameBranch.FieldName = "Name"
        Me.INDColNameBranch.Name = "INDColNameBranch"
        Me.INDColNameBranch.Visible = True
        Me.INDColNameBranch.VisibleIndex = 1
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(186, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDtxtName.Properties.MaxLength = 60
        Me.INDtxtName.Size = New System.Drawing.Size(243, 28)
        Me.INDtxtName.StyleController = Me.INDlyResponsible
        Me.INDtxtName.TabIndex = 5
        Me.INDtxtName.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDgleTypeEntailment
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleTypeEntailment, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleTypeEntailment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleTypeEntailment, True)
        Me.INDgleTypeEntailment.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleTypeEntailment, False)
        Me.INDgleTypeEntailment.Location = New System.Drawing.Point(186, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleTypeEntailment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleTypeEntailment.Name = "INDgleTypeEntailment"
        Me.INDgleTypeEntailment.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDgleTypeEntailment.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleTypeEntailment.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleTypeEntailment.Properties.Appearance.Options.UseFont = True
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleTypeEntailment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleTypeEntailment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleTypeEntailment.Properties.DisplayMember = "Item2"
        Me.INDgleTypeEntailment.Properties.ImmediatePopup = True
        Me.INDgleTypeEntailment.Properties.NullText = ""
        Me.INDgleTypeEntailment.Properties.PopupSizeable = False
        Me.INDgleTypeEntailment.Properties.ValueMember = "Item1"
        Me.INDgleTypeEntailment.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleTypeEntailment.Size = New System.Drawing.Size(244, 28)
        Me.INDgleTypeEntailment.StyleController = Me.INDlyResponsible
        Me.INDgleTypeEntailment.TabIndex = 13
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleTypeEntailment, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleTypeEntailment, 0)
        Me.INDgleTypeEntailment.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo De Vinculación"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'INDgleTypeResponsable
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleTypeResponsable, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleTypeResponsable, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleTypeResponsable, True)
        Me.INDgleTypeResponsable.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleTypeResponsable, False)
        Me.INDgleTypeResponsable.Location = New System.Drawing.Point(186, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleTypeResponsable, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleTypeResponsable.Name = "INDgleTypeResponsable"
        Me.INDgleTypeResponsable.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDgleTypeResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleTypeResponsable.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleTypeResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleTypeResponsable.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleTypeResponsable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleTypeResponsable.Properties.DisplayMember = "Item2"
        Me.INDgleTypeResponsable.Properties.ImmediatePopup = True
        Me.INDgleTypeResponsable.Properties.NullText = ""
        Me.INDgleTypeResponsable.Properties.PopupSizeable = False
        Me.INDgleTypeResponsable.Properties.ValueMember = "Item1"
        Me.INDgleTypeResponsable.Properties.View = Me.GridView1
        Me.INDgleTypeResponsable.Size = New System.Drawing.Size(244, 28)
        Me.INDgleTypeResponsable.StyleController = Me.INDlyResponsible
        Me.INDgleTypeResponsable.TabIndex = 14
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleTypeResponsable, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleTypeResponsable, 0)
        Me.INDgleTypeResponsable.ToolTip = "Este Campo es Necesario"
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tipo De Responsable"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrResponsible})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(607, 352)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrResponsible
        '
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrResponsible, False)
        Me.INDlyGrResponsible.CustomizationFormText = "Responsables"
        Me.INDlyGrResponsible.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemTypeEntailment, Me.INDlyItemBranch, Me.INDlyItemCostCenter, Me.INDlyItemResponsibleType})
        Me.INDlyGrResponsible.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrResponsible.Name = "INDlyGrResponsible"
        Me.INDlyGrResponsible.Size = New System.Drawing.Size(607, 352)
        Me.INDlyGrResponsible.Text = "Responsables"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.CustomizationFormText = "INDlyItemCode"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(583, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.Text = "Cédula"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "INDlyItemName"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(583, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemTypeEntailment
        '
        Me.INDlyItemTypeEntailment.Control = Me.INDgleTypeEntailment
        Me.INDlyItemTypeEntailment.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemTypeEntailment.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemTypeEntailment.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemTypeEntailment.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemTypeEntailment.Name = "INDlyItemTypeEntailment"
        Me.INDlyItemTypeEntailment.ShowInCustomizationForm = False
        Me.INDlyItemTypeEntailment.Size = New System.Drawing.Size(583, 36)
        Me.INDlyItemTypeEntailment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTypeEntailment.Tag = "TypeEntailment"
        Me.INDlyItemTypeEntailment.Text = "Tipo Vinculación"
        Me.INDlyItemTypeEntailment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTypeEntailment.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemTypeEntailment.TextToControlDistance = 12
        '
        'INDlyItemBranch
        '
        Me.INDlyItemBranch.Control = Me.INDglBranch
        Me.INDlyItemBranch.CustomizationFormText = "City"
        Me.INDlyItemBranch.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemBranch.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemBranch.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemBranch.Name = "INDlyItemBranch"
        Me.INDlyItemBranch.ShowInCustomizationForm = False
        Me.INDlyItemBranch.Size = New System.Drawing.Size(583, 36)
        Me.INDlyItemBranch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBranch.Tag = "IdBranch"
        Me.INDlyItemBranch.Text = "Sucursal"
        Me.INDlyItemBranch.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBranch.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemBranch.TextToControlDistance = 12
        '
        'INDlyItemCostCenter
        '
        Me.INDlyItemCostCenter.Control = Me.INDglCostCenter
        Me.INDlyItemCostCenter.CustomizationFormText = "INDlyItemCostCenter"
        Me.INDlyItemCostCenter.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemCostCenter.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCostCenter.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCostCenter.Name = "INDlyItemCostCenter"
        Me.INDlyItemCostCenter.ShowInCustomizationForm = False
        Me.INDlyItemCostCenter.Size = New System.Drawing.Size(583, 113)
        Me.INDlyItemCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCostCenter.Tag = "IdCostCenter"
        Me.INDlyItemCostCenter.Text = "Centro Costo"
        Me.INDlyItemCostCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCostCenter.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCostCenter.TextToControlDistance = 12
        '
        'INDlyItemResponsibleType
        '
        Me.INDlyItemResponsibleType.Control = Me.INDgleTypeResponsable
        Me.INDlyItemResponsibleType.CustomizationFormText = "INDlyItemResponsibleType"
        Me.INDlyItemResponsibleType.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemResponsibleType.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemResponsibleType.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemResponsibleType.Name = "INDlyItemResponsibleType"
        Me.INDlyItemResponsibleType.ShowInCustomizationForm = False
        Me.INDlyItemResponsibleType.Size = New System.Drawing.Size(583, 36)
        Me.INDlyItemResponsibleType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemResponsibleType.Tag = "TypeResponsible"
        Me.INDlyItemResponsibleType.Text = "Tipo Responsable"
        Me.INDlyItemResponsibleType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemResponsibleType.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemResponsibleType.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyResponsible
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 352)
        Me.CtrNavigationControl1.TabIndex = 1
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.MinWidth = 30
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 140
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.MinWidth = 30
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 730
        '
        'DepartmentCode
        '
        Me.DepartmentCode.Caption = "Código"
        Me.DepartmentCode.FieldName = "Code"
        Me.DepartmentCode.Name = "DepartmentCode"
        Me.DepartmentCode.Visible = True
        Me.DepartmentCode.VisibleIndex = 0
        Me.DepartmentCode.Width = 140
        '
        'DepartmentName
        '
        Me.DepartmentName.Caption = "Nombre"
        Me.DepartmentName.FieldName = "Name"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.Visible = True
        Me.DepartmentName.VisibleIndex = 1
        Me.DepartmentName.Width = 730
        '
        'FrmResponsibles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 483)
        Me.Name = "FrmResponsibles"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "571"
        Me.Text = "FrmResponsible"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyResponsible.ResumeLayout(False)
        CType(Me.INDglCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglBranch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleTypeEntailment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleTypeResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTypeEntailment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemResponsibleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyResponsible As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrResponsible As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDglBranch As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter2View As Presentation.Controls.CustomGridView
    Friend WithEvents DepartmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDlyItemBranch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDglCostCenter As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemTypeEntailment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemResponsibleType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDgleTypeEntailment As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgleTypeResponsable As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDColCodeCostCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameCostCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCodeBranch As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameBranch As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
End Class
