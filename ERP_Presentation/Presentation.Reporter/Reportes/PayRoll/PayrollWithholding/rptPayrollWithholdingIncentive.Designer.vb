<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptPayrollWithholdingIncentive
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell35 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell34 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell32 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblDateMonth = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrTable7 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow18 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDUserImp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDTypeProcedure = New DevExpress.XtraReports.UI.CalculatedField()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell33 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell36 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell45 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell46 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.INDCfArticle = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfDeclarantType = New DevExpress.XtraReports.UI.CalculatedField()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.HeightF = 20.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100.0!)
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(762.0!, 20.0!)
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UsePadding = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell35, Me.XrTableCell3, Me.XrTableCell5, Me.XrTableCell7, Me.XrTableCell14, Me.XrTableCell15, Me.XrTableCell16})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Nit")})
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell1.StylePriority.UsePadding = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell1.Weight = 0.75196189880371089R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ThirdName")})
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell2.Weight = 1.5002804565429688R
        '
        'XrTableCell35
        '
        Me.XrTableCell35.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "INDCfDeclarantType")})
        Me.XrTableCell35.Name = "XrTableCell35"
        Me.XrTableCell35.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell35.StylePriority.UsePadding = False
        Me.XrTableCell35.StylePriority.UseTextAlignment = False
        Me.XrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell35.Weight = 0.80775762635303638R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "INDTypeProcedure")})
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(4, 0, 0, 0, 100.0!)
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell3.Weight = 0.80775762635303638R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AccruedValue", "{0:c0}")})
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Weight = 0.72196221312956432R
        '
        'XrTableCell7
        '
        Me.XrTableCell7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DeductedValue", "{0:c0}")})
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Weight = 0.72196221312956432R
        '
        'XrTableCell14
        '
        Me.XrTableCell14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RetentionBase", "{0:c0}")})
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Weight = 0.74775878906250026R
        '
        'XrTableCell15
        '
        Me.XrTableCell15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RetentionValue", "{0:c0}")})
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Weight = 0.751961669921875R
        '
        'XrTableCell16
        '
        Me.XrTableCell16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "INDCfArticle")})
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.Weight = 0.75616577148437458R
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
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.INDLblDateMonth, Me.XrLabel13, Me.XrPageInfo3, Me.XrPageInfo1, Me.XrPictureBox2, Me.INDLblCompany, Me.INDLblNitCompany, Me.XrPictureBox1, Me.INDLblTitle})
        Me.PageHeader.HeightF = 236.526!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrTable2
        '
        Me.XrTable2.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable2.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 208.526!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(761.9999!, 28.0!)
        Me.XrTable2.StylePriority.UseBackColor = False
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell17, Me.XrTableCell18, Me.XrTableCell34, Me.XrTableCell19, Me.XrTableCell21, Me.XrTableCell20, Me.XrTableCell30, Me.XrTableCell31, Me.XrTableCell32})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell17
        '
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.Text = "Identificación "
        Me.XrTableCell17.Weight = 0.75196189880371089R
        '
        'XrTableCell18
        '
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.Text = "Nombre"
        Me.XrTableCell18.Weight = 1.5002804565429688R
        '
        'XrTableCell34
        '
        Me.XrTableCell34.Name = "XrTableCell34"
        Me.XrTableCell34.Text = "Declarante"
        Me.XrTableCell34.Weight = 0.80775762635303638R
        '
        'XrTableCell19
        '
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.Text = "Tipo De Procedimiento"
        Me.XrTableCell19.Weight = 0.80775762635303638R
        '
        'XrTableCell21
        '
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Text = "Total Devengado"
        Me.XrTableCell21.Weight = 0.7219620605417203R
        '
        'XrTableCell20
        '
        Me.XrTableCell20.Multiline = True
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.Text = "Total" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Deducido "
        Me.XrTableCell20.Weight = 0.7219620605417203R
        '
        'XrTableCell30
        '
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.Text = "Base Gravable"
        Me.XrTableCell30.Weight = 0.74775878906250026R
        '
        'XrTableCell31
        '
        Me.XrTableCell31.Name = "XrTableCell31"
        Me.XrTableCell31.Text = "Total retención "
        Me.XrTableCell31.Weight = 0.751961669921875R
        '
        'XrTableCell32
        '
        Me.XrTableCell32.Name = "XrTableCell32"
        Me.XrTableCell32.Text = "Articulo"
        Me.XrTableCell32.Weight = 0.75616577148437458R
        '
        'INDLblDateMonth
        '
        Me.INDLblDateMonth.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!)
        Me.INDLblDateMonth.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 147.3177!)
        Me.INDLblDateMonth.Name = "INDLblDateMonth"
        Me.INDLblDateMonth.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDateMonth.SizeF = New System.Drawing.SizeF(761.9999!, 25.0!)
        Me.INDLblDateMonth.StylePriority.UseFont = False
        Me.INDLblDateMonth.StylePriority.UseTextAlignment = False
        Me.INDLblDateMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Arial", 7.5!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(85.95816!, 20.0!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "Fecha Impresión:"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Font = New DevExpress.Drawing.DXFont("Arial", 7.5!)
        Me.XrPageInfo3.Format = "{0:dddd, dd' de 'MMMM' de 'yyyy HH:mm}"
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(85.95797!, 0.0!)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.XrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo3.SizeF = New System.Drawing.SizeF(220.0421!, 20.0!)
        Me.XrPageInfo3.StylePriority.UseFont = False
        Me.XrPageInfo3.StylePriority.UsePadding = False
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New DevExpress.Drawing.DXFont("Arial", 7.5!)
        Me.XrPageInfo1.Format = "Página {0}/{1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(665.0!, 0.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(97.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(665.0!, 32.48437!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(97.0!, 75.00001!)
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(96.99987!, 39.93753!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(568.0001!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(96.99987!, 64.93753!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(568.0001!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 32.48438!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(97.0!, 75.00001!)
        '
        'INDLblTitle
        '
        Me.INDLblTitle.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.INDLblTitle.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 120.3072!)
        Me.INDLblTitle.Name = "INDLblTitle"
        Me.INDLblTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTitle.SizeF = New System.Drawing.SizeF(762.0!, 20.00002!)
        Me.INDLblTitle.StylePriority.UseFont = False
        Me.INDLblTitle.StylePriority.UseTextAlignment = False
        Me.INDLblTitle.Text = "RETENCIÓN DE PRIMAS"
        Me.INDLblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable7})
        Me.PageFooter.HeightF = 20.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrTable7
        '
        Me.XrTable7.BackColor = System.Drawing.Color.Transparent
        Me.XrTable7.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable7.LocationFloat = New DevExpress.Utils.PointFloat(392.1044!, 0.0!)
        Me.XrTable7.Name = "XrTable7"
        Me.XrTable7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow18})
        Me.XrTable7.SizeF = New System.Drawing.SizeF(369.8956!, 20.0!)
        Me.XrTable7.StylePriority.UseBackColor = False
        Me.XrTable7.StylePriority.UseBorders = False
        '
        'XrTableRow18
        '
        Me.XrTableRow18.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDUserImp})
        Me.XrTableRow18.Name = "XrTableRow18"
        Me.XrTableRow18.Weight = 0.8R
        '
        'INDUserImp
        '
        Me.INDUserImp.Font = New DevExpress.Drawing.DXFont("Arial", 7.5!)
        Me.INDUserImp.Name = "INDUserImp"
        Me.INDUserImp.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.INDUserImp.StylePriority.UseFont = False
        Me.INDUserImp.StylePriority.UsePadding = False
        Me.INDUserImp.StylePriority.UseTextAlignment = False
        Me.INDUserImp.Text = "INDUserImp"
        Me.INDUserImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDUserImp.Weight = 1.3871086886123489R
        '
        'INDTypeProcedure
        '
        Me.INDTypeProcedure.Expression = "Iif([ProcedureTypeRTF] = '1', 'Procedimiento 1' , ' Procedimiento 2')"
        Me.INDTypeProcedure.Name = "INDTypeProcedure"
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable4})
        Me.ReportFooter.HeightF = 30.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrTable4
        '
        Me.XrTable4.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrTable4.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!)
        Me.XrTable4.LocationFloat = New DevExpress.Utils.PointFloat(341.257!, 0.0!)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 4, 0, 0, 100.0!)
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow4})
        Me.XrTable4.SizeF = New System.Drawing.SizeF(344.6025!, 20.0!)
        Me.XrTable4.StylePriority.UseBorders = False
        Me.XrTable4.StylePriority.UseFont = False
        Me.XrTable4.StylePriority.UsePadding = False
        Me.XrTable4.StylePriority.UseTextAlignment = False
        Me.XrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell33, Me.XrTableCell8, Me.XrTableCell36, Me.XrTableCell45, Me.XrTableCell46})
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 1.0R
        '
        'XrTableCell33
        '
        Me.XrTableCell33.Font = New DevExpress.Drawing.DXFont("Arial", 8.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrTableCell33.Name = "XrTableCell33"
        Me.XrTableCell33.StylePriority.UseFont = False
        Me.XrTableCell33.Text = "Totales:"
        Me.XrTableCell33.Weight = 0.46976456335809746R
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AccruedValue")})
        Me.XrTableCell8.Name = "XrTableCell8"
        XrSummary1.FormatString = "{0:c0}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell8.Summary = XrSummary1
        Me.XrTableCell8.Weight = 0.72505608356268669R
        '
        'XrTableCell36
        '
        Me.XrTableCell36.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DeductedValue")})
        Me.XrTableCell36.Name = "XrTableCell36"
        XrSummary2.FormatString = "{0:c0}"
        XrSummary2.IgnoreNullValues = True
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell36.Summary = XrSummary2
        Me.XrTableCell36.Weight = 0.72505638873830658R
        '
        'XrTableCell45
        '
        Me.XrTableCell45.Name = "XrTableCell45"
        Me.XrTableCell45.Weight = 0.75096214124921135R
        '
        'XrTableCell46
        '
        Me.XrTableCell46.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RetentionValue")})
        Me.XrTableCell46.Name = "XrTableCell46"
        XrSummary3.FormatString = "{0:c0}"
        XrSummary3.IgnoreNullValues = True
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell46.Summary = XrSummary3
        Me.XrTableCell46.Weight = 0.75518470593606069R
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PayrollRepository.PayrollViewReportWithholdingIncentiveXpo)
        '
        'INDCfArticle
        '
        Me.INDCfArticle.Expression = "Iif([TypeArticleRTF]=0,'Ninguno',Iif([TypeArticleRTF]=1,'Artículo 383','Artículo " & _
    "384' ) )"
        Me.INDCfArticle.Name = "INDCfArticle"
        '
        'INDCfDeclarantType
        '
        Me.INDCfDeclarantType.Expression = "Iif([DeclarantType]=1,'No Declarante','Declarante')"
        Me.INDCfDeclarantType.Name = "INDCfDeclarantType"
        '
        'rptPayrollWithholdingIncentive
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDTypeProcedure, Me.INDCfArticle, Me.INDCfDeclarantType})
        Me.DataSource = Me.BindingSource1
        Me.Margins = New DevExpress.Drawing.DXMargins(22, 21, 26, 25)
        Me.PageHeight = 850
        Me.PageWidth = 805
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents INDLblDateMonth As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblTitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable7 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow18 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDUserImp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDTypeProcedure As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents INDCfArticle As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfDeclarantType As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell34 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell31 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell32 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell35 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell33 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell36 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell45 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell46 As DevExpress.XtraReports.UI.XRTableCell
End Class
