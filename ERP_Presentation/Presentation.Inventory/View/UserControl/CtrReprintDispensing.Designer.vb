<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrReprintDispensing
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
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDpceRealizedDispensing = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceRealizedDispensing.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 57)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 57)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDpceRealizedDispensing
        '
        Me.INDpceRealizedDispensing.CausesValidation = False
        Me.INDpceRealizedDispensing.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceRealizedDispensing.EditValue = "Dispensaciones Realizadas"
        Me.INDpceRealizedDispensing.Location = New System.Drawing.Point(0, 9)
        Me.INDpceRealizedDispensing.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceRealizedDispensing.Name = "INDpceRealizedDispensing"
        Me.INDpceRealizedDispensing.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceRealizedDispensing.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.INDpceRealizedDispensing.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceRealizedDispensing.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceRealizedDispensing.Properties.Appearance.Options.UseFont = True
        Me.INDpceRealizedDispensing.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceRealizedDispensing.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceRealizedDispensing.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDpceRealizedDispensing.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceRealizedDispensing.Properties.Mask.IgnoreMaskBlank = False
        Me.INDpceRealizedDispensing.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDpceRealizedDispensing.Properties.PopupSizeable = False
        Me.INDpceRealizedDispensing.Properties.ShowPopupCloseButton = False
        Me.INDpceRealizedDispensing.Properties.ShowPopupShadow = False
        Me.INDpceRealizedDispensing.Size = New System.Drawing.Size(292, 34)
        Me.INDpceRealizedDispensing.StyleController = Me.LayoutControl1
        Me.INDpceRealizedDispensing.TabIndex = 11
        '
        'CtrReprintDispensing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.INDpceRealizedDispensing)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(292, 57)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrReprintDispensing"
        Me.Size = New System.Drawing.Size(292, 57)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceRealizedDispensing.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDpceRealizedDispensing As DevExpress.XtraEditors.PopupContainerEdit
End Class
