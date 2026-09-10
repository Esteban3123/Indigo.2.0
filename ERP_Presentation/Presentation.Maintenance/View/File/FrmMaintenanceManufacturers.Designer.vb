Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMaintenanceManufacturers
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyMaintenanceManufacturers = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlygMaintenanceManufacturers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyCgMaintenanceManufacturers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyMaintenanceManufacturers.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCgMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyMaintenanceManufacturers)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(754, 430)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(754, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(754, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyMaintenanceManufacturers
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 421)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyMaintenanceManufacturers
        '
        Me.INDlyMaintenanceManufacturers.AllowCustomization = False
        Me.INDlyMaintenanceManufacturers.Controls.Add(Me.INDtxtName)
        Me.INDlyMaintenanceManufacturers.Controls.Add(Me.INDbtnCode)
        Me.INDlyMaintenanceManufacturers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMaintenanceManufacturers, False)
        Me.INDlyMaintenanceManufacturers.Location = New System.Drawing.Point(202, 7)
        Me.INDlyMaintenanceManufacturers.Name = "INDlyMaintenanceManufacturers"
        Me.INDlyMaintenanceManufacturers.Root = Me.INDlygMaintenanceManufacturers
        Me.INDlyMaintenanceManufacturers.Size = New System.Drawing.Size(550, 421)
        Me.INDlyMaintenanceManufacturers.TabIndex = 1
        Me.INDlyMaintenanceManufacturers.Text = "LayoutControl1"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.Location = New System.Drawing.Point(164, 95)
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
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtName.StyleController = Me.INDlyMaintenanceManufacturers
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(164, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(246, 28)
        Me.INDbtnCode.StyleController = Me.INDlyMaintenanceManufacturers
        Me.INDbtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlygMaintenanceManufacturers
        '
        Me.INDlygMaintenanceManufacturers.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygMaintenanceManufacturers.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygMaintenanceManufacturers.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygMaintenanceManufacturers.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMaintenanceManufacturers.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygMaintenanceManufacturers, False)
        Me.INDlygMaintenanceManufacturers.CustomizationFormText = "Fabricantes"
        Me.INDlygMaintenanceManufacturers.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygMaintenanceManufacturers.GroupBordersVisible = False
        Me.INDlygMaintenanceManufacturers.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyCgMaintenanceManufacturers})
        Me.INDlygMaintenanceManufacturers.Location = New System.Drawing.Point(0, 0)
        Me.INDlygMaintenanceManufacturers.Name = "INDlygMaintenanceManufacturers"
        Me.INDlygMaintenanceManufacturers.Size = New System.Drawing.Size(550, 421)
        Me.INDlygMaintenanceManufacturers.TextVisible = False
        '
        'INDlyCgMaintenanceManufacturers
        '
        Me.INDlyCgMaintenanceManufacturers.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgMaintenanceManufacturers.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgMaintenanceManufacturers.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCgMaintenanceManufacturers.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgMaintenanceManufacturers.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCgMaintenanceManufacturers, False)
        Me.INDlyCgMaintenanceManufacturers.CustomizationFormText = "Fabricante"
        Me.INDlyCgMaintenanceManufacturers.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName})
        Me.INDlyCgMaintenanceManufacturers.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCgMaintenanceManufacturers.Name = "INDlyCgMaintenanceManufacturers"
        Me.INDlyCgMaintenanceManufacturers.Size = New System.Drawing.Size(530, 401)
        Me.INDlyCgMaintenanceManufacturers.Text = "Fabricante"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(506, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(506, 306)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'FrmMaintenanceManufacturers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 548)
        Me.Name = "FrmMaintenanceManufacturers"
        Me.Opacity = 1.0R
        Me.Tag = "2226"
        Me.Text = "Fabricantes"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyMaintenanceManufacturers.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCgMaintenanceManufacturers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyMaintenanceManufacturers As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygMaintenanceManufacturers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCgMaintenanceManufacturers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
End Class
