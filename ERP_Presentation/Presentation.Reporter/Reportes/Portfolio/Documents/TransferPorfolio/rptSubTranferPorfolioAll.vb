#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports System.Globalization

#End Region

Public Class rptSubTranferPorfolioAll
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            'Me.DataSource = ParametrosReporte(0)
            Dim filtroConsulta As String = "DocumentDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND DocumentDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

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

            Dim listReport As List(Of PortfolioTransferReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioTransferReportXpo)(Nothing, filtroConsulta)

            Me.XrSubreport1.ReportSource.DataSource = listReport

            Me.DataSource = listReport

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

    Private Sub XrTableCell37_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        Dim row = GetCurrentRow()
        If e.CalculatedValues.ToEntityList(Of Decimal)?.Any() Then
            e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.PortfolioAdvanceId.Abbreviation),
                                            IndigoSessionValues.CurrencyISO4217, row.PortfolioAdvanceId.Abbreviation))
            e.Handled = True
        End If
    End Sub

    Private Sub GroupFooter1_BeforePrint_1(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupFooter1.BeforePrint
        Me.XrSubreport2.ReportSource.DataSource = TryCast(Me.DataSource, List(Of PortfolioTransferReportXpo))
    End Sub

#End Region

End Class