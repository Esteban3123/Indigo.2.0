<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostActivityStep
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDseOrder = New DevExpress.XtraEditors.SpinEdit()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDseOrder.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDmeDescription.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciDescription,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciOrder,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDseOrder)
        Me.INDlcRoot.Controls.Add(Me.INDmeDescription)
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 210)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDseOrder
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseOrder, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseOrder, true)
        Me.INDseOrder.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseOrder.EnterMoveNextControl = true
        Me.INDseOrder.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDseOrder, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseOrder.Name = "INDseOrder"
        Me.INDseOrder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseOrder.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseOrder.Properties.Appearance.Options.UseBackColor = true
        Me.INDseOrder.Properties.Appearance.Options.UseFont = true
        Me.INDseOrder.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDseOrder.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDseOrder.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseOrder.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDseOrder.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDseOrder.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDseOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseOrder.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseOrder.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseOrder.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseOrder.Properties.MaxLength = 9
        Me.INDseOrder.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseOrder.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseOrder.Size = New System.Drawing.Size(386, 28)
        Me.INDseOrder.StyleController = Me.INDlcRoot
        Me.INDseOrder.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseOrder, 0)
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, false)
        Me.INDmeDescription.EnterMoveNextControl = true
        Me.INDmeDescription.Location = New System.Drawing.Point(12, 92)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = true
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = true
        Me.INDmeDescription.Size = New System.Drawing.Size(387, 70)
        Me.INDmeDescription.StyleController = Me.INDlcRoot
        Me.INDmeDescription.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDsbAdd.Appearance.Options.UseFont = true
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 166)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = true
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, false)
        Me.INDlcgRoot.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = false
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciAdd, Me.INDlciDescription, Me.INDlciOrder})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "Root"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 210)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 154)
        Me.INDlciAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.Name = "INDlciAdd"
        Me.INDlciAdd.Size = New System.Drawing.Size(391, 36)
        Me.INDlciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAdd.TextToControlDistance = 0
        Me.INDlciAdd.TextVisible = false
        '
        'INDlciDescription
        '
        Me.INDlciDescription.AllowHide = false
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 60)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.ShowInCustomizationForm = false
        Me.INDlciDescription.Size = New System.Drawing.Size(391, 94)
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(75, 17)
        '
        'INDlciOrder
        '
        Me.INDlciOrder.AllowHide = false
        Me.INDlciOrder.Control = Me.INDseOrder
        Me.INDlciOrder.Location = New System.Drawing.Point(0, 0)
        Me.INDlciOrder.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciOrder.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciOrder.Name = "INDlciOrder"
        Me.INDlciOrder.ShowInCustomizationForm = false
        Me.INDlciOrder.Size = New System.Drawing.Size(391, 60)
        Me.INDlciOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOrder.Text = "Orden"
        Me.INDlciOrder.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOrder.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciOrder.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciOrder.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrCostActivityStep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrCostActivityStep"
        Me.Size = New System.Drawing.Size(411, 210)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDseOrder.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDmeDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciDescription,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciOrder,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseOrder As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
End Class
