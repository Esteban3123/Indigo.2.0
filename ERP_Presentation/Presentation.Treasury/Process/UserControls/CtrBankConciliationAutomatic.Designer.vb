<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrBankConciliationAutomatic
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LFinalStatementBalance = New System.Windows.Forms.Label()
        Me.LDifferenceReconcile = New System.Windows.Forms.Label()
        Me.LMonth = New System.Windows.Forms.Label()
        Me.LEntityBankAccount = New System.Windows.Forms.Label()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LFinalStatementBalance)
        Me.LayoutControl1.Controls.Add(Me.LDifferenceReconcile)
        Me.LayoutControl1.Controls.Add(Me.LMonth)
        Me.LayoutControl1.Controls.Add(Me.LEntityBankAccount)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(339, 0, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(347, 76)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LFinalStatementBalance
        '
        Me.LFinalStatementBalance.BackColor = System.Drawing.Color.Transparent
        Me.LFinalStatementBalance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LFinalStatementBalance.Location = New System.Drawing.Point(2, 56)
        Me.LFinalStatementBalance.MaximumSize = New System.Drawing.Size(310, 17)
        Me.LFinalStatementBalance.Name = "LFinalStatementBalance"
        Me.LFinalStatementBalance.Size = New System.Drawing.Size(310, 17)
        Me.LFinalStatementBalance.TabIndex = 15
        Me.LFinalStatementBalance.Text = "Saldo final extracto: "
        '
        'LDifferenceReconcile
        '
        Me.LDifferenceReconcile.BackColor = System.Drawing.Color.Transparent
        Me.LDifferenceReconcile.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LDifferenceReconcile.Location = New System.Drawing.Point(2, 38)
        Me.LDifferenceReconcile.MaximumSize = New System.Drawing.Size(310, 17)
        Me.LDifferenceReconcile.Name = "LDifferenceReconcile"
        Me.LDifferenceReconcile.Size = New System.Drawing.Size(310, 14)
        Me.LDifferenceReconcile.TabIndex = 14
        Me.LDifferenceReconcile.Text = "Diferencia a conciliar:"
        '
        'LMonth
        '
        Me.LMonth.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LMonth.Location = New System.Drawing.Point(2, 20)
        Me.LMonth.MaximumSize = New System.Drawing.Size(310, 15)
        Me.LMonth.Name = "LMonth"
        Me.LMonth.Size = New System.Drawing.Size(310, 14)
        Me.LMonth.TabIndex = 13
        Me.LMonth.Text = "Mes:"
        '
        'LEntityBankAccount
        '
        Me.LEntityBankAccount.BackColor = System.Drawing.Color.Transparent
        Me.LEntityBankAccount.Font = New System.Drawing.Font("Segoe UI Light", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LEntityBankAccount.Location = New System.Drawing.Point(2, 2)
        Me.LEntityBankAccount.MaximumSize = New System.Drawing.Size(310, 15)
        Me.LEntityBankAccount.Name = "LEntityBankAccount"
        Me.LEntityBankAccount.Size = New System.Drawing.Size(310, 14)
        Me.LEntityBankAccount.TabIndex = 12
        Me.LEntityBankAccount.Text = "Cuenta bancaria:"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem2, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(347, 76)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LEntityBankAccount
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(24, 18)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(347, 18)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LMonth
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 18)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(24, 18)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(347, 18)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LDifferenceReconcile
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(24, 18)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(347, 18)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.LFinalStatementBalance
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 54)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(24, 20)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(347, 22)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'CtrBankConciliationAutomatic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LayoutControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrBankConciliationAutomatic"
        Me.Size = New System.Drawing.Size(347, 76)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents LMonth As Label
    Friend WithEvents LEntityBankAccount As Label
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LFinalStatementBalance As Label
    Friend WithEvents LDifferenceReconcile As Label
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
