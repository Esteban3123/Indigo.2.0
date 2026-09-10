<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptBasicBilling
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
        Dim QrCodeGenerator1 As DevExpress.XtraPrinting.BarCode.QRCodeGenerator = New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary4 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary5 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary6 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary7 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptBasicBilling))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.INDlblCUFE = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDtxtShippingDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblShippingDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDtxtInvoice = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDtxtCUFE = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDtxtInvoiceDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblInvoice = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrBarCode2 = New DevExpress.XtraReports.UI.XRBarCode()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow6 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow11 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell32 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow14 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell39 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell41 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow15 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell48 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell49 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow12 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell38 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell75 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell43 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell36 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell44 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrTable8 = New DevExpress.XtraReports.UI.XRTable()
        Me.INDtrElectronicDocumentStatus = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDlblElectronicDocumentStatus = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDtxtElectronicDocumentStatus = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow16 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell35 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDUserCreate = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDUserImp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable9 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable6 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell24 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable5 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDCllValueTotalInvoiceLetters = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDCfFooterPage = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDPrResolutionNumber = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDPrResolutionDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDPrResolutionInvoicePrefix = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDPrResolutionInitialInvoice = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDPrResolutionFinalInvoice = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDIdInvoiceSubreport = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDCfCodeName = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfTotalValue = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfCode = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfName = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfSaleModality = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDPrResolutionInitialDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDPrResolutionFinalDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDCfTerm = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDfromSaleInvoiceAll = New DevExpress.XtraReports.Parameters.Parameter()
                CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.XrTable1.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(799.9998!, 20.0!)
        Me.XrTable1.StylePriority.UseBackColor = False
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell11, Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell3, Me.XrTableCell6})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 0.8R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[INDCfCode]")})
        Me.XrTableCell1.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell1.StylePriority.UseBackColor = False
        Me.XrTableCell1.StylePriority.UseBorders = False
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.StylePriority.UsePadding = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell1.Weight = 0.38459792872703291R
        '
        'XrTableCell11
        '
        Me.XrTableCell11.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell11.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[INDCfName]")})
        Me.XrTableCell11.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell11.StylePriority.UseBackColor = False
        Me.XrTableCell11.StylePriority.UseBorders = False
        Me.XrTableCell11.StylePriority.UseFont = False
        Me.XrTableCell11.StylePriority.UsePadding = False
        Me.XrTableCell11.StylePriority.UseTextAlignment = False
        Me.XrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell11.Weight = 1.3076329202702079R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "IIF([Quantity] > 0, [Quantity], '')")})
        Me.XrTableCell4.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell4.StylePriority.UseBackColor = False
        Me.XrTableCell4.StylePriority.UseBorders = False
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UsePadding = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell4.TextFormatString = "{0:n0}"
        Me.XrTableCell4.Weight = 0.30767821133430034R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "IIf([Price]>0,[Price],'')")})
        Me.XrTableCell5.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell5.StylePriority.UseBackColor = False
        Me.XrTableCell5.StylePriority.UseBorders = False
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UsePadding = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell5.TextFormatString = "{0:c2}"
        Me.XrTableCell5.Weight = 0.38459770989732556R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([PercentageIVA] > 0, [PercentageIVA], '')")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell3.StylePriority.UseBackColor = False
        Me.XrTableCell3.StylePriority.UseBorders = False
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell3.TextFormatString = "{0:n2}"
        Me.XrTableCell3.Weight = 0.23075922937027604R
        '
        'XrTableCell6
        '
        Me.XrTableCell6.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell6.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([Value] > 0, [Value], '')")})
        Me.XrTableCell6.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell6.StylePriority.UseBackColor = False
        Me.XrTableCell6.StylePriority.UseBorders = False
        Me.XrTableCell6.StylePriority.UseFont = False
        Me.XrTableCell6.StylePriority.UsePadding = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell6.TextFormatString = "{0:c2}"
        Me.XrTableCell6.Weight = 0.46151670732762712R
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
        Me.BottomMargin.HeightF = 27.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDlblCUFE, Me.INDtxtShippingDate, Me.INDlblShippingDate, Me.XrLabel5, Me.INDtxtInvoice, Me.INDtxtCUFE, Me.XrPictureBox2, Me.XrPictureBox1, Me.XrPageInfo3, Me.XrLabel13, Me.XrPageInfo1, Me.INDLblCompany, Me.INDLblNitCompany, Me.INDtxtInvoiceDate, Me.INDlblInvoice, Me.XrLabel3})
        Me.PageHeader.HeightF = 287.9167!
        Me.PageHeader.Name = "PageHeader"
        '
        'INDlblCUFE
        '
        Me.INDlblCUFE.BackColor = System.Drawing.Color.Gainsboro
        Me.INDlblCUFE.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblCUFE.LocationFloat = New DevExpress.Utils.PointFloat(440.0!, 239.5833!)
        Me.INDlblCUFE.Name = "INDlblCUFE"
        Me.INDlblCUFE.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDlblCUFE.SizeF = New System.Drawing.SizeF(360.0006!, 20.0!)
        Me.INDlblCUFE.StylePriority.UseBackColor = False
        Me.INDlblCUFE.StylePriority.UseFont = False
        Me.INDlblCUFE.StylePriority.UsePadding = False
        Me.INDlblCUFE.StylePriority.UseTextAlignment = False
        Me.INDlblCUFE.Text = "CODIGO CUFE"
        Me.INDlblCUFE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDtxtShippingDate
        '
        Me.INDtxtShippingDate.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDtxtShippingDate.LocationFloat = New DevExpress.Utils.PointFloat(0.9595633!, 259.5833!)
        Me.INDtxtShippingDate.Name = "INDtxtShippingDate"
        Me.INDtxtShippingDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDtxtShippingDate.SizeF = New System.Drawing.SizeF(249.5834!, 20.0!)
        Me.INDtxtShippingDate.StylePriority.UseFont = False
        Me.INDtxtShippingDate.StylePriority.UsePadding = False
        Me.INDtxtShippingDate.StylePriority.UseTextAlignment = False
        Me.INDtxtShippingDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDlblShippingDate
        '
        Me.INDlblShippingDate.BackColor = System.Drawing.Color.Gainsboro
        Me.INDlblShippingDate.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblShippingDate.LocationFloat = New DevExpress.Utils.PointFloat(0!, 239.5833!)
        Me.INDlblShippingDate.Name = "INDlblShippingDate"
        Me.INDlblShippingDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDlblShippingDate.SizeF = New System.Drawing.SizeF(250.5429!, 20.0!)
        Me.INDlblShippingDate.StylePriority.UseBackColor = False
        Me.INDlblShippingDate.StylePriority.UseFont = False
        Me.INDlblShippingDate.StylePriority.UsePadding = False
        Me.INDlblShippingDate.StylePriority.UseTextAlignment = False
        Me.INDlblShippingDate.Text = "FECHA Y HORA DE VALIDACIÓN"
        Me.INDlblShippingDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.BackColor = System.Drawing.Color.Gainsboro
        Me.XrLabel5.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 199.5833!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(250.5429!, 20.0!)
        Me.XrLabel5.StylePriority.UseBackColor = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UsePadding = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "FECHA Y HORA DE EMISION"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDtxtInvoice
        '
        Me.INDtxtInvoice.BackColor = System.Drawing.Color.Gainsboro
        Me.INDtxtInvoice.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDtxtInvoice.LocationFloat = New DevExpress.Utils.PointFloat(250.5429!, 128.9583!)
        Me.INDtxtInvoice.Name = "INDtxtInvoice"
        Me.INDtxtInvoice.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDtxtInvoice.SizeF = New System.Drawing.SizeF(133.8321!, 20.0!)
        Me.INDtxtInvoice.StylePriority.UseBackColor = False
        Me.INDtxtInvoice.StylePriority.UseFont = False
        Me.INDtxtInvoice.StylePriority.UsePadding = False
        Me.INDtxtInvoice.StylePriority.UseTextAlignment = False
        Me.INDtxtInvoice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDtxtCUFE
        '
        Me.INDtxtCUFE.Font = New System.Drawing.Font("Arial", 6.0!)
        Me.INDtxtCUFE.LocationFloat = New DevExpress.Utils.PointFloat(440.0!, 259.5833!)
        Me.INDtxtCUFE.Name = "INDtxtCUFE"
        Me.INDtxtCUFE.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDtxtCUFE.SizeF = New System.Drawing.SizeF(360.0006!, 20.0!)
        Me.INDtxtCUFE.StylePriority.UseFont = False
        Me.INDtxtCUFE.StylePriority.UsePadding = False
        Me.INDtxtCUFE.StylePriority.UseTextAlignment = False
        Me.INDtxtCUFE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(699.9998!, 33.52086!)
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
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(85.45824!, 0!)
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
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(699.9998!, 0!)
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
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(99.99987!, 33.52086!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(599.9999!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(99.99987!, 58.52089!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(599.9998!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDtxtInvoiceDate
        '
        Me.INDtxtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDtxtInvoiceDate.LocationFloat = New DevExpress.Utils.PointFloat(0.9595633!, 219.5833!)
        Me.INDtxtInvoiceDate.Name = "INDtxtInvoiceDate"
        Me.INDtxtInvoiceDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDtxtInvoiceDate.SizeF = New System.Drawing.SizeF(249.5834!, 20.0!)
        Me.INDtxtInvoiceDate.StylePriority.UseFont = False
        Me.INDtxtInvoiceDate.StylePriority.UsePadding = False
        Me.INDtxtInvoiceDate.StylePriority.UseTextAlignment = False
        Me.INDtxtInvoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDlblInvoice
        '
        Me.INDlblInvoice.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblInvoice.LocationFloat = New DevExpress.Utils.PointFloat(0.9595633!, 128.9583!)
        Me.INDlblInvoice.Name = "INDlblInvoice"
        Me.INDlblInvoice.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDlblInvoice.SizeF = New System.Drawing.SizeF(249.5833!, 20.0!)
        Me.INDlblInvoice.StylePriority.UseFont = False
        Me.INDlblInvoice.StylePriority.UsePadding = False
        Me.INDlblInvoice.StylePriority.UseTextAlignment = False
        Me.INDlblInvoice.Text = "FACTURA DE VENTA N°"
        Me.INDlblInvoice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[INDCfFooterPage]")})
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 148.9583!)
        Me.XrLabel3.Multiline = True
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(250.5429!, 50.62498!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrBarCode2
        '
        Me.XrBarCode2.AutoModule = True
        Me.XrBarCode2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrBarCode2.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.XrBarCode2.LocationFloat = New DevExpress.Utils.PointFloat(520.957!, 0!)
        Me.XrBarCode2.Module = 1.0!
        Me.XrBarCode2.Name = "XrBarCode2"
        Me.XrBarCode2.Padding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100.0!)
        Me.XrBarCode2.ShowText = False
        Me.XrBarCode2.SizeF = New System.Drawing.SizeF(279.0424!, 120.0807!)
        Me.XrBarCode2.StylePriority.UseBorders = False
        Me.XrBarCode2.StylePriority.UseFont = False
        Me.XrBarCode2.StylePriority.UsePadding = False
        Me.XrBarCode2.StylePriority.UseTextAlignment = False
        Me.XrBarCode2.Symbology = QrCodeGenerator1
        Me.XrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.XrTable4, Me.XrBarCode2})
        Me.GroupHeader1.HeightF = 183.622!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrTable2
        '
        Me.XrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable2.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0.958373!, 0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow4, Me.XrTableRow6, Me.XrTableRow5, Me.XrTableRow11, Me.XrTableRow14, Me.XrTableRow15, Me.XrTableRow12, Me.XrTableRow8})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(519.9987!, 160.0!)
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell7, Me.XrTableCell10})
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 0.8R
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell7.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell7.StylePriority.UseBorders = False
        Me.XrTableCell7.StylePriority.UseFont = False
        Me.XrTableCell7.StylePriority.UsePadding = False
        Me.XrTableCell7.StylePriority.UseTextAlignment = False
        Me.XrTableCell7.Text = "CLIENTE"
        Me.XrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell7.Weight = 0.809463308893235R
        '
        'XrTableCell10
        '
        Me.XrTableCell10.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell10.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.CustomerId.Name]")})
        Me.XrTableCell10.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell10.StylePriority.UseBorders = False
        Me.XrTableCell10.StylePriority.UseFont = False
        Me.XrTableCell10.StylePriority.UsePadding = False
        Me.XrTableCell10.StylePriority.UseTextAlignment = False
        Me.XrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell10.Weight = 2.2517733573948666R
        '
        'XrTableRow6
        '
        Me.XrTableRow6.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell2, Me.XrTableCell19})
        Me.XrTableRow6.Name = "XrTableRow6"
        Me.XrTableRow6.Weight = 0.8R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell2.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell2.StylePriority.UseBorders = False
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "NIT"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell2.Weight = 0.809463308893235R
        '
        'XrTableCell19
        '
        Me.XrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.CustomerId.Nit]")})
        Me.XrTableCell19.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell19.StylePriority.UseBorders = False
        Me.XrTableCell19.StylePriority.UseFont = False
        Me.XrTableCell19.StylePriority.UsePadding = False
        Me.XrTableCell19.StylePriority.UseTextAlignment = False
        Me.XrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell19.Weight = 2.2517733573948666R
        '
        'XrTableRow5
        '
        Me.XrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell13, Me.XrTableCell14})
        Me.XrTableRow5.Name = "XrTableRow5"
        Me.XrTableRow5.Weight = 0.79999999999999993R
        '
        'XrTableCell13
        '
        Me.XrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell13.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell13.StylePriority.UseBorders = False
        Me.XrTableCell13.StylePriority.UseFont = False
        Me.XrTableCell13.StylePriority.UsePadding = False
        Me.XrTableCell13.StylePriority.UseTextAlignment = False
        Me.XrTableCell13.Text = "DIRECCION"
        Me.XrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell13.Weight = 0.80946322410616078R
        '
        'XrTableCell14
        '
        Me.XrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.AddressId.Addresss]")})
        Me.XrTableCell14.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell14.StylePriority.UseBorders = False
        Me.XrTableCell14.StylePriority.UseFont = False
        Me.XrTableCell14.StylePriority.UsePadding = False
        Me.XrTableCell14.StylePriority.UseTextAlignment = False
        Me.XrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell14.Weight = 2.2517732190226418R
        '
        'XrTableRow11
        '
        Me.XrTableRow11.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell31, Me.XrTableCell32})
        Me.XrTableRow11.Name = "XrTableRow11"
        Me.XrTableRow11.Weight = 0.79999999999999993R
        '
        'XrTableCell31
        '
        Me.XrTableCell31.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell31.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell31.Name = "XrTableCell31"
        Me.XrTableCell31.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell31.StylePriority.UseBorders = False
        Me.XrTableCell31.StylePriority.UseFont = False
        Me.XrTableCell31.StylePriority.UsePadding = False
        Me.XrTableCell31.StylePriority.UseTextAlignment = False
        Me.XrTableCell31.Text = "TELEFONO"
        Me.XrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell31.Weight = 0.80946322410616078R
        '
        'XrTableCell32
        '
        Me.XrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell32.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.CustomerId.ThirdPartyId.PersonId.Common_Phones.Phone]")})
        Me.XrTableCell32.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell32.Name = "XrTableCell32"
        Me.XrTableCell32.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell32.StylePriority.UseBorders = False
        Me.XrTableCell32.StylePriority.UseFont = False
        Me.XrTableCell32.StylePriority.UsePadding = False
        Me.XrTableCell32.StylePriority.UseTextAlignment = False
        Me.XrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell32.Weight = 2.2517732190226418R
        '
        'XrTableRow14
        '
        Me.XrTableRow14.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell39, Me.XrTableCell41})
        Me.XrTableRow14.Name = "XrTableRow14"
        Me.XrTableRow14.Weight = 0.79999999999999993R
        '
        'XrTableCell39
        '
        Me.XrTableCell39.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell39.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell39.Name = "XrTableCell39"
        Me.XrTableCell39.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell39.StylePriority.UseBorders = False
        Me.XrTableCell39.StylePriority.UseFont = False
        Me.XrTableCell39.StylePriority.UsePadding = False
        Me.XrTableCell39.StylePriority.UseTextAlignment = False
        Me.XrTableCell39.Text = "CIUDAD"
        Me.XrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell39.Weight = 0.80946322410616078R
        '
        'XrTableCell41
        '
        Me.XrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell41.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.AddressId.CityDepartment]")})
        Me.XrTableCell41.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell41.Name = "XrTableCell41"
        Me.XrTableCell41.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell41.StylePriority.UseBorders = False
        Me.XrTableCell41.StylePriority.UseFont = False
        Me.XrTableCell41.StylePriority.UsePadding = False
        Me.XrTableCell41.StylePriority.UseTextAlignment = False
        Me.XrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell41.Weight = 2.2517732190226418R
        '
        'XrTableRow15
        '
        Me.XrTableRow15.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell48, Me.XrTableCell9, Me.XrTableCell12, Me.XrTableCell49})
        Me.XrTableRow15.Name = "XrTableRow15"
        Me.XrTableRow15.Weight = 0.79999999999999993R
        '
        'XrTableCell48
        '
        Me.XrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell48.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell48.Name = "XrTableCell48"
        Me.XrTableCell48.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell48.StylePriority.UseBorders = False
        Me.XrTableCell48.StylePriority.UseFont = False
        Me.XrTableCell48.StylePriority.UsePadding = False
        Me.XrTableCell48.StylePriority.UseTextAlignment = False
        Me.XrTableCell48.Text = "METODO PAGO"
        Me.XrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell48.Weight = 0.809462595305903R
        '
        'XrTableCell9
        '
        Me.XrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableCell9.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[INDCfSaleModality]")})
        Me.XrTableCell9.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell9.StylePriority.UseBorders = False
        Me.XrTableCell9.StylePriority.UseFont = False
        Me.XrTableCell9.StylePriority.UsePadding = False
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell9.Weight = 1.3282641759085845R
        '
        'XrTableCell12
        '
        Me.XrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableCell12.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell12.StylePriority.UseBorders = False
        Me.XrTableCell12.StylePriority.UseFont = False
        Me.XrTableCell12.StylePriority.UsePadding = False
        Me.XrTableCell12.StylePriority.UseTextAlignment = False
        Me.XrTableCell12.Text = "PLAZO"
        Me.XrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell12.Weight = 0.44691474564459732R
        '
        'XrTableCell49
        '
        Me.XrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell49.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[INDCfTerm]")})
        Me.XrTableCell49.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell49.Name = "XrTableCell49"
        Me.XrTableCell49.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell49.StylePriority.UseBorders = False
        Me.XrTableCell49.StylePriority.UseFont = False
        Me.XrTableCell49.StylePriority.UsePadding = False
        Me.XrTableCell49.StylePriority.UseTextAlignment = False
        Me.XrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell49.Weight = 0.47659492626971806R
        '
        'XrTableRow12
        '
        Me.XrTableRow12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTableRow12.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell29, Me.XrTableCell30})
        Me.XrTableRow12.Name = "XrTableRow12"
        Me.XrTableRow12.StylePriority.UseBorders = False
        Me.XrTableRow12.Weight = 0.79999999999999993R
        '
        'XrTableCell29
        '
        Me.XrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrTableCell29.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell29.Multiline = True
        Me.XrTableCell29.Name = "XrTableCell29"
        Me.XrTableCell29.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell29.StylePriority.UseBorders = False
        Me.XrTableCell29.StylePriority.UseFont = False
        Me.XrTableCell29.StylePriority.UsePadding = False
        Me.XrTableCell29.StylePriority.UseTextAlignment = False
        Me.XrTableCell29.Text = "ENTIDAD ADMINISTRADORA"
        Me.XrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell29.Weight = 1.0400369126915545R
        '
        'XrTableCell30
        '
        Me.XrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrTableCell30.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell30.Multiline = True
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell30.StylePriority.UseBorders = False
        Me.XrTableCell30.StylePriority.UseFont = False
        Me.XrTableCell30.StylePriority.UsePadding = False
        Me.XrTableCell30.StylePriority.UseTextAlignment = False
        Me.XrTableCell30.Text = "XrTableCell30"
        Me.XrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell30.Weight = 2.0211995304372481R
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell17, Me.XrTableCell18})
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 0.79999999999999993R
        '
        'XrTableCell17
        '
        Me.XrTableCell17.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell17.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell17.Multiline = True
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell17.StylePriority.UseBorders = False
        Me.XrTableCell17.StylePriority.UseFont = False
        Me.XrTableCell17.StylePriority.UsePadding = False
        Me.XrTableCell17.StylePriority.UseTextAlignment = False
        Me.XrTableCell17.Text = "CONDICIÓN DE VENTA"
        Me.XrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell17.Weight = 1.0400369126915545R
        '
        'XrTableCell18
        '
        Me.XrTableCell18.Borders = CType((DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId].[ConditionSalesId].[CodeName]")})
        Me.XrTableCell18.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell18.Multiline = True
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell18.StylePriority.UseBorders = False
        Me.XrTableCell18.StylePriority.UseFont = False
        Me.XrTableCell18.StylePriority.UsePadding = False
        Me.XrTableCell18.StylePriority.UseTextAlignment = False
        Me.XrTableCell18.Text = "XrTableCell18"
        Me.XrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell18.Weight = 2.0211995304372481R
        '
        'XrTable4
        '
        Me.XrTable4.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable4.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable4.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 163.622!)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow9})
        Me.XrTable4.SizeF = New System.Drawing.SizeF(800.0!, 20.0!)
        Me.XrTable4.StylePriority.UseBackColor = False
        Me.XrTable4.StylePriority.UseBorders = False
        Me.XrTable4.StylePriority.UseFont = False
        Me.XrTable4.StylePriority.UseTextAlignment = False
        Me.XrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell38, Me.XrTableCell8, Me.XrTableCell75, Me.XrTableCell43, Me.XrTableCell36, Me.XrTableCell44})
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 0.8R
        '
        'XrTableCell38
        '
        Me.XrTableCell38.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell38.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell38.Name = "XrTableCell38"
        Me.XrTableCell38.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell38.StylePriority.UseBorders = False
        Me.XrTableCell38.StylePriority.UseFont = False
        Me.XrTableCell38.StylePriority.UsePadding = False
        Me.XrTableCell38.StylePriority.UseTextAlignment = False
        Me.XrTableCell38.Text = "CODIGO"
        Me.XrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell38.Weight = 0.38459814432016742R
        '
        'XrTableCell8
        '
        Me.XrTableCell8.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell8.StylePriority.UseBorders = False
        Me.XrTableCell8.StylePriority.UseFont = False
        Me.XrTableCell8.StylePriority.UsePadding = False
        Me.XrTableCell8.StylePriority.UseTextAlignment = False
        Me.XrTableCell8.Text = "DESCRIPCION"
        Me.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell8.Weight = 1.3076336114772871R
        '
        'XrTableCell75
        '
        Me.XrTableCell75.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell75.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell75.Name = "XrTableCell75"
        Me.XrTableCell75.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell75.StylePriority.UseBorders = False
        Me.XrTableCell75.StylePriority.UseFont = False
        Me.XrTableCell75.StylePriority.UsePadding = False
        Me.XrTableCell75.StylePriority.UseTextAlignment = False
        Me.XrTableCell75.Text = "CANTIDAD"
        Me.XrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell75.Weight = 0.30767850476519637R
        '
        'XrTableCell43
        '
        Me.XrTableCell43.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell43.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell43.Name = "XrTableCell43"
        Me.XrTableCell43.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell43.StylePriority.UseBorders = False
        Me.XrTableCell43.StylePriority.UseFont = False
        Me.XrTableCell43.StylePriority.UsePadding = False
        Me.XrTableCell43.StylePriority.UseTextAlignment = False
        Me.XrTableCell43.Text = "VR. UNITARIO"
        Me.XrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell43.Weight = 0.38459813571528451R
        '
        'XrTableCell36
        '
        Me.XrTableCell36.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell36.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell36.Name = "XrTableCell36"
        Me.XrTableCell36.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell36.StylePriority.UseBorders = False
        Me.XrTableCell36.StylePriority.UseFont = False
        Me.XrTableCell36.StylePriority.UsePadding = False
        Me.XrTableCell36.StylePriority.UseTextAlignment = False
        Me.XrTableCell36.Text = "IVA"
        Me.XrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell36.Weight = 0.23075887351741603R
        '
        'XrTableCell44
        '
        Me.XrTableCell44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell44.Name = "XrTableCell44"
        Me.XrTableCell44.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell44.StylePriority.UseFont = False
        Me.XrTableCell44.StylePriority.UsePadding = False
        Me.XrTableCell44.StylePriority.UseTextAlignment = False
        Me.XrTableCell44.Text = "TOTAL"
        Me.XrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell44.Weight = 0.46151779455134495R
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable8})
        Me.PageFooter.HeightF = 50.62504!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrTable8
        '
        Me.XrTable8.BackColor = System.Drawing.Color.Transparent
        Me.XrTable8.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 10.0!)
        Me.XrTable8.Name = "XrTable8"
        Me.XrTable8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.INDtrElectronicDocumentStatus, Me.XrTableRow16})
        Me.XrTable8.SizeF = New System.Drawing.SizeF(800.0001!, 40.0!)
        Me.XrTable8.StylePriority.UseBackColor = False
        Me.XrTable8.StylePriority.UseBorders = False
        '
        'INDtrElectronicDocumentStatus
        '
        Me.INDtrElectronicDocumentStatus.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDlblElectronicDocumentStatus, Me.INDtxtElectronicDocumentStatus})
        Me.INDtrElectronicDocumentStatus.Name = "INDtrElectronicDocumentStatus"
        Me.INDtrElectronicDocumentStatus.Weight = 0.8R
        '
        'INDlblElectronicDocumentStatus
        '
        Me.INDlblElectronicDocumentStatus.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDlblElectronicDocumentStatus.Name = "INDlblElectronicDocumentStatus"
        Me.INDlblElectronicDocumentStatus.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDlblElectronicDocumentStatus.StylePriority.UseFont = False
        Me.INDlblElectronicDocumentStatus.StylePriority.UsePadding = False
        Me.INDlblElectronicDocumentStatus.StylePriority.UseTextAlignment = False
        Me.INDlblElectronicDocumentStatus.Text = "Estado F.E.:"
        Me.INDlblElectronicDocumentStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.INDlblElectronicDocumentStatus.Weight = 0.37500031270273221R
        '
        'INDtxtElectronicDocumentStatus
        '
        Me.INDtxtElectronicDocumentStatus.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDtxtElectronicDocumentStatus.Name = "INDtxtElectronicDocumentStatus"
        Me.INDtxtElectronicDocumentStatus.StylePriority.UseFont = False
        Me.INDtxtElectronicDocumentStatus.StylePriority.UseTextAlignment = False
        Me.INDtxtElectronicDocumentStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.INDtxtElectronicDocumentStatus.Weight = 2.6249996872972678R
        '
        'XrTableRow16
        '
        Me.XrTableRow16.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell35, Me.INDUserCreate, Me.INDUserImp})
        Me.XrTableRow16.Name = "XrTableRow16"
        Me.XrTableRow16.Weight = 0.8R
        '
        'XrTableCell35
        '
        Me.XrTableCell35.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrTableCell35.Name = "XrTableCell35"
        Me.XrTableCell35.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell35.StylePriority.UseFont = False
        Me.XrTableCell35.StylePriority.UsePadding = False
        Me.XrTableCell35.StylePriority.UseTextAlignment = False
        Me.XrTableCell35.Text = "Usuario Creación:"
        Me.XrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell35.Weight = 0.37500031270273221R
        '
        'INDUserCreate
        '
        Me.INDUserCreate.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDUserCreate.Name = "INDUserCreate"
        Me.INDUserCreate.StylePriority.UseFont = False
        Me.INDUserCreate.StylePriority.UseTextAlignment = False
        Me.INDUserCreate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.INDUserCreate.Weight = 1.2378909986849187R
        '
        'INDUserImp
        '
        Me.INDUserImp.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDUserImp.Name = "INDUserImp"
        Me.INDUserImp.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDUserImp.StylePriority.UseFont = False
        Me.INDUserImp.StylePriority.UsePadding = False
        Me.INDUserImp.StylePriority.UseTextAlignment = False
        Me.INDUserImp.Text = "INDUserImp"
        Me.INDUserImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDUserImp.Weight = 1.3871086886123489R
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel23, Me.XrLabel24, Me.XrLabel20, Me.XrLabel1, Me.XrTable9, Me.XrTable6, Me.XrTable5, Me.XrTable3, Me.XrLabel11, Me.XrLabel12, Me.XrLabel15, Me.XrLabel16, Me.XrLabel18, Me.XrLabel19, Me.INDCllValueTotalInvoiceLetters, Me.XrLabel21, Me.XrLabel22})
        Me.GroupFooter1.HeightF = 230.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel23
        '
        Me.XrLabel23.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel23.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(0.9595633!, 160.0!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(519.9975!, 70.0!)
        Me.XrLabel23.StylePriority.UseBackColor = False
        Me.XrLabel23.StylePriority.UseBorders = False
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UsePadding = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel24
        '
        Me.XrLabel24.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel24.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(520.9571!, 160.0!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(279.0424!, 69.99997!)
        Me.XrLabel24.StylePriority.UseBackColor = False
        Me.XrLabel24.StylePriority.UseBorders = False
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UsePadding = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel20
        '
        Me.XrLabel20.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel20.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel20.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(520.957!, 140.0!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(279.0424!, 20.0!)
        Me.XrLabel20.StylePriority.UseBackColor = False
        Me.XrLabel20.StylePriority.UseBorders = False
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UsePadding = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "RECIBI CONFORME"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel1.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0.9595325!, 140.0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(519.9975!, 20.0!)
        Me.XrLabel1.StylePriority.UseBackColor = False
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UsePadding = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTable9
        '
        Me.XrTable9.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable9.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable9.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable9.LocationFloat = New DevExpress.Utils.PointFloat(0.958374!, 0!)
        Me.XrTable9.Name = "XrTable9"
        Me.XrTable9.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow10})
        Me.XrTable9.SizeF = New System.Drawing.SizeF(799.0421!, 20.0!)
        Me.XrTable9.StylePriority.UseBackColor = False
        Me.XrTable9.StylePriority.UseBorders = False
        Me.XrTable9.StylePriority.UseFont = False
        Me.XrTable9.StylePriority.UseTextAlignment = False
        Me.XrTable9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell20, Me.XrTableCell27, Me.XrTableCell28})
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.Weight = 0.8R
        '
        'XrTableCell20
        '
        Me.XrTableCell20.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell20.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 1, 0, 0, 100.0!)
        Me.XrTableCell20.StylePriority.UseBackColor = False
        Me.XrTableCell20.StylePriority.UseBorders = False
        Me.XrTableCell20.StylePriority.UseFont = False
        Me.XrTableCell20.StylePriority.UsePadding = False
        Me.XrTableCell20.StylePriority.UseTextAlignment = False
        Me.XrTableCell20.Text = "OBSERVACIONES"
        Me.XrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell20.Weight = 0.97556029762278462R
        '
        'XrTableCell27
        '
        Me.XrTableCell27.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell27.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell27.Name = "XrTableCell27"
        Me.XrTableCell27.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell27.StylePriority.UseBackColor = False
        Me.XrTableCell27.StylePriority.UseBorders = False
        Me.XrTableCell27.StylePriority.UseFont = False
        Me.XrTableCell27.StylePriority.UsePadding = False
        Me.XrTableCell27.StylePriority.UseTextAlignment = False
        Me.XrTableCell27.Text = "SUBTOTAL"
        Me.XrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell27.Weight = 0.3007266416313063R
        '
        'XrTableCell28
        '
        Me.XrTableCell28.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell28.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell28.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.Value]")})
        Me.XrTableCell28.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell28.Name = "XrTableCell28"
        Me.XrTableCell28.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell28.StylePriority.UseBackColor = False
        Me.XrTableCell28.StylePriority.UseBorders = False
        Me.XrTableCell28.StylePriority.UseFont = False
        Me.XrTableCell28.StylePriority.UsePadding = False
        Me.XrTableCell28.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:c2}"
        XrSummary1.IgnoreNullValues = True
        Me.XrTableCell28.Summary = XrSummary1
        Me.XrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell28.TextFormatString = "{0:c2}"
        Me.XrTableCell28.Weight = 0.22554591867464219R
        '
        'XrTable6
        '
        Me.XrTable6.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable6.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable6.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable6.LocationFloat = New DevExpress.Utils.PointFloat(520.0008!, 40.00001!)
        Me.XrTable6.Name = "XrTable6"
        Me.XrTable6.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow7})
        Me.XrTable6.SizeF = New System.Drawing.SizeF(279.9998!, 20.0!)
        Me.XrTable6.StylePriority.UseBackColor = False
        Me.XrTable6.StylePriority.UseBorders = False
        Me.XrTable6.StylePriority.UseFont = False
        Me.XrTable6.StylePriority.UseTextAlignment = False
        Me.XrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell23, Me.XrTableCell24})
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.Weight = 0.8R
        '
        'XrTableCell23
        '
        Me.XrTableCell23.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell23.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell23.Name = "XrTableCell23"
        Me.XrTableCell23.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell23.StylePriority.UseBackColor = False
        Me.XrTableCell23.StylePriority.UseBorders = False
        Me.XrTableCell23.StylePriority.UseFont = False
        Me.XrTableCell23.StylePriority.UsePadding = False
        Me.XrTableCell23.StylePriority.UseTextAlignment = False
        Me.XrTableCell23.Text = "IVA"
        Me.XrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell23.Weight = 0.30072641219507823R
        '
        'XrTableCell24
        '
        Me.XrTableCell24.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell24.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell24.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.ValueIVA]")})
        Me.XrTableCell24.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell24.Name = "XrTableCell24"
        Me.XrTableCell24.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell24.StylePriority.UseBackColor = False
        Me.XrTableCell24.StylePriority.UseBorders = False
        Me.XrTableCell24.StylePriority.UseFont = False
        Me.XrTableCell24.StylePriority.UsePadding = False
        Me.XrTableCell24.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:c2}"
        XrSummary2.IgnoreNullValues = True
        Me.XrTableCell24.Summary = XrSummary2
        Me.XrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell24.TextFormatString = "{0:c2}"
        Me.XrTableCell24.Weight = 0.22554482885255872R
        '
        'XrTable5
        '
        Me.XrTable5.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable5.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable5.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable5.LocationFloat = New DevExpress.Utils.PointFloat(520.0003!, 60.00002!)
        Me.XrTable5.Name = "XrTable5"
        Me.XrTable5.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable5.SizeF = New System.Drawing.SizeF(280.0003!, 20.0!)
        Me.XrTable5.StylePriority.UseBackColor = False
        Me.XrTable5.StylePriority.UseBorders = False
        Me.XrTable5.StylePriority.UseFont = False
        Me.XrTable5.StylePriority.UseTextAlignment = False
        Me.XrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell15, Me.XrTableCell16})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 0.8R
        '
        'XrTableCell15
        '
        Me.XrTableCell15.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell15.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell15.StylePriority.UseBackColor = False
        Me.XrTableCell15.StylePriority.UseBorders = False
        Me.XrTableCell15.StylePriority.UseFont = False
        Me.XrTableCell15.StylePriority.UsePadding = False
        Me.XrTableCell15.StylePriority.UseTextAlignment = False
        Me.XrTableCell15.Text = "RETEFUENTE"
        Me.XrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell15.Weight = 0.3007265842722493R
        '
        'XrTableCell16
        '
        Me.XrTableCell16.BackColor = System.Drawing.Color.Transparent
        Me.XrTableCell16.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.WithholdingTax]")})
        Me.XrTableCell16.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell16.StylePriority.UseBackColor = False
        Me.XrTableCell16.StylePriority.UseBorders = False
        Me.XrTableCell16.StylePriority.UseFont = False
        Me.XrTableCell16.StylePriority.UsePadding = False
        Me.XrTableCell16.StylePriority.UseTextAlignment = False
        XrSummary3.FormatString = "{0:c2}"
        XrSummary3.IgnoreNullValues = True
        Me.XrTableCell16.Summary = XrSummary3
        Me.XrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell16.TextFormatString = "{0:c2}"
        Me.XrTableCell16.Weight = 0.22554557452030005R
        '
        'XrTable3
        '
        Me.XrTable3.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable3.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable3.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.XrTable3.LocationFloat = New DevExpress.Utils.PointFloat(520.0!, 120.0!)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable3.SizeF = New System.Drawing.SizeF(280.0006!, 20.0!)
        Me.XrTable3.StylePriority.UseBackColor = False
        Me.XrTable3.StylePriority.UseBorders = False
        Me.XrTable3.StylePriority.UseFont = False
        Me.XrTable3.StylePriority.UseTextAlignment = False
        Me.XrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell21, Me.XrTableCell22})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 0.8R
        '
        'XrTableCell21
        '
        Me.XrTableCell21.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTableCell21.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell21.StylePriority.UseBackColor = False
        Me.XrTableCell21.StylePriority.UseBorders = False
        Me.XrTableCell21.StylePriority.UseFont = False
        Me.XrTableCell21.StylePriority.UsePadding = False
        Me.XrTableCell21.StylePriority.UseTextAlignment = False
        Me.XrTableCell21.Text = "VALOR NETO FACTURA:"
        Me.XrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell21.Weight = 0.3007244791779744R
        '
        'XrTableCell22
        '
        Me.XrTableCell22.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTableCell22.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.TotalValue]")})
        Me.XrTableCell22.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100.0!)
        Me.XrTableCell22.StylePriority.UseBackColor = False
        Me.XrTableCell22.StylePriority.UseBorders = False
        Me.XrTableCell22.StylePriority.UseFont = False
        Me.XrTableCell22.StylePriority.UsePadding = False
        Me.XrTableCell22.StylePriority.UseTextAlignment = False
        XrSummary4.FormatString = "{0:c2}"
        Me.XrTableCell22.Summary = XrSummary4
        Me.XrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell22.TextFormatString = "{0:c2}"
        Me.XrTableCell22.Weight = 0.22554838633390917R
        '
        'XrLabel11
        '
        Me.XrLabel11.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel11.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.Description]")})
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(0.9583739!, 20.00002!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 5, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(519.0405!, 60.0!)
        Me.XrLabel11.StylePriority.UseBackColor = False
        Me.XrLabel11.StylePriority.UseBorders = False
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UsePadding = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "XrLabel11"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel12.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(520.0007!, 20.00001!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(159.9998!, 20.0!)
        Me.XrLabel12.StylePriority.UseBackColor = False
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UsePadding = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "DESCUENTOS"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel15.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.ValueDiscount]")})
        Me.XrLabel15.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(680.0005!, 20.00001!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(119.9999!, 20.0!)
        Me.XrLabel15.StylePriority.UseBackColor = False
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UsePadding = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        XrSummary5.FormatString = "{0:c2}"
        XrSummary5.IgnoreNullValues = True
        Me.XrLabel15.Summary = XrSummary5
        Me.XrLabel15.Text = "XrLabel15"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrLabel15.TextFormatString = "{0:c2}"
        '
        'XrLabel16
        '
        Me.XrLabel16.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel16.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(0.958374!, 80.00002!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(519.0405!, 20.0!)
        Me.XrLabel16.StylePriority.UseBackColor = False
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UsePadding = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "TOTAL:"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel18
        '
        Me.XrLabel18.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel18.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(519.9988!, 80.00002!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(160.0!, 20.0!)
        Me.XrLabel18.StylePriority.UseBackColor = False
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UsePadding = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "RETENCIÓN IVA"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel19
        '
        Me.XrLabel19.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel19.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.WithholdingIVA]")})
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(679.9989!, 80.00002!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(120.0005!, 20.0!)
        Me.XrLabel19.StylePriority.UseBackColor = False
        Me.XrLabel19.StylePriority.UseBorders = False
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UsePadding = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        XrSummary6.FormatString = "{0:c2}"
        XrSummary6.IgnoreNullValues = True
        Me.XrLabel19.Summary = XrSummary6
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrLabel19.TextFormatString = "{0:c2}"
        '
        'INDCllValueTotalInvoiceLetters
        '
        Me.INDCllValueTotalInvoiceLetters.BackColor = System.Drawing.Color.Transparent
        Me.INDCllValueTotalInvoiceLetters.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.INDCllValueTotalInvoiceLetters.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDCllValueTotalInvoiceLetters.LocationFloat = New DevExpress.Utils.PointFloat(0.9583759!, 100.0!)
        Me.INDCllValueTotalInvoiceLetters.Name = "INDCllValueTotalInvoiceLetters"
        Me.INDCllValueTotalInvoiceLetters.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 5, 0, 100.0!)
        Me.INDCllValueTotalInvoiceLetters.SizeF = New System.Drawing.SizeF(519.0405!, 39.99999!)
        Me.INDCllValueTotalInvoiceLetters.StylePriority.UseBackColor = False
        Me.INDCllValueTotalInvoiceLetters.StylePriority.UseBorders = False
        Me.INDCllValueTotalInvoiceLetters.StylePriority.UseFont = False
        Me.INDCllValueTotalInvoiceLetters.StylePriority.UsePadding = False
        Me.INDCllValueTotalInvoiceLetters.StylePriority.UseTextAlignment = False
        Me.INDCllValueTotalInvoiceLetters.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel21
        '
        Me.XrLabel21.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel21.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(519.9988!, 100.0!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(160.0!, 20.0!)
        Me.XrLabel21.StylePriority.UseBackColor = False
        Me.XrLabel21.StylePriority.UseBorders = False
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UsePadding = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "RETENCIÓN ICA"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel22
        '
        Me.XrLabel22.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel22.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicBillingId.WithholdingICA]")})
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(679.9989!, 100.0!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(120.0005!, 20.0!)
        Me.XrLabel22.StylePriority.UseBackColor = False
        Me.XrLabel22.StylePriority.UseBorders = False
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UsePadding = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        XrSummary7.FormatString = "{0:c2}"
        XrSummary7.IgnoreNullValues = True
        Me.XrLabel22.Summary = XrSummary7
        Me.XrLabel22.Text = "XrLabel22"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrLabel22.TextFormatString = "{0:c2}"
        '
        'INDCfFooterPage
        '
        Me.INDCfFooterPage.Expression = resources.GetString("INDCfFooterPage.Expression")
        Me.INDCfFooterPage.Name = "INDCfFooterPage"
        '
        'INDPrResolutionNumber
        '
        Me.INDPrResolutionNumber.Description = "Parameter1"
        Me.INDPrResolutionNumber.Name = "INDPrResolutionNumber"
        Me.INDPrResolutionNumber.Visible = False
        '
        'INDPrResolutionDate
        '
        Me.INDPrResolutionDate.Description = "Parameter1"
        Me.INDPrResolutionDate.Name = "INDPrResolutionDate"
        Me.INDPrResolutionDate.Visible = False
        '
        'INDPrResolutionInvoicePrefix
        '
        Me.INDPrResolutionInvoicePrefix.Description = "Parameter1"
        Me.INDPrResolutionInvoicePrefix.Name = "INDPrResolutionInvoicePrefix"
        Me.INDPrResolutionInvoicePrefix.Visible = False
        '
        'INDPrResolutionInitialInvoice
        '
        Me.INDPrResolutionInitialInvoice.Description = "Parameter1"
        Me.INDPrResolutionInitialInvoice.Name = "INDPrResolutionInitialInvoice"
        Me.INDPrResolutionInitialInvoice.Type = GetType(Integer)
        Me.INDPrResolutionInitialInvoice.ValueInfo = "0"
        Me.INDPrResolutionInitialInvoice.Visible = False
        '
        'INDPrResolutionFinalInvoice
        '
        Me.INDPrResolutionFinalInvoice.Description = "Parameter1"
        Me.INDPrResolutionFinalInvoice.Name = "INDPrResolutionFinalInvoice"
        Me.INDPrResolutionFinalInvoice.Type = GetType(Integer)
        Me.INDPrResolutionFinalInvoice.ValueInfo = "0"
        Me.INDPrResolutionFinalInvoice.Visible = False
        '
        'INDIdInvoiceSubreport
        '
        Me.INDIdInvoiceSubreport.Description = "Parameter1"
        Me.INDIdInvoiceSubreport.Name = "INDIdInvoiceSubreport"
        Me.INDIdInvoiceSubreport.Type = GetType(Integer)
        Me.INDIdInvoiceSubreport.ValueInfo = "0"
        Me.INDIdInvoiceSubreport.Visible = False
        '
        'INDCfCodeName
        '
        Me.INDCfCodeName.Expression = resources.GetString("INDCfCodeName.Expression")
        Me.INDCfCodeName.Name = "INDCfCodeName"
        '
        'INDCfTotalValue
        '
        Me.INDCfTotalValue.Expression = "[BasicBillingId.TotalValue]"
        Me.INDCfTotalValue.Name = "INDCfTotalValue"
        '
        'INDCfCode
        '
        Me.INDCfCode.Expression = "IIf([DetailType]=1,[ProductId.Code]," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    IIf([DetailType]=2,[BillingConceptId.Co" &
    "de]," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "        IIf([DetailType]=3,[PhysicalAssetId.Plate],'')" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    )" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ")"
        Me.INDCfCode.Name = "INDCfCode"
        '
        'INDCfName
        '
        Me.INDCfName.Expression = "IIf([DetailType]=1,[ProductId.Name]," & Global.Microsoft.VisualBasic.ChrW(10) & "    IIf([DetailType]=2,[BillingConceptId.Nam" &
    "e]," & Global.Microsoft.VisualBasic.ChrW(10) & "        IIf([DetailType]=3, [PhysicalAssetId.ItemId.Description],'')" & Global.Microsoft.VisualBasic.ChrW(10) & "    )" & Global.Microsoft.VisualBasic.ChrW(10) & ")" &
    ""
        Me.INDCfName.Name = "INDCfName"
        '
        'INDCfSaleModality
        '
        Me.INDCfSaleModality.Expression = "Iif([BasicBillingId.SaleModality] = 1, 'Contado', 'Credito')"
        Me.INDCfSaleModality.Name = "INDCfSaleModality"
        '
        'INDPrResolutionInitialDate
        '
        Me.INDPrResolutionInitialDate.Name = "INDPrResolutionInitialDate"
        Me.INDPrResolutionInitialDate.Visible = False
        '
        'INDPrResolutionFinalDate
        '
        Me.INDPrResolutionFinalDate.Name = "INDPrResolutionFinalDate"
        Me.INDPrResolutionFinalDate.Visible = False
        '
        'INDCfTerm
        '
        Me.INDCfTerm.Expression = "Iif([BasicBillingId.SaleModality] = 1, '', [BasicBillingId.CustomerId.Term] + ' d" &
    "ías')"
        Me.INDCfTerm.Name = "INDCfTerm"
        '
        'INDfromSaleInvoiceAll
        '
        Me.INDfromSaleInvoiceAll.Description = "Bandera si viene del reporte general"
        Me.INDfromSaleInvoiceAll.Name = "INDfromSaleInvoiceAll"
        Me.INDfromSaleInvoiceAll.Type = GetType(Boolean)
        Me.INDfromSaleInvoiceAll.ValueInfo = "False"
        Me.INDfromSaleInvoiceAll.Visible = False
        '
        '
        'rptBasicBilling
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.GroupHeader1, Me.PageFooter, Me.GroupFooter1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDCfFooterPage, Me.INDCfCodeName, Me.INDCfTotalValue, Me.INDCfCode, Me.INDCfName, Me.INDCfSaleModality, Me.INDCfTerm})
                ' DataSource se asigna en runtime
        Me.Margins = New System.Drawing.Printing.Margins(25, 25, 26, 27)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDIdInvoiceSubreport, Me.INDPrResolutionNumber, Me.INDPrResolutionDate, Me.INDPrResolutionInvoicePrefix, Me.INDPrResolutionInitialInvoice, Me.INDPrResolutionFinalInvoice, Me.INDPrResolutionInitialDate, Me.INDPrResolutionFinalDate, Me.INDfromSaleInvoiceAll})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Tag = "2038"
        Me.Version = "20.1"
        CType(Me.XrTable1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable8,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XrTable3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit

End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents INDtxtInvoiceDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblInvoice As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrBarCode2 As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell38 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell75 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell43 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell44 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrTable8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow16 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell35 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDUserCreate As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDUserImp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable9 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable6 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell24 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable5 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDCfFooterPage As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDPrResolutionNumber As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrResolutionDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrResolutionInvoicePrefix As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrResolutionInitialInvoice As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrResolutionFinalInvoice As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDIdInvoiceSubreport As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDtxtCUFE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDtrElectronicDocumentStatus As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDlblElectronicDocumentStatus As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDtxtElectronicDocumentStatus As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCfCodeName As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfTotalValue As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell36 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCfCode As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfName As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDCllValueTotalInvoiceLetters As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableRow6 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow11 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell31 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell32 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow14 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell39 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell41 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow15 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell48 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell49 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCfSaleModality As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblCUFE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDtxtShippingDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblShippingDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDtxtInvoice As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDPrResolutionInitialDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrResolutionFinalDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCfTerm As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDfromSaleInvoiceAll As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrTableRow12 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
End Class
