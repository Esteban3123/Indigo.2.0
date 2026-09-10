<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptGeneralBalance
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
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
        Me.INDLblValueExercise = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblExercise = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblValueTotalPasivoPatrimonio = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTotalPasivoPatrimonio = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblValueTotalActivo = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblTotalActivo = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.INDTblUser = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow24 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblUserPrint = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport2, Me.XrSubreport1})
        Me.Detail.HeightF = 58.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrSubreport2
        '
        Me.XrSubreport2.LocationFloat = New DevExpress.Utils.PointFloat(408.0!, 0!)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ReportSource = New Presentation.Reporter.rptGeneralBalanceNature()
        Me.XrSubreport2.SizeF = New System.Drawing.SizeF(391.0!, 58.0!)
        '
        'XrSubreport1
        '
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptGeneralBalanceNature()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(391.0!, 58.0!)
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
        Me.BottomMargin.HeightF = 26.04167!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblNitCompany, Me.INDLblCompany, Me.XrPageInfo1, Me.XrLabel13, Me.XrPageInfo3, Me.XrPictureBox1, Me.XrPictureBox2, Me.INDLblDate, Me.INDLblTitle})
        Me.ReportHeader.HeightF = 183.3333!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(99.99998!, 58.52089!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(599.0!, 25.0!)
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
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(599.0!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(699.0!, 0!)
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
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(699.0!, 33.52089!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblDate
        '
        Me.INDLblDate.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.INDLblDate.LocationFloat = New DevExpress.Utils.PointFloat(0.00002384186!, 144.875!)
        Me.INDLblDate.Name = "INDLblDate"
        Me.INDLblDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDate.SizeF = New System.Drawing.SizeF(799.0!, 23.0!)
        Me.INDLblDate.StylePriority.UseFont = False
        Me.INDLblDate.StylePriority.UseTextAlignment = False
        Me.INDLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblTitle
        '
        Me.INDLblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTitle.LocationFloat = New DevExpress.Utils.PointFloat(0.00002384186!, 121.875!)
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
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblValueExercise, Me.INDLblExercise, Me.INDLblValueTotalPasivoPatrimonio, Me.INDLblTotalPasivoPatrimonio, Me.INDLblValueTotalActivo, Me.INDLblTotalActivo})
        Me.ReportFooter.HeightF = 53.20829!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'INDLblValueExercise
        '
        Me.INDLblValueExercise.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueExercise.LocationFloat = New DevExpress.Utils.PointFloat(660.0!, 0!)
        Me.INDLblValueExercise.Name = "INDLblValueExercise"
        Me.INDLblValueExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblValueExercise.SizeF = New System.Drawing.SizeF(139.0!, 20.0!)
        Me.INDLblValueExercise.StylePriority.UseFont = False
        Me.INDLblValueExercise.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "n"
        Me.INDLblValueExercise.Summary = XrSummary1
        Me.INDLblValueExercise.Text = " "
        Me.INDLblValueExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblExercise
        '
        Me.INDLblExercise.BackColor = System.Drawing.Color.Transparent
        Me.INDLblExercise.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblExercise.LocationFloat = New DevExpress.Utils.PointFloat(407.9999!, 0!)
        Me.INDLblExercise.Name = "INDLblExercise"
        Me.INDLblExercise.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblExercise.SizeF = New System.Drawing.SizeF(252.0!, 20.0!)
        Me.INDLblExercise.StylePriority.UseBackColor = False
        Me.INDLblExercise.StylePriority.UseFont = False
        Me.INDLblExercise.StylePriority.UseTextAlignment = False
        Me.INDLblExercise.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblValueTotalPasivoPatrimonio
        '
        Me.INDLblValueTotalPasivoPatrimonio.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueTotalPasivoPatrimonio.LocationFloat = New DevExpress.Utils.PointFloat(660.0!, 20.0!)
        Me.INDLblValueTotalPasivoPatrimonio.Name = "INDLblValueTotalPasivoPatrimonio"
        Me.INDLblValueTotalPasivoPatrimonio.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblValueTotalPasivoPatrimonio.SizeF = New System.Drawing.SizeF(139.0!, 20.0!)
        Me.INDLblValueTotalPasivoPatrimonio.StylePriority.UseFont = False
        Me.INDLblValueTotalPasivoPatrimonio.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "n"
        Me.INDLblValueTotalPasivoPatrimonio.Summary = XrSummary2
        Me.INDLblValueTotalPasivoPatrimonio.Text = " "
        Me.INDLblValueTotalPasivoPatrimonio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblTotalPasivoPatrimonio
        '
        Me.INDLblTotalPasivoPatrimonio.BackColor = System.Drawing.Color.Transparent
        Me.INDLblTotalPasivoPatrimonio.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTotalPasivoPatrimonio.LocationFloat = New DevExpress.Utils.PointFloat(407.9999!, 20.0!)
        Me.INDLblTotalPasivoPatrimonio.Name = "INDLblTotalPasivoPatrimonio"
        Me.INDLblTotalPasivoPatrimonio.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTotalPasivoPatrimonio.SizeF = New System.Drawing.SizeF(252.0!, 20.0!)
        Me.INDLblTotalPasivoPatrimonio.StylePriority.UseBackColor = False
        Me.INDLblTotalPasivoPatrimonio.StylePriority.UseFont = False
        Me.INDLblTotalPasivoPatrimonio.StylePriority.UseTextAlignment = False
        Me.INDLblTotalPasivoPatrimonio.Text = "Total Pasivo + Patrimonio"
        Me.INDLblTotalPasivoPatrimonio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblValueTotalActivo
        '
        Me.INDLblValueTotalActivo.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblValueTotalActivo.LocationFloat = New DevExpress.Utils.PointFloat(251.0!, 20.0!)
        Me.INDLblValueTotalActivo.Name = "INDLblValueTotalActivo"
        Me.INDLblValueTotalActivo.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblValueTotalActivo.SizeF = New System.Drawing.SizeF(140.0!, 20.0!)
        Me.INDLblValueTotalActivo.StylePriority.UseFont = False
        Me.INDLblValueTotalActivo.StylePriority.UseTextAlignment = False
        XrSummary3.FormatString = "n"
        Me.INDLblValueTotalActivo.Summary = XrSummary3
        Me.INDLblValueTotalActivo.Text = " "
        Me.INDLblValueTotalActivo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'INDLblTotalActivo
        '
        Me.INDLblTotalActivo.BackColor = System.Drawing.Color.Transparent
        Me.INDLblTotalActivo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTotalActivo.LocationFloat = New DevExpress.Utils.PointFloat(0!, 20.0!)
        Me.INDLblTotalActivo.Name = "INDLblTotalActivo"
        Me.INDLblTotalActivo.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTotalActivo.SizeF = New System.Drawing.SizeF(251.0!, 20.0!)
        Me.INDLblTotalActivo.StylePriority.UseBackColor = False
        Me.INDLblTotalActivo.StylePriority.UseFont = False
        Me.INDLblTotalActivo.StylePriority.UseTextAlignment = False
        Me.INDLblTotalActivo.Text = "Total Activo"
        Me.INDLblTotalActivo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblUser})
        Me.PageFooter.HeightF = 40.83334!
        Me.PageFooter.Name = "PageFooter"
        '
        'INDTblUser
        '
        Me.INDTblUser.LocationFloat = New DevExpress.Utils.PointFloat(505.4573!, 20.83333!)
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
        'rptGeneralBalance
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter, Me.PageFooter})
        Me.Margins = New System.Drawing.Printing.Margins(24, 27, 25, 26)
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "19.1"
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents INDLblTitle As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Public WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents INDLblValueTotalPasivoPatrimonio As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblTotalPasivoPatrimonio As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueTotalActivo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblTotalActivo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblValueExercise As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblExercise As DevExpress.XtraReports.UI.XRLabel
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
End Class
