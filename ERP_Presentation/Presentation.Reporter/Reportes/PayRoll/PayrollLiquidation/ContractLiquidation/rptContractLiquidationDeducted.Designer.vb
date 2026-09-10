<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptContractLiquidationDeducted
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTable34 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow34 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell136 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell137 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell138 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell139 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell140 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.INDContractLiquidationIdDeducted = New DevExpress.XtraReports.Parameters.Parameter()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        CType(Me.XrTable34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable34
        '
        Me.XrTable34.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable34.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTable34.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable34.Name = "XrTable34"
        Me.XrTable34.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow34})
        Me.XrTable34.SizeF = New System.Drawing.SizeF(799.8123!, 20.0!)
        Me.XrTable34.StylePriority.UseBorders = False
        Me.XrTable34.StylePriority.UseFont = False
        '
        'XrTableRow34
        '
        Me.XrTableRow34.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableRow34.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell136, Me.XrTableCell137, Me.XrTableCell138, Me.XrTableCell139, Me.XrTableCell140})
        Me.XrTableRow34.Name = "XrTableRow34"
        Me.XrTableRow34.StylePriority.UseBorders = False
        Me.XrTableRow34.Weight = 1.0R
        '
        'XrTableCell136
        '
        Me.XrTableCell136.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell136.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell136.BorderWidth = 2.0!
        Me.XrTableCell136.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sumaryDescription")})
        Me.XrTableCell136.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTableCell136.Name = "XrTableCell136"
        Me.XrTableCell136.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100.0!)
        Me.XrTableCell136.StylePriority.UseBackColor = False
        Me.XrTableCell136.StylePriority.UseBorders = False
        Me.XrTableCell136.StylePriority.UseBorderWidth = False
        Me.XrTableCell136.StylePriority.UseFont = False
        Me.XrTableCell136.StylePriority.UsePadding = False
        Me.XrTableCell136.StylePriority.UseTextAlignment = False
        Me.XrTableCell136.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell136.Weight = 2.42340762378042R
        '
        'XrTableCell137
        '
        Me.XrTableCell137.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableCell137.BorderWidth = 2.0!
        Me.XrTableCell137.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTableCell137.Name = "XrTableCell137"
        Me.XrTableCell137.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100.0!)
        Me.XrTableCell137.StylePriority.UseBorders = False
        Me.XrTableCell137.StylePriority.UseBorderWidth = False
        Me.XrTableCell137.StylePriority.UseFont = False
        Me.XrTableCell137.StylePriority.UsePadding = False
        Me.XrTableCell137.StylePriority.UseTextAlignment = False
        Me.XrTableCell137.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell137.Weight = 0.608668309272239R
        '
        'XrTableCell138
        '
        Me.XrTableCell138.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableCell138.BorderWidth = 2.0!
        Me.XrTableCell138.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTableCell138.Name = "XrTableCell138"
        Me.XrTableCell138.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell138.StylePriority.UseBorders = False
        Me.XrTableCell138.StylePriority.UseBorderWidth = False
        Me.XrTableCell138.StylePriority.UseFont = False
        Me.XrTableCell138.StylePriority.UsePadding = False
        Me.XrTableCell138.StylePriority.UseTextAlignment = False
        Me.XrTableCell138.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell138.Weight = 0.770544069440331R
        '
        'XrTableCell139
        '
        Me.XrTableCell139.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableCell139.BorderWidth = 2.0!
        Me.XrTableCell139.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTableCell139.Name = "XrTableCell139"
        Me.XrTableCell139.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell139.StylePriority.UseBorders = False
        Me.XrTableCell139.StylePriority.UseBorderWidth = False
        Me.XrTableCell139.StylePriority.UseFont = False
        Me.XrTableCell139.StylePriority.UsePadding = False
        Me.XrTableCell139.StylePriority.UseTextAlignment = False
        Me.XrTableCell139.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell139.Weight = 1.03538692077104R
        '
        'XrTableCell140
        '
        Me.XrTableCell140.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell140.BorderWidth = 2.0!
        Me.XrTableCell140.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sumaryDeducted", "{0:c2}")})
        Me.XrTableCell140.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrTableCell140.Name = "XrTableCell140"
        Me.XrTableCell140.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell140.StylePriority.UseBorders = False
        Me.XrTableCell140.StylePriority.UseBorderWidth = False
        Me.XrTableCell140.StylePriority.UseFont = False
        Me.XrTableCell140.StylePriority.UsePadding = False
        Me.XrTableCell140.StylePriority.UseTextAlignment = False
        Me.XrTableCell140.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell140.Weight = 0.691327112948292R
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'INDContractLiquidationIdDeducted
        '
        Me.INDContractLiquidationIdDeducted.Description = "Parameter2"
        Me.INDContractLiquidationIdDeducted.Name = "INDContractLiquidationIdDeducted"
        Me.INDContractLiquidationIdDeducted.Type = GetType(Integer)
        Me.INDContractLiquidationIdDeducted.ValueInfo = "0"
        Me.INDContractLiquidationIdDeducted.Visible = False
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PayrollRepository.PayrollViewReportContractLiquidation)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable34})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("sumaryDescription", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.HeightF = 20.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'rptContractLiquidationDeducted
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupHeader1})
        Me.DataSource = Me.BindingSource1
        Me.Margins = New DevExpress.Drawing.DXMargins(24, 26, 0, 0)
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDContractLiquidationIdDeducted})
        Me.ReportPrintOptions.PrintOnEmptyDataSource = False
        Me.Version = "15.1"
        CType(Me.XrTable34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents INDContractLiquidationIdDeducted As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrTable34 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow34 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell136 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell137 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell138 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell139 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell140 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
End Class
