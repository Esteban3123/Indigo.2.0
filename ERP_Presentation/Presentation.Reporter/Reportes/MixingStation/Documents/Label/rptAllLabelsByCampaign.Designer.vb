<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptAllLabelsByCampaign
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
        Me.XrSubreport5 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDFrMagistral = New DevExpress.XtraReports.UI.FormattingRule()
        Me.Origin = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrSubreport4 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDFrSubNPT = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDFrSubLabel = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDFrSticker = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDFrTabletSticker = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport5, Me.XrSubreport4, Me.XrSubreport3, Me.XrSubreport1, Me.XrSubreport2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 963.0832!
        Me.Detail.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrSubreport5
        '
        Me.XrSubreport5.Dpi = 254.0!
        Me.XrSubreport5.FormattingRules.Add(Me.INDFrMagistral)
        Me.XrSubreport5.GenerateOwnPages = True
        Me.XrSubreport5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 761.999817!)
        Me.XrSubreport5.Name = "XrSubreport5"
        Me.XrSubreport5.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDCampaignDetailId", Nothing, "CampaignDetailId"))
        Me.XrSubreport5.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("OriginReport", Me.Origin))
        Me.XrSubreport5.ReportSource = New Presentation.Reporter.rptMagistralLabelSub()
        Me.XrSubreport5.SizeF = New System.Drawing.SizeF(2159.0!, 201.083405!)
        Me.XrSubreport5.Visible = False
        '
        'INDFrMagistral
        '
        Me.INDFrMagistral.Condition = "[LabelType]=5"
        Me.INDFrMagistral.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDFrMagistral.Name = "INDFrMagistral"
        '
        'Origin
        '
        Me.Origin.Description = "Parameter1"
        Me.Origin.Name = "Origin"
        Me.Origin.Type = GetType(Integer)
        Me.Origin.ValueInfo = "1"
        Me.Origin.Visible = False
        '
        'XrSubreport4
        '
        Me.XrSubreport4.Dpi = 254.0!
        Me.XrSubreport4.FormattingRules.Add(Me.INDFrSubNPT)
        Me.XrSubreport4.GenerateOwnPages = True
        Me.XrSubreport4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 575.02771!)
        Me.XrSubreport4.Name = "XrSubreport4"
        Me.XrSubreport4.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDCampaignDetailId", Nothing, "CampaignDetailId"))
        Me.XrSubreport4.ReportSource = New Presentation.Reporter.rptParenteralNutritionLabelSub()
        Me.XrSubreport4.SizeF = New System.Drawing.SizeF(2159.0!, 186.9722!)
        Me.XrSubreport4.Visible = False
        '
        'INDFrSubNPT
        '
        Me.INDFrSubNPT.Condition = "[LabelType]=2"
        Me.INDFrSubNPT.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDFrSubNPT.Name = "INDFrSubNPT"
        '
        'XrSubreport3
        '
        Me.XrSubreport3.Dpi = 254.0!
        Me.XrSubreport3.FormattingRules.Add(Me.INDFrSubLabel)
        Me.XrSubreport3.GenerateOwnPages = True
        Me.XrSubreport3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 377.4722!)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("IdC", Nothing, "CampaignDetailId"))
        Me.XrSubreport3.ReportSource = New Presentation.Reporter.rptReportDoseAdjustmentLabel()
        Me.XrSubreport3.SizeF = New System.Drawing.SizeF(2159.0!, 197.5555!)
        Me.XrSubreport3.Visible = False
        '
        'INDFrSubLabel
        '
        Me.INDFrSubLabel.Condition = "[LabelType]=1"
        Me.INDFrSubLabel.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDFrSubLabel.Name = "INDFrSubLabel"
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Dpi = 254.0!
        Me.XrSubreport1.FormattingRules.Add(Me.INDFrSticker)
        Me.XrSubreport1.GenerateOwnPages = True
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDCampaignDetailId", Nothing, "CampaignDetailId"))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptStickerSub()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(2159.0!, 169.3333!)
        Me.XrSubreport1.Visible = False
        '
        'INDFrSticker
        '
        Me.INDFrSticker.Condition = "[LabelType]=3"
        Me.INDFrSticker.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDFrSticker.Name = "INDFrSticker"
        '
        'XrSubreport2
        '
        Me.XrSubreport2.Dpi = 254.0!
        Me.XrSubreport2.FormattingRules.Add(Me.INDFrTabletSticker)
        Me.XrSubreport2.GenerateOwnPages = True
        Me.XrSubreport2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 169.3333!)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDCampaignDetailId", Nothing, "CampaignDetailId"))
        Me.XrSubreport2.ReportSource = New Presentation.Reporter.rptTabletStickerSub()
        Me.XrSubreport2.SizeF = New System.Drawing.SizeF(2159.0!, 208.138901!)
        Me.XrSubreport2.Visible = False
        '
        'INDFrTabletSticker
        '
        Me.INDFrTabletSticker.Condition = "[LabelType]=4"
        Me.INDFrTabletSticker.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDFrTabletSticker.Name = "INDFrTabletSticker"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.MixingStationRepository.RequestMixingStationDetailXpo)
        '
        'rptAllLabelsByCampaign
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.DataSource = Me.BindingSource1
        Me.Dpi = 254.0!
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.INDFrSticker, Me.INDFrTabletSticker, Me.INDFrSubLabel, Me.INDFrSubNPT, Me.INDFrMagistral})
        Me.Margins = New DevExpress.Drawing.DXMargins(0, 0, 0, 0)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Origin})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.RequestParameters = False
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.SnapGridSize = 25.0!
        Me.Version = "20.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDFrSticker As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents INDFrTabletSticker As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents INDFrSubLabel As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents INDFrSubNPT As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents Origin As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrSubreport4 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrSubreport5 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDFrMagistral As DevExpress.XtraReports.UI.FormattingRule
End Class
