#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports System.Globalization
#End Region

Public Class rptLiquidationOfSheetVacations
    Implements IReport


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private fechaIni As Date
    Private fechaFin As Date

    Private filtroConsulta As String = ""
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        'El ParametrosReporte(5) se usa  para validar el origen de los parámetros   pues es usado por el formulario de vacaciones y el formulario reporte de liquidación de vacaciones.
        If ParametrosReporte(5) <> 1 Then
            filtroConsulta = "LiquidationDate >= '" & Format(ParametrosReporte(0), "yyyyMMdd") & "' And LiquidationDate <= '" & Format(ParametrosReporte(1), "yyyyMMdd") & "'"
        Else
            filtroConsulta = "VacationStartDate >= '" & Format(ParametrosReporte(0), "yyyyMMdd") & "' And VacationStartDate <= '" & Format(ParametrosReporte(1), "yyyyMMdd") & "'"
        End If

        If ParametrosReporte(2) IsNot Nothing Then
            filtroConsulta &= " And VacationPeriodId.EmployeeId.Id = '" & Me.ParametrosReporte(2) & "'"
        End If

        'filtro por Grupo
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " And VacationPeriodId.ContractId.GroupId.Code >= '" & Me.ParametrosReporte(3) & "' AND VacationPeriodId.ContractId.GroupId.Code <= '" & ParametrosReporte(4) & "'"
        End If


        Dim listVacation As List(Of PayrollVacation) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVacation)(Nothing, filtroConsulta)
        Dim listVacationNew As New List(Of PayrollVacation)
        For Each item As PayrollVacation In listVacation
            Dim query = (From e In listVacationNew Where e.VacationStartDate = item.VacationStartDate And e.VacationEndDate = item.VacationEndDate And e.VacationPeriodId.EmployeeId.Id = item.VacationPeriodId.EmployeeId.Id Select e).ToList()
            If query.Count = 0 Then
                listVacationNew.Add(item)
            End If
        Next
        Me.DataSource = listVacationNew
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptLiquidationOfSheetVacations_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        InitializeReportLocalization()

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblDateMonth.Text = "PAGOS DE NÓMINA DEL MES DE " & CDate(Me.DataSource(0).LiquidationDate).ToString(" MMMM DE yyyy").ToUpper()
        INDLblDate.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString(" al   dd De MMMM Del yyyy")
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
    ''' <summary>
    ''' Inicializa la cultura del reporte
    ''' </summary>
    Private Sub InitializeReportLocalization()
        Dim PayrollSettings As PayrollSettingsXpo =
        XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).
        AccountingService.GetXPOObject(Of PayrollSettingsXpo)(Nothing)
        If PayrollSettings IsNot Nothing Then
            Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            culture.NumberFormat = PayrollSettings.CurrencyId.Abbreviation.GetNumberFormat()
            ApplyLocalization(culture)
        End If
    End Sub
End Class