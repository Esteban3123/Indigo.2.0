<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMedicationType
    Inherits Presentation.Controls.FormBase

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
        Me.INDPcBase = New DevExpress.XtraEditors.PanelControl()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeName = New DevExpress.XtraEditors.MemoEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDCtrNcp = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcBase.SuspendLayout()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDMeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCtrNcp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDPcBase)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1057, 718)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1057, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1057, 130)
        '
        'INDPcBase
        '
        Me.INDPcBase.Controls.Add(Me.INDLcRoot)
        Me.INDPcBase.Controls.Add(Me.INDCtrNcp)
        Me.INDPcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcBase.Location = New System.Drawing.Point(2, 8)
        Me.INDPcBase.Name = "INDPcBase"
        Me.INDPcBase.Size = New System.Drawing.Size(1053, 708)
        Me.INDPcBase.TabIndex = 0
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDMeName)
        Me.INDLcRoot.Controls.Add(Me.INDBtnCode)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 2)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(849, 704)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDMeName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeName, True)
        Me.INDMeName.Location = New System.Drawing.Point(24, 186)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeName.Name = "INDMeName"
        Me.INDMeName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDMeName.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeName.Properties.Appearance.Options.UseFont = True
        Me.INDMeName.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeName.Properties.MaxLength = 200
        Me.INDMeName.Size = New System.Drawing.Size(456, 60)
        Me.INDMeName.StyleController = Me.INDLcRoot
        Me.INDMeName.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeName, 0)
        Me.INDMeName.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(24, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBtnCode.MaximumSize = New System.Drawing.Size(455, 34)
        Me.INDBtnCode.MinimumSize = New System.Drawing.Size(455, 34)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDBtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(455, 34)
        Me.INDBtnCode.StyleController = Me.INDLcRoot
        Me.INDBtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMain})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(849, 704)
        Me.Root.TextVisible = False
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.Size = New System.Drawing.Size(829, 684)
        Me.INDLcgMain.Text = "Datos principales"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(531, 91)
        Me.INDLciCode.MinSize = New System.Drawing.Size(531, 91)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(805, 91)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(68, 31)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDMeName
        Me.INDLciName.Location = New System.Drawing.Point(0, 91)
        Me.INDLciName.MaxSize = New System.Drawing.Size(460, 100)
        Me.INDLciName.MinSize = New System.Drawing.Size(54, 51)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.ShowInCustomizationForm = False
        Me.INDLciName.Size = New System.Drawing.Size(805, 534)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(68, 31)
        Me.INDLciName.TextToControlDistance = 5
        '
        'INDCtrNcp
        '
        Me.INDCtrNcp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCtrNcp.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCtrNcp.LayoutControl = Me.INDLcRoot
        Me.INDCtrNcp.Location = New System.Drawing.Point(2, 2)
        Me.INDCtrNcp.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCtrNcp.Name = "INDCtrNcp"
        Me.INDCtrNcp.Size = New System.Drawing.Size(200, 704)
        Me.INDCtrNcp.TabIndex = 0
        Me.INDCtrNcp.UseDisabledStatePainter = False
        '
        'FrmMedicationType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1057, 854)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmMedicationType"
        Me.Opacity = 1.0R
        Me.Tag = "89030"
        Me.Text = "Tipo de medicamento"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcBase.ResumeLayout(False)
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDMeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCtrNcp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDPcBase As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDCtrNcp As Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
    Friend WithEvents INDMeName As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
End Class
