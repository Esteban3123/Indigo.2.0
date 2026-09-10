#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptTransfersDocumentPayments
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(PaymentTransferId.DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(PaymentTransferId.DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'Filtro por Terceros
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " AND PaymentTransferId.ThirdPartyId.Nit >= '" & ParametrosReporte(3) & "' AND PaymentTransferId.ThirdPartyId.Nit <= '" & ParametrosReporte(4) & "'"
        End If

        'Filtro por Notas
        If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
            filtroConsulta &= " AND PaymentTransferId.Code >= '" & ParametrosReporte(5) & "' AND PaymentTransferId.Code <= '" & ParametrosReporte(6) & "'"
        End If

        If ParametrosReporte(2) <> 4 Then
            filtroConsulta &= " AND PaymentTransferId.Status = " & ParametrosReporte(2)
        End If

        Dim listReport As List(Of PaymentsPaymentTransferDetail) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsPaymentTransferDetail)(Nothing, filtroConsulta)
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

    Private Sub rptTransfersDocumentPayments_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class