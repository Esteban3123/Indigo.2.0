<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptCertificate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptCertificate))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.INDLblNitCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblCompany = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.INDLblNombreReporte = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrRichText1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrTable7 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow18 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.INDUserCreate = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDUserImp = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 25.95833!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 25.83332!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblNitCompany, Me.INDLblCompany, Me.XrPageInfo1, Me.XrLabel22, Me.XrPageInfo3, Me.XrPictureBox1, Me.XrPictureBox2, Me.INDLblNombreReporte})
        Me.PageHeader.HeightF = 222.9167!
        Me.PageHeader.Name = "PageHeader"
        '
        'INDLblNitCompany
        '
        Me.INDLblNitCompany.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.INDLblNitCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 67.8443!)
        Me.INDLblNitCompany.Name = "INDLblNitCompany"
        Me.INDLblNitCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCompany.SizeF = New System.Drawing.SizeF(573.0!, 24.99998!)
        Me.INDLblNitCompany.StylePriority.UseFont = False
        Me.INDLblNitCompany.StylePriority.UseTextAlignment = False
        Me.INDLblNitCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblCompany
        '
        Me.INDLblCompany.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblCompany.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 42.84426!)
        Me.INDLblCompany.Name = "INDLblCompany"
        Me.INDLblCompany.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblCompany.SizeF = New System.Drawing.SizeF(573.0!, 25.0!)
        Me.INDLblCompany.StylePriority.UseFont = False
        Me.INDLblCompany.StylePriority.UseTextAlignment = False
        Me.INDLblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo1.Format = "Página {0}/{1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(673.0!, 9.417191!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 20.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 9.417191!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(85.45834!, 20.0!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "Fecha Impresión:"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPageInfo3
        '
        Me.XrPageInfo3.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.XrPageInfo3.Format = "{0:dddd, dd' de 'MMMM' de 'yyyy HH:mm}"
        Me.XrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(85.45834!, 9.417191!)
        Me.XrPageInfo3.Name = "XrPageInfo3"
        Me.XrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.XrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo3.SizeF = New System.Drawing.SizeF(171.7917!, 20.0!)
        Me.XrPageInfo3.StylePriority.UseFont = False
        Me.XrPageInfo3.StylePriority.UsePadding = False
        Me.XrPageInfo3.StylePriority.UseTextAlignment = False
        Me.XrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.ImageUrl = "Resources\LogoIzquierda.png"
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 42.84427!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.ImageUrl = "Resources\LogoDerecha.png"
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(673.0!, 42.84427!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(100.0!, 75.0!)
        '
        'INDLblNombreReporte
        '
        Me.INDLblNombreReporte.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreReporte.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 126.8443!)
        Me.INDLblNombreReporte.LockedInUserDesigner = True
        Me.INDLblNombreReporte.Name = "INDLblNombreReporte"
        Me.INDLblNombreReporte.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreReporte.SizeF = New System.Drawing.SizeF(772.9999!, 25.19685!)
        Me.INDLblNombreReporte.StylePriority.UseFont = False
        Me.INDLblNombreReporte.StylePriority.UseTextAlignment = False
        Me.INDLblNombreReporte.Text = "CERTIFICADO LABORAL"
        Me.INDLblNombreReporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrRichText1})
        Me.GroupHeader1.HeightF = 72.91666!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrRichText1
        '
        Me.XrRichText1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.XrRichText1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrRichText1.Name = "XrRichText1"
        Me.XrRichText1.SerializableRtfString = resources.GetString("XrRichText1.SerializableRtfString")
        Me.XrRichText1.SizeF = New System.Drawing.SizeF(773.0!, 72.91666!)
        Me.XrRichText1.StylePriority.UseFont = False
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
        Me.XrTable7.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTable7.Name = "XrTable7"
        Me.XrTable7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow18})
        Me.XrTable7.SizeF = New System.Drawing.SizeF(772.9999!, 20.0!)
        Me.XrTable7.StylePriority.UseBackColor = False
        Me.XrTable7.StylePriority.UseBorders = False
        '
        'XrTableRow18
        '
        Me.XrTableRow18.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.INDUserCreate, Me.INDUserImp})
        Me.XrTableRow18.Name = "XrTableRow18"
        Me.XrTableRow18.Weight = 0.8R
        '
        'INDUserCreate
        '
        Me.INDUserCreate.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDUserCreate.Name = "INDUserCreate"
        Me.INDUserCreate.StylePriority.UseFont = False
        Me.INDUserCreate.StylePriority.UseTextAlignment = False
        Me.INDUserCreate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.INDUserCreate.Weight = 1.3225357946053997R
        '
        'INDUserImp
        '
        Me.INDUserImp.Font = New System.Drawing.Font("Arial", 7.5!)
        Me.INDUserImp.Name = "INDUserImp"
        Me.INDUserImp.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 100.0!)
        Me.INDUserImp.StylePriority.UseFont = False
        Me.INDUserImp.StylePriority.UsePadding = False
        Me.INDUserImp.StylePriority.UseTextAlignment = False
        Me.INDUserImp.Text = "INDUserImp"
        Me.INDUserImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.INDUserImp.Weight = 1.2462141105972531R
        '
        'rptCertificate
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.GroupHeader1, Me.PageFooter})
        Me.Margins = New System.Drawing.Printing.Margins(36, 41, 26, 26)
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents INDLblNitCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblCompany As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents INDLblNombreReporte As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrRichText1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrTable7 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow18 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents INDUserCreate As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDUserImp As DevExpress.XtraReports.UI.XRTableCell
End Class
