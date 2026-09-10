<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMaintenanceCostCenter
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMaintenanceCostCenter))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCostCenter = New DevExpress.XtraLayout.LayoutControl()
        Me.INDNameCostCenter = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlycgCostCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrCostCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCostCenter.SuspendLayout()
        CType(Me.INDNameCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCostCenter)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDLyCostCenter
        '
        Me.INDLyCostCenter.AllowCustomization = False
        Me.INDLyCostCenter.Controls.Add(Me.INDNameCostCenter)
        Me.INDLyCostCenter.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLyCostCenter, "INDLyCostCenter")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCostCenter, False)
        Me.INDLyCostCenter.Name = "INDLyCostCenter"
        Me.INDLyCostCenter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(622, 269, 680, 384)
        Me.INDLyCostCenter.OptionsFocus.EnableAutoTabOrder = False
        Me.INDLyCostCenter.Root = Me.INDlycgCostCenter
        '
        'INDNameCostCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDNameCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDNameCostCenter, True)
        resources.ApplyResources(Me.INDNameCostCenter, "INDNameCostCenter")
        Me.IndigoTextEdit1.SetMascara(Me.INDNameCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDNameCostCenter.Name = "INDNameCostCenter"
        Me.INDNameCostCenter.Properties.Appearance.BackColor = CType(resources.GetObject("INDNameCostCenter.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDNameCostCenter.Properties.Appearance.Font = CType(resources.GetObject("INDNameCostCenter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDNameCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDNameCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDNameCostCenter.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDNameCostCenter.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDNameCostCenter.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDNameCostCenter.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDNameCostCenter.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDNameCostCenter.Properties.MaxLength = 50
        Me.INDNameCostCenter.StyleController = Me.INDLyCostCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDNameCostCenter, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDBteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons10"), Boolean))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.StyleController = Me.INDLyCostCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDlycgCostCenter
        '
        resources.ApplyResources(Me.INDlycgCostCenter, "INDlycgCostCenter")
        Me.INDlycgCostCenter.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgCostCenter.GroupBordersVisible = False
        Me.INDlycgCostCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrCostCenter})
        Me.INDlycgCostCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgCostCenter.Name = "Root"
        Me.INDlycgCostCenter.Size = New System.Drawing.Size(616, 360)
        Me.INDlycgCostCenter.TextVisible = False
        '
        'INDGrCostCenter
        '
        Me.INDGrCostCenter.AppearanceGroup.Font = CType(resources.GetObject("INDGrCostCenter.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrCostCenter.AppearanceGroup.Options.UseFont = True
        resources.ApplyResources(Me.INDGrCostCenter, "INDGrCostCenter")
        Me.INDGrCostCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemCode, Me.INDLyItemName})
        Me.INDGrCostCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDGrCostCenter.Name = "INDGrCostCenter"
        Me.INDGrCostCenter.Size = New System.Drawing.Size(596, 340)
        '
        'INDLyItemCode
        '
        Me.INDLyItemCode.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemCode.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDLyItemCode, "INDLyItemCode")
        Me.INDLyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemCode.Name = "INDLyItemCode"
        Me.INDLyItemCode.ShowInCustomizationForm = False
        Me.INDLyItemCode.Size = New System.Drawing.Size(572, 36)
        Me.INDLyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemCode.Tag = "Code"
        Me.INDLyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemCode.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLyItemCode.TextToControlDistance = 12
        '
        'INDLyItemName
        '
        Me.INDLyItemName.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemName.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemName.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemName.Control = Me.INDNameCostCenter
        resources.ApplyResources(Me.INDLyItemName, "INDLyItemName")
        Me.INDLyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLyItemName.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemName.Name = "INDLyItemName"
        Me.INDLyItemName.ShowInCustomizationForm = False
        Me.INDLyItemName.Size = New System.Drawing.Size(572, 245)
        Me.INDLyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemName.Tag = "Name"
        Me.INDLyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemName.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLyItemName.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCostCenter
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmMaintenanceCostCenter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMaintenanceCostCenter"
        Me.Opacity = 1.0R
        Me.Tag = "622"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCostCenter.ResumeLayout(False)
        CType(Me.INDNameCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCostCenter As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDNameCostCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlycgCostCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrCostCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
End Class
