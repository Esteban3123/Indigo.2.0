<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptTreasuryNewsletter
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
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblPreviousBalanceDescription = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblPreviousBalance = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblCodeCash = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNumberAccounts = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblValueBalanceEndDescription = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblValueBalanceEnd = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblValueCreditBank = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionCreditBank = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblValueDebitBank = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionDebitBank = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblValueBalanceEndDescriptionCash = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblValueBalanceEndCash = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblValueCreditCash = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionCreditCash = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblValueDebitCash = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionDebitCash = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblPreviousBalanceDescriptionCash = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblPreviousBalanceCash = New DevExpress.XtraReports.UI.XRTableCell()
        Me.GhBanks = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.GFBanks = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.GfHeaderCash = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.INDTblUser = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow13 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblUserPrint = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSrTreasuryNewsletterExpendituresBank = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSrTreasuryNewsletterReceiptsBank = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSrTreasuryNewsletterReceiptsCash = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSrTreasuryNewsletterExpendituresCash = New DevExpress.XtraReports.UI.XRSubreport()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport3})
        Me.Detail.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Detail.HeightF = 35.41666!
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.StylePriority.UseFont = False
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 23.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 28.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPictureBox2, Me.XrPictureBox1, Me.XrPageInfo3, Me.XrLabel13, Me.XrPageInfo1, Me.INDLblCompany, Me.INDLblNitCompany, Me.INDLblDate, Me.INDLblTitle})
        Me.ReportHeader.HeightF = 154.5209!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(651.0!, 33.52086!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 33.52086!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(85.45834!, 0!)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.XrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo3.SizeF = New System.Drawing.SizeF(191.5834!, 20.0!)
        Me.XrPageInfo3.StylePriority.UseFont = False
        Me.XrPageInfo3.StylePriority.UsePadding = False
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo3.TextFormatString = "{0:dddd, dd' de 'MMMM' de 'yyyy HH:mm}"
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(85.45834!, 20.0!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "Fecha Impresión:"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(651.0!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo1.TextFormatString = "Página {0}/{1}"
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(99.99998!, 33.52086!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(550.9999!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(99.99998!, 58.52086!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(550.9999!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblDate
        '
        Me.INDLblDate.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.INDLblDate.LocationFloat = New DevExpress.Utils.PointFloat(0.0001220703!, 131.5209!)
        Me.INDLblDate.Name = "INDLblDate"
        Me.INDLblDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDate.SizeF = New System.Drawing.SizeF(750.9999!, 23.0!)
        Me.INDLblDate.StylePriority.UseFont = False
        Me.INDLblDate.StylePriority.UseTextAlignment = False
        Me.INDLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblTitle
        '
        Me.INDLblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTitle.LocationFloat = New DevExpress.Utils.PointFloat(0!, 108.5209!)
        Me.INDLblTitle.Name = "INDLblTitle"
        Me.INDLblTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTitle.SizeF = New System.Drawing.SizeF(749.9995!, 23.0!)
        Me.INDLblTitle.StylePriority.UseFont = False
        Me.INDLblTitle.StylePriority.UseTextAlignment = False
        Me.INDLblTitle.Text = "INDLblTitle"
        Me.INDLblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTable1
        '
        Me.XrTable1.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(9.0!, 30.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(300.0!, 20.0!)
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UsePadding = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblPreviousBalanceDescription, Me.INDLblPreviousBalance})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'INDLblPreviousBalanceDescription
        '
        Me.INDLblPreviousBalanceDescription.Name = "INDLblPreviousBalanceDescription"
        Me.INDLblPreviousBalanceDescription.Text = "Saldo Inicial"
        Me.INDLblPreviousBalanceDescription.Weight = 1.0R
        '
        'INDLblPreviousBalance
        '
        Me.INDLblPreviousBalance.Name = "INDLblPreviousBalance"
        Me.INDLblPreviousBalance.TextFormatString = "{0:c2}"
        Me.INDLblPreviousBalance.Weight = 1.0R
        '
        'INDLblCodeCash
        '
        Me.INDLblCodeCash.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblCodeCash.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDLblCodeCash.Name = "INDLblCodeCash"
        Me.INDLblCodeCash.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCodeCash.SizeF = New System.Drawing.SizeF(751.0!, 20.0!)
        Me.INDLblCodeCash.StylePriority.UseFont = False
        Me.INDLblCodeCash.StylePriority.UseTextAlignment = False
        Me.INDLblCodeCash.Text = "INDLblCodeCash"
        Me.INDLblCodeCash.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNumberAccounts
        '
        Me.INDLblNumberAccounts.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNumberAccounts.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDLblNumberAccounts.Name = "INDLblNumberAccounts"
        Me.INDLblNumberAccounts.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNumberAccounts.SizeF = New System.Drawing.SizeF(751.0!, 20.0!)
        Me.INDLblNumberAccounts.StylePriority.UseFont = False
        Me.INDLblNumberAccounts.StylePriority.UseTextAlignment = False
        Me.INDLblNumberAccounts.Text = "INDLblNumberAccounts"
        Me.INDLblNumberAccounts.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTable2
        '
        Me.XrTable2.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(9.000041!, 33.64563!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(300.0!, 20.0!)
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UsePadding = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblValueBalanceEndDescription, Me.INDLblValueBalanceEnd})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'INDLblValueBalanceEndDescription
        '
        Me.INDLblValueBalanceEndDescription.Name = "INDLblValueBalanceEndDescription"
        Me.INDLblValueBalanceEndDescription.Text = "Saldo Total"
        Me.INDLblValueBalanceEndDescription.Weight = 1.0R
        '
        'INDLblValueBalanceEnd
        '
        Me.INDLblValueBalanceEnd.Name = "INDLblValueBalanceEnd"
        Me.INDLblValueBalanceEnd.TextFormatString = "{0:c2}"
        Me.INDLblValueBalanceEnd.Weight = 1.0R
        '
        'INDLblValueCreditBank
        '
        Me.INDLblValueCreditBank.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueCreditBank.LocationFloat = New DevExpress.Utils.PointFloat(629.9995!, 0!)
        Me.INDLblValueCreditBank.Name = "INDLblValueCreditBank"
        Me.INDLblValueCreditBank.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblValueCreditBank.SizeF = New System.Drawing.SizeF(120.0!, 20.0!)
        Me.INDLblValueCreditBank.StylePriority.UseFont = False
        Me.INDLblValueCreditBank.StylePriority.UsePadding = False
        Me.INDLblValueCreditBank.StylePriority.UseTextAlignment = False
        Me.INDLblValueCreditBank.Text = " "
        Me.INDLblValueCreditBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblValueCreditBank.TextFormatString = "{0:c2}"
        '
        'INDLblDescriptionCreditBank
        '
        Me.INDLblDescriptionCreditBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionCreditBank.LocationFloat = New DevExpress.Utils.PointFloat(375.0!, 0!)
        Me.INDLblDescriptionCreditBank.Name = "INDLblDescriptionCreditBank"
        Me.INDLblDescriptionCreditBank.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblDescriptionCreditBank.SizeF = New System.Drawing.SizeF(254.9995!, 20.0!)
        Me.INDLblDescriptionCreditBank.StylePriority.UseFont = False
        Me.INDLblDescriptionCreditBank.StylePriority.UsePadding = False
        Me.INDLblDescriptionCreditBank.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionCreditBank.Text = "Total Créditos"
        Me.INDLblDescriptionCreditBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblValueDebitBank
        '
        Me.INDLblValueDebitBank.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueDebitBank.LocationFloat = New DevExpress.Utils.PointFloat(243.75!, 0!)
        Me.INDLblValueDebitBank.Name = "INDLblValueDebitBank"
        Me.INDLblValueDebitBank.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblValueDebitBank.SizeF = New System.Drawing.SizeF(131.0!, 20.0!)
        Me.INDLblValueDebitBank.StylePriority.UseFont = False
        Me.INDLblValueDebitBank.StylePriority.UsePadding = False
        Me.INDLblValueDebitBank.StylePriority.UseTextAlignment = False
        Me.INDLblValueDebitBank.Text = " "
        Me.INDLblValueDebitBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblValueDebitBank.TextFormatString = "{0:c2}"
        '
        'INDLblDescriptionDebitBank
        '
        Me.INDLblDescriptionDebitBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionDebitBank.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDLblDescriptionDebitBank.Name = "INDLblDescriptionDebitBank"
        Me.INDLblDescriptionDebitBank.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblDescriptionDebitBank.SizeF = New System.Drawing.SizeF(243.75!, 20.0!)
        Me.INDLblDescriptionDebitBank.StylePriority.UseFont = False
        Me.INDLblDescriptionDebitBank.StylePriority.UsePadding = False
        Me.INDLblDescriptionDebitBank.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionDebitBank.Text = "Total Débitos"
        Me.INDLblDescriptionDebitBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTable4
        '
        Me.XrTable4.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTable4.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 32.52019!)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow4})
        Me.XrTable4.SizeF = New System.Drawing.SizeF(300.0!, 20.0!)
        Me.XrTable4.StylePriority.UseFont = False
        Me.XrTable4.StylePriority.UsePadding = False
        Me.XrTable4.Visible = False
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblValueBalanceEndDescriptionCash, Me.INDLblValueBalanceEndCash})
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 1.0R
        '
        'INDLblValueBalanceEndDescriptionCash
        '
        Me.INDLblValueBalanceEndDescriptionCash.Name = "INDLblValueBalanceEndDescriptionCash"
        Me.INDLblValueBalanceEndDescriptionCash.Text = "Saldo Total"
        Me.INDLblValueBalanceEndDescriptionCash.Weight = 1.0R
        '
        'INDLblValueBalanceEndCash
        '
        Me.INDLblValueBalanceEndCash.Name = "INDLblValueBalanceEndCash"
        Me.INDLblValueBalanceEndCash.TextFormatString = "{0:c2}"
        Me.INDLblValueBalanceEndCash.Weight = 1.0R
        '
        'INDLblValueCreditCash
        '
        Me.INDLblValueCreditCash.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueCreditCash.LocationFloat = New DevExpress.Utils.PointFloat(630.9999!, 0!)
        Me.INDLblValueCreditCash.Name = "INDLblValueCreditCash"
        Me.INDLblValueCreditCash.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblValueCreditCash.SizeF = New System.Drawing.SizeF(120.0!, 20.0!)
        Me.INDLblValueCreditCash.StylePriority.UseFont = False
        Me.INDLblValueCreditCash.StylePriority.UsePadding = False
        Me.INDLblValueCreditCash.TextFormatString = "{0:c2}"
        '
        'INDLblDescriptionCreditCash
        '
        Me.INDLblDescriptionCreditCash.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionCreditCash.LocationFloat = New DevExpress.Utils.PointFloat(375.9999!, 0!)
        Me.INDLblDescriptionCreditCash.Name = "INDLblDescriptionCreditCash"
        Me.INDLblDescriptionCreditCash.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblDescriptionCreditCash.SizeF = New System.Drawing.SizeF(255.0!, 20.0!)
        Me.INDLblDescriptionCreditCash.StylePriority.UseFont = False
        Me.INDLblDescriptionCreditCash.StylePriority.UsePadding = False
        Me.INDLblDescriptionCreditCash.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionCreditCash.Text = "Total Créditos"
        Me.INDLblDescriptionCreditCash.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblValueDebitCash
        '
        Me.INDLblValueDebitCash.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueDebitCash.LocationFloat = New DevExpress.Utils.PointFloat(243.0!, 0.00003051758!)
        Me.INDLblValueDebitCash.Name = "INDLblValueDebitCash"
        Me.INDLblValueDebitCash.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblValueDebitCash.SizeF = New System.Drawing.SizeF(132.9999!, 20.0!)
        Me.INDLblValueDebitCash.StylePriority.UseFont = False
        Me.INDLblValueDebitCash.StylePriority.UsePadding = False
        Me.INDLblValueDebitCash.StylePriority.UseTextAlignment = False
        Me.INDLblValueDebitCash.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblValueDebitCash.TextFormatString = "{0:c2}"
        '
        'INDLblDescriptionDebitCash
        '
        Me.INDLblDescriptionDebitCash.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionDebitCash.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDLblDescriptionDebitCash.Name = "INDLblDescriptionDebitCash"
        Me.INDLblDescriptionDebitCash.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblDescriptionDebitCash.SizeF = New System.Drawing.SizeF(243.0!, 20.0!)
        Me.INDLblDescriptionDebitCash.StylePriority.UseFont = False
        Me.INDLblDescriptionDebitCash.StylePriority.UsePadding = False
        Me.INDLblDescriptionDebitCash.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionDebitCash.Text = "Total Débitos"
        Me.INDLblDescriptionDebitCash.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTable3
        '
        Me.XrTable3.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTable3.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 30.0!)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable3.SizeF = New System.Drawing.SizeF(300.0!, 20.0!)
        Me.XrTable3.StylePriority.UseFont = False
        Me.XrTable3.StylePriority.UsePadding = False
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblPreviousBalanceDescriptionCash, Me.INDLblPreviousBalanceCash})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'INDLblPreviousBalanceDescriptionCash
        '
        Me.INDLblPreviousBalanceDescriptionCash.Name = "INDLblPreviousBalanceDescriptionCash"
        Me.INDLblPreviousBalanceDescriptionCash.Text = "Saldo Inicial"
        Me.INDLblPreviousBalanceDescriptionCash.Weight = 1.0R
        '
        'INDLblPreviousBalanceCash
        '
        Me.INDLblPreviousBalanceCash.Name = "INDLblPreviousBalanceCash"
        Me.INDLblPreviousBalanceCash.TextFormatString = "{0:c2}"
        Me.INDLblPreviousBalanceCash.Weight = 1.0R
        '
        'GhBanks
        '
        Me.GhBanks.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDSrTreasuryNewsletterExpendituresBank, Me.INDSrTreasuryNewsletterReceiptsBank, Me.INDLblNumberAccounts, Me.XrTable1})
        Me.GhBanks.HeightF = 176.5418!
        Me.GhBanks.Level = 1
        Me.GhBanks.Name = "GhBanks"
        Me.GhBanks.Visible = False
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblDescriptionDebitCash, Me.INDLblValueDebitCash, Me.INDLblDescriptionCreditCash, Me.INDLblValueCreditCash, Me.XrTable4})
        Me.ReportFooter.HeightF = 62.52019!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'GFBanks
        '
        Me.GFBanks.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblDescriptionDebitBank, Me.INDLblValueDebitBank, Me.INDLblDescriptionCreditBank, Me.INDLblValueCreditBank, Me.XrTable2})
        Me.GFBanks.HeightF = 63.64563!
        Me.GFBanks.Name = "GFBanks"
        '
        'GfHeaderCash
        '
        Me.GfHeaderCash.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblCodeCash, Me.XrTable3, Me.INDSrTreasuryNewsletterReceiptsCash, Me.INDSrTreasuryNewsletterExpendituresCash})
        Me.GfHeaderCash.HeightF = 176.0!
        Me.GfHeaderCash.Level = 1
        Me.GfHeaderCash.Name = "GfHeaderCash"
        Me.GfHeaderCash.Visible = False
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblUser})
        Me.PageFooter.HeightF = 35.89605!
        Me.PageFooter.Name = "PageFooter"
        '
        'INDTblUser
        '
        Me.INDTblUser.LocationFloat = New DevExpress.Utils.PointFloat(420.756!, 10.0!)
        Me.INDTblUser.Name = "INDTblUser"
        Me.INDTblUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDTblUser.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow13})
        Me.INDTblUser.SizeF = New System.Drawing.SizeF(338.244!, 20.0!)
        Me.INDTblUser.StylePriority.UsePadding = False
        Me.INDTblUser.StylePriority.UseTextAlignment = False
        Me.INDTblUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow13
        '
        Me.XrTableRow13.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblUserPrint})
        Me.XrTableRow13.Name = "XrTableRow13"
        Me.XrTableRow13.Weight = 1.0R
        '
        'INDLblUserPrint
        '
        Me.INDLblUserPrint.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblUserPrint.Name = "INDLblUserPrint"
        Me.INDLblUserPrint.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblUserPrint.StylePriority.UseFont = False
        Me.INDLblUserPrint.StylePriority.UsePadding = False
        Me.INDLblUserPrint.StylePriority.UseTextAlignment = False
        Me.INDLblUserPrint.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblUserPrint.Weight = 1.1364997978888423R
        '
        'XrSubreport3
        '
        Me.XrSubreport3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.ReportSource = New Presentation.Reporter.rptTreasuryNewsletterSummaryNewsletter()
        Me.XrSubreport3.SizeF = New System.Drawing.SizeF(749.9996!, 35.41666!)
        Me.XrSubreport3.Visible = False
        '
        'INDSrTreasuryNewsletterExpendituresBank
        '
        Me.INDSrTreasuryNewsletterExpendituresBank.LocationFloat = New DevExpress.Utils.PointFloat(375.0!, 65.00006!)
        Me.INDSrTreasuryNewsletterExpendituresBank.Name = "INDSrTreasuryNewsletterExpendituresBank"
        Me.INDSrTreasuryNewsletterExpendituresBank.ReportSource = New Presentation.Reporter.rptTreasuryNewsletterExpenditures()
        Me.INDSrTreasuryNewsletterExpendituresBank.SizeF = New System.Drawing.SizeF(376.0!, 111.5417!)
        '
        'INDSrTreasuryNewsletterReceiptsBank
        '
        Me.INDSrTreasuryNewsletterReceiptsBank.LocationFloat = New DevExpress.Utils.PointFloat(0.25!, 65.00002!)
        Me.INDSrTreasuryNewsletterReceiptsBank.Name = "INDSrTreasuryNewsletterReceiptsBank"
        Me.INDSrTreasuryNewsletterReceiptsBank.ReportSource = New Presentation.Reporter.rptTreasuryNewsletterReceipts()
        Me.INDSrTreasuryNewsletterReceiptsBank.SizeF = New System.Drawing.SizeF(374.75!, 111.5417!)
        '
        'INDSrTreasuryNewsletterReceiptsCash
        '
        Me.INDSrTreasuryNewsletterReceiptsCash.LocationFloat = New DevExpress.Utils.PointFloat(0!, 65.0!)
        Me.INDSrTreasuryNewsletterReceiptsCash.Name = "INDSrTreasuryNewsletterReceiptsCash"
        Me.INDSrTreasuryNewsletterReceiptsCash.ReportSource = New Presentation.Reporter.rptTreasuryNewsletterReceiptsCash()
        Me.INDSrTreasuryNewsletterReceiptsCash.SizeF = New System.Drawing.SizeF(375.0!, 111.0!)
        '
        'INDSrTreasuryNewsletterExpendituresCash
        '
        Me.INDSrTreasuryNewsletterExpendituresCash.LocationFloat = New DevExpress.Utils.PointFloat(375.9998!, 65.00002!)
        Me.INDSrTreasuryNewsletterExpendituresCash.Name = "INDSrTreasuryNewsletterExpendituresCash"
        Me.INDSrTreasuryNewsletterExpendituresCash.ReportSource = New Presentation.Reporter.rptTreasuryNewsletterExpendituresCash()
        Me.INDSrTreasuryNewsletterExpendituresCash.SizeF = New System.Drawing.SizeF(375.0002!, 111.0!)
        '
        'rptTreasuryNewsletter
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.GhBanks, Me.ReportFooter, Me.GFBanks, Me.GfHeaderCash, Me.PageFooter})
        Me.Margins = New System.Drawing.Printing.Margins(43, 48, 23, 28)
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.Version = "20.1"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Public WithEvents INDSrTreasuryNewsletterReceiptsBank As DevExpress.XtraReports.UI.XRSubreport
    Public WithEvents INDSrTreasuryNewsletterExpendituresBank As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents INDLblTitle As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDLblNumberAccounts As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblCodeCash As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueCreditBank As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionCreditBank As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueDebitBank As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionDebitBank As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblPreviousBalanceDescription As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblPreviousBalance As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblValueBalanceEndDescription As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblValueBalanceEnd As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueCreditCash As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionCreditCash As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueDebitCash As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionDebitCash As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents INDSrTreasuryNewsletterExpendituresCash As DevExpress.XtraReports.UI.XRSubreport
    Public WithEvents INDSrTreasuryNewsletterReceiptsCash As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblValueBalanceEndDescriptionCash As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblValueBalanceEndCash As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblPreviousBalanceDescriptionCash As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblPreviousBalanceCash As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GhBanks As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents GFBanks As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GfHeaderCash As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents INDTblUser As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow13 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblUserPrint As DevExpress.XtraReports.UI.XRTableCell
End Class
