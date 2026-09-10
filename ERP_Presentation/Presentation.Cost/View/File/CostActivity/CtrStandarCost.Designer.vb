Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrStandarCost
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
        Me.INDLStandarCostValue = New System.Windows.Forms.Label()
        Me.INDLTitle = New System.Windows.Forms.Label()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTitle = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStandarCostValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStandarCostValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDLStandarCostValue)
        Me.LayoutControl1.Controls.Add(Me.INDLTitle)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(339, 0, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(347, 88)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLStandarCostValue
        '
        Me.INDLStandarCostValue.BackColor = System.Drawing.Color.Transparent
        Me.INDLStandarCostValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLStandarCostValue.Location = New System.Drawing.Point(12, 45)
        Me.INDLStandarCostValue.Name = "INDLStandarCostValue"
        Me.INDLStandarCostValue.Size = New System.Drawing.Size(310, 31)
        Me.INDLStandarCostValue.TabIndex = 14
        Me.INDLStandarCostValue.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'INDLTitle
        '
        Me.INDLTitle.BackColor = System.Drawing.Color.Transparent
        Me.INDLTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLTitle.Location = New System.Drawing.Point(12, 12)
        Me.INDLTitle.Name = "INDLTitle"
        Me.INDLTitle.Size = New System.Drawing.Size(310, 29)
        Me.INDLTitle.TabIndex = 12
        Me.INDLTitle.Text = "Costo Estándar Promedio"
        Me.INDLTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTitle, Me.INDLciStandarCostValue})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(347, 88)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciTitle
        '
        Me.INDLciTitle.Control = Me.INDLTitle
        Me.INDLciTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTitle.MaxSize = New System.Drawing.Size(314, 35)
        Me.INDLciTitle.MinSize = New System.Drawing.Size(24, 1)
        Me.INDLciTitle.Name = "INDLciTitle"
        Me.INDLciTitle.Size = New System.Drawing.Size(327, 33)
        Me.INDLciTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTitle.TextVisible = False
        '
        'INDLciStandarCostValue
        '
        Me.INDLciStandarCostValue.Control = Me.INDLStandarCostValue
        Me.INDLciStandarCostValue.Location = New System.Drawing.Point(0, 33)
        Me.INDLciStandarCostValue.MaxSize = New System.Drawing.Size(314, 35)
        Me.INDLciStandarCostValue.MinSize = New System.Drawing.Size(24, 1)
        Me.INDLciStandarCostValue.Name = "INDLciStandarCostValue"
        Me.INDLciStandarCostValue.Size = New System.Drawing.Size(327, 35)
        Me.INDLciStandarCostValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStandarCostValue.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciStandarCostValue.TextVisible = False
        '
        'CtrStandarCost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LayoutControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrStandarCost"
        Me.Size = New System.Drawing.Size(347, 88)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStandarCostValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDLTitle As Label
    Friend WithEvents INDLciTitle As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLStandarCostValue As Label
    Friend WithEvents INDLciStandarCostValue As DevExpress.XtraLayout.LayoutControlItem
End Class
