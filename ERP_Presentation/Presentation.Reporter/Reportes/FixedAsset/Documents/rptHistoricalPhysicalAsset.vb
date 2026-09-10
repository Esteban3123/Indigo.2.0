#Region "Librerias Improtadas"

Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Reporter

#End Region

Public Class rptHistoricalPhysicalAsset
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim filters As Dictionary(Of String, String)

    Dim dtReportHistoricalPhysicalAsset As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' Abreviación de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' Variable para darle formato de decimales a los valores del reporte
    ''' </summary>
    Private _decimalFormat As Integer

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task
        Try
            filters = ParametrosReporte(0)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SP_ReportHistoricalPhysicalAssetAsync(filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportHistoricalPhysicalAsset = ds.Tables("ReportHistoricalPhysicalAsset")
                Me.DataSource = dtReportHistoricalPhysicalAsset
                Me.DataMember = "ReportHistoricalPhysicalAsset"
                SetCurrencyFormat(filters("LegalBookId"))
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

#End Region

#Region "Events"

    Private Sub rptFixedAssetHistoricalDepreciation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim INDPeriod As Date = "01/" & filters("Month") & "/" & filters("Year")
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblDate.Text = "Periodo depreciado: " & INDPeriod.ToString("MMMM Del yyyy")
        Me.INDPrmLegalBookName.Value = filters("LegalBookCodeName")

        Dim address, phoneNumber, codeips, city As String
        'cargar direccion, telefono y codigo ips
        If ParametrosReporte(0) IsNot Nothing Then
            Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(IndigoSessionValues.IndigoOperatingUnitId)
            address = operatingUnit(0).Address
            phoneNumber = operatingUnit(0).Phone
            codeips = operatingUnit(0).IPSCode
            If operatingUnit(0).IdCity IsNot Nothing Then
                city = operatingUnit(0).IdCity.Descripcion
            Else
                city = "No asignada(o)"
            End If
        Else
            address = "No asignada(o)"
            phoneNumber = "No asignada(o)"
            codeips = "No asignada(o)"
            city = "No asignada(o)"
        End If

        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & address &
                            " - Teléfono: " & phoneNumber & " - Código IPS: " & codeips

        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
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

#Region "Cambio de formato de moneda"
    ''' <summary>
    ''' Evento que obtiene los parámetros para establecer el formato de la moneda
    ''' </summary>
    Private Sub SetCurrencyFormat(ByVal legalBookId As Integer)
        'Obtenemos los parámetros de activos fijos
        Dim filter = "OperatingUnitId = " & IndigoSessionValues.IndigoOperatingUnitId
        Dim settingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetXPOObject(Of SettingFixedAssetXpo)(filter)
        'Establecemos decimales
        If settingFixedAsset.CurrencyId?.RoundingType IsNot Nothing Then
            FormatValueWithDecimals(settingFixedAsset.CurrencyId.RoundingType)
        End If

        'Obtenemos la moneda del libro seleccionado en el reporte
        Dim filter2 As String = "Id = " & legalBookId.ToString
        Dim book = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetXPOObject(Of BookXpo)(filter2)
        _currencyAbbreviation = book.OfficialCurrencyId.Abbreviation
    End Sub

    ''' <summary>
    ''' Método para formatear los valores del reporte según el tipo de redondeo parametrizado a la moneda
    ''' </summary>
    ''' <param name="roundingType"></param>
    Private Sub FormatValueWithDecimals(roundingType As Integer)
        Select Case roundingType
            Case 1
                _decimalFormat = 2
            Case 2
                _decimalFormat = 1
            Case >= 3
                _decimalFormat = 0
        End Select
    End Sub

    ''' <summary>
    ''' Método que modifica el formato de las celdas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ChangeFormat(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell2.BeforePrint, XrTableCell14.BeforePrint, XrTableCell4.BeforePrint, XrTableCell32.BeforePrint
        sender.Text = Utils.GetMoneyWithISO4217(sender.Text, _currencyAbbreviation, _decimalFormat)
    End Sub
    ''' <summary>
    ''' Modifica el formato de las celdas que obtienen totales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ChangeFormat_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell8.SummaryGetResult, XrTableCell12.SummaryGetResult, XrTableCell13.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), _currencyAbbreviation, _decimalFormat)
        e.Handled = True
    End Sub

#End Region

End Class