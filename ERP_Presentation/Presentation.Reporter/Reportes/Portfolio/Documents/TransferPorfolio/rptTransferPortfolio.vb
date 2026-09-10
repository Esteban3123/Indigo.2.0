#Region "Imports"

Imports System.Globalization
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.Base

#End Region

Public Class rptTransferPortfolio
    Implements IReport

#Region "Properties"

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property listDataSource As List(Of PortfolioTransferReportXpo)

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            If listDataSource?.Any() Then
                Me.DataSource = listDataSource.FindAll(Function(d) d.Id = ParametrosReporte(0))
            Else
                Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
                Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioTransferReportXpo)(Nothing, filtroConsulta)
             End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

#End Region

#Region "Methods"

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#Region "Events"

    Private Sub rptTransferPortfolio_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdTransferP").Value}
            If DirectCast(ParametrosFilter("FlagReport").Value, Boolean) AndAlso TryCast(Me.DataSource, List(Of PortfolioTransferReportXpo))?.Any(Function(x) x.Id = ParametrosReporte(0)) Then
                Me.listDataSource = Me.DataSource
            End If
            CargarDataSource()
        End If

        Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & GetCurrentColumnValue("CreationUser") & "'")
        If INDListUser IsNot Nothing AndAlso INDListUser.Any() Then
            INDUserCreate.Text = INDListUser(0).CodeName
        End If

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit

        '---Se establece el numbert fortmat al reporte dependiendo de la moneda
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = TryCast(Me.DataSource, List(Of PortfolioTransferReportXpo))?.FirstOrDefault.PortfolioAdvanceId.Abbreviation
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        ApplyLocalization(_culture)
    End Sub

    'INDCfDebitValue
    Private Sub XrTableCell31_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell31.BeforePrint
        Dim row = TryCast(DetailReport.GetCurrentRow(), Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferOtherConceptReportXpo)

        If row IsNot Nothing Then
            Dim value = IIf(row.Nature = 1, row.Value, 0)
            XrTableCell31.Text = Utils.GetMoneyWithISO4217(value, row.PortfolioTransferId.PortfolioAdvanceId.Abbreviation)
        End If
    End Sub

    'Total Facturas
    Private Sub XrTableCell47_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell47.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell47.Text = Utils.GetMoneyWithISO4217(GetCurrentColumnValue("INDCfTotalDetail").ToString(),
                                                       DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Abbreviation)

    End Sub

    'total grupo facturas
    Private Sub XrTableCell37_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell37.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell37.Text = Utils.GetMoneyWithISO4217(DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Value,
                                                       DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Abbreviation)
    End Sub

    'CreditValue
    Private Sub XrTableCell34_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell34.BeforePrint
        Dim row = TryCast(DetailReport.GetCurrentRow(), Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferOtherConceptReportXpo)

        If row IsNot Nothing Then
            Dim value = IIf(row.Nature = 1, 0, row.Value)
            XrTableCell34.Text = Utils.GetMoneyWithISO4217(value, row.PortfolioTransferId.PortfolioAdvanceId.Abbreviation)
        End If
    End Sub

    'total otros conceptos
    Private Sub XrTableCell42_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell42.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell42.Text = Utils.GetMoneyWithISO4217(GetCurrentColumnValue("INDCfTotalOtherConcept").ToString(),
                                                       DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Abbreviation)
    End Sub

    'anticipo
    Private Sub XrTableCell45_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell45.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell45.Text = Utils.GetMoneyWithISO4217(GetCurrentColumnValue("INDCfTotalTransfer").ToString(),
                                                       DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Abbreviation)
    End Sub

    'valor abono
    Private Sub XrTableCell41_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell41.BeforePrint
        Dim row = TryCast(DetailReport1.GetCurrentRow(), Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferDetailReportXpo)

        If row IsNot Nothing Then
            XrTableCell41.Text = Utils.GetMoneyWithISO4217(row.Value, row.PortfolioTrasferId.PortfolioAdvanceId.Abbreviation)
        End If
    End Sub

    Private Sub XrTableCell26_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell26.BeforePrint
        Dim row = TryCast(DetailReport3.GetCurrentRow(), Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferDetailReportXpo)

        If row IsNot Nothing Then
            XrTableCell26.Text = Utils.GetMoneyWithISO4217(row.Value, row.PortfolioTrasferId.PortfolioAdvanceId.Abbreviation)
        End If
    End Sub


    'INDCfTotalTransfer
    Private Sub XrTableCell55_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell55.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell55.Text = Utils.GetMoneyWithISO4217(GetCurrentColumnValue("INDCfTotalTransfer").ToString(),
                                                       DirectCast(row, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioTransferReportXpo).PortfolioAdvanceId.Abbreviation)
    End Sub

#End Region

End Class