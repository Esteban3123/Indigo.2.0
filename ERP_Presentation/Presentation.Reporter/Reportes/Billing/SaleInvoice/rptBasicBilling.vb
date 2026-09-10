#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports System.Drawing.Printing
Imports DevExpress.XtraPrinting.BarCode
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.XtraReports.Parameters
Imports System.IO
Imports System.Globalization
Imports DevExpress.Drawing

#End Region

Public Class rptBasicBilling
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' abreviacion de la moneda
    ''' </summary>
    Dim CurrencyAbbreviation As String = IndigoSessionValues.CurrencyISO4217

    ''' <summary>
    ''' Nombre de la moneda
    ''' </summary>
    Dim CurrencyName As String = IndigoSessionValues.CurrencyName

    Dim INDList As List(Of BasicBillingDetailReportXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "BasicBillingId.Id = " & ParametrosReporte(0)
        If ParametrosReporte.Count > 1 Then
            filtroConsulta = "BasicBillingId.InvoiceId = " & ParametrosReporte(1)
        End If

        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BasicBillingDetailReportXpo)(Nothing, filtroConsulta)

        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing AndAlso INDListUser.Count > 0 Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If

            If INDList.Any(Function(x) x.ServicesProvidedId IsNot Nothing) Then
                Dim listServicesProvided As New List(Of BillingConceptReportXpo)

                INDList.ForEach(Sub(m)
                                    If m.ServicesProvidedId IsNot Nothing Then
                                        listServicesProvided.Add(m.ServicesProvidedId)
                                    End If
                                End Sub)

                For Each item In listServicesProvided

                    Dim Father As BasicBillingDetailReportXpo = Nothing
                    INDList.ForEach(Sub(m)
                                        If m.ServicesProvidedId IsNot Nothing And m.ServicesProvidedId?.Id = item.Id Then
                                            Father = m
                                        End If
                                    End Sub)

                    If Father IsNot Nothing Then
                        Dim servicesProvided As New BasicBillingDetailReportXpo(INDList(0).Session) With {.BasicBillingId = Father.BasicBillingId, .BillingConceptId = item, .DetailType = Father.DetailType, .Quantity = Father.Quantity, .Price = Father.Price,
                                                                                                        .Value = Father.Value, .PercentageIVA = Father.PercentageIVA}
                        Father.Quantity = 0
                        Father.Value = 0
                        Father.Price = 0
                        Father.PercentageIVA = 0
                        INDList.Insert(INDList.IndexOf(Father) + 1, servicesProvided)
                    End If
                Next
            End If

            Dim basicBilling = INDList(0).BasicBillingId

            If basicBilling.ThirdPartyEntityCopayId IsNot Nothing Then
                Dim serviceInvoice = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetXPOObject(Of BillingInvoiceCopayXpo)("BasicBillingId.Id = " & basicBilling.Id & "")
                XrTableRow12.Visible = True
                XrTableCell30.Text = serviceInvoice.InvoiceId.DescriptionHealthAdministrator
            Else
                XrTableRow12.Visible = False
            End If

            If basicBilling.ConditionSalesId IsNot Nothing Then
                XrTableRow8.Visible = True
            Else
                XrTableRow8.Visible = False
                If XrTableRow12.Visible Then
                    XrTableCell30.Borders = CType((DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
                    XrTableCell29.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
                Else
                    XrTableCell48.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
                    XrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
                    XrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
                    XrTableCell49.Borders = CType((DevExpress.XtraPrinting.BorderSide.Right Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
                End If
            End If

            INDPrResolutionNumber.Value = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.ResolutionNumber.Trim()
            INDPrResolutionDate.Value = Format(CDate(CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.ResolutionDate.ToString()), "dd/MM/yyyy")
            INDPrResolutionInvoicePrefix.Value = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.InvoicePrefix.Trim()
            INDPrResolutionInitialInvoice.Value = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.InitialInvoice
            INDPrResolutionFinalInvoice.Value = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.FinalInvoice
            INDPrResolutionInitialDate.Value = Format(CDate(CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.InitialDate.ToString()), "dd/MM/yyyy")
            INDPrResolutionFinalDate.Value = Format(CDate(CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.BillingAuthorizationId.FinalDate.ToString()), "dd/MM/yyyy")

            Dim invoice = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.InvoiceId
            If invoice Is Nothing Then
                INDlblInvoice.Text = "CODIGO"
                INDtxtInvoice.Text = CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.Code
                INDtxtInvoiceDate.Text = Format(CDate(CType(INDList(0), BasicBillingDetailReportXpo).BasicBillingId.DocumentDate.ToString()), "dd/MM/yyyy HH:mm")

                INDlblCUFE.Visible = False
                INDtxtCUFE.Visible = False
                XrBarCode2.Visible = False
                INDlblShippingDate.Visible = False
                INDtxtShippingDate.Visible = False
                INDtrElectronicDocumentStatus.Visible = False
            Else
                INDlblCUFE.Visible = False
                INDtxtCUFE.Visible = False
                XrBarCode2.Visible = False
                INDlblShippingDate.Visible = False
                INDtxtShippingDate.Visible = False
                INDtrElectronicDocumentStatus.Visible = False

                INDlblInvoice.Text = "FACTURA ELECTRÓNICA DE VENTA N°"
                INDtxtInvoice.Text = invoice.InvoiceNumber
                INDtxtInvoiceDate.Text = Format(CDate(invoice.InvoiceDate.ToString()), "dd/MM/yyyy HH:mm")

                If String.IsNullOrEmpty(invoice.CUFE) = False Then
                    INDlblCUFE.Visible = True
                    INDtxtCUFE.Visible = True
                    XrBarCode2.Visible = True

                    INDtxtCUFE.Text = invoice.CUFE
                    XrBarCode2.Text = invoice.QR

                    ' Set the bar code's type to QRCode.
                    XrBarCode2.Symbology = New QRCodeGenerator()
                    XrBarCode2.AutoModule = True
                    CType(XrBarCode2.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte

                    Dim INDElectronicDocument = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ElectronicDocumentReportXpo)(Nothing, "EntityId = '" & invoice.Id & "' And EntityName = 'Invoice'")
                    If INDElectronicDocument IsNot Nothing AndAlso INDElectronicDocument.Count > 0 Then
                        Dim statusDIAN = CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).Status
                        If statusDIAN > 1 Then
                            INDlblShippingDate.Visible = True
                            INDtxtShippingDate.Visible = True
                            If CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).ValidationDate IsNot Nothing Then
                                INDtxtShippingDate.Text = Format(CDate(CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).ValidationDate.ToString()), "dd/MM/yyyy HH:mm")
                            ElseIf CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).ShippingDate IsNot Nothing Then
                                INDtxtShippingDate.Text = Format(CDate(CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).ShippingDate.ToString()), "dd/MM/yyyy HH:mm")
                            End If
                        End If
                        INDtrElectronicDocumentStatus.Visible = True
                        INDtxtElectronicDocumentStatus.Text = CType(INDElectronicDocument(0), ElectronicDocumentReportXpo).StatusName
                    End If
                End If
            End If

            If invoice?.IsElectronicTicket Then
                INDlblInvoice.Text = "TIQUETE ELECTRONICO DE VENTA N°"
            End If
        End If
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptBasicBilling_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim ParametrosFilter As ParameterCollection = Me.Parameters
        Dim paramValue As Object = Nothing
        If Me.Parameters.Count > 0 Then
            paramValue = Me.Parameters(0).Value
        End If
        ' Cargar el diseño personalizado antes de cualquier otra operación
        If ParametrosFilter?("INDfromSaleInvoiceAll")?.Value AndAlso Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _classRpt = New rptSubSaleInvoiceAll()
            Dim reportCustomer = _classRpt.LoadCustomLayout(Me.Tag, Me.GetType().Name)
            ' Si se carga el diseño personalizado, aplicar al reporte actual
            If reportCustomer IsNot Nothing Then
                Me.LoadLayout(reportCustomer)
            End If
            ' Restaurar los valores de los parámetros después de cargar el diseño
            If paramValue IsNot Nothing Then
                Me.Parameters(0).Value = paramValue
            End If
        End If

        ' Ahora continuar con la lógica de asignación de datos y demás parámetros
        If Me.Parameters.Count > 0 AndAlso Me.Parameters(0).Value > 0 Then
            ParametrosReporte = New Object() {0, ParametrosFilter("INDIdInvoiceSubreport").Value}
            CargarDataSource()
        End If

        Dim detail = DirectCast(GetCurrentRow(), BasicBillingDetailReportXpo)
        If detail IsNot Nothing Then
            If detail.BasicBillingId.Status = 3 Then
                Me.Watermark.Text = "ANULADO"
                Me.Watermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.ForwardDiagonal
                Me.Watermark.Font = New DXFont(Me.Watermark.Font.Name, 40)
                Me.Watermark.ForeColor = Color.DodgerBlue
                Me.Watermark.TextTransparency = 150
                Me.Watermark.ShowBehind = False
            End If
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        
        If INDList?.Any() Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            CurrencyAbbreviation = INDList?.FirstOrDefault?.BasicBillingId?.CurrencyAbbreviation
            CurrencyName = INDList?.FirstOrDefault?.BasicBillingId?.CurrencyName
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        ' Calcular valores numéricos y asignar el texto
        Dim _integerPart As Int64 = Int(Convert.ToDecimal(GetCurrentColumnValue("INDCfTotalValue")))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("INDCfTotalValue")) - _integerPart, "0.00"), 2)

        Me.INDCllValueTotalInvoiceLetters.Text = String.Format("{0} {1}{2}",
                                                     Utils.Num2Text(_integerPart).ToString,
                                                     CurrencyName.ToUpper,
                                                     If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString} {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))
    End Sub

End Class