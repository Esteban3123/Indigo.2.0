Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPartsAccesoriesConsumables
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPartsAccesoriesConsumables))
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyTrademark = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDteName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLycgPrincipalsDatas = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyINameTrademark = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyTrademark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyTrademark.SuspendLayout()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycgPrincipalsDatas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyINameTrademark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyTrademark)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1362, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1362, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1362, 98)
        '
        'INDlyTrademark
        '
        Me.INDlyTrademark.AllowCustomization = False
        Me.INDlyTrademark.Controls.Add(Me.INDBteCode)
        Me.INDlyTrademark.Controls.Add(Me.INDteName)
        Me.INDlyTrademark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyTrademark, False)
        Me.INDlyTrademark.Location = New System.Drawing.Point(202, 7)
        Me.INDlyTrademark.Name = "INDlyTrademark"
        Me.INDlyTrademark.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2202, 282, 574, 569)
        Me.INDlyTrademark.Root = Me.LayoutControlGroup1
        Me.INDlyTrademark.Size = New System.Drawing.Size(1158, 570)
        Me.INDlyTrademark.TabIndex = 1
        Me.INDlyTrademark.Text = "LayoutControl1"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, False)
        Me.INDBteCode.Location = New System.Drawing.Point(166, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.MaximumSize = New System.Drawing.Size(189, 28)
        Me.INDBteCode.MinimumSize = New System.Drawing.Size(0, 28)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("INDBteCode.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDBteCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDBteCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(189, 28)
        Me.INDBteCode.StyleController = Me.INDlyTrademark
        Me.INDBteCode.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDteName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteName, False)
        Me.INDteName.Location = New System.Drawing.Point(166, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDteName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteName.Name = "INDteName"
        Me.INDteName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteName.Properties.Appearance.Options.UseBackColor = True
        Me.INDteName.Properties.Appearance.Options.UseFont = True
        Me.INDteName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteName.Properties.MaxLength = 50
        Me.INDteName.Size = New System.Drawing.Size(274, 28)
        Me.INDteName.StyleController = Me.INDlyTrademark
        Me.INDteName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteName, 0)
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Partes, Accesorios y Consumibles"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLycgPrincipalsDatas})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1158, 570)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLycgPrincipalsDatas
        '
        Me.INDLycgPrincipalsDatas.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLycgPrincipalsDatas.AppearanceGroup.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLycgPrincipalsDatas.AppearanceItemCaption.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLycgPrincipalsDatas.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLycgPrincipalsDatas, False)
        Me.INDLycgPrincipalsDatas.CustomizationFormText = "Datos Generales"
        Me.INDLycgPrincipalsDatas.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyINameTrademark, Me.INDlyItemCode})
        Me.INDLycgPrincipalsDatas.Location = New System.Drawing.Point(0, 0)
        Me.INDLycgPrincipalsDatas.Name = "INDLycgPrincipalsDatas"
        Me.INDLycgPrincipalsDatas.Size = New System.Drawing.Size(1138, 550)
        Me.INDLycgPrincipalsDatas.Text = "Datos Generales"
        '
        'INDLyINameTrademark
        '
        Me.INDLyINameTrademark.Control = Me.INDteName
        Me.INDLyINameTrademark.CustomizationFormText = "Nombre"
        Me.INDLyINameTrademark.Location = New System.Drawing.Point(0, 36)
        Me.INDLyINameTrademark.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyINameTrademark.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyINameTrademark.Name = "INDLyINameTrademark"
        Me.INDLyINameTrademark.ShowInCustomizationForm = False
        Me.INDLyINameTrademark.Size = New System.Drawing.Size(1114, 455)
        Me.INDLyINameTrademark.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyINameTrademark.Tag = "570"
        Me.INDLyINameTrademark.Text = "Nombre"
        Me.INDLyINameTrademark.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyINameTrademark.TextSize = New System.Drawing.Size(130, 21)
        Me.INDLyINameTrademark.TextToControlDistance = 12
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDBteCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(1114, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(130, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControl1.Appearance.Image = CType(resources.GetObject("CtrNavigationControl1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.Appearance.Options.UseImage = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyTrademark
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmPartsAccesoriesConsumables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 701)
        Me.Name = "FrmPartsAccesoriesConsumables"
        Me.Opacity = 1.0R
        Me.Tag = "1531"
        Me.Text = "Partes, Accesorios y Consumibles"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyTrademark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyTrademark.ResumeLayout(False)
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycgPrincipalsDatas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyINameTrademark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyTrademark As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLycgPrincipalsDatas As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDteName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyINameTrademark As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
End Class
