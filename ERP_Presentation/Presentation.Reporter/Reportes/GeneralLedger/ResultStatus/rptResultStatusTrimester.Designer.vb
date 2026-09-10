<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptResultStatusTrimester
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
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary4 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary5 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary6 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary7 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary8 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary9 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptResultStatusTrimester))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDCllPeriodCurrent = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllPeriodPrevious = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable8 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell25 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrTable9 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDCllTotalIncomeCurrent = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllTotalExpensesCostsCurrent = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllTotalIncomePrevious = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllTotalExpensesCostsPrevious = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllDescriptionExercise = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDCllValueExercise = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDGhClass = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrTable10 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDBalanceCurrent = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentIncome = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentExpensesCosts = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalancePreviousIncome = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalancePreviousExpensesCosts = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDTotalExercise = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDDescriptionExercise = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentAccountLvl1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDPreviousBalanceAccountLvl1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDGhSubAuxiliar = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.IncludedAccountNumber = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDBalancePrevious = New DevExpress.XtraReports.UI.CalculatedField()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable10, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.XrTable1})
        Me.PageHeader.HeightF = 61.45833!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrTable2
        '
        Me.XrTable2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(491.0001!, 0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(307.9999!, 40.625!)
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDCllPeriodCurrent, Me.INDCllPeriodPrevious})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'INDCllPeriodCurrent
        '
        Me.INDCllPeriodCurrent.Name = "INDCllPeriodCurrent"
        Me.INDCllPeriodCurrent.Weight = 1.0R
        '
        'INDCllPeriodPrevious
        '
        Me.INDCllPeriodPrevious.Name = "INDCllPeriodPrevious"
        Me.INDCllPeriodPrevious.Weight = 1.0R
        '
        'XrTable1
        '
        Me.XrTable1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(1.0!, 40.625!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(798.0!, 20.0!)
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3, Me.XrTableCell4})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Código"
        Me.XrTableCell1.Weight = 0.33834586466165417R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseBorders = False
        Me.XrTableCell2.Text = "Nombre Cuenta"
        Me.XrTableCell2.Weight = 1.5037593984962405R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "Valor Saldo"
        Me.XrTableCell3.Weight = 0.57894736842105265R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Valor Saldo"
        Me.XrTableCell4.Weight = 0.57894736842105265R
        '
        'XrTable8
        '
        Me.XrTable8.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTable8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable8.Name = "XrTable8"
        Me.XrTable8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow8})
        Me.XrTable8.SizeF = New System.Drawing.SizeF(799.0!, 20.0!)
        Me.XrTable8.StylePriority.UseFont = False
        Me.XrTable8.StylePriority.UseTextAlignment = False
        Me.XrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell25, Me.XrTableCell26, Me.XrTableCell27})
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 1.0R
        '
        'XrTableCell25
        '
        Me.XrTableCell25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell25.Name = "XrTableCell25"
        Me.XrTableCell25.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell25.StylePriority.UseFont = False
        Me.XrTableCell25.StylePriority.UsePadding = False
        Me.XrTableCell25.Text = "Total:"
        Me.XrTableCell25.Weight = 1.7966211889652672R
        '
        'XrTableCell26
        '
        Me.XrTableCell26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalanceCurrentAccountLvl1")})
        Me.XrTableCell26.Name = "XrTableCell26"
        Me.XrTableCell26.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell26.StylePriority.UsePadding = False
        XrSummary1.FormatString = "{0:c2}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell26.Summary = XrSummary1
        Me.XrTableCell26.Weight = 0.62515575207668594R
        '
        'XrTableCell27
        '
        Me.XrTableCell27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDPreviousBalanceAccountLvl1")})
        Me.XrTableCell27.Name = "XrTableCell27"
        Me.XrTableCell27.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell27.StylePriority.UsePadding = False
        XrSummary2.FormatString = "{0:c2}"
        XrSummary2.IgnoreNullValues = True
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell27.Summary = XrSummary2
        Me.XrTableCell27.Weight = 0.57822305895804671R
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable9})
        Me.ReportFooter.HeightF = 20.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrTable9
        '
        Me.XrTable9.LocationFloat = New DevExpress.Utils.PointFloat(0.9999593!, 0!)
        Me.XrTable9.Name = "XrTable9"
        Me.XrTable9.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow9})
        Me.XrTable9.SizeF = New System.Drawing.SizeF(644.0001!, 20.0!)
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDCllTotalIncomeCurrent, Me.INDCllTotalExpensesCostsCurrent, Me.INDCllTotalIncomePrevious, Me.INDCllTotalExpensesCostsPrevious, Me.INDCllDescriptionExercise, Me.INDCllValueExercise})
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 1.0R
        '
        'INDCllTotalIncomeCurrent
        '
        Me.INDCllTotalIncomeCurrent.BorderColor = System.Drawing.Color.Transparent
        Me.INDCllTotalIncomeCurrent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalanceCurrentIncome")})
        Me.INDCllTotalIncomeCurrent.ForeColor = System.Drawing.Color.Transparent
        Me.INDCllTotalIncomeCurrent.Name = "INDCllTotalIncomeCurrent"
        Me.INDCllTotalIncomeCurrent.StylePriority.UseBorderColor = False
        Me.INDCllTotalIncomeCurrent.StylePriority.UseForeColor = False
        XrSummary3.FormatString = "{0:n}"
        XrSummary3.IgnoreNullValues = True
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllTotalIncomeCurrent.Summary = XrSummary3
        Me.INDCllTotalIncomeCurrent.Weight = 0.003490846148334753R
        '
        'INDCllTotalExpensesCostsCurrent
        '
        Me.INDCllTotalExpensesCostsCurrent.BorderColor = System.Drawing.Color.Transparent
        Me.INDCllTotalExpensesCostsCurrent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalanceCurrentExpensesCosts")})
        Me.INDCllTotalExpensesCostsCurrent.ForeColor = System.Drawing.Color.Transparent
        Me.INDCllTotalExpensesCostsCurrent.Name = "INDCllTotalExpensesCostsCurrent"
        Me.INDCllTotalExpensesCostsCurrent.StylePriority.UseBorderColor = False
        Me.INDCllTotalExpensesCostsCurrent.StylePriority.UseForeColor = False
        XrSummary4.FormatString = "{0:n}"
        XrSummary4.IgnoreNullValues = True
        XrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllTotalExpensesCostsCurrent.Summary = XrSummary4
        Me.INDCllTotalExpensesCostsCurrent.Weight = 0.003490846148334753R
        '
        'INDCllTotalIncomePrevious
        '
        Me.INDCllTotalIncomePrevious.BorderColor = System.Drawing.Color.Transparent
        Me.INDCllTotalIncomePrevious.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalancePreviousIncome")})
        Me.INDCllTotalIncomePrevious.ForeColor = System.Drawing.Color.Transparent
        Me.INDCllTotalIncomePrevious.Name = "INDCllTotalIncomePrevious"
        Me.INDCllTotalIncomePrevious.StylePriority.UseBorderColor = False
        Me.INDCllTotalIncomePrevious.StylePriority.UseForeColor = False
        XrSummary5.FormatString = "{0:n}"
        XrSummary5.IgnoreNullValues = True
        XrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllTotalIncomePrevious.Summary = XrSummary5
        Me.INDCllTotalIncomePrevious.Weight = 0.0034908461483347509R
        '
        'INDCllTotalExpensesCostsPrevious
        '
        Me.INDCllTotalExpensesCostsPrevious.BorderColor = System.Drawing.Color.Transparent
        Me.INDCllTotalExpensesCostsPrevious.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalancePreviousExpensesCosts")})
        Me.INDCllTotalExpensesCostsPrevious.ForeColor = System.Drawing.Color.Transparent
        Me.INDCllTotalExpensesCostsPrevious.Name = "INDCllTotalExpensesCostsPrevious"
        Me.INDCllTotalExpensesCostsPrevious.StylePriority.UseBorderColor = False
        Me.INDCllTotalExpensesCostsPrevious.StylePriority.UseForeColor = False
        XrSummary6.FormatString = "{0:n}"
        XrSummary6.IgnoreNullValues = True
        XrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllTotalExpensesCostsPrevious.Summary = XrSummary6
        Me.INDCllTotalExpensesCostsPrevious.Weight = 0.0034908461483346997R
        '
        'INDCllDescriptionExercise
        '
        Me.INDCllDescriptionExercise.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDDescriptionExercise")})
        Me.INDCllDescriptionExercise.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDCllDescriptionExercise.Name = "INDCllDescriptionExercise"
        Me.INDCllDescriptionExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDCllDescriptionExercise.StylePriority.UseFont = False
        Me.INDCllDescriptionExercise.StylePriority.UsePadding = False
        Me.INDCllDescriptionExercise.StylePriority.UseTextAlignment = False
        Me.INDCllDescriptionExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDCllDescriptionExercise.Weight = 0.81947643444913476R
        '
        'INDCllValueExercise
        '
        Me.INDCllValueExercise.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDTotalExercise")})
        Me.INDCllValueExercise.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDCllValueExercise.Name = "INDCllValueExercise"
        Me.INDCllValueExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDCllValueExercise.StylePriority.UseFont = False
        Me.INDCllValueExercise.StylePriority.UsePadding = False
        Me.INDCllValueExercise.StylePriority.UseTextAlignment = False
        XrSummary7.FormatString = "{0:c2}"
        XrSummary7.IgnoreNullValues = True
        XrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.INDCllValueExercise.Summary = XrSummary7
        Me.INDCllValueExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDCllValueExercise.Weight = 0.29061285378565632R
        '
        'INDGhClass
        '
        Me.INDGhClass.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("classCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.INDGhClass.HeightF = 0!
        Me.INDGhClass.Level = 1
        Me.INDGhClass.Name = "INDGhClass"
        '
        'XrTable10
        '
        Me.XrTable10.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTable10.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable10.Name = "XrTable10"
        Me.XrTable10.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow10})
        Me.XrTable10.SizeF = New System.Drawing.SizeF(799.0!, 15.0!)
        Me.XrTable10.StylePriority.UseFont = False
        Me.XrTable10.StylePriority.UseTextAlignment = False
        Me.XrTable10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell28, Me.XrTableCell29, Me.XrTableCell30, Me.XrTableCell31})
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.Weight = 1.0R
        '
        'XrTableCell28
        '
        Me.XrTableCell28.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountCode")})
        Me.XrTableCell28.Name = "XrTableCell28"
        Me.XrTableCell28.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell28.StylePriority.UsePadding = False
        Me.XrTableCell28.Weight = 0.34167722304879833R
        '
        'XrTableCell29
        '
        Me.XrTableCell29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountName")})
        Me.XrTableCell29.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell29.Name = "XrTableCell29"
        Me.XrTableCell29.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell29.StylePriority.UseFont = False
        Me.XrTableCell29.StylePriority.UsePadding = False
        Me.XrTableCell29.Weight = 1.4549441186953085R
        '
        'XrTableCell30
        '
        Me.XrTableCell30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalanceCurrent")})
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell30.StylePriority.UsePadding = False
        Me.XrTableCell30.StylePriority.UseTextAlignment = False
        XrSummary8.FormatString = "{0:c2}"
        XrSummary8.IgnoreNullValues = True
        XrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell30.Summary = XrSummary8
        Me.XrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell30.Weight = 0.62515596214762126R
        '
        'XrTableCell31
        '
        Me.XrTableCell31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.INDBalancePrevious")})
        Me.XrTableCell31.Name = "XrTableCell31"
        Me.XrTableCell31.StylePriority.UseTextAlignment = False
        XrSummary9.FormatString = "{0:c2}"
        XrSummary9.IgnoreNullValues = True
        XrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell31.Summary = XrSummary9
        Me.XrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell31.Weight = 0.57822269610827193R
        '
        'INDBalanceCurrent
        '
        Me.INDBalanceCurrent.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrent.Expression = "[valueCreditMovement] - [valueDebitMovement]"
        Me.INDBalanceCurrent.Name = "INDBalanceCurrent"
        '
        'INDBalanceCurrentIncome
        '
        Me.INDBalanceCurrentIncome.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentIncome.Expression = "Iif([nature] = 'Credito' And [mainAccountClassType] = 2, [INDBalanceCurrentAccoun" &
    "tLvl1] ,0 )"
        Me.INDBalanceCurrentIncome.Name = "INDBalanceCurrentIncome"
        '
        'INDBalanceCurrentExpensesCosts
        '
        Me.INDBalanceCurrentExpensesCosts.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentExpensesCosts.Expression = "Iif([nature] = 'Debito' And [mainAccountClassType] = 2, [INDBalanceCurrentAccount" &
    "Lvl1] ,0 )"
        Me.INDBalanceCurrentExpensesCosts.Name = "INDBalanceCurrentExpensesCosts"
        '
        'INDBalancePreviousIncome
        '
        Me.INDBalancePreviousIncome.DataMember = "SP_ReportResulStatus"
        Me.INDBalancePreviousIncome.Expression = "Iif([nature] = 'Credito' And [mainAccountClassType] = 2, [INDPreviousBalanceAccou" &
    "ntLvl1]  ,0 )"
        Me.INDBalancePreviousIncome.Name = "INDBalancePreviousIncome"
        '
        'INDBalancePreviousExpensesCosts
        '
        Me.INDBalancePreviousExpensesCosts.DataMember = "SP_ReportResulStatus"
        Me.INDBalancePreviousExpensesCosts.Expression = "Iif([nature] = 'Debito' And [mainAccountClassType] = 2, [INDPreviousBalanceAccoun" &
    "tLvl1] ,0 )"
        Me.INDBalancePreviousExpensesCosts.Name = "INDBalancePreviousExpensesCosts"
        '
        'INDTotalExercise
        '
        Me.INDTotalExercise.DataMember = "SP_ReportResulStatus"
        Me.INDTotalExercise.Expression = "[INDBalanceCurrentAccountLvl1]"
        Me.INDTotalExercise.Name = "INDTotalExercise"
        '
        'INDDescriptionExercise
        '
        Me.INDDescriptionExercise.DataMember = "SP_ReportResulStatus"
        Me.INDDescriptionExercise.Expression = "Iif([].Sum([INDTotalExercise]) < 0, 'DEFICIT DEL EJERCICIO' ,'SUPERAVIT DEL EJERC" &
    "ICIO' )"
        Me.INDDescriptionExercise.Name = "INDDescriptionExercise"
        '
        'INDBalanceCurrentAccountLvl1
        '
        Me.INDBalanceCurrentAccountLvl1.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentAccountLvl1.Expression = "Iif([mainAccountLevel] = 1, [INDBalanceCurrent] , 0)"
        Me.INDBalanceCurrentAccountLvl1.Name = "INDBalanceCurrentAccountLvl1"
        '
        'INDPreviousBalanceAccountLvl1
        '
        Me.INDPreviousBalanceAccountLvl1.DataMember = "SP_ReportResulStatus"
        Me.INDPreviousBalanceAccountLvl1.Expression = "Iif([mainAccountLevel] = 1, [INDBalancePrevious], 0)"
        Me.INDPreviousBalanceAccountLvl1.Name = "INDPreviousBalanceAccountLvl1"
        '
        'INDGhSubAuxiliar
        '
        Me.INDGhSubAuxiliar.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable10})
        Me.INDGhSubAuxiliar.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("mainAccountCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.INDGhSubAuxiliar.HeightF = 15.0!
        Me.INDGhSubAuxiliar.Name = "INDGhSubAuxiliar"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable8})
        Me.GroupFooter1.HeightF = 20.0!
        Me.GroupFooter1.Level = 1
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'IncludedAccountNumber
        '
        Me.IncludedAccountNumber.Description = "Permite saber si se muestra la columna de no. cuenta"
        Me.IncludedAccountNumber.Name = "IncludedAccountNumber"
        Me.IncludedAccountNumber.Type = GetType(Boolean)
        Me.IncludedAccountNumber.ValueInfo = "True"
        Me.IncludedAccountNumber.Visible = False
        '
        'INDBalancePrevious
        '
        Me.INDBalancePrevious.DataMember = "SP_ReportResulStatus"
        Me.INDBalancePrevious.Expression = "[previousBalance] * Iif([nature] = 'Debito', -1, 1)"
        Me.INDBalancePrevious.Name = "INDBalancePrevious"
        '
        'rptResultStatusTrimester
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter, Me.INDGhClass, Me.INDGhSubAuxiliar, Me.GroupFooter1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDBalanceCurrent, Me.INDBalancePrevious, Me.INDBalanceCurrentIncome, Me.INDBalanceCurrentExpensesCosts, Me.INDBalancePreviousIncome, Me.INDBalancePreviousExpensesCosts, Me.INDTotalExercise, Me.INDDescriptionExercise, Me.INDBalanceCurrentAccountLvl1, Me.INDPreviousBalanceAccountLvl1})
        Me.DataMember = "SP_ReportResulStatus"
        Me.DataSourceSchema = resources.GetString("$this.DataSourceSchema")
        Me.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Margins = New System.Drawing.Printing.Margins(26, 25, 100, 100)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.IncludedAccountNumber})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.Version = "19.1"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDCllPeriodCurrent As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllPeriodPrevious As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell25 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrTable9 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDCllDescriptionExercise As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllValueExercise As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllTotalIncomeCurrent As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllTotalExpensesCostsCurrent As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllTotalIncomePrevious As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCllTotalExpensesCostsPrevious As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDGhClass As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTable10 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell31 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDBalanceCurrent As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentIncome As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentExpensesCosts As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalancePreviousIncome As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalancePreviousExpensesCosts As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDTotalExercise As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDDescriptionExercise As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentAccountLvl1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDPreviousBalanceAccountLvl1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDGhSubAuxiliar As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents IncludedAccountNumber As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDBalancePrevious As DevExpress.XtraReports.UI.CalculatedField
End Class
