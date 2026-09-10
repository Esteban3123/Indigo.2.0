#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraRichEdit.Model
Imports System.Configuration
Imports DevExpress.DataAccess
Imports DevExpress.DataAccess.ConnectionParameters
Imports Presentation.Base
Imports System.Data.SqlClient
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports System.Globalization
#End Region

Public Class rptGeneralBalanceComparative
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReportGeneralBalanceStandard As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Dim currencyAbbreviation As String

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task
        Try
            criterias = ParametrosReporte(0)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportGeneralBalanceAsync(criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportGeneralBalanceStandard = ds.Tables("ReportGeneralBalance")
                Me.DataSource = dtReportGeneralBalanceStandard
                Me.DataMember = "ReportGeneralBalance"

                Dim book
                If Not String.IsNullOrEmpty(criterias.Item("LegalBookId")) Then
                    Dim legalBookId = CDec(criterias.Item("LegalBookId"))
                    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
                        IndigoSessionValues.AuditMessageWcf.Functional = Tag
                        Dim mess As New MessageHeader(Of AuditMessage)(IndigoSessionValues.AuditMessageWcf)
                        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
                        OperationContext.Current.OutgoingMessageHeaders.Add(header)
                        book = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBookByIdAsync(legalBookId)
                    End Using
                    If book.ObjectEmbbeded IsNot Nothing Then
                        currencyAbbreviation = book.ObjectEmbbeded.Currency.Abbreviation
                    End If
                End If
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

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

    Private Sub RemoveColumns(numberAccounts As Boolean)
        'El ancho de las otras columnas se sumara al del nombre de la cuenta contable
        Dim widthToAddToHeader As Decimal = 0
        Dim widthToAddToDetail As Decimal = 0

        If Not numberAccounts Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchMainAccountCode")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdMainAccountCode")
        End If
    End Sub

    Private Function RemoveCell(ColumnName As String) As Decimal
        Dim width As Decimal = 0

        'Cabecera
        Dim RowDescription As XRTableRow = INDTblDescription.Rows(0)
        If RowDescription.Cells(ColumnName) IsNot Nothing Then
            Dim control = Me.FindControl(ColumnName, True)
            width = control.SizeF.Width
            RowDescription.Cells.Remove(control)
        End If

        'Detalle
        Dim RowSubAuxiliar As XRTableRow = INDTblSubAuxiliar.Rows(0)
        If RowSubAuxiliar.Cells(ColumnName) IsNot Nothing Then
            Dim control = Me.FindControl(ColumnName, True)
            width = control.SizeF.Width
            RowSubAuxiliar.Cells.Remove(control)
        End If

        Return width
    End Function

#End Region

#Region "Events"

    Private Sub rptGeneralBalanceComparative_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim title = criterias("Title")
        Dim initialDate = New Date(criterias("YearInitial"), criterias("MonthInitial"), 1)
        Dim endDate = New Date(criterias("YearFinal"), criterias("MonthFinal"), 1)
        Dim numberAccounts As Boolean = CType(criterias("NumberAccounts"), Boolean)

        'Cargar los valores del titulo
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblTitle.Text = title
        Me.INDLblDate.Text = "Periodo Inicial " & initialDate.ToString("MMM yyyy") & " - " & "Periodo Final " & endDate.ToString("MMM yyyy")
        Me.INDtchInitialPeriod.Text = initialDate.ToString("MMM yyyy")
        Me.INDtchFinalPeriod.Text = endDate.ToString("MMM yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Dim _culture As CultureInfo
        _culture = CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New CultureInfo(currencyAbbreviation.GetCultureId).NumberFormat
        Me.ApplyLocalization(_culture)

        Me.RemoveColumns(numberAccounts)
    End Sub

    Private Sub XrTableRow12_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow12.BeforePrint
        Dim level = GetCurrentColumnValue("MainAccountLevel")
        If String.IsNullOrEmpty(level.ToString()) OrElse level = 1 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub XrTableRow3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow3.BeforePrint, XrTableRow4.BeforePrint
        Dim availability = GetCurrentColumnValue("MainAccountAvailability")
        If String.IsNullOrEmpty(availability.ToString()) OrElse availability = 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub XrTableCell11_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell11.SummaryGetResult, XrTableCell12.SummaryGetResult, XrTableCell13.SummaryGetResult, XrTableCell15.SummaryGetResult, XrTableCell16.SummaryGetResult, XrTableCell17.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(currencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, currencyAbbreviation))
        e.Handled = True
    End Sub

#End Region

End Class