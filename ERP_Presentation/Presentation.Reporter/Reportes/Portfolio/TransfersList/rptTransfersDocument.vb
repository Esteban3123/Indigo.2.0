#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptTransfersDocument
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(PortfolioTrasferId.DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(PortfolioTrasferId.DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'filtro por terceros
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= "AND PortfolioTrasferId.ThirdPartyId.Nit >= '" & ParametrosReporte(3) & "' AND PortfolioTrasferId.ThirdPartyId.Nit <= '" & ParametrosReporte(4) & "'"
        End If

        'filtro por Traslados
        If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
            filtroConsulta &= "AND PortfolioTrasferId.Code >= '" & ParametrosReporte(5) & "' AND PortfolioTrasferId.Code <= '" & ParametrosReporte(6) & "'"
        End If

        If ParametrosReporte(2) <> 4 Then
            filtroConsulta &= " AND PortfolioTrasferId.Status = " & ParametrosReporte(2)
        End If

        Dim listReport As List(Of PortfolioTransferDetailReportXpo) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioTransferDetailReportXpo)(Nothing, filtroConsulta)
        Me.DataSource = listReport
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptTransfersDocument_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class