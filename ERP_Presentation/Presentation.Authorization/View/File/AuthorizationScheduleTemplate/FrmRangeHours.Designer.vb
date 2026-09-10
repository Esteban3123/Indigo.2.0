Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRangeHours
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddRange = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDdteInitial = New DevExpress.XtraEditors.TimeEdit()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDcheckNextDay = New DevExpress.XtraEditors.CheckEdit()
        Me.INDdteEnd = New DevExpress.XtraEditors.TimeEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInitial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNextDay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDcheckNextDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNextDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(438, 233)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(438, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(438, 98)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddRange)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 191)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(434, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'INDbtnAddRange
        '
        Me.INDbtnAddRange.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddRange.Appearance.Options.UseFont = True
        Me.INDbtnAddRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddRange.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddRange, True)
        Me.INDbtnAddRange.Name = "INDbtnAddRange"
        Me.INDbtnAddRange.Size = New System.Drawing.Size(430, 36)
        Me.INDbtnAddRange.TabIndex = 0
        Me.INDbtnAddRange.Text = "Aceptar"
        '
        'INDdteInitial
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteInitial, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteInitial, False)
        Me.INDdteInitial.EditValue = New Date(2020, 5, 20, 0, 0, 0, 0)
        Me.INDdteInitial.EnterMoveNextControl = True
        Me.INDdteInitial.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteInitial, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteInitial.Name = "INDdteInitial"
        Me.INDdteInitial.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteInitial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitial.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteInitial.Properties.Appearance.Options.UseFont = True
        Me.INDdteInitial.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitial.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteInitial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitial.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteInitial.Size = New System.Drawing.Size(386, 28)
        Me.INDdteInitial.StyleController = Me.INDlyRoot
        Me.INDdteInitial.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteInitial, 0)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDcheckNextDay)
        Me.INDlyRoot.Controls.Add(Me.INDdteEnd)
        Me.INDlyRoot.Controls.Add(Me.INDdteInitial)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(434, 184)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDcheckNextDay
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDcheckNextDay, False)
        Me.INDcheckNextDay.EnterMoveNextControl = True
        Me.INDcheckNextDay.Location = New System.Drawing.Point(12, 132)
        Me.INDcheckNextDay.Name = "INDcheckNextDay"
        Me.INDcheckNextDay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDcheckNextDay.Properties.Appearance.Options.UseFont = True
        Me.INDcheckNextDay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDcheckNextDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDcheckNextDay.Properties.Caption = "Día Siguiente"
        Me.INDcheckNextDay.Size = New System.Drawing.Size(410, 25)
        Me.INDcheckNextDay.StyleController = Me.INDlyRoot
        Me.INDcheckNextDay.TabIndex = 4
        '
        'INDdteEnd
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteEnd, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteEnd, False)
        Me.INDdteEnd.EditValue = New Date(2020, 5, 20, 0, 0, 0, 0)
        Me.INDdteEnd.EnterMoveNextControl = True
        Me.INDdteEnd.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteEnd, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteEnd.Name = "INDdteEnd"
        Me.INDdteEnd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEnd.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteEnd.Properties.Appearance.Options.UseFont = True
        Me.INDdteEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteEnd.Size = New System.Drawing.Size(386, 28)
        Me.INDdteEnd.StyleController = Me.INDlyRoot
        Me.INDdteEnd.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteEnd, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInitial, Me.INDlyItemEnd, Me.INDlyItemNextDay})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(434, 184)
        Me.Root.TextVisible = False
        '
        'INDlyItemInitial
        '
        Me.INDlyItemInitial.Control = Me.INDdteInitial
        Me.INDlyItemInitial.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemInitial.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitial.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitial.Name = "INDlyItemInitial"
        Me.INDlyItemInitial.Size = New System.Drawing.Size(414, 60)
        Me.INDlyItemInitial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitial.Text = "Hora Inicial"
        Me.INDlyItemInitial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInitial.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitial.TextToControlDistance = 5
        '
        'INDlyItemEnd
        '
        Me.INDlyItemEnd.Control = Me.INDdteEnd
        Me.INDlyItemEnd.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemEnd.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEnd.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEnd.Name = "INDlyItemEnd"
        Me.INDlyItemEnd.Size = New System.Drawing.Size(414, 60)
        Me.INDlyItemEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEnd.Text = "Hora Final"
        Me.INDlyItemEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEnd.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEnd.TextToControlDistance = 5
        '
        'INDlyItemNextDay
        '
        Me.INDlyItemNextDay.Control = Me.INDcheckNextDay
        Me.INDlyItemNextDay.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemNextDay.Name = "INDlyItemNextDay"
        Me.INDlyItemNextDay.Size = New System.Drawing.Size(414, 44)
        Me.INDlyItemNextDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNextDay.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemNextDay.TextToControlDistance = 0
        Me.INDlyItemNextDay.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmRangeHours
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 355)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRangeHours"
        Me.Opacity = 1.0R
        Me.Text = "Rango de Horas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDcheckNextDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNextDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddRange As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDdteInitial As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDlyItemInitial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteEnd As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDlyItemEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcheckNextDay As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents INDlyItemNextDay As DevExpress.XtraLayout.LayoutControlItem
End Class
