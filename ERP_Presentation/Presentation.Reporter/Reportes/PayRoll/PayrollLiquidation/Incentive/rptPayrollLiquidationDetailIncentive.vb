#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports Presentation.Base
#End Region

Public Class rptPayrollLiquidationDetailIncentive

    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmPayrollControl"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            filtroConsulta = "PeriodInitialDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND PeriodEndDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por estado
            If ParametrosReporte(2) <> "T" Then
                filtroConsulta &= " AND RegisterStatus = '" & ParametrosReporte(2) & "'"
            End If

            'filtro por empleado
            If ParametrosReporte(3) IsNot Nothing Then
                filtroConsulta &= " AND EmployeeId = " & ParametrosReporte(3)
            End If

            'filtro por Grupo
            If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
                filtroConsulta &= " AND GroupCode >= '" & ParametrosReporte(4) & "' AND GroupCode <= '" & ParametrosReporte(5) & "'"
            End If

            'filtro por Unidad funcional
            If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= " AND FunctionalCode >= '" & ParametrosReporte(6) & "' AND FunctionalCode <= '" & ParametrosReporte(7) & "'"
            End If

            'filtro por Concepto
            If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
                filtroConsulta &= " AND ConceptId >= '" & ParametrosReporte(8) & "' AND ConceptId <= '" & ParametrosReporte(9) & "'"
            End If

            'filtro Periodo
            If ParametrosReporte(10) <> 3 Then
                filtroConsulta &= " AND Period = " & ParametrosReporte(10)
            End If

            Dim sucursalIni As String = IIf(ParametrosReporte(11) Is Nothing Or CStr(ParametrosReporte(11)) = String.Empty, "NULL", CStr(ParametrosReporte(11)))
            Dim sucursalFin As String = IIf(ParametrosReporte(12) Is Nothing Or CStr(ParametrosReporte(12)) = String.Empty, "NULL", CStr(ParametrosReporte(12)))

            filtroConsulta = String.Format("{0} AND ((BranchOfficeId >= {1} AND BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of ViewReportTotalPaidEmployeeIncentiveConcept)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayrollLiquidationDetailIncentive_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
End Class