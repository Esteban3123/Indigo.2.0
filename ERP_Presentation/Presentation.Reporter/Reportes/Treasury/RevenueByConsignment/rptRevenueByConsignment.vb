#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptRevenueByConsignment
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(IdCashReceipt.DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(IdCashReceipt.DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "# AND PaymentMethodTypes = 4"

        'Se filtra por Cuentas
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " AND IdEntityBankAccount.Code >= '" & ParametrosReporte(3) & "' AND IdEntityBankAccount.Code <= '" & ParametrosReporte(4) & "'"
        End If

        'Se filtra por Usuarios
        If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
            filtroConsulta &= " AND IdCashReceipt.CreationUser >= '" & ParametrosReporte(5) & "' AND IdCashReceipt.CreationUser <= '" & ParametrosReporte(6) & "'"
        End If

        If ParametrosReporte(2) <> 4 Then
            filtroConsulta &= "AND IdCashReceipt.Status = " & ParametrosReporte(2)
        End If

        Dim listReport As List(Of TreasuryPaymentMethodsXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryPaymentMethodsXpo)(Nothing, filtroConsulta)
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

    Private Sub rptRevenueByConsignment_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLBlSubtitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
    End Sub
End Class