<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmElectronicSignatureToken
    Inherits DevExpress.XtraEditors.XtraForm
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmElectronicSignatureToken))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbSave = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeToken = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgToken = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit2 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTeToken.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgToken, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSbSave)
        Me.LayoutControl1.Controls.Add(Me.INDTeToken)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'INDSbSave
        '
        Me.INDSbSave.Appearance.Font = CType(resources.GetObject("INDSbSave.Appearance.Font"), System.Drawing.Font)
        Me.INDSbSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDSbSave, "INDSbSave")
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbSave, True)
        Me.INDSbSave.Name = "INDSbSave"
        Me.INDSbSave.StyleController = Me.LayoutControl1
        '
        'INDTeToken
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDTeToken, True)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDTeToken, False)
        Me.INDTeToken.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTeToken, "INDTeToken")
        Me.IndigoTextEdit2.SetMascara(Me.INDTeToken, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeToken.Name = "INDTeToken"
        Me.INDTeToken.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTeToken.Properties.Appearance.Font = CType(resources.GetObject("INDTeToken.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTeToken.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.INDTeToken.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeToken.Properties.Appearance.Options.UseFont = True
        Me.INDTeToken.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeToken.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDTeToken.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTeToken.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTeToken.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.INDTeToken.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeToken.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeToken.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTeToken.Properties.Mask.EditMask = resources.GetString("INDTeToken.Properties.Mask.EditMask")
        Me.INDTeToken.Properties.Mask.MaskType = CType(resources.GetObject("INDTeToken.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTeToken.Properties.MaxLength = 100
        Me.INDTeToken.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDTeToken, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgToken, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(824, 131)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgToken
        '
        Me.INDlcgToken.AppearanceGroup.Font = CType(resources.GetObject("INDlcgToken.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceGroup.Options.UseFont = True
        Me.INDlcgToken.AppearanceItemCaption.Font = CType(resources.GetObject("INDlcgToken.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgToken.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlcgToken.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgToken.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlcgToken.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgToken.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlcgToken.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgToken.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlcgToken.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgToken.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlcgToken.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlcgToken.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgToken, False)
        resources.ApplyResources(Me.INDlcgToken, "INDlcgToken")
        Me.INDlcgToken.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.INDlcgToken.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgToken.Name = "INDlcgToken"
        Me.INDlcgToken.Size = New System.Drawing.Size(814, 89)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem7.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem7.Control = Me.INDTeToken
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(400, 36)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(790, 36)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Spacing = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(43, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.INDSbSave
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 89)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(154, 26)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(154, 26)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(814, 32)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmElectronicSignatureToken
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmElectronicSignatureToken"
        Me.ShowInTaskbar = False
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTeToken.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgToken, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgToken As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDTeToken As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit2 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
End Class
