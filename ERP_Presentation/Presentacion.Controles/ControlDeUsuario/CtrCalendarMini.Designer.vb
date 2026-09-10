<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCalendarMini
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
        Me.INDpcMain = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDpcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpcMain
        '
        Me.INDpcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcMain.Location = New System.Drawing.Point(0, 0)
        Me.INDpcMain.Name = "INDpcMain"
        Me.INDpcMain.Size = New System.Drawing.Size(190, 200)
        Me.INDpcMain.TabIndex = 0
        '
        'CtrCalendarMini
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpcMain)
        Me.Name = "CtrCalendarMini"
        Me.Size = New System.Drawing.Size(190, 200)
        CType(Me.INDpcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpcMain As DevExpress.XtraEditors.PanelControl

End Class
