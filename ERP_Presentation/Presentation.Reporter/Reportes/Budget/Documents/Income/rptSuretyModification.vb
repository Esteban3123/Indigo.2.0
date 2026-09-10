#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters
#End Region


Public Class rptSuretyModification
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private INDUser As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta = "Id = " & ParametrosReporte(0)
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BudgetService.GetCollection(Of BudgetCollectionModificationReportXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), BudgetCollectionModificationReportXpo).CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).BudgetService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
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

    Private Sub rptSuretyModification_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDPrIdCollectionSubreport").Value}
            CargarDataSource()
        End If

        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        If INDUser.count() > 0 Then
            INDUserCreate.Text = If(INDUser Is Nothing, String.Empty, INDUser(0).CodeName)
        End If
    End Sub
End Class