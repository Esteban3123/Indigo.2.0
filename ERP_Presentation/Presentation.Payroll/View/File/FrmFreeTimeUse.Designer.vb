<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFreeTimeUse
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
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcFreeTimeUse = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlciFreeTimeUse = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcFreeTimeUse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcFreeTimeUse.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciFreeTimeUse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcFreeTimeUse)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(734, 326)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(734, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(734, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlcFreeTimeUse
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 317)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlcFreeTimeUse
        '
        Me.INDlcFreeTimeUse.Controls.Add(Me.INDtxtName)
        Me.INDlcFreeTimeUse.Controls.Add(Me.INDbtnCode)
        Me.INDlcFreeTimeUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcFreeTimeUse.Location = New System.Drawing.Point(202, 7)
        Me.INDlcFreeTimeUse.Name = "INDlcFreeTimeUse"
        Me.INDlcFreeTimeUse.Root = Me.INDlciFreeTimeUse
        Me.INDlcFreeTimeUse.Size = New System.Drawing.Size(530, 317)
        Me.INDlcFreeTimeUse.TabIndex = 1
        Me.INDlcFreeTimeUse.Text = "LayoutControl1"
        '
        'INDtxtName
        '
        Me.INDtxtName.Location = New System.Drawing.Point(24, 143)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 30
        Me.INDtxtName.Size = New System.Drawing.Size(368, 28)
        Me.INDtxtName.StyleController = Me.INDlcFreeTimeUse
        Me.INDtxtName.TabIndex = 5
        '
        'INDbtnCode
        '
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(368, 28)
        Me.INDbtnCode.StyleController = Me.INDlcFreeTimeUse
        Me.INDbtnCode.TabIndex = 4
        '
        'INDlciFreeTimeUse
        '
        Me.INDlciFreeTimeUse.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDlciFreeTimeUse.AppearanceGroup.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDlciFreeTimeUse.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciFreeTimeUse.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciFreeTimeUse.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlciFreeTimeUse.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciFreeTimeUse.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlciFreeTimeUse.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlciFreeTimeUse.GroupBordersVisible = False
        Me.INDlciFreeTimeUse.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.INDlciFreeTimeUse.Name = "INDlciFreeTimeUse"
        Me.INDlciFreeTimeUse.Size = New System.Drawing.Size(530, 317)
        Me.INDlciFreeTimeUse.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.CustomizationFormText = "Uso del Tiempo Libre"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciName, Me.INDlciCode})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(510, 297)
        Me.INDlcgMainData.Text = "Uso del Tiempo Libre"
        '
        'INDlciName
        '
        Me.INDlciName.Control = Me.INDtxtName
        Me.INDlciName.CustomizationFormText = "Nombre"
        Me.INDlciName.Location = New System.Drawing.Point(0, 64)
        Me.INDlciName.MaxSize = New System.Drawing.Size(372, 64)
        Me.INDlciName.MinSize = New System.Drawing.Size(372, 64)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.ShowInCustomizationForm = False
        Me.INDlciName.Size = New System.Drawing.Size(486, 180)
        Me.INDlciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciName.Text = "Nombre"
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciName.TextToControlDistance = 5
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(372, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(372, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.Size = New System.Drawing.Size(486, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'FrmFreeTimeUse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 461)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmFreeTimeUse"
        Me.Opacity = 1.0R
        Me.Tag = "2035"
        Me.Text = "FrmFreeTimeUse"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcFreeTimeUse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcFreeTimeUse.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciFreeTimeUse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcFreeTimeUse As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlciFreeTimeUse As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
End Class
