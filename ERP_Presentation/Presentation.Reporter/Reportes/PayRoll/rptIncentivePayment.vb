#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Payroll.Entities
#End Region

Public Class rptIncentivePayment
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmIncentivePayment"

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "PeriodEndDate >= #" & Format(Me.ParametrosReporte(1), "yyyy-MM-dd") & "# And PeriodEndDate <= #" & Format(Me.ParametrosReporte(2), "yyyy-MM-dd") & "#"

        Dim filtroGrupos As String = String.Empty
        If Me.ParametrosReporte(0) IsNot Nothing Then

            If Me.ParametrosReporte(0).Count > 0 Then

                filtroGrupos = "  And ("
                For Each grupo As Group In Me.ParametrosReporte(0)
                    filtroGrupos += " ContractId.GroupId.Id = " & grupo.Id & " or"
                Next
                filtroConsulta += filtroGrupos
            End If
        End If

        If Not String.IsNullOrEmpty(filtroGrupos) Then
            filtroConsulta = filtroConsulta.Remove(filtroConsulta.Length - 2, 2)
            filtroConsulta += ")"
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollIncentivePayment)(Nothing, filtroConsulta)
        If Me.ParametrosReporte(3) IsNot Nothing Then
            Me.INDlblAddress.Text = If(Me.ParametrosReporte(3).Address IsNot Nothing, Me.ParametrosReporte(3).Address.Trim() & If(Me.ParametrosReporte(3).City IsNot Nothing, If(Me.ParametrosReporte(3).City.Name IsNot Nothing, " " & Me.ParametrosReporte(3).City.Name.Trim() & If(Me.ParametrosReporte(3).City.Department IsNot Nothing, " - " & Me.ParametrosReporte(3).City.Department.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me.ParametrosReporte(3).Phone IsNot Nothing, Me.ParametrosReporte(3).Phone.Trim() & If(Me.ParametrosReporte(3).EmailAudit IsNot Nothing, " - " & Me.ParametrosReporte(3).EmailAudit.Trim(), String.Empty), String.Empty)
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptIncentivePayment.CNameReport
        End Get
    End Property

    Private Sub rptUnemployment_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDlblFechaInicio.Text = Me.ParametrosReporte(2)
    End Sub
End Class