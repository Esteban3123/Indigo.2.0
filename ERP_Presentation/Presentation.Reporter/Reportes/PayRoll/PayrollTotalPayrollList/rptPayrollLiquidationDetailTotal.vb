#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptPayrollLiquidationDetailTotal
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

        filtroConsulta = "PayrollId.PayrollDateLiquidated >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND PayrollId.PayrollDateLiquidated <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'filtro por estado
        If ParametrosReporte(2) <> "T" Then
            filtroConsulta &= " AND PayrollId.RegisterStatus = '" & ParametrosReporte(2) & "'"
        End If

        'filtro por empleado
        If ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND PayrollId.EmployeeId.Id = " & ParametrosReporte(3)
        End If

        'filtro por Grupo
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " AND PayrollId.GroupId.Code >= '" & ParametrosReporte(4) & "' AND PayrollId.GroupId.Code <= '" & ParametrosReporte(5) & "'"
        End If

        'filtro por Unidad funcional
        'If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
        '    filtroConsulta &= " AND PayrollId.ContractId.FunctionalUnitId.Code >= '" & ParametrosReporte(6) & "' AND PayrollId.ContractId.FunctionalUnitId.Code <= '" & ParametrosReporte(7) & "'"
        'End If

        Dim list As List(Of PayrollLiquidationDetail) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollLiquidationDetail)(Nothing, filtroConsulta)

        For Each item In list.OrderBy(Function(x) x.PayrollId.GroupId.Id).ThenBy(Function(x) x.PayrollId.EmployeeId.Id).ToList
            If Not dictionaryEmployee.ContainsKey(item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id) Then
                item.NumEmploye = 1
                dictionaryEmployee.Add(item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id, item.PayrollId.GroupId.Id & " - " & item.PayrollId.EmployeeId.Id)
            Else
                item.NumEmploye = 0
            End If
        Next

        Me.DataSource = list
        'Dim item = list.GroupBy(Function(x) x.PayrollId.GroupId.Id And x.PayrollId.EmployeeId.Id).Count()

        'Dim netoPagar As Integer = Convert.ToInt32(INDLblTotalDevengado) + Convert.ToInt32(INDLblTotalDeducciones)

        'INDLblNetoPagar.Text = Convert.ToString(netoPagar)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayrollLiquidationDetailTotalGrupos_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.XrLabel25.Text = CStr(totalEmploye)
    End Sub

End Class