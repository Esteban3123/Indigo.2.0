<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrRejillaCustomizable
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
        Me.INDgcCustomizable = New DevExpress.XtraGrid.GridControl()
        Me.INDgcCustomizableView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDTimer = New System.Windows.Forms.Timer()
        CType(Me.INDgcCustomizable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcCustomizableView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDgcCustomizable
        '
        Me.INDgcCustomizable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDgcCustomizable.Location = New System.Drawing.Point(0, 0)
        Me.INDgcCustomizable.MainView = Me.INDgcCustomizableView
        Me.INDgcCustomizable.Name = "INDgcCustomizable"
        Me.INDgcCustomizable.Size = New System.Drawing.Size(469, 262)
        Me.INDgcCustomizable.TabIndex = 0
        Me.INDgcCustomizable.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcCustomizableView})
        '
        'INDgcCustomizableView
        '
        Me.INDgcCustomizableView.GridControl = Me.INDgcCustomizable
        Me.INDgcCustomizableView.Name = "INDgcCustomizableView"
        Me.INDgcCustomizableView.OptionsBehavior.Editable = False
        Me.INDgcCustomizableView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcCustomizableView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcCustomizableView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgcCustomizableView.OptionsView.ShowGroupPanel = False
        '
        'INDTimer
        '
        Me.INDTimer.Interval = 1
        '
        'CtrRejillaCustomizable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDgcCustomizable)
        Me.Name = "CtrRejillaCustomizable"
        Me.Size = New System.Drawing.Size(469, 262)
        CType(Me.INDgcCustomizable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcCustomizableView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDgcCustomizableView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTimer As System.Windows.Forms.Timer
    Public WithEvents INDgcCustomizable As DevExpress.XtraGrid.GridControl

End Class
