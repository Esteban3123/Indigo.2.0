#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptVacation
    Implements IReport


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "VacationPeriodId.EndDatePeriod <= '" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "'"

        'filtro por Estado
        If ParametrosReporte(1) IsNot Nothing And ParametrosReporte(1) <> "T" Then
            filtroConsulta &= " AND State = " & ParametrosReporte(1)
        End If

        'filtro Por Empleado
        If ParametrosReporte(2) IsNot Nothing Then
            filtroConsulta &= " And VacationPeriodId.EmployeeId = " & ParametrosReporte(2)

        End If


        Dim groupIni As String = IIf(ParametrosReporte(3) Is Nothing, "NULL", ParametrosReporte(3))
        Dim groupFin As String = IIf(ParametrosReporte(4) Is Nothing, "NULL", ParametrosReporte(4))
        filtroConsulta = String.Format("{0} AND ((VacationPeriodId.ContractId.GroupId >= {1} AND VacationPeriodId.ContractId.GroupId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, groupIni, groupFin)

        Dim sucursalIni As String = IIf(ParametrosReporte(5) Is Nothing, "NULL", ParametrosReporte(5))
        Dim sucursalFin As String = IIf(ParametrosReporte(6) Is Nothing, "NULL", ParametrosReporte(6))

        filtroConsulta = String.Format("{0} AND ((VacationPeriodId.ContractId.FunctionalUnitId >= {1} AND VacationPeriodId.ContractId.FunctionalUnitId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)




        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVacation)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptVacation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        'INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        INDLblDate.Text = "Vacaciones " & CDate(Me.ParametrosReporte(0)).ToString("De MMMM Del yyyy").ToUpper()
    End Sub
End Class