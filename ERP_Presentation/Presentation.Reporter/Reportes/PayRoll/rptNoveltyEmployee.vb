#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptNoveltyEmployee
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Payroll.FrmNoveltyEmployee"

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptNoveltyEmployee_DataSourceRowChanged(sender As Object, e As DevExpress.XtraReports.UI.DataSourceRowEventArgs) Handles MyBase.DataSourceRowChanged

        Dim tipoNovedad As Int16 = GetCurrentColumnValue("TypeNovelty")
        If tipoNovedad = 1 Then
            lblTipoNovedad.Text = "Incapacidad"
        ElseIf tipoNovedad = 2 Then
            lblTipoNovedad.Text = "Sancion"
        ElseIf tipoNovedad = 3 Then
            lblTipoNovedad.Text = "Licencia"
        End If

        Dim estado As Int16 = GetCurrentColumnValue("Status")
        If estado = 0 Then
            INDlblEstado.Text = "Normal"
        ElseIf estado = 1 Then
            INDlblEstado.Text = "Liquidada"
        ElseIf estado = 2 Then
            INDlblEstado.Text = "Parcialmente Liquidada"
        End If

    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollNovelty)(Nothing, "EmployeeId.Id= " & Me.ParametrosReporte(0) & "  And EmployeeId.Payroll_Contract[Status = 1] ")
        ''verificar datos para cargar mostrar informacion de la unidad operativa
        If Me.ParametrosReporte(1) IsNot Nothing Then
            Me.INDlblAddress.Text = If(Me.ParametrosReporte(1).Address IsNot Nothing, Me.ParametrosReporte(1).Address.Trim() & If(Me.ParametrosReporte(1).City IsNot Nothing, If(Me.ParametrosReporte(1).City.Name IsNot Nothing, " " & Me.ParametrosReporte(1).City.Name.Trim() & If(Me.ParametrosReporte(1).City.Department IsNot Nothing, " - " & Me.ParametrosReporte(1).City.Department.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me.ParametrosReporte(1).Phone IsNot Nothing, Me.ParametrosReporte(1).Phone.Trim() & If(Me.ParametrosReporte(1).EmailAudit IsNot Nothing, " - " & Me.ParametrosReporte(1).EmailAudit.Trim(), String.Empty), String.Empty)
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptNoveltyEmployee.CNameReport
        End Get
    End Property

    Public Sub New(ByVal operatingUnit As OperatingUnit)
        ' This call is required by the designer.
        InitializeComponent()
        Me.ParametrosReporte(1) = operatingUnit

    End Sub

End Class