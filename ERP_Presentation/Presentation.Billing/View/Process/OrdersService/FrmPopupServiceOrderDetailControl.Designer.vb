<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupServiceOrderDetailControl
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
        Me.LcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAddServiceOrderDetailControl = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMeServiceOrderDetailControlJustify = New DevExpress.XtraEditors.MemoEdit()
        Me.LcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciServiceOrderDetailControlJustify = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LcRoot.SuspendLayout()
        CType(Me.INDMeServiceOrderDetailControlJustify.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceOrderDetailControlJustify, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LcRoot
        '
        Me.LcRoot.Controls.Add(Me.INDbtnAddServiceOrderDetailControl)
        Me.LcRoot.Controls.Add(Me.INDMeServiceOrderDetailControlJustify)
        Me.LcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcRoot.Name = "LcRoot"
        Me.LcRoot.Root = Me.LcgRoot
        Me.LcRoot.Size = New System.Drawing.Size(413, 206)
        Me.LcRoot.TabIndex = 0
        Me.LcRoot.Text = "LayoutControl1"
        '
        'INDbtnAddServiceOrderDetailControl
        '
        Me.INDbtnAddServiceOrderDetailControl.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddServiceOrderDetailControl.Appearance.Options.UseFont = True
        Me.INDbtnAddServiceOrderDetailControl.Location = New System.Drawing.Point(12, 162)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddServiceOrderDetailControl, True)
        Me.INDbtnAddServiceOrderDetailControl.Name = "INDbtnAddServiceOrderDetailControl"
        Me.INDbtnAddServiceOrderDetailControl.Size = New System.Drawing.Size(386, 30)
        Me.INDbtnAddServiceOrderDetailControl.StyleController = Me.LcRoot
        Me.INDbtnAddServiceOrderDetailControl.TabIndex = 6
        Me.INDbtnAddServiceOrderDetailControl.Text = "Aceptar"
        '
        'INDMeServiceOrderDetailControlJustify
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeServiceOrderDetailControlJustify, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeServiceOrderDetailControlJustify, False)
        Me.INDMeServiceOrderDetailControlJustify.EnterMoveNextControl = True
        Me.INDMeServiceOrderDetailControlJustify.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeServiceOrderDetailControlJustify, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeServiceOrderDetailControlJustify.Name = "INDMeServiceOrderDetailControlJustify"
        Me.INDMeServiceOrderDetailControlJustify.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeServiceOrderDetailControlJustify.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeServiceOrderDetailControlJustify.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeServiceOrderDetailControlJustify.Properties.Appearance.Options.UseFont = True
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeServiceOrderDetailControlJustify.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeServiceOrderDetailControlJustify.Size = New System.Drawing.Size(386, 120)
        Me.INDMeServiceOrderDetailControlJustify.StyleController = Me.LcRoot
        Me.INDMeServiceOrderDetailControlJustify.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeServiceOrderDetailControlJustify, 0)
        '
        'LcgRoot
        '
        Me.LcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceGroup.Options.UseFont = True
        Me.LcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgRoot, False)
        Me.LcgRoot.CustomizationFormText = "LcgRoot"
        Me.LcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgRoot.GroupBordersVisible = False
        Me.LcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciServiceOrderDetailControlJustify, Me.LiAcept})
        Me.LcgRoot.Name = "LcgRoot"
        Me.LcgRoot.Size = New System.Drawing.Size(413, 206)
        Me.LcgRoot.TextVisible = False
        '
        'INDLciServiceOrderDetailControlJustify
        '
        Me.INDLciServiceOrderDetailControlJustify.Control = Me.INDMeServiceOrderDetailControlJustify
        Me.INDLciServiceOrderDetailControlJustify.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciServiceOrderDetailControlJustify.Location = New System.Drawing.Point(0, 0)
        Me.INDLciServiceOrderDetailControlJustify.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDLciServiceOrderDetailControlJustify.MinSize = New System.Drawing.Size(390, 150)
        Me.INDLciServiceOrderDetailControlJustify.Name = "INDLciServiceOrderDetailControlJustify"
        Me.INDLciServiceOrderDetailControlJustify.Size = New System.Drawing.Size(393, 150)
        Me.INDLciServiceOrderDetailControlJustify.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciServiceOrderDetailControlJustify.Text = "Descripción"
        Me.INDLciServiceOrderDetailControlJustify.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciServiceOrderDetailControlJustify.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciServiceOrderDetailControlJustify.TextSize = New System.Drawing.Size(133, 21)
        Me.INDLciServiceOrderDetailControlJustify.TextToControlDistance = 5
        '
        'LiAcept
        '
        Me.LiAcept.Control = Me.INDbtnAddServiceOrderDetailControl
        Me.LiAcept.CustomizationFormText = "LiAcept"
        Me.LiAcept.Location = New System.Drawing.Point(0, 150)
        Me.LiAcept.MaxSize = New System.Drawing.Size(390, 34)
        Me.LiAcept.MinSize = New System.Drawing.Size(390, 34)
        Me.LiAcept.Name = "LiAcept"
        Me.LiAcept.Size = New System.Drawing.Size(393, 36)
        Me.LiAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiAcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.LiAcept.TextToControlDistance = 0
        Me.LiAcept.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPopupServiceOrderDetailControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 206)
        Me.Controls.Add(Me.LcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupServiceOrderDetailControl"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Justificación Control de Autorización"
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LcRoot.ResumeLayout(False)
        CType(Me.INDMeServiceOrderDetailControlJustify.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceOrderDetailControlJustify, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDMeServiceOrderDetailControlJustify As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciServiceOrderDetailControlJustify As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDbtnAddServiceOrderDetailControl As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LiAcept As DevExpress.XtraLayout.LayoutControlItem
End Class
