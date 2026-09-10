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
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraPrinting.BarCode
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports System.IO

#End Region

Public Class rptSaleInvoiceCapitated
    Implements IReport, IReferenceToReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim list As List(Of InvoiceEntityCapitatedReportXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            'Dim List = XpoService.ListViewInvoiceByInvoiceId(ParametrosReporte(0), IndigoSessionValues.TransactionalContainer)
            Dim filtroConsulta As String = "InvoiceId.Id = " & ParametrosReporte(0)
            list = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InvoiceEntityCapitatedReportXpo)(Nothing, filtroConsulta)
            Me.DataSource = list
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Sub CargarDataSource2() Implements IReferenceToReport.CargarDataSource2
        Try
            Dim filtroConsulta As String = "InvoiceId.Id = " & ParametrosReporte(0)
            list = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InvoiceEntityCapitatedReportXpo)(Nothing, filtroConsulta)
            Me.DataSource = list
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptSaleInvoiceCapitated_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim ParametrosFilter As ParameterCollection = Me.Parameters

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            ParametrosReporte = New Object() {ParametrosFilter("INDIdInvoiceSubreport").Value}
            CargarDataSource()
        End If

        If list.count > 0 Then
            Dim item = list(0)
            Dim address, phoneNumber, codeips As String
            'cargar direccion, telefono y codigo ips
            If item.OperatingUnitId <> 0 Then
                Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(item.OperatingUnitId)
                address = operatingUnit(0).Address
                phoneNumber = operatingUnit(0).Phone
                codeips = operatingUnit(0).IPSCode
            Else
                address = "No asignada(o)"
                phoneNumber = "No asignada(o)"
                codeips = "No asignada(o)"
            End If

            INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & address &
                                " - Teléfono: " & phoneNumber & " - Código IPS: " & codeips


            If String.IsNullOrEmpty(item.InvoiceId.CUFE) Then
                XrTableCell15.Visible = False
                XrTableCell17.Visible = False
                XrLabel10.Visible = False
                XrLabel15.Visible = False
                XrBarCode2.HeightF = 20
            Else
                XrBarCode2.Symbology = New QRCodeGenerator()
                XrBarCode2.HeightF = 140

                ' If the AutoModule property is set to false, uncomment the next line.
                XrBarCode2.AutoModule = True
                CType(XrBarCode2.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte
            End If


        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Dim TotalValue = TryCast(DataSource, List(Of InvoiceEntityCapitatedReportXpo))(0).InvoiceId?.TotalValue
        Me.XrTableCell45.Text = Utils.Num2Text(IIf(TotalValue Is Nothing, Convert.ToDecimal(GetCurrentColumnValue("TotalValue")), TotalValue)).ToString & " PESOS M/Cte."

        'carga la definicion personalizada del reporte, si este viene del rptSubSaleInvoiceAll
        If ParametrosFilter?("INDfromSaleInvoiceAll")?.Value AndAlso Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _classRpt = New rptSubSaleInvoiceAll()
            Dim reportCustomer = _classRpt.LoadCustomLayout(Me.Tag, Me.GetType().Name)
            If reportCustomer IsNot Nothing Then
                Me.LoadLayout(reportCustomer)
            End If
        End If
    End Sub

End Class