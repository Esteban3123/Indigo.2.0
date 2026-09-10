<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptResultStatus
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.INDLblBook = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.INDLblTotalExpensesCosts = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionExpensesCosts = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTotalExercise = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionExercise = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTotalIncoime = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDescriptionIncome = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.INDTblUser = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow24 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblUserPrint = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDSbrTrimester = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSbrStandar = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSbrCosts = New DevExpress.XtraReports.UI.XRSubreport()
        Me.INDSbrIncome = New DevExpress.XtraReports.UI.XRSubreport()
        Me.CurrencyLabel = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDSbrTrimester, Me.INDSbrStandar, Me.INDSbrCosts, Me.INDSbrIncome})
        Me.Detail.HeightF = 58.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 25.0!
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
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.CurrencyLabel, Me.INDLblBook, Me.INDLblNitCompany, Me.INDLblCompany, Me.XrPageInfo1, Me.XrLabel13, Me.XrPageInfo3, Me.XrPictureBox1, Me.XrPictureBox2, Me.INDLblDate, Me.INDLblTitle})
        Me.ReportHeader.HeightF = 210.4167!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'INDLblBook
        '
        Me.INDLblBook.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblBook.LocationFloat = New DevExpress.Utils.PointFloat(0!, 136.3748!)
        Me.INDLblBook.Name = "INDLblBook"
        Me.INDLblBook.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblBook.SizeF = New System.Drawing.SizeF(798.9999!, 25.0!)
        Me.INDLblBook.StylePriority.UseFont = False
        Me.INDLblBook.StylePriority.UseTextAlignment = False
        Me.INDLblBook.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 58.52089!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(599.0001!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 33.52089!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(599.0001!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(699.0001!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo1.TextFormatString = "Página {0}/{1}"
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
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 33.52089!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(699.0001!, 33.52089!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblDate
        '
        Me.INDLblDate.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.INDLblDate.LocationFloat = New DevExpress.Utils.PointFloat(0!, 184.375!)
        Me.INDLblDate.Name = "INDLblDate"
        Me.INDLblDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDate.SizeF = New System.Drawing.SizeF(799.0!, 23.00002!)
        Me.INDLblDate.StylePriority.UseFont = False
        Me.INDLblDate.StylePriority.UseTextAlignment = False
        Me.INDLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblTitle
        '
        Me.INDLblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTitle.LocationFloat = New DevExpress.Utils.PointFloat(0!, 113.3748!)
        Me.INDLblTitle.Name = "INDLblTitle"
        Me.INDLblTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTitle.SizeF = New System.Drawing.SizeF(799.0!, 23.0!)
        Me.INDLblTitle.StylePriority.UseFont = False
        Me.INDLblTitle.StylePriority.UseTextAlignment = False
        Me.INDLblTitle.Text = "INDLblTitle"
        Me.INDLblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblTotalExpensesCosts, Me.INDLblDescriptionExpensesCosts, Me.INDLblTotalExercise, Me.INDLblDescriptionExercise, Me.INDLblTotalIncoime, Me.INDLblDescriptionIncome})
        Me.ReportFooter.HeightF = 67.70834!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'INDLblTotalExpensesCosts
        '
        Me.INDLblTotalExpensesCosts.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblTotalExpensesCosts.LocationFloat = New DevExpress.Utils.PointFloat(660.0001!, 20.0!)
        Me.INDLblTotalExpensesCosts.Name = "INDLblTotalExpensesCosts"
        Me.INDLblTotalExpensesCosts.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTotalExpensesCosts.SizeF = New System.Drawing.SizeF(139.0!, 20.0!)
        Me.INDLblTotalExpensesCosts.StylePriority.UseFont = False
        Me.INDLblTotalExpensesCosts.StylePriority.UseTextAlignment = False
        Me.INDLblTotalExpensesCosts.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblTotalExpensesCosts.Visible = False
        '
        'INDLblDescriptionExpensesCosts
        '
        Me.INDLblDescriptionExpensesCosts.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionExpensesCosts.LocationFloat = New DevExpress.Utils.PointFloat(408.0002!, 20.0!)
        Me.INDLblDescriptionExpensesCosts.Name = "INDLblDescriptionExpensesCosts"
        Me.INDLblDescriptionExpensesCosts.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDescriptionExpensesCosts.SizeF = New System.Drawing.SizeF(252.0!, 20.0!)
        Me.INDLblDescriptionExpensesCosts.StylePriority.UseFont = False
        Me.INDLblDescriptionExpensesCosts.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionExpensesCosts.Text = "Total Gastos + Costos"
        Me.INDLblDescriptionExpensesCosts.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblDescriptionExpensesCosts.Visible = False
        '
        'INDLblTotalExercise
        '
        Me.INDLblTotalExercise.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblTotalExercise.LocationFloat = New DevExpress.Utils.PointFloat(660.0001!, 0!)
        Me.INDLblTotalExercise.Name = "INDLblTotalExercise"
        Me.INDLblTotalExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTotalExercise.SizeF = New System.Drawing.SizeF(139.0!, 20.0!)
        Me.INDLblTotalExercise.StylePriority.UseFont = False
        Me.INDLblTotalExercise.StylePriority.UseTextAlignment = False
        Me.INDLblTotalExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblTotalExercise.Visible = False
        '
        'INDLblDescriptionExercise
        '
        Me.INDLblDescriptionExercise.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionExercise.LocationFloat = New DevExpress.Utils.PointFloat(408.0002!, 0!)
        Me.INDLblDescriptionExercise.Name = "INDLblDescriptionExercise"
        Me.INDLblDescriptionExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDescriptionExercise.SizeF = New System.Drawing.SizeF(252.0!, 20.0!)
        Me.INDLblDescriptionExercise.StylePriority.UseFont = False
        Me.INDLblDescriptionExercise.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblDescriptionExercise.Visible = False
        '
        'INDLblTotalIncoime
        '
        Me.INDLblTotalIncoime.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblTotalIncoime.LocationFloat = New DevExpress.Utils.PointFloat(252.0!, 20.0!)
        Me.INDLblTotalIncoime.Name = "INDLblTotalIncoime"
        Me.INDLblTotalIncoime.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTotalIncoime.SizeF = New System.Drawing.SizeF(139.0!, 20.0!)
        Me.INDLblTotalIncoime.StylePriority.UseFont = False
        Me.INDLblTotalIncoime.StylePriority.UseTextAlignment = False
        Me.INDLblTotalIncoime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblTotalIncoime.Visible = False
        '
        'INDLblDescriptionIncome
        '
        Me.INDLblDescriptionIncome.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblDescriptionIncome.LocationFloat = New DevExpress.Utils.PointFloat(0!, 20.0!)
        Me.INDLblDescriptionIncome.Name = "INDLblDescriptionIncome"
        Me.INDLblDescriptionIncome.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDescriptionIncome.SizeF = New System.Drawing.SizeF(252.0!, 20.0!)
        Me.INDLblDescriptionIncome.StylePriority.UseFont = False
        Me.INDLblDescriptionIncome.StylePriority.UseTextAlignment = False
        Me.INDLblDescriptionIncome.Text = "Total Ingresos"
        Me.INDLblDescriptionIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblDescriptionIncome.Visible = False
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblUser})
        Me.PageFooter.HeightF = 47.91667!
        Me.PageFooter.Name = "PageFooter"
        '
        'INDTblUser
        '
        Me.INDTblUser.LocationFloat = New DevExpress.Utils.PointFloat(505.4574!, 27.08333!)
        Me.INDTblUser.Name = "INDTblUser"
        Me.INDTblUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDTblUser.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow24})
        Me.INDTblUser.SizeF = New System.Drawing.SizeF(293.5427!, 20.0!)
        Me.INDTblUser.StylePriority.UsePadding = False
        Me.INDTblUser.StylePriority.UseTextAlignment = False
        Me.INDTblUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow24
        '
        Me.XrTableRow24.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDLblUserPrint})
        Me.XrTableRow24.Name = "XrTableRow24"
        Me.XrTableRow24.Weight = 1.0R
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
        Me.INDLblUserPrint.Weight = 1.0559997656068969R
        '
        'INDSbrTrimester
        '
        Me.INDSbrTrimester.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDSbrTrimester.Name = "INDSbrTrimester"
        Me.INDSbrTrimester.ReportSource = New Presentation.Reporter.rptResultStatusTrimester()
        Me.INDSbrTrimester.SizeF = New System.Drawing.SizeF(798.9999!, 57.99999!)
        Me.INDSbrTrimester.Visible = False
        '
        'INDSbrStandar
        '
        Me.INDSbrStandar.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDSbrStandar.Name = "INDSbrStandar"
        Me.INDSbrStandar.ReportSource = New Presentation.Reporter.rptResultStatusStandar()
        Me.INDSbrStandar.SizeF = New System.Drawing.SizeF(798.9999!, 57.99999!)
        Me.INDSbrStandar.Visible = False
        '
        'INDSbrCosts
        '
        Me.INDSbrCosts.LocationFloat = New DevExpress.Utils.PointFloat(408.0001!, 0!)
        Me.INDSbrCosts.Name = "INDSbrCosts"
        Me.INDSbrCosts.ReportSource = New Presentation.Reporter.rptResultStatusExpensesCosts()
        Me.INDSbrCosts.SizeF = New System.Drawing.SizeF(391.0!, 58.0!)
        Me.INDSbrCosts.Visible = False
        '
        'INDSbrIncome
        '
        Me.INDSbrIncome.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDSbrIncome.Name = "INDSbrIncome"
        Me.INDSbrIncome.ReportSource = New Presentation.Reporter.rptResultStatusIncome()
        Me.INDSbrIncome.SizeF = New System.Drawing.SizeF(391.0!, 58.0!)
        Me.INDSbrIncome.Visible = False
        '
        'CurrencyLabel
        '
        Me.CurrencyLabel.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.CurrencyLabel.LocationFloat = New DevExpress.Utils.PointFloat(0!, 161.375!)
        Me.CurrencyLabel.Name = "CurrencyLabel"
        Me.CurrencyLabel.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.CurrencyLabel.SizeF = New System.Drawing.SizeF(799.0!, 23.0!)
        Me.CurrencyLabel.StylePriority.UseFont = False
        Me.CurrencyLabel.StylePriority.UseTextAlignment = False
        Me.CurrencyLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'rptResultStatus
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter, Me.PageFooter})
        Me.Margins = New System.Drawing.Printing.Margins(26, 25, 25, 25)
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "20.1"
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents INDLblTitle As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents INDSbrIncome As DevExpress.XtraReports.UI.XRSubreport
    Public WithEvents INDSbrCosts As DevExpress.XtraReports.UI.XRSubreport
    Public WithEvents INDSbrStandar As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents INDLblTotalExpensesCosts As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionExpensesCosts As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblTotalExercise As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionExercise As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblTotalIncoime As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDescriptionIncome As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents INDSbrTrimester As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents INDLblDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents INDTblUser As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow24 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblUserPrint As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblBook As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents CurrencyLabel As DevExpress.XtraReports.UI.XRLabel
End Class
