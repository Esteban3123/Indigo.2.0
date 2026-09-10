<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptSubPaymentOrder
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.RptPaymentOrder1 = New Presentation.Reporter.rptPaymentOrder()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RptPaymentOrder1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1})
        Me.Detail.HeightF = 100.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 26.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 24.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.BudgetRepository.BudgetPaymentOrderReportXpo)
        '
        'XrSubreport1
        '
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDPrIdPaymentOrder", Nothing, "Id"))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptPaymentOrder()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(1000.0!, 100.0!)
        '
        'RptPaymentOrder1
        '
        Me.RptPaymentOrder1.Landscape = True
        Me.RptPaymentOrder1.Margins = New System.Drawing.Printing.Margins(50, 50, 26, 24)
        Me.RptPaymentOrder1.Name = "RptPaymentOrder1"
        Me.RptPaymentOrder1.PageHeight = 850
        Me.RptPaymentOrder1.PageWidth = 1100
        Me.RptPaymentOrder1.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.RptPaymentOrder1.ParametrosReporte = Nothing
        Me.RptPaymentOrder1.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.RptPaymentOrder1.Version = "15.1"
        '
        'rptSubPaymentOrder
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.DataSource = Me.BindingSource1
        Me.Landscape = True
        Me.Margins = New DevExpress.Drawing.DXMargins(50, 50, 26, 24)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RptPaymentOrder1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents RptPaymentOrder1 As Presentation.Reporter.rptPaymentOrder
End Class
