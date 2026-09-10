<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrDeterioro
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.LcDeterioro = New DevExpress.XtraLayout.LayoutControl()
        Me.TxtTasaVPN = New DevExpress.XtraEditors.SpinEdit()
        Me.TxtTiempoRecaudo = New DevExpress.XtraEditors.SpinEdit()
        Me.LcgDeterioro = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LciRecaudo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciTasa = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LcDeterioro,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LcDeterioro.SuspendLayout
        CType(Me.TxtTasaVPN.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtTiempoRecaudo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LcgDeterioro,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LciRecaudo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LciTasa,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'LcDeterioro
        '
        Me.LcDeterioro.Controls.Add(Me.TxtTasaVPN)
        Me.LcDeterioro.Controls.Add(Me.TxtTiempoRecaudo)
        Me.LcDeterioro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcDeterioro.Location = New System.Drawing.Point(0, 0)
        Me.LcDeterioro.Name = "LcDeterioro"
        Me.LcDeterioro.Root = Me.LcgDeterioro
        Me.LcDeterioro.Size = New System.Drawing.Size(292, 65)
        Me.LcDeterioro.TabIndex = 0
        Me.LcDeterioro.Text = "LayoutControl1"
        '
        'TxtTasaVPN
        '
        Me.TxtTasaVPN.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.TxtTasaVPN.Location = New System.Drawing.Point(201, 35)
        Me.TxtTasaVPN.Name = "TxtTasaVPN"
        Me.TxtTasaVPN.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TxtTasaVPN.Properties.Appearance.Options.UseFont = true
        Me.TxtTasaVPN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtTasaVPN.Properties.Mask.EditMask = "p4"
        Me.TxtTasaVPN.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.TxtTasaVPN.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.TxtTasaVPN.Size = New System.Drawing.Size(79, 20)
        Me.TxtTasaVPN.StyleController = Me.LcDeterioro
        Me.TxtTasaVPN.TabIndex = 5
        '
        'TxtTiempoRecaudo
        '
        Me.TxtTiempoRecaudo.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.TxtTiempoRecaudo.EnterMoveNextControl = true
        Me.TxtTiempoRecaudo.Location = New System.Drawing.Point(201, 5)
        Me.TxtTiempoRecaudo.Name = "TxtTiempoRecaudo"
        Me.TxtTiempoRecaudo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TxtTiempoRecaudo.Properties.Appearance.Options.UseFont = true
        Me.TxtTiempoRecaudo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtTiempoRecaudo.Properties.Mask.EditMask = "n0"
        Me.TxtTiempoRecaudo.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.TxtTiempoRecaudo.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.TxtTiempoRecaudo.Size = New System.Drawing.Size(79, 20)
        Me.TxtTiempoRecaudo.StyleController = Me.LcDeterioro
        Me.TxtTiempoRecaudo.TabIndex = 4
        '
        'LcgDeterioro
        '
        Me.LcgDeterioro.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgDeterioro.GroupBordersVisible = false
        Me.LcgDeterioro.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LciRecaudo, Me.LciTasa})
        Me.LcgDeterioro.Location = New System.Drawing.Point(0, 0)
        Me.LcgDeterioro.Name = "LcgDeterioro"
        Me.LcgDeterioro.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LcgDeterioro.Size = New System.Drawing.Size(292, 65)
        Me.LcgDeterioro.TextVisible = false
        '
        'LciRecaudo
        '
        Me.LciRecaudo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LciRecaudo.AppearanceItemCaption.Options.UseFont = true
        Me.LciRecaudo.Control = Me.TxtTiempoRecaudo
        Me.LciRecaudo.Location = New System.Drawing.Point(0, 0)
        Me.LciRecaudo.MaxSize = New System.Drawing.Size(280, 30)
        Me.LciRecaudo.MinSize = New System.Drawing.Size(280, 30)
        Me.LciRecaudo.Name = "LciRecaudo"
        Me.LciRecaudo.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 5, 0)
        Me.LciRecaudo.Size = New System.Drawing.Size(292, 30)
        Me.LciRecaudo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciRecaudo.Text = "Tiempo  Recaudo del Flujo Futuro (Meses)"
        Me.LciRecaudo.TextSize = New System.Drawing.Size(193, 13)
        '
        'LciTasa
        '
        Me.LciTasa.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LciTasa.AppearanceItemCaption.Options.UseFont = true
        Me.LciTasa.Control = Me.TxtTasaVPN
        Me.LciTasa.Location = New System.Drawing.Point(0, 30)
        Me.LciTasa.MaxSize = New System.Drawing.Size(280, 30)
        Me.LciTasa.MinSize = New System.Drawing.Size(280, 30)
        Me.LciTasa.Name = "LciTasa"
        Me.LciTasa.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 5, 0)
        Me.LciTasa.Size = New System.Drawing.Size(292, 35)
        Me.LciTasa.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciTasa.Text = "Tasa Definida para Aplicación del VPN"
        Me.LciTasa.TextSize = New System.Drawing.Size(193, 13)
        '
        'CtrDeterioro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LcDeterioro)
        Me.MaximumSize = New System.Drawing.Size(292, 65)
        Me.MinimumSize = New System.Drawing.Size(292, 65)
        Me.Name = "CtrDeterioro"
        Me.Size = New System.Drawing.Size(292, 65)
        CType(Me.LcDeterioro,System.ComponentModel.ISupportInitialize).EndInit
        Me.LcDeterioro.ResumeLayout(false)
        CType(Me.TxtTasaVPN.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtTiempoRecaudo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LcgDeterioro,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LciRecaudo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LciTasa,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents LcDeterioro As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgDeterioro As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TxtTasaVPN As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents TxtTiempoRecaudo As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LciRecaudo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LciTasa As DevExpress.XtraLayout.LayoutControlItem
End Class
