<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiagnosedDisease
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
        Me.INDlcDiagnosedDisease = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlciDiagnosedDisease = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcDiagnosedDisease, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcDiagnosedDisease.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDiagnosedDisease, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcDiagnosedDisease)
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
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlcDiagnosedDisease
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 317)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlcDiagnosedDisease
        '
        Me.INDlcDiagnosedDisease.Controls.Add(Me.INDtxtName)
        Me.INDlcDiagnosedDisease.Controls.Add(Me.INDbtnCode)
        Me.INDlcDiagnosedDisease.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcDiagnosedDisease.Location = New System.Drawing.Point(202, 7)
        Me.INDlcDiagnosedDisease.Name = "INDlcDiagnosedDisease"
        Me.INDlcDiagnosedDisease.Root = Me.INDlciDiagnosedDisease
        Me.INDlcDiagnosedDisease.Size = New System.Drawing.Size(530, 317)
        Me.INDlcDiagnosedDisease.TabIndex = 1
        Me.INDlcDiagnosedDisease.Text = "LayoutControl1"
        '
        'INDtxtName
        '
        Me.INDtxtName.Location = New System.Drawing.Point(24, 142)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(368, 28)
        Me.INDtxtName.StyleController = Me.INDlcDiagnosedDisease
        Me.INDtxtName.TabIndex = 5
        '
        'INDbtnCode
        '
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 78)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(368, 28)
        Me.INDbtnCode.StyleController = Me.INDlcDiagnosedDisease
        Me.INDbtnCode.TabIndex = 4
        '
        'INDlciDiagnosedDisease
        '
        Me.INDlciDiagnosedDisease.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDlciDiagnosedDisease.AppearanceGroup.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDlciDiagnosedDisease.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciDiagnosedDisease.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciDiagnosedDisease.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlciDiagnosedDisease.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciDiagnosedDisease.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlciDiagnosedDisease.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlciDiagnosedDisease.GroupBordersVisible = False
        Me.INDlciDiagnosedDisease.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.INDlciDiagnosedDisease.Name = "INDlciDiagnosedDisease"
        Me.INDlciDiagnosedDisease.Size = New System.Drawing.Size(530, 317)
        Me.INDlciDiagnosedDisease.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.CustomizationFormText = "Enfermedad Diagnosticada"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCode, Me.INDlciName})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(510, 297)
        Me.INDlcgMainData.Text = "Enfermedad Diagnosticada"
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
        Me.INDlciCode.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciName
        '
        Me.INDlciName.Control = Me.INDtxtName
        Me.INDlciName.CustomizationFormText = "Enfermedad"
        Me.INDlciName.Location = New System.Drawing.Point(0, 64)
        Me.INDlciName.MaxSize = New System.Drawing.Size(372, 64)
        Me.INDlciName.MinSize = New System.Drawing.Size(372, 64)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.Size = New System.Drawing.Size(486, 180)
        Me.INDlciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciName.Text = "Enfermedad"
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciName.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciName.TextToControlDistance = 5
        '
        'FrmDiagnosedDisease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 461)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmDiagnosedDisease"
        Me.Opacity = 1.0R
        Me.Tag = "2034"
        Me.Text = "FrmDiagnosedDisease"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcDiagnosedDisease, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcDiagnosedDisease.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDiagnosedDisease, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcDiagnosedDisease As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlciDiagnosedDisease As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
End Class
