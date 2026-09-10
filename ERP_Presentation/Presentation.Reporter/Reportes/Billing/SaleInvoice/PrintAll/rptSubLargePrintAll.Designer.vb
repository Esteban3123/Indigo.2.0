<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptSubLargePrintAll
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
        Me.XrSubreport4 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDfrLiquidateMasterAccountIsNotInvoiced = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDfrLiquidateMasterAccount = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDfrIsNotInvoiced = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDfrIsInvoiced = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport4, Me.XrSubreport3, Me.XrSubreport2, Me.XrSubreport1})
        Me.Detail.HeightF = 400.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrSubreport4
        '
        Me.XrSubreport4.FormattingRules.Add(Me.INDfrLiquidateMasterAccountIsNotInvoiced)
        Me.XrSubreport4.GenerateOwnPages = True
        Me.XrSubreport4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrSubreport4.Name = "XrSubreport4"
        Me.XrSubreport4.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDRevenueControlDetailId", Nothing, "RevenueControlDetailId"))
        Me.XrSubreport4.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDAdmissionNumber", Nothing, "AdmissionNumber"))
        Me.XrSubreport4.ReportSource = New Presentation.Reporter.rptInvoicePartialMotherAccount()
        Me.XrSubreport4.SizeF = New System.Drawing.SizeF(805.0!, 100.0!)
        Me.XrSubreport4.Visible = False
        '
        'INDfrLiquidateMasterAccountIsNotInvoiced
        '
        Me.INDfrLiquidateMasterAccountIsNotInvoiced.Condition = "[IsInvoiced] = False And [LiquidateMasterAccount] = True"
        Me.INDfrLiquidateMasterAccountIsNotInvoiced.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDfrLiquidateMasterAccountIsNotInvoiced.Name = "INDfrLiquidateMasterAccountIsNotInvoiced"
        '
        'XrSubreport3
        '
        Me.XrSubreport3.FormattingRules.Add(Me.INDfrLiquidateMasterAccount)
        Me.XrSubreport3.GenerateOwnPages = True
        Me.XrSubreport3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 300.0!)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDIdInvoiceSubreport", Nothing, "InvoiceId"))
        Me.XrSubreport3.ReportSource = New Presentation.Reporter.rptSaleInvoiceMotherAccount()
        Me.XrSubreport3.SizeF = New System.Drawing.SizeF(805.0!, 100.0!)
        Me.XrSubreport3.Visible = False
        '
        'INDfrLiquidateMasterAccount
        '
        Me.INDfrLiquidateMasterAccount.Condition = "[IsInvoiced]= True And[LiquidateMasterAccount] = True"
        Me.INDfrLiquidateMasterAccount.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDfrLiquidateMasterAccount.Name = "INDfrLiquidateMasterAccount"
        '
        'XrSubreport2
        '
        Me.XrSubreport2.FormattingRules.Add(Me.INDfrIsNotInvoiced)
        Me.XrSubreport2.GenerateOwnPages = True
        Me.XrSubreport2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 200.0!)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDRevenueControlDetailId", Nothing, "RevenueControlDetailId"))
        Me.XrSubreport2.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDAdmissionNumber", Nothing, "AdmissionNumber"))
        Me.XrSubreport2.ReportSource = New Presentation.Reporter.rptInvoicePartial()
        Me.XrSubreport2.SizeF = New System.Drawing.SizeF(805.0!, 100.0!)
        Me.XrSubreport2.Visible = False
        '
        'INDfrIsNotInvoiced
        '
        Me.INDfrIsNotInvoiced.Condition = "[IsInvoiced] = False And [LiquidateMasterAccount] = False"
        Me.INDfrIsNotInvoiced.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDfrIsNotInvoiced.Name = "INDfrIsNotInvoiced"
        '
        'XrSubreport1
        '
        Me.XrSubreport1.FormattingRules.Add(Me.INDfrIsInvoiced)
        Me.XrSubreport1.GenerateOwnPages = True
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 100.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDIdInvoiceSubreport", Nothing, "InvoiceId"))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptSaleInvoice()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(805.0!, 100.0!)
        Me.XrSubreport1.Visible = False
        '
        'INDfrIsInvoiced
        '
        Me.INDfrIsInvoiced.Condition = "[IsInvoiced] = True And [LiquidateMasterAccount] = False"
        Me.INDfrIsInvoiced.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDfrIsInvoiced.Name = "INDfrIsInvoiced"
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
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.BillingRepository.SubLargeDetail)
        '
        'rptSubLargePrintAll
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource1})
        Me.DataSource = Me.BindingSource1
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.INDfrIsInvoiced, Me.INDfrIsNotInvoiced, Me.INDfrLiquidateMasterAccount, Me.INDfrLiquidateMasterAccountIsNotInvoiced})
        Me.Margins = New DevExpress.Drawing.DXMargins(18, 27, 26, 26)
        Me.RequestParameters = False
        Me.Version = "20.1"
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit

End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDfrIsNotInvoiced As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents INDfrIsInvoiced As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrSubreport4 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDfrLiquidateMasterAccountIsNotInvoiced As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents INDfrLiquidateMasterAccount As DevExpress.XtraReports.UI.FormattingRule
End Class
