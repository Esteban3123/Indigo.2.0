#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptPayrollLiquidationDetailTotalIncentive
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim dictionaryEmployee As New Dictionary(Of String, String)

    Const CNameReport = "Payroll.FrmPayrollControl"

    Public totalEmploye As Integer = 0
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = Nothing

        filtroConsulta = "PeriodInitialDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND PeriodEndDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'filtro por estado
        If ParametrosReporte(2) <> "T" Then
            filtroConsulta &= " AND EmployeeTypeCode = '" & ParametrosReporte(2) & "'"
        End If

        'filtro por empleado
        If ParametrosReporte(3) <> 3 Then
            filtroConsulta &= " AND Period = " & ParametrosReporte(3)
        End If

        'filtro por empleado
        If ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " AND EmployeeId = " & ParametrosReporte(4)
        End If

        Dim sucursalIni As String = IIf(ParametrosReporte(5) Is Nothing Or CStr(ParametrosReporte(5)) = String.Empty, "NULL", CStr(ParametrosReporte(5)))
        Dim sucursalFin As String = IIf(ParametrosReporte(6) Is Nothing Or CStr(ParametrosReporte(6)) = String.Empty, "NULL", CStr(ParametrosReporte(6)))

        filtroConsulta = String.Format("{0} AND ((BranchOfficeId >= {1} AND BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVTotalIncentiveConceptReportXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayrollLiquidationDetailTotalIncentive_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub

End Class