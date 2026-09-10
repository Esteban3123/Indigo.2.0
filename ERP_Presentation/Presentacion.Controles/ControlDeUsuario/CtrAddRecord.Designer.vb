<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAddRecord
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrAddRecord))
        Me.INDpceAddRecord = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDbtnAddRecord = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDpceAddRecord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpceAddRecord
        '
        Me.INDpceAddRecord.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDpceAddRecord.Location = New System.Drawing.Point(0, 70)
        Me.INDpceAddRecord.Name = "INDpceAddRecord"
        Me.INDpceAddRecord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDpceAddRecord.Size = New System.Drawing.Size(90, 20)
        Me.INDpceAddRecord.TabIndex = 0
        '
        'INDbtnAddRecord
        '
        Me.INDbtnAddRecord.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbtnAddRecord.Appearance.Options.UseFont = True
        Me.INDbtnAddRecord.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnAddRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddRecord.Image = CType(resources.GetObject("INDbtnAddRecord.Image"), System.Drawing.Image)
        Me.INDbtnAddRecord.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.INDbtnAddRecord.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnAddRecord.Name = "INDbtnAddRecord"
        Me.INDbtnAddRecord.Size = New System.Drawing.Size(90, 90)
        Me.INDbtnAddRecord.TabIndex = 1
        Me.INDbtnAddRecord.Text = "Agregar"
        Me.INDbtnAddRecord.ToolTip = "Agregar Nuevo Registro"
        '
        'CtrAddRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDbtnAddRecord)
        Me.Controls.Add(Me.INDpceAddRecord)
        Me.MaximumSize = New System.Drawing.Size(90, 90)
        Me.MinimumSize = New System.Drawing.Size(90, 90)
        Me.Name = "CtrAddRecord"
        Me.Size = New System.Drawing.Size(90, 90)
        CType(Me.INDpceAddRecord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents INDpceAddRecord As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDbtnAddRecord As DevExpress.XtraEditors.SimpleButton

End Class
