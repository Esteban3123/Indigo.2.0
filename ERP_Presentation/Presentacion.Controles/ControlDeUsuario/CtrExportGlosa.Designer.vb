<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrExportGlosa
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
        Me.INDbtnAddRecord = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgCExport = New DevExpress.XtraGrid.GridControl()
        Me.INDgvExport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ClID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClIdGlosaInvoiceDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClInvoiceNumberGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Clinvoicenumbercl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClBalanceInvoice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLCostCenterCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLServiceCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ClServiceName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLValueServiceManual = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLAmmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLInvoicedValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLPatientValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLEntityValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLValueGlosa = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLcodeConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLcodeResponsale = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CLcomment = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDgCExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDbtnAddRecord
        '
        Me.INDbtnAddRecord.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnAddRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddRecord.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnAddRecord.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnAddRecord.Name = "INDbtnAddRecord"
        Me.INDbtnAddRecord.Size = New System.Drawing.Size(344, 235)
        Me.INDbtnAddRecord.TabIndex = 1
        Me.INDbtnAddRecord.Text = "Exportar"
        Me.INDbtnAddRecord.ToolTip = "Agregar Nuevo Registro"
        '
        'INDgCExport
        '
        Me.INDgCExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgCExport.Location = New System.Drawing.Point(3, 3)
        Me.INDgCExport.MainView = Me.INDgvExport
        Me.INDgCExport.Name = "INDgCExport"
        Me.INDgCExport.Size = New System.Drawing.Size(342, 227)
        Me.INDgCExport.TabIndex = 2
        Me.INDgCExport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvExport})
        Me.INDgCExport.Visible = False
        '
        'INDgvExport
        '
        Me.INDgvExport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ClID, Me.ClIdGlosaInvoiceDetail, Me.ClInvoiceNumberGroup, Me.Clinvoicenumbercl, Me.ClBalanceInvoice, Me.CLCostCenterCode, Me.CLServiceCode, Me.ClServiceName, Me.CLValueServiceManual, Me.CLUnitValue, Me.CLAmmount, Me.CLInvoicedValue, Me.CLPatientValue, Me.CLEntityValue, Me.CLValueGlosa, Me.CLcodeConcept, Me.CLcodeResponsale, Me.CLcomment})
        Me.INDgvExport.GridControl = Me.INDgCExport
        Me.INDgvExport.GroupCount = 1
        Me.INDgvExport.Name = "INDgvExport"
        Me.INDgvExport.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ClInvoiceNumberGroup, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'ClID
        '
        Me.ClID.Caption = "CodigoDetalle"
        Me.ClID.FieldName = "Id"
        Me.ClID.MinWidth = 100
        Me.ClID.Name = "ClID"
        Me.ClID.Visible = True
        Me.ClID.VisibleIndex = 0
        Me.ClID.Width = 121
        '
        'ClIdGlosaInvoiceDetail
        '
        Me.ClIdGlosaInvoiceDetail.Caption = "CodigoQX"
        Me.ClIdGlosaInvoiceDetail.FieldName = "IdQx"
        Me.ClIdGlosaInvoiceDetail.MinWidth = 100
        Me.ClIdGlosaInvoiceDetail.Name = "ClIdGlosaInvoiceDetail"
        Me.ClIdGlosaInvoiceDetail.Visible = True
        Me.ClIdGlosaInvoiceDetail.VisibleIndex = 1
        Me.ClIdGlosaInvoiceDetail.Width = 100
        '
        'ClInvoiceNumberGroup
        '
        Me.ClInvoiceNumberGroup.Caption = "Factura"
        Me.ClInvoiceNumberGroup.FieldName = "InvoiceNumber"
        Me.ClInvoiceNumberGroup.MinWidth = 100
        Me.ClInvoiceNumberGroup.Name = "ClInvoiceNumberGroup"
        Me.ClInvoiceNumberGroup.Visible = True
        Me.ClInvoiceNumberGroup.VisibleIndex = 2
        Me.ClInvoiceNumberGroup.Width = 201
        '
        'Clinvoicenumbercl
        '
        Me.Clinvoicenumbercl.Caption = "Factura"
        Me.Clinvoicenumbercl.FieldName = "InvoiceNumber"
        Me.Clinvoicenumbercl.MinWidth = 100
        Me.Clinvoicenumbercl.Name = "Clinvoicenumbercl"
        Me.Clinvoicenumbercl.Visible = True
        Me.Clinvoicenumbercl.VisibleIndex = 2
        Me.Clinvoicenumbercl.Width = 121
        '
        'ClBalanceInvoice
        '
        Me.ClBalanceInvoice.Caption = "Saldo Factura"
        Me.ClBalanceInvoice.DisplayFormat.FormatString = "c0"
        Me.ClBalanceInvoice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ClBalanceInvoice.FieldName = "BalanceInvoice"
        Me.ClBalanceInvoice.MinWidth = 120
        Me.ClBalanceInvoice.Name = "ClBalanceInvoice"
        Me.ClBalanceInvoice.Visible = True
        Me.ClBalanceInvoice.VisibleIndex = 3
        Me.ClBalanceInvoice.Width = 120
        '
        'CLCostCenterCode
        '
        Me.CLCostCenterCode.Caption = "C.C"
        Me.CLCostCenterCode.FieldName = "CostCenterCode"
        Me.CLCostCenterCode.MinWidth = 120
        Me.CLCostCenterCode.Name = "CLCostCenterCode"
        Me.CLCostCenterCode.Visible = True
        Me.CLCostCenterCode.VisibleIndex = 4
        Me.CLCostCenterCode.Width = 120
        '
        'CLServiceCode
        '
        Me.CLServiceCode.Caption = "CodigoServicio"
        Me.CLServiceCode.FieldName = "ServiceCode"
        Me.CLServiceCode.MinWidth = 120
        Me.CLServiceCode.Name = "CLServiceCode"
        Me.CLServiceCode.Visible = True
        Me.CLServiceCode.VisibleIndex = 5
        Me.CLServiceCode.Width = 120
        '
        'ClServiceName
        '
        Me.ClServiceName.Caption = "NombreServicio"
        Me.ClServiceName.FieldName = "ServiceName"
        Me.ClServiceName.MinWidth = 300
        Me.ClServiceName.Name = "ClServiceName"
        Me.ClServiceName.Visible = True
        Me.ClServiceName.VisibleIndex = 6
        Me.ClServiceName.Width = 300
        '
        'CLValueServiceManual
        '
        Me.CLValueServiceManual.Caption = "ValorServicio"
        Me.CLValueServiceManual.DisplayFormat.FormatString = "n0"
        Me.CLValueServiceManual.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLValueServiceManual.FieldName = "ValueServiceManual"
        Me.CLValueServiceManual.MinWidth = 120
        Me.CLValueServiceManual.Name = "CLValueServiceManual"
        Me.CLValueServiceManual.Visible = True
        Me.CLValueServiceManual.VisibleIndex = 7
        Me.CLValueServiceManual.Width = 120
        '
        'CLUnitValue
        '
        Me.CLUnitValue.Caption = "ValorUnitario"
        Me.CLUnitValue.DisplayFormat.FormatString = "n0"
        Me.CLUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLUnitValue.FieldName = "UnitValue"
        Me.CLUnitValue.MinWidth = 120
        Me.CLUnitValue.Name = "CLUnitValue"
        Me.CLUnitValue.Visible = True
        Me.CLUnitValue.VisibleIndex = 8
        Me.CLUnitValue.Width = 120
        '
        'CLAmmount
        '
        Me.CLAmmount.Caption = "Cantidad"
        Me.CLAmmount.FieldName = "Ammount"
        Me.CLAmmount.Name = "CLAmmount"
        Me.CLAmmount.Visible = True
        Me.CLAmmount.VisibleIndex = 9
        Me.CLAmmount.Width = 20
        '
        'CLInvoicedValue
        '
        Me.CLInvoicedValue.Caption = "Valor"
        Me.CLInvoicedValue.DisplayFormat.FormatString = "n0"
        Me.CLInvoicedValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLInvoicedValue.FieldName = "InvoicedValue"
        Me.CLInvoicedValue.MinWidth = 120
        Me.CLInvoicedValue.Name = "CLInvoicedValue"
        Me.CLInvoicedValue.Visible = True
        Me.CLInvoicedValue.VisibleIndex = 10
        Me.CLInvoicedValue.Width = 120
        '
        'CLPatientValue
        '
        Me.CLPatientValue.Caption = "ValorPaciente"
        Me.CLPatientValue.DisplayFormat.FormatString = "n0"
        Me.CLPatientValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLPatientValue.FieldName = "PatientValue"
        Me.CLPatientValue.MinWidth = 120
        Me.CLPatientValue.Name = "CLPatientValue"
        Me.CLPatientValue.Visible = True
        Me.CLPatientValue.VisibleIndex = 11
        Me.CLPatientValue.Width = 120
        '
        'CLEntityValue
        '
        Me.CLEntityValue.Caption = "ValorEntidad"
        Me.CLEntityValue.DisplayFormat.FormatString = "n0"
        Me.CLEntityValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLEntityValue.FieldName = "EntityValue"
        Me.CLEntityValue.MinWidth = 120
        Me.CLEntityValue.Name = "CLEntityValue"
        Me.CLEntityValue.Visible = True
        Me.CLEntityValue.VisibleIndex = 12
        Me.CLEntityValue.Width = 120
        '
        'CLValueGlosa
        '
        Me.CLValueGlosa.Caption = "ValorGlosa"
        Me.CLValueGlosa.DisplayFormat.FormatString = "n0"
        Me.CLValueGlosa.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CLValueGlosa.FieldName = "ValueGlosa"
        Me.CLValueGlosa.MinWidth = 120
        Me.CLValueGlosa.Name = "CLValueGlosa"
        Me.CLValueGlosa.Visible = True
        Me.CLValueGlosa.VisibleIndex = 13
        Me.CLValueGlosa.Width = 120
        '
        'CLcodeConcept
        '
        Me.CLcodeConcept.Caption = "CodigoConcepto"
        Me.CLcodeConcept.FieldName = "codeConcept"
        Me.CLcodeConcept.MinWidth = 120
        Me.CLcodeConcept.Name = "CLcodeConcept"
        Me.CLcodeConcept.Visible = True
        Me.CLcodeConcept.VisibleIndex = 14
        Me.CLcodeConcept.Width = 120
        '
        'CLcodeResponsale
        '
        Me.CLcodeResponsale.Caption = "CodigoResponsable"
        Me.CLcodeResponsale.FieldName = "codeResponsale"
        Me.CLcodeResponsale.MinWidth = 120
        Me.CLcodeResponsale.Name = "CLcodeResponsale"
        Me.CLcodeResponsale.Visible = True
        Me.CLcodeResponsale.VisibleIndex = 15
        Me.CLcodeResponsale.Width = 120
        '
        'CLcomment
        '
        Me.CLcomment.Caption = "Comentario"
        Me.CLcomment.FieldName = "comment"
        Me.CLcomment.MinWidth = 200
        Me.CLcomment.Name = "CLcomment"
        Me.CLcomment.Visible = True
        Me.CLcomment.VisibleIndex = 16
        Me.CLcomment.Width = 212
        '
        'CtrExportGlosa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDgCExport)
        Me.Controls.Add(Me.INDbtnAddRecord)
        Me.Name = "CtrExportGlosa"
        Me.Size = New System.Drawing.Size(344, 235)
        CType(Me.INDgCExport,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvExport,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDbtnAddRecord As DevExpress.XtraEditors.SimpleButton
    Public WithEvents INDgCExport As DevExpress.XtraGrid.GridControl
    Public WithEvents INDgvExport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ClID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClInvoiceNumberGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLCostCenterCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLServiceCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClServiceName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLValueServiceManual As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLUnitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLAmmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLInvoicedValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLValueGlosa As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLcodeConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLcodeResponsale As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLcomment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Clinvoicenumbercl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClIdGlosaInvoiceDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLPatientValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CLEntityValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClBalanceInvoice As DevExpress.XtraGrid.Columns.GridColumn
End Class
