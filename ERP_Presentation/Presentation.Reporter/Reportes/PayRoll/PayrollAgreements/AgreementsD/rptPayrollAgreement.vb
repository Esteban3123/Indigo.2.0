#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports Presentation.Base
#End Region


Public Class rptPayrollAgreement
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmPayrollControl"


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "GetDate(DatePayment) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DatePayment) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por Grupo
            If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
                filtroConsulta &= " And AgreementsCId.GroupId.Code >= '" & Me.ParametrosReporte(2) & "' AND AgreementsCId.GroupId.Code <= '" & ParametrosReporte(3) & "'"
            End If

            'filtro por Empleado
            If ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " And AgreementsCId.EmployeeId = " & ParametrosReporte(4)
            End If

            'filtro por estado
            If ParametrosReporte(5) <> 6 Then
                filtroConsulta &= " AND AgreementsCId.State = '" & ParametrosReporte(5) & "'"
            End If

            'filtro por Entidad
            If ParametrosReporte(7) IsNot Nothing And ParametrosReporte(8) IsNot Nothing Then
                filtroConsulta &= " And AgreementsCId.CompanyId.Nit >= '" & Me.ParametrosReporte(7) & "' AND AgreementsCId.CompanyId.Nit <= '" & ParametrosReporte(8) & "'"
            End If

            'filtro por surcursal
            If ParametrosReporte(9) IsNot Nothing AndAlso ParametrosReporte(10) IsNot Nothing Then
                filtroConsulta &= " AND ((AgreementsCId.EmployeeId.Payroll_Contract[Status = 1 AND FunctionalUnitId.BranchOfficeId >= " & ParametrosReporte(9) & " AND FunctionalUnitId.BranchOfficeId <= " & ParametrosReporte(10) & "]))"
            End If

            ' Obtener datos y pre-calcular campos
            Dim rawData = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollAgreementsDReportXpo)(Nothing, filtroConsulta)
            Dim payrollDate = If(rawData.FirstOrDefault()?.PayrollLiquidationDetail?.FirstOrDefault()?.PayrollDate, Date.MinValue)
            Dim optimizedData = rawData.Select(Function(item) New With {
                .DatePayment = item.DatePayment,
                .ShareValuePaid = item.ShareValuePaid,
                .TypePayment = item.TypePayment,
                .PayrollLiquidationDetailPayrollDate = payrollDate,
                .AgreementsCIdConsecutive = If(item.AgreementsCId?.Consecutive, ""),
                .AgreementsCIdEmployeeIdThirdPartyIdNit = If(item.AgreementsCId?.EmployeeId?.ThirdPartyId?.Nit, ""),
                .AgreementsCIdEmployeeIdThirdPartyIdName = If(item.AgreementsCId?.EmployeeId?.ThirdPartyId?.Name, ""),
                .AgreementsCIdAgreementValue = If(item.AgreementsCId?.AgreementValue, 0D),
                .AgreementsCIdLiquidationType = If(item.AgreementsCId?.LiquidationType, ""),
                .AgreementsCIdState = item.AgreementsCId?.State,
            .INDLiquidationType = GetLiquidationTypeText(item.AgreementsCId?.LiquidationType),
                .INDCfState = GetStateText(item.AgreementsCId?.State),
                .INDCfTypePayment = GetTypePaymentText(item.TypePayment)
            }).ToList()

            Me.DataSource = optimizedData

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    ''' <summary>
    ''' Reemplaza: Iif([AgreementsCId.LiquidationType] = '1','Con Monto', 'Sin Monto')
    ''' </summary>
    Private Function GetLiquidationTypeText(liquidationType As String) As String
        If liquidationType = "1" Then
            Return "Con Monto"
        Else
            Return "Sin Monto"
        End If
    End Function

    ''' <summary>
    ''' Reemplaza: Iif([State]=1, 'Sin Confirmar',Iif([State]=2,'Confirmado',Iif([State]=3,'Suspendido',...)))
    ''' </summary>
    Private Function GetStateText(state As Integer?) As String
        Select Case state
            Case 1
                Return "Sin Confirmar"
            Case 2
                Return "Confirmado"
            Case 3
                Return "Suspendido"
            Case 4
                Return "Terminado"
            Case Else
                Return "Anulado"
        End Select
    End Function

    ''' <summary>
    ''' Reemplaza: Iif([TypePayment]=1,'Por Nomina',Iif([TypePayment]=2, 'Manual',...)))
    ''' </summary>
    Private Function GetTypePaymentText(typePayment As Integer?) As String
        Select Case typePayment
            Case 1
                Return "Por Nomina"
            Case 2
                Return "Manual"
            Case 3
                Return "Por Archivo"
            Case 4
                Return "Por Vacaciones"
            Case Else
                Return "Por Liquidación de Contrato"
        End Select
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
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

    Private Sub rptPayrollAgreementsC_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDlblFechaInicio.Text = Me.ParametrosReporte(0)
        INDlblFechaFin.Text = Me.ParametrosReporte(1)
        INDLblState.Text = Me.ParametrosReporte(6)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint

    End Sub
End Class