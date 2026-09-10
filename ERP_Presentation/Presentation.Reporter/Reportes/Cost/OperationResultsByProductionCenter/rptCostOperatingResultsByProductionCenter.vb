#Region "Imports"

Imports System.Drawing.Printing
Imports System.Globalization
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptCostOperatingResultsByProductionCenter
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim OperatingProductionCenter As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return Nothing
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostListReportOperatingProductionCenter(CInt(ParametrosReporte(1)), CInt(ParametrosReporte(3)), CInt(ParametrosReporte(0)), CStr(ParametrosReporte(4)), CStr(ParametrosReporte(5)), ParametrosReporte(6), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                OperatingProductionCenter = ds.Tables("OperatingProductionCenter")
                Me.DataSource = OperatingProductionCenter
                Me.DataMember = "OperatingProductionCenter"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

    Private Sub rptCostOperatingResultsByProductionCenter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        InitializeReportLocalization()
        Dim fechaIni = New Date(Me.ParametrosReporte(0), Me.ParametrosReporte(1), 1)
        Dim fechafin = New Date(Me.ParametrosReporte(2), Me.ParametrosReporte(3), 1)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "DESDE " & fechaIni.ToString(" MMMM DE yyyy").ToUpper() & "  HASTA " & fechafin.ToString(" MMMM DE yyyy").ToUpper()
    End Sub

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
    ''' Inicializa la cultura del reporte
    ''' </summary>
    Private Sub InitializeReportLocalization()
        Dim companySettings As GeneralLedgerCompanySettingsXpo =
        XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).
        AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
        If companySettings IsNot Nothing Then
            Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            culture.NumberFormat = companySettings.OfficialCurrency.Abbreviation.GetNumberFormat()
            ApplyLocalization(culture)
        End If
    End Sub

#End Region

End Class