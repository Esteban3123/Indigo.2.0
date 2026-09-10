#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptPayrollWithholdingByGroupRetroactive
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim fechaIni As Date
    Dim fechaFin As Date

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        fechaIni = New Date(Me.ParametrosReporte(1), Me.ParametrosReporte(2), 1)
        Dim fecha As Date = fechaIni.AddMonths(+1)
        fechaFin = fecha.AddDays(-1)

        Dim filtroConsulta As String = "InitialDateRetroactive >= '" & Format(fechaIni, "yyyyMMdd") & "' And InitialDateRetroactive <= '" & Format(fechaFin, "yyyyMMdd") & "'"

        'filtro por Grupo
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " And IdGroup >= '" & Me.ParametrosReporte(3) & "' AND IdGroup <= '" & ParametrosReporte(4) & "'"
        End If

        'Filtro por administrativos u operativos
        If ParametrosReporte(5) <> 3 Then
            filtroConsulta &= " And ProcedimientoRete = '" & Me.ParametrosReporte(5) & "'"
        End If

        'Se filtra por Empleado
        If ParametrosReporte(0) IsNot Nothing Then
            filtroConsulta &= " AND IdEmployee = " & ParametrosReporte(0)
        End If

        Dim sucursalIni As String = IIf(ParametrosReporte(6) Is Nothing Or ParametrosReporte(6) = String.Empty, "NULL", ParametrosReporte(6))
        Dim sucursalFin As String = IIf(ParametrosReporte(7) Is Nothing Or ParametrosReporte(7) = String.Empty, "NULL", ParametrosReporte(7))

        filtroConsulta = String.Format("{0} AND ((BranchOfficeId >= {1} AND BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportWithholdingRetroactiveXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayrollWithholding_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDateMonth.Text = "RETENCIÓN DE NÓMINA RETROACTIVA DEL PERIODO DE " & CDate(fechaIni).ToString("MMMM DE yyyy").ToUpper()
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
End Class