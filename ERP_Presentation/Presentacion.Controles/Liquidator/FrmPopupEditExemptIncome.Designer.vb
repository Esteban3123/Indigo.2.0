<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupEditExemptIncome
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
        Me.INDSbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcDeliveryTime = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSeNewValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLcgPccDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciNewValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDeliveryTime.SuspendLayout()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeNewValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNewValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDSbAccept
        '
        Me.INDSbAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAccept.Appearance.Options.UseFont = True
        Me.INDSbAccept.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAccept.Location = New System.Drawing.Point(0, 225)
        Me.INDSbAccept.Name = "INDSbAccept"
        Me.INDSbAccept.Size = New System.Drawing.Size(452, 36)
        Me.INDSbAccept.TabIndex = 1
        Me.INDSbAccept.Text = "Aceptar"
        '
        'INDLcDeliveryTime
        '
        Me.INDLcDeliveryTime.Controls.Add(Me.INDMeObservations)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDSeNewValue)
        Me.INDLcDeliveryTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDeliveryTime.Location = New System.Drawing.Point(0, 0)
        Me.INDLcDeliveryTime.Name = "INDLcDeliveryTime"
        Me.INDLcDeliveryTime.Root = Me.INDLcgPccDeliveryTime
        Me.INDLcDeliveryTime.Size = New System.Drawing.Size(452, 225)
        Me.INDLcDeliveryTime.TabIndex = 3
        Me.INDLcDeliveryTime.Text = "LayoutControl1"
        '
        'INDMeObservations
        '
        Me.INDMeObservations.EditValue = ""
        Me.INDMeObservations.Location = New System.Drawing.Point(12, 96)
        Me.INDMeObservations.Name = "INDMeObservations"
        Me.INDMeObservations.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDMeObservations.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservations.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservations.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservations.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDMeObservations.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDMeObservations.Size = New System.Drawing.Size(428, 117)
        Me.INDMeObservations.StyleController = Me.INDLcDeliveryTime
        Me.INDMeObservations.TabIndex = 23
        '
        'INDSeNewValue
        '
        Me.INDSeNewValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeNewValue.EnterMoveNextControl = True
        Me.INDSeNewValue.Location = New System.Drawing.Point(12, 38)
        Me.INDSeNewValue.Name = "INDSeNewValue"
        Me.INDSeNewValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeNewValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeNewValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeNewValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeNewValue.Properties.Appearance.Options.UseFont = True
        Me.INDSeNewValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeNewValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeNewValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeNewValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeNewValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeNewValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeNewValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeNewValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeNewValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeNewValue.Properties.EditFormat.FormatString = "c0"
        Me.INDSeNewValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeNewValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeNewValue.Properties.Mask.EditMask = "c0"
        Me.INDSeNewValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeNewValue.Properties.MaxLength = 18
        Me.INDSeNewValue.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDSeNewValue.Properties.NullText = "$0"
        Me.INDSeNewValue.Size = New System.Drawing.Size(428, 28)
        Me.INDSeNewValue.StyleController = Me.INDLcDeliveryTime
        Me.INDSeNewValue.TabIndex = 2
        '
        'INDLcgPccDeliveryTime
        '
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgPccDeliveryTime.GroupBordersVisible = False
        Me.INDLcgPccDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciNewValue, Me.INDLciObservations})
        Me.INDLcgPccDeliveryTime.Name = "Root"
        Me.INDLcgPccDeliveryTime.Size = New System.Drawing.Size(452, 225)
        Me.INDLcgPccDeliveryTime.TextVisible = False
        '
        'INDLciNewValue
        '
        Me.INDLciNewValue.Control = Me.INDSeNewValue
        Me.INDLciNewValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciNewValue.CustomizationFormText = "Valor"
        Me.INDLciNewValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLciNewValue.Name = "INDLciNewValue"
        Me.INDLciNewValue.Size = New System.Drawing.Size(432, 58)
        Me.INDLciNewValue.Text = "Valor"
        Me.INDLciNewValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNewValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNewValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciNewValue.TextToControlDistance = 5
        '
        'INDLciObservations
        '
        Me.INDLciObservations.Control = Me.INDMeObservations
        Me.INDLciObservations.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciObservations.CustomizationFormText = "Observaciones"
        Me.INDLciObservations.Location = New System.Drawing.Point(0, 58)
        Me.INDLciObservations.Name = "INDLciObservations"
        Me.INDLciObservations.Size = New System.Drawing.Size(432, 147)
        Me.INDLciObservations.Text = "Observaciones"
        Me.INDLciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciObservations.TextToControlDistance = 5
        '
        'FrmPopupEditExemptIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 261)
        Me.Controls.Add(Me.INDLcDeliveryTime)
        Me.Controls.Add(Me.INDSbAccept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupEditExemptIncome"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Editar"
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDeliveryTime.ResumeLayout(False)
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeNewValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNewValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDSbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcDeliveryTime As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgPccDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciNewValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSeNewValue As DevExpress.XtraEditors.SpinEdit
End Class
