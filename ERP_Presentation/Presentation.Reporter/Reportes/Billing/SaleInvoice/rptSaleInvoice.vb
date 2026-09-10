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

#End Region

Public Class rptSaleInvoice
    Implements IReport, IReferenceToReport

    Dim dictionarySum As New Dictionary(Of Integer, Integer)
    Dim totalSum As Decimal = 0

    Public Sub New()
        InitializeComponent()

        Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(Convert.ToInt32(GetCurrentColumnValue("OperatingUnitId")))
        INDPrNit.Value = IndigoSessionValues.IndigoCompanyNit
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblCompanyPageHeader.Text = INDLblCompany.Text
        INDLblNitCompanyPageHeader.Text = INDLblNitCompany.Text
        InvoiceIvaReportId.Value = 1

        'carga la definicion personalizada del reporte, si este viene del rptSubSaleInvoiceAll
        If Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _tag = Me.Tag
            Dim Name = Me.GetType().Name
            Dim nameRepDefault As String = If(File.Exists(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default")), File.ReadAllText(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default"))?.Trim(), "*")
            Dim pattern As String = $"{_tag}.{Name}.{nameRepDefault}.repx"
            Dim reportCustomer = Directory.GetFiles(ConfigurationFile.Instance.ReportsPath, pattern, SearchOption.TopDirectoryOnly)?.FirstOrDefault
            If reportCustomer IsNot Nothing Then
                Me.LoadLayout(reportCustomer)
            End If
        End If
    End Sub

