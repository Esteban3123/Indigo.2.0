#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptListAccountsPayableByBudget
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        Dim status = ParametrosReporte(2)
        Dim distributionLines As String = ParametrosReporte(3)
        Dim suppliers As String = ParametrosReporte(4)
        Dim accountPayables As String = ParametrosReporte(5)
        Dim budgets As String = ParametrosReporte(6)

        If status IsNot Nothing Then
            filtroConsulta &= String.Format(" AND Status IN ({0})", status)
        End If

        If Not String.IsNullOrEmpty(distributionLines) Then
            filtroConsulta &= String.Format(" AND IdSuppliersDistributionLines.IdDistributionLine.Id IN ({0})", distributionLines)
        End If

        If Not String.IsNullOrEmpty(suppliers) Then
            filtroConsulta &= String.Format(" AND IdSupplier.Id IN ({0})", suppliers)
        End If

        If Not String.IsNullOrEmpty(accountPayables) Then
            filtroConsulta &= String.Format(" AND Id IN ({0})", accountPayables)
        End If

        If Not String.IsNullOrEmpty(budgets) Then
            filtroConsulta &= String.Format(" AND ViewAccountPayableCommitment[BudgetId IN ({0})]", budgets)
        End If

        Dim listReport As List(Of PaymentsAccountPayable) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsAccountPayable)(Nothing, filtroConsulta)
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

    Private Sub rptListAccountsPayableByBudget_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    Private Sub DetailReport_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DetailReport.BeforePrint
        If Me.DetailReport.GetCurrentColumnValue("Id") Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub
End Class