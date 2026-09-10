#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Text

#End Region

Public Class ReportAdminService
    Implements IReportAdminService

#Region "Builder"

    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    Public Function GetListReportCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportCircularAccountsReceivable
        Try
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Portfolio].[SP_ReportCircularAccountsReceivable] '" & Session.HisContainer & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportCircularAccountsReceivable")
            ds.Tables.Add(dt.Copy())

            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GenerateFileCircularAccountsReceivable(filters As Dictionary(Of String, String), Session As SessionValues) As ActionResult(Of StringBuilder) Implements IReportAdminService.GenerateFileCircularAccountsReceivable
        Dim result As New StringBuilder()
        Dim filename As String = String.Empty

        Dim data As DataSet = Me.GetListReportCircularAccountsReceivable(filters, Session)

        Dim dt As New DataTable
        If data IsNot Nothing Then
            dt = data.Tables("ReportCircularAccountsReceivable")
        End If

        If dt.Rows.Count > 0 Then
            Dim lineHead As String = "Nit,"
            lineHead &= "Digito de Verificacion,"
            lineHead &= "Periodo de Reporte,"
            lineHead &= "Año,"
            lineHead &= "Tipo de Factura,"
            lineHead &= "No. Factura,"
            lineHead &= "Valor Factura,"
            lineHead &= "Codigo EAPB,"
            lineHead &= "Codigo de Municipio,"
            lineHead &= "Acuerdo de Pago"
            result.Append(lineHead)


            Dim EntityNit As String = String.Empty
            Dim EntityDigitVerification As String = String.Empty
            Dim RadicatedYear As Integer
            Dim RadicatedMonth As Integer
            Dim InvoiceType As Integer
            Dim InvoiceNumber As String
            Dim InvoiceValue As Decimal
            Dim HealthEntityCode As String
            Dim MunicipalityCode As String
            Dim PaymentAgreement As String

            'Se reccorre los rows del dataRow para armar el archivo plano
            For Each item As DataRow In dt.Rows
                EntityNit = item("EntityNit")
                EntityDigitVerification = item("EntityDigitVerification")
                RadicatedMonth = item("RadicatedMonth")
                RadicatedYear = item("RadicatedYear")
                InvoiceType = item("InvoiceType")
                InvoiceNumber = item("InvoiceNumber")
                InvoiceValue = item("InvoiceValue")
                HealthEntityCode = item("HealthEntityCode")
                MunicipalityCode = item("MunicipalityCode")
                PaymentAgreement = item("PaymentAgreement")

                Dim lineDet As String = vbCrLf
                lineDet &= EntityNit & ","
                lineDet &= EntityDigitVerification & ","
                lineDet &= RadicatedMonth & ","
                lineDet &= RadicatedYear & ","
                lineDet &= InvoiceType & ","
                lineDet &= InvoiceNumber & ","
                lineDet &= InvoiceValue.ToString().Replace(",", ".") & ","
                lineDet &= HealthEntityCode & ","
                lineDet &= MunicipalityCode & ","
                lineDet &= PaymentAgreement
                result.Append(lineDet)
            Next

            filename = EntityNit & EntityDigitVerification & RadicatedMonth & RadicatedYear & "179"
        End If

        Return New ActionResult(Of StringBuilder) With {.ObjectEmbbeded = result, .Message = filename}
    End Function

    Public Function GetListReportPortfolioByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportPortfolioByAge
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Portfolio].[SP_ReportPortfolioByAge] '" & Session.HisContainer & "', '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportPortfolioByAge")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetListReportPortfolioReconciliation(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportPortfolioReconciliation
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Portfolio].[SP_ReportPortfolioReconciliation] '" & Session.HisContainer & "', '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportPortfolioReconciliation")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetListReportRadicateInvoice(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportRadicateInvoice
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Portfolio].[SP_ReportRadicateInvoice] '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportRadicateInvoice")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetListReportPortfolio2193(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportPortfolio2193
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Portfolio].[SP_ReportPortfolio2193] '" & Session.HisContainer & "', '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportPortfolio2193")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Methods Privates"

    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        If ConfigurationManager.ConnectionStrings("CONX_GENESIS_REPORTS") IsNot Nothing Then
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS, String.Empty, session.TransactionalContainer, False)
        Else
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        End If

        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
