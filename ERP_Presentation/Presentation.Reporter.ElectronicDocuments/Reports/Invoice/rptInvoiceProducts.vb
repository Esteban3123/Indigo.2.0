#Region "Librerias Improtadas"

Imports System.IO
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Reporter.ElectronicDocuments.Infrastructure
Imports Presentation.Reporter.ElectronicDocuments.Utilities
Imports Presentation.Reporter.ElectronicDocuments.XpoEntities
Imports Presentation.Reporter.ElectronicDocuments.Reports

#End Region

Public Class rptInvoiceProducts
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "DocumentInvoiceProductSalesId.Id = " & ParametrosReporte(0)
        If ParametrosReporte.Count > 1 Then
            filtroConsulta = "DocumentInvoiceProductSalesId.InvoiceId = " & ParametrosReporte(1)
        End If

        Dim INDList As List(Of InventoryDocumentInvoiceProductSalesDetailReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryDocumentInvoiceProductSalesDetailReportXpo)(Nothing, filtroConsulta)
        If INDList.Exists(Function(x) x.Id > 0) Then
            Dim INDNameUser = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.CreationUser.Trim()
            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")
            If INDListUser IsNot Nothing AndAlso INDListUser.Count > 0 Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If

            If IndigoSessionValues.IndigoCompanyType = 1 Then
                INDPrResolutionNumber.Value = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.BillingAuthorizationId.ResolutionNumber.Trim()
                INDPrResolutionDate.Value = CDate(CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.BillingAuthorizationId.ResolutionDate.ToString()).ToString("dd/MM/yyyy")
                INDPrResolutionInvoicePrefix.Value = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.BillingAuthorizationId.InvoicePrefix.Trim()
                INDPrResolutionInitialInvoice.Value = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.BillingAuthorizationId.InitialInvoice
                INDPrResolutionFinalInvoice.Value = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.BillingAuthorizationId.FinalInvoice
            End If

            If INDList.FirstOrDefault.DocumentInvoiceProductSalesId.ConditionSalesId IsNot Nothing Then

                Dim filter As String = $"Id =  {INDList.FirstOrDefault.DocumentInvoiceProductSalesId.ConditionSalesId}"
                Dim _conditionSalesXpo = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetXPOObject(Of ConditionSalesXpo)(filter)

                If _conditionSalesXpo IsNot Nothing Then
                    XrTableCell50.Text = $" {_conditionSalesXpo.CodeName}"
                End If
            Else
                XrTableCell46.Text = String.Empty
            End If

            Dim invoice = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.InvoiceId
            If invoice Is Nothing OrElse String.IsNullOrEmpty(invoice.CUFE) Then
                XrTableRow13.Visible = False
                XrTableRow14.Visible = False
                XrTableRow43.Visible = False
                XrTableRow48.Visible = False
                INDlblTitle.Text = "FACTURA DE VENTA"
                INDtcInvoiceDate.Text = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.DocumentDate
                XrBarCode2.Text = CType(INDList(0), InventoryDocumentInvoiceProductSalesDetailReportXpo).DocumentInvoiceProductSalesId.Code
            Else
                XrTableRow14.Visible = True
                XrTableRow43.Visible = True
                XrTableRow48.Visible = True

                Dim INDElectronicDocument = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingElectronicDocumentReportXpo)(Nothing, "EntityId = '" & invoice.Id & "' And EntityName = 'Invoice'")
                INDlblTitle.Text = String.Format("FACTURA ELECTR�NICA DE VENTA N� {0}", invoice.InvoiceNumber)
                INDtcStatusDIAN.Text = CType(INDElectronicDocument(0), BillingElectronicDocumentReportXpo).StatusName
                If CType(INDElectronicDocument(0), BillingElectronicDocumentReportXpo).ValidationDate IsNot Nothing Then
                    INDtcValidationDate.Text = CDate(CType(INDElectronicDocument(0), BillingElectronicDocumentReportXpo).ValidationDate.ToString()).ToString("dd/MM/yyyy HH:mm")
                End If
                INDtcInvoiceDate.Text = CDate(invoice.InvoiceDate.ToString()).ToString("dd/MM/yyyy HH:mm")
                INDtcCUFE.Text = invoice.CUFE

                ' Set the bar code's type to QRCode.
                XrBarCode2.Symbology = New QRCodeGenerator()

                ' Adjust the bar code's main properties.
                Dim dimensions = 105
                If Not String.IsNullOrEmpty(XrBarCode2.Tag.ToString()) Then
                    dimensions = CType(XrBarCode2.Tag.ToString(), Integer)
                End If

                XrBarCode2.Text = invoice.QR
                XrBarCode2.Width = dimensions
                XrBarCode2.Height = dimensions

                ' If the AutoModule property is set to false, uncomment the next line.
                XrBarCode2.AutoModule = True
                CType(XrBarCode2.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte
            End If

            If invoice.IsElectronicTicket Then
                INDlblTitle.Text = $"TIQUETE ELECTRONICO DE VENTA {If(String.IsNullOrEmpty(invoice.InvoiceNumber), String.Empty, $"N° {invoice.InvoiceNumber}")}"
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

    Private Sub rptInvoiceProducts_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim ParametrosFilter As ParameterCollection = Me.Parameters

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            ParametrosReporte = New Object() {0, ParametrosFilter.GetByName("INDIdInvoiceSubreport").Value}
            CargarDataSource()
        End If

        Dim detail = DirectCast(GetCurrentRow(), InventoryDocumentInvoiceProductSalesDetailReportXpo)
        If detail IsNot Nothing Then
            If detail.DocumentInvoiceProductSalesId.Status = 3 Then
                Me.Watermark.Text = "ANULADO"
                Me.Watermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.ForwardDiagonal
                Me.Watermark.Font = New Font(Me.Watermark.Font.FontFamily, 40)
                Me.Watermark.ForeColor = Color.DodgerBlue
                Me.Watermark.TextTransparency = 150
                Me.Watermark.ShowBehind = False
            End If
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresi�n : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Me.INDCllValueTotalInvoiceLettersTypePatient.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDCfNetWorth"))).ToString & " PESOS M/Cte."

        'carga la definicion personalizada del reporte, si este viene del rptSubSaleInvoiceAll
        If ParametrosFilter?.GetByName("INDfromSaleInvoiceAll")?.Value AndAlso Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _classRpt = New rptSubSaleInvoiceAll()
            Dim reportCustomer = _classRpt.LoadCustomLayout(Me.Tag, Me.GetType().Name)
            If reportCustomer IsNot Nothing Then
                Me.LoadLayout(reportCustomer)
            End If
        End If
    End Sub
End Class