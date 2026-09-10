<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrStatusTransactions
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlblHeon = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblVie = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblNameHeon = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblNameVie = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlblHeon)
        Me.LayoutControl1.Controls.Add(Me.INDlblVie)
        Me.LayoutControl1.Controls.Add(Me.INDlblNameHeon)
        Me.LayoutControl1.Controls.Add(Me.INDlblNameVie)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlblHeon
        '
        Me.INDlblHeon.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlblHeon.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblHeon.Location = New System.Drawing.Point(173, 33)
        Me.INDlblHeon.Name = "INDlblHeon"
        Me.INDlblHeon.Size = New System.Drawing.Size(81, 17)
        Me.INDlblHeon.StyleController = Me.LayoutControl1
        Me.INDlblHeon.TabIndex = 7
        Me.INDlblHeon.Text = "LabelControl3"
        '
        'INDlblVie
        '
        Me.INDlblVie.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlblVie.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblVie.Location = New System.Drawing.Point(173, 12)
        Me.INDlblVie.Name = "INDlblVie"
        Me.INDlblVie.Size = New System.Drawing.Size(81, 17)
        Me.INDlblVie.StyleController = Me.LayoutControl1
        Me.INDlblVie.TabIndex = 6
        Me.INDlblVie.Text = "LabelControl3"
        '
        'INDlblNameHeon
        '
        Me.INDlblNameHeon.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlblNameHeon.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblNameHeon.Location = New System.Drawing.Point(12, 33)
        Me.INDlblNameHeon.Name = "INDlblNameHeon"
        Me.INDlblNameHeon.Size = New System.Drawing.Size(109, 17)
        Me.INDlblNameHeon.StyleController = Me.LayoutControl1
        Me.INDlblNameHeon.TabIndex = 5
        Me.INDlblNameHeon.Text = "Transacción HEON"
        '
        'INDlblNameVie
        '
        Me.INDlblNameVie.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlblNameVie.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblNameVie.Location = New System.Drawing.Point(12, 12)
        Me.INDlblNameVie.Name = "INDlblNameVie"
        Me.INDlblNameVie.Size = New System.Drawing.Size(132, 17)
        Me.INDlblNameVie.StyleController = Me.LayoutControl1
        Me.INDlblNameVie.TabIndex = 4
        Me.INDlblNameVie.Text = "Transacción Indigo Vie"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlblNameVie
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(136, 21)
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDlblNameHeon
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 21)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(136, 21)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDlblVie
        Me.LayoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.LayoutControlItem3.Location = New System.Drawing.Point(136, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(136, 21)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDlblHeon
        Me.LayoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.LayoutControlItem4.Location = New System.Drawing.Point(136, 21)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(136, 21)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'CtrStatusTransactions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrStatusTransactions"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlblNameVie As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblNameHeon As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblVie As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblHeon As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
