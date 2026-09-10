#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports DevExpress.XtraReports.Parameters
Imports System.Globalization

#End Region

Public Class rptAccountReceivableDocument
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDUser As Object
    Dim IndList
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "AccountReceivableDocumentId.Id = " & ParametrosReporte(0)
        IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableDocumentDetailReportXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(IndList(0), PortfolioAccountReceivableDocumentDetailReportXpo).AccountReceivableDocumentId.CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = IndList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptAccountReceivableDocument_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        If IndList IsNot Nothing AndAlso Not String.IsNullOrEmpty(IndList?(0).AccountReceivableDocumentId.CurrencyId?.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = IndList?(0).AccountReceivableDocumentId.CurrencyId.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat()
            ApplyLocalization(_culture)
        End If

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdReceivableDocument").Value}
            CargarDataSource()
        End If

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        If INDUser.count() > 0 Then
            INDUserCreate.Text = INDUser(0).CodeName
        End If

    End Sub
End Class