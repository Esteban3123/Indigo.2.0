Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPackagingUnit
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDCnNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgPackaginUnit = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtAbbreviation = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAbbreviation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPackaginUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDtxtAbbreviation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAbbreviation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnNavigation)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 611)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'INDCnNavigation
        '
        Me.INDCnNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnNavigation.LayoutControl = Me.INDLcBase
        Me.INDCnNavigation.Location = New System.Drawing.Point(2, 7)
        Me.INDCnNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnNavigation.Name = "INDCnNavigation"
        Me.INDCnNavigation.Size = New System.Drawing.Size(200, 602)
        Me.INDCnNavigation.TabIndex = 0
        Me.INDCnNavigation.UseDisabledStatePainter = False
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "Unidades de Empaque"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgPackaginUnit})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 602)
        Me.INDLcgBase.Text = "Unidades de Empaque"
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgPackaginUnit
        '
        Me.INDLcgPackaginUnit.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgPackaginUnit.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgPackaginUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPackaginUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPackaginUnit.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPackaginUnit.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgPackaginUnit.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgPackaginUnit.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPackaginUnit.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPackaginUnit, False)
        Me.INDLcgPackaginUnit.CustomizationFormText = "Unidad de Empaque"
        Me.INDLcgPackaginUnit.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName, Me.INDLciAbbreviation})
        Me.INDLcgPackaginUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPackaginUnit.Name = "INDLcgPackaginUnit"
        Me.INDLcgPackaginUnit.Size = New System.Drawing.Size(784, 582)
        Me.INDLcgPackaginUnit.Text = "Unidad de Empaque"
        '
        'INDLciCode
        '
        Me.INDLciCode.AllowHide = False
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(760, 36)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 12
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(171, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(239, 28)
        Me.INDBtnCode.StyleController = Me.INDLcBase
        Me.INDBtnCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDLcBase
        '
        Me.INDLcBase.AllowCustomization = False
        Me.INDLcBase.Controls.Add(Me.INDtxtAbbreviation)
        Me.INDLcBase.Controls.Add(Me.INDTxtName)
        Me.INDLcBase.Controls.Add(Me.INDBtnCode)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, False)
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 602)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDtxtAbbreviation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAbbreviation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAbbreviation, True)
        Me.INDtxtAbbreviation.EnterMoveNextControl = True
        Me.INDtxtAbbreviation.Location = New System.Drawing.Point(171, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAbbreviation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAbbreviation.Name = "INDtxtAbbreviation"
        Me.INDtxtAbbreviation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtAbbreviation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAbbreviation.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAbbreviation.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtAbbreviation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAbbreviation.Properties.MaxLength = 10
        Me.INDtxtAbbreviation.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtAbbreviation.StyleController = Me.INDLcBase
        Me.INDtxtAbbreviation.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAbbreviation, 0)
        Me.INDtxtAbbreviation.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        Me.INDTxtName.Location = New System.Drawing.Point(171, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.MaxLength = 100
        Me.INDTxtName.Size = New System.Drawing.Size(239, 28)
        Me.INDTxtName.StyleController = Me.INDLcBase
        Me.INDTxtName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        Me.INDTxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDLciName
        '
        Me.INDLciName.AllowHide = False
        Me.INDLciName.Control = Me.INDTxtName
        Me.INDLciName.CustomizationFormText = "Nombre"
        Me.INDLciName.Location = New System.Drawing.Point(0, 36)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.ShowInCustomizationForm = False
        Me.INDLciName.Size = New System.Drawing.Size(760, 36)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciName.TextToControlDistance = 12
        '
        'INDLciAbbreviation
        '
        Me.INDLciAbbreviation.AllowHide = False
        Me.INDLciAbbreviation.Control = Me.INDtxtAbbreviation
        Me.INDLciAbbreviation.CustomizationFormText = "LayoutControlItem3"
        Me.INDLciAbbreviation.Location = New System.Drawing.Point(0, 72)
        Me.INDLciAbbreviation.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciAbbreviation.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciAbbreviation.Name = "INDLciAbbreviation"
        Me.INDLciAbbreviation.ShowInCustomizationForm = False
        Me.INDLciAbbreviation.Size = New System.Drawing.Size(760, 451)
        Me.INDLciAbbreviation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAbbreviation.Text = "Abreviatura"
        Me.INDLciAbbreviation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAbbreviation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAbbreviation.TextToControlDistance = 12
        '
        'FrmPackagingUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmPackagingUnit"
        Me.Opacity = 1.0R
        Me.Tag = "1508"
        Me.Text = "Unidades de Empaque"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPackaginUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDtxtAbbreviation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAbbreviation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtAbbreviation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAbbreviation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgPackaginUnit As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
End Class
