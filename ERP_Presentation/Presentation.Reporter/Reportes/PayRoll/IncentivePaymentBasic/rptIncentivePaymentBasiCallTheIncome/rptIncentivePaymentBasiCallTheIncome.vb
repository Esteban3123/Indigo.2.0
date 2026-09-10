#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptIncentivePaymentBasiCallTheIncome
    Implements IReport


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "PeriodInitialDate >= '" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "' And PeriodEndDate <= '" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "'"

        'filtro por Tipo de empleado
        If ParametrosReporte(2) <> "T" Then
            filtroConsulta &= "And ContractId.EmployeeId.EmployeeTypeId.Code = " & ParametrosReporte(2)
        End If
        'filtro Periodo
        If ParametrosReporte(3) <> 3 Then
            filtroConsulta &= "And Period = " & ParametrosReporte(3)
        End If
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollIncentivePayment)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptIncentivePaymentBasiCallTheIncome_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "PAGO PRIMA SERVICIOS " & CDate(Me.ParametrosReporte(1)).ToString("De MMMM Del yyyy").ToUpper()
    End Sub
End Class