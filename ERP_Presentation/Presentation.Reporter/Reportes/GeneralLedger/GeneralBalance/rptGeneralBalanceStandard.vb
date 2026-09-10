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

Public Class rptGeneralBalanceStandard
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

    Private Sub RemoveColumns(accountLevel As Integer, detailingThird As Boolean, numberAccounts As Boolean)
        'El ancho de las otras columnas se sumara al del nombre de la cuenta contable
        Dim widthToAddToHeader As Decimal = 0
        Dim widthToAddToDetail As Decimal = 0

        If Not numberAccounts Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchMainAccountCode")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdMainAccountCode")
        End If

        If Not detailingThird OrElse accountLevel < 5 Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchThirdParty")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdThirdParty")
        End If

        If accountLevel < 5 Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchMainAuxiliary")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdMainAuxiliary")
        End If

        If accountLevel < 4 Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchSubAccount")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdSubAccount")
        End If

        If accountLevel < 3 Then
            widthToAddToHeader = widthToAddToHeader + Me.RemoveCell("INDtchAccount")
            widthToAddToDetail = widthToAddToDetail + Me.RemoveCell("INDtcdAccount")
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

    Private Sub rptGeneralBalanceStandard_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim title = criterias("Title")
        Dim endDate = New Date(criterias("YearFinal"), criterias("MonthFinal"), 1)
        Dim endPeriod = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, endDate))
        Dim accountLevel As Integer = CType(criterias("AccountLevel"), Integer)
        Dim detailingThird As Boolean = CType(criterias("DetailingThird"), Boolean)
        Dim numberAccounts As Boolean = CType(criterias("NumberAccounts"), Boolean)

        'Cargar los valores del titulo
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblTitle.Text = title
        Me.INDLblDate.Text = "Periodo Contable Terminado el " & endPeriod.ToString("dd \de MMMM \de yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblBook.Text = criterias("LegalBookName")

        Dim _culture As CultureInfo
        _culture = CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = currencyAbbreviation.GetNumberFormat
        Me.ApplyLocalization(_culture)

        Me.RemoveColumns(accountLevel, detailingThird, numberAccounts)
    End Sub

    Private Sub XrTableRow7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow7.BeforePrint
        Dim thirdPartyId = GetCurrentColumnValue("ThirdPartyId")
        If String.IsNullOrEmpty(thirdPartyId.ToString()) Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

End Class