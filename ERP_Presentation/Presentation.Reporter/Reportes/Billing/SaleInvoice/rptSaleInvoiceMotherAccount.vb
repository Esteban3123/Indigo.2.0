#Region "Imports"

Imports System.Drawing.Printing
Imports System.IO
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.Base
Imports System.Globalization
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class rptSaleInvoiceMotherAccount
    Implements IReport

    Dim dictionarySum As New Dictionary(Of Integer, Integer)
    Dim totalSum As Decimal = 0

#Region "Properties"

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Const CNameReport = "Facturacion.CtrFolio"
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptSaleInvoiceMotherAccount.CNameReport
        End Get
    End Property

    Public WriteOnly Property SetValueCodingServices As Integer
        Set(value As Integer)
            If Me.Parameters.Count > 0 Then
                Me.Parameters("INDprCodingServices").Value = value
            End If
        End Set
    End Property

    Private _iNEMPRESU As INEMPRESU

    Private VisibleConditionalSales As Boolean
#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoice)(Nothing, filtroConsulta)
            Me._iNEMPRESU = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetXPOObject(Of INEMPRESU)(Nothing)

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

#End Region

#Region "Methods"

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#Region "Events"
    Dim _culture As CultureInfo
    Private Sub rptSaleInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        dictionarySum = New Dictionary(Of Integer, Integer)

        Dim ParametrosFilter As ParameterCollection = Me?.Parameters

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            ParametrosReporte = New Object() {ParametrosFilter("INDIdInvoiceSubreport").Value}
            CargarDataSource()
        End If

        Dim CurrencyAbbreviation As String = IndigoSessionValues.CurrencyISO4217

        If Me.DataSource IsNot Nothing AndAlso Not String.IsNullOrEmpty(TryCast(Me.DataSource, List(Of BillingVReportInvoice))?.FirstOrDefault()?.CurrencyId?.Abbreviation) Then
            _culture = CultureInfo.CurrentCulture.Clone()
            CurrencyAbbreviation = Me.DataSource?(0).CurrencyId.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        ''Se consulta si al menos uno tiene iva para mostrar la sección respectiva
        Dim dataa As BillingVReportInvoice = TryCast(Me.DataSource, List(Of BillingVReportInvoice))?.FirstOrDefault()

        If dataa IsNot Nothing Then
            Dim subTotalValue As Decimal = dataa.SubTotalService
            If TryCast(dataa.BillingVReportInvoiceDetail.ToList(), List(Of BillingVReportInvoiceDetail)).Any(Function(x) x.IvaPercentage IsNot Nothing) Then
                GroupIvaDetailFooter.Visible = True
            End If
        End If

        'Condicion de ventas' 
        'Verificar si hay al menos 3 elementos en ParametrosReporte (índice 2) debido a que asi se esta enviando desde ctrfolio
        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte.Count > 2 Then
            VisibleConditionalSales = ParametrosReporte(2)
        Else
            VisibleConditionalSales = ParametrosFilter("INDrequiresConditionsSale").Value
        End If

        If VisibleConditionalSales And Not String.IsNullOrEmpty(GetCurrentColumnValue("ConditionSalesCodeName")) Then
            XrTableCell128.Visible = True
            INDtcConditionSales.Visible = True
        Else
            XrTableCell128.Visible = False
            INDtcConditionSales.Visible = False
        End If

        'Codigo QR de Facturas Electrónicas
        If String.IsNullOrEmpty(GetCurrentColumnValue("CUFE")) Then
            INDTrStatus_Visible(False)

            INDTrCUFE.Visible = False
            XrLabel9.Visible = False

            INDBcBarcode.Symbology = New Code128Generator()
            INDBcBarcode.HeightF = 20
        Else
            INDTrStatus_Visible(True)

            INDTrCUFE.Visible = True
            XrLabel9.Visible = True

            INDBcBarcode.Symbology = New QRCodeGenerator()
            CType(INDBcBarcode.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte
            INDBcBarcode.HeightF = 140
        End If

        'Agrupar o detallar el reporte
        If INDPrTypeReport.Value = 2 Then
            ReportHeader1.Visible = False
            INDGHDetail.Visible = False
            Detail1.Visible = False
        Else
            ReportHeader1.Visible = True
            INDGHDetail.Visible = True
            Detail1.Visible = True

            If INDPrGroupServices.Value = 1 AndAlso INDPrGroupProducts.Value = 1 Then
                INDXrtDetail.Visible = False
            Else
                INDXrtDetail.Visible = True
            End If
        End If

        If INDPrGroupServices.Value = 1 AndAlso INDPrGroupProducts.Value = 1 Then
            INDXrtDetail.Visible = False
            INDXrtDetailWithoutDate.Visible = True
        Else
            INDXrtDetail.Visible = True
            INDXrtDetailWithoutDate.Visible = False
        End If

        Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(Convert.ToInt32(GetCurrentColumnValue("OperatingUnitId")))
        INDPrNit.Value = IndigoSessionValues.IndigoCompanyNit
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblCompanyPageHeader.Text = INDLblCompany.Text
        INDLblNitCompanyPageHeader.Text = INDLblNitCompany.Text
        InvoiceIvaReportId.Value = 2
        Dim totalValue As Decimal = Convert.ToDecimal(GetCurrentColumnValue("ThirdPartySalesValue")) - Convert.ToDecimal(GetCurrentColumnValue("TaxDevolutionValue"))
        Dim _integerPart As Integer = Int(totalValue)
        Dim _decimalPart As Integer = Strings.Right(Format(totalValue - _integerPart, "0.00"), 2)
        Dim currencyName As String = If(TryCast(Me.DataSource, List(Of BillingVReportInvoice))?.FirstOrDefault()?.CurrencyId?.ISO4217Xpo?.CurrencyName IsNot Nothing, TryCast(Me.DataSource, List(Of BillingVReportInvoice))?.FirstOrDefault()?.CurrencyId?.ISO4217Xpo?.CurrencyName, IndigoSessionValues.CurrencyISO4217)
        INDPrValueInLetters.Value = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString, currencyName.ToUpper(),
                                                    If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString} {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))

        Dim rewriteUserPrint = String.IsNullOrEmpty(IndigoSessionValues.UserIndigo)
        Dim userCode = If(rewriteUserPrint, GetCurrentColumnValue("UserCode"), IndigoSessionValues.UserIndigo)
        Dim userName = If(rewriteUserPrint, GetCurrentColumnValue("FullNameUser"), IndigoSessionValues.UserIndigoName)
        INDUserImp.Text = "Usuario Impresión : " & userCode & " - " & userName

        If _iNEMPRESU IsNot Nothing AndAlso Not String.IsNullOrEmpty(_iNEMPRESU.RouteLogoReportLeft) Then
            XrPictureBox3.ImageUrl = _iNEMPRESU.RouteLogoReportLeft
            XrPictureBox1.ImageUrl = _iNEMPRESU.RouteLogoReportLeft
        End If

        If _iNEMPRESU IsNot Nothing AndAlso Not String.IsNullOrEmpty(_iNEMPRESU.RouteLogoReportRight) Then
            XrPictureBox4.ImageUrl = _iNEMPRESU.RouteLogoReportRight
            XrPictureBox2.ImageUrl = _iNEMPRESU.RouteLogoReportRight
        End If

        'carga la definicion personalizada del reporte, si este viene del rptSubSaleInvoiceAll
        If ParametrosFilter?("INDfromSaleInvoiceAll")?.Value AndAlso Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _classRpt = New rptSubSaleInvoiceAll()
            Dim reportCustomer = _classRpt.LoadCustomLayout(Me.Tag, Me.GetType().Name)
            If reportCustomer IsNot Nothing Then
                Me.LoadLayout(reportCustomer)
            End If
        End If
    End Sub

    Private Sub INDTrStatus_Visible(Visible As Boolean)
        INDtcStatusDIAN.Visible = Visible
        XrTableCell27.Visible = Visible

        If Not VisibleConditionalSales And Not Visible Then
            INDTrStatus.Visible = False
        End If
    End Sub

    Private Sub XrTableRow18_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow18.BeforePrint
        If String.IsNullOrEmpty(GetCurrentColumnValue("Observation")) Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub XrTable7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable7.BeforePrint
        If DetailReport.GetCurrentColumnValue("SurgicalId") Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub XrTableRow45_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow45.BeforePrint
        Dim currentRow As BillingVReportInvoice = TryCast(Me.GetCurrentRow(), BillingVReportInvoice)
        If currentRow IsNot Nothing AndAlso currentRow.IsMasterAccount = 2 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub
#End Region

#Region "Summary Events"

    Private Sub XrTableCell94_SummaryRowChanged(sender As Object, e As EventArgs) Handles XrTableCell94.SummaryRowChanged
        If Not dictionarySum.ContainsKey(GetCurrentColumnValue("invoiceDetailId")) Then
            totalSum += Convert.ToDecimal(GetCurrentColumnValue("INDTotalGroup"))
            dictionarySum.Add(GetCurrentColumnValue("invoiceDetailId"), GetCurrentColumnValue("invoiceDetailId"))
        End If
    End Sub

    Private Sub XrTableCell94_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell94.SummaryGetResult
        e.Result = totalSum
        e.Handled = True
    End Sub

    Private Sub XrTableCell94_SummaryReset(sender As Object, e As EventArgs) Handles XrTableCell94.SummaryReset
        totalSum = 0
    End Sub

#End Region

End Class