Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRIPSServiceGroups
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTeName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgGeneralInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1451, 559)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1451, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 550)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDTeName)
        Me.INDLcRoot.Controls.Add(Me.INDBeCode)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(1247, 550)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDTeName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeName, True)
        Me.INDTeName.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTeName.Name = "INDTeName"
        Me.INDTeName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTeName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTeName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeName.Properties.Appearance.Options.UseFont = True
        Me.INDTeName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeName.Properties.MaxLength = 100
        Me.INDTeName.Size = New System.Drawing.Size(386, 28)
        Me.INDTeName.StyleController = Me.INDLcRoot
        Me.INDTeName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeName, 0)
        Me.INDTeName.ToolTip = "Este Campo es Necesario"
        '
        'INDBeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBeCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBeCode, True)
        Me.INDBeCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDBeCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDBeCode.Name = "INDBeCode"
        Me.INDBeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.Appearance.Options.UseFont = True
        Me.INDBeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Contract.My.Resources.Resources.BuscarMetro
        Me.INDBeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBeCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDBeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDBeCode.Properties.MaxLength = 5
        Me.INDBeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBeCode.StyleController = Me.INDLcRoot
        Me.INDBeCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBeCode, 0)
        Me.INDBeCode.ToolTip = "Este campo es necesario"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgGeneralInfo})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1247, 550)
        Me.Root.TextVisible = False
        '
        'INDLcgGeneralInfo
        '
        Me.INDLcgGeneralInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInfo.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralInfo, False)
        Me.INDLcgGeneralInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName})
        Me.INDLcgGeneralInfo.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgGeneralInfo.Name = "INDLcgGeneralInfo"
        Me.INDLcgGeneralInfo.Size = New System.Drawing.Size(1227, 530)
        Me.INDLcgGeneralInfo.Text = "Información General"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBeCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(1203, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(133, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDTeName
        Me.INDLciName.Location = New System.Drawing.Point(0, 60)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(1203, 417)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(133, 21)
        Me.INDLciName.TextToControlDistance = 5
        '
        'FrmRIPSServiceGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1451, 694)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmRIPSServiceGroups"
        Me.Opacity = 1.0R
        Me.Tag = "2835"
        Me.Text = "FrmRIPSServiceGroups"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTeName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgGeneralInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
End Class
