Imports Infrastructure.Data.Xpo.PortfolioRepository

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptSubTranferPorfolioAll
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.Flag = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.INDCfSumValues = New DevExpress.XtraReports.UI.CalculatedField()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.GroupFooter3 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.ThirdPartyValue = New DevExpress.XtraReports.Parameters.Parameter()
        Me.CurrencyValue = New DevExpress.XtraReports.Parameters.Parameter()
        Me.ValueTotal = New DevExpress.XtraReports.Parameters.Parameter()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Expanded = False
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ThirdPartyId.Id", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Flag
        '
        Me.Flag.Description = "Parameter1"
        Me.Flag.Name = "Flag"
        Me.Flag.Type = GetType(Boolean)
        Me.Flag.ValueInfo = "True"
        Me.Flag.Visible = False
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
        Me.BottomMargin.HeightF = 26.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'INDCfSumValues
        '
        Me.INDCfSumValues.Expression = "[PortfolioTransferDetailReportXpo].[Value] +[PortfolioTransferOtherConceptReportX" &
    "po].[Value]"
        Me.INDCfSumValues.Name = "INDCfSumValues"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1, Me.GroupHeader3, Me.GroupFooter3})
        Me.DetailReport.DataMember = "PortfolioTransferDetailReportXpo"
        Me.DetailReport.DataSource = Me.BindingSource2
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        Me.DetailReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify
        '
        'Detail1
        '
        Me.Detail1.Expanded = False
        Me.Detail1.HeightF = 143.125!
        Me.Detail1.Name = "Detail1"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1})
        Me.GroupHeader3.HeightF = 112.25!
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'XrSubreport1
        '
        Me.XrSubreport1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([DocumentType] = 1 Or [DocumentType] = 2 Or [DocumentType] = 3 Or [DocumentTy" &
                    "pe] = 5, True, ?)")})
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0.0001271566!, 10.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDSubIdTransferP", Nothing, "Id"))
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("FlagReport", Me.Flag))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptTransferPortfolio()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(800.9999!, 100.0!)
        '
        'GroupFooter3
        '
        Me.GroupFooter3.Expanded = False
        Me.GroupFooter3.HeightF = 60.29166!
        Me.GroupFooter3.Name = "GroupFooter3"
        Me.GroupFooter3.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        '
        'BindingSource2
        '
        Me.BindingSource2.DataSource = GetType(Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Expanded = False
        Me.GroupHeader1.HeightF = 52.16667!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Expanded = False
        Me.GroupHeader2.Level = 1
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'ThirdPartyValue
        '
        Me.ThirdPartyValue.Description = "Tercero "
        Me.ThirdPartyValue.Name = "ThirdPartyValue"
        Me.ThirdPartyValue.Type = GetType(Integer)
        Me.ThirdPartyValue.ValueInfo = "0"
        Me.ThirdPartyValue.Visible = False
        '
        'CurrencyValue
        '
        Me.CurrencyValue.Description = "Parameter1"
        Me.CurrencyValue.Name = "CurrencyValue"
        Me.CurrencyValue.Type = GetType(Integer)
        Me.CurrencyValue.ValueInfo = "0"
        Me.CurrencyValue.Visible = False
        '
        'ValueTotal
        '
        Me.ValueTotal.Description = "Parameter1"
        Me.ValueTotal.Name = "ValueTotal"
        Me.ValueTotal.Type = GetType(Integer)
        Me.ValueTotal.ValueInfo = "0"
        Me.ValueTotal.Visible = False
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport2})
        Me.GroupFooter1.Name = "GroupFooter1"
        Me.GroupFooter1.StylePriority.UseTextAlignment = False
        Me.GroupFooter1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify
        '
        'XrSubreport2
        '
        Me.XrSubreport2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([DocumentType] = 1 Or [DocumentType] = 2 Or [DocumentType] = 3 Or [DocumentTy" &
                    "pe] = 5, True, ?)")})
        Me.XrSubreport2.LocationFloat = New DevExpress.Utils.PointFloat(0.0001220703!, 0!)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ReportSource = New Presentation.Reporter.rptTransferPortfolioTotalized()
        Me.XrSubreport2.SizeF = New System.Drawing.SizeF(800.9999!, 100.0!)
        '
        'rptSubTranferPorfolioAll
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.DetailReport, Me.GroupHeader1, Me.GroupHeader2, Me.GroupFooter1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDCfSumValues})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource2})
        Me.DataSource = Me.BindingSource2
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.5!)
        Me.Margins = New DevExpress.Drawing.DXMargins(50, 49, 26, 26)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Flag, Me.ThirdPartyValue, Me.CurrencyValue, Me.ValueTotal})
        Me.RequestParameters = False
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "20.1"
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents INDCfSumValues As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents Flag As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents BindingSource2 As BindingSource
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter3 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents ThirdPartyValue As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents CurrencyValue As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents ValueTotal As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
End Class
