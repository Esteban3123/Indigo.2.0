#Region "Imports"

Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptDeteriorationIndicationsByPhysicalAssetSummarized
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para el datatable con los datos del reporte
    ''' </summary>
    Dim dtDeteriorationIndicationsByPhysicalAsset As DataTable

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

#Region "Events"

    Private Sub rptDeteriorationIndicationsByPhysicalAssetSummarized_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

#End Region

#Region "Methods"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetListReportDeteriorationIndicationsByPhysicalAssetAsync(ParametrosReporte(0), Me.ParametrosReporte(1), Me.ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(6), ParametrosReporte(7), Me.IndigoSessionValues)
            If ds IsNot Nothing Then
                dtDeteriorationIndicationsByPhysicalAsset = ds.Tables("DeteriorationIndicationsByPhysicalAsset")
                Me.DataSource = dtDeteriorationIndicationsByPhysicalAsset
                Me.DataMember = "DeteriorationIndicationsByPhysicalAsset"
                SetCurrencyFormat()
            Else
                Me.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

#End Region

#Region "Cambio de formato de moneda"
    ''' <summary>
    ''' Evento que obtiene los parámetros para establecer el formato de la moneda
    ''' </summary>
    Private Sub SetCurrencyFormat()
        'Obtenemos los parámetros de activos fijos
        Dim filter = "OperatingUnitId = " & IndigoSessionValues.IndigoOperatingUnitId
        Dim settingFixedAsset = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetXPOObject(Of SettingFixedAssetXpo)(filter)
        'Obtenemos la moneda de los parámetros
        If settingFixedAsset.CurrencyId IsNot Nothing Then
            _currencyAbbreviation = settingFixedAsset.CurrencyId.Abbreviation
        End If
        'Establecemos decimales
        If settingFixedAsset.CurrencyId?.RoundingType IsNot Nothing Then
            FormatValueWithDecimals(settingFixedAsset.CurrencyId.RoundingType)
        End If
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
    Private Sub ChangeFormat(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell7.BeforePrint, XrTableCell11.BeforePrint, XrTableCell2.BeforePrint, XrTableCell32.BeforePrint, XrTableCell23.BeforePrint
        sender.Text = Utils.GetMoneyWithISO4217(sender.Text, _currencyAbbreviation, _decimalFormat)
    End Sub

#End Region

End Class