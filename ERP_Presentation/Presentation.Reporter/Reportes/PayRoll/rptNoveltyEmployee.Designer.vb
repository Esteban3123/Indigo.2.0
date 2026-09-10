<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptNoveltyEmployee
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblTipoNovedad = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblEstado = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.INDLblNombreReporte = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNombreEmpresaCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.INDlblAddress = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblPhoneEmail = New DevExpress.XtraReports.UI.XRLabel()
        Me.rptHCMedicamentosControl = New DevExpress.XtraReports.UI.XtraReport()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rptHCMedicamentosControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel7, Me.lblTipoNovedad, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.INDlblEstado})
        Me.Detail.HeightF = 19.79167!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Reason")})
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(579.3747!, 0.0!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(441.5706!, 17.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = "XrLabel7"
        '
        'lblTipoNovedad
        '
        Me.lblTipoNovedad.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTipoNovedad.LocationFloat = New DevExpress.Utils.PointFloat(240.2082!, 0.0!)
        Me.lblTipoNovedad.Name = "lblTipoNovedad"
        Me.lblTipoNovedad.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblTipoNovedad.SizeF = New System.Drawing.SizeF(205.8334!, 17.0!)
        Me.lblTipoNovedad.StylePriority.UseFont = False
        Me.lblTipoNovedad.StylePriority.UseTextAlignment = False
        Me.lblTipoNovedad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Days")})
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(165.8332!, 0.0!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(74.37491!, 17.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "XrLabel4"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EndDate", "{0:dd/MM/yyyy}")})
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(87.29147!, 0.0!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(78.54163!, 17.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RealDate", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 0.0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(77.08328!, 17.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'INDlblEstado
        '
        Me.INDlblEstado.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblEstado.LocationFloat = New DevExpress.Utils.PointFloat(446.0416!, 0.0!)
        Me.INDlblEstado.Name = "INDlblEstado"
        Me.INDlblEstado.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblEstado.SizeF = New System.Drawing.SizeF(135.3048!, 17.0!)
        Me.INDlblEstado.StylePriority.UseFont = False
        Me.INDlblEstado.StylePriority.UseTextAlignment = False
        Me.INDlblEstado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 29.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 24.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.HeightF = 4.166667!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblNombreReporte, Me.INDLblNitCliente, Me.INDLblNombreEmpresaCliente, Me.XrLine4, Me.XrPageInfo1, Me.XrPageInfo2, Me.XrLabel22})
        Me.PageHeader.Name = "PageHeader"
        '
        'INDLblNombreReporte
        '
        Me.INDLblNombreReporte.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreReporte.LocationFloat = New DevExpress.Utils.PointFloat(9.054779!, 47.5197!)
        Me.INDLblNombreReporte.LockedInUserDesigner = True
        Me.INDLblNombreReporte.Name = "INDLblNombreReporte"
        Me.INDLblNombreReporte.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreReporte.SizeF = New System.Drawing.SizeF(1011.811!, 25.19684!)
        Me.INDLblNombreReporte.StylePriority.UseFont = False
        Me.INDLblNombreReporte.StylePriority.UseTextAlignment = False
        Me.INDLblNombreReporte.Text = "NOVEDADES EMPLEADO"
        Me.INDLblNombreReporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCliente
        '
        Me.INDLblNitCliente.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.INDLblNitCliente.LocationFloat = New DevExpress.Utils.PointFloat(9.054779!, 30.98425!)
        Me.INDLblNitCliente.Name = "INDLblNitCliente"
        Me.INDLblNitCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCliente.SizeF = New System.Drawing.SizeF(1011.811!, 16.53543!)
        Me.INDLblNitCliente.StylePriority.UseFont = False
        Me.INDLblNitCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNitCliente.Text = "800000100-0"
        Me.INDLblNitCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNombreEmpresaCliente
        '
        Me.INDLblNombreEmpresaCliente.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.INDLblNombreEmpresaCliente.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreEmpresaCliente.LocationFloat = New DevExpress.Utils.PointFloat(9.054779!, 4.999996!)
        Me.INDLblNombreEmpresaCliente.Name = "INDLblNombreEmpresaCliente"
        Me.INDLblNombreEmpresaCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreEmpresaCliente.SizeF = New System.Drawing.SizeF(1011.811!, 25.19685!)
        Me.INDLblNombreEmpresaCliente.StylePriority.UseFont = False
        Me.INDLblNombreEmpresaCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNombreEmpresaCliente.Text = "Vie HealtTech"
        Me.INDLblNombreEmpresaCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLine4
        '
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 75.91661!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(1010.737!, 2.083359!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Format = "Página {0}/{1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(937.7948!, 78.07088!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(83.07086!, 16.92913!)
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Format = "{0:dddd, dd' de 'MMMM' de 'yyyy hh:mm tt}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(675.316!, 77.99997!)
        Me.XrPageInfo2.LockedInUserDesigner = True
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(264.5833!, 17.00001!)
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(547.5244!, 77.99997!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(127.7917!, 17.00001!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Fecha de Impresión:"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel8, Me.XrLabel17, Me.XrLabel9, Me.XrLabel16, Me.XrLabel14, Me.xrLabel10, Me.xrLabel11, Me.xrLabel1, Me.xrLabel12, Me.xrLabel13, Me.xrLabel5, Me.xrLabel15, Me.xrLabel6})
        Me.GroupHeader1.HeightF = 143.125!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 43.89588!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(143.43!, 17.0!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "Numero Documento :"
        '
        'XrLabel17
        '
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(448.5413!, 125.0833!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(135.3051!, 17.0!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "Estado"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EmployeeId.Payroll_Contract.Id")})
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(153.638!, 77.8958!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(303.0!, 17.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "XrLabel9"
        '
        'XrLabel16
        '
        Me.XrLabel16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EmployeeId.ThirdPartyId.Name")})
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(153.6381!, 60.89585!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(303.0!, 17.0!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.Text = "XrLabel16"
        '
        'XrLabel14
        '
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EmployeeId.ThirdPartyId.Nit")})
        Me.XrLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(153.6381!, 43.89585!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(303.0!, 17.0!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.Text = "XrLabel14"
        '
        'xrLabel10
        '
        Me.xrLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 125.0833!)
        Me.xrLabel10.Name = "xrLabel10"
        Me.xrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel10.SizeF = New System.Drawing.SizeF(79.58324!, 17.0!)
        Me.xrLabel10.StylePriority.UseFont = False
        Me.xrLabel10.StylePriority.UseTextAlignment = False
        Me.xrLabel10.Text = "Fecha Inicio"
        Me.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'xrLabel11
        '
        Me.xrLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(89.79136!, 125.0833!)
        Me.xrLabel11.Name = "xrLabel11"
        Me.xrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel11.SizeF = New System.Drawing.SizeF(78.54156!, 17.0!)
        Me.xrLabel11.StylePriority.UseFont = False
        Me.xrLabel11.StylePriority.UseTextAlignment = False
        Me.xrLabel11.Text = "Fecha Final"
        Me.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'xrLabel1
        '
        Me.xrLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(583.8463!, 125.0833!)
        Me.xrLabel1.Name = "xrLabel1"
        Me.xrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel1.SizeF = New System.Drawing.SizeF(441.5705!, 17.0!)
        Me.xrLabel1.StylePriority.UseFont = False
        Me.xrLabel1.StylePriority.UseTextAlignment = False
        Me.xrLabel1.Text = "Descripcion"
        Me.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'xrLabel12
        '
        Me.xrLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(242.7078!, 125.0833!)
        Me.xrLabel12.Name = "xrLabel12"
        Me.xrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel12.SizeF = New System.Drawing.SizeF(205.8334!, 17.0!)
        Me.xrLabel12.StylePriority.UseFont = False
        Me.xrLabel12.StylePriority.UseTextAlignment = False
        Me.xrLabel12.Text = "Tipo Novedad"
        Me.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'xrLabel13
        '
        Me.xrLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(168.3329!, 125.0833!)
        Me.xrLabel13.Name = "xrLabel13"
        Me.xrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel13.SizeF = New System.Drawing.SizeF(74.37491!, 17.0!)
        Me.xrLabel13.StylePriority.UseFont = False
        Me.xrLabel13.StylePriority.UseTextAlignment = False
        Me.xrLabel13.Text = "Dias"
        Me.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'xrLabel5
        '
        Me.xrLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 60.89579!)
        Me.xrLabel5.Name = "xrLabel5"
        Me.xrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel5.SizeF = New System.Drawing.SizeF(143.43!, 17.0!)
        Me.xrLabel5.StylePriority.UseFont = False
        Me.xrLabel5.Text = "Nombre :"
        '
        'xrLabel15
        '
        Me.xrLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(10.20813!, 77.8958!)
        Me.xrLabel15.Name = "xrLabel15"
        Me.xrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel15.SizeF = New System.Drawing.SizeF(143.4297!, 17.0!)
        Me.xrLabel15.StylePriority.UseFont = False
        Me.xrLabel15.Text = "Codigo Contrato :"
        '
        'xrLabel6
        '
        Me.xrLabel6.BackColor = System.Drawing.Color.Gainsboro
        Me.xrLabel6.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.xrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(9.999974!, 10.00001!)
        Me.xrLabel6.Name = "xrLabel6"
        Me.xrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel6.SizeF = New System.Drawing.SizeF(1010.866!, 17.0!)
        Me.xrLabel6.StylePriority.UseBackColor = False
        Me.xrLabel6.StylePriority.UseFont = False
        Me.xrLabel6.Text = "INFORMACION EMPLEADO"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PayrollRepository.PayrollNovelty)
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDlblAddress, Me.INDlblPhoneEmail})
        Me.ReportFooter.HeightF = 53.125!
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.PrintAtBottom = True
        '
        'INDlblAddress
        '
        Me.INDlblAddress.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblAddress.LocationFloat = New DevExpress.Utils.PointFloat(7.708231!, 9.999974!)
        Me.INDlblAddress.Multiline = True
        Me.INDlblAddress.Name = "INDlblAddress"
        Me.INDlblAddress.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblAddress.SizeF = New System.Drawing.SizeF(1013.237!, 16.4584!)
        Me.INDlblAddress.StylePriority.UseFont = False
        Me.INDlblAddress.StylePriority.UseTextAlignment = False
        Me.INDlblAddress.Text = "Dirección Cra 7 No. 11 - 65 Teléfono 8724100 Ext.1372 - Neiva - Huila Pagina 1 de" & _
    " 1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.INDlblAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'INDlblPhoneEmail
        '
        Me.INDlblPhoneEmail.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblPhoneEmail.LocationFloat = New DevExpress.Utils.PointFloat(7.708231!, 26.66658!)
        Me.INDlblPhoneEmail.Multiline = True
        Me.INDlblPhoneEmail.Name = "INDlblPhoneEmail"
        Me.INDlblPhoneEmail.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblPhoneEmail.SizeF = New System.Drawing.SizeF(1013.237!, 16.4584!)
        Me.INDlblPhoneEmail.StylePriority.UseFont = False
        Me.INDlblPhoneEmail.StylePriority.UseTextAlignment = False
        Me.INDlblPhoneEmail.Text = "E-mail: AuditorMedico@medilaser.com.co"
        Me.INDlblPhoneEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'rptHCMedicamentosControl
        '
        Me.rptHCMedicamentosControl.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.GroupHeader1, Me.ReportFooter})
        Me.rptHCMedicamentosControl.Bookmark = "Medicamentos de Control"
        Me.rptHCMedicamentosControl.Margins = New System.Drawing.Printing.Margins(29, 26, 29, 24)
        Me.rptHCMedicamentosControl.Name = "rptHCMedicamentosControl"
        Me.rptHCMedicamentosControl.PageHeight = 1100
        Me.rptHCMedicamentosControl.PageWidth = 850
        Me.rptHCMedicamentosControl.Version = "13.1"
        '
        'rptNoveltyEmployee
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.GroupHeader1, Me.ReportFooter})
        Me.DataSource = Me.BindingSource1
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(35, 35, 29, 24)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "13.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rptHCMedicamentosControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents INDLblNombreReporte As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNombreEmpresaCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents lblTipoNovedad As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Private WithEvents INDlblEstado As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents INDlblAddress As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblPhoneEmail As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents rptHCMedicamentosControl As DevExpress.XtraReports.UI.XtraReport
End Class
