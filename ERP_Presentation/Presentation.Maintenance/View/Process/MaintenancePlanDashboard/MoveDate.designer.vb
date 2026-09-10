<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MoveDate
    Inherits System.Windows.Forms.Form

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
        Me.DateNavigator1 = New DevExpress.XtraScheduler.DateNavigator()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateNavigator1
        '
        Me.DateNavigator1.DateTime = New Date(CType(0, Long))
        Me.DateNavigator1.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.[Default]
        Me.DateNavigator1.HotDate = Nothing
        Me.DateNavigator1.Location = New System.Drawing.Point(29, 22)
        Me.DateNavigator1.Multiselect = False
        Me.DateNavigator1.Name = "DateNavigator1"
        Me.DateNavigator1.CellPadding = New System.Windows.Forms.Padding(2)
        Me.DateNavigator1.Size = New System.Drawing.Size(179, 175)
        Me.DateNavigator1.TabIndex = 0
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SimpleButton1.Location = New System.Drawing.Point(0, 211)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(238, 23)
        Me.SimpleButton1.TabIndex = 1
        Me.SimpleButton1.Text = "Aceptar"
        '
        'MoveDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(238, 234)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.DateNavigator1)
        Me.Name = "MoveDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MoveDate"
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateNavigator1 As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
End Class
