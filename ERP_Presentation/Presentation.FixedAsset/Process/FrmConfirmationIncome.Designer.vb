Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfirmationIncome
    Inherits FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConfirmationIncome))
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBlockConfirmation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDRgProcess = New DevExpress.XtraEditors.RadioGroup()
        Me.INDDeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeStarDate = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBlockConfirmation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBlockConfirmation.SuspendLayout()
        CType(Me.INDRgProcess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeStarDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeStarDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBlockConfirmation)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
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
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControlPanel1.Appearance.Image = CType(resources.GetObject("CtrNavigationControlPanel1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControlPanel1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControlPanel1.Appearance.Options.UseImage = True
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcBlockConfirmation
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcBlockConfirmation
        '
        Me.INDLcBlockConfirmation.Controls.Add(Me.INDRgProcess)
        Me.INDLcBlockConfirmation.Controls.Add(Me.INDDeEndDate)
        Me.INDLcBlockConfirmation.Controls.Add(Me.INDDeStarDate)
        Me.INDLcBlockConfirmation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBlockConfirmation.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBlockConfirmation.Name = "INDLcBlockConfirmation"
        Me.INDLcBlockConfirmation.Root = Me.LayoutControlGroup1
        Me.INDLcBlockConfirmation.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcBlockConfirmation.TabIndex = 1
        Me.INDLcBlockConfirmation.Text = "LayoutControl1"
        '
        'INDRgProcess
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgProcess, False)
        Me.INDRgProcess.Location = New System.Drawing.Point(24, 203)
        Me.INDRgProcess.Name = "INDRgProcess"
        Me.INDRgProcess.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgProcess.Properties.Appearance.Options.UseFont = True
        Me.INDRgProcess.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgProcess.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgProcess.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDRgProcess.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgProcess.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgProcess.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgProcess.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Ingresos"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Transacciones"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Traslados"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Devoluciones"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Salidas"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Saldos Iniciales"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Valorización y Desvalorización")})
        Me.INDRgProcess.Size = New System.Drawing.Size(386, 347)
        Me.INDRgProcess.StyleController = Me.INDLcBlockConfirmation
        Me.INDRgProcess.TabIndex = 6
        '
        'INDDeEndDate
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDate, False)
        Me.INDDeEndDate.EditValue = Nothing
        Me.INDDeEndDate.Location = New System.Drawing.Point(24, 143)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDate.Name = "INDDeEndDate"
        Me.INDDeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeEndDate.StyleController = Me.INDLcBlockConfirmation
        Me.INDDeEndDate.TabIndex = 5
        '
        'INDDeStarDate
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeStarDate, False)
        Me.INDDeStarDate.EditValue = Nothing
        Me.INDDeStarDate.Location = New System.Drawing.Point(24, 83)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeStarDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeStarDate.Name = "INDDeStarDate"
        Me.INDDeStarDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeStarDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeStarDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeStarDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeStarDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeStarDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeStarDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeStarDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeStarDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeStarDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeStarDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeStarDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeStarDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeStarDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeStarDate.StyleController = Me.INDLcBlockConfirmation
        Me.INDDeStarDate.TabIndex = 4
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 574)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1241, 554)
        Me.LayoutControlGroup2.Text = "Confirmación en Bloque"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDDeStarDate
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1217, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Fecha Inicio"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(78, 21)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDDeEndDate
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1217, 60)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Fecha Fin"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(78, 21)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDRgProcess
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 0)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 25)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1217, 375)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Proceso"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(78, 21)
        '
        'FrmConfirmationIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmConfirmationIncome"
        Me.Opacity = 1.0R
        Me.Tag = "1123"
        Me.Text = "Confirmación Masiva"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBlockConfirmation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBlockConfirmation.ResumeLayout(False)
        CType(Me.INDRgProcess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeStarDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeStarDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBlockConfirmation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDRgProcess As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDDeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDDeStarDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
