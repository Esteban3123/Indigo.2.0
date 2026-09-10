#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptExpeditionCDPAndRP
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmPayStub"

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    Dim _operatingUnit As OperatingUnit

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "PayrollDateLiquidated >= '" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "' And PayrollDateLiquidated <= '" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "'"

        'filtro tipo de concepto
        If ParametrosReporte(2) IsNot Nothing Then
            filtroConsulta &= "And ConceptType = " & ParametrosReporte(2)
        End If
        'filtro por tipo de empleado
        If ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND Code = '" & ParametrosReporte(3) & "'"
        End If

        Dim sucursalIni As String = IIf(ParametrosReporte(5) Is Nothing Or ParametrosReporte(5) = String.Empty, "NULL", ParametrosReporte(5))
        Dim sucursalFin As String = IIf(ParametrosReporte(6) Is Nothing Or ParametrosReporte(6) = String.Empty, "NULL", ParametrosReporte(6))

        filtroConsulta = String.Format("{0} AND ((BranchOfficeID >= {1} AND BranchOfficeID <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)

        If ParametrosReporte(4) = 1 Then
            XrLTipo.Text = "Nómina"
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of ViewReportExpeditionCDPAndRP)(Nothing, filtroConsulta)
        Else
            XrLTipo.Text = "Primas"
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of ViewReportExpeditionCDPAndRPIncentivePayment)(Nothing, filtroConsulta)
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayStub_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        'INDGleConceptType.EditValue, INDGleEmployeeType.EditValue

        If ParametrosReporte(2) = 1 Then
            XrLabel1.Text = "Relación de todos los devengos"

        End If
        If ParametrosReporte(2) = 1 And ParametrosReporte(3) = "001" Then
            XrLabel1.Text = "Relación de los devengos administrativos"
        ElseIf ParametrosReporte(2) = 1 And ParametrosReporte(3) = "002" Then
            XrLabel1.Text = "Relación de devengos de empleados operativos"
        End If

        If ParametrosReporte(2) = 2 Then
            XrLabel1.Text = "Relación de todos los Deducciones"

        End If
        If ParametrosReporte(2) = 2 And ParametrosReporte(3) = "001" Then
            XrLabel1.Text = "Relación de los Deducidos administrativos"
        ElseIf ParametrosReporte(2) = 2 And ParametrosReporte(3) = "002" Then
            XrLabel1.Text = "Relación de deducciones de empleados operativos"
        End If

        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
    End Sub
End Class