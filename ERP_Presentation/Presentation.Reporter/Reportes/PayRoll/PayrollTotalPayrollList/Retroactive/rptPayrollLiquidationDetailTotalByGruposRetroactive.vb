#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptPayrollLiquidationDetailTotalByGruposRetroactive
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

        filtroConsulta = "InitialDateRetroactive >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND InitialDateRetroactive <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'filtro por estado
        'If ParametrosReporte(2) <> "T" Then
        '    filtroConsulta &= " AND PayrollId.RegisterStatus = '" & ParametrosReporte(2) & "'"
        'End If

        'filtro por empleado
        If ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND IdEmployee = " & ParametrosReporte(3)
        End If

        'filtro por Grupo
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " AND GroupCode >= '" & ParametrosReporte(4) & "' AND GroupCode <= '" & ParametrosReporte(5) & "'"
        End If

        Dim sucursalIni As String = IIf(ParametrosReporte(6) Is Nothing Or ParametrosReporte(6) = String.Empty, "NULL", ParametrosReporte(6))
        Dim sucursalFin As String = IIf(ParametrosReporte(7) Is Nothing Or ParametrosReporte(7) = String.Empty, "NULL", ParametrosReporte(7))

        filtroConsulta = String.Format("{0} AND ((BranchOfficeId >= {1} AND BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

        Dim list As List(Of PayrollVTotalRetroactiveConceptReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVTotalRetroactiveConceptReportXpo)(Nothing, filtroConsulta)

        'For Each item In list.OrderBy(Function(x) x.PayrollId.GroupId.Id).ThenBy(Function(x) x.PayrollId.EmployeeId.Id).ToList
        '    If Not dictionaryEmployee.ContainsKey(item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id) Then
        '        item.NumEmploye = 1
        '        dictionaryEmployee.Add(item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id, item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id)
        '    Else
        '        item.NumEmploye = 0
        '    End If
        'Next

        Me.DataSource = list

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayrollLiquidationDetailTotalByGruposRetroactive_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub

End Class