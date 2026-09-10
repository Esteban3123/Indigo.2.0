<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfigurationConection
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
        Me.components = New System.ComponentModel.Container()
        Me.CompletionWizardPage1 = New DevExpress.XtraWizard.CompletionWizardPage()
        Me.WelcomeWizardPage1 = New DevExpress.XtraWizard.WelcomeWizardPage()
        Me.WizardControl1 = New DevExpress.XtraWizard.WizardControl()
        Me.WizardPage1 = New DevExpress.XtraWizard.WizardPage()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardControl1.SuspendLayout()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CompletionWizardPage1
        '
        Me.CompletionWizardPage1.Name = "CompletionWizardPage1"
        Me.CompletionWizardPage1.Size = New System.Drawing.Size(603, 334)
        '
        'WelcomeWizardPage1
        '
        Me.WelcomeWizardPage1.Name = "WelcomeWizardPage1"
        Me.WelcomeWizardPage1.Size = New System.Drawing.Size(603, 334)
        Me.WelcomeWizardPage1.Text = "Asistente de configuracion Indigo Vie EHR"
        '
        'WizardControl1
        '
        Me.WizardControl1.Controls.Add(Me.WelcomeWizardPage1)
        Me.WizardControl1.Controls.Add(Me.CompletionWizardPage1)
        Me.WizardControl1.Controls.Add(Me.WizardPage1)
        Me.WizardControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WizardControl1.Location = New System.Drawing.Point(0, 0)
        Me.WizardControl1.Name = "WizardControl1"
        Me.WizardControl1.Pages.AddRange(New DevExpress.XtraWizard.BaseWizardPage() {Me.WelcomeWizardPage1, Me.WizardPage1, Me.CompletionWizardPage1})
        Me.WizardControl1.Size = New System.Drawing.Size(820, 467)
        '
        'WizardPage1
        '
        Me.WizardPage1.Name = "WizardPage1"
        Me.WizardPage1.Size = New System.Drawing.Size(788, 322)
        '
        'GridControl1
        '
        Me.IndigoGridControl1.SetAddActions(Me.GridControl1, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GridControl1, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.GridControl1, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GridControl1, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GridControl1, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GridControl1, False)
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(400, 200)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GridControl1, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GridControl1, New System.Drawing.Size(0, 0))
        Me.GridControl1.TabIndex = 1
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'FrmConfigurationConection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 467)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.WizardControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmConfigurationConection"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Configurar"
        CType(Me.WizardControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardControl1.ResumeLayout(False)
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CompletionWizardPage1 As DevExpress.XtraWizard.CompletionWizardPage
    Friend WithEvents WelcomeWizardPage1 As DevExpress.XtraWizard.WelcomeWizardPage
    Friend WithEvents WizardControl1 As DevExpress.XtraWizard.WizardControl
    Friend WithEvents WizardPage1 As DevExpress.XtraWizard.WizardPage
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
