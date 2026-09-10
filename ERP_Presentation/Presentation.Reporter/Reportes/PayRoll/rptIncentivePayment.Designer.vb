<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptIncentivePayment
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.INDLblNombreReporte = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNombreEmpresaCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDLblNitCliente = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblFechaInicio = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.INDlblAddress = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblPhoneEmail = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.INDlblFechaFin = New DevExpress.XtraReports.UI.XRLabel()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel19, Me.XrLabel18, Me.XrLabel17, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel11, Me.XrLabel9, Me.XrLabel20})
        Me.Detail.HeightF = 17.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel19
        '
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "WorkingDays")})
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(857.9585!, 0.0!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(64.99994!, 17.0!)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SanctionDays")})
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(803.3751!, 0.0!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(54.58331!, 17.0!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TransportHelpValue", "{0:$ #,##}")})
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(696.7084!, 0.0!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.NullValueText = "-"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(106.6667!, 17.0!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "XrLabel17"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel14
        '
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AverageSalary", "{0:$ #,##}")})
        Me.XrLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(590.0417!, 0.0!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(106.6666!, 17.0!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.Text = "XrLabel14"
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BasicSalary", "{0:$ #,##}")})
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(483.3751!, 0.0!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(106.6667!, 17.0!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.Text = "XrLabel13"
        '
        'XrLabel12
        '
        Me.XrLabel12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ContractId.ContractInitialDate", "{0:dd/MM/yyyy}")})
        Me.XrLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(361.0834!, 0.0!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(122.2917!, 17.0!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "XrLabel12"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ContractId.EmployeeId.ThirdPartyId.Name")})
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(103.5418!, 0.0!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(257.5416!, 17.0!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "XrLabel11"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ContractId.EmployeeId.ThirdPartyId.Nit")})
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(103.5418!, 17.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel20
        '
        Me.XrLabel20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PaidValue", "{0:$ #,##}")})
        Me.XrLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(923.3334!, 0.0!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(106.6665!, 17.0!)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "XrLabel20"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        Me.ReportHeader.HeightF = 39.58333!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDLblNombreReporte, Me.INDLblNombreEmpresaCliente, Me.INDLblNitCliente, Me.XrLabel22, Me.XrPageInfo2, Me.XrPageInfo1, Me.XrLine4})
        Me.PageHeader.Name = "PageHeader"
        '
        'INDLblNombreReporte
        '
        Me.INDLblNombreReporte.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreReporte.LocationFloat = New DevExpress.Utils.PointFloat(8.295278!, 43.99681!)
        Me.INDLblNombreReporte.LockedInUserDesigner = True
        Me.INDLblNombreReporte.Name = "INDLblNombreReporte"
        Me.INDLblNombreReporte.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreReporte.SizeF = New System.Drawing.SizeF(1011.811!, 25.19684!)
        Me.INDLblNombreReporte.StylePriority.UseFont = False
        Me.INDLblNombreReporte.StylePriority.UseTextAlignment = False
        Me.INDLblNombreReporte.Text = "LIQUIDACION PRIMAS"
        Me.INDLblNombreReporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNombreEmpresaCliente
        '
        Me.INDLblNombreEmpresaCliente.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.INDLblNombreEmpresaCliente.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.INDLblNombreEmpresaCliente.LocationFloat = New DevExpress.Utils.PointFloat(8.295278!, 1.477112!)
        Me.INDLblNombreEmpresaCliente.Name = "INDLblNombreEmpresaCliente"
        Me.INDLblNombreEmpresaCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNombreEmpresaCliente.SizeF = New System.Drawing.SizeF(1011.811!, 25.19685!)
        Me.INDLblNombreEmpresaCliente.StylePriority.UseFont = False
        Me.INDLblNombreEmpresaCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNombreEmpresaCliente.Text = "Vie HealtTech"
        Me.INDLblNombreEmpresaCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'INDLblNitCliente
        '
        Me.INDLblNitCliente.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.INDLblNitCliente.LocationFloat = New DevExpress.Utils.PointFloat(8.295258!, 26.67395!)
        Me.INDLblNitCliente.Name = "INDLblNitCliente"
        Me.INDLblNitCliente.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDLblNitCliente.SizeF = New System.Drawing.SizeF(1012.716!, 16.53543!)
        Me.INDLblNitCliente.StylePriority.UseFont = False
        Me.INDLblNitCliente.StylePriority.UseTextAlignment = False
        Me.INDLblNitCliente.Text = "800000100-0"
        Me.INDLblNitCliente.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(548.3635!, 81.52288!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(127.7917!, 17.00001!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Fecha de Impresión:"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrPageInfo2.Format = "{0:dddd, dd' de 'MMMM' de 'yyyy hh:mm tt}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(676.1552!, 81.52288!)
        Me.XrPageInfo2.LockedInUserDesigner = True
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(264.5833!, 17.00001!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.XrPageInfo1.Format = "Página {0}/{1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(938.6339!, 81.59376!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(83.07086!, 16.92913!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        '
        'XrLine4
        '
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(8.295258!, 79.5104!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(1010.906!, 2.083359!)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.INDlblFechaFin, Me.XrLabel23, Me.XrLabel7, Me.XrLabel6, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel1, Me.INDlblFechaInicio, Me.xrLabel16, Me.xrLabel15, Me.xrLabel5, Me.xrLabel8, Me.xrLabel10, Me.XrLine2})
        Me.GroupHeader1.HeightF = 131.25!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(923.3328!, 107.2084!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(106.6667!, 17.00002!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "Valor Pagado"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(857.9579!, 91.49993!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(64.99994!, 32.70843!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Dias Trabajados"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(803.3746!, 91.49993!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(54.58331!, 32.70843!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "Dias Sancion"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(696.7079!, 107.2084!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(106.6667!, 17.00002!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Aux. Transporte"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(590.0411!, 107.2084!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(106.6667!, 17.00002!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "Salario Promedio"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(483.3746!, 107.2084!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(106.6667!, 17.00002!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Salario Basico"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'INDlblFechaInicio
        '
        Me.INDlblFechaInicio.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblFechaInicio.LocationFloat = New DevExpress.Utils.PointFloat(190.4164!, 1.833312!)
        Me.INDlblFechaInicio.Name = "INDlblFechaInicio"
        Me.INDlblFechaInicio.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblFechaInicio.SizeF = New System.Drawing.SizeF(206.7087!, 17.0!)
        Me.INDlblFechaInicio.StylePriority.UseFont = False
        Me.INDlblFechaInicio.StylePriority.UseTextAlignment = False
        Me.INDlblFechaInicio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLabel16
        '
        Me.xrLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 1.833312!)
        Me.xrLabel16.Name = "xrLabel16"
        Me.xrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel16.SizeF = New System.Drawing.SizeF(180.4164!, 17.0!)
        Me.xrLabel16.StylePriority.UseFont = False
        Me.xrLabel16.StylePriority.UseTextAlignment = False
        Me.xrLabel16.Text = "Fecha Inicio :"
        Me.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLabel15
        '
        Me.xrLabel15.BackColor = System.Drawing.Color.Gainsboro
        Me.xrLabel15.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.xrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 66.41664!)
        Me.xrLabel15.Name = "xrLabel15"
        Me.xrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel15.SizeF = New System.Drawing.SizeF(1020.079!, 17.0!)
        Me.xrLabel15.StylePriority.UseBackColor = False
        Me.xrLabel15.StylePriority.UseFont = False
        Me.xrLabel15.StylePriority.UseTextAlignment = False
        Me.xrLabel15.Text = "LISTADO EMPLEADOS LIQUIDACION"
        Me.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLabel5
        '
        Me.xrLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(103.5413!, 107.2084!)
        Me.xrLabel5.Name = "xrLabel5"
        Me.xrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel5.SizeF = New System.Drawing.SizeF(257.5416!, 17.0!)
        Me.xrLabel5.StylePriority.UseFont = False
        Me.xrLabel5.StylePriority.UseTextAlignment = False
        Me.xrLabel5.Text = "Nombre"
        Me.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel8
        '
        Me.xrLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(2.082888!, 91.49993!)
        Me.xrLabel8.Name = "xrLabel8"
        Me.xrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel8.SizeF = New System.Drawing.SizeF(101.4584!, 32.70842!)
        Me.xrLabel8.StylePriority.UseFont = False
        Me.xrLabel8.StylePriority.UseTextAlignment = False
        Me.xrLabel8.Text = "Numero Documento"
        Me.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLabel10
        '
        Me.xrLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.xrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(361.0829!, 107.2084!)
        Me.xrLabel10.Name = "xrLabel10"
        Me.xrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrLabel10.SizeF = New System.Drawing.SizeF(122.2917!, 17.00002!)
        Me.xrLabel10.StylePriority.UseFont = False
        Me.xrLabel10.StylePriority.UseTextAlignment = False
        Me.xrLabel10.Text = "Fecha Inicio Contrato"
        Me.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLine2
        '
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(2.082888!, 127.3333!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(1027.917!, 3.916679!)
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel24, Me.XrLabel21})
        Me.GroupFooter1.HeightF = 79.16666!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLine1
        '
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(2.083397!, 1.041667!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(1025.945!, 6.250064!)
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(566.8837!, 7.29173!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(236.4914!, 17.0!)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "Total:"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel21
        '
        Me.XrLabel21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PaidValue")})
        Me.XrLabel21.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(803.3751!, 7.29173!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(226.6248!, 17.0!)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:$ #,##}"
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel21.Summary = XrSummary1
        Me.XrLabel21.Text = "XrLabel21"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        Me.INDlblAddress.LocationFloat = New DevExpress.Utils.PointFloat(8.38147!, 33.5416!)
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
        Me.INDlblPhoneEmail.LocationFloat = New DevExpress.Utils.PointFloat(8.38147!, 50.0!)
        Me.INDlblPhoneEmail.Multiline = True
        Me.INDlblPhoneEmail.Name = "INDlblPhoneEmail"
        Me.INDlblPhoneEmail.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblPhoneEmail.SizeF = New System.Drawing.SizeF(1013.237!, 16.4584!)
        Me.INDlblPhoneEmail.StylePriority.UseFont = False
        Me.INDlblPhoneEmail.StylePriority.UseTextAlignment = False
        Me.INDlblPhoneEmail.Text = "E-mail: AuditorMedico@medilaser.com.co"
        Me.INDlblPhoneEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel23
        '
        Me.XrLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(8.29525!, 18.83332!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(180.4164!, 17.0!)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "Fecha Fin :"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'INDlblFechaFin
        '
        Me.INDlblFechaFin.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.INDlblFechaFin.LocationFloat = New DevExpress.Utils.PointFloat(188.7116!, 18.83332!)
        Me.INDlblFechaFin.Name = "INDlblFechaFin"
        Me.INDlblFechaFin.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.INDlblFechaFin.SizeF = New System.Drawing.SizeF(206.7087!, 17.0!)
        Me.INDlblFechaFin.StylePriority.UseFont = False
        Me.INDlblFechaFin.StylePriority.UseTextAlignment = False
        Me.INDlblFechaFin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.PayrollRepository.PayrollIncentivePayment)
        '
        'rptIncentivePayment
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.GroupHeader1, Me.GroupFooter1, Me.ReportFooter})
        Me.DataSource = Me.BindingSource1
        Me.Landscape = true
        Me.Margins = New System.Drawing.Printing.Margins(35, 35, 29, 24)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.Version = "13.1"
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit

End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents INDLblNombreReporte As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNombreEmpresaCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDLblNitCliente As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Private WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents INDlblFechaInicio As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents xrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Private WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblAddress As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents INDlblPhoneEmail As DevExpress.XtraReports.UI.XRLabel
    Private WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents INDlblFechaFin As DevExpress.XtraReports.UI.XRLabel
End Class
