<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptResultStatusExpensesCosts
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptResultStatusExpensesCosts))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.INDTblTotalExpensesCosts = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDCllTotalExpensesCosts = New DevExpress.XtraReports.UI.XRTableCell()
        Me.FormattingRule2 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.INDGhSubAuxiliar = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrTable7 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.INDCurrentBalance = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCurrentBalanceAccountLvl1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.IncludedAccountNumber = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTblTotalExpensesCosts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.PageHeader.HeightF = 20.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrTable1
        '
        Me.XrTable1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(390.0!, 20.0!)
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Código"
        Me.XrTableCell1.Weight = 0.53846177321213939R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Text = "Nombre Cuenta Contable"
        Me.XrTableCell2.Weight = 1.5384613037109376R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "Saldo Actual"
        Me.XrTableCell3.Weight = 0.92307692307692313R
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblTotalExpensesCosts})
        Me.ReportFooter.HeightF = 16.66667!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'INDTblTotalExpensesCosts
        '
        Me.INDTblTotalExpensesCosts.BorderColor = System.Drawing.Color.Transparent
        Me.INDTblTotalExpensesCosts.ForeColor = System.Drawing.Color.Transparent
        Me.INDTblTotalExpensesCosts.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDTblTotalExpensesCosts.Name = "INDTblTotalExpensesCosts"
        Me.INDTblTotalExpensesCosts.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow7})
        Me.INDTblTotalExpensesCosts.SizeF = New System.Drawing.SizeF(2.0!, 2.0!)
        Me.INDTblTotalExpensesCosts.StylePriority.UseBorderColor = False
        Me.INDTblTotalExpensesCosts.StylePriority.UseForeColor = False
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDCllTotalExpensesCosts})
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.Weight = 1.0R
        '
        'INDCllTotalExpensesCosts
        '
        Me.INDCllTotalExpensesCosts.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDCurrentBalanceAccountLvl1")})
        Me.INDCllTotalExpensesCosts.Name = "INDCllTotalExpensesCosts"
        XrSummary1.FormatString = "{0:n}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllTotalExpensesCosts.Summary = XrSummary1
        Me.INDCllTotalExpensesCosts.Weight = 1.0R
        '
        'FormattingRule2
        '
        Me.FormattingRule2.Condition = "[IdMainAccount.IdAccountLevel.Level] = 4"
        Me.FormattingRule2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule2.Name = "FormattingRule2"
        '
        'INDGhSubAuxiliar
        '
        Me.INDGhSubAuxiliar.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable7})
        Me.INDGhSubAuxiliar.FormattingRules.Add(Me.FormattingRule1)
        Me.INDGhSubAuxiliar.FormattingRules.Add(Me.FormattingRule2)
        Me.INDGhSubAuxiliar.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("mainAccountCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.INDGhSubAuxiliar.HeightF = 15.0!
        Me.INDGhSubAuxiliar.Name = "INDGhSubAuxiliar"
        '
        'XrTable7
        '
        Me.XrTable7.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTable7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable7.Name = "XrTable7"
        Me.XrTable7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow8})
        Me.XrTable7.SizeF = New System.Drawing.SizeF(390.0!, 15.0!)
        Me.XrTable7.StylePriority.UseFont = False
        Me.XrTable7.StylePriority.UseTextAlignment = False
        Me.XrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell19, Me.XrTableCell20, Me.XrTableCell21})
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 1.0R
        '
        'XrTableCell19
        '
        Me.XrTableCell19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountCode")})
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell19.StylePriority.UsePadding = False
        Me.XrTableCell19.Weight = 0.53846177321213951R
        '
        'XrTableCell20
        '
        Me.XrTableCell20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountName")})
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell20.StylePriority.UsePadding = False
        Me.XrTableCell20.Weight = 1.538461421086238R
        '
        'XrTableCell21
        '
        Me.XrTableCell21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDCurrentBalance")})
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.XrTableCell21.StylePriority.UsePadding = False
        Me.XrTableCell21.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:c2}"
        XrSummary2.IgnoreNullValues = True
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell21.Summary = XrSummary2
        Me.XrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell21.Weight = 0.92307680570162243R
        '
        'FormattingRule1
        '
        Me.FormattingRule1.Condition = "[IdMainAccount.IdAccountLevel.Level] = 5"
        Me.FormattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule1.Name = "FormattingRule1"
        '
        'INDCurrentBalance
        '
        Me.INDCurrentBalance.DataMember = "SP_ReportResulStatus"
        Me.INDCurrentBalance.Expression = "[valueDebitMovement] - [valueCreditMovement]"
        Me.INDCurrentBalance.Name = "INDCurrentBalance"
        '
        'INDCurrentBalanceAccountLvl1
        '
        Me.INDCurrentBalanceAccountLvl1.DataMember = "SP_ReportResulStatus"
        Me.INDCurrentBalanceAccountLvl1.Expression = "Iif([mainAccountLevel] = 1, [INDCurrentBalance] , 0)"
        Me.INDCurrentBalanceAccountLvl1.Name = "INDCurrentBalanceAccountLvl1"
        '
        'IncludedAccountNumber
        '
        Me.IncludedAccountNumber.Description = "Permite saber si se muestra la columna de no. cuenta"
        Me.IncludedAccountNumber.Name = "IncludedAccountNumber"
        Me.IncludedAccountNumber.Type = GetType(Boolean)
        Me.IncludedAccountNumber.ValueInfo = "True"
        Me.IncludedAccountNumber.Visible = False
        '
        'rptResultStatusExpensesCosts
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter, Me.INDGhSubAuxiliar})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDCurrentBalance, Me.INDCurrentBalanceAccountLvl1})
        Me.DataMember = "SP_ReportResulStatus"
        Me.DataSourceSchema = resources.GetString("$this.DataSourceSchema")
        Me.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FormattingRule1, Me.FormattingRule2})
        Me.Margins = New System.Drawing.Printing.Margins(27, 433, 100, 100)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.IncludedAccountNumber})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "19.1"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTblTotalExpensesCosts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDTblTotalExpensesCosts As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDCllTotalExpensesCosts As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDGhSubAuxiliar As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTable7 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents FormattingRule2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    Friend INDCurrentBalance As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCurrentBalanceAccountLvl1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents IncludedAccountNumber As DevExpress.XtraReports.Parameters.Parameter
End Class
