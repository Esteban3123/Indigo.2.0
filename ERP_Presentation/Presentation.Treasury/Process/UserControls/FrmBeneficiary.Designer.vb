<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBeneficiary
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtIdentification = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyBeneficiary = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlgrBeneficiary = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliIdentification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtIdentification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyBeneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlgrBeneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsbAcept)
        Me.LayoutControl1.Controls.Add(Me.INDtxtName)
        Me.LayoutControl1.Controls.Add(Me.INDtxtIdentification)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlyBeneficiary
        Me.LayoutControl1.Size = New System.Drawing.Size(540, 191)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsbAcept
        '
        Me.INDsbAcept.Location = New System.Drawing.Point(24, 132)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAcept, False)
        Me.INDsbAcept.Name = "INDsbAcept"
        Me.INDsbAcept.Size = New System.Drawing.Size(492, 32)
        Me.INDsbAcept.StyleController = Me.LayoutControl1
        Me.INDsbAcept.TabIndex = 6
        Me.INDsbAcept.Text = "Aceptar"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(171, 96)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtName.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtName.StyleController = Me.LayoutControl1
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDtxtIdentification
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtIdentification, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtIdentification, False)
        Me.INDtxtIdentification.EnterMoveNextControl = True
        Me.INDtxtIdentification.Location = New System.Drawing.Point(171, 60)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtIdentification, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtIdentification.Name = "INDtxtIdentification"
        Me.INDtxtIdentification.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtIdentification.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIdentification.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtIdentification.Properties.Appearance.Options.UseFont = True
        Me.INDtxtIdentification.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtIdentification.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtIdentification.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIdentification.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtIdentification.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtIdentification.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtIdentification.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtIdentification.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtIdentification.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtIdentification.StyleController = Me.LayoutControl1
        Me.INDtxtIdentification.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtIdentification, 0)
        '
        'INDlyBeneficiary
        '
        Me.INDlyBeneficiary.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyBeneficiary.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyBeneficiary.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyBeneficiary.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyBeneficiary.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyBeneficiary.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyBeneficiary.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyBeneficiary.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyBeneficiary.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyBeneficiary, False)
        Me.INDlyBeneficiary.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlyBeneficiary.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyBeneficiary.GroupBordersVisible = False
        Me.INDlyBeneficiary.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlgrBeneficiary})
        Me.INDlyBeneficiary.Location = New System.Drawing.Point(0, 0)
        Me.INDlyBeneficiary.Name = "INDlyBeneficiary"
        Me.INDlyBeneficiary.Size = New System.Drawing.Size(540, 191)
        Me.INDlyBeneficiary.Text = "INDlyBeneficiary"
        Me.INDlyBeneficiary.TextVisible = False
        '
        'INDlgrBeneficiary
        '
        Me.INDlgrBeneficiary.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlgrBeneficiary.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlgrBeneficiary.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlgrBeneficiary.AppearanceItemCaption.Options.UseFont = True
        Me.INDlgrBeneficiary.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlgrBeneficiary.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlgrBeneficiary.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlgrBeneficiary.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlgrBeneficiary.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlgrBeneficiary, False)
        Me.INDlgrBeneficiary.CustomizationFormText = "LayoutControlGroup2"
        Me.INDlgrBeneficiary.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliName, Me.INDliIdentification, Me.INDliAcept})
        Me.INDlgrBeneficiary.Location = New System.Drawing.Point(0, 0)
        Me.INDlgrBeneficiary.Name = "INDlgrBeneficiary"
        Me.INDlgrBeneficiary.Size = New System.Drawing.Size(520, 171)
        Me.INDlgrBeneficiary.Text = "Beneficiario"
        '
        'INDliName
        '
        Me.INDliName.Control = Me.INDtxtName
        Me.INDliName.CustomizationFormText = "LayoutControlItem2"
        Me.INDliName.Location = New System.Drawing.Point(0, 36)
        Me.INDliName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliName.Name = "INDliName"
        Me.INDliName.Size = New System.Drawing.Size(496, 36)
        Me.INDliName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliName.Text = "Nombre"
        Me.INDliName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliName.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliName.TextToControlDistance = 12
        '
        'INDliIdentification
        '
        Me.INDliIdentification.Control = Me.INDtxtIdentification
        Me.INDliIdentification.CustomizationFormText = "LayoutControlItem1"
        Me.INDliIdentification.Location = New System.Drawing.Point(0, 0)
        Me.INDliIdentification.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliIdentification.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliIdentification.Name = "INDliIdentification"
        Me.INDliIdentification.Size = New System.Drawing.Size(496, 36)
        Me.INDliIdentification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliIdentification.Text = "Identificación"
        Me.INDliIdentification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliIdentification.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliIdentification.TextToControlDistance = 12
        '
        'INDliAcept
        '
        Me.INDliAcept.Control = Me.INDsbAcept
        Me.INDliAcept.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAcept.Location = New System.Drawing.Point(0, 72)
        Me.INDliAcept.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDliAcept.MinSize = New System.Drawing.Size(1, 36)
        Me.INDliAcept.Name = "INDliAcept"
        Me.INDliAcept.Size = New System.Drawing.Size(496, 39)
        Me.INDliAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAcept.Text = "INDliAcept"
        Me.INDliAcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAcept.TextToControlDistance = 0
        Me.INDliAcept.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20140620"

        '
        'FrmBeneficiary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 191)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmBeneficiary"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "FrmBeneficiary"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtIdentification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyBeneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlgrBeneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyBeneficiary As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtIdentification As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlgrBeneficiary As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliIdentification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDsbAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAcept As DevExpress.XtraLayout.LayoutControlItem
End Class
