<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRelatedInvoices
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRelatedInvoices))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDSleInvoice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColInvoice_InvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInvoice_InvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInvoice_ThirdPartySalesValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAssociate = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(22, 21)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(193, 25)
        Me.LabelControl1.TabIndex = 19
        Me.LabelControl1.Text = "Facturas Relacionadas"
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(547, 15)
        Me.MarqueeProgressBarControl1.TabIndex = 20
        Me.MarqueeProgressBarControl1.Visible = False
        '
        'INDSleInvoice
        '
        Me.INDSleInvoice.Location = New System.Drawing.Point(22, 55)
        Me.INDSleInvoice.Name = "INDSleInvoice"
        Me.INDSleInvoice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleInvoice.Properties.Appearance.Options.UseFont = True
        Me.INDSleInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleInvoice.Properties.DisplayMember = "InvoiceNumber"
        Me.INDSleInvoice.Properties.NullText = ""
        Me.INDSleInvoice.Properties.PopupView = Me.INDGvInvoice
        Me.INDSleInvoice.Properties.ValueMember = "InvoiceId"
        Me.INDSleInvoice.Size = New System.Drawing.Size(360, 28)
        Me.INDSleInvoice.TabIndex = 21
        '
        'INDGvInvoice
        '
        Me.INDGvInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColInvoice_InvoiceNumber, Me.INDColInvoice_InvoiceDate, Me.INDColInvoice_ThirdPartySalesValue})
        Me.INDGvInvoice.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvInvoice.Name = "INDGvInvoice"
        Me.INDGvInvoice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvInvoice.OptionsView.ShowGroupPanel = False
        '
        'INDColInvoice_InvoiceNumber
        '
        Me.INDColInvoice_InvoiceNumber.Caption = "Factura"
        Me.INDColInvoice_InvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDColInvoice_InvoiceNumber.Name = "INDColInvoice_InvoiceNumber"
        Me.INDColInvoice_InvoiceNumber.OptionsColumn.AllowEdit = False
        Me.INDColInvoice_InvoiceNumber.OptionsColumn.AllowFocus = False
        Me.INDColInvoice_InvoiceNumber.Visible = True
        Me.INDColInvoice_InvoiceNumber.VisibleIndex = 0
        '
        'INDColInvoice_InvoiceDate
        '
        Me.INDColInvoice_InvoiceDate.Caption = "Fecha"
        Me.INDColInvoice_InvoiceDate.FieldName = "InvoiceDate"
        Me.INDColInvoice_InvoiceDate.Name = "INDColInvoice_InvoiceDate"
        Me.INDColInvoice_InvoiceDate.OptionsColumn.AllowEdit = False
        Me.INDColInvoice_InvoiceDate.OptionsColumn.AllowFocus = False
        Me.INDColInvoice_InvoiceDate.Visible = True
        Me.INDColInvoice_InvoiceDate.VisibleIndex = 1
        '
        'INDColInvoice_ThirdPartySalesValue
        '
        Me.INDColInvoice_ThirdPartySalesValue.Caption = "Valor"
        Me.INDColInvoice_ThirdPartySalesValue.DisplayFormat.FormatString = "c"
        Me.INDColInvoice_ThirdPartySalesValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInvoice_ThirdPartySalesValue.FieldName = "ThirdPartySalesValue"
        Me.INDColInvoice_ThirdPartySalesValue.Name = "INDColInvoice_ThirdPartySalesValue"
        Me.INDColInvoice_ThirdPartySalesValue.OptionsColumn.AllowEdit = False
        Me.INDColInvoice_ThirdPartySalesValue.OptionsColumn.AllowFocus = False
        Me.INDColInvoice_ThirdPartySalesValue.Visible = True
        Me.INDColInvoice_ThirdPartySalesValue.VisibleIndex = 2
        '
        'INDBtnAssociate
        '
        Me.INDBtnAssociate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAssociate.Appearance.Options.UseFont = True
        Me.INDBtnAssociate.Location = New System.Drawing.Point(404, 55)
        Me.INDBtnAssociate.Name = "INDBtnAssociate"
        Me.INDBtnAssociate.Size = New System.Drawing.Size(120, 28)
        Me.INDBtnAssociate.TabIndex = 22
        Me.INDBtnAssociate.Text = "Asociar"
        '
        'FrmRelatedInvoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 98)
        Me.Controls.Add(Me.INDBtnAssociate)
        Me.Controls.Add(Me.INDSleInvoice)
        Me.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmRelatedInvoices.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRelatedInvoices"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Facturas Relacionadas"
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents INDSleInvoice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDBtnAssociate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDColInvoice_InvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInvoice_InvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInvoice_ThirdPartySalesValue As DevExpress.XtraGrid.Columns.GridColumn
End Class
