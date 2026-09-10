#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.Drawing
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptCanceledInvoice
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Facturacion.CtrFolio"

    Dim dictionarySum As New Dictionary(Of Integer, Integer)

    Dim totalSum As Decimal = 0

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            If ParametrosReporte.Count > 1 Then
                INDprCodingServices.Value = ParametrosReporte(1)
            End If
            'se ovtiene los detalles
            Dim ListDetail = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.ListViewInvoiceDetailAnulateByInvoiceId(ParametrosReporte(0))
            Me.DataSource = ListDetail
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function LoadDatasource() As DevExpress.Xpo.XPCollection(Of BillingVReportInvoiceDetailAnulate)
        Try
            Return XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.ListViewInvoiceDetailAnulateByInvoiceId(ParametrosReporte(0))
        Catch ex As Exception
            'MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Return Nothing
        End Try
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function


    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptCanceledInvoice.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Function CalcularEdad(INDDateN As Date) As String
        Dim INDEdadY, INDEdadM, INDEdadD, INDYearR, INDMonthsR, INDDaysR, INDDaysMonthR, INDEdadYN, INDEdadMN, INDEdadDN As Integer

        INDDaysR = Now.Day
        INDMonthsR = Now.Month
        INDDaysMonthR = DateTime.DaysInMonth(Now.Year, Now.Month)
        INDYearR = Now.Year
        INDEdadD = Format(INDDateN, "dd")
        INDEdadM = Format(INDDateN, "MM")
        INDEdadY = Format(INDDateN, "yyyy")

        INDEdadDN = INDDaysR - INDEdadD

        If (INDEdadDN < 0) Then
            INDEdadMN = INDMonthsR - INDEdadM - 1
            INDEdadDN += INDDaysMonthR
        Else
            INDEdadMN = INDMonthsR - INDEdadM
        End If

        If (INDEdadMN < 0) Then
            INDEdadMN += 12
            INDEdadYN = INDYearR - INDEdadY - 1
        Else
            INDEdadYN = INDYearR - INDEdadY
        End If

        Return INDEdadYN & " Años / " & INDEdadMN & " Meses / " & INDEdadDN & " Días"
    End Function

    Private Sub rptSaleInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim reporte As XtraReport = (CType(sender, XtraReport))
        'cuando cambian los Parametros
        dictionarySum = New Dictionary(Of Integer, Integer)
        If INDPrTypeReport.Value = 2 Then
            GroupHeader3.Visible = False
            Detail.Visible = False
        Else
            If INDPrGroupProductsAndServices.Value = 1 Then
                XrTable15.Visible = True
                XrTable4.Visible = False

                XrTable5.Visible = False
                XrTable12.Visible = True
                XrTable9.Visible = True
            Else
                XrTable15.Visible = False
                XrTable4.Visible = True

                XrTable5.Visible = True
                XrTable12.Visible = False
                XrTable9.Visible = False
            End If
            GroupHeader3.Visible = True
            Detail.Visible = True
        End If

        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdInvoiceSubreport").Value}
            CargarDataSource()
        End If
        'se Obtiene la cabecera
        Dim filtroConsulta = "Id = " & ParametrosReporte(0)
        Dim ListHeader = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoice)(Nothing, filtroConsulta)
        If ListHeader.Count > 0 AndAlso ListHeader IsNot Nothing Then

            Dim item = ListHeader(0)
            XrLabel2.Text = item.InvoiceDate
            XrLabel17.Text = item.InvoiceDate
            'XrLabel15.Text = item.InvoiceExpirationDate
            If item.DocumentType = 5 Then
                XrLabel9.Text = "REGISTRO DE SERVICIOS N° " & item.InvoiceNumber
                XrLabel22.Text = "REGISTRO DE SERVICIOS N° " & item.InvoiceNumber
            Else
                XrLabel9.Text = "FACTURA DE VENTA N° " & item.InvoiceNumber
                XrLabel22.Text = "FACTURA DE VENTA N° " & item.InvoiceNumber
            End If
            XrTableCell2.Text = item.ThirdPartyAddress
            XrTableCell109.Text = item.ThirdPartyPhone
            XrTableCell81.Text = item.Name
            XrTableCell116.Text = item.Name
            XrTableCell73.Text = item.Nit
            XrTableCell114.Text = item.Nit
            XrTableCell33.Text = item.InvoiceCategory
            XrTableCell299.Text = item.InvoiceCategory
            XrTableCell78.Text = item.CareGroup
            INDTcDescriptionEAPB.Text = item.DescriptionHealthAdministrator
            XrTableCell6.Text = item.HealthEntityCode
            XrTableCell51.Text = item.Contract
            Dim status = item.Status
            If status = 1 Then
                XrTableCell32.Text = "FACTURADA"
                XrTableCell112.Text = "FACTURADA"
            Else
                XrTableCell32.Text = "ANULADA"
                XrTableCell112.Text = "ANULADA"
            End If
            XrTableCell10.Text = item.PatientName
            XrTableCell118.Text = item.PatientCode + " - " + item.PatientName
            Dim INDTipoPac = item.PatientType
            Select Case INDTipoPac
                Case 1
                    XrTableCell11.Text = "Contributivo"
                Case 2
                    XrTableCell11.Text = "Subsidiado"
                Case 3
                    XrTableCell11.Text = "Vinculado"
                Case 4
                    XrTableCell11.Text = "Particular"
                Case 5
                    XrTableCell11.Text = "Otro"
                Case 6
                    XrTableCell11.Text = "Desplazado Reg. Contributivo"
                Case 7
                    XrTableCell11.Text = "Desplazado Reg. Subsidiado"
                Case 8
                    XrTableCell11.Text = "Desplazado no Asegurado"
            End Select
            XrTableCell12.Text = item.AdmissionNumber
            XrTableCell14.Text = item.PatientCode
            XrTableCell16.Text = item.AdmissionDate
            XrTableCell22.Text = item.EgressDate
            'If item.TypeAdmission = 1 Then
            '    XrTableCell16.Text = Format(item.AdmissionDate, "yyyy-MM-dd")
            '    XrTableCell22.Text = Format(item.EgressDate, "yyyy-MM-dd")
            'Else
            '    XrTableCell16.Text = item.AdmissionDate
            '    XrTableCell22.Text = item.EgressDate
            'End If

            XrTableCell18.Text = item.PatientLevel
            XrTableCell20.Text = item.PatientAddress
            XrTableCell24.Text = item.PatientAge
            XrTableCell26.Text = item.PatientTelephoneNumber + " - " + item.PatientPhoneMovil
            XrTableCell30.Text = item.UserCode & " " & item.FullNameUser
            XrLabel3.Text = "AGENTE RETENEDOR IMPUESTO SOBRE LAS VENTAS AL REG COMUN / RESOLUCIÓN " & item.ResolutionNumber & " DEL " & item.ResolutionDate & " HABILI-AUTORIZA " & item.ResolutionInitialInvoice & " AL " & item.ResolutionFinalInvoice & " - FACTURACION POR COMPUTADOR - EFECTUAR RETENCION DEL 2% SERVICIOS DE SA"
            'INDUserCreate.Text = item.UserCode & " - " & item.FullNameUser
            Dim printingMode = item.PrintingMode
            Dim CareGroupType = item.CareGroupType
            If CareGroupType = 1 Then
                XrTableCell84.Visible = True
                XrTableCell51.Visible = True
            ElseIf CareGroupType = 3 Then
                XrTableCell79.Visible = False
                XrTableCell6.Visible = False
                XrTableCell84.Visible = False
                XrTableCell51.Visible = False
            Else
                XrTableCell84.Visible = False
                XrTableCell51.Visible = False
            End If

            'tamaño de impresion
            If INDPrSizePage.Value = 2 Then
                reporte.PaperKind = Printing.PaperKind.A5Rotated
                reporte.Margins.Left = 14
                reporte.Margins.Right = 13
                GroupFooter3.Visible = False
                GroupFooter2.Visible = True

                XrTableCell122.Text = Format(item.SubTotalService, "c0")
                XrTableCell199.Text = Format(item.TotalPatientSalesPrice, "c0")
                XrTableCell126.Text = Format(item.ThirdPartySalesValue, "c0")
                Me.XrTableCell120.Text = Utils.Num2Text(Convert.ToDecimal(item.ThirdPartySalesValue)).ToString & " PESOS M/Cte."
                GroupHeader4.Visible = False
            Else
                reporte.PaperKind = Printing.PaperKind.Letter
                reporte.Margins.Left = 24
                reporte.Margins.Right = 25
                GroupFooter3.Visible = True
                GroupFooter2.Visible = False

                XrTableCell53.Text = Format(item.SubTotalService, "c0")
                XrTableCell50.Text = Format(item.ThirdPartyDiscountValue, "c0")
                XrTableCell54.Text = Format(item.TotalPatientSalesPrice, "c0")
                XrTableCell77.Text = Format(item.PatientDiscount, "c0")
                XrTableCell56.Text = Format(item.TotalPatientAccountReceivable, "c0")
                XrTableCell64.Text = Format(item.ThirdPartySalesValue, "c0")
                Me.XrTableCell45.Text = Utils.Num2Text(Convert.ToDecimal(item.ThirdPartySalesValue)).ToString & " PESOS M/Cte."
                GroupHeader4.Visible = True
            End If

            'cargar direccion, telefono y codigo ips
            Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(item.OperatingUnitId)
            INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & operatingUnit(0).Address & " - Teléfono: " & operatingUnit(0).Phone & " - Código IPS: " & operatingUnit(0).IPSCode
            XrLabel19.Text = INDLblNitCompany.Text

            ' si tiene pagare
            If item.IdPay IsNot Nothing Then
                GroupFooter2.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
                ReportFooter.Visible = True
                INDPrIdPay.Value = item.IdPay
            End If
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        XrLabel20.Text = IndigoSessionValues.IndigoCompanyName
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

    End Sub

    Sub SetTextWatermark(report As XtraReport)
        ' Adjust text watermark settings.
        report.Watermark.Text = "FACTURA ANULADA"
        report.Watermark.TextDirection = DirectionMode.ForwardDiagonal
        report.Watermark.Font = New DXFont(report.Watermark.Font.Name, 80)
        report.Watermark.ForeColor = Color.DodgerBlue
        report.Watermark.TextTransparency = 200
        report.Watermark.ShowBehind = False
        report.Watermark.PageRange = "1-50"

    End Sub

    Private Sub XrTableCell94_SummaryRowChanged(sender As Object, e As EventArgs) Handles XrTableCell94.SummaryRowChanged
        If Not dictionarySum.ContainsKey(GetCurrentColumnValue("invoiceDetailId")) Then
            totalSum += Convert.ToDecimal(GetCurrentColumnValue("INDTotalGroup"))
            dictionarySum.Add(GetCurrentColumnValue("invoiceDetailId"), GetCurrentColumnValue("invoiceDetailId"))
        End If
    End Sub

    Private Sub XrTableCell94_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell94.SummaryGetResult
        e.Result = Format(Math.Ceiling(totalSum), "c0")
        e.Handled = True
    End Sub

    Private Sub XrTableCell94_SummaryReset(sender As Object, e As EventArgs) Handles XrTableCell94.SummaryReset
        totalSum = 0
    End Sub

End Class