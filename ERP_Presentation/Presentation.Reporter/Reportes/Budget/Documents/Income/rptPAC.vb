#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptPAC
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private INDUser As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim rubros = ParametrosReporte(0)
        Dim filtroConsulta = "CategoryId.Id = " & (CType(rubros(0), Domain.Entities.AnnualizedCashFlow).CategoryId)
        Dim INDList As List(Of BudgetAnnualizedCashFlowReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of BudgetAnnualizedCashFlowReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then
            Dim INDCodeUser = CType(INDList(0), BudgetAnnualizedCashFlowReportXpo).CreationUser.Trim()
            INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
            If INDUser.count() > 0 Then
                INDUserCreate.Text = INDUser(0).CodeName
            End If
        End If
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPAC_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
    End Sub
End Class