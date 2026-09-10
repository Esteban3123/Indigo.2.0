#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptPlaneTreasuryIncentivePayment
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "PeriodInitialDate >= '" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "' AND PeriodEndDate <= '" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "'"

        'Se filtra por periodo
        If ParametrosReporte(2) <> 3 Then
            filtroConsulta &= " AND Period = " & ParametrosReporte(2)
        End If

        'filtro por Grupo
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " AND GroupCode >= '" & ParametrosReporte(3) & "' AND GroupCode <= '" & ParametrosReporte(4) & "'"
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVPlaneTreasuryIncentivePaymentReportXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPlaneTreasuryIncentivePayment_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
End Class