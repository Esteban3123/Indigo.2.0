<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptTransferPortfolioTotalized
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptTransferPortfolioTotalized))
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.INDCfStatusText = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfCity = New DevExpress.XtraReports.UI.CalculatedField()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.INDTblUser = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell35 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDUserCreate = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDLblUserPrint = New DevExpress.XtraReports.UI.XRTableCell()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.INDCfDebitValue = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfTotalTransfer = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfValueOther = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfValueDetailTra = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfCreditValue = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDSubIdTransferP = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDCfTotalOtherConcept = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDCfTotalDetail = New DevExpress.XtraReports.UI.CalculatedField()
        Me.FlagReport = New DevExpress.XtraReports.Parameters.Parameter()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable8 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell36 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow16 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell48 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell49 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell50 = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ThirdPartyId.Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("PortfolioAdvanceId.Abbreviation", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
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
        Me.BottomMargin.HeightF = 26.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'GroupHeader1
        '
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ThirdPartyId.Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("PortfolioAdvanceId.Abbreviation", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.HeightF = 0!
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.SnapLinePadding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        '
        'INDCfStatusText
        '
        Me.INDCfStatusText.Expression = "Iif([Status] = 1, 'Registrado' ,Iif([Status] = 2, 'Confirmado' , 'Anulado') )"
        Me.INDCfStatusText.Name = "INDCfStatusText"
        '
        'INDCfCity
        '
        Me.INDCfCity.Expression = resources.GetString("INDCfCity.Expression")
        Me.INDCfCity.Name = "INDCfCity"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDTblUser})
        Me.PageFooter.HeightF = 20.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'INDTblUser
        '
        Me.INDTblUser.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDTblUser.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.INDTblUser.Name = "INDTblUser"
        Me.INDTblUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.INDTblUser.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow9})
        Me.INDTblUser.SizeF = New System.Drawing.SizeF(751.0!, 20.0!)
        Me.INDTblUser.StylePriority.UseFont = False
        Me.INDTblUser.StylePriority.UsePadding = False
        Me.INDTblUser.StylePriority.UseTextAlignment = False
        Me.INDTblUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell35, Me.INDUserCreate, Me.INDLblUserPrint})
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 1.0R
        '
        'XrTableCell35
        '
        Me.XrTableCell35.Name = "XrTableCell35"
        Me.XrTableCell35.Text = "Usuario Creación :"
        Me.XrTableCell35.Weight = 0.36000029938068412R
        '
        'INDUserCreate
        '
        Me.INDUserCreate.Name = "INDUserCreate"
        Me.INDUserCreate.Weight = 1.1039998756048617R
        '
        'INDLblUserPrint
        '
        Me.INDLblUserPrint.Name = "INDLblUserPrint"
        Me.INDLblUserPrint.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.INDLblUserPrint.StylePriority.UsePadding = False
        Me.INDLblUserPrint.StylePriority.UseTextAlignment = False
        Me.INDLblUserPrint.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDLblUserPrint.Weight = 1.0559997656068969R
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo)
        '
        'INDCfDebitValue
        '
        Me.INDCfDebitValue.DataMember = "PortfolioTransferOtherConceptReportXpo"
        Me.INDCfDebitValue.Expression = "Iif([Nature]=1, [Value]  , 0)"
        Me.INDCfDebitValue.Name = "INDCfDebitValue"
        '
        'INDCfTotalTransfer
        '
        Me.INDCfTotalTransfer.Expression = "[INDCfTotalDetail] + [INDCfTotalOtherConcept]"
        Me.INDCfTotalTransfer.Name = "INDCfTotalTransfer"
        '
        'INDCfValueOther
        '
        Me.INDCfValueOther.DataMember = "PortfolioTransferOtherConceptReportXpo"
        Me.INDCfValueOther.Expression = "Sum([INDCfCreditValue] - [INDCfDebitValue])"
        Me.INDCfValueOther.Name = "INDCfValueOther"
        '
        'INDCfValueDetailTra
        '
        Me.INDCfValueDetailTra.DataMember = "PortfolioTransferDetailReportXpo"
        Me.INDCfValueDetailTra.Expression = "Sum([Value])"
        Me.INDCfValueDetailTra.Name = "INDCfValueDetailTra"
        '
        'INDCfCreditValue
        '
        Me.INDCfCreditValue.DataMember = "PortfolioTransferOtherConceptReportXpo"
        Me.INDCfCreditValue.Expression = "Iif([Nature]=2, [Value], 0 )"
        Me.INDCfCreditValue.Name = "INDCfCreditValue"
        '
        'INDSubIdTransferP
        '
        Me.INDSubIdTransferP.Description = "Parameter1"
        Me.INDSubIdTransferP.Name = "INDSubIdTransferP"
        Me.INDSubIdTransferP.Type = GetType(Integer)
        Me.INDSubIdTransferP.ValueInfo = "0"
        Me.INDSubIdTransferP.Visible = False
        '
        'INDCfTotalOtherConcept
        '
        Me.INDCfTotalOtherConcept.Expression = "IsNull([PortfolioTransferOtherConceptReportXpo.INDCfValueOther], 0)"
        Me.INDCfTotalOtherConcept.Name = "INDCfTotalOtherConcept"
        '
        'INDCfTotalDetail
        '
        Me.INDCfTotalDetail.Expression = "IsNull([PortfolioTransferDetailReportXpo.INDCfValueDetailTra], 0)"
        Me.INDCfTotalDetail.Name = "INDCfTotalDetail"
        '
        'FlagReport
        '
        Me.FlagReport.Description = "Parameter1"
        Me.FlagReport.Name = "FlagReport"
        Me.FlagReport.Type = GetType(Boolean)
        Me.FlagReport.ValueInfo = "False"
        Me.FlagReport.Visible = False
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.GroupFooter1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail
        Me.GroupFooter1.HeightF = 20.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrTable2
        '
        Me.XrTable2.BackColor = System.Drawing.Color.Transparent
        Me.XrTable2.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable2.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(750.6415!, 20.0!)
        Me.XrTable2.StylePriority.UseBackColor = False
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell6})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PortfolioAdvanceId].[ThirdPartyId].[NitName]")})
        Me.XrTableCell4.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UsePadding = False
        Me.XrTableCell4.Weight = 1.0205549433253138R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PortfolioAdvanceId].[CommonCurrency].[Abbreviation]")})
        Me.XrTableCell5.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UsePadding = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell5.Weight = 0.3275645301225979R
        '
        'XrTableCell6
        '
        Me.XrTableCell6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([INDCfTotalTransfer])")})
        Me.XrTableCell6.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell6.StylePriority.UseFont = False
        Me.XrTableCell6.StylePriority.UsePadding = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:c0}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell6.Summary = XrSummary1
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell6.TextFormatString = "{0:c0}"
        Me.XrTableCell6.Weight = 0.40773526679557792R
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblCompany, Me.XrPictureBox1, Me.INDLblNitCompany, Me.XrPictureBox2, Me.XrPageInfo1, Me.XrPageInfo3, Me.XrLabel13, Me.XrTable8})
        Me.ReportHeader.HeightF = 176.0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 34.20833!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(551.0!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 34.20833!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 59.20833!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(551.0!, 25.0!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(651.0001!, 34.20833!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(651.0001!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo1.TextFormatString = "Página {0}/{1}"
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
        'XrTable8
        '
        Me.XrTable8.BackColor = System.Drawing.Color.Gainsboro
        Me.XrTable8.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 135.25!)
        Me.XrTable8.Name = "XrTable8"
        Me.XrTable8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow8, Me.XrTableRow16})
        Me.XrTable8.SizeF = New System.Drawing.SizeF(751.0001!, 40.0!)
        Me.XrTable8.StylePriority.UseBackColor = False
        Me.XrTable8.StylePriority.UseBorders = False
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell36})
        Me.XrTableRow8.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.StylePriority.UseFont = False
        Me.XrTableRow8.Weight = 0.8R
        '
        'XrTableCell36
        '
        Me.XrTableCell36.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell36.Name = "XrTableCell36"
        Me.XrTableCell36.StylePriority.UseFont = False
        Me.XrTableCell36.StylePriority.UseTextAlignment = False
        Me.XrTableCell36.Text = "TOTALIZADOS"
        Me.XrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell36.Weight = 3.0R
        '
        'XrTableRow16
        '
        Me.XrTableRow16.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell48, Me.XrTableCell49, Me.XrTableCell50})
        Me.XrTableRow16.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.XrTableRow16.Name = "XrTableRow16"
        Me.XrTableRow16.StylePriority.UseFont = False
        Me.XrTableRow16.StylePriority.UseTextAlignment = False
        Me.XrTableRow16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableRow16.Weight = 0.8R
        '
        'XrTableCell48
        '
        Me.XrTableCell48.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell48.Name = "XrTableCell48"
        Me.XrTableCell48.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell48.StylePriority.UseFont = False
        Me.XrTableCell48.StylePriority.UsePadding = False
        Me.XrTableCell48.StylePriority.UseTextAlignment = False
        Me.XrTableCell48.Text = "Tercero"
        Me.XrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell48.Weight = 1.7428549624527512R
        '
        'XrTableCell49
        '
        Me.XrTableCell49.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell49.Name = "XrTableCell49"
        Me.XrTableCell49.Padding = New DevExpress.XtraPrinting.PaddingInfo(1, 0, 0, 0, 100.0!)
        Me.XrTableCell49.StylePriority.UseFont = False
        Me.XrTableCell49.StylePriority.UsePadding = False
        Me.XrTableCell49.StylePriority.UseTextAlignment = False
        Me.XrTableCell49.Text = "Moneda"
        Me.XrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell49.Weight = 0.55940181593695493R
        '
        'XrTableCell50
        '
        Me.XrTableCell50.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell50.Name = "XrTableCell50"
        Me.XrTableCell50.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.XrTableCell50.StylePriority.UseFont = False
        Me.XrTableCell50.StylePriority.UsePadding = False
        Me.XrTableCell50.StylePriority.UseTextAlignment = False
        Me.XrTableCell50.Text = "Valor"
        Me.XrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell50.Weight = 0.6977432216102939R
        '
        'rptTransferPortfolioTotalized
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupHeader1, Me.PageFooter, Me.GroupFooter1, Me.ReportHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDCfStatusText, Me.INDCfCity, Me.INDCfTotalTransfer, Me.INDCfTotalDetail, Me.INDCfTotalOtherConcept, Me.INDCfValueDetailTra, Me.INDCfDebitValue, Me.INDCfCreditValue, Me.INDCfValueOther})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource1})
        Me.DataSource = Me.BindingSource1
        Me.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Margins = New System.Drawing.Printing.Margins(50, 49, 26, 26)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDSubIdTransferP, Me.FlagReport})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "20.1"
        CType(Me.INDTblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents INDCfStatusText As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfCity As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents INDTblUser As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell35 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDUserCreate As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDLblUserPrint As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDCfDebitValue As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfTotalTransfer As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfValueOther As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfValueDetailTra As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfCreditValue As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDSubIdTransferP As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDCfTotalOtherConcept As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDCfTotalDetail As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents FlagReport As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrTable8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell36 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow16 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell48 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell49 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell50 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
End Class
