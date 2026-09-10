<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptSubMassiveConfirmNotes
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
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.FormattingRule2 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FormattingRule3 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.RptNotes1 = New Presentation.Reporter.rptNotes()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RptNotes1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport3, Me.XrSubreport2, Me.XrSubreport1})
        Me.Detail.HeightF = 289.625!
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
        Me.BottomMargin.HeightF = 25.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'FormattingRule1
        '
        Me.FormattingRule1.Condition = "[NoteType] = 3"
        '
        '
        '
        Me.FormattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.FormattingRule1.Name = "FormattingRule1"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.TreasuryRepository.TreasuryNotesXpo)
        '
        'FormattingRule2
        '
        Me.FormattingRule2.Condition = "[NoteType] = 1 or [NoteType] = 2"
        '
        '
        '
        Me.FormattingRule2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.FormattingRule2.Name = "FormattingRule2"
        '
        'FormattingRule3
        '
        Me.FormattingRule3.Condition = "[NoteType] = 4"
        '
        '
        '
        Me.FormattingRule3.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.FormattingRule3.Name = "FormattingRule3"
        '
        'XrSubreport3
        '
        Me.XrSubreport3.FormattingRules.Add(Me.FormattingRule1)
        Me.XrSubreport3.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 197.9583!)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDSubIdNotesVoucherTransaction", Nothing, "Id"))
        Me.XrSubreport3.ReportSource = New Presentation.Reporter.rptNotesReversionVoucherTransaction()
        Me.XrSubreport3.SizeF = New System.Drawing.SizeF(701.0!, 91.66666!)
        Me.XrSubreport3.Visible = False
        '
        'XrSubreport2
        '
        Me.XrSubreport2.FormattingRules.Add(Me.FormattingRule3)
        Me.XrSubreport2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 100.0!)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDSubIdNotesReceiptCash", Nothing, "Id"))
        Me.XrSubreport2.ReportSource = New Presentation.Reporter.rptNotesReversionCashReceipt()
        Me.XrSubreport2.SizeF = New System.Drawing.SizeF(700.9999!, 97.95833!)
        Me.XrSubreport2.Visible = False
        '
        'XrSubreport1
        '
        Me.XrSubreport1.FormattingRules.Add(Me.FormattingRule2)
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDSubIdNotes", Nothing, "Id"))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptNotes()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(700.9999!, 100.0!)
        Me.XrSubreport1.Visible = False
        '
        'RptNotes1
        '
        Me.RptNotes1.Margins = New System.Drawing.Printing.Margins(99, 50, 26, 27)
        Me.RptNotes1.Name = "RptNotes1"
        Me.RptNotes1.PageHeight = 1100
        Me.RptNotes1.PageWidth = 850
        Me.RptNotes1.ParametrosReporte = Nothing
        Me.RptNotes1.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.RptNotes1.Version = "15.1"
        '
        'rptSubMassiveConfirmNotes
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.DataSource = Me.BindingSource1
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FormattingRule1, Me.FormattingRule2, Me.FormattingRule3})
        Me.Margins = New DevExpress.Drawing.DXMargins(100, 49, 26, 25)
        Me.RequestParameters = False
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RptNotes1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents RptNotes1 As Presentation.Reporter.rptNotes
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents FormattingRule2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents FormattingRule3 As DevExpress.XtraReports.UI.FormattingRule
End Class