#Region "Properties"

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Const CNameReport = "Facturacion.CtrFolio"
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptSaleInvoice.CNameReport
        End Get
    End Property

    Public WriteOnly Property SetValueCodingServices As Integer
        Set(value As Integer)
            If Me.Parameters.Count > 0 Then
                Me.Parameters("INDprCodingServices").Value = value
            End If
        End Set
    End Property
    ''' <summary>
    ''' Abreviación de la moneda
    ''' </summary>
    Dim CurrencyAbbreviation As String

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoice)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Sub CargarDataSource2() Implements IReferenceToReport.CargarDataSource2
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoice)(Nothing, filtroConsulta)
        Catch ex As Exception
            Throw ex
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

    ''' <summary>
    ''' Función que calcula los extendidos del reporte 
    ''' para evitar que se queden pegados con la customización
    ''' </summary>
    Private Sub CalculateOtherValues()
        'Se consulta si al menos uno tiene iva para mostrar la sección respectiva

        Dim idInvoice As Integer = DirectCast(Me.GetCurrentColumnValue("Id"), Integer)
        Dim dataa As BillingVReportInvoice = TryCast(Me.DataSource, List(Of BillingVReportInvoice))?.Where(Function(b) b.Id = idInvoice).FirstOrDefault()

        'Valor Subtotal del paciente
        Dim detail = TryCast(dataa.BillingVReportInvoiceDetail.ToList(), List(Of BillingVReportInvoiceDetail)).Any(Function(x) x.IvaPercentage IsNot Nothing)
        Dim subTotalValue As Decimal = dataa.SubTotalService
        If detail Then
            Dim totalIva = Math.Round(TryCast(dataa.BillingVReportInvoiceDetail.ToList(), List(Of BillingVReportInvoiceDetail)).Where(Function(x) x.IvaTotalValue > 0).Sum(Function(x) x.IvaTotalValue * x.InvoicedQuantity), 2)
            subTotalValue = Math.Round(subTotalValue - totalIva, 2)

            IvaTotalAllCell1.Text = Utils.GetMoneyWithISO4217(totalIva, CurrencyAbbreviation)
            XrTableCell162.Visible = True
            GroupIvaDetailFooter.Visible = True
            XrSubreport1.Visible = (totalIva > 0)
        End If
        dataa.SubtotalPatient = Utils.GetMoneyWithISO4217(subTotalValue, CurrencyAbbreviation)

        'Valor Subtotal en Letras
        Dim _integerPart As Long = CLng(Int(CDbl(GetCurrentColumnValue("ThirdPartySalesValue"))))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("ThirdPartySalesValue")) - _integerPart, "0.00"), 2)
        dataa.SubtotalLetters = String.Format("{0} {1}{2}",
                                                 Utils.Num2Text(_integerPart).ToString, IndigoSessionValues.CurrencyISO4217,
                                                 If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString} {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))

        'Usuario de Impresión
        Dim rewriteUserPrint = String.IsNullOrEmpty(IndigoSessionValues.UserIndigo)
        Dim userCode = If(rewriteUserPrint, GetCurrentColumnValue("UserCode"), IndigoSessionValues.UserIndigo)
        Dim userName = If(rewriteUserPrint, GetCurrentColumnValue("FullNameUser"), IndigoSessionValues.UserIndigoName)
        dataa.UserPrint = "Usuario Impresión : " & userCode & " - " & userName
    End Sub

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
        CurrencyAbbreviation = IndigoSessionValues.CurrencyISO4217
        If Me.DataSource IsNot Nothing AndAlso Me.DataSource.Count > 0 AndAlso Not String.IsNullOrEmpty(Me.DataSource(0)?.CurrencyId?.Abbreviation) Then
            _culture = CultureInfo.CurrentCulture.Clone()
            CurrencyAbbreviation = Me.DataSource?(0).CurrencyId.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        CalculateOtherValues()

        'Codigo QR de Facturas Electrónicas
        If String.IsNullOrEmpty(GetCurrentColumnValue("CUFE")) Then
            INDTrPayments.Visible = False
            INDTrStatus.Visible = False
            INDTrCUFE.Visible = False
        Else
            INDTrPayments.Visible = True
            INDTrStatus.Visible = True
            INDTrCUFE.Visible = True

            INDBcBarcode.Symbology = New QRCodeGenerator()
            CType(INDBcBarcode.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte
            INDBcBarcode.HeightF = 140
        End If

        'Si el tipo de grupo de atención maneja contrato
        If GetCurrentColumnValue("CareGroupType") = 1 Then
            INDtrCustomerContract.Visible = True
        Else
            INDtrCustomerContract.Visible = False
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
                INDXrtHeaderDetail.Visible = False
                INDXrtHeaderDetailWithoutDate.Visible = True
                INDXrtDetail.Visible = False
                INDXrtDetailWithoutDate.Visible = True
            Else
                INDXrtHeaderDetail.Visible = True
                INDXrtHeaderDetailWithoutDate.Visible = False
                INDXrtDetail.Visible = True
                INDXrtDetailWithoutDate.Visible = False
            End If
        End If

        'tamaño de impresion
        Dim reporte As XtraReport = (CType(sender, XtraReport))
        If INDPrSizePage.Value = 2 Then
            reporte.PaperKind = Printing.PaperKind.A5Rotated
            reporte.Margins.Left = 14
            reporte.Margins.Right = 12

            INDGHCustomer.Visible = False
            INDGfTypeNotPatient.Visible = False
            INDGfTypePrintHalfLetter.Visible = True
            INDGfTypePatient.Visible = False
        Else
            reporte.PaperKind = Printing.PaperKind.Letter
            reporte.Margins.Left = 14
            reporte.Margins.Right = 12

            INDGHCustomer.Visible = True
            INDGfTypePrintHalfLetter.Visible = False

            If GetCurrentColumnValue("CareGroupType") = 3 Then
                INDTrCustomerHeader.Visible = False
                INDTrCustomerAddress.Visible = False
                INDtrCustomerEAPB.Visible = False

                INDGfTypeNotPatient.Visible = False
                INDGfTypePatient.Visible = True
            Else
                INDTrCustomerHeader.Visible = True
                INDTrCustomerAddress.Visible = True
                INDtrCustomerEAPB.Visible = True

                INDGfTypeNotPatient.Visible = True
                INDGfTypePatient.Visible = False
            End If
        End If

        'Mostrar la firma del funcionario
        If INDFirms.Value = 1 Then
            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & GetCurrentColumnValue("UserCode") & "'")
            If INDListUser IsNot Nothing Then
                Dim INDListPerson = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of PersonXpo)(Nothing, "Id = " & CType(INDListUser(0), UserXpo).IdPerson.Id)
                If INDListPerson IsNot Nothing Then
                    Dim INDListThirdParty = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCollection(Of ThirdPartyXpo)(Nothing, "Nit = '" & CType(INDListPerson(0), PersonXpo).Identification & "'")
                    If INDListThirdParty IsNot Nothing AndAlso INDListThirdParty(0).DigitalSignature IsNot Nothing Then
                        Dim mem As MemoryStream = New MemoryStream(INDListThirdParty(0).DigitalSignature)
                        INDXrpbFirm.Image = New Bitmap(mem)
                        INDXrpbFirm.Visible = True
                    Else
                        MessageIndigo.Show("Funcionario sin firma digitalizada en el sistema", MessageType.Information, Me.Text)
                    End If
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' funcion para cargar la definicion customizada de los subreportes
    ''' </summary>
    ''' <param name="_tag"></param>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    Public Function LoadCustomLayout(_tag As Object, Name As String) As String
        Dim nameRepDefault As String = If(File.Exists(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default")), File.ReadAllText(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default"))?.Trim(), "*")
        Dim pattern As String = $"{_tag}.{Name}.{nameRepDefault}.repx"
        Return Directory.GetFiles(ConfigurationFile.Instance.ReportsPath, pattern, SearchOption.TopDirectoryOnly)?.FirstOrDefault
    End Function

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