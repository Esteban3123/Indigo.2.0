#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.CloudAgent


#End Region

Public Class rptLogisticsProductionCenterRecord
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim header As InteropCostLogisticsProductionCenterRecordReportXpo

    Dim periodo As String
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        header = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InteropCostService.GetLogisticsProductionCenterRecordById(CInt(ParametrosReporte(0)))
        XrTableCell16.Text = header.Code.ToString & " - " & header.Description.ToString
        XrTableCell19.Text = Format(header.CreationDate, "yyyy-MM-dd")
        XrTableCell10.Text = header.ProductionCenterId.Code & " - " & header.ProductionCenterId.Name

        Dim INDNameUser = header.CreationUser.Trim
        Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")
        If INDListUser IsNot Nothing Then
            Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
            Me.INDUserCreate.Text = INDCodName
        End If

        Dim filtroConsulta As String = " CenterRecordId = '" & ParametrosReporte(0) & "'"
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of InteropCostViewLogisticsProductionCenterRecordDetailReportXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub INDLblCompany_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblCompany.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        periodo = header.RecordDate
        INDLblDateMonth.Text = "PRODUCCIÓN DE CENTROS LÓGICOS DEL " & CDate(periodo).ToString("dd De MMMM Del yyyy").ToUpper()
    End Sub
End Class