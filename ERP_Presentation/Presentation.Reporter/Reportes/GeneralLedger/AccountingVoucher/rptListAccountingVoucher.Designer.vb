<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptListAccountingVoucher
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrTable8 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell51 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell55 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell52 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell56 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell59 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell54 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell60 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell58 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblDates = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell32 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell33 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDNameMoviment = New DevExpress.XtraReports.UI.CalculatedField()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.INDTblUser = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow24 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDLblUserPrint = New DevExpress.XtraReports.UI.XRTableCell()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.INDGroupBy = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDLblBook = New DevExpress.XtraReports.UI.XRLabel()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblBook, Me.XrTable8, Me.INDLblTitle, Me.INDLblDates, Me.XrPictureBox2, Me.XrPictureBox1, Me.INDLblCompany, Me.INDLblNitCompany, Me.XrLabel13, Me.XrPageInfo3, Me.XrPageInfo1})
        Me.PageHeader.HeightF = 221.8646!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrTable8
        '
        Me.XrTable8.BackColor = System.Drawing.Color.Transparent
        Me.XrTable8.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable8.LocationFloat = New DevExpress.Utils.PointFloat(0.0004053116!, 200.4063!)
        Me.XrTable8.Name = "XrTable8"
        Me.XrTable8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow9})
        Me.XrTable8.SizeF = New System.Drawing.SizeF(1049.0!, 21.45831!)
        Me.XrTable8.StylePriority.UseBackColor = False
        Me.XrTable8.StylePriority.UseBorders = False
        Me.XrTable8.StylePriority.UseFont = False
        Me.XrTable8.StylePriority.UseTextAlignment = False
        Me.XrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell51, Me.XrTableCell55, Me.XrTableCell52, Me.XrTableCell56, Me.XrTableCell59, Me.XrTableCell54, Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3, Me.XrTableCell60, Me.XrTableCell58})
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 0.8R
        '
        'XrTableCell51
        '
        Me.XrTableCell51.Name = "XrTableCell51"
        Me.XrTableCell51.StylePriority.UseTextAlignment = False
        Me.XrTableCell51.Text = "Consecutivo"
        Me.XrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell51.Weight = 0.20084818496021395R
        '
        'XrTableCell55
        '
        Me.XrTableCell55.Name = "XrTableCell55"
        Me.XrTableCell55.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell55.StylePriority.UsePadding = False
        Me.XrTableCell55.StylePriority.UseTextAlignment = False
        Me.XrTableCell55.Text = "Movimiento Contable"
        Me.XrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell55.Weight = 0.36353608346880559R
        '
        'XrTableCell52
        '
        Me.XrTableCell52.Name = "XrTableCell52"
        Me.XrTableCell52.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell52.StylePriority.UsePadding = False
        Me.XrTableCell52.StylePriority.UseTextAlignment = False
        Me.XrTableCell52.Text = "Libro Oficial"
        Me.XrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell52.Weight = 0.19690461781145441R
        '
        'XrTableCell56
        '
        Me.XrTableCell56.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell56.Name = "XrTableCell56"
        Me.XrTableCell56.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell56.StylePriority.UseBorders = False
        Me.XrTableCell56.StylePriority.UsePadding = False
        Me.XrTableCell56.StylePriority.UseTextAlignment = False
        Me.XrTableCell56.Text = "Tipo Comprobante"
        Me.XrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell56.Weight = 0.39376195625650057R
        '
        'XrTableCell59
        '
        Me.XrTableCell59.Name = "XrTableCell59"
        Me.XrTableCell59.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell59.StylePriority.UsePadding = False
        Me.XrTableCell59.StylePriority.UseTextAlignment = False
        Me.XrTableCell59.Text = "Fecha"
        Me.XrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell59.Weight = 0.15658113007314289R
        '
        'XrTableCell54
        '
        Me.XrTableCell54.Name = "XrTableCell54"
        Me.XrTableCell54.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell54.StylePriority.UsePadding = False
        Me.XrTableCell54.StylePriority.UseTextAlignment = False
        Me.XrTableCell54.Text = "Estado"
        Me.XrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell54.Weight = 0.17774414126861135R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell1.StylePriority.UsePadding = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.Text = "Cuenta"
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell1.Weight = 0.45181487241877971R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "Tercero"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell2.Weight = 0.37733894301771354R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "Centro Costo"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell3.Weight = 0.26569359893201372R
        '
        'XrTableCell60
        '
        Me.XrTableCell60.Name = "XrTableCell60"
        Me.XrTableCell60.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell60.StylePriority.UsePadding = False
        Me.XrTableCell60.StylePriority.UseTextAlignment = False
        Me.XrTableCell60.Text = "Débito"
        Me.XrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell60.Weight = 0.21130932300375964R
        '
        'XrTableCell58
        '
        Me.XrTableCell58.Name = "XrTableCell58"
        Me.XrTableCell58.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell58.StylePriority.UsePadding = False
        Me.XrTableCell58.StylePriority.UseTextAlignment = False
        Me.XrTableCell58.Text = "Crédito"
        Me.XrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell58.Weight = 0.20446781100214785R
        '
        'INDLblTitle
        '
        Me.INDLblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblTitle.LocationFloat = New DevExpress.Utils.PointFloat(0!, 128.3645!)
        Me.INDLblTitle.Name = "INDLblTitle"
        Me.INDLblTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblTitle.SizeF = New System.Drawing.SizeF(1049.0!, 23.0!)
        Me.INDLblTitle.StylePriority.UseFont = False
        Me.INDLblTitle.StylePriority.UseTextAlignment = False
        Me.INDLblTitle.Text = "LISTADO DE COMPROBANTES CONTABLES"
        Me.INDLblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblDates
        '
        Me.INDLblDates.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.INDLblDates.LocationFloat = New DevExpress.Utils.PointFloat(0!, 177.4063!)
        Me.INDLblDates.Name = "INDLblDates"
        Me.INDLblDates.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblDates.SizeF = New System.Drawing.SizeF(1049.0!, 23.0!)
        Me.INDLblDates.StylePriority.UseFont = False
        Me.INDLblDates.StylePriority.UseTextAlignment = False
        Me.INDLblDates.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(949.0!, 36.05213!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0.000228564!, 36.05213!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0003!, 36.05213!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(848.9997!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0003!, 61.05213!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(848.9997!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
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
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(949.0!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo1.TextFormatString = "Página {0}/{1}"
        '
        'XrTable4
        '
        Me.XrTable4.Font = New System.Drawing.Font("Times New Roman", 9.0!)
        Me.XrTable4.LocationFloat = New DevExpress.Utils.PointFloat(0.0003814697!, 0!)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow5})
        Me.XrTable4.SizeF = New System.Drawing.SizeF(1049.0!, 20.0!)
        Me.XrTable4.StylePriority.UseFont = False
        '
        'XrTableRow5
        '
        Me.XrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell26, Me.XrTableCell27, Me.XrTableCell28, Me.XrTableCell29, Me.XrTableCell30, Me.XrTableCell31, Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell6, Me.XrTableCell32, Me.XrTableCell33})
        Me.XrTableRow5.Name = "XrTableRow5"
        Me.XrTableRow5.Weight = 0.8R
        '
        'XrTableCell26
        '
        Me.XrTableCell26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdAccounting.Consecutive")})
        Me.XrTableCell26.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell26.Name = "XrTableCell26"
        Me.XrTableCell26.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell26.StylePriority.UseFont = False
        Me.XrTableCell26.StylePriority.UsePadding = False
        Me.XrTableCell26.StylePriority.UseTextAlignment = False
        Me.XrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell26.Weight = 0.19675427583319124R
        '
        'XrTableCell27
        '
        Me.XrTableCell27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "INDNameMoviment")})
        Me.XrTableCell27.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell27.Name = "XrTableCell27"
        Me.XrTableCell27.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell27.StylePriority.UseFont = False
        Me.XrTableCell27.StylePriority.UsePadding = False
        Me.XrTableCell27.StylePriority.UseTextAlignment = False
        Me.XrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell27.Weight = 0.35409027681194314R
        '
        'XrTableCell28
        '
        Me.XrTableCell28.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdAccounting.LegalBookId.CodeName")})
        Me.XrTableCell28.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell28.Name = "XrTableCell28"
        Me.XrTableCell28.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell28.StylePriority.UseFont = False
        Me.XrTableCell28.StylePriority.UsePadding = False
        Me.XrTableCell28.StylePriority.UseTextAlignment = False
        Me.XrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell28.Weight = 0.19289214219724662R
        '
        'XrTableCell29
        '
        Me.XrTableCell29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdAccounting.IdJournalVoucher.CodeName")})
        Me.XrTableCell29.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell29.Name = "XrTableCell29"
        Me.XrTableCell29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCell29.StylePriority.UseFont = False
        Me.XrTableCell29.StylePriority.UsePadding = False
        Me.XrTableCell29.StylePriority.UseTextAlignment = False
        Me.XrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell29.Weight = 0.3857379640406392R
        '
        'XrTableCell30
        '
        Me.XrTableCell30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdAccounting.VoucherDate", "{0:dd/MM/yyyy}")})
        Me.XrTableCell30.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.StylePriority.UseFont = False
        Me.XrTableCell30.StylePriority.UseTextAlignment = False
        Me.XrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell30.Weight = 0.15339019479693192R
        '
        'XrTableCell31
        '
        Me.XrTableCell31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdAccounting.Status")})
        Me.XrTableCell31.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell31.Name = "XrTableCell31"
        Me.XrTableCell31.StylePriority.UseFont = False
        Me.XrTableCell31.StylePriority.UseTextAlignment = False
        Me.XrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell31.Weight = 0.17412228225323839R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdMainAccount.NumberName")})
        Me.XrTableCell4.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell4.Weight = 0.44260769628720042R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdThirdParty.NitName")})
        Me.XrTableCell5.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell5.Weight = 0.36964971751536663R
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "IdCostCenter.CodeName")})
        Me.XrTableCell6.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseFont = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell6.Weight = 0.26027872688534232R
        '
        'XrTableCell32
        '
        Me.XrTableCell32.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DebitValue")})
        Me.XrTableCell32.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell32.Name = "XrTableCell32"
        Me.XrTableCell32.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell32.StylePriority.UseFont = False
        Me.XrTableCell32.StylePriority.UsePadding = False
        Me.XrTableCell32.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:c2}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell32.Summary = XrSummary1
        Me.XrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell32.Weight = 0.20802282754886635R
        '
        'XrTableCell33
        '
        Me.XrTableCell33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "CreditValue")})
        Me.XrTableCell33.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.XrTableCell33.Name = "XrTableCell33"
        Me.XrTableCell33.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell33.StylePriority.UseFont = False
        Me.XrTableCell33.StylePriority.UsePadding = False
        Me.XrTableCell33.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:c2}"
        XrSummary2.IgnoreNullValues = True
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell33.Summary = XrSummary2
        Me.XrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell33.Weight = 0.20132028803867537R
        '
        'INDNameMoviment
        '
        Me.INDNameMoviment.Expression = "[IdAccounting.EntityCode] + ' - ' + [IdAccounting.EntityName]"
        Me.INDNameMoviment.Name = "INDNameMoviment"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblUser})
        Me.PageFooter.HeightF = 20.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'INDTblUser
        '
        Me.INDTblUser.LocationFloat = New DevExpress.Utils.PointFloat(686.7072!, 0!)
        Me.INDTblUser.Name = "INDTblUser"
        Me.INDTblUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDTblUser.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow24})
        Me.INDTblUser.SizeF = New System.Drawing.SizeF(362.2928!, 20.0!)
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
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable4})
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("IdAccounting.IdJournalVoucher.Code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("INDGroupBy", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader2.HeightF = 20.0!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'INDGroupBy
        '
        Me.INDGroupBy.Expression = "[IdAccounting.Consecutive] + [IdAccounting.IdJournalVoucher.Code]"
        Me.INDGroupBy.Name = "INDGroupBy"
        '
        'INDLblBook
        '
        Me.INDLblBook.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.INDLblBook.LocationFloat = New DevExpress.Utils.PointFloat(0.0004053116!, 151.3645!)
        Me.INDLblBook.Name = "INDLblBook"
        Me.INDLblBook.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblBook.SizeF = New System.Drawing.SizeF(1049.0!, 26.04173!)
        Me.INDLblBook.StylePriority.UseFont = False
        Me.INDLblBook.StylePriority.UseTextAlignment = False
        Me.INDLblBook.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.AccountingRepository.JournalVoucherDetailsXpo)
        '
        'rptListAccountingVoucher
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter, Me.GroupHeader2})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDNameMoviment, Me.INDGroupBy})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource1})
        Me.DataSource = Me.BindingSource1
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(25, 26, 25, 25)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "19.1"
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents INDLblTitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblDates As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell51 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell55 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell52 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell56 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell59 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell54 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell60 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell58 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell31 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell32 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell33 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDNameMoviment As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents INDTblUser As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow24 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDLblUserPrint As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents INDGroupBy As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblBook As DevExpress.XtraReports.UI.XRLabel
End Class
