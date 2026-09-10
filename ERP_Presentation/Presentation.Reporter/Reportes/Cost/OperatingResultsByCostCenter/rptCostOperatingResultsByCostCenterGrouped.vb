#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.CloudAgent

#End Region
Public Class rptCostOperatingResultsByCostCenterGrouped
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim fechaIni As Date
    Dim fechafin As Date

    Dim ListReportOperatingResult As List(Of SP_CostReportOperatingResult_Result)
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        fechaIni = New Date(Me.ParametrosReporte(0), Me.ParametrosReporte(1), 1)
        fechafin = New Date(Me.ParametrosReporte(2), Me.ParametrosReporte(3), 1)

        ListReportOperatingResult = IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListReportOperatingResult(CInt(ParametrosReporte(1)), CInt(ParametrosReporte(3)), CInt(ParametrosReporte(0)), CStr(ParametrosReporte(4)), CStr(ParametrosReporte(5)), CInt(ParametrosReporte(6)))

        Dim filtroConsulta As String = "Year >= '" & Me.ParametrosReporte(0) & "' And Year <= '" & Me.ParametrosReporte(2) & "'"
        filtroConsulta &= " And Month >= '" & Me.ParametrosReporte(1) & "' And Month <= '" & Me.ParametrosReporte(3) & "'"
        'filtro por centro de producción 
        If ParametrosReporte(4) <> String.Empty And ParametrosReporte(5) <> String.Empty Then
            filtroConsulta &= " AND Code >= '" & ParametrosReporte(4) & "' AND Code <= '" & ParametrosReporte(5) & "'"
        End If
        ''filtro por Tipo Centro Producción
        'If ParametrosReporte(4) Is Nothing And ParametrosReporte(5) Is Nothing Then
        '    filtroConsulta &= " And CenterType = 1"
        'End If
        'Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of InteropCostViewReportGeneralProfitabilityTotalCost)(Nothing, filtroConsulta)

        Me.DataSource = ListReportOperatingResult
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptOperatingResultsByCostCenter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "DESDE " & fechaIni.ToString(" MMMM DE yyyy").ToUpper() & "  HASTA " & fechafin.ToString(" MMMM DE yyyy").ToUpper()
    End Sub
End Class