#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
#End Region

Public Class rptAccountPayableTransfers
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDUser As Object
    Private INDUserAccep As Object
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        If ParametrosReporte(1) = 1 Then
            INDCo.Value = "CUENTA POR PAGAR"
        Else
            INDCo.Value = "REEMBOLSOS"
        End If

        Dim filtroConsulta As String = "AccountPayableTransferId.Id = " & ParametrosReporte(0)
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of PaymentsAccountPayableTransferDetailReportXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), PaymentsAccountPayableTransferDetailReportXpo).AccountPayableTransferId.CreationUser.Trim()
        Dim INDAcceptanceUser = CType(INDList(0), PaymentsAccountPayableTransferDetailReportXpo).AcceptanceUser
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        INDUserAccep = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDAcceptanceUser & "'")

        If INDUserAccep.Count > 0 Then
            Me.INDLblAceptanceUser.Text = INDUserAccep(0).CodeName
        Else
            Me.INDLblAceptanceUser.Text = ""
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

    Private Sub rptAccountPayableTransfers_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        If INDUser.count() > 0 Then
            Me.INDLblCreationUser.Text = INDUser(0).CodeName
            Me.INDLblTransferUser.Text = INDUser(0).CodeName
        End If
    End Sub
End Class