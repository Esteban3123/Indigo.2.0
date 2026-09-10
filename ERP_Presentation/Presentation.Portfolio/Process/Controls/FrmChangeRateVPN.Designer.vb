<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmChangeRateVPN
    Inherits DevExpress.XtraEditors.XtraForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.TxtVPNRate = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BtnAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.TxtVPNRate.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.TxtVPNRate)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(360, 86)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'TxtVPNRate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtVPNRate, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtVPNRate, false)
        Me.TxtVPNRate.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.TxtVPNRate.EnterMoveNextControl = true
        Me.TxtVPNRate.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.TxtVPNRate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtVPNRate.Name = "TxtVPNRate"
        Me.TxtVPNRate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TxtVPNRate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.TxtVPNRate.Properties.Appearance.Options.UseBackColor = true
        Me.TxtVPNRate.Properties.Appearance.Options.UseFont = true
        Me.TxtVPNRate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.TxtVPNRate.Properties.AppearanceFocused.Options.UseFont = true
        Me.TxtVPNRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtVPNRate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.TxtVPNRate.Properties.Mask.EditMask = "P4"
        Me.TxtVPNRate.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.TxtVPNRate.Size = New System.Drawing.Size(336, 28)
        Me.TxtVPNRate.StyleController = Me.LayoutControl1
        Me.TxtVPNRate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtVPNRate, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(360, 86)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.TxtVPNRate
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(340, 66)
        Me.LayoutControlItem1.Text = "Valor Tasa VPN"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(98, 17)
        '
        'BtnAcept
        '
        Me.BtnAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.BtnAcept.Appearance.Options.UseFont = true
        Me.BtnAcept.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnAcept.Location = New System.Drawing.Point(0, 86)
        Me.BtnAcept.Name = "BtnAcept"
        Me.BtnAcept.Size = New System.Drawing.Size(360, 28)
        Me.BtnAcept.TabIndex = 1
        Me.BtnAcept.Text = "ACEPTAR"
        '
        'FrmChangeRateVPN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 114)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.BtnAcept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmChangeRateVPN"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cambiar Tasa VPN"
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.TxtVPNRate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BtnAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents TxtVPNRate As DevExpress.XtraEditors.SpinEdit
End Class
