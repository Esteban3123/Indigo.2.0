#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptIncentivePaymentBasiCallTheIncome2
    Implements IReport


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        ' Obtener el código del tipo de empleado desde su Id
        Dim employeeTypeId As Integer = CInt(ParametrosReporte(2))
        Dim employeeTypeCode As String = ""

        If employeeTypeId > 0 Then
            Dim employeeType = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetXPOObject(Of PayrollEmployeeTypeXpo)("Id = " & employeeTypeId)
            If employeeType IsNot Nothing Then
                employeeTypeCode = employeeType.Code
            End If
        End If

        Dim filtroConsulta As String = "PeriodInitialDate >= '" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "' And PeriodEndDate <= '" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "'"

        ' Filtrar por código de tipo de empleado si se obtuvo
        If Not String.IsNullOrEmpty(employeeTypeCode) Then
            filtroConsulta &= " AND EmployeeTypeCode = '" & employeeTypeCode & "'"
        End If

        'Detectar origen del reporte mediante bandera explícita:
        Dim sourceForm As String = If(ParametrosReporte.Length > 8 AndAlso ParametrosReporte(8) IsNot Nothing, ParametrosReporte(8).ToString(), "")
        Dim hasEmployee As Boolean = (sourceForm = "FrmRelationIncentivePayments")

        If hasEmployee Then
            'filtro por empleado
            If ParametrosReporte(3) IsNot Nothing Then
                filtroConsulta &= " AND EmployeeId = " & ParametrosReporte(3)
            End If

            'filtro Periodo (posición 4 cuando hay empleado)
            If ParametrosReporte(4) IsNot Nothing AndAlso ParametrosReporte(4) <> 3 Then
                filtroConsulta &= " And Period = " & ParametrosReporte(4)
            End If

            'filtro Sucursales (no hay filtro de grupos cuando no se especifica grupo en FrmRelationIncentivePayments)
            Dim sucursalIni As String = If(ParametrosReporte(6) IsNot Nothing AndAlso ParametrosReporte(6) <> String.Empty, ParametrosReporte(6).ToString(), "NULL")
            Dim sucursalFin As String = If(ParametrosReporte(7) IsNot Nothing AndAlso ParametrosReporte(7) <> String.Empty, ParametrosReporte(7).ToString(), "NULL")
            filtroConsulta = String.Format("{0} AND ((BranchOfficeID >= {1} AND BranchOfficeID <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

        Else
            'Desde FrmReportIncentivePayment (SIN empleado)

            'filtro Periodo (posición 3 cuando NO hay empleado)
            If ParametrosReporte(3) IsNot Nothing AndAlso ParametrosReporte(3) <> 3 Then
                filtroConsulta &= " And Period = " & ParametrosReporte(3)
            End If

            'filtro Grupos
            Dim groupIni As String = If(ParametrosReporte(4) IsNot Nothing, ParametrosReporte(4).ToString(), "NULL")
            Dim groupFin As String = If(ParametrosReporte(5) IsNot Nothing, ParametrosReporte(5).ToString(), "NULL")
            filtroConsulta = String.Format("{0} AND ((GroupId >= {1} AND GroupId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, groupIni, groupFin)

            'filtro Sucursales
            Dim sucursalIni As String = If(ParametrosReporte(6) IsNot Nothing AndAlso ParametrosReporte(6) <> String.Empty, ParametrosReporte(6).ToString(), "NULL")
            Dim sucursalFin As String = If(ParametrosReporte(7) IsNot Nothing AndAlso ParametrosReporte(7) <> String.Empty, ParametrosReporte(7).ToString(), "NULL")
            filtroConsulta = String.Format("{0} AND ((BranchOfficeID >= {1} AND BranchOfficeID <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVIncentivePaymentReportXpo)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptIncentivePaymentBasiCallTheIncome2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        'INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        INDLblDate.Text = "PAGO PRIMA SERVICIOS " & CDate(Me.ParametrosReporte(1)).ToString("De MMMM Del yyyy").ToUpper()
    End Sub
End Class