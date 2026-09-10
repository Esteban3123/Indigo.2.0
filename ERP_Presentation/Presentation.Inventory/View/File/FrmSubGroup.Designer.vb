Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSubGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSubGroup))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlySubGroup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDckHandlesExpiry = New DevExpress.XtraEditors.CheckEdit()
        Me.INDckHandleBatch = New DevExpress.XtraEditors.CheckEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygSubGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHandleBatch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHandlesExpiry = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoCheckedListBoxControl1 = New Presentation.Controls.IndigoCheckedListBoxControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlySubGroup.SuspendLayout()
        CType(Me.INDckHandlesExpiry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDckHandleBatch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHandleBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHandlesExpiry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlySubGroup)
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
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlySubGroup
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlySubGroup
        '
        Me.INDlySubGroup.AllowCustomization = False
        Me.INDlySubGroup.Controls.Add(Me.INDckHandlesExpiry)
        Me.INDlySubGroup.Controls.Add(Me.INDckHandleBatch)
        Me.INDlySubGroup.Controls.Add(Me.INDtxtName)
        Me.INDlySubGroup.Controls.Add(Me.INDbtnCode)
        resources.ApplyResources(Me.INDlySubGroup, "INDlySubGroup")
        Me.LayoutControls.SetIsCustomizable(Me.INDlySubGroup, False)
        Me.INDlySubGroup.Name = "INDlySubGroup"
        Me.INDlySubGroup.Root = Me.LayoutControlGroup1
        '
        'INDckHandlesExpiry
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDckHandlesExpiry, False)
        resources.ApplyResources(Me.INDckHandlesExpiry, "INDckHandlesExpiry")
        Me.INDckHandlesExpiry.Name = "INDckHandlesExpiry"
        Me.INDckHandlesExpiry.Properties.Appearance.Font = CType(resources.GetObject("INDckHandlesExpiry.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDckHandlesExpiry.Properties.Appearance.Options.UseFont = True
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDckHandlesExpiry.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDckHandlesExpiry.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDckHandlesExpiry.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDckHandlesExpiry.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDckHandlesExpiry.Properties.Caption = resources.GetString("INDckHandlesExpiry.Properties.Caption")
        Me.INDckHandlesExpiry.StyleController = Me.INDlySubGroup
        '
        'INDckHandleBatch
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDckHandleBatch, False)
        Me.INDckHandleBatch.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDckHandleBatch, "INDckHandleBatch")
        Me.INDckHandleBatch.Name = "INDckHandleBatch"
        Me.INDckHandleBatch.Properties.Appearance.Font = CType(resources.GetObject("INDckHandleBatch.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDckHandleBatch.Properties.Appearance.Options.UseFont = True
        Me.INDckHandleBatch.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDckHandleBatch.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDckHandleBatch.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDckHandleBatch.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDckHandleBatch.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDckHandleBatch.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDckHandleBatch.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDckHandleBatch.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDckHandleBatch.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDckHandleBatch.Properties.Caption = resources.GetString("INDckHandleBatch.Properties.Caption")
        Me.INDckHandleBatch.StyleController = Me.INDlySubGroup
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlySubGroup
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        resources.ApplyResources(Me.INDbtnCode, "INDbtnCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbtnCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbtnCode.Properties.Appearance.Font = CType(resources.GetObject("INDbtnCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbtnCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbtnCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbtnCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbtnCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbtnCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbtnCode.Properties.Buttons1"), CType(resources.GetObject("INDbtnCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbtnCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbtnCode.Properties.Buttons7"), CType(resources.GetObject("INDbtnCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbtnCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbtnCode.Properties.Buttons10"), Boolean))})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.StyleController = Me.INDlySubGroup
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygSubGroup})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(447, 286)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygSubGroup
        '
        Me.INDlygSubGroup.AppearanceGroup.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlygSubGroup.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlygSubGroup.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygSubGroup.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygSubGroup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygSubGroup.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlygSubGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygSubGroup.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlygSubGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygSubGroup.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlygSubGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygSubGroup.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlygSubGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygSubGroup.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlygSubGroup.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlygSubGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygSubGroup, False)
        resources.ApplyResources(Me.INDlygSubGroup, "INDlygSubGroup")
        Me.INDlygSubGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemHandleBatch, Me.INDlyItemHandlesExpiry})
        Me.INDlygSubGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDlygSubGroup.Name = "INDlygSubGroup"
        Me.INDlygSubGroup.Size = New System.Drawing.Size(427, 266)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(403, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(100, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(403, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(100, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemHandleBatch
        '
        Me.INDlyItemHandleBatch.Control = Me.INDckHandleBatch
        resources.ApplyResources(Me.INDlyItemHandleBatch, "INDlyItemHandleBatch")
        Me.INDlyItemHandleBatch.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemHandleBatch.Name = "INDlyItemHandleBatch"
        Me.INDlyItemHandleBatch.ShowInCustomizationForm = False
        Me.INDlyItemHandleBatch.Size = New System.Drawing.Size(403, 29)
        Me.INDlyItemHandleBatch.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemHandleBatch.TextVisible = False
        '
        'INDlyItemHandlesExpiry
        '
        Me.INDlyItemHandlesExpiry.Control = Me.INDckHandlesExpiry
        resources.ApplyResources(Me.INDlyItemHandlesExpiry, "INDlyItemHandlesExpiry")
        Me.INDlyItemHandlesExpiry.Location = New System.Drawing.Point(0, 101)
        Me.INDlyItemHandlesExpiry.Name = "INDlyItemHandlesExpiry"
        Me.INDlyItemHandlesExpiry.ShowInCustomizationForm = False
        Me.INDlyItemHandlesExpiry.Size = New System.Drawing.Size(403, 106)
        Me.INDlyItemHandlesExpiry.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemHandlesExpiry.TextVisible = False
        '
        'FrmSubGroup
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmSubGroup"
        Me.Opacity = 1.0R
        Me.Tag = "334"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlySubGroup.ResumeLayout(False)
        CType(Me.INDckHandlesExpiry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDckHandleBatch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHandleBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHandlesExpiry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlySubGroup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents IndigoCheckedListBoxControl1 As Presentation.Controls.IndigoCheckedListBoxControl
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlygSubGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDckHandlesExpiry As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDckHandleBatch As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDlyItemHandleBatch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemHandlesExpiry As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As Presentation.Controls.IndigoCheckEdit
End Class
