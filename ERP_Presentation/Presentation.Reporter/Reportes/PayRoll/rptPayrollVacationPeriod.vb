#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region


Public Class rptPayrollVacationPeriod
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim fechaIni As Date = New Date(Me.ParametrosReporte(1), 1, 1)
        Dim fecha As Date = fechaIni.AddMonths(+12)
        Dim fechaFin As Date = fecha.AddDays(-1)

        Dim filtroConsulta As String = "VacationPeriodId.EmployeeId.ThirdPartyId.Nit = '" & Me.ParametrosReporte(0) & "' AND VacationStartDate >= '" & Format(fechaIni, "yyyyMMdd") & "' And VacationStartDate <= '" & Format(fechaFin, "yyyyMMdd") & "'"
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

    Private Sub rptPayrollVacationPeriod_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

    End Sub
End Class