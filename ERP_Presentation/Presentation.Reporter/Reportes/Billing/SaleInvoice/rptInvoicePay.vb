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

#End Region

Public Class rptInvoicePay
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim listDetail As List(Of Infrastructure.Data.Xpo.BillingRepository.PortfolioAccountReceivableXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Me.CargarDataSourceGeneric()
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Sub CargarDataSourceGeneric()
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            listDetail = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of PortfolioAccountReceivableXpo)(Nothing, filtroConsulta)
            Me.DataSource = listDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte


    Private Sub rptInvoicePay_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        Dim ParametrosFilter As ParameterCollection = Me.Parameters
        If Me.Parameters.Count > 0 And Me.INDPrAccountReceivableId.Value > 0 Then
            ParametrosReporte = New Object() {ParametrosFilter("INDPrAccountReceivableId").Value}
            CargarDataSourceGeneric()
        End If

        If listDetail.Count > 0 Then
            Dim item = listDetail(0)
            If item.OperatingUnitId > 0 Then
                Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(item.OperatingUnitId)
                XrTableCell1.Text = operatingUnit(0).IdCity.Descripcion & ","
            End If

        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDPrNameCompany.Value = IndigoSessionValues.IndigoCompanyName

        'carga la definicion personalizada del reporte, si este viene del rptSaleInvoice
        If ParametrosFilter?("INDfromSaleInvoice")?.Value AndAlso IO.Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
            Dim _classRpt = New rptSaleInvoice()
            Dim reportCustomer = _classRpt.LoadCustomLayout(Me.Tag, Me.GetType().Name)
            Presentation.Controls.ReportHelper.LoadCustomizationReport(Me, reportCustomer)
        End If
    End Sub

End Class