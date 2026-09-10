#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities

#End Region


Public Class rptSchedule
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmContractLiquidation"

    Dim filtro As String

    Dim ListHeader As List(Of PayrollSchedule)

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim period As String = Me.ParametrosReporte(1).ToString
        If Me.ParametrosReporte(1).ToString.Length = 1 Then
            period = "0" & Me.ParametrosReporte(1).ToString & "/"
        Else
            period = Me.ParametrosReporte(1).ToString & "/"
        End If
        period &= Me.ParametrosReporte(2).ToString()
        filtro = "Period = '" & period & "'"

        'filtro por Unidad funcional
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtro &= " AND FunctionalUnitId >= '" & ParametrosReporte(4) & "' AND FunctionalUnitId <= '" & ParametrosReporte(5) & "'"
        End If

        'Filtro de Empleados
        If ParametrosReporte(0) IsNot Nothing Then
            filtro &= " And EmployeeId = " & Me.ParametrosReporte(0)
        End If

        Dim sucursalIni As String = "NULL"
        Dim sucursalFin As String = "NULL"

        If ParametrosReporte(6) IsNot Nothing Then
            sucursalIni = ParametrosReporte(6).ToString()
        End If

        If ParametrosReporte(7) IsNot Nothing Then
            sucursalFin = ParametrosReporte(7).ToString()
        End If

        filtro = String.Format("{0} AND ((FunctionalUnitId.BranchOfficeId >= {1} AND FunctionalUnitId.BranchOfficeId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtro, sucursalIni, sucursalFin)

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollSchedule)(Nothing, filtro)

        ListHeader = DataSource

        If Me.ParametrosReporte(3) IsNot Nothing Then
            Me.INDlblAddress.Text = If(Me.ParametrosReporte(3).Address IsNot Nothing, Me.ParametrosReporte(3).Address.Trim() & If(Me.ParametrosReporte(3).City IsNot Nothing, If(Me.ParametrosReporte(3).City.Name IsNot Nothing, " " & Me.ParametrosReporte(3).City.Name.Trim() & If(Me.ParametrosReporte(3).City.Department IsNot Nothing, " - " & Me.ParametrosReporte(3).City.Department.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me.ParametrosReporte(3).Phone IsNot Nothing, Me.ParametrosReporte(3).Phone.Trim() & If(Me.ParametrosReporte(3).EmailAudit IsNot Nothing, " - " & Me.ParametrosReporte(3).EmailAudit.Trim(), String.Empty), String.Empty)
        End If

        INDLblDateMonth.Text = "CUADRO DE TURNO DEL MES DE " & CDate(period).ToString(" MMMM DE yyyy").ToUpper()
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Private Sub rptContractLiquidation_AfterPrint(sender As Object, e As EventArgs) Handles MyBase.AfterPrint


    End Sub

    Private Sub INDlblTotalPagar_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub rptContractLiquidation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
    End Sub

    Dim acumulado As Decimal = 0
    Private Sub rptContractLiquidation_DataSourceRowChanged(sender As Object, e As DevExpress.XtraReports.UI.DataSourceRowEventArgs) Handles MyBase.DataSourceRowChanged

        If ListHeader.Count > 0 AndAlso ListHeader IsNot Nothing Then

            Dim payrollScehduleId = (GetCurrentColumnValue("Id"))
            Dim item = ListHeader.Where(Function(d) d.Id = payrollScehduleId).FirstOrDefault() 'Filtramos por el id par ano limitar al filtro


            Dim HorasNormales As Integer = 0
            Dim HorasEventos As Integer = 0

            If item.D01 IsNot Nothing Then
                HorasNormales = item.D01.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = item.D01.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D02 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D02.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D02.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D03 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D03.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D03.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D04 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D04.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D04.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D05 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D05.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D05.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D06 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D06.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D06.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D07 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D07.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D07.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D08 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D08.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D08.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D09 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D09.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D09.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D10 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D10.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D10.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D11 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D11.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D11.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D12 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D12.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D12.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D13 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D13.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D13.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D14 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D14.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D14.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D15 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D15.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D15.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D16 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D16.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D16.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D17 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D17.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D17.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D18 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D18.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D18.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D19 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D19.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D19.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D20 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D20.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D20.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D21 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D21.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D21.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D22 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D22.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D22.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D23 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D23.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D23.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D24 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D24.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D24.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D25 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D25.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D25.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D26 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D26.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D26.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D27 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D27.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D27.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D28 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D28.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D28.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D29 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D29.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D29.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D30 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D30.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D30.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            If item.D31 IsNot Nothing Then
                HorasNormales = HorasNormales + item.D31.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = False).Sum(Function(x) x.TotalNumberHours)
                HorasEventos = HorasEventos + item.D31.Payroll_ScheduleDetailHour.Where(Function(x) x.Event1 = True And x.Approved = True).Sum(Function(x) x.TotalNumberHours)
            End If

            xrLabel33.Text = HorasNormales
            XrLabel38.Text = HorasEventos

        End If

    End Sub

    Private Sub INDlblTotalPagar_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property
End Class