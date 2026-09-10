<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDevolutionUnified
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptDevolutionUnified))
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrRichText2 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrRichText5 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblNombreEmpresaCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDPrCompanyName = New DevExpress.XtraReports.Parameters.Parameter()
        Me.INDLblNitCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDPrCompanyNit = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrRichText1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrRichText7 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrRichText4 = New DevExpress.XtraReports.UI.XRRichText()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrRichText6 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrRichText9 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrRichText8 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrRichText3 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.INDPrOperatingUnit = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.XrRichText2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3, Me.XrTableCell4})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.BackColor = System.Drawing.Color.LightGray
        Me.XrTableCell3.BorderColor = System.Drawing.Color.Black
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "InvoiceNumber")})
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseBackColor = False
        Me.XrTableCell3.StylePriority.UseBorderColor = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 1.4134615538106177R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.BackColor = System.Drawing.Color.LightGray
        Me.XrTableCell4.BorderColor = System.Drawing.Color.Black
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BalanceInvoice", "{0:$#,#.00}")})
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseBackColor = False
        Me.XrTableCell4.StylePriority.UseBorderColor = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 1.5865384461893823R
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(0!, 312.0044!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(197.9167!, 23.0!)
        Me.XrLabel9.Text = "Apreciado (a) Doctor (a):"
        '
        'XrLabel19
        '
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GlosaDevolutionsReceptionCId.PersonSendsPosition")})
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(0!, 304.0858!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(326.0416!, 23.0!)
        Me.XrLabel19.Text = "XrLabel19"
        '
        'XrLabel17
        '
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(0!, 177.0833!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(180.2083!, 23.0!)
        Me.XrLabel17.Text = "Cordialmente."
        '
        'XrRichText2
        '
        Me.XrRichText2.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 120.4167!)
        Me.XrRichText2.Name = "XrRichText2"
        Me.XrRichText2.SerializableRtfString = resources.GetString("XrRichText2.SerializableRtfString")
        Me.XrRichText2.SizeF = New System.Drawing.SizeF(650.0!, 136.5417!)
        '
        'XrRichText5
        '
        Me.XrRichText5.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText5.LocationFloat = New DevExpress.Utils.PointFloat(0.0001271566!, 491.2501!)
        Me.XrRichText5.Name = "XrRichText5"
        Me.XrRichText5.SerializableRtfString = resources.GetString("XrRichText5.SerializableRtfString")
        Me.XrRichText5.SizeF = New System.Drawing.SizeF(649.9998!, 79.62497!)
        '
        'XrTableCell2
        '
        Me.XrTableCell2.BackColor = System.Drawing.Color.Black
        Me.XrTableCell2.ForeColor = System.Drawing.Color.White
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseBackColor = False
        Me.XrTableCell2.StylePriority.UseForeColor = False
        Me.XrTableCell2.Text = "Valor"
        Me.XrTableCell2.Weight = 1.5865384461893823R
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 197.9626!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.XrLabel7.Text = "Ciudad"
        '
        'bindingSource1
        '
        Me.bindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.GlosasRepository.GlosaDevolutionsReceptionDXpo)
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPictureBox2, Me.XrPictureBox1, Me.INDLblNombreEmpresaCliente, Me.INDLblNitCliente, Me.XrRichText1, Me.XrRichText7, Me.XrLabel10, Me.XrLabel9, Me.XrLabel7, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3})
        Me.ReportHeader.HeightF = 379.1368!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(549.1809!, 0!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblNombreEmpresaCliente
        '
        Me.INDLblNombreEmpresaCliente.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.INDLblNombreEmpresaCliente.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.INDPrCompanyName, "Text", "")})
        Me.INDLblNombreEmpresaCliente.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreEmpresaCliente.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 0!)
        Me.INDLblNombreEmpresaCliente.Name = "INDLblNombreEmpresaCliente"
        Me.INDLblNombreEmpresaCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreEmpresaCliente.SizeF = New System.Drawing.SizeF(449.1809!, 25.19685!)
        Me.INDLblNombreEmpresaCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNombreEmpresaCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDPrCompanyName
        '
        Me.INDPrCompanyName.Description = "Nombre Empresa"
        Me.INDPrCompanyName.Name = "INDPrCompanyName"
        Me.INDPrCompanyName.Visible = False
        '
        'INDLblNitCliente
        '
        Me.INDLblNitCliente.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.INDPrCompanyNit, "Text", "")})
        Me.INDLblNitCliente.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 25.19685!)
        Me.INDLblNitCliente.Name = "INDLblNitCliente"
        Me.INDLblNitCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCliente.SizeF = New System.Drawing.SizeF(449.1808!, 19.79166!)
        Me.INDLblNitCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNitCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDPrCompanyNit
        '
        Me.INDPrCompanyNit.Description = "Nit Empresa"
        Me.INDPrCompanyNit.Name = "INDPrCompanyNit"
        Me.INDPrCompanyNit.Visible = False
        '
        'XrRichText1
        '
        Me.XrRichText1.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText1.LocationFloat = New DevExpress.Utils.PointFloat(0.0002991919!, 99.71264!)
        Me.XrRichText1.Name = "XrRichText1"
        Me.XrRichText1.SerializableRtfString = resources.GetString("XrRichText1.SerializableRtfString")
        Me.XrRichText1.SizeF = New System.Drawing.SizeF(650.0001!, 22.99998!)
        Me.XrRichText1.StylePriority.UseFont = False
        '
        'XrRichText7
        '
        Me.XrRichText7.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 255.7543!)
        Me.XrRichText7.Name = "XrRichText7"
        Me.XrRichText7.SerializableRtfString = resources.GetString("XrRichText7.SerializableRtfString")
        Me.XrRichText7.SizeF = New System.Drawing.SizeF(640.0!, 42.79166!)
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(0!, 354.0877!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(650.0!, 25.04904!)
        Me.XrLabel10.Text = "De manera comedida, me permito comunicarle la irregularidad observada en el trámi" &
    "te que:"
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GlosaDevolutionsReceptionCId.ReceivesDevolutionPosition")})
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 179.9626!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(649.1807!, 17.99998!)
        Me.XrLabel5.Text = "XrLabel5"
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GlosaDevolutionsReceptionCId.ReceivesDevolution")})
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 161.9626!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(649.1807!, 18.0!)
        Me.XrLabel4.Text = "XrLabel4"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 143.9626!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.XrLabel3.Text = "Señores"
        '
        'XrLabel16
        '
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(0!, 124.2917!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(404.1667!, 23.0!)
        Me.XrLabel16.Text = "Se anexa lo enunciado con soportes en original"
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GlosaDevolutionsReceptionCId.PersonSends")})
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(0!, 281.0858!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(326.0416!, 23.0!)
        Me.XrLabel18.Text = "XrLabel18"
        '
        'XrLabel15
        '
        Me.XrLabel15.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(0!, 750.0!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(649.9999!, 57.70837!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Cualquier inquietud o duda sobre el respecto, con gusto será atendida por nuestro" &
    " personal autorizado para dicho fin, en el teléfono 8724100 extensión 1247 o en " &
    "el celular corporativo No. 3175860144."
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel19, Me.XrLabel18, Me.XrLabel17, Me.XrLabel16})
        Me.ReportFooter.HeightF = 327.0858!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrRichText4
        '
        Me.XrRichText4.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 370.7501!)
        Me.XrRichText4.Name = "XrRichText4"
        Me.XrRichText4.SerializableRtfString = resources.GetString("XrRichText4.SerializableRtfString")
        Me.XrRichText4.SizeF = New System.Drawing.SizeF(650.0!, 52.16666!)
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrRichText6, Me.XrRichText9, Me.XrRichText8, Me.XrLabel15, Me.XrRichText5, Me.XrTable1, Me.XrRichText4, Me.XrRichText3, Me.XrRichText2, Me.XrLabel11})
        Me.Detail.HeightF = 817.7084!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrRichText6
        '
        Me.XrRichText6.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText6.LocationFloat = New DevExpress.Utils.PointFloat(0.0002994537!, 51.04166!)
        Me.XrRichText6.Name = "XrRichText6"
        Me.XrRichText6.SerializableRtfString = resources.GetString("XrRichText6.SerializableRtfString")
        Me.XrRichText6.SizeF = New System.Drawing.SizeF(650.0!, 58.41668!)
        '
        'XrRichText9
        '
        Me.XrRichText9.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.XrRichText9.LocationFloat = New DevExpress.Utils.PointFloat(0!, 673.9583!)
        Me.XrRichText9.Name = "XrRichText9"
        Me.XrRichText9.SerializableRtfString = resources.GetString("XrRichText9.SerializableRtfString")
        Me.XrRichText9.SizeF = New System.Drawing.SizeF(650.0!, 64.66675!)
        '
        'XrRichText8
        '
        Me.XrRichText8.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.XrRichText8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 582.2917!)
        Me.XrRichText8.Name = "XrRichText8"
        Me.XrRichText8.SerializableRtfString = resources.GetString("XrRichText8.SerializableRtfString")
        Me.XrRichText8.SizeF = New System.Drawing.SizeF(650.0!, 81.33325!)
        '
        'XrTable1
        '
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 436.0417!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1, Me.XrTableRow2})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(649.9999!, 41.66669!)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.BackColor = System.Drawing.Color.Black
        Me.XrTableCell1.ForeColor = System.Drawing.Color.White
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseBackColor = False
        Me.XrTableCell1.StylePriority.UseForeColor = False
        Me.XrTableCell1.Text = "Numero de Factura"
        Me.XrTableCell1.Weight = 1.4134615538106177R
        '
        'XrRichText3
        '
        Me.XrRichText3.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.XrRichText3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 271.4583!)
        Me.XrRichText3.Name = "XrRichText3"
        Me.XrRichText3.SerializableRtfString = resources.GetString("XrRichText3.SerializableRtfString")
        Me.XrRichText3.SizeF = New System.Drawing.SizeF(650.0!, 84.45831!)
        '
        'XrLabel11
        '
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GlosaDevolutionsReceptionCId.CustomerId.Name", """{0}""")})
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(0!, 9.999974!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(649.9999!, 23.0!)
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "XrLabel11"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
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
        'INDPrOperatingUnit
        '
        Me.INDPrOperatingUnit.Description = "Unidad Operativa"
        Me.INDPrOperatingUnit.Name = "INDPrOperatingUnit"
        Me.INDPrOperatingUnit.Visible = False
        '
        'rptDevolutionUnified
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.bindingSource1})
        Me.DataSource = Me.bindingSource1
        Me.Font = New System.Drawing.Font("Arial", 11.0!)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDPrCompanyNit, Me.INDPrCompanyName, Me.INDPrOperatingUnit})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "19.1"
        CType(Me.XrRichText2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrRichText2 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrRichText5 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents bindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents INDLblNombreEmpresaCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrRichText1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrRichText7 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrRichText4 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents XrRichText9 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrRichText8 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrRichText3 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDPrCompanyNit As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents INDPrCompanyName As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrRichText6 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents INDPrOperatingUnit As DevExpress.XtraReports.Parameters.Parameter
End Class
