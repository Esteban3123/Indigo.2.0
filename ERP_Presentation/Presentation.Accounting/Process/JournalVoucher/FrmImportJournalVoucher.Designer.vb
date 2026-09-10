<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportJournalVoucher
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImportJournalVoucher))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcImportJournalVoucher = New DevExpress.XtraGrid.GridControl()
        Me.INDGvImportJournalVoucher = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolImportJournalVoucherConsecutive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportJournalVoucherEntityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportJournalVoucherEntityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportJournalVoucherVoucherDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportJournalVoucherDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportJournalVoucherStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcImportJournalVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvImportJournalVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcImportJournalVoucher)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcImportJournalVoucher
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcImportJournalVoucher, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcImportJournalVoucher, Nothing)
        Me.INDGcImportJournalVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcImportJournalVoucher, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcImportJournalVoucher, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcImportJournalVoucher, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcImportJournalVoucher, False)
        Me.INDGcImportJournalVoucher.Location = New System.Drawing.Point(14, 14)
        Me.INDGcImportJournalVoucher.MainView = Me.INDGvImportJournalVoucher
        Me.INDGcImportJournalVoucher.Name = "INDGcImportJournalVoucher"
        Me.INDGcImportJournalVoucher.Size = New System.Drawing.Size(756, 522)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcImportJournalVoucher, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcImportJournalVoucher.TabIndex = 5
        Me.INDGcImportJournalVoucher.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvImportJournalVoucher})
        '
        'INDGvImportJournalVoucher
        '
        Me.INDGvImportJournalVoucher.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvImportJournalVoucher.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvImportJournalVoucher.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvImportJournalVoucher.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvImportJournalVoucher.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvImportJournalVoucher.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportJournalVoucher.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvImportJournalVoucher.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportJournalVoucher.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvImportJournalVoucher.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvImportJournalVoucher.Appearance.Row.Options.UseFont = True
        Me.INDGvImportJournalVoucher.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvImportJournalVoucher.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvImportJournalVoucher.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolImportJournalVoucherConsecutive, Me.INDcolImportJournalVoucherEntityName, Me.INDcolImportJournalVoucherEntityCode, Me.INDcolImportJournalVoucherVoucherDate, Me.INDcolImportJournalVoucherDetail, Me.INDcolImportJournalVoucherStatus})
        Me.INDGvImportJournalVoucher.GridControl = Me.INDGcImportJournalVoucher
        Me.INDGvImportJournalVoucher.Name = "INDGvImportJournalVoucher"
        Me.INDGvImportJournalVoucher.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvImportJournalVoucher.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvImportJournalVoucher.OptionsView.ShowAutoFilterRow = True
        Me.INDGvImportJournalVoucher.OptionsView.ShowDetailButtons = False
        Me.INDGvImportJournalVoucher.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvImportJournalVoucher, False)
        '
        'INDcolImportJournalVoucherConsecutive
        '
        Me.INDcolImportJournalVoucherConsecutive.Caption = "Consecutivo"
        Me.INDcolImportJournalVoucherConsecutive.FieldName = "Consecutive"
        Me.INDcolImportJournalVoucherConsecutive.Name = "INDcolImportJournalVoucherConsecutive"
        Me.INDcolImportJournalVoucherConsecutive.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherConsecutive.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherConsecutive.Visible = True
        Me.INDcolImportJournalVoucherConsecutive.VisibleIndex = 0
        Me.INDcolImportJournalVoucherConsecutive.Width = 89
        '
        'INDcolImportJournalVoucherEntityName
        '
        Me.INDcolImportJournalVoucherEntityName.Caption = "Origen"
        Me.INDcolImportJournalVoucherEntityName.FieldName = "EntityNameDescription"
        Me.INDcolImportJournalVoucherEntityName.Name = "INDcolImportJournalVoucherEntityName"
        Me.INDcolImportJournalVoucherEntityName.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherEntityName.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherEntityName.Visible = True
        Me.INDcolImportJournalVoucherEntityName.VisibleIndex = 1
        Me.INDcolImportJournalVoucherEntityName.Width = 120
        '
        'INDcolImportJournalVoucherEntityCode
        '
        Me.INDcolImportJournalVoucherEntityCode.Caption = "Documento"
        Me.INDcolImportJournalVoucherEntityCode.FieldName = "EntityCode"
        Me.INDcolImportJournalVoucherEntityCode.Name = "INDcolImportJournalVoucherEntityCode"
        Me.INDcolImportJournalVoucherEntityCode.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherEntityCode.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherEntityCode.Visible = True
        Me.INDcolImportJournalVoucherEntityCode.VisibleIndex = 2
        Me.INDcolImportJournalVoucherEntityCode.Width = 84
        '
        'INDcolImportJournalVoucherVoucherDate
        '
        Me.INDcolImportJournalVoucherVoucherDate.Caption = "Fecha Documento"
        Me.INDcolImportJournalVoucherVoucherDate.FieldName = "VoucherDate"
        Me.INDcolImportJournalVoucherVoucherDate.Name = "INDcolImportJournalVoucherVoucherDate"
        Me.INDcolImportJournalVoucherVoucherDate.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherVoucherDate.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherVoucherDate.Visible = True
        Me.INDcolImportJournalVoucherVoucherDate.VisibleIndex = 3
        Me.INDcolImportJournalVoucherVoucherDate.Width = 123
        '
        'INDcolImportJournalVoucherDetail
        '
        Me.INDcolImportJournalVoucherDetail.Caption = "Observaciones"
        Me.INDcolImportJournalVoucherDetail.FieldName = "Detail"
        Me.INDcolImportJournalVoucherDetail.Name = "INDcolImportJournalVoucherDetail"
        Me.INDcolImportJournalVoucherDetail.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherDetail.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherDetail.Visible = True
        Me.INDcolImportJournalVoucherDetail.VisibleIndex = 4
        Me.INDcolImportJournalVoucherDetail.Width = 232
        '
        'INDcolImportJournalVoucherStatus
        '
        Me.INDcolImportJournalVoucherStatus.Caption = "Estado"
        Me.INDcolImportJournalVoucherStatus.FieldName = "StatusName"
        Me.INDcolImportJournalVoucherStatus.Name = "INDcolImportJournalVoucherStatus"
        Me.INDcolImportJournalVoucherStatus.OptionsColumn.AllowEdit = False
        Me.INDcolImportJournalVoucherStatus.OptionsColumn.AllowFocus = False
        Me.INDcolImportJournalVoucherStatus.Visible = True
        Me.INDcolImportJournalVoucherStatus.VisibleIndex = 5
        Me.INDcolImportJournalVoucherStatus.Width = 90
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControlGroup2.Text = "Importar Información"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcImportJournalVoucher
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(760, 526)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(780, 32)
        Me.INDBtnAdd.TabIndex = 6
        Me.INDBtnAdd.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 550)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'FrmImportJournalVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 586)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmImportJournalVoucher.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmImportJournalVoucher"
        Me.ShowInTaskbar = False
        Me.Text = "Importar Comprobante Contable"
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDGcImportJournalVoucher,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvImportJournalVoucher,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGcImportJournalVoucher As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvImportJournalVoucher As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDcolImportJournalVoucherConsecutive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportJournalVoucherEntityName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportJournalVoucherVoucherDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDcolImportJournalVoucherEntityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportJournalVoucherDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportJournalVoucherStatus As DevExpress.XtraGrid.Columns.GridColumn
End Class
