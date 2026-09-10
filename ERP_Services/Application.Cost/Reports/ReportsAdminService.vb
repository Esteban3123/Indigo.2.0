#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.SqlClient
Imports System.Text

#End Region

Public Class ReportsAdminService
    Implements IReportsAdminService

#Region "Methods"

    Public Function GetReportEstimateCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportsAdminService.GetReportEstimateCosts
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Cost].[SP_ReportEstimateCosts] '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportEstimateCosts")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetListReportActivityCosts(filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportsAdminService.GetListReportActivityCosts
        Try
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Cost].[SP_ReportActivityCosts] '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportActivityCosts")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportComparativeCosts(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportsAdminService.GetReportComparativeCosts
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim xmlFilters = Utils.DictionaryToXML(filters)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Cost].[SP_ReportComparativeCosts] '" & xmlCriterias & "', '" & xmlFilters & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportComparativeCosts")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportResultProductionCostsExpenses(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportsAdminService.GetReportResultProductionCostsExpenses
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Cost].[SP_CostReportResultProductionCostsExpenses] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportResultProductionCostsExpenses")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportResultProductionCostsExpensesDetail(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportsAdminService.GetReportResultProductionCostsExpensesDetail
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Cost].[SP_CostReportResultProductionCostsExpensesDetail] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportResultProductionCostsExpensesDetail")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Methods Privates"

    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 36000
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
