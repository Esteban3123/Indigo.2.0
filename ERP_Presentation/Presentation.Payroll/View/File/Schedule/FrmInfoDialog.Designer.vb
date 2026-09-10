<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInfoDialog
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcInfoDialog = New DevExpress.XtraGrid.GridControl()
        Me.INDgvInfoDialog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColErrorEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColErrorDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColErrorIcon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepIconScheduleDetail = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDColErrorTemplate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColErrorMessege = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcInfoDialog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvInfoDialog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepIconScheduleDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDBtnOK)
        Me.LayoutControl1.Controls.Add(Me.INDgcInfoDialog)
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(734, 461)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDBtnOK
        '
        Me.INDBtnOK.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnOK.Appearance.Options.UseFont = True
        Me.INDBtnOK.Location = New System.Drawing.Point(12, 417)
        Me.INDBtnOK.Name = "INDBtnOK"
        Me.INDBtnOK.Size = New System.Drawing.Size(710, 32)
        Me.INDBtnOK.StyleController = Me.LayoutControl1
        Me.INDBtnOK.TabIndex = 6
        Me.INDBtnOK.Text = "OK"
        '
        'INDgcInfoDialog
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInfoDialog, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInfoDialog, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInfoDialog, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInfoDialog, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInfoDialog, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInfoDialog, False)
        Me.INDgcInfoDialog.Location = New System.Drawing.Point(12, 46)
        Me.INDgcInfoDialog.MainView = Me.INDgvInfoDialog
        Me.INDgcInfoDialog.Name = "INDgcInfoDialog"
        Me.INDgcInfoDialog.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepIconScheduleDetail})
        Me.INDgcInfoDialog.Size = New System.Drawing.Size(710, 367)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInfoDialog, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcInfoDialog.TabIndex = 5
        Me.INDgcInfoDialog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvInfoDialog})
        '
        'INDgvInfoDialog
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDgvInfoDialog.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvInfoDialog.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvInfoDialog.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvInfoDialog.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvInfoDialog.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvInfoDialog.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInfoDialog.Appearance.Row.Options.UseFont = True
        Me.INDgvInfoDialog.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvInfoDialog.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvInfoDialog.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColErrorEmployee, Me.INDColErrorDay, Me.INDColErrorIcon, Me.INDColErrorTemplate, Me.INDColErrorMessege})
        Me.INDgvInfoDialog.GridControl = Me.INDgcInfoDialog
        Me.INDgvInfoDialog.Name = "INDgvInfoDialog"
        Me.INDgvInfoDialog.OptionsBehavior.Editable = False
        Me.INDgvInfoDialog.OptionsCustomization.AllowGroup = False
        Me.INDgvInfoDialog.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvInfoDialog.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvInfoDialog.OptionsView.ShowAutoFilterRow = True
        Me.INDgvInfoDialog.OptionsView.ShowGroupPanel = False
        Me.INDgvInfoDialog.Tag = 371
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvInfoDialog, False)
        '
        'INDColErrorEmployee
        '
        Me.INDColErrorEmployee.Caption = "Empleado"
        Me.INDColErrorEmployee.FieldName = "EmployeeError"
        Me.INDColErrorEmployee.Name = "INDColErrorEmployee"
        Me.INDColErrorEmployee.Visible = True
        Me.INDColErrorEmployee.VisibleIndex = 0
        Me.INDColErrorEmployee.Width = 186
        '
        'INDColErrorDay
        '
        Me.INDColErrorDay.Caption = "Día"
        Me.INDColErrorDay.FieldName = "DayError"
        Me.INDColErrorDay.Name = "INDColErrorDay"
        Me.INDColErrorDay.Visible = True
        Me.INDColErrorDay.VisibleIndex = 1
        Me.INDColErrorDay.Width = 219
        '
        'INDColErrorIcon
        '
        Me.INDColErrorIcon.Caption = " "
        Me.INDColErrorIcon.ColumnEdit = Me.RepIconScheduleDetail
        Me.INDColErrorIcon.FieldName = "IconError"
        Me.INDColErrorIcon.Name = "INDColErrorIcon"
        Me.INDColErrorIcon.Visible = True
        Me.INDColErrorIcon.VisibleIndex = 2
        Me.INDColErrorIcon.Width = 36
        '
        'RepIconScheduleDetail
        '
        Me.RepIconScheduleDetail.Name = "RepIconScheduleDetail"
        Me.RepIconScheduleDetail.NullText = " "
        '
        'INDColErrorTemplate
        '
        Me.INDColErrorTemplate.Caption = "Turno"
        Me.INDColErrorTemplate.FieldName = "TemplateError"
        Me.INDColErrorTemplate.Name = "INDColErrorTemplate"
        Me.INDColErrorTemplate.Visible = True
        Me.INDColErrorTemplate.VisibleIndex = 3
        Me.INDColErrorTemplate.Width = 231
        '
        'INDColErrorMessege
        '
        Me.INDColErrorMessege.Caption = "Razón"
        Me.INDColErrorMessege.FieldName = "MessegeError"
        Me.INDColErrorMessege.Name = "INDColErrorMessege"
        Me.INDColErrorMessege.Visible = True
        Me.INDColErrorMessege.VisibleIndex = 4
        Me.INDColErrorMessege.Width = 451
        '
        'LabelControl1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl1, False)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl1, False)
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(710, 30)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "No se registraron algunos turnos!"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(734, 461)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LabelControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(714, 34)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcInfoDialog
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(203, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(714, 371)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDBtnOK
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 405)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(182, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(714, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'FrmInfoDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 461)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmInfoDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcInfoDialog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvInfoDialog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepIconScheduleDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcInfoDialog As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvInfoDialog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDColErrorDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColErrorIcon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepIconScheduleDetail As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDColErrorTemplate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColErrorMessege As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDColErrorEmployee As DevExpress.XtraGrid.Columns.GridColumn
End Class
