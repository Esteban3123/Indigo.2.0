#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Payroll.Entities
#End Region

Public Class rptIncreaseSalary
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmReportIncreaseSalary"

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "CreationDate >= '" & Format(Me.ParametrosReporte(0), "yyyyMMdd") & "' And CreationDate <= '" & Format(Me.ParametrosReporte(1), "yyyyMMdd") & "'"

        'filtro por Empleado
        If ParametrosReporte(2) IsNot Nothing Then
            filtroConsulta &= " And IdEmployee = " & ParametrosReporte(2)
        End If

        'filtro por Grupo
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " And CodeGroup >= " & Me.ParametrosReporte(3) & " AND CodeGroup <= " & ParametrosReporte(4)
        End If

        'Filtro por Sucursal
        If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
            filtroConsulta &= " And CodeBranchOffice >= " & Me.ParametrosReporte(5) & " AND CodeBranchOffice <= " & ParametrosReporte(6)
        End If

        'Filtro por Unidad Funcional
        If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
            filtroConsulta &= " And CodeFunctionalUnit >= " & Me.ParametrosReporte(7) & " AND CodeFunctionalUnit <= " & ParametrosReporte(8)
        End If

        'Filtro por Cargo
        If ParametrosReporte(9) IsNot Nothing And ParametrosReporte(10) IsNot Nothing Then
            filtroConsulta &= " And CodePosition >= " & Me.ParametrosReporte(9) & " AND CodePosition <= " & ParametrosReporte(10)
        End If


        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollViewReportIncreaseSalaryXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptIncreaseSalary.CNameReport
        End Get
    End Property

    Private Sub rptUnemployment_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        lblAñoLiquidacion.Text = Me.ParametrosReporte(1).ToString()
    End Sub
End Class