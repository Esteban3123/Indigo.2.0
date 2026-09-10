<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptUnemployedLiquidation
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.INDLblNombreEmpresaCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNombreReporte = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeaderInformation = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblAñoLiquidacion = New DevExpress.XtraReports.UI.XRLabel()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.INDlblAddress = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblPhoneEmail = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel26, Me.XrLabel18, Me.XrLabel17, Me.XrLabel13, Me.XrLabel14, Me.XrLabel12, Me.XrLabel9, Me.XrLabel2, Me.XrLabel1, Me.XrLabel7})
        Me.Detail.HeightF = 17.00001!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel26
        '
        Me.XrLabel26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BasicSalary", "{0:$ #,##}")})
        Me.XrLabel26.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(746.3251!, 0!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(94.42926!, 17.0!)
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "FundContractId.FundId.Name")})
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(572.6251!, 0!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(173.7!, 17.0!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.Text = "XrLabel18"
        '
        'XrLabel17
        '
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "UnemployedInterestTotal", "{0:$ #,##}")})
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(840.7543!, 0!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(96.17474!, 17.0!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "XrLabel17"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TotalUnemployed", "{0:$ #,##}")})
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(936.9291!, 0!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(93.0708!, 17.0!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel14
        '
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "UnemployedEndingDate", "{0:dd/MM/yyyy}")})
        Me.XrLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(387.5418!, 0!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(69.1665!, 17.0!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel12
        '
        Me.XrLabel12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "UnemployedInitialDate", "{0:dd/MM/yyyy}")})
        Me.XrLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(307.9584!, 0!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(79.16675!, 17.0!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "XrLabel12"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SanctionTotalDays")})
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(513.8751!, 0!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(57.70782!, 17.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EmployeeId.ThirdPartyId.Name")})
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(87.91679!, 0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(220.0416!, 17.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "XrLabel2"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EmployeeId.ThirdPartyId.Nit")})
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(87.91679!, 17.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "WorkedTotalDays")})
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(456.7083!, 0!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(57.16687!, 17.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 28.75001!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 26.00002!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.HeightF = 30.20833!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel22, Me.XrPageInfo2, Me.XrPageInfo1, Me.XrLine4, Me.INDLblNombreEmpresaCliente, Me.INDLblNitCliente, Me.INDLblNombreReporte})
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(546.6586!, 72.99998!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(127.7917!, 17.00001!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Fecha de Impresión:"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(674.4503!, 72.99998!)
        Me.XrPageInfo2.LockedInUserDesigner = True
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(264.5833!, 17.00001!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.TextFormatString = "{0:dddd, dd' de 'MMMM' de 'yyyy hh:mm tt}"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(936.9291!, 73.07088!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(83.07086!, 16.92913!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.TextFormatString = "Página {0}/{1}"
        '
        'XrLine4
        '
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(8.189042!, 70.91662!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(1011.89!, 2.083359!)
        '
        'INDLblNombreEmpresaCliente
        '
        Me.INDLblNombreEmpresaCliente.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.INDLblNombreEmpresaCliente.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreEmpresaCliente.LocationFloat = New DevExpress.Utils.PointFloat(8.189042!, 0!)
        Me.INDLblNombreEmpresaCliente.Name = "INDLblNombreEmpresaCliente"
        Me.INDLblNombreEmpresaCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreEmpresaCliente.SizeF = New System.Drawing.SizeF(1011.811!, 25.19685!)
        Me.INDLblNombreEmpresaCliente.StylePriority.UseFont = False
        Me.INDLblNombreEmpresaCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNombreEmpresaCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCliente
        '
        Me.INDLblNitCliente.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.INDLblNitCliente.LocationFloat = New DevExpress.Utils.PointFloat(8.189042!, 25.98425!)
        Me.INDLblNitCliente.Name = "INDLblNitCliente"
        Me.INDLblNitCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCliente.SizeF = New System.Drawing.SizeF(1011.811!, 16.53543!)
        Me.INDLblNitCliente.StylePriority.UseFont = False
        Me.INDLblNitCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNitCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNombreReporte
        '
        Me.INDLblNombreReporte.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreReporte.LocationFloat = New DevExpress.Utils.PointFloat(8.189042!, 42.5197!)
        Me.INDLblNombreReporte.LockedInUserDesigner = True
        Me.INDLblNombreReporte.Name = "INDLblNombreReporte"
        Me.INDLblNombreReporte.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreReporte.SizeF = New System.Drawing.SizeF(1011.811!, 25.19684!)
        Me.INDLblNombreReporte.StylePriority.UseFont = False
        Me.INDLblNombreReporte.StylePriority.UseTextAlignment = False
        Me.INDLblNombreReporte.Text = "LIQUIDACION CESANTIAS"
        Me.INDLblNombreReporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupHeaderInformation
        '
        Me.GroupHeaderInformation.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel25, Me.XrLine2, Me.XrLabel20, Me.XrLabel19, Me.xrLabel10, Me.xrLabel8, Me.xrLabel5, Me.xrLabel3, Me.xrLabel4, Me.xrLabel6, Me.xrLabel11, Me.xrLabel15, Me.xrLabel16, Me.lblAñoLiquidacion})
        Me.GroupHeaderInformation.HeightF = 98.33336!
        Me.GroupHeaderInformation.Name = "GroupHeaderInformation"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(744.5585!, 76.12498!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(96.1958!, 17.0!)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.Text = "Salario Basico"
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLine2
        '
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(8.3815!, 96.25!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(1011.89!, 2.083359!)
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(936.9291!, 76.12502!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(93.0708!, 17.0!)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "Total Cesantias"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(840.7543!, 76.12502!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(96.17474!, 17.0!)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "Intereses"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel10
        '
        Me.xrLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(307.9584!, 76.12498!)
        Me.xrLabel10.Name = "xrLabel10"
        Me.xrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel10.SizeF = New System.Drawing.SizeF(79.58337!, 17.0!)
        Me.xrLabel10.StylePriority.UseFont = False
        Me.xrLabel10.StylePriority.UseTextAlignment = False
        Me.xrLabel10.Text = "Fecha Inicio"
        Me.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'xrLabel8
        '
        Me.xrLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(2.083397!, 60.4166!)
        Me.xrLabel8.Name = "xrLabel8"
        Me.xrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel8.SizeF = New System.Drawing.SizeF(85.8334!, 32.70842!)
        Me.xrLabel8.StylePriority.UseFont = False
        Me.xrLabel8.StylePriority.UseTextAlignment = False
        Me.xrLabel8.Text = "Numero Documento"
        Me.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel5
        '
        Me.xrLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(87.91679!, 76.12498!)
        Me.xrLabel5.Name = "xrLabel5"
        Me.xrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel5.SizeF = New System.Drawing.SizeF(220.0416!, 17.0!)
        Me.xrLabel5.StylePriority.UseFont = False
        Me.xrLabel5.StylePriority.UseTextAlignment = False
        Me.xrLabel5.Text = "Nombre"
        Me.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel3
        '
        Me.xrLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(387.5418!, 76.12502!)
        Me.xrLabel3.Name = "xrLabel3"
        Me.xrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel3.SizeF = New System.Drawing.SizeF(69.16653!, 17.0!)
        Me.xrLabel3.StylePriority.UseFont = False
        Me.xrLabel3.StylePriority.UseTextAlignment = False
        Me.xrLabel3.Text = "Fecha Fin"
        Me.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'xrLabel4
        '
        Me.xrLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(456.7083!, 76.12502!)
        Me.xrLabel4.Name = "xrLabel4"
        Me.xrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel4.SizeF = New System.Drawing.SizeF(57.16681!, 17.0!)
        Me.xrLabel4.StylePriority.UseFont = False
        Me.xrLabel4.StylePriority.UseTextAlignment = False
        Me.xrLabel4.Text = "N. Dias"
        Me.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel6
        '
        Me.xrLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(513.8751!, 60.4166!)
        Me.xrLabel6.Name = "xrLabel6"
        Me.xrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel6.SizeF = New System.Drawing.SizeF(58.74988!, 32.70839!)
        Me.xrLabel6.StylePriority.UseFont = False
        Me.xrLabel6.StylePriority.UseTextAlignment = False
        Me.xrLabel6.Text = "Dias Sancion"
        Me.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel11
        '
        Me.xrLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(572.625!, 76.12498!)
        Me.xrLabel11.Name = "xrLabel11"
        Me.xrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel11.SizeF = New System.Drawing.SizeF(171.9335!, 17.0!)
        Me.xrLabel11.StylePriority.UseFont = False
        Me.xrLabel11.StylePriority.UseTextAlignment = False
        Me.xrLabel11.Text = "Fondo"
        Me.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel15
        '
        Me.xrLabel15.BackColor = System.Drawing.Color.Gainsboro
        Me.xrLabel15.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.xrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(0.0005086263!, 35.33332!)
        Me.xrLabel15.Name = "xrLabel15"
        Me.xrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel15.SizeF = New System.Drawing.SizeF(1020.079!, 21.95833!)
        Me.xrLabel15.StylePriority.UseBackColor = False
        Me.xrLabel15.StylePriority.UseFont = False
        Me.xrLabel15.StylePriority.UseTextAlignment = False
        Me.xrLabel15.Text = "LISTADO EMPLEADOS LIQUIDACION"
        Me.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLabel16
        '
        Me.xrLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(0!, 1.999992!)
        Me.xrLabel16.Name = "xrLabel16"
        Me.xrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel16.SizeF = New System.Drawing.SizeF(180.4164!, 17.0!)
        Me.xrLabel16.StylePriority.UseFont = False
        Me.xrLabel16.StylePriority.UseTextAlignment = False
        Me.xrLabel16.Text = "Liquidación Cesantias Año:"
        Me.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblAñoLiquidacion
        '
        Me.lblAñoLiquidacion.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblAñoLiquidacion.LocationFloat = New DevExpress.Utils.PointFloat(180.4164!, 1.999982!)
        Me.lblAñoLiquidacion.Name = "lblAñoLiquidacion"
        Me.lblAñoLiquidacion.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblAñoLiquidacion.SizeF = New System.Drawing.SizeF(206.7087!, 17.0!)
        Me.lblAñoLiquidacion.StylePriority.UseFont = False
        Me.lblAñoLiquidacion.StylePriority.UseTextAlignment = False
        Me.lblAñoLiquidacion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PayrollRepository.PayrollUnemployedLiquidation)
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDlblAddress, Me.INDlblPhoneEmail})
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.PrintAtBottom = True
        '
        'INDlblAddress
        '
        Me.INDlblAddress.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblAddress.LocationFloat = New DevExpress.Utils.PointFloat(8.3815!, 33.5416!)
        Me.INDlblAddress.Multiline = True
        Me.INDlblAddress.Name = "INDlblAddress"
        Me.INDlblAddress.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblAddress.SizeF = New System.Drawing.SizeF(1013.237!, 16.4584!)
        Me.INDlblAddress.StylePriority.UseFont = False
        Me.INDlblAddress.StylePriority.UseTextAlignment = False
        Me.INDlblAddress.Text = "Dirección Cra 7 No. 11 - 65 Teléfono 8724100 Ext.1372 - Neiva - Huila Pagina 1 de" &
    " 1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.INDlblAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'INDlblPhoneEmail
        '
        Me.INDlblPhoneEmail.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblPhoneEmail.LocationFloat = New DevExpress.Utils.PointFloat(8.3815!, 50.0!)
        Me.INDlblPhoneEmail.Multiline = True
        Me.INDlblPhoneEmail.Name = "INDlblPhoneEmail"
        Me.INDlblPhoneEmail.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblPhoneEmail.SizeF = New System.Drawing.SizeF(1013.237!, 16.4584!)
        Me.INDlblPhoneEmail.StylePriority.UseFont = False
        Me.INDlblPhoneEmail.StylePriority.UseTextAlignment = False
        Me.INDlblPhoneEmail.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.INDlblPhoneEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel21
        '
        Me.XrLabel21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TotalUnemployed")})
        Me.XrLabel21.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(925.4708!, 9.999974!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(104.5291!, 17.0!)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:$ #,##}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel21.Summary = XrSummary1
        Me.XrLabel21.Text = "XrLabel21"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel23
        '
        Me.XrLabel23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "UnemployedInterestTotal")})
        Me.XrLabel23.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(816.7959!, 9.999974!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(108.6748!, 17.0!)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:$ #,##}"
        XrSummary2.IgnoreNullValues = True
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel23.Summary = XrSummary2
        Me.XrLabel23.Text = "XrLabel23"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(508.0671!, 9.999974!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(198.9914!, 17.0!)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "Total:"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel27, Me.XrLine1, Me.XrLabel24, Me.XrLabel21, Me.XrLabel23})
        Me.GroupFooter1.HeightF = 40.625!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel27
        '
        Me.XrLabel27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BasicSalary")})
        Me.XrLabel27.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(707.0585!, 9.999974!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(109.7374!, 17.0!)
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.StylePriority.UseTextAlignment = False
        XrSummary3.FormatString = "{0:$ #,##}"
        XrSummary3.IgnoreNullValues = True
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel27.Summary = XrSummary3
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine1
        '
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(8.109856!, 3.749974!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(1011.89!, 2.083359!)
        '
        'rptUnemployedLiquidation
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.GroupHeaderInformation, Me.ReportFooter, Me.GroupFooter1})
        Me.Bookmark = "Reporte Liquidacion de Cesantias"
        Me.DataSource = Me.BindingSource1
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(35, 35, 29, 26)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "20.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents INDLblNombreEmpresaCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNombreReporte As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents GroupHeaderInformation As DevExpress.XtraReports.UI.GroupHeaderBand
    Private WithEvents xrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents lblAñoLiquidacion As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents INDlblAddress As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblPhoneEmail As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
End Class
