#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptAccrualsByCostCenterSummary
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private fechaIni As Date
    Private fechaFin As Date

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim fechaIni1 As Date = New Date(Me.ParametrosReporte(0), Me.ParametrosReporte(1), 1)

        Dim fechaIni2 = New Date(Me.ParametrosReporte(2), Me.ParametrosReporte(3), 1)
        Dim fecha2 As Date = fechaIni2.AddMonths(+1)
        fechaFin = fecha2.AddDays(-1)


        Dim filtroConsulta As String = "PayrollId.PayrollDateLiquidated >= '" & Format(fechaIni1, "yyyyMMdd") & "' And PayrollId.PayrollDateLiquidated <= '" & Format(fechaFin, "yyyyMMdd") & "'"


        'filtro por Centro de costo
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " And PayrollId.EmployeeId.CostCenterId.Code >= '" & Me.ParametrosReporte(4) & "' AND PayrollId.EmployeeId.CostCenterId.Code <= '" & ParametrosReporte(5) & "'"
        End If

        'filtro por Grupo
        If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
            filtroConsulta &= " And PayrollId.GroupId.Code >= '" & Me.ParametrosReporte(6) & "' AND PayrollId.GroupId.Code <= '" & ParametrosReporte(7) & "'"
        End If
        'filtro solo Devengos
        filtroConsulta &= " And ConceptType = 1"

        'Filtro por administrativos u operativos
        If ParametrosReporte(8) <> "T" Then
            filtroConsulta &= " And PayrollId.EmployeeId.EmployeeTypeId.Code = '" & Me.ParametrosReporte(8) & "'"
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollLiquidationDetail)(Nothing, filtroConsulta)

        INDLblDateMonth.Text = "PAGOS DE NÓMINA DEL PERIODO DE " & CDate(fechaIni1).ToString(" MMMM DE yyyy").ToUpper() & " -" & CDate(fechaFin).ToString(" MMMM DE yyyy").ToUpper()
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptNoveltiesPayroll_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If ParametrosReporte(8) = "001" Then
            INDLblTitle.Text = "RELACIÓN DE DEVENGOS POR CENTROS DE COSTOS DE LA UNIDAD ADMINISTRATIVA"
        ElseIf ParametrosReporte(8) = "002" Then
            INDLblTitle.Text = "RELACIÓN DE DEVENGOS POR CENTROS DE COSTOS DE LA UNIDAD OPERATIVA"
        Else
            INDLblTitle.Text = "RELACIÓN DE DEVENGOS POR CENTROS DE COSTOS"
        End If
    End Sub
End Class