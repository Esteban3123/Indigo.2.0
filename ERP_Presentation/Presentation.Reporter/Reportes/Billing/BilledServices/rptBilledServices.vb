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
Imports System.Globalization

#End Region

Public Class rptBilledServices
    Implements IReport
    Implements IReportAsync

#Region "Variables"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte si el campo moneda esta seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Currency

#End Region

#Region "IReport"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filter As String = ""

            If ParametrosReporte(3) = 1 Then
                filter = "InvoiceDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# and InvoiceDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"
            End If

            filter &= " and DocumentType in (" & ParametrosReporte(4) & ")"

            If ParametrosReporte(2) <> 3 Then
                filter &= " and StatusInvoice = " & ParametrosReporte(2)
            End If

            If String.IsNullOrEmpty(ParametrosReporte(7)) = False Then
                filter &= " and ThirdPartyId in (" & ParametrosReporte(7) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(8)) = False Then
                filter &= " and HealthAdministratorId in (" & ParametrosReporte(8) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(9)) = False Then
                filter &= " and CareGroupId in (" & ParametrosReporte(9) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(10)) = False Then
                filter &= " and CareCenterCode in (" & ParametrosReporte(10) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(11)) = False Then
                filter &= " and UserCode in (" & ParametrosReporte(11) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(12)) = False Then
                filter &= " and FunctionalUnitCode in (" & ParametrosReporte(12) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(13)) = False Then
                filter &= " and PerformsHealthProfessionalCode in (" & ParametrosReporte(13) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(14)) = False AndAlso String.IsNullOrEmpty(ParametrosReporte(15)) = False Then
                filter &= " and (CupsEntityId in (" & ParametrosReporte(14) & ") or ProductId in (" & ParametrosReporte(15) & "))"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(14)) = False AndAlso String.IsNullOrEmpty(ParametrosReporte(15)) = True Then
                filter &= " and CupsEntityId in (" & ParametrosReporte(14) & ")"
            End If

            If String.IsNullOrEmpty(ParametrosReporte(14)) = True AndAlso String.IsNullOrEmpty(ParametrosReporte(15)) = False Then
                filter &= " and ProductId in (" & ParametrosReporte(15) & ")"
            End If

            'filtro por moneda
            If ParametrosReporte(16) > 0 Then
                filter &= " and CurrencyId = " & ParametrosReporte(16)
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ViewBillingStatisticsWithServicesXpo)(Nothing, filter)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

#End Region

#Region "Methods"

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento que se dispara al pintar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptBilledServices_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If ParametrosReporte(6) = 2 Then 'Entidad
            GroupHeader2.Visible = False
            GroupHeader1.Visible = False
        ElseIf ParametrosReporte(6) = 3 Then 'Grupo Atencion
            GroupHeader1.Visible = False
        End If

        If ParametrosReporte(5) = 1 Then 'Resumido
            XrTable3.Visible = True
            XrTable2.Visible = False

            XrTable8.Visible = True
            XrTable1.Visible = False
        Else 'Detallado
            XrTable3.Visible = False
            XrTable2.Visible = True

            XrTable8.Visible = False
            XrTable1.Visible = True
        End If

        INDPrmTypeReport.Value = ParametrosReporte(5)
        INDPrmGroupBy.Value = ParametrosReporte(6)

        Me.INDLblTitle.Text = "ESTADÍSTICO DE SERVICIOS - " + IIf(ParametrosReporte(5) = 1, "RESUMIDO", "DETALLADO")
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")

        If Not String.IsNullOrEmpty(Currency?.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = Currency.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If
    End Sub

    Private Sub XrTableCell16_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell16.SummaryGetResult, XrTableCell17.SummaryGetResult, XrTableCell18.SummaryGetResult,
                                                                                                         XrTableCell40.SummaryGetResult, XrTableCell41.SummaryGetResult, XrTableCell42.SummaryGetResult,
                                                                                                         XrTableCell43.SummaryGetResult, XrTableCell44.SummaryGetResult, XrTableCell45.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell26_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell26.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell26.Text = Utils.GetMoneyWithISO4217(row.TotalValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell6.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell6.Text = Utils.GetMoneyWithISO4217(row.ValueCopay, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell7.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell7.Text = Utils.GetMoneyWithISO4217(row.EntityValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell32_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell32.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell32.Text = Utils.GetMoneyWithISO4217(row.TotalValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell33_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell33.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell33.Text = Utils.GetMoneyWithISO4217(row.ValueCopay, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell34_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell34.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell34.Text = Utils.GetMoneyWithISO4217(row.EntityValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell21_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell21.SummaryGetResult, XrTableCell22.SummaryGetResult, XrTableCell29.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

#End Region

End Class