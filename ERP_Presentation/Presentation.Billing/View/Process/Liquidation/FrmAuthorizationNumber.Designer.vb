<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAuthorizationNumber
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAuthorizationNumber))
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtAuthorizationNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAuthorizationNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDtxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAuthorizationNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsbAcept)
        Me.INDlcRoot.Controls.Add(Me.INDtxtAuthorizationNumber)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(430, 94)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsbAcept
        '
        Me.INDsbAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAcept.Appearance.Options.UseFont = True
        Me.INDsbAcept.Location = New System.Drawing.Point(12, 48)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAcept, True)
        Me.INDsbAcept.Name = "INDsbAcept"
        Me.INDsbAcept.Size = New System.Drawing.Size(406, 28)
        Me.INDsbAcept.StyleController = Me.INDlcRoot
        Me.INDsbAcept.TabIndex = 1
        Me.INDsbAcept.Text = "Aceptar"
        '
        'INDtxtAuthorizationNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAuthorizationNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAuthorizationNumber, False)
        Me.INDtxtAuthorizationNumber.EnterMoveNextControl = True
        Me.INDtxtAuthorizationNumber.Location = New System.Drawing.Point(172, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAuthorizationNumber, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtAuthorizationNumber.Name = "INDtxtAuthorizationNumber"
        Me.INDtxtAuthorizationNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAuthorizationNumber.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtAuthorizationNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtAuthorizationNumber.Properties.MaxLength = 20
        Me.INDtxtAuthorizationNumber.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtAuthorizationNumber.StyleController = Me.INDlcRoot
        Me.INDtxtAuthorizationNumber.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAuthorizationNumber, 0)
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAuthorizationNumber, Me.INDlciAcept})
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(430, 94)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDliAuthorizationNumber
        '
        Me.INDliAuthorizationNumber.Control = Me.INDtxtAuthorizationNumber
        Me.INDliAuthorizationNumber.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAuthorizationNumber.Location = New System.Drawing.Point(0, 0)
        Me.INDliAuthorizationNumber.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDliAuthorizationNumber.MinSize = New System.Drawing.Size(410, 36)
        Me.INDliAuthorizationNumber.Name = "INDliAuthorizationNumber"
        Me.INDliAuthorizationNumber.Size = New System.Drawing.Size(410, 36)
        Me.INDliAuthorizationNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAuthorizationNumber.Text = "Numero Autorización"
        Me.INDliAuthorizationNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAuthorizationNumber.TextSize = New System.Drawing.Size(155, 21)
        Me.INDliAuthorizationNumber.TextToControlDistance = 5
        '
        'INDlciAcept
        '
        Me.INDlciAcept.Control = Me.INDsbAcept
        Me.INDlciAcept.CustomizationFormText = "LayoutControlItem2"
        Me.INDlciAcept.Location = New System.Drawing.Point(0, 36)
        Me.INDlciAcept.MaxSize = New System.Drawing.Size(410, 32)
        Me.INDlciAcept.MinSize = New System.Drawing.Size(410, 32)
        Me.INDlciAcept.Name = "INDlciAcept"
        Me.INDlciAcept.Size = New System.Drawing.Size(410, 38)
        Me.INDlciAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAcept.TextToControlDistance = 0
        Me.INDlciAcept.TextVisible = False
        '
        'FrmAuthorizationNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 94)
        Me.Controls.Add(Me.INDlcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmAuthorizationNumber.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAuthorizationNumber"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambiar Número Autorización"
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDtxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAuthorizationNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtxtAuthorizationNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliAuthorizationNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDlciAcept As DevExpress.XtraLayout.LayoutControlItem
End Class
