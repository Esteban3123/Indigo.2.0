Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupResumptionHoliday
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
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyResumption = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdteEntryDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDdteEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDdteInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtRequestedDays = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygResumption = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEntryDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRequestedDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyResumption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyResumption.SuspendLayout()
        CType(Me.INDdteEntryDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEntryDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRequestedDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygResumption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEntryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequestedDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyResumption)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(665, 370)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(665, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(665, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyResumption
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 361)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyResumption
        '
        Me.INDlyResumption.Controls.Add(Me.INDdteEntryDate)
        Me.INDlyResumption.Controls.Add(Me.INDdteEndDate)
        Me.INDlyResumption.Controls.Add(Me.INDdteInitialDate)
        Me.INDlyResumption.Controls.Add(Me.INDtxtRequestedDays)
        Me.INDlyResumption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyResumption.Location = New System.Drawing.Point(202, 7)
        Me.INDlyResumption.Name = "INDlyResumption"
        Me.INDlyResumption.Root = Me.LayoutControlGroup1
        Me.INDlyResumption.Size = New System.Drawing.Size(461, 361)
        Me.INDlyResumption.TabIndex = 1
        Me.INDlyResumption.Text = "LayoutControl1"
        '
        'INDdteEntryDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteEntryDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteEntryDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteEntryDate, False)
        Me.INDdteEntryDate.EditValue = Nothing
        Me.INDdteEntryDate.EnterMoveNextControl = True
        Me.INDdteEntryDate.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteEntryDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteEntryDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteEntryDate.Name = "INDdteEntryDate"
        Me.INDdteEntryDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteEntryDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEntryDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteEntryDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteEntryDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEntryDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteEntryDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEntryDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEntryDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteEntryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteEntryDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteEntryDate.Properties.ReadOnly = True
        Me.INDdteEntryDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteEntryDate.StyleController = Me.INDlyResumption
        Me.INDdteEntryDate.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteEntryDate, 0)
        '
        'INDdteEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteEndDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteEndDate, False)
        Me.INDdteEndDate.EditValue = Nothing
        Me.INDdteEndDate.EnterMoveNextControl = True
        Me.INDdteEndDate.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteEndDate.Name = "INDdteEndDate"
        Me.INDdteEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteEndDate.Properties.ReadOnly = True
        Me.INDdteEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteEndDate.StyleController = Me.INDlyResumption
        Me.INDdteEndDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteEndDate, 0)
        '
        'INDdteInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteInitialDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteInitialDate, False)
        Me.INDdteInitialDate.EditValue = Nothing
        Me.INDdteInitialDate.EnterMoveNextControl = True
        Me.INDdteInitialDate.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteInitialDate.Name = "INDdteInitialDate"
        Me.INDdteInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteInitialDate.StyleController = Me.INDlyResumption
        Me.INDdteInitialDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteInitialDate, 0)
        '
        'INDtxtRequestedDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRequestedDays, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRequestedDays, False)
        Me.INDtxtRequestedDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtRequestedDays.EnterMoveNextControl = True
        Me.INDtxtRequestedDays.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRequestedDays, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDtxtRequestedDays.Name = "INDtxtRequestedDays"
        Me.INDtxtRequestedDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtRequestedDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRequestedDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRequestedDays.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRequestedDays.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRequestedDays.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRequestedDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtxtRequestedDays.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtRequestedDays.Properties.Mask.EditMask = "[0-9]+"
        Me.INDtxtRequestedDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtRequestedDays.Properties.MaxValue = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.INDtxtRequestedDays.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtRequestedDays.StyleController = Me.INDlyResumption
        Me.INDtxtRequestedDays.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRequestedDays, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygResumption})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(461, 361)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygResumption
        '
        Me.INDlygResumption.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygResumption.AppearanceGroup.Options.UseFont = True
        Me.INDlygResumption.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygResumption.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygResumption.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygResumption.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygResumption.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygResumption.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygResumption.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygResumption.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygResumption.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygResumption.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygResumption.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygResumption.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygResumption, False)
        Me.INDlygResumption.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInitialDate, Me.INDlyItemEndDate, Me.INDlyItemEntryDate, Me.INDlyItemRequestedDays})
        Me.INDlygResumption.Location = New System.Drawing.Point(0, 0)
        Me.INDlygResumption.Name = "INDlygResumption"
        Me.INDlygResumption.Size = New System.Drawing.Size(441, 341)
        Me.INDlygResumption.Text = "Datos Principales"
        '
        'INDlyItemInitialDate
        '
        Me.INDlyItemInitialDate.Control = Me.INDdteInitialDate
        Me.INDlyItemInitialDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemInitialDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialDate.Name = "INDlyItemInitialDate"
        Me.INDlyItemInitialDate.ShowInCustomizationForm = False
        Me.INDlyItemInitialDate.Size = New System.Drawing.Size(417, 60)
        Me.INDlyItemInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialDate.Text = "Fecha Inicio"
        Me.INDlyItemInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInitialDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitialDate.TextToControlDistance = 5
        '
        'INDlyItemEndDate
        '
        Me.INDlyItemEndDate.Control = Me.INDdteEndDate
        Me.INDlyItemEndDate.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemEndDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndDate.Name = "INDlyItemEndDate"
        Me.INDlyItemEndDate.ShowInCustomizationForm = False
        Me.INDlyItemEndDate.Size = New System.Drawing.Size(417, 60)
        Me.INDlyItemEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndDate.Text = "Fecha Fin"
        Me.INDlyItemEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEndDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEndDate.TextToControlDistance = 5
        '
        'INDlyItemEntryDate
        '
        Me.INDlyItemEntryDate.Control = Me.INDdteEntryDate
        Me.INDlyItemEntryDate.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemEntryDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEntryDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEntryDate.Name = "INDlyItemEntryDate"
        Me.INDlyItemEntryDate.ShowInCustomizationForm = False
        Me.INDlyItemEntryDate.Size = New System.Drawing.Size(417, 102)
        Me.INDlyItemEntryDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEntryDate.Text = "Fecha Ingreso"
        Me.INDlyItemEntryDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEntryDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEntryDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEntryDate.TextToControlDistance = 5
        '
        'INDlyItemRequestedDays
        '
        Me.INDlyItemRequestedDays.Control = Me.INDtxtRequestedDays
        Me.INDlyItemRequestedDays.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequestedDays.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRequestedDays.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRequestedDays.Name = "INDlyItemRequestedDays"
        Me.INDlyItemRequestedDays.ShowInCustomizationForm = False
        Me.INDlyItemRequestedDays.Size = New System.Drawing.Size(417, 60)
        Me.INDlyItemRequestedDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRequestedDays.Text = "Días Solicitados"
        Me.INDlyItemRequestedDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRequestedDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRequestedDays.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRequestedDays.TextToControlDistance = 5
        '
        'FrmPopupResumptionHoliday
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 488)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupResumptionHoliday"
        Me.Opacity = 1.0R
        Me.Tag = "1814"
        Me.Text = "Reanudar"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyResumption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyResumption.ResumeLayout(False)
        CType(Me.INDdteEntryDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEntryDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRequestedDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygResumption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEntryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequestedDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyResumption As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygResumption As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDdteInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteEntryDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemEntryDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemRequestedDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtRequestedDays As DevExpress.XtraEditors.SpinEdit
End Class
