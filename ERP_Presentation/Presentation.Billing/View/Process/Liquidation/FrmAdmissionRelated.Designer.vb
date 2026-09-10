<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAdmissionRelated
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAdmissionRelated))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDSleAdmissionNumber2 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.SearchLookUpEditExAdmissionView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExAdmissionView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(22, 21)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(197, 25)
        Me.LabelControl1.TabIndex = 19
        Me.LabelControl1.Text = "Ingresos Relacionados"
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(728, 15)
        Me.MarqueeProgressBarControl1.TabIndex = 20
        Me.MarqueeProgressBarControl1.Visible = False
        '
        'INDSleAdmissionNumber2
        '
        Me.INDSleAdmissionNumber2.AllowQueryOne = True
        Me.INDSleAdmissionNumber2.Datasource = Nothing
        Me.INDSleAdmissionNumber2.DisplayMember = "{FullNameAdmission}"
        Me.INDSleAdmissionNumber2.DisplayNullText = ""
        Me.INDSleAdmissionNumber2.EditValue = Nothing
        Me.INDSleAdmissionNumber2.EnterMoveNextControl = True
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleAdmissionNumber2.IdOpenForm = 0
        Me.INDSleAdmissionNumber2.IsReadOnly = False
        Me.INDSleAdmissionNumber2.Location = New System.Drawing.Point(22, 62)
        Me.INDSleAdmissionNumber2.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleAdmissionNumber2.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleAdmissionNumber2.Name = "INDSleAdmissionNumber2"
        Me.INDSleAdmissionNumber2.PopupContainerControl = Nothing
        Me.INDSleAdmissionNumber2.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.INDSleAdmissionNumber2.Size = New System.Drawing.Size(684, 28)
        Me.INDSleAdmissionNumber2.TabIndex = 18
        Me.INDSleAdmissionNumber2.ValueMember = "AdmissionCode"
        Me.INDSleAdmissionNumber2.View = Me.SearchLookUpEditExAdmissionView1
        '
        'SearchLookUpEditExAdmissionView1
        '
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExAdmissionView1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExAdmissionView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExAdmissionView1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExAdmissionView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExAdmissionView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.SearchLookUpEditExAdmissionView1.Name = "SearchLookUpEditExAdmissionView1"
        Me.SearchLookUpEditExAdmissionView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No. Ingreso"
        Me.GridColumn1.FieldName = "AdmissionCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Identificación"
        Me.GridColumn2.FieldName = "PatientCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Paciente"
        Me.GridColumn3.FieldName = "PatientName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "T. Ingreso"
        Me.GridColumn4.FieldName = "AdmissionType"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha Ingreso"
        Me.GridColumn5.FieldName = "AdmissionDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Estancia (Cama)"
        Me.GridColumn6.FieldName = "BedStay"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'FrmAdmissionRelated
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 111)
        Me.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.INDSleAdmissionNumber2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmAdmissionRelated.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAdmissionRelated"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ingresos Relacionados"
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExAdmissionView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDSleAdmissionNumber2 As Controls.SearchLookUpEditExAdmission
    Friend WithEvents SearchLookUpEditExAdmissionView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
End Class
