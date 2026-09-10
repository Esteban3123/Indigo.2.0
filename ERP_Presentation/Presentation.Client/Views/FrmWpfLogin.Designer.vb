<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWpfLogin
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWpfLogin))
        Me.INDglCompanies = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDglvCompanies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCompanyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCompanyNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolProductionCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.FrmLoginWPF1 = New Presentation.Client.FrmLoginWPF()
        CType(Me.INDglCompanies.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglvCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDglCompanies
        '
        Me.INDglCompanies.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.INDglCompanies.Location = New System.Drawing.Point(1442, 241)
        Me.INDglCompanies.Name = "INDglCompanies"
        Me.INDglCompanies.Properties.Appearance.BorderColor = System.Drawing.Color.White
        Me.INDglCompanies.Properties.Appearance.Options.UseBorderColor = True
        Me.INDglCompanies.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.INDglCompanies.Properties.DisplayMember = "Name"
        Me.INDglCompanies.Properties.PopupView = Me.INDglvCompanies
        Me.INDglCompanies.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDglCompanies.Properties.ValueMember = "Code"
        Me.INDglCompanies.Size = New System.Drawing.Size(1, 6)
        Me.INDglCompanies.TabIndex = 5
        Me.INDglCompanies.Visible = False
        '
        'INDglvCompanies
        '
        Me.INDglvCompanies.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDglvCompanies.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDglvCompanies.Appearance.GroupRow.Options.UseFont = True
        Me.INDglvCompanies.Appearance.GroupRow.Options.UseForeColor = True
        Me.INDglvCompanies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDglvCompanies.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDglvCompanies.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDglvCompanies.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.INDglvCompanies.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDglvCompanies.Appearance.Row.Options.UseFont = True
        Me.INDglvCompanies.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCompanyCode, Me.INDcolCompanyName, Me.INDcolCompanyNit, Me.INDcolProductionCompany})
        Me.INDglvCompanies.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDglvCompanies.Name = "INDglvCompanies"
        Me.INDglvCompanies.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDglvCompanies.OptionsView.ShowGroupPanel = False
        '
        'INDcolCompanyCode
        '
        Me.INDcolCompanyCode.Caption = "Codigo"
        Me.INDcolCompanyCode.FieldName = "Code"
        Me.INDcolCompanyCode.MaxWidth = 70
        Me.INDcolCompanyCode.MinWidth = 70
        Me.INDcolCompanyCode.Name = "INDcolCompanyCode"
        Me.INDcolCompanyCode.OptionsColumn.AllowEdit = False
        Me.INDcolCompanyCode.OptionsColumn.AllowFocus = False
        Me.INDcolCompanyCode.Visible = True
        Me.INDcolCompanyCode.VisibleIndex = 0
        Me.INDcolCompanyCode.Width = 70
        '
        'INDcolCompanyName
        '
        Me.INDcolCompanyName.Caption = "Empresa"
        Me.INDcolCompanyName.FieldName = "Name"
        Me.INDcolCompanyName.Name = "INDcolCompanyName"
        Me.INDcolCompanyName.OptionsColumn.AllowEdit = False
        Me.INDcolCompanyName.OptionsColumn.AllowFocus = False
        Me.INDcolCompanyName.Visible = True
        Me.INDcolCompanyName.VisibleIndex = 1
        Me.INDcolCompanyName.Width = 1262
        '
        'INDcolCompanyNit
        '
        Me.INDcolCompanyNit.Caption = "Nit"
        Me.INDcolCompanyNit.FieldName = "CompanyNit"
        Me.INDcolCompanyNit.Name = "INDcolCompanyNit"
        Me.INDcolCompanyNit.OptionsColumn.AllowEdit = False
        Me.INDcolCompanyNit.OptionsColumn.AllowFocus = False
        '
        'INDcolProductionCompany
        '
        Me.INDcolProductionCompany.Caption = "Produccion"
        Me.INDcolProductionCompany.FieldName = "ProductionCompany"
        Me.INDcolProductionCompany.MaxWidth = 90
        Me.INDcolProductionCompany.MinWidth = 90
        Me.INDcolProductionCompany.Name = "INDcolProductionCompany"
        Me.INDcolProductionCompany.OptionsColumn.AllowEdit = False
        Me.INDcolProductionCompany.OptionsColumn.AllowFocus = False
        Me.INDcolProductionCompany.Visible = True
        Me.INDcolProductionCompany.VisibleIndex = 2
        Me.INDcolProductionCompany.Width = 90
        '
        'ElementHost1
        '
        Me.ElementHost1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElementHost1.Location = New System.Drawing.Point(0, 0)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(1602, 807)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Me.FrmLoginWPF1
        '
        'FrmWpfLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1602, 807)
        Me.Controls.Add(Me.INDglCompanies)
        Me.Controls.Add(Me.ElementHost1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmWpfLogin"
        Me.Opacity = 0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Indigo Vie Cloud Platform"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.INDglCompanies.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglvCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend FrmLoginWPF1 As FrmLoginWPF
    Friend WithEvents INDglCompanies As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDglvCompanies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolCompanyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCompanyNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolProductionCompany As DevExpress.XtraGrid.Columns.GridColumn
End Class
