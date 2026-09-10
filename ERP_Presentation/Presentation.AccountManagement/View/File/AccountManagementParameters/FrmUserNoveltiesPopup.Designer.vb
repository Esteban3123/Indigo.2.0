<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserNoveltiesPopup
    Inherits Presentation.Controls.FormBase

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
        Me.INDlyNoveltyPopup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAddNovelty = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeNoveltyDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDLeNoveltyStatus = New DevExpress.XtraEditors.LookUpEdit()
        Me.INDDeNoveltyDate = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLygNoveltyGeneral = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciNoveltyDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNoveltyStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNoveltyDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddButton = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyNoveltyPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyNoveltyPopup.SuspendLayout()
        CType(Me.INDTeNoveltyDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLeNoveltyStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeNoveltyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeNoveltyDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygNoveltyGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNoveltyDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNoveltyStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNoveltyDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyNoveltyPopup)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(800, 315)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(800, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(800, 130)
        '
        'INDlyNoveltyPopup
        '
        Me.INDlyNoveltyPopup.AllowCustomization = False
        Me.INDlyNoveltyPopup.Controls.Add(Me.INDSbAddNovelty)
        Me.INDlyNoveltyPopup.Controls.Add(Me.INDTeNoveltyDescription)
        Me.INDlyNoveltyPopup.Controls.Add(Me.INDLeNoveltyStatus)
        Me.INDlyNoveltyPopup.Controls.Add(Me.INDDeNoveltyDate)
        Me.INDlyNoveltyPopup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyNoveltyPopup, False)
        Me.INDlyNoveltyPopup.Location = New System.Drawing.Point(2, 7)
        Me.INDlyNoveltyPopup.Name = "INDlyNoveltyPopup"
        Me.INDlyNoveltyPopup.Root = Me.LayoutControlGroup1
        Me.INDlyNoveltyPopup.Size = New System.Drawing.Size(796, 306)
        Me.INDlyNoveltyPopup.TabIndex = 4
        Me.INDlyNoveltyPopup.Text = "LayoutControl1"
        '
        'INDSbAddNovelty
        '
        Me.INDSbAddNovelty.Location = New System.Drawing.Point(24, 252)
        Me.INDSbAddNovelty.MaximumSize = New System.Drawing.Size(0, 30)
        Me.INDSbAddNovelty.Name = "INDSbAddNovelty"
        Me.INDSbAddNovelty.Size = New System.Drawing.Size(748, 30)
        Me.INDSbAddNovelty.StyleController = Me.INDlyNoveltyPopup
        Me.INDSbAddNovelty.TabIndex = 4
        Me.INDSbAddNovelty.Text = "Agregar"
        '
        'INDTeNoveltyDescription
        '
        Me.INDTeNoveltyDescription.Location = New System.Drawing.Point(24, 195)
        Me.INDTeNoveltyDescription.Name = "INDTeNoveltyDescription"
        Me.INDTeNoveltyDescription.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDTeNoveltyDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTeNoveltyDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeNoveltyDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTeNoveltyDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeNoveltyDescription.Properties.Appearance.Options.UseFont = True
        Me.INDTeNoveltyDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeNoveltyDescription.Properties.MaxLength = 300
        Me.INDTeNoveltyDescription.Size = New System.Drawing.Size(748, 28)
        Me.INDTeNoveltyDescription.StyleController = Me.INDlyNoveltyPopup
        Me.INDTeNoveltyDescription.TabIndex = 2
        '
        'INDLeNoveltyStatus
        '
        Me.INDLeNoveltyStatus.Location = New System.Drawing.Point(24, 137)
        Me.INDLeNoveltyStatus.Name = "INDLeNoveltyStatus"
        Me.INDLeNoveltyStatus.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDLeNoveltyStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLeNoveltyStatus.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDLeNoveltyStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDLeNoveltyStatus.Properties.Appearance.Options.UseFont = True
        Me.INDLeNoveltyStatus.Properties.Appearance.Options.UseForeColor = True
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDLeNoveltyStatus.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDLeNoveltyStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDLeNoveltyStatus.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Estado")})
        Me.INDLeNoveltyStatus.Properties.DropDownRows = 2
        Me.INDLeNoveltyStatus.Properties.MaxLength = 100
        Me.INDLeNoveltyStatus.Properties.NullText = ""
        Me.INDLeNoveltyStatus.Size = New System.Drawing.Size(748, 28)
        Me.INDLeNoveltyStatus.StyleController = Me.INDlyNoveltyPopup
        Me.INDLeNoveltyStatus.TabIndex = 1
        Me.INDLeNoveltyStatus.TabStop = False
        '
        'INDDeNoveltyDate
        '
        Me.INDDeNoveltyDate.EditValue = Nothing
        Me.INDDeNoveltyDate.Location = New System.Drawing.Point(24, 79)
        Me.INDDeNoveltyDate.Name = "INDDeNoveltyDate"
        Me.INDDeNoveltyDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDeNoveltyDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeNoveltyDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeNoveltyDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeNoveltyDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeNoveltyDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeNoveltyDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeNoveltyDate.Properties.DisplayFormat.FormatString = ""
        Me.INDDeNoveltyDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDDeNoveltyDate.Properties.EditFormat.FormatString = ""
        Me.INDDeNoveltyDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDDeNoveltyDate.Properties.Mask.EditMask = ""
        Me.INDDeNoveltyDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.INDDeNoveltyDate.Properties.MaxLength = 20
        Me.INDDeNoveltyDate.Size = New System.Drawing.Size(748, 28)
        Me.INDDeNoveltyDate.StyleController = Me.INDlyNoveltyPopup
        Me.INDDeNoveltyDate.TabIndex = 0
        Me.INDDeNoveltyDate.ToolTip = "Este Campo es Necesario"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "Forma Farmacéutica"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLygNoveltyGeneral})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(796, 306)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLygNoveltyGeneral
        '
        Me.INDLygNoveltyGeneral.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygNoveltyGeneral.AppearanceGroup.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygNoveltyGeneral.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygNoveltyGeneral.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygNoveltyGeneral.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygNoveltyGeneral.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygNoveltyGeneral.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLygNoveltyGeneral.CustomizationFormText = "Información General"
        Me.INDLygNoveltyGeneral.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.INDLygNoveltyGeneral.Location = New System.Drawing.Point(0, 0)
        Me.INDLygNoveltyGeneral.Name = "INDLygNoveltyGeneral"
        Me.INDLygNoveltyGeneral.Size = New System.Drawing.Size(776, 286)
        Me.INDLygNoveltyGeneral.Text = "Información General"
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciNoveltyDate, Me.INDLciNoveltyStatus, Me.INDLciNoveltyDescription, Me.INDLciAddButton})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(752, 233)
        '
        'INDLciNoveltyDate
        '
        Me.INDLciNoveltyDate.AllowHide = False
        Me.INDLciNoveltyDate.Control = Me.INDDeNoveltyDate
        Me.INDLciNoveltyDate.CustomizationFormText = "Fecha"
        Me.INDLciNoveltyDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciNoveltyDate.Name = "INDLciNoveltyDate"
        Me.INDLciNoveltyDate.ShowInCustomizationForm = False
        Me.INDLciNoveltyDate.Size = New System.Drawing.Size(752, 58)
        Me.INDLciNoveltyDate.Text = "Fecha"
        Me.INDLciNoveltyDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNoveltyDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNoveltyDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciNoveltyDate.TextToControlDistance = 5
        '
        'INDLciNoveltyStatus
        '
        Me.INDLciNoveltyStatus.AllowHide = False
        Me.INDLciNoveltyStatus.Control = Me.INDLeNoveltyStatus
        Me.INDLciNoveltyStatus.CustomizationFormText = "Estado"
        Me.INDLciNoveltyStatus.Location = New System.Drawing.Point(0, 58)
        Me.INDLciNoveltyStatus.Name = "INDLciNoveltyStatus"
        Me.INDLciNoveltyStatus.OptionsTableLayoutItem.ColumnIndex = 1
        Me.INDLciNoveltyStatus.ShowInCustomizationForm = False
        Me.INDLciNoveltyStatus.Size = New System.Drawing.Size(752, 58)
        Me.INDLciNoveltyStatus.Text = "Estado"
        Me.INDLciNoveltyStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNoveltyStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNoveltyStatus.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciNoveltyStatus.TextToControlDistance = 5
        '
        'INDLciNoveltyDescription
        '
        Me.INDLciNoveltyDescription.AllowHide = False
        Me.INDLciNoveltyDescription.Control = Me.INDTeNoveltyDescription
        Me.INDLciNoveltyDescription.Location = New System.Drawing.Point(0, 116)
        Me.INDLciNoveltyDescription.Name = "INDLciNoveltyDescription"
        Me.INDLciNoveltyDescription.OptionsTableLayoutItem.RowIndex = 1
        Me.INDLciNoveltyDescription.Size = New System.Drawing.Size(752, 58)
        Me.INDLciNoveltyDescription.Text = "Descripción"
        Me.INDLciNoveltyDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNoveltyDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNoveltyDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciNoveltyDescription.TextToControlDistance = 5
        '
        'INDLciAddButton
        '
        Me.INDLciAddButton.ContentVertAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.INDLciAddButton.Control = Me.INDSbAddNovelty
        Me.INDLciAddButton.Location = New System.Drawing.Point(0, 174)
        Me.INDLciAddButton.Name = "INDLciAddButton"
        Me.INDLciAddButton.Size = New System.Drawing.Size(752, 59)
        Me.INDLciAddButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddButton.TextVisible = False
        '
        'FrmUserNoveltiesPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmUserNoveltiesPopup"
        Me.Opacity = 1.0R
        Me.Text = "Novedades"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyNoveltyPopup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyNoveltyPopup.ResumeLayout(False)
        CType(Me.INDTeNoveltyDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLeNoveltyStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeNoveltyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeNoveltyDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygNoveltyGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNoveltyDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNoveltyStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNoveltyDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDlyNoveltyPopup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLygNoveltyGeneral As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciNoveltyDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTeNoveltyDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLeNoveltyStatus As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents INDDeNoveltyDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciNoveltyDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNoveltyStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSbAddNovelty As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAddButton As DevExpress.XtraLayout.LayoutControlItem
End Class
