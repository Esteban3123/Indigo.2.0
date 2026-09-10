Imports System.Windows.Forms
Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardDesigner
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDashboardDesigner))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.CtrDashBoardDesign1 = New Presentation.Controls.CtrDashBoardDesign()
        Me.SuspendLayout()
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "xml"
        Me.SaveFileDialog1.Filter = "Archivo de Diseño (*.xml)|*.xml"
        '
        'CtrDashBoardDesign1
        '
        Me.CtrDashBoardDesign1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrDashBoardDesign1.Location = New System.Drawing.Point(0, 0)
        Me.CtrDashBoardDesign1.Name = "CtrDashBoardDesign1"
        Me.CtrDashBoardDesign1.Size = New System.Drawing.Size(1167, 606)
        Me.CtrDashBoardDesign1.TabIndex = 1
        '
        'FrmDashboardDesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1167, 606)
        Me.Controls.Add(Me.CtrDashBoardDesign1)
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmDashboardDesigner.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.Name = "FrmDashboardDesigner"
        Me.Text = "Balanced Scorecard"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrDashBoardDesign1 As CtrDashBoardDesign
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
