<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrActivoInactivo
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrActivoInactivo))
        Me.INDchkEstado = New DevExpress.XtraEditors.RadioGroup()
        Me.INDlabelEstado = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblShowInfo = New DevExpress.XtraEditors.LabelControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDchkEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDchkEstado
        '
        resources.ApplyResources(Me.INDchkEstado, "INDchkEstado")
        Me.INDchkEstado.Name = "INDchkEstado"
        Me.INDchkEstado.Properties.AllowFocused = False
        Me.INDchkEstado.Properties.Appearance.BackColor = CType(resources.GetObject("INDchkEstado.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDchkEstado.Properties.Appearance.Font = CType(resources.GetObject("INDchkEstado.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDchkEstado.Properties.Appearance.ForeColor = CType(resources.GetObject("INDchkEstado.Properties.Appearance.ForeColor"), System.Drawing.Color)
        Me.INDchkEstado.Properties.Appearance.Options.UseBackColor = True
        Me.INDchkEstado.Properties.Appearance.Options.UseFont = True
        Me.INDchkEstado.Properties.Appearance.Options.UseForeColor = True
        Me.INDchkEstado.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDchkEstado.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDchkEstado.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDchkEstado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDchkEstado.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDchkEstado.Properties.Items"), Object), resources.GetString("INDchkEstado.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDchkEstado.Properties.Items2"), Object), resources.GetString("INDchkEstado.Properties.Items3"))})
        '
        'INDlabelEstado
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlabelEstado, True)
        Me.INDlabelEstado.Appearance.Font = CType(resources.GetObject("INDlabelEstado.Appearance.Font"), System.Drawing.Font)
        Me.INDlabelEstado.Appearance.ForeColor = CType(resources.GetObject("INDlabelEstado.Appearance.ForeColor"), System.Drawing.Color)
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlabelEstado, False)
        resources.ApplyResources(Me.INDlabelEstado, "INDlabelEstado")
        Me.INDlabelEstado.Name = "INDlabelEstado"
        '
        'INDLblShowInfo
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLblShowInfo, True)
        Me.INDLblShowInfo.Appearance.Font = CType(resources.GetObject("INDLblShowInfo.Appearance.Font"), System.Drawing.Font)
        Me.INDLblShowInfo.Appearance.ForeColor = CType(resources.GetObject("INDLblShowInfo.Appearance.ForeColor"), System.Drawing.Color)
        Me.INDLblShowInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLblShowInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDLblShowInfo.AutoEllipsis = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLblShowInfo, False)
        resources.ApplyResources(Me.INDLblShowInfo, "INDLblShowInfo")
        Me.INDLblShowInfo.Name = "INDLblShowInfo"
        '
        'CtrActivoInactivo
        '
        Me.Appearance.BackColor = CType(resources.GetObject("CtrActivoInactivo.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDLblShowInfo)
        Me.Controls.Add(Me.INDlabelEstado)
        Me.Controls.Add(Me.INDchkEstado)
        Me.Name = "CtrActivoInactivo"
        CType(Me.INDchkEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDchkEstado As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDlabelEstado As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Public WithEvents INDLblShowInfo As DevExpress.XtraEditors.LabelControl

End Class
