<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupExemptIncomeDetail
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
        Me.components = New System.ComponentModel.Container()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgPccDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcExemptIncomeDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvExemptIncomeDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColTypeDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCodeDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInvoice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDebitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCoCreditValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColExemptIncome = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMaxDeductionsAndRentExents = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPrevoiusRetentions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcDeliveryTime = New DevExpress.XtraLayout.LayoutControl()
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiRemove = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopMenuActions2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExemptIncomeDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvExemptIncomeDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDeliveryTime.SuspendLayout()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDSbAdd.Location = New System.Drawing.Point(0, 0)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(710, 36)
        Me.INDSbAdd.TabIndex = 1
        Me.INDSbAdd.Text = "Agregar"
        Me.INDSbAdd.Visible = False
        '
        'INDLcgPccDeliveryTime
        '
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgPccDeliveryTime.GroupBordersVisible = False
        Me.INDLcgPccDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDLcgPccDeliveryTime.Name = "Root"
        Me.INDLcgPccDeliveryTime.Size = New System.Drawing.Size(710, 453)
        Me.INDLcgPccDeliveryTime.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcExemptIncomeDetail
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(690, 433)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDGcExemptIncomeDetail
        '
        Me.INDGcExemptIncomeDetail.Location = New System.Drawing.Point(12, 12)
        Me.INDGcExemptIncomeDetail.MainView = Me.INDGvExemptIncomeDetail
        Me.INDGcExemptIncomeDetail.Name = "INDGcExemptIncomeDetail"
        Me.INDGcExemptIncomeDetail.Size = New System.Drawing.Size(686, 429)
        Me.INDGcExemptIncomeDetail.TabIndex = 4
        Me.INDGcExemptIncomeDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvExemptIncomeDetail})
        '
        'INDGvExemptIncomeDetail
        '
        Me.INDGvExemptIncomeDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColTypeDocument, Me.INDColCodeDocument, Me.INDColInvoice, Me.INDColDate, Me.INDColDebitValue, Me.INDCoCreditValue, Me.INDColExemptIncome, Me.INDColMaxDeductionsAndRentExents, Me.INDColPrevoiusRetentions})
        Me.INDGvExemptIncomeDetail.GridControl = Me.INDGcExemptIncomeDetail
        Me.INDGvExemptIncomeDetail.Name = "INDGvExemptIncomeDetail"
        Me.INDGvExemptIncomeDetail.OptionsView.ShowFooter = True
        Me.INDGvExemptIncomeDetail.OptionsView.ShowGroupPanel = False
        '
        'INDColTypeDocument
        '
        Me.INDColTypeDocument.Caption = "Tipo"
        Me.INDColTypeDocument.FieldName = "TypeName"
        Me.INDColTypeDocument.Name = "INDColTypeDocument"
        Me.INDColTypeDocument.OptionsColumn.AllowEdit = False
        Me.INDColTypeDocument.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColTypeDocument.Visible = True
        Me.INDColTypeDocument.VisibleIndex = 0
        Me.INDColTypeDocument.Width = 126
        '
        'INDColCodeDocument
        '
        Me.INDColCodeDocument.Caption = "Código"
        Me.INDColCodeDocument.FieldName = "Code"
        Me.INDColCodeDocument.Name = "INDColCodeDocument"
        Me.INDColCodeDocument.OptionsColumn.AllowEdit = False
        Me.INDColCodeDocument.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCodeDocument.Visible = True
        Me.INDColCodeDocument.VisibleIndex = 1
        Me.INDColCodeDocument.Width = 97
        '
        'INDColInvoice
        '
        Me.INDColInvoice.Caption = "No. Factura"
        Me.INDColInvoice.FieldName = "BillNumber"
        Me.INDColInvoice.Name = "INDColInvoice"
        Me.INDColInvoice.OptionsColumn.AllowEdit = False
        Me.INDColInvoice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColInvoice.Visible = True
        Me.INDColInvoice.VisibleIndex = 2
        Me.INDColInvoice.Width = 97
        '
        'INDColDate
        '
        Me.INDColDate.Caption = "Fecha"
        Me.INDColDate.FieldName = "DocumentDate"
        Me.INDColDate.Name = "INDColDate"
        Me.INDColDate.OptionsColumn.AllowEdit = False
        Me.INDColDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDate.Visible = True
        Me.INDColDate.VisibleIndex = 3
        Me.INDColDate.Width = 125
        '
        'INDColDebitValue
        '
        Me.INDColDebitValue.Caption = "Débito"
        Me.INDColDebitValue.DisplayFormat.FormatString = "c0"
        Me.INDColDebitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDebitValue.FieldName = "DebitValue"
        Me.INDColDebitValue.Name = "INDColDebitValue"
        Me.INDColDebitValue.OptionsColumn.AllowEdit = False
        Me.INDColDebitValue.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDebitValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DebitValue", "{0:C0}")})
        Me.INDColDebitValue.Visible = True
        Me.INDColDebitValue.VisibleIndex = 4
        Me.INDColDebitValue.Width = 100
        '
        'INDCoCreditValue
        '
        Me.INDCoCreditValue.Caption = "Crédito"
        Me.INDCoCreditValue.DisplayFormat.FormatString = "c0"
        Me.INDCoCreditValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDCoCreditValue.FieldName = "CreditValue"
        Me.INDCoCreditValue.Name = "INDCoCreditValue"
        Me.INDCoCreditValue.OptionsColumn.AllowEdit = False
        Me.INDCoCreditValue.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDCoCreditValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CreditValue", "{0:C0}")})
        Me.INDCoCreditValue.Visible = True
        Me.INDCoCreditValue.VisibleIndex = 5
        Me.INDCoCreditValue.Width = 123
        '
        'INDColExemptIncome
        '
        Me.INDColExemptIncome.Caption = "Renta exenta 25%"
        Me.INDColExemptIncome.DisplayFormat.FormatString = "c0"
        Me.INDColExemptIncome.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColExemptIncome.FieldName = "ExemptIncome"
        Me.INDColExemptIncome.Name = "INDColExemptIncome"
        Me.INDColExemptIncome.OptionsColumn.AllowEdit = False
        Me.INDColExemptIncome.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColExemptIncome.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ExemptIncome", "{0:C0}")})
        Me.INDColExemptIncome.Width = 176
        '
        'INDColMaxDeductionsAndRentExents
        '
        Me.INDColMaxDeductionsAndRentExents.Caption = "Rentas exentas y deducciones"
        Me.INDColMaxDeductionsAndRentExents.DisplayFormat.FormatString = "c0"
        Me.INDColMaxDeductionsAndRentExents.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColMaxDeductionsAndRentExents.FieldName = "MaxDeductionsAndRentExents"
        Me.INDColMaxDeductionsAndRentExents.Name = "INDColMaxDeductionsAndRentExents"
        Me.INDColMaxDeductionsAndRentExents.OptionsColumn.AllowEdit = False
        Me.INDColMaxDeductionsAndRentExents.OptionsColumn.ShowInCustomizationForm = False
        Me.INDColMaxDeductionsAndRentExents.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MaxDeductionsAndRentExents", "{0:C0}")})
        Me.INDColMaxDeductionsAndRentExents.Width = 176
        '
        'INDColPrevoiusRetentions
        '
        Me.INDColPrevoiusRetentions.Caption = "Retención aplicada"
        Me.INDColPrevoiusRetentions.DisplayFormat.FormatString = "c0"
        Me.INDColPrevoiusRetentions.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColPrevoiusRetentions.FieldName = "RetentionValue383"
        Me.INDColPrevoiusRetentions.Name = "INDColPrevoiusRetentions"
        Me.INDColPrevoiusRetentions.OptionsColumn.AllowEdit = False
        Me.INDColPrevoiusRetentions.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColPrevoiusRetentions.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RetentionValue383", "{0:C0}")})
        '
        'INDLcDeliveryTime
        '
        Me.INDLcDeliveryTime.Controls.Add(Me.INDGcExemptIncomeDetail)
        Me.INDLcDeliveryTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDeliveryTime.Location = New System.Drawing.Point(0, 36)
        Me.INDLcDeliveryTime.Name = "INDLcDeliveryTime"
        Me.INDLcDeliveryTime.Root = Me.INDLcgPccDeliveryTime
        Me.INDLcDeliveryTime.Size = New System.Drawing.Size(710, 453)
        Me.INDLcDeliveryTime.TabIndex = 3
        Me.INDLcDeliveryTime.Text = "LayoutControl1"
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiRemove})
        Me.BarManager2.MaxItemId = 32
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 0)
        Me.BarDockControl5.Manager = Me.BarManager2
        Me.BarDockControl5.Size = New System.Drawing.Size(710, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 489)
        Me.BarDockControl6.Manager = Me.BarManager2
        Me.BarDockControl6.Size = New System.Drawing.Size(710, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 0)
        Me.BarDockControl7.Manager = Me.BarManager2
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 489)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(710, 0)
        Me.BarDockControl8.Manager = Me.BarManager2
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 489)
        '
        'INDBbiRemove
        '
        Me.INDBbiRemove.Caption = "Eliminar"
        Me.INDBbiRemove.Id = 13
        Me.INDBbiRemove.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiRemove.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiRemove.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiRemove.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiRemove.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiRemove.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiRemove.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiRemove.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiRemove.Name = "INDBbiRemove"
        '
        'INDPopMenuActions2
        '
        Me.INDPopMenuActions2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRemove)})
        Me.INDPopMenuActions2.Manager = Me.BarManager2
        Me.INDPopMenuActions2.Name = "INDPopMenuActions2"
        '
        'FrmPopupExemptIncomeDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 489)
        Me.Controls.Add(Me.INDLcDeliveryTime)
        Me.Controls.Add(Me.INDSbAdd)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupExemptIncomeDetail"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Detalle"
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExemptIncomeDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvExemptIncomeDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDeliveryTime.ResumeLayout(False)
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgPccDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcExemptIncomeDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvExemptIncomeDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcDeliveryTime As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDColTypeDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCodeDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInvoice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDebitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCoCreditValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColExemptIncome As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMaxDeductionsAndRentExents As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiRemove As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDColPrevoiusRetentions As DevExpress.XtraGrid.Columns.GridColumn
End Class
